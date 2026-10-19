using Microsoft.Extensions.Configuration;

namespace Cau4_QuanTraSua.Config;

/// <summary>
/// Đọc appsettings.json và ánh xạ mục "Payment" sang lớp có kiểu PaymentSettings.
/// </summary>
public static class AppConfig
{
    public static PaymentSettings LoadPayment(string? basePath = null)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(basePath ?? AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .Build();

        return config.GetSection("Payment").Get<PaymentSettings>() ?? new PaymentSettings();
    }
}
