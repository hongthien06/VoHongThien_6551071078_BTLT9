using Microsoft.EntityFrameworkCore;
using Cau5_QuanLyDatTour.Models;

namespace Cau5_QuanLyDatTour;

public static class DatTourService
{
    /// <summary>Tổng tiền VND = giá ngoại tệ x số người x tỷ giá.</summary>
    public static decimal TinhTong(decimal giaNgoaiTe, int soNguoi, decimal tyGia)
        => Math.Round(giaNgoaiTe * soNguoi * tyGia, 0, MidpointRounding.AwayFromZero);

    /// <summary>Số chỗ còn lại = SoChoToiDa - tổng số người đã đặt tour đó.</summary>
    public static async Task<int> ConLaiAsync(QlDatTourContext db, int maTour)
    {
        var tour = await db.Tours.AsNoTracking().FirstAsync(t => t.MaTour == maTour);
        int daDat = await db.DatTours.Where(d => d.MaTour == maTour).SumAsync(d => (int?)d.SoNguoi) ?? 0;
        return tour.SoChoToiDa - daDat;
    }

    /// <summary>Lưu đơn đặt tour kèm tỷ giá tại thời điểm đặt. Ném InvalidOperationException nếu vượt số chỗ còn lại.</summary>
    public static async Task<DatTour> DatAsync(int maKh, int maTour, int soNguoi, decimal tyGia)
    {
        if (soNguoi <= 0) throw new InvalidOperationException("Số người phải lớn hơn 0.");

        using var db = AppConfig.CreateContext();
        var tour = await db.Tours.FirstAsync(t => t.MaTour == maTour);
        int conLai = await ConLaiAsync(db, maTour);
        if (soNguoi > conLai)
            throw new InvalidOperationException($"Tour \"{tour.TenTour}\" chỉ còn {conLai} chỗ, không thể đặt {soNguoi} người.");

        var dat = new DatTour
        {
            MaKh = maKh,
            MaTour = maTour,
            SoNguoi = soNguoi,
            NgayDat = DateTime.Now,
            TyGia = tyGia,
            TongTienVnd = TinhTong(tour.GiaNgoaiTe, soNguoi, tyGia)
        };
        db.DatTours.Add(dat);
        await db.SaveChangesAsync();
        return dat;
    }
}
