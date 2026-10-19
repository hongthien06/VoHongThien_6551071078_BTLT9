namespace Cau2_QuanLyNhaTro;

partial class FormKhachThue
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
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
        grpThongTin = new GroupBox();
        lblHoTen = new Label();
        txtHoTen = new TextBox();
        rdoNam = new RadioButton();
        rdoNu = new RadioButton();
        lblNgaySinh = new Label();
        dtpNgaySinh = new DateTimePicker();
        lblSoDienThoai = new Label();
        txtSoDienThoai = new TextBox();
        lblPhong = new Label();
        cboPhong = new ComboBox();
        lblTinh = new Label();
        cboTinh = new ComboBox();
        lblPhuongXa = new Label();
        cboPhuongXa = new ComboBox();
        chkTamTru = new CheckBox();
        btnThem = new Button();
        btnSua = new Button();
        btnXoa = new Button();
        btnLamMoi = new Button();
        btnGayLoi = new Button();
        txtTimKiem = new TextBox();
        lblLocPhong = new Label();
        cboLocPhong = new ComboBox();
        lblLocTinh = new Label();
        cboLocTinh = new ComboBox();
        btnTim = new Button();
        btnBoLoc = new Button();
        dgvKhach = new DataGridView();
        colHoTen = new DataGridViewTextBoxColumn();
        colSoDienThoai = new DataGridViewTextBoxColumn();
        colPhong = new DataGridViewTextBoxColumn();
        colGiaThue = new DataGridViewTextBoxColumn();
        colQueQuan = new DataGridViewTextBoxColumn();
        colTamTru = new DataGridViewCheckBoxColumn();
        statusStrip1 = new StatusStrip();
        lblTrangThai = new ToolStripStatusLabel();
        grpThongTin.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvKhach).BeginInit();
        statusStrip1.SuspendLayout();
        SuspendLayout();
        //
        // grpThongTin
        //
        grpThongTin.Controls.Add(lblHoTen);
        grpThongTin.Controls.Add(txtHoTen);
        grpThongTin.Controls.Add(rdoNam);
        grpThongTin.Controls.Add(rdoNu);
        grpThongTin.Controls.Add(lblNgaySinh);
        grpThongTin.Controls.Add(dtpNgaySinh);
        grpThongTin.Controls.Add(lblSoDienThoai);
        grpThongTin.Controls.Add(txtSoDienThoai);
        grpThongTin.Controls.Add(lblPhong);
        grpThongTin.Controls.Add(cboPhong);
        grpThongTin.Controls.Add(lblTinh);
        grpThongTin.Controls.Add(cboTinh);
        grpThongTin.Controls.Add(lblPhuongXa);
        grpThongTin.Controls.Add(cboPhuongXa);
        grpThongTin.Controls.Add(chkTamTru);
        grpThongTin.Location = new Point(10, 8);
        grpThongTin.Name = "grpThongTin";
        grpThongTin.Size = new Size(270, 290);
        grpThongTin.TabIndex = 0;
        grpThongTin.TabStop = false;
        grpThongTin.Text = "Thông tin khách thuê";
        //
        // lblHoTen
        //
        lblHoTen.AutoSize = true;
        lblHoTen.Location = new Point(12, 32);
        lblHoTen.Name = "lblHoTen";
        lblHoTen.Text = "Họ tên";
        //
        // txtHoTen
        //
        txtHoTen.Location = new Point(106, 28);
        txtHoTen.MaxLength = 100;
        txtHoTen.Name = "txtHoTen";
        txtHoTen.Size = new Size(154, 23);
        txtHoTen.TabIndex = 0;
        //
        // rdoNam
        //
        rdoNam.AutoSize = true;
        rdoNam.Checked = true;
        rdoNam.Location = new Point(106, 60);
        rdoNam.Name = "rdoNam";
        rdoNam.TabIndex = 1;
        rdoNam.TabStop = true;
        rdoNam.Text = "Nam";
        //
        // rdoNu
        //
        rdoNu.AutoSize = true;
        rdoNu.Location = new Point(174, 60);
        rdoNu.Name = "rdoNu";
        rdoNu.TabIndex = 2;
        rdoNu.Text = "Nữ";
        //
        // lblNgaySinh
        //
        lblNgaySinh.AutoSize = true;
        lblNgaySinh.Location = new Point(12, 94);
        lblNgaySinh.Name = "lblNgaySinh";
        lblNgaySinh.Text = "Ngày sinh";
        //
        // dtpNgaySinh
        //
        dtpNgaySinh.Format = DateTimePickerFormat.Custom;
        dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
        dtpNgaySinh.Location = new Point(106, 90);
        dtpNgaySinh.Name = "dtpNgaySinh";
        dtpNgaySinh.Size = new Size(154, 23);
        dtpNgaySinh.TabIndex = 3;
        //
        // lblSoDienThoai
        //
        lblSoDienThoai.AutoSize = true;
        lblSoDienThoai.Location = new Point(12, 128);
        lblSoDienThoai.Name = "lblSoDienThoai";
        lblSoDienThoai.Text = "Số điện thoại";
        //
        // txtSoDienThoai
        //
        txtSoDienThoai.Location = new Point(106, 124);
        txtSoDienThoai.MaxLength = 15;
        txtSoDienThoai.Name = "txtSoDienThoai";
        txtSoDienThoai.Size = new Size(154, 23);
        txtSoDienThoai.TabIndex = 4;
        //
        // lblPhong
        //
        lblPhong.AutoSize = true;
        lblPhong.Location = new Point(12, 162);
        lblPhong.Name = "lblPhong";
        lblPhong.Text = "Phòng";
        //
        // cboPhong
        //
        cboPhong.DropDownStyle = ComboBoxStyle.DropDownList;
        cboPhong.Location = new Point(106, 158);
        cboPhong.Name = "cboPhong";
        cboPhong.Size = new Size(154, 23);
        cboPhong.TabIndex = 5;
        //
        // lblTinh
        //
        lblTinh.AutoSize = true;
        lblTinh.Location = new Point(12, 196);
        lblTinh.Name = "lblTinh";
        lblTinh.Text = "Tỉnh/Thành";
        //
        // cboTinh
        //
        cboTinh.DropDownStyle = ComboBoxStyle.DropDown;
        cboTinh.Location = new Point(106, 192);
        cboTinh.MaxLength = 100;
        cboTinh.Name = "cboTinh";
        cboTinh.Size = new Size(154, 23);
        cboTinh.MaxDropDownItems = 5;
        cboTinh.IntegralHeight = false;
        cboTinh.DropDownHeight = 110;
        cboTinh.TabIndex = 6;
        cboTinh.SelectedIndexChanged += cboTinh_SelectedIndexChanged;
        //
        // lblPhuongXa
        //
        lblPhuongXa.AutoSize = true;
        lblPhuongXa.Location = new Point(12, 230);
        lblPhuongXa.Name = "lblPhuongXa";
        lblPhuongXa.Text = "Phường/Xã";
        //
        // cboPhuongXa
        //
        cboPhuongXa.DropDownStyle = ComboBoxStyle.DropDown;
        cboPhuongXa.Location = new Point(106, 226);
        cboPhuongXa.MaxLength = 100;
        cboPhuongXa.Name = "cboPhuongXa";
        cboPhuongXa.Size = new Size(154, 23);
        cboPhuongXa.MaxDropDownItems = 5;
        cboPhuongXa.IntegralHeight = false;
        cboPhuongXa.DropDownHeight = 110;
        cboPhuongXa.TabIndex = 7;
        //
        // chkTamTru
        //
        chkTamTru.AutoSize = true;
        chkTamTru.Location = new Point(106, 260);
        chkTamTru.Name = "chkTamTru";
        chkTamTru.TabIndex = 8;
        chkTamTru.Text = "Đã đăng ký tạm trú";
        //
        // btnThem
        //
        btnThem.Location = new Point(10, 305);
        btnThem.Name = "btnThem";
        btnThem.Size = new Size(62, 28);
        btnThem.TabIndex = 1;
        btnThem.Text = "Thêm";
        btnThem.Click += btnThem_Click;
        //
        // btnSua
        //
        btnSua.Location = new Point(76, 305);
        btnSua.Name = "btnSua";
        btnSua.Size = new Size(62, 28);
        btnSua.TabIndex = 2;
        btnSua.Text = "Sửa";
        btnSua.Click += btnSua_Click;
        //
        // btnXoa
        //
        btnXoa.Location = new Point(142, 305);
        btnXoa.Name = "btnXoa";
        btnXoa.Size = new Size(62, 28);
        btnXoa.TabIndex = 3;
        btnXoa.Text = "Xóa";
        btnXoa.Click += btnXoa_Click;
        //
        // btnLamMoi
        //
        btnLamMoi.Location = new Point(208, 305);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(72, 28);
        btnLamMoi.TabIndex = 4;
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.Click += btnLamMoi_Click;
        //
        // btnGayLoi (nút ẩn dùng để kiểm tra xử lý lỗi toàn cục)
        //
        btnGayLoi.Location = new Point(10, 340);
        btnGayLoi.Name = "btnGayLoi";
        btnGayLoi.Size = new Size(100, 28);
        btnGayLoi.TabIndex = 5;
        btnGayLoi.Text = "Gây lỗi thử";
        btnGayLoi.Visible = false;
        btnGayLoi.Click += btnGayLoi_Click;
        //
        // txtTimKiem
        //
        txtTimKiem.Location = new Point(290, 12);
        txtTimKiem.Name = "txtTimKiem";
        txtTimKiem.PlaceholderText = "Tìm theo tên hoặc SĐT";
        txtTimKiem.Size = new Size(230, 23);
        txtTimKiem.TabIndex = 6;
        //
        // lblLocPhong
        //
        lblLocPhong.AutoSize = true;
        lblLocPhong.Location = new Point(530, 16);
        lblLocPhong.Name = "lblLocPhong";
        lblLocPhong.Text = "Phòng:";
        //
        // cboLocPhong
        //
        cboLocPhong.DropDownStyle = ComboBoxStyle.DropDownList;
        cboLocPhong.Location = new Point(580, 12);
        cboLocPhong.Name = "cboLocPhong";
        cboLocPhong.Size = new Size(120, 23);
        cboLocPhong.IntegralHeight = false;
        cboLocPhong.DropDownHeight = 110;
        cboLocPhong.TabIndex = 7;
        //
        // lblLocTinh
        //
        lblLocTinh.AutoSize = true;
        lblLocTinh.Location = new Point(290, 50);
        lblLocTinh.Name = "lblLocTinh";
        lblLocTinh.Text = "Tỉnh/Thành:";
        //
        // cboLocTinh
        //
        cboLocTinh.DropDownStyle = ComboBoxStyle.DropDownList;
        cboLocTinh.Location = new Point(365, 46);
        cboLocTinh.Name = "cboLocTinh";
        cboLocTinh.Size = new Size(154, 23);
        cboLocTinh.IntegralHeight = false;
        cboLocTinh.DropDownHeight = 110;
        cboLocTinh.TabIndex = 8;
        //
        // btnTim
        //
        btnTim.Location = new Point(710, 10);
        btnTim.Name = "btnTim";
        btnTim.Size = new Size(50, 27);
        btnTim.TabIndex = 9;
        btnTim.Text = "Tìm";
        btnTim.Click += btnTim_Click;
        //
        // btnBoLoc
        //
        btnBoLoc.Location = new Point(540, 44);
        btnBoLoc.Name = "btnBoLoc";
        btnBoLoc.Size = new Size(58, 27);
        btnBoLoc.TabIndex = 10;
        btnBoLoc.Text = "Bỏ lọc";
        btnBoLoc.Click += btnBoLoc_Click;
        //
        // dgvKhach
        //
        dgvKhach.AllowUserToAddRows = false;
        dgvKhach.AllowUserToDeleteRows = false;
        dgvKhach.AllowUserToResizeRows = false;
        dgvKhach.AutoGenerateColumns = false;
        dgvKhach.BackgroundColor = SystemColors.AppWorkspace;
        dgvKhach.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvKhach.Columns.AddRange(new DataGridViewColumn[] { colHoTen, colSoDienThoai, colPhong, colGiaThue, colQueQuan, colTamTru });
        dgvKhach.Location = new Point(290, 78);
        dgvKhach.MultiSelect = false;
        dgvKhach.Name = "dgvKhach";
        dgvKhach.ReadOnly = true;
        dgvKhach.RowHeadersWidth = 20;
        dgvKhach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvKhach.Size = new Size(720, 290);
        dgvKhach.TabIndex = 11;
        dgvKhach.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvKhach.SelectionChanged += dgvKhach_SelectionChanged;
        //
        // colHoTen
        //
        colHoTen.DataPropertyName = "HoTen";
        colHoTen.HeaderText = "Họ tên";
        colHoTen.Name = "colHoTen";
        colHoTen.ReadOnly = true;
        colHoTen.Width = 150;
        //
        // colSoDienThoai
        //
        colSoDienThoai.DataPropertyName = "SoDienThoai";
        colSoDienThoai.HeaderText = "SĐT";
        colSoDienThoai.Name = "colSoDienThoai";
        colSoDienThoai.ReadOnly = true;
        colSoDienThoai.Width = 100;
        //
        // colPhong
        //
        colPhong.DataPropertyName = "TenPhong";
        colPhong.HeaderText = "Phòng";
        colPhong.Name = "colPhong";
        colPhong.ReadOnly = true;
        colPhong.Width = 60;
        //
        // colGiaThue
        //
        colGiaThue.DataPropertyName = "GiaThue";
        colGiaThue.HeaderText = "Giá thuê";
        colGiaThue.Name = "colGiaThue";
        colGiaThue.ReadOnly = true;
        colGiaThue.Width = 100;
        //
        // colQueQuan
        //
        colQueQuan.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colQueQuan.DataPropertyName = "QueQuan";
        colQueQuan.HeaderText = "Quê quán";
        colQueQuan.Name = "colQueQuan";
        colQueQuan.ReadOnly = true;
        //
        // colTamTru
        //
        colTamTru.DataPropertyName = "TamTru";
        colTamTru.HeaderText = "Tạm trú";
        colTamTru.Name = "colTamTru";
        colTamTru.ReadOnly = true;
        colTamTru.Width = 70;
        //
        // statusStrip1
        //
        statusStrip1.Items.AddRange(new ToolStripItem[] { lblTrangThai });
        statusStrip1.Name = "statusStrip1";
        statusStrip1.TabIndex = 12;
        //
        // lblTrangThai
        //
        lblTrangThai.Name = "lblTrangThai";
        lblTrangThai.Text = "Sẵn sàng";
        //
        // FormKhachThue
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1020, 405);
        Controls.Add(grpThongTin);
        Controls.Add(btnThem);
        Controls.Add(btnSua);
        Controls.Add(btnXoa);
        Controls.Add(btnLamMoi);
        Controls.Add(btnGayLoi);
        Controls.Add(txtTimKiem);
        Controls.Add(lblLocPhong);
        Controls.Add(cboLocPhong);
        Controls.Add(lblLocTinh);
        Controls.Add(cboLocTinh);
        Controls.Add(btnTim);
        Controls.Add(btnBoLoc);
        Controls.Add(dgvKhach);
        Controls.Add(statusStrip1);
        KeyPreview = true;
        MinimumSize = new Size(1036, 444);
        Name = "FormKhachThue";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Quản lý nhà trọ - Khách thuê";
        Load += FormKhachThue_Load;
        KeyDown += FormKhachThue_KeyDown;
        grpThongTin.ResumeLayout(false);
        grpThongTin.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvKhach).EndInit();
        statusStrip1.ResumeLayout(false);
        statusStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private GroupBox grpThongTin;
    private Label lblHoTen;
    private TextBox txtHoTen;
    private RadioButton rdoNam;
    private RadioButton rdoNu;
    private Label lblNgaySinh;
    private DateTimePicker dtpNgaySinh;
    private Label lblSoDienThoai;
    private TextBox txtSoDienThoai;
    private Label lblPhong;
    private ComboBox cboPhong;
    private Label lblTinh;
    private ComboBox cboTinh;
    private Label lblPhuongXa;
    private ComboBox cboPhuongXa;
    private CheckBox chkTamTru;
    private Button btnThem;
    private Button btnSua;
    private Button btnXoa;
    private Button btnLamMoi;
    private Button btnGayLoi;
    private TextBox txtTimKiem;
    private Label lblLocPhong;
    private ComboBox cboLocPhong;
    private Label lblLocTinh;
    private ComboBox cboLocTinh;
    private Button btnTim;
    private Button btnBoLoc;
    private DataGridView dgvKhach;
    private DataGridViewTextBoxColumn colHoTen;
    private DataGridViewTextBoxColumn colSoDienThoai;
    private DataGridViewTextBoxColumn colPhong;
    private DataGridViewTextBoxColumn colGiaThue;
    private DataGridViewTextBoxColumn colQueQuan;
    private DataGridViewCheckBoxColumn colTamTru;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel lblTrangThai;
}
