using Cau4_QuanTraSua.Models;
using Cau4_QuanTraSua.Services;
using Microsoft.EntityFrameworkCore;

namespace Cau4_QuanTraSua;

/// <summary>Danh sách hóa đơn: lọc theo ngày, xem chi tiết, doanh thu ngày.</summary>
public partial class FormHoaDon : Form
{
    private readonly QlTraSuaContext _context = new();
    private bool _isBinding = false;

    public FormHoaDon()
    {
        InitializeComponent();
    }

    private async void FormHoaDon_Load(object sender, EventArgs e)
    {
        await LoadHoaDonAsync();
    }

    private async void dtpNgay_ValueChanged(object sender, EventArgs e)
    {
        await LoadHoaDonAsync();
    }

    private async Task LoadHoaDonAsync()
    {
        try
        {
            _isBinding = true;
            var tu = dtpNgay.Value.Date;
            var den = tu.AddDays(1);
            var ds = await _context.HoaDons.AsNoTracking()
                .Where(h => h.NgayLap >= tu && h.NgayLap < den)
                .OrderBy(h => h.NgayLap)
                .Select(h => new { h.MaHd, h.NgayLap, h.HinhThucTt, h.TongTien })
                .ToListAsync();

            dgvHoaDon.DataSource = ds;
            dgvHoaDon.Columns["MaHd"]!.HeaderText = "Mã HD";
            dgvHoaDon.Columns["NgayLap"]!.HeaderText = "Giờ lập";
            dgvHoaDon.Columns["NgayLap"]!.DefaultCellStyle.Format = "HH:mm:ss";
            dgvHoaDon.Columns["HinhThucTt"]!.HeaderText = "Hình thức";
            dgvHoaDon.Columns["TongTien"]!.HeaderText = "Tổng tiền";
            dgvHoaDon.Columns["TongTien"]!.DefaultCellStyle.Format = "N0";

            var doanhThu = await new BanHangService(_context).DoanhThuNgayAsync(dtpNgay.Value);
            lblDoanhThu.Text = "Doanh thu ngày: " + Tien.Dinh(doanhThu);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi nạp hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _isBinding = false;
        }
        await LoadChiTietAsync();
    }

    private async void dgvHoaDon_SelectionChanged(object sender, EventArgs e)
    {
        if (_isBinding) return;
        await LoadChiTietAsync();
    }

    private async Task LoadChiTietAsync()
    {
        try
        {
            if (dgvHoaDon.CurrentRow?.Cells["MaHd"].Value is not int maHd)
            {
                dgvChiTiet.DataSource = null;
                return;
            }
            using var ctx = new QlTraSuaContext();
            var ct = await ctx.ChiTietHoaDons.AsNoTracking()
                .Where(c => c.MaHd == maHd)
                .Select(c => new
                {
                    TenMon = c.MaMonNavigation.TenMon,
                    c.SoLy,
                    c.DonGia,
                    ThanhTien = c.SoLy * c.DonGia
                })
                .ToListAsync();

            dgvChiTiet.DataSource = ct;
            dgvChiTiet.Columns["TenMon"]!.HeaderText = "Tên món";
            dgvChiTiet.Columns["SoLy"]!.HeaderText = "SL";
            dgvChiTiet.Columns["DonGia"]!.HeaderText = "Đơn giá";
            dgvChiTiet.Columns["DonGia"]!.DefaultCellStyle.Format = "N0";
            dgvChiTiet.Columns["ThanhTien"]!.HeaderText = "Thành tiền";
            dgvChiTiet.Columns["ThanhTien"]!.DefaultCellStyle.Format = "N0";
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi nạp chi tiết hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _context.Dispose();
        base.OnFormClosed(e);
    }
}
