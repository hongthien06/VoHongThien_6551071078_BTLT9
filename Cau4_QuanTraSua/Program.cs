using Serilog;

namespace Cau4_QuanTraSua;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // Cấu hình Serilog: ghi file log theo ngày vào thư mục logs
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File(Path.Combine(AppContext.BaseDirectory, "logs", "trasua-.log"),
                rollingInterval: RollingInterval.Day)
            .CreateLogger();

        try
        {
            var vi = new System.Globalization.CultureInfo("vi-VN");
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = vi;
            System.Globalization.CultureInfo.CurrentCulture = vi;
            ApplicationConfiguration.Initialize();
            Application.Run(new FormChinh());
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
