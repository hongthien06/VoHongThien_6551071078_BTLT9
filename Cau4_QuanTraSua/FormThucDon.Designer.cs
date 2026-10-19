namespace Cau4_QuanTraSua;

partial class FormThucDon
{
    private System.ComponentModel.IContainer components = null;
    private Label lblTenMon, lblDanhMuc, lblGiaBan, lblTimKiem, lblLoc;
    private TextBox txtTenMon, txtTimKiem;
    private ComboBox cboDanhMuc, cboLocDanhMuc;
    private NumericUpDown nudGiaBan;
    private CheckBox chkDangBan;
    private Button btnThem, btnSua, btnXoa;
    private DataGridView dgvMon;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblTenMon = new Label(); lblDanhMuc = new Label(); lblGiaBan = new Label();
        lblTimKiem = new Label(); lblLoc = new Label();
        txtTenMon = new TextBox(); txtTimKiem = new TextBox();
        cboDanhMuc = new ComboBox(); cboLocDanhMuc = new ComboBox();
        nudGiaBan = new NumericUpDown(); chkDangBan = new CheckBox();
        btnThem = new Button(); btnSua = new Button(); btnXoa = new Button();
        dgvMon = new DataGridView();
        ((System.ComponentModel.ISupportInitialize)nudGiaBan).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvMon).BeginInit();
        SuspendLayout();

        lblTenMon.AutoSize = true; lblTenMon.Location = new Point(20, 23); lblTenMon.Text = "Tên món:";
        txtTenMon.Location = new Point(100, 20); txtTenMon.Size = new Size(230, 23); txtTenMon.Name = "txtTenMon";

        lblDanhMuc.AutoSize = true; lblDanhMuc.Location = new Point(20, 58); lblDanhMuc.Text = "Danh mục:";
        cboDanhMuc.DropDownStyle = ComboBoxStyle.DropDownList; cboDanhMuc.Location = new Point(100, 55);
        cboDanhMuc.Size = new Size(230, 23); cboDanhMuc.Name = "cboDanhMuc";

        lblGiaBan.AutoSize = true; lblGiaBan.Location = new Point(20, 93); lblGiaBan.Text = "Giá bán:";
        nudGiaBan.Location = new Point(100, 90); nudGiaBan.Size = new Size(120, 23); nudGiaBan.Name = "nudGiaBan";
        nudGiaBan.Maximum = 10000000; nudGiaBan.Increment = 1000; nudGiaBan.ThousandsSeparator = true;

        chkDangBan.AutoSize = true; chkDangBan.Checked = true; chkDangBan.Location = new Point(100, 125);
        chkDangBan.Name = "chkDangBan"; chkDangBan.Text = "Đang bán";

        btnThem.Location = new Point(20, 160); btnThem.Size = new Size(90, 32); btnThem.Name = "btnThem"; btnThem.Text = "Thêm"; btnThem.Click += btnThem_Click;
        btnSua.Location = new Point(120, 160); btnSua.Size = new Size(90, 32); btnSua.Name = "btnSua"; btnSua.Text = "Sửa"; btnSua.Click += btnSua_Click;
        btnXoa.Location = new Point(220, 160); btnXoa.Size = new Size(90, 32); btnXoa.Name = "btnXoa"; btnXoa.Text = "Xóa"; btnXoa.Click += btnXoa_Click;

        lblTimKiem.AutoSize = true; lblTimKiem.Location = new Point(360, 23); lblTimKiem.Text = "Tìm theo tên món:";
        txtTimKiem.Location = new Point(480, 20); txtTimKiem.Size = new Size(200, 23); txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.TextChanged += txtTimKiem_TextChanged;

        lblLoc.AutoSize = true; lblLoc.Location = new Point(360, 58); lblLoc.Text = "Lọc theo danh mục:";
        cboLocDanhMuc.DropDownStyle = ComboBoxStyle.DropDownList; cboLocDanhMuc.Location = new Point(480, 55);
        cboLocDanhMuc.Size = new Size(200, 23); cboLocDanhMuc.Name = "cboLocDanhMuc";
        cboLocDanhMuc.SelectedIndexChanged += cboLocDanhMuc_SelectedIndexChanged;

        dgvMon.AllowUserToAddRows = false; dgvMon.AllowUserToDeleteRows = false; dgvMon.ReadOnly = true;
        dgvMon.MultiSelect = false; dgvMon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvMon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; dgvMon.RowHeadersVisible = false;
        dgvMon.Location = new Point(20, 210); dgvMon.Size = new Size(660, 280); dgvMon.Name = "dgvMon";
        dgvMon.SelectionChanged += dgvMon_SelectionChanged;

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(704, 510);
        Controls.AddRange(new Control[] { lblTenMon, txtTenMon, lblDanhMuc, cboDanhMuc, lblGiaBan, nudGiaBan, chkDangBan,
            btnThem, btnSua, btnXoa, lblTimKiem, txtTimKiem, lblLoc, cboLocDanhMuc, dgvMon });
        Name = "FormThucDon";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Thực đơn - Quán trà sữa";
        Load += FormThucDon_Load;
        ((System.ComponentModel.ISupportInitialize)nudGiaBan).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvMon).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
