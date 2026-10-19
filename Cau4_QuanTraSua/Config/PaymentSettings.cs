namespace Cau4_QuanTraSua.Config;

/// <summary>
/// Cấu hình thanh toán đọc từ mục "Payment" trong appsettings.json (không hard-code trong code).
/// </summary>
public class PaymentSettings
{
    public string BankId { get; set; } = "";
    public string AccountNo { get; set; } = "";
    public string AccountName { get; set; } = "";
    public string Template { get; set; } = "compact2";
    public string ShopName { get; set; } = "";
}
