using System;
using System.Collections.Generic;

namespace Cau5_QuanLyDatTour.Models;

public partial class Tour
{
    public int MaTour { get; set; }

    public string TenTour { get; set; } = null!;

    public string? DiemDen { get; set; }

    public string NgoaiTe { get; set; } = null!;

    public decimal GiaNgoaiTe { get; set; }

    public DateOnly? NgayKhoiHanh { get; set; }

    public int SoChoToiDa { get; set; }

    public virtual ICollection<DatTour> DatTours { get; set; } = new List<DatTour>();
}
