namespace Cau4_QuanTraSua;

partial class FormBanHang
{
    private System.ComponentModel.IContainer components = null;
    private GroupBox grpChonMon, grpDon, grpThanhToan;
    private Label lblDanhMuc, lblSoLy, lblTongTien, lblTaiKhoan, lblSoTien, lblNoiDung;
    private ComboBox cboDanhMuc;
    private ListBox lstMon;
    private NumericUpDown nudSoLy;
    private Button btnThemVaoDon, btnXoaDong, btnChuyenKhoan, btnTienMat, btnDaNhanTien;
    private DataGridView dgvGioHang;
    private PictureBox picQR;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        grpChonMon = new GroupBox(); grpDon = new GroupBox(); grpThanhToan = new GroupBox();
        lblDanhMuc = new Label(); lblSoLy = new Label(); lblTongTien = new Label();
        lblTaiKhoan = new Label(); lblSoTien = new Label(); lblNoiDung = new Label();
        cboDanhMuc = new ComboBox(); lstMon = new ListBox(); nudSoLy = new NumericUpDown();
        btnThemVaoDon = new Button(); btnXoaDong = new Button(); btnChuyenKhoan = new Button();
        btnTienMat = new Button(); btnDaNhanTien = new Button();
        dgvGioHang = new DataGridView(); picQR = new PictureBox();
        ((System.ComponentModel.ISupportInitialize)nudSoLy).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvGioHang).BeginInit();
        ((System.ComponentModel.ISupportInitialize)picQR).BeginInit();
        grpChonMon.SuspendLayout(); grpDon.SuspendLayout(); grpThanhToan.SuspendLayout();
        SuspendLayout();

        // grpChonMon
        grpChonMon.Text = "Chọn món"; grpChonMon.Location = new Point(12, 10); grpChonMon.Size = new Size(240, 430);
        grpChonMon.Controls.AddRange(new Control[] { lblDanhMuc, cboDanhMuc, lstMon, lblSoLy, nudSoLy, btnThemVaoDon });
        lblDanhMuc.AutoSize = true; lblDanhMuc.Location = new Point(10, 25); lblDanhMuc.Text = "Danh mục:";
        cboDanhMuc.DropDownStyle = ComboBoxStyle.DropDownList; cboDanhMuc.Location = new Point(10, 45);
        cboDanhMuc.Size = new Size(220, 23); cboDanhMuc.Name = "cboDanhMuc";
        cboDanhMuc.SelectedIndexChanged += cboDanhMuc_SelectedIndexChanged;
        lstMon.Location = new Point(10, 85); lstMon.Size = new Size(220, 244); lstMon.Name = "lstMon";
        lblSoLy.AutoSize = true; lblSoLy.Location = new Point(10, 350); lblSoLy.Text = "Số ly:";
        nudSoLy.Location = new Point(70, 347); nudSoLy.Size = new Size(70, 23); nudSoLy.Name = "nudSoLy";
        nudSoLy.Minimum = 1; nudSoLy.Maximum = 99; nudSoLy.Value = 1;
        btnThemVaoDon.Location = new Point(10, 385); btnThemVaoDon.Size = new Size(220, 30);
        btnThemVaoDon.Name = "btnThemVaoDon"; btnThemVaoDon.Text = "Thêm vào đơn"; btnThemVaoDon.Click += btnThemVaoDon_Click;

        // grpDon
        grpDon.Text = "Đơn hiện tại"; grpDon.Location = new Point(262, 10); grpDon.Size = new Size(320, 430);
        grpDon.Controls.AddRange(new Control[] { dgvGioHang, btnXoaDong, lblTongTien });
        dgvGioHang.AllowUserToAddRows = false; dgvGioHang.AllowUserToDeleteRows = false; dgvGioHang.ReadOnly = true;
        dgvGioHang.MultiSelect = false; dgvGioHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvGioHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; dgvGioHang.RowHeadersVisible = false;
        dgvGioHang.Location = new Point(10, 25); dgvGioHang.Size = new Size(300, 304); dgvGioHang.Name = "dgvGioHang";
        btnXoaDong.Location = new Point(10, 340); btnXoaDong.Size = new Size(300, 30);
        btnXoaDong.Name = "btnXoaDong"; btnXoaDong.Text = "Xóa dòng"; btnXoaDong.Click += btnXoaDong_Click;
        lblTongTien.AutoSize = true; lblTongTien.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblTongTien.Location = new Point(60, 388); lblTongTien.Name = "lblTongTien"; lblTongTien.Text = "Tổng tiền: 0 đ";

        // grpThanhToan
        grpThanhToan.Text = "Thanh toán"; grpThanhToan.Location = new Point(592, 10); grpThanhToan.Size = new Size(260, 430);
        grpThanhToan.Controls.AddRange(new Control[] { btnChuyenKhoan, btnTienMat, picQR, lblTaiKhoan, lblSoTien, lblNoiDung, btnDaNhanTien });
        btnChuyenKhoan.Location = new Point(10, 25); btnChuyenKhoan.Size = new Size(240, 30);
        btnChuyenKhoan.Name = "btnChuyenKhoan"; btnChuyenKhoan.Text = "Thanh toán chuyển khoản"; btnChuyenKhoan.Click += btnChuyenKhoan_Click;
        btnTienMat.Location = new Point(10, 62); btnTienMat.Size = new Size(240, 30);
        btnTienMat.Name = "btnTienMat"; btnTienMat.Text = "Tiền mặt"; btnTienMat.Click += btnTienMat_Click;
        picQR.Location = new Point(30, 100); picQR.Size = new Size(200, 200); picQR.Name = "picQR";
        picQR.SizeMode = PictureBoxSizeMode.Zoom; picQR.BorderStyle = BorderStyle.FixedSingle;
        lblTaiKhoan.Location = new Point(10, 305); lblTaiKhoan.Size = new Size(240, 48); lblTaiKhoan.Name = "lblTaiKhoan";
        lblSoTien.AutoSize = true; lblSoTien.Location = new Point(10, 357); lblSoTien.Name = "lblSoTien"; lblSoTien.Text = "Số tiền:";
        lblNoiDung.AutoSize = true; lblNoiDung.Location = new Point(10, 377); lblNoiDung.Name = "lblNoiDung"; lblNoiDung.Text = "Nội dung:";
        btnDaNhanTien.BackColor = Color.SeaGreen; btnDaNhanTien.ForeColor = Color.White; btnDaNhanTien.FlatStyle = FlatStyle.Flat;
        btnDaNhanTien.Location = new Point(10, 397); btnDaNhanTien.Size = new Size(240, 28);
        btnDaNhanTien.Name = "btnDaNhanTien"; btnDaNhanTien.Text = "Đã nhận tiền"; btnDaNhanTien.Click += btnDaNhanTien_Click;

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(864, 452);
        Controls.Add(grpChonMon); Controls.Add(grpDon); Controls.Add(grpThanhToan);
        Name = "FormBanHang";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Bán hàng - Quán trà sữa";
        Load += FormBanHang_Load;
        ((System.ComponentModel.ISupportInitialize)nudSoLy).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvGioHang).EndInit();
        ((System.ComponentModel.ISupportInitialize)picQR).EndInit();
        grpChonMon.ResumeLayout(false); grpChonMon.PerformLayout();
        grpDon.ResumeLayout(false); grpDon.PerformLayout();
        grpThanhToan.ResumeLayout(false); grpThanhToan.PerformLayout();
        ResumeLayout(false);
    }
}
