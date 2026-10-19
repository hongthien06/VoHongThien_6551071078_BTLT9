using System.Globalization;
using Serilog;

namespace Cau5_QuanLyDatTour;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("vi-VN");
        CultureInfo.CurrentCulture = new CultureInfo("vi-VN");

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File(Path.Combine(AppConfig.BaseDirectory, "logs", "log-.txt"),
                rollingInterval: RollingInterval.Day,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

        // Bắt lỗi toàn cục
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (s, e) =>
        {
            Log.Error(e.Exception, "ThreadException");
            MessageBox.Show("Đã xảy ra lỗi: " + e.Exception.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        };
        AppDomain.CurrentDomain.UnhandledException += (s, e) =>
        {
            Log.Fatal(e.ExceptionObject as Exception, "UnhandledException");
            Log.CloseAndFlush();
        };

        ApplicationConfiguration.Initialize();
        Log.Information("Khởi động ứng dụng");
        Application.Run(new Form1());
        Log.Information("Thoát ứng dụng");
        Log.CloseAndFlush();
    }
}
