using Microsoft.Extensions.Configuration;

namespace Cau1_SoChiTieu;

/// <summary>
/// Lớp cấu hình có kiểu (strongly-typed), ánh xạ từ appsettings.json
/// </summary>
public class AppSettings
{
    public ConnectionStringsSettings ConnectionStrings { get; set; } = new();

    /// <summary>
    /// Đọc appsettings.json (+ appsettings.Development.json nếu có) cạnh file .exe
    /// </summary>
    public static AppSettings Load()
    {
        var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";

        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: false)
            .Build();

        return config.Get<AppSettings>() ?? new AppSettings();
    }
}

public class ConnectionStringsSettings
{
    public string QL_ChiTieu { get; set; } = string.Empty;
}
