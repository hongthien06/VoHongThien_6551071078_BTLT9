using System.Text.Json;
using Serilog;

namespace Cau5_QuanLyDatTour;

/// <summary>Kết quả tra tỷ giá: 1 đơn vị ngoại tệ = TyGia VND.</summary>
public record TyGiaKetQua(decimal TyGia, string Nguon, bool TuCache);

public class TyGiaService
{
    private static readonly TimeSpan CacheTime = TimeSpan.FromMinutes(10);

    private readonly ExchangeRateOptions _options;
    private readonly HttpClient _http;
    private readonly Func<DateTime> _now;
    private readonly Dictionary<string, (TyGiaKetQua Value, DateTime Expire)> _cache = new();
    private readonly object _lock = new();

    public TyGiaService(ExchangeRateOptions options, HttpMessageHandler? handler = null, Func<DateTime>? now = null)
    {
        _options = options;
        _now = now ?? (() => DateTime.Now);
        _http = handler == null ? new HttpClient() : new HttpClient(handler);
        _http.Timeout = TimeSpan.FromSeconds(Math.Max(1, options.TimeoutSeconds));
    }

    /// <summary>Lấy tỷ giá ngoại tệ → VND (có cache 10 phút, tự chuyển sang địa chỉ dự phòng nếu địa chỉ chính lỗi).</summary>
    public async Task<TyGiaKetQua> LayTyGiaAsync(string ngoaiTe)
    {
        string code = ngoaiTe.Trim().ToLowerInvariant();

        lock (_lock)
        {
            if (_cache.TryGetValue(code, out var c) && c.Expire > _now())
            {
                Log.Information("Tỷ giá {Code} lấy từ bộ nhớ đệm (nguồn gốc: {Nguon})", code, c.Value.Nguon);
                return c.Value with { TuCache = true };
            }
        }

        string primary = _options.PrimaryUrl.Replace("{code}", code);
        string fallback = _options.FallbackUrl.Replace("{code}", code);

        TyGiaKetQua kq;
        try
        {
            kq = await GoiApiAsync(primary, code);
            Log.Information("Lấy tỷ giá {Code} bằng địa chỉ chính: {Url}", code, primary);
        }
        catch (Exception ex)
        {
            Log.Warning("Địa chỉ chính {Url} lỗi: {Reason}. Chuyển sang địa chỉ dự phòng", primary, ex.Message);
            try
            {
                kq = await GoiApiAsync(fallback, code);
                Log.Information("Lấy tỷ giá {Code} bằng địa chỉ dự phòng: {Url}", code, fallback);
            }
            catch (Exception ex2)
            {
                Log.Error(ex2, "Địa chỉ dự phòng {Url} cũng lỗi", fallback);
                throw new InvalidOperationException(
                    $"Không lấy được tỷ giá {ngoaiTe.ToUpperInvariant()} từ cả hai địa chỉ API: {ex2.Message}", ex2);
            }
        }

        lock (_lock)
        {
            _cache[code] = (kq, _now() + CacheTime);
        }
        return kq;
    }

    private async Task<TyGiaKetQua> GoiApiAsync(string url, string code)
    {
        using var resp = await _http.GetAsync(url);
        resp.EnsureSuccessStatusCode();
        string json = await resp.Content.ReadAsStringAsync();

        // Cấu trúc: {"date":"...","thb":{"vnd":...}}
        using var doc = JsonDocument.Parse(json);
        decimal rate = doc.RootElement.GetProperty(code).GetProperty("vnd").GetDecimal();
        if (rate <= 0) throw new InvalidDataException("Tỷ giá không hợp lệ");
        return new TyGiaKetQua(rate, new Uri(url).Host, false);
    }
}
