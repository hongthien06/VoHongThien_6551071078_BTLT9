using Cau4_QuanTraSua.Models;
using Microsoft.EntityFrameworkCore;

namespace Cau4_QuanTraSua;

/// <summary>Quản lý thực đơn: CRUD món, tìm theo tên món và lọc theo danh mục (join 2 bảng).</summary>
public partial class FormThucDon : Form
{
    private readonly QlTraSuaContext _context = new();
    private bool _isBinding = false;

    public FormThucDon()
    {
        InitializeComponent();
    }

    private async void FormThucDon_Load(object sender, EventArgs e)
    {
        await LoadDanhMucAsync();
        await LoadDataAsync();
    }

    private async Task LoadDanhMucAsync()
    {
        try
        {
            _isBinding = true;
            var ds = await _context.DanhMucMons.AsNoTracking()
                .OrderBy(x => x.MaDm)
                .Select(x => new { x.MaDm, x.TenDm })
                .ToListAsync();

            cboDanhMuc.DataSource = ds;
            cboDanhMuc.DisplayMember = "TenDm";
            cboDanhMuc.ValueMember = "MaDm";

            // ComboBox lọc có thêm mục Tất cả
            var loc = ds.ToList();
            loc.Insert(0, new { MaDm = 0, TenDm = "-- Tất cả danh mục --" });
            cboLocDanhMuc.DataSource = loc;
            cboLocDanhMuc.DisplayMember = "TenDm";
            cboLocDanhMuc.ValueMember = "MaDm";
            cboLocDanhMuc.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi nạp danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _isBinding = false;
        }
    }

    private async Task LoadDataAsync()
    {
        try
        {
            _isBinding = true;
            // Include để join bảng DanhMucMon lấy tên danh mục
            var query = _context.MonUongs.AsNoTracking()
                .Include(x => x.MaDmNavigation)
                .AsQueryable();

            var tuKhoa = txtTimKiem.Text.Trim();
            if (!string.IsNullOrEmpty(tuKhoa))
                query = query.Where(x => x.TenMon.Contains(tuKhoa));

            if (cboLocDanhMuc.SelectedValue is int maDm && maDm > 0)
                query = query.Where(x => x.MaDm == maDm);

            var ds = await query.OrderBy(x => x.MaMon)
                .Select(x => new
                {
                    x.MaMon,
                    x.TenMon,
                    TenDanhMuc = x.MaDmNavigation.TenDm,
                    x.GiaBan,
                    x.DangBan
                })
                .ToListAsync();

            dgvMon.DataSource = ds;
            dgvMon.Columns["MaMon"]!.Visible = false;
            dgvMon.Columns["TenMon"]!.HeaderText = "Tên món";
            dgvMon.Columns["TenDanhMuc"]!.HeaderText = "Danh mục";
            dgvMon.Columns["GiaBan"]!.HeaderText = "Giá bán";
            dgvMon.Columns["GiaBan"]!.DefaultCellStyle.Format = "N0";
            dgvMon.Columns["DangBan"]!.HeaderText = "Đang bán";
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi nạp thực đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _isBinding = false;
        }
    }

    private int? MaMonDangChon()
    {
        if (dgvMon.CurrentRow?.Cells["MaMon"].Value is int ma) return ma;
        return null;
    }

    private async void dgvMon_SelectionChanged(object sender, EventArgs e)
    {
        if (_isBinding) return;
        var ma = MaMonDangChon();
        if (ma == null) return;
        var mon = await _context.MonUongs.AsNoTracking().FirstOrDefaultAsync(x => x.MaMon == ma.Value);
        if (mon == null) return;
        txtTenMon.Text = mon.TenMon;
        cboDanhMuc.SelectedValue = mon.MaDm;
        nudGiaBan.Value = Math.Min(nudGiaBan.Maximum, mon.GiaBan);
        chkDangBan.Checked = mon.DangBan;
    }

    private bool KiemTraNhap()
    {
        if (string.IsNullOrWhiteSpace(txtTenMon.Text))
        {
            MessageBox.Show("Vui lòng nhập tên món.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtTenMon.Focus();
            return false;
        }
        if (cboDanhMuc.SelectedValue == null)
        {
            MessageBox.Show("Vui lòng chọn danh mục.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        return true;
    }

    private async void btnThem_Click(object sender, EventArgs e)
    {
        if (!KiemTraNhap()) return;
        try
        {
            _context.MonUongs.Add(new MonUong
            {
                TenMon = txtTenMon.Text.Trim(),
                MaDm = (int)cboDanhMuc.SelectedValue!,
                GiaBan = nudGiaBan.Value,
                DangBan = chkDangBan.Checked
            });
            await _context.SaveChangesAsync();
            await LoadDataAsync();
            MessageBox.Show("Đã thêm món.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi thêm món: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnSua_Click(object sender, EventArgs e)
    {
        var ma = MaMonDangChon();
        if (ma == null)
        {
            MessageBox.Show("Vui lòng chọn món cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (!KiemTraNhap()) return;
        try
        {
            var mon = await _context.MonUongs.FirstOrDefaultAsync(x => x.MaMon == ma.Value);
            if (mon == null) return;
            mon.TenMon = txtTenMon.Text.Trim();
            mon.MaDm = (int)cboDanhMuc.SelectedValue!;
            mon.GiaBan = nudGiaBan.Value;
            mon.DangBan = chkDangBan.Checked;
            await _context.SaveChangesAsync();
            await LoadDataAsync();
            MessageBox.Show("Đã cập nhật món.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi sửa món: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnXoa_Click(object sender, EventArgs e)
    {
        var ma = MaMonDangChon();
        if (ma == null)
        {
            MessageBox.Show("Vui lòng chọn món cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (MessageBox.Show("Bạn có chắc muốn xóa món này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;
        try
        {
            var mon = await _context.MonUongs.FirstOrDefaultAsync(x => x.MaMon == ma.Value);
            if (mon == null) return;
            _context.MonUongs.Remove(mon);
            await _context.SaveChangesAsync();
            await LoadDataAsync();
        }
        catch (DbUpdateException)
        {
            // Món đã có trong hóa đơn (ràng buộc khóa ngoại) -> không xóa được
            _context.ChangeTracker.Clear();
            MessageBox.Show("Món này đã có trong hóa đơn nên không thể xóa. Hãy bỏ chọn \"Đang bán\" thay vì xóa.",
                "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi xóa món: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void txtTimKiem_TextChanged(object sender, EventArgs e)
    {
        if (_isBinding) return;
        await LoadDataAsync();
    }

    private async void cboLocDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_isBinding) return;
        await LoadDataAsync();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _context.Dispose();
        base.OnFormClosed(e);
    }
}
