using System.Globalization;
using Cau1_SoChiTieu.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Cau1_SoChiTieu;

public partial class Form1 : Form
{
    private static readonly string[] DanhSachLoaiChi = { "Ăn uống", "Đi lại", "Học tập", "Giải trí", "Khác" };
    private static readonly CultureInfo ViVN = new("vi-VN");

    private readonly DbContextOptions<QlChiTieuContext> _options;
    private bool _dangNap; // true khi đang nạp lưới, tránh đổ dữ liệu dòng đầu lên control

    // Điều kiện lọc đang áp dụng (null = không lọc)
    private string? _tuKhoa;
    private string? _loaiChi;
    private DateOnly? _tuNgay;
    private DateOnly? _denNgay;

    public Form1(DbContextOptions<QlChiTieuContext> options)
    {
        _options = options;
        InitializeComponent();

        cboLoaiChi.Items.AddRange(DanhSachLoaiChi);
        cboLoaiChi.SelectedIndex = 0;

        cboLocLoaiChi.Items.Add("Tất cả");
        cboLocLoaiChi.Items.AddRange(DanhSachLoaiChi);
        cboLocLoaiChi.SelectedIndex = 0;

        // Mặc định: từ đầu tháng đến hôm nay
        var homNay = DateTime.Today;
        dtpTuNgay.Value = new DateTime(homNay.Year, homNay.Month, 1);
        dtpDenNgay.Value = homNay;

        // Ngày chi không lớn hơn hôm nay
        dtpNgayChi.MaxDate = homNay;
        dtpNgayChi.Value = homNay;

        txtTimKiem.KeyDown += (s, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnTim_Click(s, EventArgs.Empty);
            }
        };

        Load += (s, e) => TaiDuLieu();
    }

    private QlChiTieuContext TaoContext() => new(_options);

    /// <summary>
    /// Tải lại lưới theo điều kiện lọc đang áp dụng và cập nhật nhãn Tổng chi
    /// </summary>
    private void TaiDuLieu()
    {
        try
        {
            using var db = TaoContext();
            var ds = ChiTieuService.Loc(db, _tuKhoa, _loaiChi, _tuNgay, _denNgay);

            _dangNap = true;
            dgvChiTieu.DataSource = ds.Select(c => new
            {
                MaCT = c.MaCt,
                NgayChi = c.NgayChi.ToDateTime(TimeOnly.MinValue),
                c.NoiDung,
                c.LoaiChi,
                c.SoTien,
                c.GhiChu
            }).ToList();

            lblTongChi.Text = $"Tổng chi: {ds.Sum(c => c.SoTien).ToString("N0", ViVN)} đ";
            dgvChiTieu.ClearSelection();
            _dangNap = false;
        }
        catch (Exception ex)
        {
            _dangNap = false;
            BaoLoiCSDL(ex, "Tải danh sách chi tiêu");
        }
    }

    private void BaoLoiCSDL(Exception ex, string thaoTac)
    {
        Log.Error(ex, "Lỗi truy cập CSDL khi: {ThaoTac}", thaoTac);
        MessageBox.Show("Không thể truy cập cơ sở dữ liệu. Vui lòng kiểm tra kết nối và thử lại.",
            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private void LamMoiForm()
    {
        dtpNgayChi.Value = DateTime.Today;
        txtNoiDung.Clear();
        cboLoaiChi.SelectedIndex = 0;
        numSoTien.Value = 0;
        txtGhiChu.Clear();
        dgvChiTieu.ClearSelection();
        txtNoiDung.Focus();
    }

    /// <summary>
    /// Kiểm tra hợp lệ: nội dung không rỗng, số tiền > 0, ngày chi không lớn hơn hôm nay
    /// </summary>
    private bool KiemTraHopLe()
    {
        if (string.IsNullOrWhiteSpace(txtNoiDung.Text))
        {
            MessageBox.Show("Nội dung không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtNoiDung.Focus();
            return false;
        }
        if (numSoTien.Value <= 0)
        {
            MessageBox.Show("Số tiền phải lớn hơn 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            numSoTien.Focus();
            return false;
        }
        if (dtpNgayChi.Value.Date > DateTime.Today)
        {
            MessageBox.Show("Ngày chi không được lớn hơn hôm nay.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            dtpNgayChi.Focus();
            return false;
        }
        return true;
    }

    private int? MaDangChon()
    {
        if (dgvChiTieu.CurrentRow == null || !dgvChiTieu.CurrentRow.Selected) return null;
        return Convert.ToInt32(dgvChiTieu.CurrentRow.Cells["colMa"].Value);
    }

    private void dgvChiTieu_SelectionChanged(object? sender, EventArgs e)
    {
        if (_dangNap || dgvChiTieu.SelectedRows.Count == 0) return;
        var row = dgvChiTieu.SelectedRows[0];

        dtpNgayChi.Value = Convert.ToDateTime(row.Cells["colNgayChi"].Value).Date;
        txtNoiDung.Text = row.Cells["colNoiDung"].Value?.ToString();
        cboLoaiChi.SelectedItem = row.Cells["colLoaiChi"].Value?.ToString();
        numSoTien.Value = Convert.ToDecimal(row.Cells["colSoTien"].Value);
        txtGhiChu.Text = row.Cells["colGhiChu"].Value?.ToString();
    }

    private void btnThem_Click(object? sender, EventArgs e)
    {
        if (!KiemTraHopLe()) return;
        try
        {
            using var db = TaoContext();
            var ct = new ChiTieu
            {
                NgayChi = DateOnly.FromDateTime(dtpNgayChi.Value),
                NoiDung = txtNoiDung.Text.Trim(),
                LoaiChi = cboLoaiChi.Text,
                SoTien = numSoTien.Value,
                GhiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim()
            };
            db.ChiTieus.Add(ct);
            db.SaveChanges();

            Log.Information("Thêm khoản chi MaCT={MaCT}", ct.MaCt);
            MessageBox.Show("Thêm khoản chi thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LamMoiForm();
            TaiDuLieu();
        }
        catch (Exception ex)
        {
            BaoLoiCSDL(ex, "Thêm khoản chi");
        }
    }

    private void btnSua_Click(object? sender, EventArgs e)
    {
        var ma = MaDangChon();
        if (ma == null)
        {
            MessageBox.Show("Vui lòng chọn một khoản chi trong danh sách để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        if (!KiemTraHopLe()) return;
        try
        {
            using var db = TaoContext();
            var ct = db.ChiTieus.Find(ma.Value);
            if (ct == null)
            {
                MessageBox.Show("Khoản chi không còn tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TaiDuLieu();
                return;
            }
            ct.NgayChi = DateOnly.FromDateTime(dtpNgayChi.Value);
            ct.NoiDung = txtNoiDung.Text.Trim();
            ct.LoaiChi = cboLoaiChi.Text;
            ct.SoTien = numSoTien.Value;
            ct.GhiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim();
            db.SaveChanges();

            Log.Information("Sửa khoản chi MaCT={MaCT}", ct.MaCt);
            MessageBox.Show("Sửa khoản chi thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LamMoiForm();
            TaiDuLieu();
        }
        catch (Exception ex)
        {
            BaoLoiCSDL(ex, "Sửa khoản chi");
        }
    }

    private void btnXoa_Click(object? sender, EventArgs e)
    {
        var ma = MaDangChon();
        if (ma == null)
        {
            MessageBox.Show("Vui lòng chọn một khoản chi trong danh sách để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        var xacNhan = MessageBox.Show("Bạn có chắc chắn muốn xóa khoản chi này?", "Xác nhận xóa",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (xacNhan != DialogResult.Yes) return;

        try
        {
            using var db = TaoContext();
            var ct = db.ChiTieus.Find(ma.Value);
            if (ct != null)
            {
                db.ChiTieus.Remove(ct);
                db.SaveChanges();
            }

            Log.Information("Xóa khoản chi MaCT={MaCT}", ma.Value);
            MessageBox.Show("Xóa khoản chi thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LamMoiForm();
            TaiDuLieu();
        }
        catch (Exception ex)
        {
            BaoLoiCSDL(ex, "Xóa khoản chi");
        }
    }

    private void btnLamMoi_Click(object? sender, EventArgs e)
    {
        LamMoiForm();

        // Bỏ điều kiện lọc và hiển thị lại toàn bộ danh sách
        _tuKhoa = null;
        _loaiChi = null;
        _tuNgay = null;
        _denNgay = null;
        txtTimKiem.Clear();
        cboLocLoaiChi.SelectedIndex = 0;
        TaiDuLieu();
    }

    private void btnTim_Click(object? sender, EventArgs e)
    {
        if (dtpTuNgay.Value.Date > dtpDenNgay.Value.Date)
        {
            MessageBox.Show("Từ ngày không được lớn hơn Đến ngày.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        _tuKhoa = txtTimKiem.Text.Trim();
        _loaiChi = cboLocLoaiChi.SelectedIndex > 0 ? cboLocLoaiChi.Text : null;
        _tuNgay = DateOnly.FromDateTime(dtpTuNgay.Value);
        _denNgay = DateOnly.FromDateTime(dtpDenNgay.Value);
        TaiDuLieu();
    }
}
