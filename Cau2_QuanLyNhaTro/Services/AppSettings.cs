using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Cau2_QuanLyNhaTro.Models;

namespace Cau2_QuanLyNhaTro.Services;

/// <summary>
/// Đọc cấu hình từ appsettings.json (Microsoft.Extensions.Configuration) và tạo DbContext.
/// </summary>
public static class AppSettings
{
    public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
        .Build();

    public static string ConnectionString =>
        Configuration.GetConnectionString("QL_NhaTro")
        ?? "Server=localhost;Database=QL_NhaTro;Trusted_Connection=True;TrustServerCertificate=True";

    public static string ApiBaseUrl =>
        Configuration["DiaChiApi:BaseUrl"] ?? "https://provinces.open-api.vn/api/v2/";

    public static int ApiTimeoutSeconds =>
        int.TryParse(Configuration["DiaChiApi:TimeoutSeconds"], out var s) && s > 0 ? s : 10;

    public static QlNhaTroContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<QlNhaTroContext>()
            .UseSqlServer(ConnectionString)
            .Options;
        return new QlNhaTroContext(options);
    }
}
