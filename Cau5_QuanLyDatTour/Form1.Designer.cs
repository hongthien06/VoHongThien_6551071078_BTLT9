namespace Cau5_QuanLyDatTour;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        tabMain = new TabControl();
        tabTour = new TabPage();
        tabKhach = new TabPage();
        tabDat = new TabPage();

        // ===== Tab Tour =====
        lblTenTour = new Label { Text = "Tên tour:", Location = new Point(12, 15), AutoSize = true };
        txtTenTour = new TextBox { Location = new Point(90, 12), Width = 220 };
        lblDiemDen = new Label { Text = "Điểm đến:", Location = new Point(330, 15), AutoSize = true };
        txtDiemDen = new TextBox { Location = new Point(410, 12), Width = 160 };
        lblNgoaiTe = new Label { Text = "Ngoại tệ:", Location = new Point(12, 47), AutoSize = true };
        cboNgoaiTe = new ComboBox { Location = new Point(90, 44), Width = 90, DropDownStyle = ComboBoxStyle.DropDownList };
        lblGiaNgoaiTe = new Label { Text = "Giá:", Location = new Point(200, 47), AutoSize = true };
        nudGiaNgoaiTe = new NumericUpDown { Location = new Point(235, 44), Width = 110, Maximum = 1000000000, DecimalPlaces = 2, ThousandsSeparator = true };
        lblKhoiHanh = new Label { Text = "Khởi hành:", Location = new Point(365, 47), AutoSize = true };
        dtpKhoiHanh = new DateTimePicker { Location = new Point(440, 44), Width = 120, Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy" };
        lblSoCho = new Label { Text = "Số chỗ:", Location = new Point(580, 47), AutoSize = true };
        nudSoCho = new NumericUpDown { Location = new Point(635, 44), Width = 60, Minimum = 1, Maximum = 1000, Value = 10 };
        btnThemTour = new Button { Text = "Thêm", Location = new Point(12, 80), Width = 75, Height = 28 };
        btnSuaTour = new Button { Text = "Sửa", Location = new Point(95, 80), Width = 75, Height = 28 };
        btnXoaTour = new Button { Text = "Xóa", Location = new Point(178, 80), Width = 75, Height = 28 };
        txtTimTour = new TextBox { Location = new Point(300, 82), Width = 280, PlaceholderText = "Tìm theo tên hoặc điểm đến" };
        btnTimTour = new Button { Text = "Tìm", Location = new Point(590, 80), Width = 75, Height = 28 };
        dgvTour = new DataGridView
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AutoGenerateColumns = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            RowHeadersVisible = false,
            BackgroundColor = SystemColors.Window
        };
        dgvTour.Columns.AddRange(
            Col("MaTour", "Mã", 50),
            Col("TenTour", "Tên tour", 200),
            Col("DiemDen", "Điểm đến", 90),
            Col("NgoaiTe", "Ngoại tệ", 60),
            Col("GiaNgoaiTe", "Giá", 90, "#,##0.##"),
            Col("NgayKhoiHanh", "Khởi hành", 85),
            Col("SoChoToiDa", "Số chỗ", 60));
        tabTour.Text = "Tour";
        pnlTopTour = new Panel { Dock = DockStyle.Top, Height = 120 };
        pnlTopTour.Controls.AddRange(new Control[] {
            lblTenTour, txtTenTour, lblDiemDen, txtDiemDen, lblNgoaiTe, cboNgoaiTe, lblGiaNgoaiTe, nudGiaNgoaiTe,
            lblKhoiHanh, dtpKhoiHanh, lblSoCho, nudSoCho, btnThemTour, btnSuaTour, btnXoaTour, txtTimTour, btnTimTour });
        tabTour.Controls.Add(dgvTour);
        tabTour.Controls.Add(pnlTopTour);

        // ===== Tab Khách hàng =====
        lblHoTen = new Label { Text = "Họ tên:", Location = new Point(12, 15), AutoSize = true };
        txtHoTen = new TextBox { Location = new Point(90, 12), Width = 220 };
        lblSdt = new Label { Text = "Số ĐT:", Location = new Point(330, 15), AutoSize = true };
        txtSdt = new TextBox { Location = new Point(390, 12), Width = 140, MaxLength = 15 };
        lblEmail = new Label { Text = "Email:", Location = new Point(12, 47), AutoSize = true };
        txtEmail = new TextBox { Location = new Point(90, 44), Width = 220 };
        btnThemKH = new Button { Text = "Thêm", Location = new Point(12, 80), Width = 75, Height = 28 };
        btnSuaKH = new Button { Text = "Sửa", Location = new Point(95, 80), Width = 75, Height = 28 };
        btnXoaKH = new Button { Text = "Xóa", Location = new Point(178, 80), Width = 75, Height = 28 };
        txtTimKH = new TextBox { Location = new Point(300, 82), Width = 280, PlaceholderText = "Tìm theo tên hoặc SĐT" };
        btnTimKH = new Button { Text = "Tìm", Location = new Point(590, 80), Width = 75, Height = 28 };
        dgvKhach = new DataGridView
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AutoGenerateColumns = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            RowHeadersVisible = false,
            BackgroundColor = SystemColors.Window
        };
        dgvKhach.Columns.AddRange(
            Col("MaKh", "Mã", 50),
            Col("HoTen", "Họ tên", 220),
            Col("SoDienThoai", "Số ĐT", 120),
            Col("Email", "Email", 240));
        tabKhach.Text = "Khách hàng";
        pnlTopKhach = new Panel { Dock = DockStyle.Top, Height = 120 };
        pnlTopKhach.Controls.AddRange(new Control[] {
            lblHoTen, txtHoTen, lblSdt, txtSdt, lblEmail, txtEmail, btnThemKH, btnSuaKH, btnXoaKH, txtTimKH, btnTimKH });
        tabKhach.Controls.Add(dgvKhach);
        tabKhach.Controls.Add(pnlTopKhach);

        // ===== Tab Đặt tour =====
        grpDat = new GroupBox { Text = "Thông tin đặt tour", Location = new Point(8, 8), Size = new Size(620, 238) };
        lblKhachHang = new Label { Text = "Khách hàng:", Location = new Point(15, 27), AutoSize = true };
        cboKhachHang = new ComboBox { Location = new Point(135, 24), Width = 280, DropDownStyle = ComboBoxStyle.DropDownList };
        lblTour = new Label { Text = "Tour:", Location = new Point(15, 57), AutoSize = true };
        cboTour = new ComboBox { Location = new Point(135, 54), Width = 280, DropDownStyle = ComboBoxStyle.DropDownList };
        lblSoNguoi = new Label { Text = "Số người:", Location = new Point(15, 87), AutoSize = true };
        nudSoNguoi = new NumericUpDown { Location = new Point(135, 84), Width = 60, Minimum = 1, Maximum = 1, Value = 1 };
        lblGiaTitle = new Label { Text = "Giá/người:", Location = new Point(15, 115), AutoSize = true };
        lblGiaNguoi = new Label { Text = "", Location = new Point(135, 115), AutoSize = true };
        lblTyGiaTitle = new Label { Text = "Tỷ giá hôm nay:", Location = new Point(15, 140), AutoSize = true };
        lblTyGia = new Label { Text = "", Location = new Point(135, 140), AutoSize = true };
        lblNguonTyGia = new Label
        {
            Text = "",
            Location = new Point(335, 140),
            AutoSize = true,
            BackColor = Color.Gainsboro,
            ForeColor = Color.DimGray,
            Font = new Font("Segoe UI", 7.5F)
        };
        lblTongTienTitle = new Label { Text = "Tổng tiền:", Location = new Point(15, 163), AutoSize = true, Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.DarkRed };
        lblTongTien = new Label { Text = "", Location = new Point(135, 163), AutoSize = true, Font = new Font("Segoe UI", 11F, FontStyle.Bold), ForeColor = Color.DarkRed };
        btnDatTour = new Button { Text = "Đặt tour", Location = new Point(135, 200), Width = 90, Height = 28, BackColor = Color.ForestGreen, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        grpDat.Controls.AddRange(new Control[] {
            lblKhachHang, cboKhachHang, lblTour, cboTour, lblSoNguoi, nudSoNguoi, lblGiaTitle, lblGiaNguoi,
            lblTyGiaTitle, lblTyGia, lblNguonTyGia, lblTongTienTitle, lblTongTien, btnDatTour });

        txtTimDat = new TextBox { Location = new Point(12, 255), Width = 220, PlaceholderText = "Tìm theo tên khách hoặc tên tour" };
        lblThang = new Label { Text = "Tháng khởi hành", Location = new Point(245, 258), AutoSize = true };
        cboThang = new ComboBox { Location = new Point(375, 255), Width = 100, DropDownStyle = ComboBoxStyle.DropDownList };
        btnTimDat = new Button { Text = "Tìm", Location = new Point(490, 253), Width = 75, Height = 28 };
        dgvDatTour = new DataGridView
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AutoGenerateColumns = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            RowHeadersVisible = false,
            BackgroundColor = SystemColors.Window
        };
        dgvDatTour.Columns.AddRange(
            Col("Ma", "Mã", 50),
            Col("KhachHang", "Khách hàng", 120),
            Col("Tour", "Tour", 190),
            Col("SoNguoi", "Số người", 75),
            Col("TyGia", "Tỷ giá", 80, "#,##0.####"),
            Col("TongTienVnd", "Tổng tiền (VND)", 105, "#,##0 \"đ\""),
            Col("NgayDat", "Ngày đặt", 140, "dd/MM/yyyy HH:mm"));
        lblTongDoanhThu = new Label
        {
            Text = "Tổng doanh thu: 0 đ",
            AutoSize = true,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            Location = new Point(560, 6)
        };
        pnlTopDat = new Panel { Dock = DockStyle.Top, Height = 285 };
        pnlTopDat.Controls.AddRange(new Control[] { grpDat, txtTimDat, lblThang, cboThang, btnTimDat });
        pnlBottomDat = new Panel { Dock = DockStyle.Bottom, Height = 30 };
        pnlBottomDat.Controls.Add(lblTongDoanhThu);
        lblTongDoanhThu.Dock = DockStyle.Right;
        lblTongDoanhThu.Padding = new Padding(0, 6, 12, 0);
        tabDat.Text = "Đặt tour";
        tabDat.Controls.Add(dgvDatTour);
        tabDat.Controls.Add(pnlBottomDat);
        tabDat.Controls.Add(pnlTopDat);

        // ===== Form =====
        tabMain.Dock = DockStyle.Fill;
        tabMain.Controls.AddRange(new Control[] { tabTour, tabKhach, tabDat });

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(880, 540);
        Controls.Add(tabMain);
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Quản lý đặt tour";
        Load += Form1_Load;
    }

    private static DataGridViewTextBoxColumn Col(string prop, string header, int width, string format = "")
    {
        var c = new DataGridViewTextBoxColumn { DataPropertyName = prop, HeaderText = header, Width = width, FillWeight = width, Name = "col" + prop };
        if (format != "") c.DefaultCellStyle.Format = format;
        return c;
    }

    #endregion

    private TabControl tabMain;
    private TabPage tabTour, tabKhach, tabDat;
    private Panel pnlTopTour, pnlTopKhach, pnlTopDat, pnlBottomDat;

    // Tab Tour
    private Label lblTenTour, lblDiemDen, lblNgoaiTe, lblGiaNgoaiTe, lblKhoiHanh, lblSoCho;
    private TextBox txtTenTour, txtDiemDen, txtTimTour;
    private ComboBox cboNgoaiTe;
    private NumericUpDown nudGiaNgoaiTe, nudSoCho;
    private DateTimePicker dtpKhoiHanh;
    private Button btnThemTour, btnSuaTour, btnXoaTour, btnTimTour;
    private DataGridView dgvTour;

    // Tab Khách hàng
    private Label lblHoTen, lblSdt, lblEmail;
    private TextBox txtHoTen, txtSdt, txtEmail, txtTimKH;
    private Button btnThemKH, btnSuaKH, btnXoaKH, btnTimKH;
    private DataGridView dgvKhach;

    // Tab Đặt tour
    private GroupBox grpDat;
    private Label lblKhachHang, lblTour, lblSoNguoi, lblGiaTitle, lblGiaNguoi, lblTyGiaTitle, lblTyGia, lblNguonTyGia, lblTongTienTitle, lblTongTien;
    private ComboBox cboKhachHang, cboTour, cboThang;
    private NumericUpDown nudSoNguoi;
    private Button btnDatTour, btnTimDat;
    private TextBox txtTimDat;
    private Label lblThang, lblTongDoanhThu;
    private DataGridView dgvDatTour;
}
