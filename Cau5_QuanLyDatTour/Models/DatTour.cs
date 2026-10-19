using System;
using System.Collections.Generic;

namespace Cau5_QuanLyDatTour.Models;

public partial class DatTour
{
    public int MaDat { get; set; }

    public int MaKh { get; set; }

    public int MaTour { get; set; }

    public int SoNguoi { get; set; }

    public DateTime NgayDat { get; set; }

    public decimal TyGia { get; set; }

    public decimal TongTienVnd { get; set; }

    public virtual KhachHang MaKhNavigation { get; set; } = null!;

    public virtual Tour MaTourNavigation { get; set; } = null!;
}
