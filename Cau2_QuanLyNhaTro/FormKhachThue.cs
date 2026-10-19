using System.Text.Json;
using System.Text.RegularExpressions;
using Serilog;
using Cau2_QuanLyNhaTro.Models;
using Cau2_QuanLyNhaTro.Services;

namespace Cau2_QuanLyNhaTro;

public partial class FormKhachThue : Form
{
    private const string ThongBaoLoiMang = "Không tải được danh sách địa chỉ, vui lòng kiểm tra mạng";

    private bool _isBinding = false;
    private int? _maKhachChon = null;   // khách đang chọn trên lưới (dùng cho Sửa/Xóa)
    private int _wardRequestId = 0;     // chống kết quả cũ ghi đè khi chọn tỉnh liên tục
    private int _fillId = 0;            // chống race khi click nhanh nhiều dòng

    public FormKhachThue()
    {
        InitializeComponent();
    }

    // ===================== LOAD =====================

    private async void FormKhachThue_Load(object? sender, EventArgs e)
    {
        await LoadComboLocAsync();
        await LoadPhongNhapAsync(null);
        await LoadGridAsync();
        // Gọi API bất đồng bộ, không chặn giao diện
        await LoadTinhAsync();
    }

    private void FormKhachThue_KeyDown(object? sender, KeyEventArgs e)
    {
        // Ctrl+Shift+E: hiện/ẩn nút "Gây lỗi thử"
        if (e.Control && e.Shift && e.KeyCode == Keys.E)
            btnGayLoi.Visible = !btnGayLoi.Visible;
    }

    /// <summary>ComboBox lọc theo phòng và theo tỉnh (có mục "Tất cả").</summary>
    private async Task LoadComboLocAsync()
    {
        _isBinding = true;
        try
        {
            var phongs = await NhaTroService.GetTatCaPhongAsync();
            phongs.Insert(0, new PhongItem(0, "Tất cả"));
            cboLocPhong.DisplayMember = "TenHienThi";
            cboLocPhong.ValueMember = "MaPhong";
            cboLocPhong.DataSource = phongs;

            var tinhs = await NhaTroService.GetTinhDangCoAsync();
            tinhs.Insert(0, new DiaChiItem { code = 0, name = "Tất cả" });
            cboLocTinh.DisplayMember = "name";
            cboLocTinh.ValueMember = "code";
            cboLocTinh.DataSource = tinhs;
        }
        finally { _isBinding = false; }
    }

    /// <summary>ComboBox phòng khi nhập: chỉ phòng Trống (+ phòng hiện tại của khách đang sửa).</summary>
    private async Task LoadPhongNhapAsync(int? maPhongHienTai)
    {
        var ds = await NhaTroService.GetPhongHopLeAsync(maPhongHienTai);
        _isBinding = true;
        try
        {
            cboPhong.DisplayMember = "TenHienThi";
            cboPhong.ValueMember = "MaPhong";
            cboPhong.DataSource = ds;
            if (maPhongHienTai.HasValue) cboPhong.SelectedValue = maPhongHienTai.Value;
            else cboPhong.SelectedIndex = ds.Count > 0 ? 0 : -1;
        }
        finally { _isBinding = false; }
    }

    private async Task LoadGridAsync()
    {
        int? maPhong = cboLocPhong.SelectedValue is int p && p > 0 ? p : null;
        int? maTinh = cboLocTinh.SelectedValue is int t && t > 0 ? t : null;
        var ds = await NhaTroService.TimKhachAsync(txtTimKiem.Text, maPhong, maTinh);

        _isBinding = true;
        try
        {
            dgvKhach.DataSource = ds;
            dgvKhach.ClearSelection();
            dgvKhach.CurrentCell = null;
        }
        finally { _isBinding = false; }
        _maKhachChon = null;
    }

    // ===================== API TỈNH / PHƯỜNG-XÃ =====================

    private void SetLoading(ComboBox cbo)
    {
        _isBinding = true;
        try
        {
            cbo.DataSource = null;
            cbo.Enabled = false;
            cbo.Text = "Đang tải…";
        }
        finally { _isBinding = false; }
    }

    private async Task LoadTinhAsync()
    {
        lblTrangThai.Text = "Đang tải…";
        SetLoading(cboTinh);
        try
        {
            var ds = await DiaChiApiService.GetTinhAsync();
            _isBinding = true;
            try
            {
                cboTinh.DisplayMember = "name";
                cboTinh.ValueMember = "code";
                cboTinh.DataSource = ds;
                cboTinh.SelectedIndex = -1;
                cboTinh.Text = "";
            }
            finally { _isBinding = false; }
            lblTrangThai.Text = $"Đã tải {ds.Count} tỉnh/thành từ provinces.open-api.vn";
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or JsonException)
        {
            Log.Warning(ex, "Không tải được danh sách tỉnh/thành từ API");
            _isBinding = true;
            try { cboTinh.DataSource = null; cboTinh.Text = ""; }
            finally { _isBinding = false; }
            lblTrangThai.Text = ThongBaoLoiMang + " - có thể nhập tay";
            MessageBox.Show(ThongBaoLoiMang + ".\nBạn vẫn có thể gõ tay Tỉnh/Thành và Phường/Xã để lưu khách.",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        finally
        {
            cboTinh.Enabled = true; // mở khóa (ở chế độ DropDown để gõ tay khi lỗi)
        }
    }

    /// <summary>Nạp phường/xã của tỉnh. Trả về true nếu tải được.</summary>
    private async Task<bool> LoadPhuongXaAsync(int maTinh)
    {
        int id = ++_wardRequestId;
        lblTrangThai.Text = "Đang tải…";
        SetLoading(cboPhuongXa);
        try
        {
            var ds = await DiaChiApiService.GetPhuongXaAsync(maTinh);
            if (id != _wardRequestId) return false; // đã chọn tỉnh khác
            _isBinding = true;
            try
            {
                cboPhuongXa.DisplayMember = "name";
                cboPhuongXa.ValueMember = "code";
                cboPhuongXa.DataSource = ds;
                cboPhuongXa.SelectedIndex = -1;
                cboPhuongXa.Text = "";
            }
            finally { _isBinding = false; }
            lblTrangThai.Text = $"Đã tải {ds.Count} phường/xã";
            return true;
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or JsonException)
        {
            if (id != _wardRequestId) return false;
            Log.Warning(ex, "Không tải được danh sách phường/xã của tỉnh {MaTinh}", maTinh);
            _isBinding = true;
            try { cboPhuongXa.DataSource = null; cboPhuongXa.Text = ""; }
            finally { _isBinding = false; }
            lblTrangThai.Text = ThongBaoLoiMang + " - có thể nhập tay";
            MessageBox.Show(ThongBaoLoiMang + ".\nBạn vẫn có thể gõ tay Phường/Xã để lưu khách.",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        finally
        {
            if (id == _wardRequestId) cboPhuongXa.Enabled = true;
        }
    }

    private async void cboTinh_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_isBinding) return;
        if (cboTinh.SelectedIndex >= 0 && cboTinh.SelectedValue is int ma)
        {
            await LoadPhuongXaAsync(ma);
        }
        else
        {
            _wardRequestId++;
            _isBinding = true;
            try { cboPhuongXa.DataSource = null; cboPhuongXa.Text = ""; cboPhuongXa.Enabled = true; }
            finally { _isBinding = false; }
        }
    }

    // ===================== HIỂN THỊ KHÁCH ĐANG CHỌN =====================

    private async void dgvKhach_SelectionChanged(object? sender, EventArgs e)
    {
        if (_isBinding) return;
        if (dgvKhach.SelectedRows.Count == 0) return;
        if (dgvKhach.SelectedRows[0].DataBoundItem is not KhachRow row) return;

        int fill = ++_fillId;
        var k = await NhaTroService.GetKhachAsync(row.MaKhach);
        if (k == null || fill != _fillId) return;

        _maKhachChon = k.MaKhach;
        txtHoTen.Text = k.HoTen;
        if (k.GioiTinh == "Nữ") rdoNu.Checked = true; else rdoNam.Checked = true;
        dtpNgaySinh.Value = k.NgaySinh.HasValue ? k.NgaySinh.Value.ToDateTime(TimeOnly.MinValue) : DateTime.Today;
        txtSoDienThoai.Text = k.SoDienThoai ?? "";
        chkTamTru.Checked = k.DaDangKyTamTru;

        await LoadPhongNhapAsync(k.MaPhong);
        if (fill != _fillId) return;

        // Tỉnh/Thành
        bool coTrongDs = false;
        if (k.MaTinh.HasValue && cboTinh.DataSource != null)
        {
            _isBinding = true;
            try
            {
                cboTinh.SelectedValue = k.MaTinh.Value;
                coTrongDs = cboTinh.SelectedIndex >= 0;
            }
            finally { _isBinding = false; }
        }
        if (!coTrongDs)
        {
            // Offline hoặc không có trong danh sách: hiện tên đã lưu
            _isBinding = true;
            try { cboTinh.SelectedIndex = -1; cboTinh.Text = k.TenTinh ?? ""; }
            finally { _isBinding = false; }
        }

        // Phường/Xã
        bool taiDuoc = coTrongDs && await LoadPhuongXaAsync(k.MaTinh!.Value);
        if (fill != _fillId) return;
        bool coPhuongXa = false;
        if (taiDuoc && k.MaPhuongXa.HasValue)
        {
            _isBinding = true;
            try
            {
                cboPhuongXa.SelectedValue = k.MaPhuongXa.Value;
                coPhuongXa = cboPhuongXa.SelectedIndex >= 0;
            }
            finally { _isBinding = false; }
        }
        if (!coPhuongXa)
        {
            _isBinding = true;
            try
            {
                if (!taiDuoc) { cboPhuongXa.DataSource = null; cboPhuongXa.Enabled = true; }
                cboPhuongXa.SelectedIndex = -1;
                cboPhuongXa.Text = k.TenPhuongXa ?? "";
            }
            finally { _isBinding = false; }
        }
    }

    // ===================== NÚT CHỨC NĂNG =====================

    /// <summary>Đọc & kiểm tra dữ liệu form; trả null nếu không hợp lệ.</summary>
    private KhachThue? DocForm()
    {
        string hoTen = txtHoTen.Text.Trim();
        if (hoTen.Length == 0)
        {
            MessageBox.Show("Vui lòng nhập họ tên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtHoTen.Focus();
            return null;
        }
        string sdt = txtSoDienThoai.Text.Trim();
        if (sdt.Length > 0 && !Regex.IsMatch(sdt, @"^\d{1,15}$"))
        {
            MessageBox.Show("Số điện thoại chỉ gồm chữ số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtSoDienThoai.Focus();
            return null;
        }
        if (dtpNgaySinh.Value.Date > DateTime.Today)
        {
            MessageBox.Show("Ngày sinh không được lớn hơn ngày hiện tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            dtpNgaySinh.Focus();
            return null;
        }
        if (cboPhong.SelectedValue is not int maPhong)
        {
            MessageBox.Show("Vui lòng chọn phòng (chỉ liệt kê phòng còn trống).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            cboPhong.Focus();
            return null;
        }

        // Tỉnh/Thành: chọn từ API thì lưu cả mã + tên; gõ tay thì chỉ lưu tên
        int? maTinh = null; string? tenTinh = cboTinh.Text.Trim();
        if (cboTinh.SelectedIndex >= 0 && cboTinh.SelectedItem is DiaChiItem t) { maTinh = t.code; tenTinh = t.name; }
        int? maPx = null; string? tenPx = cboPhuongXa.Text.Trim();
        if (cboPhuongXa.SelectedIndex >= 0 && cboPhuongXa.SelectedItem is DiaChiItem w) { maPx = w.code; tenPx = w.name; }

        return new KhachThue
        {
            HoTen = hoTen,
            GioiTinh = rdoNu.Checked ? "Nữ" : "Nam",
            NgaySinh = DateOnly.FromDateTime(dtpNgaySinh.Value),
            SoDienThoai = sdt.Length == 0 ? null : sdt,
            MaTinh = maTinh,
            TenTinh = string.IsNullOrEmpty(tenTinh) ? null : tenTinh,
            MaPhuongXa = maPx,
            TenPhuongXa = string.IsNullOrEmpty(tenPx) ? null : tenPx,
            DaDangKyTamTru = chkTamTru.Checked,
            MaPhong = maPhong
        };
    }

    private async void btnThem_Click(object? sender, EventArgs e)
    {
        var k = DocForm();
        if (k == null) return;
        try
        {
            await NhaTroService.ThemAsync(k);
            MessageBox.Show("Đã thêm khách thuê.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await LamMoiAsync(taiLaiLoc: true);
        }
        catch (Exception ex) { BaoLoiThaoTac(ex, "thêm"); }
    }

    private async void btnSua_Click(object? sender, EventArgs e)
    {
        if (_maKhachChon == null)
        {
            MessageBox.Show("Vui lòng chọn khách cần sửa trên danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        var k = DocForm();
        if (k == null) return;
        k.MaKhach = _maKhachChon.Value;
        try
        {
            await NhaTroService.SuaAsync(k);
            MessageBox.Show("Đã cập nhật khách thuê.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await LamMoiAsync(taiLaiLoc: true);
        }
        catch (Exception ex) { BaoLoiThaoTac(ex, "sửa"); }
    }

    private async void btnXoa_Click(object? sender, EventArgs e)
    {
        if (_maKhachChon == null)
        {
            MessageBox.Show("Vui lòng chọn khách cần xóa trên danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (MessageBox.Show("Bạn có chắc muốn xóa khách thuê này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        try
        {
            await NhaTroService.XoaAsync(_maKhachChon.Value);
            MessageBox.Show("Đã xóa khách thuê.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await LamMoiAsync(taiLaiLoc: true);
        }
        catch (Exception ex) { BaoLoiThaoTac(ex, "xóa"); }
    }

    private async void btnLamMoi_Click(object? sender, EventArgs e)
    {
        await LamMoiAsync(taiLaiLoc: true);
    }

    /// <summary>Xóa trắng form, nạp lại phòng hợp lệ, lưới và combo lọc.</summary>
    private async Task LamMoiAsync(bool taiLaiLoc)
    {
        _fillId++;
        _maKhachChon = null;
        txtHoTen.Clear();
        rdoNam.Checked = true;
        dtpNgaySinh.Value = DateTime.Today;
        txtSoDienThoai.Clear();
        chkTamTru.Checked = false;

        _wardRequestId++;
        _isBinding = true;
        try
        {
            cboTinh.SelectedIndex = -1;
            cboTinh.Text = "";
            cboPhuongXa.DataSource = null;
            cboPhuongXa.Text = "";
            cboPhuongXa.Enabled = true;
        }
        finally { _isBinding = false; }

        await LoadPhongNhapAsync(null);
        if (taiLaiLoc)
        {
            int? p = cboLocPhong.SelectedValue is int pp ? pp : null;
            int? t = cboLocTinh.SelectedValue is int tt ? tt : null;
            await LoadComboLocAsync();
            _isBinding = true;
            try
            {
                if (p.HasValue) cboLocPhong.SelectedValue = p.Value;
                if (t.HasValue) cboLocTinh.SelectedValue = t.Value;
            }
            finally { _isBinding = false; }
        }
        await LoadGridAsync();
        txtHoTen.Focus();
    }

    private void BaoLoiThaoTac(Exception ex, string thaoTac)
    {
        Log.Error(ex, "Lỗi khi {ThaoTac} khách thuê", thaoTac);
        MessageBox.Show($"Không thể {thaoTac} khách thuê: {ex.Message}", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    // ===================== TÌM KIẾM / LỌC =====================

    private async void btnTim_Click(object? sender, EventArgs e)
    {
        await LoadGridAsync();
    }

    private async void btnBoLoc_Click(object? sender, EventArgs e)
    {
        _isBinding = true;
        try
        {
            txtTimKiem.Clear();
            if (cboLocPhong.Items.Count > 0) cboLocPhong.SelectedIndex = 0;
            if (cboLocTinh.Items.Count > 0) cboLocTinh.SelectedIndex = 0;
        }
        finally { _isBinding = false; }
        await LoadGridAsync();
    }

    // ===================== LỖI TOÀN CỤC (nút ẩn) =====================

    private void btnGayLoi_Click(object? sender, EventArgs e)
    {
        // Cố ý ném lỗi không được bắt -> Application.ThreadException xử lý
        throw new InvalidOperationException("Lỗi thử nghiệm: người dùng bấm nút 'Gây lỗi thử'");
    }
}
