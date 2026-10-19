using System;
using System.Collections.Generic;

namespace Cau5_QuanLyDatTour.Models;

public partial class KhachHang
{
    public int MaKh { get; set; }

    public string HoTen { get; set; } = null!;

    public string? SoDienThoai { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<DatTour> DatTours { get; set; } = new List<DatTour>();
}
