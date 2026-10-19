using Cau1_SoChiTieu.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Cau1_SoChiTieu;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // Ghi log theo ngày vào thư mục Logs cạnh file .exe: log-yyyyMMdd.txt
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File(
                Path.Combine(AppContext.BaseDirectory, "Logs", "log-.txt"),
                rollingInterval: RollingInterval.Day)
            .CreateLogger();

        try
        {
            var settings = AppSettings.Load();
            var options = new DbContextOptionsBuilder<QlChiTieuContext>()
                .UseSqlServer(settings.ConnectionStrings.QL_ChiTieu)
                .Options;

            Log.Information("Khởi động ứng dụng Sổ chi tiêu cá nhân");
            Application.Run(new Form1(options));
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Lỗi khi khởi động ứng dụng");
            MessageBox.Show("Không thể khởi động ứng dụng. Vui lòng kiểm tra file appsettings.json.",
                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
