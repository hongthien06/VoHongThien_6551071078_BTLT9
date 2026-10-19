using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Cau5_QuanLyDatTour.Models;

namespace Cau5_QuanLyDatTour;

public partial class Form1 : Form
{
    private readonly TyGiaService _tyGiaService = new(AppConfig.ExchangeRate);
    private bool _isBinding = false;
    private int _tyGiaSeq = 0;          // chống ghi đè khi đổi tour nhanh
    private decimal? _tyGiaHienTai;     // tỷ giá đang hiển thị (đã làm tròn 4 số lẻ)

    private record KhachItem(int MaKh, string HienThi);
    private record DatTourRow(string Ma, string KhachHang, string Tour, int SoNguoi, decimal TyGia, decimal TongTienVnd, DateTime NgayDat);

    public Form1()
    {
        InitializeComponent();

        // Phiên bản hiển thị trên thanh tiêu đề
        string version = Assembly.GetExecutingAssembly()
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion?.Split('+')[0] ?? "1.0.0";
        Text = $"Quản lý đặt tour - Bình An v{version}";

        cboNgoaiTe.Items.AddRange(new object[] { "THB", "KRW", "JPY", "USD" });
        cboNgoaiTe.SelectedIndex = 0;

        cboThang.Items.Add("Tất cả");
        for (int i = 1; i <= 12; i++) cboThang.Items.Add($"Tháng {i}");
        cboThang.SelectedIndex = 0;

        // Tab Tour
        btnThemTour.Click += btnThemTour_Click;
        btnSuaTour.Click += btnSuaTour_Click;
        btnXoaTour.Click += btnXoaTour_Click;
        btnTimTour.Click += async (s, e) => await LoadTourAsync();
        txtTimTour.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) await LoadTourAsync(); };
        dgvTour.SelectionChanged += dgvTour_SelectionChanged;

        // Tab Khách hàng
        btnThemKH.Click += btnThemKH_Click;
        btnSuaKH.Click += btnSuaKH_Click;
        btnXoaKH.Click += btnXoaKH_Click;
        btnTimKH.Click += async (s, e) => await LoadKhachAsync();
        txtTimKH.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) await LoadKhachAsync(); };
        dgvKhach.SelectionChanged += dgvKhach_SelectionChanged;

        // Tab Đặt tour
        tabMain.SelectedIndexChanged += async (s, e) => { if (tabMain.SelectedTab == tabDat) await LoadTabDatAsync(); };
        cboTour.SelectedIndexChanged += async (s, e) => await CapNhatThongTinTourAsync();
        nudSoNguoi.ValueChanged += (s, e) => CapNhatTongTien();
        nudSoNguoi.Validating += nudSoNguoi_Validating;
        btnDatTour.Click += btnDatTour_Click;
        btnTimDat.Click += async (s, e) => await LoadDatTourAsync();
        txtTimDat.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) await LoadDatTourAsync(); };
    }

    private async void Form1_Load(object? sender, EventArgs e)
    {
        await LoadTourAsync();
        await LoadKhachAsync();
    }

    private static void BaoLoi(string msg) =>
        MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

    // ================= TAB TOUR =================

    private async Task LoadTourAsync()
    {
        try
        {
            using var db = AppConfig.CreateContext();
            string key = txtTimTour.Text.Trim();
            var query = db.Tours.AsNoTracking().AsQueryable();
            if (key != "")
                query = query.Where(t => t.TenTour.Contains(key) || (t.DiemDen != null && t.DiemDen.Contains(key)));
            _isBinding = true;
            dgvTour.DataSource = await query.OrderBy(t => t.MaTour).ToListAsync();
            _isBinding = false;
            dgvTour.ClearSelection();
        }
        catch (Exception ex)
        {
            _isBinding = false;
            Log.Error(ex, "Lỗi tải danh sách tour");
            MessageBox.Show("Không tải được danh sách tour: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void dgvTour_SelectionChanged(object? sender, EventArgs e)
    {
        if (_isBinding || dgvTour.CurrentRow?.DataBoundItem is not Tour t) return;
        txtTenTour.Text = t.TenTour;
        txtDiemDen.Text = t.DiemDen;
        cboNgoaiTe.SelectedItem = t.NgoaiTe.Trim();
        nudGiaNgoaiTe.Value = t.GiaNgoaiTe;
        dtpKhoiHanh.Value = t.NgayKhoiHanh?.ToDateTime(TimeOnly.MinValue) ?? DateTime.Today;
        nudSoCho.Value = Math.Max(nudSoCho.Minimum, Math.Min(nudSoCho.Maximum, t.SoChoToiDa));
    }

    private bool KiemTraTour()
    {
        if (txtTenTour.Text.Trim() == "") { BaoLoi("Vui lòng nhập tên tour."); txtTenTour.Focus(); return false; }
        if (cboNgoaiTe.SelectedItem == null) { BaoLoi("Vui lòng chọn ngoại tệ."); return false; }
        if (nudGiaNgoaiTe.Value <= 0) { BaoLoi("Giá phải lớn hơn 0."); nudGiaNgoaiTe.Focus(); return false; }
        return true;
    }

    private void GanTour(Tour t)
    {
        t.TenTour = txtTenTour.Text.Trim();
        t.DiemDen = txtDiemDen.Text.Trim();
        t.NgoaiTe = cboNgoaiTe.SelectedItem!.ToString()!;
        t.GiaNgoaiTe = nudGiaNgoaiTe.Value;
        t.NgayKhoiHanh = DateOnly.FromDateTime(dtpKhoiHanh.Value);
        t.SoChoToiDa = (int)nudSoCho.Value;
    }

    private async void btnThemTour_Click(object? sender, EventArgs e)
    {
        if (!KiemTraTour()) return;
        using var db = AppConfig.CreateContext();
        var t = new Tour();
        GanTour(t);
        db.Tours.Add(t);
        await db.SaveChangesAsync();
        await LoadTourAsync();
    }

    private async void btnSuaTour_Click(object? sender, EventArgs e)
    {
        if (dgvTour.CurrentRow?.DataBoundItem is not Tour sel) { BaoLoi("Vui lòng chọn tour cần sửa."); return; }
        if (!KiemTraTour()) return;
        using var db = AppConfig.CreateContext();
        var t = await db.Tours.FindAsync(sel.MaTour);
        if (t == null) return;
        GanTour(t);
        await db.SaveChangesAsync();
        await LoadTourAsync();
    }

    private async void btnXoaTour_Click(object? sender, EventArgs e)
    {
        if (dgvTour.CurrentRow?.DataBoundItem is not Tour sel) { BaoLoi("Vui lòng chọn tour cần xóa."); return; }
        if (MessageBox.Show($"Xóa tour \"{sel.TenTour}\"?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        try
        {
            using var db = AppConfig.CreateContext();
            var t = await db.Tours.FindAsync(sel.MaTour);
            if (t == null) return;
            db.Tours.Remove(t);
            await db.SaveChangesAsync();
            await LoadTourAsync();
        }
        catch (DbUpdateException)
        {
            BaoLoi("Tour đã có đơn đặt, không thể xóa.");
        }
    }

    // ================= TAB KHÁCH HÀNG =================

    private async Task LoadKhachAsync()
    {
        try
        {
            using var db = AppConfig.CreateContext();
            string key = txtTimKH.Text.Trim();
            var query = db.KhachHangs.AsNoTracking().AsQueryable();
            if (key != "")
                query = query.Where(k => k.HoTen.Contains(key) || (k.SoDienThoai != null && k.SoDienThoai.Contains(key)));
            _isBinding = true;
            dgvKhach.DataSource = await query.OrderBy(k => k.MaKh).ToListAsync();
            _isBinding = false;
            dgvKhach.ClearSelection();
        }
        catch (Exception ex)
        {
            _isBinding = false;
            Log.Error(ex, "Lỗi tải danh sách khách hàng");
            MessageBox.Show("Không tải được danh sách khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void dgvKhach_SelectionChanged(object? sender, EventArgs e)
    {
        if (_isBinding || dgvKhach.CurrentRow?.DataBoundItem is not KhachHang k) return;
        txtHoTen.Text = k.HoTen;
        txtSdt.Text = k.SoDienThoai;
        txtEmail.Text = k.Email;
    }

    private bool KiemTraKhach()
    {
        if (txtHoTen.Text.Trim() == "") { BaoLoi("Vui lòng nhập họ tên."); txtHoTen.Focus(); return false; }
        return true;
    }

    private void GanKhach(KhachHang k)
    {
        k.HoTen = txtHoTen.Text.Trim();
        k.SoDienThoai = txtSdt.Text.Trim();
        k.Email = txtEmail.Text.Trim();
    }

    private async void btnThemKH_Click(object? sender, EventArgs e)
    {
        if (!KiemTraKhach()) return;
        using var db = AppConfig.CreateContext();
        var k = new KhachHang();
        GanKhach(k);
        db.KhachHangs.Add(k);
        await db.SaveChangesAsync();
        await LoadKhachAsync();
    }

    private async void btnSuaKH_Click(object? sender, EventArgs e)
    {
        if (dgvKhach.CurrentRow?.DataBoundItem is not KhachHang sel) { BaoLoi("Vui lòng chọn khách hàng cần sửa."); return; }
        if (!KiemTraKhach()) return;
        using var db = AppConfig.CreateContext();
        var k = await db.KhachHangs.FindAsync(sel.MaKh);
        if (k == null) return;
        GanKhach(k);
        await db.SaveChangesAsync();
        await LoadKhachAsync();
    }

    private async void btnXoaKH_Click(object? sender, EventArgs e)
    {
        if (dgvKhach.CurrentRow?.DataBoundItem is not KhachHang sel) { BaoLoi("Vui lòng chọn khách hàng cần xóa."); return; }
        if (MessageBox.Show($"Xóa khách hàng \"{sel.HoTen}\"?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        try
        {
            using var db = AppConfig.CreateContext();
            var k = await db.KhachHangs.FindAsync(sel.MaKh);
            if (k == null) return;
            db.KhachHangs.Remove(k);
            await db.SaveChangesAsync();
            await LoadKhachAsync();
        }
        catch (DbUpdateException)
        {
            BaoLoi("Khách hàng đã có đơn đặt tour, không thể xóa.");
        }
    }

    // ================= TAB ĐẶT TOUR =================

    private async Task LoadTabDatAsync()
    {
        try
        {
            using var db = AppConfig.CreateContext();
            var khach = await db.KhachHangs.AsNoTracking().OrderBy(k => k.MaKh)
                .Select(k => new KhachItem(k.MaKh, k.HoTen + " - " + k.SoDienThoai)).ToListAsync();
            var tours = await db.Tours.AsNoTracking().OrderBy(t => t.MaTour).ToListAsync();

            _isBinding = true;
            cboKhachHang.DataSource = khach;
            cboKhachHang.DisplayMember = nameof(KhachItem.HienThi);
            cboKhachHang.ValueMember = nameof(KhachItem.MaKh);
            cboTour.DataSource = tours;
            cboTour.DisplayMember = nameof(Tour.TenTour);
            cboTour.ValueMember = nameof(Tour.MaTour);
            _isBinding = false;
        }
        catch (Exception ex)
        {
            _isBinding = false;
            Log.Error(ex, "Lỗi tải dữ liệu tab Đặt tour");
            MessageBox.Show("Không tải được dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        await CapNhatThongTinTourAsync();
        await LoadDatTourAsync();
    }

    /// <summary>Khi chọn tour: cập nhật số chỗ còn lại, giá/người và lấy tỷ giá hôm nay từ API.</summary>
    private async Task CapNhatThongTinTourAsync()
    {
        if (_isBinding || cboTour.SelectedItem is not Tour tour) return;
        int seq = ++_tyGiaSeq;

        lblGiaNguoi.Text = $"{tour.GiaNgoaiTe:#,##0.##} {tour.NgoaiTe.Trim()}";
        _tyGiaHienTai = null;
        lblTyGia.Text = "Đang lấy tỷ giá...";
        lblNguonTyGia.Text = "";
        lblTongTien.Text = "";

        try
        {
            using (var db = AppConfig.CreateContext())
            {
                int conLai = await DatTourService.ConLaiAsync(db, tour.MaTour);
                nudSoNguoi.Minimum = conLai > 0 ? 1 : 0;
                nudSoNguoi.Maximum = Math.Max(conLai, 0);
                if (nudSoNguoi.Value > nudSoNguoi.Maximum) nudSoNguoi.Value = nudSoNguoi.Maximum;
                if (nudSoNguoi.Value < nudSoNguoi.Minimum) nudSoNguoi.Value = nudSoNguoi.Minimum;
            }

            var kq = await _tyGiaService.LayTyGiaAsync(tour.NgoaiTe);
            if (seq != _tyGiaSeq) return;   // đã chọn tour khác
            _tyGiaHienTai = Math.Round(kq.TyGia, 4);
            lblTyGia.Text = $"1 {tour.NgoaiTe.Trim()} = {_tyGiaHienTai:#,##0.####} VND";
            lblNguonTyGia.Text = $" Tỷ giá lấy từ: {kq.Nguon}{(kq.TuCache ? " (bộ nhớ đệm)" : "")} ";
            CapNhatTongTien();
        }
        catch (Exception ex)
        {
            if (seq != _tyGiaSeq) return;
            Log.Error(ex, "Lỗi lấy tỷ giá");
            lblTyGia.Text = "Không lấy được tỷ giá";
            MessageBox.Show(ex.Message, "Lỗi tỷ giá", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void CapNhatTongTien()
    {
        if (cboTour.SelectedItem is Tour tour && _tyGiaHienTai.HasValue)
            lblTongTien.Text = DatTourService.TinhTong(tour.GiaNgoaiTe, (int)nudSoNguoi.Value, _tyGiaHienTai.Value).ToString("#,##0") + " đ";
        else
            lblTongTien.Text = "";
    }

    /// <summary>Gõ số người vượt số chỗ còn lại thì báo và đưa về mức tối đa.</summary>
    private void nudSoNguoi_Validating(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        if (int.TryParse(nudSoNguoi.Text, out int v) && v > nudSoNguoi.Maximum)
        {
            BaoLoi($"Tour chỉ còn {(int)nudSoNguoi.Maximum} chỗ.");
            nudSoNguoi.Value = nudSoNguoi.Maximum;
        }
    }

    private async void btnDatTour_Click(object? sender, EventArgs e)
    {
        if (cboKhachHang.SelectedItem is not KhachItem kh || cboTour.SelectedItem is not Tour tour)
        {
            BaoLoi("Vui lòng chọn khách hàng và tour.");
            return;
        }
        if (!_tyGiaHienTai.HasValue)
        {
            BaoLoi("Chưa có tỷ giá hiện hành, không thể đặt tour.");
            return;
        }
        try
        {
            // Lưu kèm tỷ giá tại thời điểm đặt
            var dat = await DatTourService.DatAsync(kh.MaKh, tour.MaTour, (int)nudSoNguoi.Value, _tyGiaHienTai.Value);
            MessageBox.Show($"Đặt tour thành công. Tổng tiền: {dat.TongTienVnd:#,##0} đ", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (InvalidOperationException ex)
        {
            BaoLoi(ex.Message);   // vượt số chỗ còn lại
        }
        await CapNhatThongTinTourAsync();
        await LoadDatTourAsync();
    }

    private async Task LoadDatTourAsync()
    {
        try
        {
            using var db = AppConfig.CreateContext();
            string key = txtTimDat.Text.Trim();
            int thang = cboThang.SelectedIndex;   // 0 = tất cả, 1..12 = tháng khởi hành

            var query = db.DatTours.AsNoTracking()
                .Include(d => d.MaKhNavigation).Include(d => d.MaTourNavigation).AsQueryable();
            if (key != "")
                query = query.Where(d => d.MaKhNavigation.HoTen.Contains(key) || d.MaTourNavigation.TenTour.Contains(key));
            if (thang > 0)
                query = query.Where(d => d.MaTourNavigation.NgayKhoiHanh != null && d.MaTourNavigation.NgayKhoiHanh.Value.Month == thang);

            var list = (await query.OrderBy(d => d.MaDat).ToListAsync())
                .Select(d => new DatTourRow("DT" + d.MaDat.ToString("D3"), d.MaKhNavigation.HoTen, d.MaTourNavigation.TenTour,
                    d.SoNguoi, d.TyGia, d.TongTienVnd, d.NgayDat)).ToList();

            dgvDatTour.DataSource = list;
            lblTongDoanhThu.Text = $"Tổng doanh thu: {list.Sum(r => r.TongTienVnd):#,##0} đ";
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Lỗi tải danh sách đặt tour");
            MessageBox.Show("Không tải được danh sách đặt tour: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
