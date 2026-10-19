using System.Net.Http.Json;
using System.Text.Json;

namespace Cau2_QuanLyNhaTro.Services;

/// <summary>Một tỉnh/thành hoặc phường/xã trả về từ API (name, code).</summary>
public class DiaChiItem
{
    // Tên thuộc tính viết thường để khớp DisplayMember = "name", ValueMember = "code"
    public string name { get; set; } = "";
    public int code { get; set; }

    // Chỉ có khi gọi /p/{code}?depth=2
    public List<DiaChiItem>? wards { get; set; }
}

/// <summary>
/// Gọi API hành chính provinces.open-api.vn (v2). Dùng chung MỘT HttpClient, Timeout 10 giây.
/// </summary>
public static class DiaChiApiService
{
    private static readonly HttpClient _http = new()
    {
        BaseAddress = new Uri(AppSettings.ApiBaseUrl),
        Timeout = TimeSpan.FromSeconds(AppSettings.ApiTimeoutSeconds)
    };

    private static readonly JsonSerializerOptions _json = new() { PropertyNameCaseInsensitive = true };

    /// <summary>GET /api/v2/ - danh sách tỉnh/thành.</summary>
    public static async Task<List<DiaChiItem>> GetTinhAsync()
    {
        var list = await _http.GetFromJsonAsync<List<DiaChiItem>>("", _json);
        return list ?? new List<DiaChiItem>();
    }

    /// <summary>GET /api/v2/p/{code}?depth=2 - danh sách phường/xã của tỉnh.</summary>
    public static async Task<List<DiaChiItem>> GetPhuongXaAsync(int maTinh)
    {
        var tinh = await _http.GetFromJsonAsync<DiaChiItem>($"p/{maTinh}?depth=2", _json);
        return tinh?.wards ?? new List<DiaChiItem>();
    }
}
