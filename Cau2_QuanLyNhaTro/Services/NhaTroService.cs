using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Cau2_QuanLyNhaTro.Models;

namespace Cau2_QuanLyNhaTro.Services;

/// <summary>Phòng hiển thị trong ComboBox: "P03 - 1.800.000 đ".</summary>
public record PhongItem(int MaPhong, string TenHienThi);

/// <summary>Một dòng DataGridView (kết quả JOIN KhachThue - PhongTro).</summary>
public class KhachRow
{
    public int MaKhach { get; set; }
    public string HoTen { get; set; } = "";
    public string SoDienThoai { get; set; } = "";
    public string TenPhong { get; set; } = "";
    public string GiaThue { get; set; } = "";
    public string QueQuan { get; set; } = "";
    public bool TamTru { get; set; }
}

/// <summary>Nghiệp vụ CRUD trên 2 bảng + đổi trạng thái phòng.</summary>
public static class NhaTroService
{
    public const string Trong = "Trống";
    public const string DangThue = "Đang thuê";
    private static readonly CultureInfo Vi = new("vi-VN");

    public static string FormatGia(decimal gia) => gia.ToString("N0", Vi) + " đ";

    private static PhongItem ToItem(PhongTro p) => new(p.MaPhong, $"{p.TenPhong} - {FormatGia(p.GiaThue)}");

    /// <summary>Phòng hợp lệ để chọn: phòng "Trống", cộng thêm phòng hiện tại của khách đang sửa.</summary>
    public static async Task<List<PhongItem>> GetPhongHopLeAsync(int? maPhongHienTai = null)
    {
        using var db = AppSettings.CreateContext();
        var ds = await db.PhongTros.AsNoTracking()
            .Where(p => p.TinhTrang == Trong || p.MaPhong == maPhongHienTai)
            .OrderBy(p => p.TenPhong).ToListAsync();
        return ds.Select(ToItem).ToList();
    }

    /// <summary>Toàn bộ phòng (dùng cho ComboBox lọc).</summary>
    public static async Task<List<PhongItem>> GetTatCaPhongAsync()
    {
        using var db = AppSettings.CreateContext();
        var ds = await db.PhongTros.AsNoTracking().OrderBy(p => p.TenPhong).ToListAsync();
        return ds.Select(ToItem).ToList();
    }

    /// <summary>Các tỉnh đang có trong dữ liệu khách (dùng cho ComboBox lọc theo tỉnh, không cần mạng).</summary>
    public static async Task<List<DiaChiItem>> GetTinhDangCoAsync()
    {
        using var db = AppSettings.CreateContext();
        var ds = await db.KhachThues.AsNoTracking()
            .Where(k => k.MaTinh != null && k.TenTinh != null)
            .Select(k => new { k.MaTinh, k.TenTinh }).Distinct().ToListAsync();
        return ds.OrderBy(x => x.TenTinh)
            .Select(x => new DiaChiItem { code = x.MaTinh!.Value, name = x.TenTinh! }).ToList();
    }

    /// <summary>JOIN KhachThue - PhongTro, có tìm kiếm/lọc.</summary>
    public static async Task<List<KhachRow>> TimKhachAsync(string? tuKhoa, int? maPhong, int? maTinh)
    {
        using var db = AppSettings.CreateContext();
        var q = db.KhachThues.AsNoTracking().Include(k => k.MaPhongNavigation).AsQueryable();

        tuKhoa = tuKhoa?.Trim();
        if (!string.IsNullOrEmpty(tuKhoa))
            q = q.Where(k => k.HoTen.Contains(tuKhoa) || (k.SoDienThoai != null && k.SoDienThoai.Contains(tuKhoa)));
        if (maPhong.HasValue) q = q.Where(k => k.MaPhong == maPhong.Value);
        if (maTinh.HasValue) q = q.Where(k => k.MaTinh == maTinh.Value);

        var ds = await q.OrderBy(k => k.MaKhach).ToListAsync();
        return ds.Select(k => new KhachRow
        {
            MaKhach = k.MaKhach,
            HoTen = k.HoTen,
            SoDienThoai = k.SoDienThoai ?? "",
            TenPhong = k.MaPhongNavigation.TenPhong,
            GiaThue = FormatGia(k.MaPhongNavigation.GiaThue),
            QueQuan = string.Join(" - ", new[] { k.TenPhuongXa, k.TenTinh }.Where(s => !string.IsNullOrWhiteSpace(s))),
            TamTru = k.DaDangKyTamTru
        }).ToList();
    }

    public static async Task<KhachThue?> GetKhachAsync(int maKhach)
    {
        using var db = AppSettings.CreateContext();
        return await db.KhachThues.AsNoTracking().FirstOrDefaultAsync(k => k.MaKhach == maKhach);
    }

    /// <summary>Thêm khách; phòng chuyển "Đang thuê".</summary>
    public static async Task ThemAsync(KhachThue khach)
    {
        using var db = AppSettings.CreateContext();
        using var tran = await db.Database.BeginTransactionAsync();

        var phong = await db.PhongTros.FindAsync(khach.MaPhong)
            ?? throw new InvalidOperationException("Phòng không tồn tại.");
        if (phong.TinhTrang != Trong)
            throw new InvalidOperationException($"Phòng {phong.TenPhong} đang có người thuê, không thể thêm.");

        db.KhachThues.Add(khach);
        phong.TinhTrang = DangThue;
        await db.SaveChangesAsync();
        await tran.CommitAsync();
    }

    /// <summary>Sửa khách; nếu đổi phòng thì cập nhật trạng thái phòng cũ và phòng mới.</summary>
    public static async Task SuaAsync(KhachThue khachMoi)
    {
        using var db = AppSettings.CreateContext();
        using var tran = await db.Database.BeginTransactionAsync();

        var khach = await db.KhachThues.FindAsync(khachMoi.MaKhach)
            ?? throw new InvalidOperationException("Không tìm thấy khách cần sửa.");
        int phongCu = khach.MaPhong;

        if (khachMoi.MaPhong != phongCu)
        {
            var phongMoi = await db.PhongTros.FindAsync(khachMoi.MaPhong)
                ?? throw new InvalidOperationException("Phòng không tồn tại.");
            if (phongMoi.TinhTrang != Trong)
                throw new InvalidOperationException($"Phòng {phongMoi.TenPhong} đang có người thuê, không thể chuyển vào.");
        }

        khach.HoTen = khachMoi.HoTen;
        khach.GioiTinh = khachMoi.GioiTinh;
        khach.NgaySinh = khachMoi.NgaySinh;
        khach.SoDienThoai = khachMoi.SoDienThoai;
        khach.MaTinh = khachMoi.MaTinh;
        khach.TenTinh = khachMoi.TenTinh;
        khach.MaPhuongXa = khachMoi.MaPhuongXa;
        khach.TenPhuongXa = khachMoi.TenPhuongXa;
        khach.DaDangKyTamTru = khachMoi.DaDangKyTamTru;
        khach.MaPhong = khachMoi.MaPhong;
        await db.SaveChangesAsync();

        await CapNhatTinhTrangPhongAsync(db, khachMoi.MaPhong);
        if (phongCu != khachMoi.MaPhong) await CapNhatTinhTrangPhongAsync(db, phongCu);
        await tran.CommitAsync();
    }

    /// <summary>Xóa khách; nếu là người cuối cùng của phòng thì phòng trở lại "Trống".</summary>
    public static async Task XoaAsync(int maKhach)
    {
        using var db = AppSettings.CreateContext();
        using var tran = await db.Database.BeginTransactionAsync();

        var khach = await db.KhachThues.FindAsync(maKhach)
            ?? throw new InvalidOperationException("Không tìm thấy khách cần xóa.");
        int maPhong = khach.MaPhong;
        db.KhachThues.Remove(khach);
        await db.SaveChangesAsync();

        await CapNhatTinhTrangPhongAsync(db, maPhong);
        await tran.CommitAsync();
    }

    /// <summary>Phòng còn khách → "Đang thuê", hết khách → "Trống".</summary>
    private static async Task CapNhatTinhTrangPhongAsync(QlNhaTroContext db, int maPhong)
    {
        var phong = await db.PhongTros.FindAsync(maPhong);
        if (phong == null) return;
        bool conKhach = await db.KhachThues.AnyAsync(k => k.MaPhong == maPhong);
        phong.TinhTrang = conKhach ? DangThue : Trong;
        await db.SaveChangesAsync();
    }
}
