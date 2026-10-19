using Serilog;

namespace Cau2_QuanLyNhaTro;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // Cấu hình Serilog: ghi log ra file logs\log-yyyyMMdd.txt (kèm stack trace)
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File(
                Path.Combine(AppContext.BaseDirectory, "logs", "log-.txt"),
                rollingInterval: RollingInterval.Day,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

        // Xử lý lỗi toàn cục (9.3): phải gọi trước khi tạo form
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (s, e) => XuLyLoiChuaBat(e.Exception, "Application.ThreadException");
        AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            XuLyLoiChuaBat(e.ExceptionObject as Exception ?? new Exception(e.ExceptionObject?.ToString()),
                "AppDomain.UnhandledException");

        try
        {
            Log.Information("Ứng dụng khởi động");
            ApplicationConfiguration.Initialize();
            Application.Run(new FormKhachThue());
        }
        finally
        {
            Log.Information("Ứng dụng kết thúc");
            Log.CloseAndFlush();
        }
    }

    /// <summary>Ghi log (kèm stack trace) và báo thân thiện cho người dùng.</summary>
    private static void XuLyLoiChuaBat(Exception ex, string nguon)
    {
        try { Log.Error(ex, "Lỗi chưa bắt ({Nguon})", nguon); }
        catch { /* không để lỗi ghi log làm app chết */ }

        MessageBox.Show("Đã xảy ra lỗi, thông tin đã được ghi lại", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
