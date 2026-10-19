using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Cau5_QuanLyDatTour.Models;

namespace Cau5_QuanLyDatTour;

/// <summary>Mục "ExchangeRate" trong appsettings.json (lớp cấu hình có kiểu).</summary>
public class ExchangeRateOptions
{
    public string PrimaryUrl { get; set; } = "";
    public string FallbackUrl { get; set; } = "";
    public int TimeoutSeconds { get; set; } = 10;
}

/// <summary>Đọc appsettings.json nằm cạnh file exe.</summary>
public static class AppConfig
{
    public static string BaseDirectory { get; } =
        Path.GetDirectoryName(Environment.ProcessPath) ?? AppContext.BaseDirectory;

    public static IConfigurationRoot Configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(BaseDirectory)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
        .Build();

    public static ExchangeRateOptions ExchangeRate { get; } =
        Configuration.GetSection("ExchangeRate").Get<ExchangeRateOptions>() ?? new ExchangeRateOptions();

    public static string ConnectionString =>
        Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Thiếu ConnectionStrings:DefaultConnection trong appsettings.json");

    public static QlDatTourContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<QlDatTourContext>()
            .UseSqlServer(ConnectionString)
            .Options;
        return new QlDatTourContext(options);
    }
}
