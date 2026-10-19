using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Cau3_KeHoachDaNgoai.Models;

namespace Cau3_KeHoachDaNgoai;

public partial class FormChuyenDi : Form
{
    private readonly WeatherService _weather = new();
    private bool _isBinding = false;
    private bool _isLoadingWeather = false;

    // Trạng thái panel thời tiết, giữ lại để vẽ lại ngay khi đổi ngôn ngữ
    private enum WeatherState { None, Result, NoData }
    private WeatherState _wxState = WeatherState.None;
    private WeatherDay? _wxDay;
    private string _wxPlace = "";
    private DateOnly _wxDate;

    public FormChuyenDi()
    {
        InitializeComponent();
        nudNganSach.Maximum = 999999999999m; // DECIMAL(18,0) thừa sức cho ngân sách
    }

    // ===================== LOAD / NGÔN NGỮ =====================

    private async void FormChuyenDi_Load(object? sender, EventArgs e)
    {
        // Đọc ngôn ngữ đã lưu trong usersettings.json
        var settings = UserSettings.Load();
        Lang.SetLanguage(settings.Language);

        _isBinding = true;
        cboNgonNgu.SelectedIndex = Lang.Current == "en" ? 1 : 0;
        _isBinding = false;

        ApplyLanguage();
        await LoadDataAsync();
    }

    private void cboNgonNgu_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_isBinding) return;
        Lang.SetLanguage(cboNgonNgu.SelectedIndex == 1 ? "en" : "vi");
        new UserSettings { Language = Lang.Current }.Save();
        ApplyLanguage();
        dgvChuyenDi.Invalidate();
    }

    /// <summary>Áp dụng lại ngôn ngữ + định dạng ngày/tiền cho toàn bộ control, không cần khởi động lại.</summary>
    private void ApplyLanguage()
    {
        var ci = Lang.Culture;
        _isBinding = true;

        Text = Lang.T("form.title");
        lblTieuDe.Text = Lang.T("lbl.title");
        lblNgonNgu.Text = Lang.T("lbl.language");
        grpThongTin.Text = Lang.T("grp.info");
        lblTenChuyen.Text = Lang.T("lbl.name");
        lblDiaDiem.Text = Lang.T("lbl.place");
        lblNgayDi.Text = Lang.T("lbl.date");
        lblSoNguoi.Text = Lang.T("lbl.people");
        lblNganSach.Text = Lang.T("lbl.budget");
        lblPhuongTien.Text = Lang.T("lbl.vehicle");
        chkDaHoanThanh.Text = Lang.T("chk.done");
        btnThem.Text = Lang.T("btn.add");
        btnSua.Text = Lang.T("btn.edit");
        btnXoa.Text = Lang.T("btn.delete");
        btnLamMoi.Text = Lang.T("btn.reset");
        btnXemThoiTiet.Text = Lang.T("btn.weather");
        txtTim.PlaceholderText = Lang.T("txt.search.hint");
        chkChiChuaHoanThanh.Text = Lang.T("chk.onlyPending");
        lblTu.Text = Lang.T("lbl.from");
        lblDen.Text = Lang.T("lbl.to");
        btnTim.Text = Lang.T("btn.search");

        // Tiêu đề cột lưới
        colMa.HeaderText = Lang.T("col.id");
        colTen.HeaderText = Lang.T("col.name");
        colDiaDiem.HeaderText = Lang.T("col.place");
        colNgayDi.HeaderText = Lang.T("col.date");
        colSoNguoi.HeaderText = Lang.T("col.people");
        colNganSach.HeaderText = Lang.T("col.budget");
        colPhuongTien.HeaderText = Lang.T("col.vehicle");
        colHoanThanh.HeaderText = Lang.T("col.done");

        // Định dạng ngày theo CultureInfo: DateTimePicker và lưới
        string pattern = ci.DateTimeFormat.ShortDatePattern;
        dtpNgayDi.CustomFormat = pattern;
        dtpTu.CustomFormat = pattern;
        dtpDen.CustomFormat = pattern;
        colNgayDi.DefaultCellStyle.FormatProvider = ci;
        colNganSach.DefaultCellStyle.FormatProvider = ci;

        // NumericUpDown cần làm mới chuỗi hiển thị theo culture (dấu phân cách nghìn)
        nudNganSach.ThousandsSeparator = false;
        nudNganSach.ThousandsSeparator = true;
        nudSoNguoi.ThousandsSeparator = true;
        nudSoNguoi.ThousandsSeparator = false;

        // Phương tiện: lưu tiếng Việt trong CSDL, hiển thị theo ngôn ngữ
        int sel = cboPhuongTien.SelectedIndex;
        cboPhuongTien.Items.Clear();
        foreach (var v in Lang.Vehicles) cboPhuongTien.Items.Add(Lang.Vehicle(v));
        cboPhuongTien.SelectedIndex = sel >= 0 ? sel : 0;

        _isBinding = false;
        RenderWeather();
    }

    private void dgvChuyenDi_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex >= 0 && e.ColumnIndex == colPhuongTien.Index && e.Value is string s)
        {
            e.Value = Lang.Vehicle(s);
            e.FormattingApplied = true;
        }
    }

    // ===================== HIỂN THỊ DỮ LIỆU / TÌM KIẾM =====================

    private async Task LoadDataAsync()
    {
        try
        {
            using var db = new QlDaNgoaiContext();
            var query = db.ChuyenDis.AsNoTracking().AsQueryable();

            string kw = txtTim.Text.Trim();
            if (kw.Length > 0)
                query = query.Where(x => x.TenChuyen.Contains(kw) || x.DiaDiem.Contains(kw));
            if (chkChiChuaHoanThanh.Checked)
                query = query.Where(x => !x.DaHoanThanh);
            if (dtpTu.Checked)
            {
                var tu = DateOnly.FromDateTime(dtpTu.Value);
                query = query.Where(x => x.NgayDi >= tu);
            }
            if (dtpDen.Checked)
            {
                var den = DateOnly.FromDateTime(dtpDen.Value);
                query = query.Where(x => x.NgayDi <= den);
            }

            var list = await query.OrderBy(x => x.NgayDi).ThenBy(x => x.MaChuyen).ToListAsync();

            _isBinding = true;
            dgvChuyenDi.DataSource = list;
            dgvChuyenDi.ClearSelection();
            _isBinding = false;
        }
        catch (Exception ex)
        {
            _isBinding = false;
            MessageBox.Show(Lang.T("msg.dbError", ex.Message), Lang.T("msg.error"),
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnTim_Click(object? sender, EventArgs e)
    {
        if (dtpTu.Checked && dtpDen.Checked && dtpTu.Value.Date > dtpDen.Value.Date)
        {
            MessageBox.Show(Lang.T("msg.fromAfterTo"), Lang.T("msg.title"),
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        await LoadDataAsync();
    }

    private void dgvChuyenDi_SelectionChanged(object? sender, EventArgs e)
    {
        if (_isBinding || dgvChuyenDi.CurrentRow?.DataBoundItem is not ChuyenDi c) return;
        if (!dgvChuyenDi.CurrentRow.Selected) return;

        txtTenChuyen.Text = c.TenChuyen;
        txtDiaDiem.Text = c.DiaDiem;
        dtpNgayDi.Value = c.NgayDi.ToDateTime(TimeOnly.MinValue);
        nudSoNguoi.Value = Math.Clamp(c.SoNguoi, (int)nudSoNguoi.Minimum, (int)nudSoNguoi.Maximum);
        nudNganSach.Value = Math.Clamp(c.NganSach, nudNganSach.Minimum, nudNganSach.Maximum);
        int idx = Array.IndexOf(Lang.Vehicles, c.PhuongTien);
        cboPhuongTien.SelectedIndex = idx >= 0 ? idx : 0;
        chkDaHoanThanh.Checked = c.DaHoanThanh;
    }

    // ===================== CRUD =====================

    private ChuyenDi? SelectedTrip => dgvChuyenDi.CurrentRow?.Selected == true
        ? dgvChuyenDi.CurrentRow.DataBoundItem as ChuyenDi : null;

    /// <summary>Kiểm tra hợp lệ; isNew = true thì ngày đi không được nhỏ hơn hôm nay.</summary>
    private bool ValidateInput(bool isNew)
    {
        string? err = null;
        Control? focus = null;

        if (string.IsNullOrWhiteSpace(txtTenChuyen.Text)) { err = "msg.nameRequired"; focus = txtTenChuyen; }
        else if (string.IsNullOrWhiteSpace(txtDiaDiem.Text)) { err = "msg.placeRequired"; focus = txtDiaDiem; }
        else if (isNew && dtpNgayDi.Value.Date < DateTime.Today) { err = "msg.datePast"; focus = dtpNgayDi; }
        else if (nudSoNguoi.Value < 1) { err = "msg.peopleInvalid"; focus = nudSoNguoi; }
        else if (nudNganSach.Value < 0) { err = "msg.budgetInvalid"; focus = nudNganSach; }

        if (err == null) return true;
        MessageBox.Show(Lang.T(err), Lang.T("msg.title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        focus?.Focus();
        return false;
    }

    private async void btnThem_Click(object? sender, EventArgs e)
    {
        if (!ValidateInput(isNew: true)) return;
        try
        {
            using var db = new QlDaNgoaiContext();
            db.ChuyenDis.Add(new ChuyenDi
            {
                TenChuyen = txtTenChuyen.Text.Trim(),
                DiaDiem = txtDiaDiem.Text.Trim(),
                NgayDi = DateOnly.FromDateTime(dtpNgayDi.Value),
                SoNguoi = (int)nudSoNguoi.Value,
                NganSach = nudNganSach.Value,
                PhuongTien = Lang.Vehicles[cboPhuongTien.SelectedIndex],
                DaHoanThanh = chkDaHoanThanh.Checked
            });
            await db.SaveChangesAsync();
            MessageBox.Show(Lang.T("msg.added"), Lang.T("msg.title"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            await LoadDataAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(Lang.T("msg.dbError", ex.Message), Lang.T("msg.error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnSua_Click(object? sender, EventArgs e)
    {
        var sel = SelectedTrip;
        if (sel == null)
        {
            MessageBox.Show(Lang.T("msg.pickRow"), Lang.T("msg.title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (!ValidateInput(isNew: false)) return;
        try
        {
            using var db = new QlDaNgoaiContext();
            var c = await db.ChuyenDis.FindAsync(sel.MaChuyen);
            if (c == null)
            {
                MessageBox.Show(Lang.T("msg.notFoundRow"), Lang.T("msg.title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                await LoadDataAsync();
                return;
            }
            c.TenChuyen = txtTenChuyen.Text.Trim();
            c.DiaDiem = txtDiaDiem.Text.Trim();
            c.NgayDi = DateOnly.FromDateTime(dtpNgayDi.Value);
            c.SoNguoi = (int)nudSoNguoi.Value;
            c.NganSach = nudNganSach.Value;
            c.PhuongTien = Lang.Vehicles[cboPhuongTien.SelectedIndex];
            c.DaHoanThanh = chkDaHoanThanh.Checked;
            await db.SaveChangesAsync();
            MessageBox.Show(Lang.T("msg.updated"), Lang.T("msg.title"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            await LoadDataAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(Lang.T("msg.dbError", ex.Message), Lang.T("msg.error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnXoa_Click(object? sender, EventArgs e)
    {
        var sel = SelectedTrip;
        if (sel == null)
        {
            MessageBox.Show(Lang.T("msg.pickRow"), Lang.T("msg.title"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (MessageBox.Show(Lang.T("msg.confirmDelete", sel.TenChuyen), Lang.T("msg.confirm"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        try
        {
            using var db = new QlDaNgoaiContext();
            var c = await db.ChuyenDis.FindAsync(sel.MaChuyen);
            if (c != null)
            {
                db.ChuyenDis.Remove(c);
                await db.SaveChangesAsync();
            }
            ClearInputs();
            await LoadDataAsync();
            MessageBox.Show(Lang.T("msg.deleted"), Lang.T("msg.title"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(Lang.T("msg.dbError", ex.Message), Lang.T("msg.error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ClearInputs()
    {
        txtTenChuyen.Clear();
        txtDiaDiem.Clear();
        dtpNgayDi.Value = DateTime.Today;
        nudSoNguoi.Value = 1;
        nudNganSach.Value = 0;
        cboPhuongTien.SelectedIndex = 0;
        chkDaHoanThanh.Checked = false;
        txtTenChuyen.Focus();
    }

    private async void btnLamMoi_Click(object? sender, EventArgs e)
    {
        ClearInputs();
        // Làm mới cũng xóa bộ lọc tìm kiếm
        txtTim.Clear();
        chkChiChuaHoanThanh.Checked = false;
        dtpTu.Checked = false;
        dtpDen.Checked = false;
        _wxState = WeatherState.None;
        RenderWeather();
        await LoadDataAsync();
        dgvChuyenDi.ClearSelection();
    }

    // ===================== THỜI TIẾT =====================

    private async void btnXemThoiTiet_Click(object? sender, EventArgs e)
    {
        var trip = SelectedTrip;
        if (trip == null)
        {
            MessageBox.Show(Lang.T("msg.pickToWeather"), Lang.T("msg.title"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        if (_isLoadingWeather) return;

        SetLoading(true);
        try
        {
            var day = await _weather.GetWeatherAsync(trip.DiaDiem, trip.NgayDi);
            _wxPlace = trip.DiaDiem;
            _wxDate = trip.NgayDi;
            _wxDay = day;
            _wxState = day == null ? WeatherState.NoData : WeatherState.Result;
            RenderWeather();
        }
        catch (WeatherException ex)
        {
            string key = ex.Kind switch
            {
                WeatherError.PlaceNotFound => "msg.placeNotFound",
                WeatherError.Timeout => "msg.timeout",
                WeatherError.Network => "msg.network",
                _ => "msg.badData"
            };
            string text = ex.Kind == WeatherError.PlaceNotFound ? Lang.T(key, trip.DiaDiem) : Lang.T(key);
            MessageBox.Show(text, Lang.T("msg.title"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(Lang.T("msg.dbError", ex.Message), Lang.T("msg.error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            SetLoading(false);
        }
    }

    /// <summary>Khóa nút và hiện ProgressBar kiểu Marquee lúc đang tải.</summary>
    private void SetLoading(bool loading)
    {
        _isLoadingWeather = loading;
        btnXemThoiTiet.Enabled = !loading;
        if (loading)
        {
            progressBar.Style = ProgressBarStyle.Marquee;
        }
        else
        {
            progressBar.Style = ProgressBarStyle.Blocks;
            progressBar.Value = 0;
        }
    }

    /// <summary>Vẽ panel thời tiết theo trạng thái hiện tại và ngôn ngữ hiện tại.</summary>
    private void RenderWeather()
    {
        pnlCanhBao.Visible = false;
        switch (_wxState)
        {
            case WeatherState.Result when _wxDay != null:
                var d = _wxDay;
                var kind = WeatherService.GetKind(d.WeatherCode);
                grpThoiTiet.Text = Lang.T("grp.weather.for", _wxDate.ToString("d", Lang.Culture), _wxPlace);
                lblIcon.Text = WeatherService.GetIcon(kind);
                lblMoTa.Text = Lang.T(WeatherService.GetDescriptionKey(kind));
                lblNhietDo.Text = Lang.T("wx.temp", d.TempMin, d.TempMax);
                lblXacSuatMua.Text = d.RainProbability is int p ? Lang.T("wx.rainProb", p) : Lang.T("wx.rainProbNA");
                if (WeatherService.ShouldWarn(d.RainProbability))
                {
                    lblCanhBao.Text = "⚠  " + Lang.T("wx.advice");
                    pnlCanhBao.Visible = true;
                }
                break;
            case WeatherState.NoData:
                grpThoiTiet.Text = Lang.T("grp.weather.for", _wxDate.ToString("d", Lang.Culture), _wxPlace);
                lblIcon.Text = "";
                lblMoTa.Text = Lang.T("wx.noData");
                lblNhietDo.Text = "";
                lblXacSuatMua.Text = "";
                break;
            default:
                grpThoiTiet.Text = Lang.T("grp.weather");
                lblIcon.Text = "";
                lblMoTa.Text = "";
                lblNhietDo.Text = "";
                lblXacSuatMua.Text = "";
                break;
        }
    }
}
