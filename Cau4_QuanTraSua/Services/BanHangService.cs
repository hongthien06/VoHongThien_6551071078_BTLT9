using Cau4_QuanTraSua.Models;
using Microsoft.EntityFrameworkCore;

namespace Cau4_QuanTraSua.Services;

/// <summary>Lưu hóa đơn, tính doanh thu ngày.</summary>
public class BanHangService
{
    private readonly QlTraSuaContext _context;

    public BanHangService(QlTraSuaContext context) => _context = context;

    /// <summary>Số hóa đơn dự kiến tiếp theo (dùng ghép nội dung chuyển khoản trước khi lưu).</summary>
    public async Task<int> LayMaHdTiepTheoAsync()
    {
        var ketQua = await _context.Database
            .SqlQueryRaw<int>("SELECT CAST(ISNULL(CAST(last_value AS bigint) + CAST(increment_value AS bigint), CAST(seed_value AS bigint)) AS int) AS Value " +
                              "FROM sys.identity_columns WHERE object_id = OBJECT_ID('dbo.HoaDon')")
            .ToListAsync();
        return ketQua.Count > 0 ? ketQua[0] : 1;
    }

    /// <summary>Lưu HoaDon + ChiTietHoaDon trong cùng một lần SaveChanges.</summary>
    public async Task<HoaDon> LuuHoaDonAsync(GioHang gio, string hinhThucTt)
    {
        if (gio.Dong.Count == 0) throw new InvalidOperationException("Đơn hàng đang trống.");

        var hd = new HoaDon
        {
            NgayLap = DateTime.Now,
            TongTien = gio.TongTien,
            HinhThucTt = hinhThucTt,
            DaThanhToan = true
        };
        foreach (var d in gio.Dong)
        {
            hd.ChiTietHoaDons.Add(new ChiTietHoaDon { MaMon = d.MaMon, SoLy = d.SoLy, DonGia = d.DonGia });
        }
        _context.HoaDons.Add(hd);
        await _context.SaveChangesAsync();
        return hd;
    }

    public async Task<decimal> DoanhThuNgayAsync(DateTime ngay)
    {
        var tu = ngay.Date;
        var den = tu.AddDays(1);
        return await _context.HoaDons.AsNoTracking()
            .Where(h => h.DaThanhToan && h.NgayLap >= tu && h.NgayLap < den)
            .SumAsync(h => (decimal?)h.TongTien) ?? 0m;
    }
}
