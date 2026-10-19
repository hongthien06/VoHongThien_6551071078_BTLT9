namespace Cau3_KeHoachDaNgoai
{
    partial class FormChuyenDi
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
            lblTieuDe = new Label();
            lblNgonNgu = new Label();
            cboNgonNgu = new ComboBox();
            grpThongTin = new GroupBox();
            lblTenChuyen = new Label();
            txtTenChuyen = new TextBox();
            lblDiaDiem = new Label();
            txtDiaDiem = new TextBox();
            lblNgayDi = new Label();
            dtpNgayDi = new DateTimePicker();
            lblSoNguoi = new Label();
            nudSoNguoi = new NumericUpDown();
            lblNganSach = new Label();
            nudNganSach = new NumericUpDown();
            lblPhuongTien = new Label();
            cboPhuongTien = new ComboBox();
            chkDaHoanThanh = new CheckBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            btnXemThoiTiet = new Button();
            progressBar = new ProgressBar();
            grpThoiTiet = new GroupBox();
            lblIcon = new Label();
            lblMoTa = new Label();
            lblNhietDo = new Label();
            lblXacSuatMua = new Label();
            pnlCanhBao = new Panel();
            lblCanhBao = new Label();
            txtTim = new TextBox();
            chkChiChuaHoanThanh = new CheckBox();
            btnTim = new Button();
            lblTu = new Label();
            dtpTu = new DateTimePicker();
            lblDen = new Label();
            dtpDen = new DateTimePicker();
            dgvChuyenDi = new DataGridView();
            colMa = new DataGridViewTextBoxColumn();
            colTen = new DataGridViewTextBoxColumn();
            colDiaDiem = new DataGridViewTextBoxColumn();
            colNgayDi = new DataGridViewTextBoxColumn();
            colSoNguoi = new DataGridViewTextBoxColumn();
            colNganSach = new DataGridViewTextBoxColumn();
            colPhuongTien = new DataGridViewTextBoxColumn();
            colHoanThanh = new DataGridViewCheckBoxColumn();
            grpThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudSoNguoi).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudNganSach).BeginInit();
            grpThoiTiet.SuspendLayout();
            pnlCanhBao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvChuyenDi).BeginInit();
            SuspendLayout();
            //
            // lblTieuDe
            //
            lblTieuDe.AutoSize = true;
            lblTieuDe.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTieuDe.Location = new Point(12, 10);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(300, 25);
            lblTieuDe.Text = "Kế hoạch dã ngoại cuối tuần";
            //
            // lblNgonNgu
            //
            lblNgonNgu.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblNgonNgu.AutoSize = true;
            lblNgonNgu.Location = new Point(556, 16);
            lblNgonNgu.Name = "lblNgonNgu";
            lblNgonNgu.Text = "Ngôn ngữ";
            //
            // cboNgonNgu
            //
            cboNgonNgu.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cboNgonNgu.DropDownStyle = ComboBoxStyle.DropDownList;
            cboNgonNgu.Items.AddRange(new object[] { "Tiếng Việt", "English" });
            cboNgonNgu.Location = new Point(628, 12);
            cboNgonNgu.Name = "cboNgonNgu";
            cboNgonNgu.Size = new Size(112, 23);
            cboNgonNgu.SelectedIndexChanged += cboNgonNgu_SelectedIndexChanged;
            //
            // grpThongTin
            //
            grpThongTin.Controls.Add(lblTenChuyen);
            grpThongTin.Controls.Add(txtTenChuyen);
            grpThongTin.Controls.Add(lblDiaDiem);
            grpThongTin.Controls.Add(txtDiaDiem);
            grpThongTin.Controls.Add(lblNgayDi);
            grpThongTin.Controls.Add(dtpNgayDi);
            grpThongTin.Controls.Add(lblSoNguoi);
            grpThongTin.Controls.Add(nudSoNguoi);
            grpThongTin.Controls.Add(lblNganSach);
            grpThongTin.Controls.Add(nudNganSach);
            grpThongTin.Controls.Add(lblPhuongTien);
            grpThongTin.Controls.Add(cboPhuongTien);
            grpThongTin.Controls.Add(chkDaHoanThanh);
            grpThongTin.Controls.Add(btnThem);
            grpThongTin.Controls.Add(btnSua);
            grpThongTin.Controls.Add(btnXoa);
            grpThongTin.Controls.Add(btnLamMoi);
            grpThongTin.Location = new Point(12, 46);
            grpThongTin.Name = "grpThongTin";
            grpThongTin.Size = new Size(360, 290);
            grpThongTin.Text = "Thông tin chuyến đi";
            //
            // lblTenChuyen
            //
            lblTenChuyen.AutoSize = true;
            lblTenChuyen.Location = new Point(14, 31);
            lblTenChuyen.Name = "lblTenChuyen";
            lblTenChuyen.Text = "Tên chuyến";
            //
            // txtTenChuyen
            //
            txtTenChuyen.Location = new Point(110, 28);
            txtTenChuyen.MaxLength = 100;
            txtTenChuyen.Name = "txtTenChuyen";
            txtTenChuyen.Size = new Size(235, 23);
            //
            // lblDiaDiem
            //
            lblDiaDiem.AutoSize = true;
            lblDiaDiem.Location = new Point(14, 63);
            lblDiaDiem.Name = "lblDiaDiem";
            lblDiaDiem.Text = "Địa điểm";
            //
            // txtDiaDiem
            //
            txtDiaDiem.Location = new Point(110, 60);
            txtDiaDiem.MaxLength = 100;
            txtDiaDiem.Name = "txtDiaDiem";
            txtDiaDiem.Size = new Size(235, 23);
            //
            // lblNgayDi
            //
            lblNgayDi.AutoSize = true;
            lblNgayDi.Location = new Point(14, 95);
            lblNgayDi.Name = "lblNgayDi";
            lblNgayDi.Text = "Ngày đi";
            //
            // dtpNgayDi
            //
            dtpNgayDi.Format = DateTimePickerFormat.Custom;
            dtpNgayDi.CustomFormat = "dd/MM/yyyy";
            dtpNgayDi.Location = new Point(110, 92);
            dtpNgayDi.Name = "dtpNgayDi";
            dtpNgayDi.Size = new Size(130, 23);
            //
            // lblSoNguoi
            //
            lblSoNguoi.AutoSize = true;
            lblSoNguoi.Location = new Point(14, 127);
            lblSoNguoi.Name = "lblSoNguoi";
            lblSoNguoi.Text = "Số người";
            //
            // nudSoNguoi
            //
            nudSoNguoi.Location = new Point(110, 124);
            nudSoNguoi.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudSoNguoi.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudSoNguoi.Name = "nudSoNguoi";
            nudSoNguoi.Size = new Size(70, 23);
            nudSoNguoi.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // lblNganSach
            //
            lblNganSach.AutoSize = true;
            lblNganSach.Location = new Point(14, 159);
            lblNganSach.Name = "lblNganSach";
            lblNganSach.Text = "Ngân sách";
            //
            // nudNganSach
            //
            nudNganSach.Increment = new decimal(new int[] { 100000, 0, 0, 0 });
            nudNganSach.Location = new Point(110, 156);
            nudNganSach.Name = "nudNganSach";
            nudNganSach.Size = new Size(130, 23);
            nudNganSach.ThousandsSeparator = true;
            //
            // lblPhuongTien
            //
            lblPhuongTien.AutoSize = true;
            lblPhuongTien.Location = new Point(14, 191);
            lblPhuongTien.Name = "lblPhuongTien";
            lblPhuongTien.Text = "Phương tiện";
            //
            // cboPhuongTien
            //
            cboPhuongTien.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPhuongTien.Location = new Point(110, 188);
            cboPhuongTien.Name = "cboPhuongTien";
            cboPhuongTien.Size = new Size(130, 23);
            //
            // chkDaHoanThanh
            //
            chkDaHoanThanh.AutoSize = true;
            chkDaHoanThanh.Location = new Point(110, 222);
            chkDaHoanThanh.Name = "chkDaHoanThanh";
            chkDaHoanThanh.Text = "Đã hoàn thành";
            chkDaHoanThanh.UseVisualStyleBackColor = true;
            //
            // btnThem
            //
            btnThem.Location = new Point(14, 252);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(78, 28);
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            //
            // btnSua
            //
            btnSua.Location = new Point(100, 252);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(78, 28);
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            //
            // btnXoa
            //
            btnXoa.Location = new Point(186, 252);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(78, 28);
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            //
            // btnLamMoi
            //
            btnLamMoi.Location = new Point(272, 252);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(78, 28);
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            //
            // btnXemThoiTiet
            //
            btnXemThoiTiet.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnXemThoiTiet.Location = new Point(388, 50);
            btnXemThoiTiet.Name = "btnXemThoiTiet";
            btnXemThoiTiet.Size = new Size(140, 28);
            btnXemThoiTiet.Text = "Xem thời tiết";
            btnXemThoiTiet.UseVisualStyleBackColor = true;
            btnXemThoiTiet.Click += btnXemThoiTiet_Click;
            //
            // progressBar
            //
            progressBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            progressBar.Location = new Point(538, 55);
            progressBar.MarqueeAnimationSpeed = 30;
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(202, 18);
            progressBar.Style = ProgressBarStyle.Blocks;
            //
            // grpThoiTiet
            //
            grpThoiTiet.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            grpThoiTiet.Controls.Add(lblIcon);
            grpThoiTiet.Controls.Add(lblMoTa);
            grpThoiTiet.Controls.Add(lblNhietDo);
            grpThoiTiet.Controls.Add(lblXacSuatMua);
            grpThoiTiet.Controls.Add(pnlCanhBao);
            grpThoiTiet.Location = new Point(388, 86);
            grpThoiTiet.Name = "grpThoiTiet";
            grpThoiTiet.Size = new Size(352, 250);
            grpThoiTiet.Text = "Dự báo thời tiết";
            //
            // lblIcon
            //
            lblIcon.Font = new Font("Segoe UI Emoji", 40F);
            lblIcon.Location = new Point(10, 40);
            lblIcon.Name = "lblIcon";
            lblIcon.Size = new Size(110, 100);
            lblIcon.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblMoTa
            //
            lblMoTa.Font = new Font("Segoe UI", 14F);
            lblMoTa.Location = new Point(125, 45);
            lblMoTa.Name = "lblMoTa";
            lblMoTa.Size = new Size(220, 30);
            //
            // lblNhietDo
            //
            lblNhietDo.Location = new Point(125, 85);
            lblNhietDo.Name = "lblNhietDo";
            lblNhietDo.Size = new Size(220, 20);
            //
            // lblXacSuatMua
            //
            lblXacSuatMua.Location = new Point(125, 110);
            lblXacSuatMua.Name = "lblXacSuatMua";
            lblXacSuatMua.Size = new Size(220, 20);
            //
            // pnlCanhBao
            //
            pnlCanhBao.BackColor = Color.FromArgb(255, 204, 153);
            pnlCanhBao.Controls.Add(lblCanhBao);
            pnlCanhBao.Location = new Point(12, 175);
            pnlCanhBao.Name = "pnlCanhBao";
            pnlCanhBao.Size = new Size(328, 56);
            pnlCanhBao.Visible = false;
            //
            // lblCanhBao
            //
            lblCanhBao.Dock = DockStyle.Fill;
            lblCanhBao.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCanhBao.Name = "lblCanhBao";
            lblCanhBao.Padding = new Padding(8, 0, 8, 0);
            lblCanhBao.TextAlign = ContentAlignment.MiddleLeft;
            //
            // txtTim
            //
            txtTim.Location = new Point(12, 348);
            txtTim.Name = "txtTim";
            txtTim.PlaceholderText = "Tìm tên chuyến hoặc địa điểm...";
            txtTim.Size = new Size(240, 23);
            //
            // chkChiChuaHoanThanh
            //
            chkChiChuaHoanThanh.AutoSize = true;
            chkChiChuaHoanThanh.Location = new Point(262, 350);
            chkChiChuaHoanThanh.Name = "chkChiChuaHoanThanh";
            chkChiChuaHoanThanh.Text = "Chỉ hiện chuyến chưa hoàn thành";
            chkChiChuaHoanThanh.UseVisualStyleBackColor = true;
            //
            // btnTim
            //
            btnTim.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTim.Location = new Point(650, 346);
            btnTim.Name = "btnTim";
            btnTim.Size = new Size(90, 28);
            btnTim.Text = "Tìm";
            btnTim.UseVisualStyleBackColor = true;
            btnTim.Click += btnTim_Click;
            //
            // lblTu
            //
            lblTu.AutoSize = true;
            lblTu.Location = new Point(12, 385);
            lblTu.Name = "lblTu";
            lblTu.Text = "Từ ngày";
            //
            // dtpTu
            //
            dtpTu.Format = DateTimePickerFormat.Custom;
            dtpTu.CustomFormat = "dd/MM/yyyy";
            dtpTu.Location = new Point(75, 381);
            dtpTu.Name = "dtpTu";
            dtpTu.ShowCheckBox = true;
            dtpTu.Checked = false;
            dtpTu.Size = new Size(140, 23);
            //
            // lblDen
            //
            lblDen.AutoSize = true;
            lblDen.Location = new Point(235, 385);
            lblDen.Name = "lblDen";
            lblDen.Text = "Đến ngày";
            //
            // dtpDen
            //
            dtpDen.Format = DateTimePickerFormat.Custom;
            dtpDen.CustomFormat = "dd/MM/yyyy";
            dtpDen.Location = new Point(305, 381);
            dtpDen.Name = "dtpDen";
            dtpDen.ShowCheckBox = true;
            dtpDen.Checked = false;
            dtpDen.Size = new Size(140, 23);
            //
            // dgvChuyenDi
            //
            dgvChuyenDi.AllowUserToAddRows = false;
            dgvChuyenDi.AllowUserToDeleteRows = false;
            dgvChuyenDi.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvChuyenDi.AutoGenerateColumns = false;
            dgvChuyenDi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvChuyenDi.BackgroundColor = SystemColors.Window;
            dgvChuyenDi.Columns.AddRange(new DataGridViewColumn[] { colMa, colTen, colDiaDiem, colNgayDi, colSoNguoi, colNganSach, colPhuongTien, colHoanThanh });
            dgvChuyenDi.Location = new Point(12, 414);
            dgvChuyenDi.MultiSelect = false;
            dgvChuyenDi.Name = "dgvChuyenDi";
            dgvChuyenDi.ReadOnly = true;
            dgvChuyenDi.RowHeadersWidth = 24;
            dgvChuyenDi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvChuyenDi.Size = new Size(728, 224);
            dgvChuyenDi.SelectionChanged += dgvChuyenDi_SelectionChanged;
            dgvChuyenDi.CellFormatting += dgvChuyenDi_CellFormatting;
            //
            // columns
            //
            colMa.DataPropertyName = "MaChuyen";
            colMa.Name = "colMa";
            colMa.FillWeight = 45;
            colMa.MinimumWidth = 45;
            colTen.DataPropertyName = "TenChuyen";
            colTen.Name = "colTen";
            colTen.FillWeight = 160;
            colTen.MinimumWidth = 90;
            colDiaDiem.DataPropertyName = "DiaDiem";
            colDiaDiem.Name = "colDiaDiem";
            colDiaDiem.FillWeight = 100;
            colDiaDiem.MinimumWidth = 45;
            colNgayDi.DataPropertyName = "NgayDi";
            colNgayDi.DefaultCellStyle = new DataGridViewCellStyle { Format = "d" };
            colNgayDi.Name = "colNgayDi";
            colNgayDi.FillWeight = 85;
            colNgayDi.MinimumWidth = 80;
            colSoNguoi.DataPropertyName = "SoNguoi";
            colSoNguoi.Name = "colSoNguoi";
            colSoNguoi.FillWeight = 70;
            colSoNguoi.MinimumWidth = 60;
            colNganSach.DataPropertyName = "NganSach";
            colNganSach.DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" };
            colNganSach.Name = "colNganSach";
            colNganSach.FillWeight = 90;
            colNganSach.MinimumWidth = 75;
            colPhuongTien.DataPropertyName = "PhuongTien";
            colPhuongTien.Name = "colPhuongTien";
            colPhuongTien.FillWeight = 90;
            colPhuongTien.MinimumWidth = 75;
            colHoanThanh.DataPropertyName = "DaHoanThanh";
            colHoanThanh.Name = "colHoanThanh";
            colHoanThanh.FillWeight = 75;
            colHoanThanh.MinimumWidth = 70;
            //
            // FormChuyenDi
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(752, 650);
            Controls.Add(lblTieuDe);
            Controls.Add(lblNgonNgu);
            Controls.Add(cboNgonNgu);
            Controls.Add(grpThongTin);
            Controls.Add(btnXemThoiTiet);
            Controls.Add(progressBar);
            Controls.Add(grpThoiTiet);
            Controls.Add(txtTim);
            Controls.Add(chkChiChuaHoanThanh);
            Controls.Add(btnTim);
            Controls.Add(lblTu);
            Controls.Add(dtpTu);
            Controls.Add(lblDen);
            Controls.Add(dtpDen);
            Controls.Add(dgvChuyenDi);
            MinimumSize = new Size(768, 689);
            Name = "FormChuyenDi";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Kế hoạch dã ngoại cuối tuần";
            Load += FormChuyenDi_Load;
            grpThongTin.ResumeLayout(false);
            grpThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudSoNguoi).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudNganSach).EndInit();
            grpThoiTiet.ResumeLayout(false);
            pnlCanhBao.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvChuyenDi).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTieuDe;
        private Label lblNgonNgu;
        private ComboBox cboNgonNgu;
        private GroupBox grpThongTin;
        private Label lblTenChuyen;
        private TextBox txtTenChuyen;
        private Label lblDiaDiem;
        private TextBox txtDiaDiem;
        private Label lblNgayDi;
        private DateTimePicker dtpNgayDi;
        private Label lblSoNguoi;
        private NumericUpDown nudSoNguoi;
        private Label lblNganSach;
        private NumericUpDown nudNganSach;
        private Label lblPhuongTien;
        private ComboBox cboPhuongTien;
        private CheckBox chkDaHoanThanh;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Button btnXemThoiTiet;
        private ProgressBar progressBar;
        private GroupBox grpThoiTiet;
        private Label lblIcon;
        private Label lblMoTa;
        private Label lblNhietDo;
        private Label lblXacSuatMua;
        private Panel pnlCanhBao;
        private Label lblCanhBao;
        private TextBox txtTim;
        private CheckBox chkChiChuaHoanThanh;
        private Button btnTim;
        private Label lblTu;
        private DateTimePicker dtpTu;
        private Label lblDen;
        private DateTimePicker dtpDen;
        private DataGridView dgvChuyenDi;
        private DataGridViewTextBoxColumn colMa;
        private DataGridViewTextBoxColumn colTen;
        private DataGridViewTextBoxColumn colDiaDiem;
        private DataGridViewTextBoxColumn colNgayDi;
        private DataGridViewTextBoxColumn colSoNguoi;
        private DataGridViewTextBoxColumn colNganSach;
        private DataGridViewTextBoxColumn colPhuongTien;
        private DataGridViewCheckBoxColumn colHoanThanh;
    }
}
