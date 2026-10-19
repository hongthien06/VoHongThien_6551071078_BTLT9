using Cau4_QuanTraSua.Config;
using Serilog;

namespace Cau4_QuanTraSua.Services;

/// <summary>Tạo URL VietQR (img.vietqr.io) và tải ảnh QR bất đồng bộ.</summary>
public class VietQrService
{
    private readonly PaymentSettings _settings;
    private readonly HttpClient _http;

    public VietQrService(PaymentSettings settings, HttpClient? http = null)
    {
        _settings = settings;
        _http = http ?? new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
    }

    public PaymentSettings Settings => _settings;

    /// <summary>Nội dung chuyển khoản, ví dụ "TRASUA HD1024".</summary>
    public static string TaoNoiDung(int maHd) => $"TRASUA HD{maHd}";

    /// <summary>Ghép địa chỉ ảnh VietQR; các tham số được mã hóa bằng Uri.EscapeDataString.</summary>
    public string TaoUrl(decimal tongTien, string noiDung)
    {
        var amount = decimal.Truncate(tongTien).ToString(System.Globalization.CultureInfo.InvariantCulture);
        return $"https://img.vietqr.io/image/{_settings.BankId}-{_settings.AccountNo}-{_settings.Template}.png" +
               $"?amount={Uri.EscapeDataString(amount)}" +
               $"&addInfo={Uri.EscapeDataString(noiDung)}" +
               $"&accountName={Uri.EscapeDataString(_settings.AccountName)}";
    }

    /// <summary>
    /// Tải ảnh QR bằng HttpClient.GetByteArrayAsync. Trả về null nếu lỗi (mất mạng...).
    /// Log Information khi tạo QR (chỉ ghi MaHD và số tiền, KHÔNG ghi số tài khoản), Error khi tải thất bại.
    /// </summary>
    public async Task<byte[]?> TaiAnhQrAsync(int maHd, decimal tongTien)
    {
        var noiDung = TaoNoiDung(maHd);
        var url = TaoUrl(tongTien, noiDung);
        Log.Information("Tạo QR thanh toán: MaHD={MaHD}, SoTien={SoTien}", maHd, tongTien);
        try
        {
            return await _http.GetByteArrayAsync(url);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Tải ảnh QR thất bại: MaHD={MaHD}, SoTien={SoTien}", maHd, tongTien);
            return null;
        }
    }
}
