using System.ComponentModel;

namespace Cau4_QuanTraSua.Services;

/// <summary>Một dòng trong giỏ hàng (đơn hiện tại).</summary>
public class DongGioHang
{
    [Browsable(false)]
    public int MaMon { get; set; }
    public string TenMon { get; set; } = "";
    public int SoLy { get; set; }
    public decimal DonGia { get; set; }
    public decimal ThanhTien => SoLy * DonGia;
}

/// <summary>Giỏ hàng: thêm món (cộng dồn số lượng nếu đã có), xóa dòng, tính tổng tiền.</summary>
public class GioHang
{
    public BindingList<DongGioHang> Dong { get; } = new();

    public decimal TongTien => Dong.Sum(d => d.ThanhTien);

    public void ThemMon(int maMon, string tenMon, decimal donGia, int soLy)
    {
        if (soLy <= 0) throw new ArgumentOutOfRangeException(nameof(soLy), "Số ly phải lớn hơn 0.");

        var daCo = Dong.FirstOrDefault(d => d.MaMon == maMon);
        if (daCo != null)
        {
            daCo.SoLy += soLy;
            // Thông báo để lưới cập nhật lại dòng
            Dong.ResetItem(Dong.IndexOf(daCo));
        }
        else
        {
            Dong.Add(new DongGioHang { MaMon = maMon, TenMon = tenMon, DonGia = donGia, SoLy = soLy });
        }
    }

    public void XoaDong(int index)
    {
        if (index >= 0 && index < Dong.Count) Dong.RemoveAt(index);
    }

    public void Xoa() => Dong.Clear();
}
