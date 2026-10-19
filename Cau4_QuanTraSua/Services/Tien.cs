using System.Globalization;

namespace Cau4_QuanTraSua.Services;

/// <summary>Định dạng tiền Việt: 120.000 đ</summary>
public static class Tien
{
    private static readonly CultureInfo Vi = new("vi-VN");

    public static string Dinh(decimal soTien) => soTien.ToString("N0", Vi) + " đ";
}
