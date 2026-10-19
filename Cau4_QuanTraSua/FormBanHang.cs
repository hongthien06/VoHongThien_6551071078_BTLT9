using System.ComponentModel;
using Cau4_QuanTraSua.Config;
using Cau4_QuanTraSua.Models;
using Cau4_QuanTraSua.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Cau4_QuanTraSua;

/// <summary>Lập đơn: chọn món -> giỏ hàng -> thanh toán tiền mặt hoặc chuyển khoản VietQR.</summary>
public partial class FormBanHang : Form
{
    private readonly QlTraSuaContext _context = new();
    private readonly BanHangService _banHang;
    private readonly VietQrService _vietQr;
    private readonly GioHang _gio = new();

    // Mã hóa đơn dự kiến dùng trong nội dung chuyển khoản của mã QR đang hiển thị
    private int _maHdQr;

    public FormBanHang()
    {
        InitializeComponent();
        _banHang = new BanHangService(_context);
        _vietQr = new VietQrService(AppConfig.LoadPayment());
        Text = "Bán hàng - " + _vietQr.Settings.ShopName;
    }

    private async void FormBanHang_Load(object sender, EventArgs e)
    {
        dgvGioHang.DataSource = _gio.Dong;
        dgvGioHang.Columns["TenMon"]!.HeaderText = "Tên món";
        dgvGioHang.Columns["SoLy"]!.HeaderText = "SL";
        dgvGioHang.Columns["DonGia"]!.HeaderText = "Đơn giá";
        dgvGioHang.Columns["DonGia"]!.DefaultCellStyle.Format = "N0";
        dgvGioHang.Columns["ThanhTien"]!.HeaderText = "Thành tiền";
        dgvGioHang.Columns["ThanhTien"]!.DefaultCellStyle.Format = "N0";
        dgvGioHang.Columns["TenMon"]!.FillWeight = 200;
        dgvGioHang.Columns["SoLy"]!.FillWeight = 50;
        dgvGioHang.Columns["DonGia"]!.FillWeight = 90;
        dgvGioHang.Columns["ThanhTien"]!.FillWeight = 100;
        _gio.Dong.ListChanged += Gio_ListChanged;

        CapNhatTongTien();
        ResetThanhToan();
        await LoadDanhMucAsync();
    }

    private async Task LoadDanhMucAsync()
    {
        try
        {
            var ds = await _context.DanhMucMons.AsNoTracking()
                .OrderBy(x => x.MaDm)
                .Select(x => new { x.MaDm, x.TenDm })
                .ToListAsync();
            cboDanhMuc.DataSource = ds;
            cboDanhMuc.DisplayMember = "TenDm";
            cboDanhMuc.ValueMember = "MaDm";
            await LoadMonAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi nạp danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async Task LoadMonAsync()
    {
        if (cboDanhMuc.SelectedValue is not int maDm) return;
        try
        {
            // Context riêng vì SelectedIndexChanged có thể kích hoạt song song với lần nạp đầu
            using var ctx = new QlTraSuaContext();
            var ds = await ctx.MonUongs.AsNoTracking()
                .Where(x => x.MaDm == maDm && x.DangBan)
                .OrderBy(x => x.MaMon)
                .ToListAsync();
            lstMon.DataSource = ds.Select(x => new MonHienThi(x.MaMon, x.TenMon, x.GiaBan)).ToList();
            lstMon.DisplayMember = nameof(MonHienThi.Hien);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi nạp món: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void cboDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
    {
        await LoadMonAsync();
    }

    private void btnThemVaoDon_Click(object sender, EventArgs e)
    {
        if (lstMon.SelectedItem is not MonHienThi mon)
        {
            MessageBox.Show("Vui lòng chọn món.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        // Cộng dồn số lượng nếu món đã có trong giỏ
        _gio.ThemMon(mon.MaMon, mon.TenMon, mon.GiaBan, (int)nudSoLy.Value);
    }

    private void btnXoaDong_Click(object sender, EventArgs e)
    {
        if (dgvGioHang.CurrentRow == null) return;
        _gio.XoaDong(dgvGioHang.CurrentRow.Index);
    }

    private void Gio_ListChanged(object? sender, ListChangedEventArgs e)
    {
        CapNhatTongTien();
        // Giỏ hàng đổi thì mã QR cũ không còn đúng số tiền
        ResetThanhToan();
    }

    private void CapNhatTongTien()
    {
        lblTongTien.Text = "Tổng tiền: " + Tien.Dinh(_gio.TongTien);
    }

    private void ResetThanhToan()
    {
        _maHdQr = 0;
        picQR.Image?.Dispose();
        picQR.Image = null;
        lblTaiKhoan.Text = "";
        lblSoTien.Text = "Số tiền:";
        lblNoiDung.Text = "Nội dung:";
        btnDaNhanTien.Enabled = false;
    }

    private async void btnChuyenKhoan_Click(object sender, EventArgs e)
    {
        if (_gio.Dong.Count == 0)
        {
            MessageBox.Show("Đơn hàng đang trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        btnChuyenKhoan.Enabled = false;
        try
        {
            var tong = _gio.TongTien;
            _maHdQr = await _banHang.LayMaHdTiepTheoAsync();
            var noiDung = VietQrService.TaoNoiDung(_maHdQr);

            // Luôn hiện số tiền, nội dung, số tài khoản dạng chữ (để khách chuyển tay nếu không tải được QR)
            var s = _vietQr.Settings;
            lblTaiKhoan.Text = $"Chủ TK: {s.AccountName}\nNgân hàng: {s.BankId}\nSTK: {s.AccountNo}";
            lblSoTien.Text = "Số tiền: " + Tien.Dinh(tong);
            lblNoiDung.Text = "Nội dung: " + noiDung;

            var bytes = await _vietQr.TaiAnhQrAsync(_maHdQr, tong);
            if (bytes != null)
            {
                using var ms = new MemoryStream(bytes);
                using var tam = Image.FromStream(ms);
                picQR.Image?.Dispose();
                picQR.Image = new Bitmap(tam);
            }
            else
            {
                picQR.Image = null;
                MessageBox.Show("Không tải được mã QR (có thể mất mạng). Khách có thể chuyển khoản thủ công theo số tài khoản " +
                                "và nội dung hiển thị, hoặc chọn thanh toán Tiền mặt.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // Vẫn cho phép xác nhận đã nhận tiền khi khách chuyển tay
            btnDaNhanTien.Enabled = true;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Lỗi khi tạo thanh toán chuyển khoản");
            MessageBox.Show("Lỗi tạo thanh toán: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnChuyenKhoan.Enabled = true;
        }
    }

    private async void btnDaNhanTien_Click(object sender, EventArgs e)
    {
        await LuuHoaDonAsync("Chuyển khoản");
    }

    private async void btnTienMat_Click(object sender, EventArgs e)
    {
        await LuuHoaDonAsync("Tiền mặt");
    }

    private async Task LuuHoaDonAsync(string hinhThuc)
    {
        if (_gio.Dong.Count == 0)
        {
            MessageBox.Show("Đơn hàng đang trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        try
        {
            var maHdQr = _maHdQr;
            // HoaDon + ChiTietHoaDon lưu trong cùng một lần SaveChanges
            var hd = await _banHang.LuuHoaDonAsync(_gio, hinhThuc);
            Log.Information("Đã lưu hóa đơn: MaHD={MaHD}, SoTien={SoTien}, HinhThuc={HinhThuc}", hd.MaHd, hd.TongTien, hinhThuc);

            var msg = $"Đã lưu hóa đơn HD{hd.MaHd} ({hinhThuc}) - {Tien.Dinh(hd.TongTien)}.";
            if (hinhThuc == "Chuyển khoản" && maHdQr != 0 && maHdQr != hd.MaHd)
            {
                Log.Warning("Mã hóa đơn lưu ({MaHD}) khác mã dự kiến trong nội dung QR ({MaHDQR})", hd.MaHd, maHdQr);
                msg += $"\nLưu ý: nội dung chuyển khoản của khách là HD{maHdQr}, khác mã hóa đơn thực tế.";
            }
            _gio.Xoa();
            MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Lỗi lưu hóa đơn");
            MessageBox.Show("Lỗi lưu hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _context.Dispose();
        base.OnFormClosed(e);
    }

    /// <summary>Món hiển thị trong ListBox: "Tên - 35.000".</summary>
    private record MonHienThi(int MaMon, string TenMon, decimal GiaBan)
    {
        public string Hien => $"{TenMon} - {GiaBan.ToString("N0", new System.Globalization.CultureInfo("vi-VN"))}";
    }
}
