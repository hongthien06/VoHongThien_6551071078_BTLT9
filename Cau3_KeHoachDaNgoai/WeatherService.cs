using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Cau3_KeHoachDaNgoai;

public enum WeatherKind { Clear, Cloudy, Fog, Rain, Thunder, Other }

/// <summary>Giá trị dự báo của đúng một ngày.</summary>
public record WeatherDay(DateOnly Date, int WeatherCode, double TempMin, double TempMax, int? RainProbability);

public enum WeatherError { PlaceNotFound, Timeout, Network, BadData }

public class WeatherException : Exception
{
    public WeatherError Kind { get; }
    public WeatherException(WeatherError kind, Exception? inner = null) : base(kind.ToString(), inner) => Kind = kind;
}

/// <summary>Gọi API open-meteo (geocoding + forecast) và các hàm xử lý thuần để dễ kiểm thử.</summary>
public class WeatherService
{
    // HttpClient dùng chung toàn ứng dụng, Timeout 8 giây
    private static readonly HttpClient SharedClient = new() { Timeout = TimeSpan.FromSeconds(8) };

    private readonly HttpClient _http;

    public WeatherService() : this(SharedClient) { }

    public WeatherService(HttpClient http) => _http = http;

    // ---------- Hàm thuần ----------

    /// <summary>weather_code -> loại thời tiết: 0 Quang; 1-3 Có mây; 45,48 Sương mù; 51-67 và 80-82 Mưa; 95-99 Dông.</summary>
    public static WeatherKind GetKind(int code)
    {
        if (code == 0) return WeatherKind.Clear;
        if (code >= 1 && code <= 3) return WeatherKind.Cloudy;
        if (code == 45 || code == 48) return WeatherKind.Fog;
        if ((code >= 51 && code <= 67) || (code >= 80 && code <= 82)) return WeatherKind.Rain;
        if (code >= 95 && code <= 99) return WeatherKind.Thunder;
        return WeatherKind.Other;
    }

    public static string GetIcon(WeatherKind kind) => kind switch
    {
        WeatherKind.Clear => "☀️",
        WeatherKind.Cloudy => "⛅",
        WeatherKind.Fog => "\U0001F32B️",
        WeatherKind.Rain => "\U0001F327️",
        WeatherKind.Thunder => "⛈️",
        _ => "\U0001F321️",
    };

    public static string GetDescriptionKey(WeatherKind kind) => kind switch
    {
        WeatherKind.Clear => "wx.clear",
        WeatherKind.Cloudy => "wx.cloudy",
        WeatherKind.Fog => "wx.fog",
        WeatherKind.Rain => "wx.rain",
        WeatherKind.Thunder => "wx.thunder",
        _ => "wx.other",
    };

    /// <summary>Xác suất mưa từ 60% trở lên thì cảnh báo.</summary>
    public static bool ShouldWarn(int? rainProbability) => rainProbability is >= 60;

    /// <summary>Tìm chỉ số của ngày trong mảng daily.time (định dạng yyyy-MM-dd); -1 nếu không có.</summary>
    public static int FindDayIndex(IList<string?> times, DateOnly date)
    {
        string key = date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        for (int i = 0; i < times.Count; i++)
            if (times[i] == key) return i;
        return -1;
    }

    /// <summary>Phân tích JSON geocoding; null nếu kết quả trống.</summary>
    public static (double Lat, double Lon)? ParseGeocoding(string json)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            if (!doc.RootElement.TryGetProperty("results", out var results)
                || results.ValueKind != JsonValueKind.Array || results.GetArrayLength() == 0)
                return null;
            var first = results[0];
            return (first.GetProperty("latitude").GetDouble(), first.GetProperty("longitude").GetDouble());
        }
        catch (Exception ex) when (ex is JsonException || ex is KeyNotFoundException || ex is InvalidOperationException)
        {
            throw new WeatherException(WeatherError.BadData, ex);
        }
    }

    /// <summary>Phân tích JSON forecast, lấy các giá trị tại đúng chỉ số của ngày; null nếu ngày không nằm trong dự báo.</summary>
    public static WeatherDay? ParseForecast(string json, DateOnly date)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            var daily = doc.RootElement.GetProperty("daily");
            var times = new List<string?>();
            foreach (var t in daily.GetProperty("time").EnumerateArray()) times.Add(t.GetString());

            int idx = FindDayIndex(times, date);
            if (idx < 0) return null;

            int code = daily.GetProperty("weather_code")[idx].GetInt32();
            double max = daily.GetProperty("temperature_2m_max")[idx].GetDouble();
            double min = daily.GetProperty("temperature_2m_min")[idx].GetDouble();

            int? rain = null;
            var rainEl = daily.GetProperty("precipitation_probability_max")[idx];
            if (rainEl.ValueKind == JsonValueKind.Number) rain = (int)Math.Round(rainEl.GetDouble());

            return new WeatherDay(date, code, min, max, rain);
        }
        catch (Exception ex) when (ex is JsonException || ex is KeyNotFoundException
                                   || ex is InvalidOperationException || ex is ArgumentOutOfRangeException
                                   || ex is FormatException)
        {
            throw new WeatherException(WeatherError.BadData, ex);
        }
    }

    // ---------- Gọi API ----------

    private async Task<string> GetStringAsync(string url, CancellationToken ct)
    {
        try
        {
            using var resp = await _http.GetAsync(url, ct);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadAsStringAsync(ct);
        }
        catch (OperationCanceledException) when (!ct.IsCancellationRequested)
        {
            throw new WeatherException(WeatherError.Timeout); // HttpClient.Timeout
        }
        catch (HttpRequestException ex)
        {
            throw new WeatherException(WeatherError.Network, ex);
        }
    }

    /// <summary>Bước 1: geocoding lấy latitude, longitude.</summary>
    public async Task<(double Lat, double Lon)> GetCoordinatesAsync(string diaDiem, CancellationToken ct = default)
    {
        string url = "https://geocoding-api.open-meteo.com/v1/search?name="
                     + Uri.EscapeDataString(diaDiem) + "&count=1&language=vi";
        var json = await GetStringAsync(url, ct);
        return ParseGeocoding(json) ?? throw new WeatherException(WeatherError.PlaceNotFound);
    }

    /// <summary>Bước 2: forecast 16 ngày; trả về null nếu ngày đi nằm ngoài khoảng dự báo.</summary>
    public async Task<WeatherDay?> GetForecastAsync(double lat, double lon, DateOnly date, CancellationToken ct = default)
    {
        var inv = CultureInfo.InvariantCulture;
        string url = "https://api.open-meteo.com/v1/forecast?latitude=" + lat.ToString(inv)
                     + "&longitude=" + lon.ToString(inv)
                     + "&daily=weather_code,temperature_2m_max,temperature_2m_min,precipitation_probability_max"
                     + "&timezone=auto&forecast_days=16";
        var json = await GetStringAsync(url, ct);
        return ParseForecast(json, date);
    }

    public async Task<WeatherDay?> GetWeatherAsync(string diaDiem, DateOnly date, CancellationToken ct = default)
    {
        var (lat, lon) = await GetCoordinatesAsync(diaDiem, ct);
        return await GetForecastAsync(lat, lon, date, ct);
    }
}
