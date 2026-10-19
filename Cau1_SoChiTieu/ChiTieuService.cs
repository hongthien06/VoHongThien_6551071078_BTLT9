using Cau1_SoChiTieu.Models;
using Microsoft.EntityFrameworkCore;

namespace Cau1_SoChiTieu;

/// <summary>
/// Truy vấn dữ liệu bảng ChiTieu
/// </summary>
public static class ChiTieuService
{
    /// <summary>
    /// Lọc theo nội dung, loại chi, khoảng ngày (tham số null = bỏ qua điều kiện)
    /// </summary>
    public static List<ChiTieu> Loc(QlChiTieuContext db, string? tuKhoa, string? loaiChi, DateOnly? tuNgay, DateOnly? denNgay)
    {
        IQueryable<ChiTieu> query = db.ChiTieus.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(tuKhoa))
        {
            var kw = tuKhoa.Trim();
            query = query.Where(c => c.NoiDung.Contains(kw));
        }
        if (!string.IsNullOrEmpty(loaiChi))
            query = query.Where(c => c.LoaiChi == loaiChi);
        if (tuNgay.HasValue)
            query = query.Where(c => c.NgayChi >= tuNgay.Value);
        if (denNgay.HasValue)
            query = query.Where(c => c.NgayChi <= denNgay.Value);

        return query.OrderBy(c => c.NgayChi).ThenBy(c => c.MaCt).ToList();
    }
}
