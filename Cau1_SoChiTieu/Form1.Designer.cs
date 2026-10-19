namespace Cau1_SoChiTieu;

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
        System.Windows.Forms.DataGridViewCellStyle dgvHeaderStyle = new System.Windows.Forms.DataGridViewCellStyle();
        this.grpThongTin = new System.Windows.Forms.GroupBox();
        this.lblNgayChi = new System.Windows.Forms.Label();
        this.dtpNgayChi = new System.Windows.Forms.DateTimePicker();
        this.lblNoiDung = new System.Windows.Forms.Label();
        this.txtNoiDung = new System.Windows.Forms.TextBox();
        this.lblLoaiChi = new System.Windows.Forms.Label();
        this.cboLoaiChi = new System.Windows.Forms.ComboBox();
        this.lblSoTien = new System.Windows.Forms.Label();
        this.numSoTien = new System.Windows.Forms.NumericUpDown();
        this.lblGhiChu = new System.Windows.Forms.Label();
        this.txtGhiChu = new System.Windows.Forms.TextBox();
        this.grpTimKiem = new System.Windows.Forms.GroupBox();
        this.txtTimKiem = new System.Windows.Forms.TextBox();
        this.lblLocLoaiChi = new System.Windows.Forms.Label();
        this.cboLocLoaiChi = new System.Windows.Forms.ComboBox();
        this.lblTuNgay = new System.Windows.Forms.Label();
        this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
        this.lblDenNgay = new System.Windows.Forms.Label();
        this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
        this.btnTim = new System.Windows.Forms.Button();
        this.btnThem = new System.Windows.Forms.Button();
        this.btnSua = new System.Windows.Forms.Button();
        this.btnXoa = new System.Windows.Forms.Button();
        this.btnLamMoi = new System.Windows.Forms.Button();
        this.dgvChiTieu = new System.Windows.Forms.DataGridView();
        this.colMa = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colNgayChi = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colNoiDung = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colLoaiChi = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colSoTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colGhiChu = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.lblTongChi = new System.Windows.Forms.Label();
        this.grpThongTin.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numSoTien)).BeginInit();
        this.grpTimKiem.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dgvChiTieu)).BeginInit();
        this.SuspendLayout();
        //
        // grpThongTin
        //
        this.grpThongTin.Controls.Add(this.lblNgayChi);
        this.grpThongTin.Controls.Add(this.dtpNgayChi);
        this.grpThongTin.Controls.Add(this.lblNoiDung);
        this.grpThongTin.Controls.Add(this.txtNoiDung);
        this.grpThongTin.Controls.Add(this.lblLoaiChi);
        this.grpThongTin.Controls.Add(this.cboLoaiChi);
        this.grpThongTin.Controls.Add(this.lblSoTien);
        this.grpThongTin.Controls.Add(this.numSoTien);
        this.grpThongTin.Controls.Add(this.lblGhiChu);
        this.grpThongTin.Controls.Add(this.txtGhiChu);
        this.grpThongTin.Location = new System.Drawing.Point(12, 8);
        this.grpThongTin.Name = "grpThongTin";
        this.grpThongTin.Size = new System.Drawing.Size(480, 150);
        this.grpThongTin.TabIndex = 0;
        this.grpThongTin.TabStop = false;
        this.grpThongTin.Text = "Thông tin khoản chi";
        //
        // lblNgayChi
        //
        this.lblNgayChi.AutoSize = true;
        this.lblNgayChi.Location = new System.Drawing.Point(15, 29);
        this.lblNgayChi.Name = "lblNgayChi";
        this.lblNgayChi.Text = "Ngày chi";
        //
        // dtpNgayChi
        //
        this.dtpNgayChi.CustomFormat = "dd/MM/yyyy";
        this.dtpNgayChi.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
        this.dtpNgayChi.Location = new System.Drawing.Point(85, 25);
        this.dtpNgayChi.Name = "dtpNgayChi";
        this.dtpNgayChi.Size = new System.Drawing.Size(130, 23);
        this.dtpNgayChi.TabIndex = 0;
        //
        // lblNoiDung
        //
        this.lblNoiDung.AutoSize = true;
        this.lblNoiDung.Location = new System.Drawing.Point(15, 62);
        this.lblNoiDung.Name = "lblNoiDung";
        this.lblNoiDung.Text = "Nội dung";
        //
        // txtNoiDung
        //
        this.txtNoiDung.Location = new System.Drawing.Point(85, 58);
        this.txtNoiDung.MaxLength = 100;
        this.txtNoiDung.Name = "txtNoiDung";
        this.txtNoiDung.Size = new System.Drawing.Size(130, 23);
        this.txtNoiDung.TabIndex = 1;
        //
        // lblLoaiChi
        //
        this.lblLoaiChi.AutoSize = true;
        this.lblLoaiChi.Location = new System.Drawing.Point(15, 95);
        this.lblLoaiChi.Name = "lblLoaiChi";
        this.lblLoaiChi.Text = "Loại chi";
        //
        // cboLoaiChi
        //
        this.cboLoaiChi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cboLoaiChi.Location = new System.Drawing.Point(85, 91);
        this.cboLoaiChi.Name = "cboLoaiChi";
        this.cboLoaiChi.Size = new System.Drawing.Size(130, 23);
        this.cboLoaiChi.TabIndex = 2;
        //
        // lblSoTien
        //
        this.lblSoTien.AutoSize = true;
        this.lblSoTien.Location = new System.Drawing.Point(240, 29);
        this.lblSoTien.Name = "lblSoTien";
        this.lblSoTien.Text = "Số tiền";
        //
        // numSoTien
        //
        this.numSoTien.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
        this.numSoTien.Location = new System.Drawing.Point(300, 25);
        this.numSoTien.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
        this.numSoTien.Name = "numSoTien";
        this.numSoTien.Size = new System.Drawing.Size(165, 23);
        this.numSoTien.TabIndex = 3;
        this.numSoTien.ThousandsSeparator = true;
        //
        // lblGhiChu
        //
        this.lblGhiChu.AutoSize = true;
        this.lblGhiChu.Location = new System.Drawing.Point(240, 62);
        this.lblGhiChu.Name = "lblGhiChu";
        this.lblGhiChu.Text = "Ghi chú";
        //
        // txtGhiChu
        //
        this.txtGhiChu.AcceptsReturn = true;
        this.txtGhiChu.Location = new System.Drawing.Point(300, 58);
        this.txtGhiChu.MaxLength = 200;
        this.txtGhiChu.Multiline = true;
        this.txtGhiChu.Name = "txtGhiChu";
        this.txtGhiChu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.txtGhiChu.Size = new System.Drawing.Size(165, 56);
        this.txtGhiChu.TabIndex = 4;
        //
        // grpTimKiem
        //
        this.grpTimKiem.Controls.Add(this.txtTimKiem);
        this.grpTimKiem.Controls.Add(this.lblLocLoaiChi);
        this.grpTimKiem.Controls.Add(this.cboLocLoaiChi);
        this.grpTimKiem.Controls.Add(this.lblTuNgay);
        this.grpTimKiem.Controls.Add(this.dtpTuNgay);
        this.grpTimKiem.Controls.Add(this.lblDenNgay);
        this.grpTimKiem.Controls.Add(this.dtpDenNgay);
        this.grpTimKiem.Controls.Add(this.btnTim);
        this.grpTimKiem.Location = new System.Drawing.Point(505, 8);
        this.grpTimKiem.Name = "grpTimKiem";
        this.grpTimKiem.Size = new System.Drawing.Size(285, 150);
        this.grpTimKiem.TabIndex = 1;
        this.grpTimKiem.TabStop = false;
        this.grpTimKiem.Text = "Tìm kiếm";
        //
        // txtTimKiem
        //
        this.txtTimKiem.Location = new System.Drawing.Point(15, 25);
        this.txtTimKiem.Name = "txtTimKiem";
        this.txtTimKiem.PlaceholderText = "Nhập nội dung...";
        this.txtTimKiem.Size = new System.Drawing.Size(255, 23);
        this.txtTimKiem.TabIndex = 0;
        //
        // lblLocLoaiChi
        //
        this.lblLocLoaiChi.AutoSize = true;
        this.lblLocLoaiChi.Location = new System.Drawing.Point(15, 62);
        this.lblLocLoaiChi.Name = "lblLocLoaiChi";
        this.lblLocLoaiChi.Text = "Loại chi";
        //
        // cboLocLoaiChi
        //
        this.cboLocLoaiChi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cboLocLoaiChi.Location = new System.Drawing.Point(80, 58);
        this.cboLocLoaiChi.Name = "cboLocLoaiChi";
        this.cboLocLoaiChi.Size = new System.Drawing.Size(190, 23);
        this.cboLocLoaiChi.TabIndex = 1;
        //
        // lblTuNgay
        //
        this.lblTuNgay.AutoSize = true;
        this.lblTuNgay.Location = new System.Drawing.Point(15, 95);
        this.lblTuNgay.Name = "lblTuNgay";
        this.lblTuNgay.Text = "Từ ngày";
        //
        // dtpTuNgay
        //
        this.dtpTuNgay.CustomFormat = "dd/MM/yyyy";
        this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
        this.dtpTuNgay.Location = new System.Drawing.Point(80, 91);
        this.dtpTuNgay.Name = "dtpTuNgay";
        this.dtpTuNgay.Size = new System.Drawing.Size(120, 23);
        this.dtpTuNgay.TabIndex = 2;
        //
        // lblDenNgay
        //
        this.lblDenNgay.AutoSize = true;
        this.lblDenNgay.Location = new System.Drawing.Point(15, 122);
        this.lblDenNgay.Name = "lblDenNgay";
        this.lblDenNgay.Text = "Đến ngày";
        //
        // dtpDenNgay
        //
        this.dtpDenNgay.CustomFormat = "dd/MM/yyyy";
        this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
        this.dtpDenNgay.Location = new System.Drawing.Point(80, 118);
        this.dtpDenNgay.Name = "dtpDenNgay";
        this.dtpDenNgay.Size = new System.Drawing.Size(120, 23);
        this.dtpDenNgay.TabIndex = 3;
        //
        // btnTim
        //
        this.btnTim.Location = new System.Drawing.Point(210, 117);
        this.btnTim.Name = "btnTim";
        this.btnTim.Size = new System.Drawing.Size(60, 25);
        this.btnTim.TabIndex = 4;
        this.btnTim.Text = "Tìm";
        this.btnTim.UseVisualStyleBackColor = true;
        this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
        //
        // btnThem
        //
        this.btnThem.Location = new System.Drawing.Point(120, 168);
        this.btnThem.Name = "btnThem";
        this.btnThem.Size = new System.Drawing.Size(80, 28);
        this.btnThem.TabIndex = 2;
        this.btnThem.Text = "Thêm";
        this.btnThem.UseVisualStyleBackColor = true;
        this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
        //
        // btnSua
        //
        this.btnSua.Location = new System.Drawing.Point(210, 168);
        this.btnSua.Name = "btnSua";
        this.btnSua.Size = new System.Drawing.Size(80, 28);
        this.btnSua.TabIndex = 3;
        this.btnSua.Text = "Sửa";
        this.btnSua.UseVisualStyleBackColor = true;
        this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
        //
        // btnXoa
        //
        this.btnXoa.Location = new System.Drawing.Point(300, 168);
        this.btnXoa.Name = "btnXoa";
        this.btnXoa.Size = new System.Drawing.Size(80, 28);
        this.btnXoa.TabIndex = 4;
        this.btnXoa.Text = "Xóa";
        this.btnXoa.UseVisualStyleBackColor = true;
        this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
        //
        // btnLamMoi
        //
        this.btnLamMoi.Location = new System.Drawing.Point(390, 168);
        this.btnLamMoi.Name = "btnLamMoi";
        this.btnLamMoi.Size = new System.Drawing.Size(80, 28);
        this.btnLamMoi.TabIndex = 5;
        this.btnLamMoi.Text = "Làm mới";
        this.btnLamMoi.UseVisualStyleBackColor = true;
        this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
        //
        // dgvChiTieu
        //
        this.dgvChiTieu.AllowUserToAddRows = false;
        this.dgvChiTieu.AllowUserToDeleteRows = false;
        this.dgvChiTieu.AllowUserToResizeRows = false;
        this.dgvChiTieu.AutoGenerateColumns = false;
        this.dgvChiTieu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        this.dgvChiTieu.BackgroundColor = System.Drawing.Color.White;
        dgvHeaderStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        this.dgvChiTieu.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
        this.dgvChiTieu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMa, this.colNgayChi, this.colNoiDung, this.colLoaiChi, this.colSoTien, this.colGhiChu });
        this.dgvChiTieu.Location = new System.Drawing.Point(12, 205);
        this.dgvChiTieu.MultiSelect = false;
        this.dgvChiTieu.Name = "dgvChiTieu";
        this.dgvChiTieu.ReadOnly = true;
        this.dgvChiTieu.RowHeadersWidth = 25;
        this.dgvChiTieu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dgvChiTieu.Size = new System.Drawing.Size(778, 250);
        this.dgvChiTieu.TabIndex = 6;
        this.dgvChiTieu.SelectionChanged += new System.EventHandler(this.dgvChiTieu_SelectionChanged);
        //
        // colMa
        //
        this.colMa.DataPropertyName = "MaCT";
        this.colMa.DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle { Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter, Format = "000" };
        this.colMa.FillWeight = 50F;
        this.colMa.HeaderText = "Mã";
        this.colMa.Name = "colMa";
        this.colMa.ReadOnly = true;
        //
        // colNgayChi
        //
        this.colNgayChi.DataPropertyName = "NgayChi";
        this.colNgayChi.DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle { Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter, Format = "dd/MM/yyyy" };
        this.colNgayChi.FillWeight = 80F;
        this.colNgayChi.HeaderText = "Ngày chi";
        this.colNgayChi.Name = "colNgayChi";
        this.colNgayChi.ReadOnly = true;
        //
        // colNoiDung
        //
        this.colNoiDung.DataPropertyName = "NoiDung";
        this.colNoiDung.FillWeight = 130F;
        this.colNoiDung.HeaderText = "Nội dung";
        this.colNoiDung.Name = "colNoiDung";
        this.colNoiDung.ReadOnly = true;
        //
        // colLoaiChi
        //
        this.colLoaiChi.DataPropertyName = "LoaiChi";
        this.colLoaiChi.FillWeight = 80F;
        this.colLoaiChi.HeaderText = "Loại chi";
        this.colLoaiChi.Name = "colLoaiChi";
        this.colLoaiChi.ReadOnly = true;
        //
        // colSoTien
        //
        this.colSoTien.DataPropertyName = "SoTien";
        this.colSoTien.DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle { Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight, Format = "N0", FormatProvider = System.Globalization.CultureInfo.GetCultureInfo("vi-VN") };
        this.colSoTien.FillWeight = 80F;
        this.colSoTien.HeaderText = "Số tiền";
        this.colSoTien.Name = "colSoTien";
        this.colSoTien.ReadOnly = true;
        //
        // colGhiChu
        //
        this.colGhiChu.DataPropertyName = "GhiChu";
        this.colGhiChu.FillWeight = 150F;
        this.colGhiChu.HeaderText = "Ghi chú";
        this.colGhiChu.Name = "colGhiChu";
        this.colGhiChu.ReadOnly = true;
        //
        // lblTongChi
        //
        this.lblTongChi.AutoSize = false;
        this.lblTongChi.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
        this.lblTongChi.ForeColor = System.Drawing.Color.Red;
        this.lblTongChi.Location = new System.Drawing.Point(440, 462);
        this.lblTongChi.Name = "lblTongChi";
        this.lblTongChi.Size = new System.Drawing.Size(350, 25);
        this.lblTongChi.TabIndex = 7;
        this.lblTongChi.Text = "Tổng chi: 0 đ";
        this.lblTongChi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        //
        // Form1
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(802, 497);
        this.Controls.Add(this.grpThongTin);
        this.Controls.Add(this.grpTimKiem);
        this.Controls.Add(this.btnThem);
        this.Controls.Add(this.btnSua);
        this.Controls.Add(this.btnXoa);
        this.Controls.Add(this.btnLamMoi);
        this.Controls.Add(this.dgvChiTieu);
        this.Controls.Add(this.lblTongChi);
        this.Name = "Form1";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Sổ chi tiêu cá nhân";
        this.grpThongTin.ResumeLayout(false);
        this.grpThongTin.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numSoTien)).EndInit();
        this.grpTimKiem.ResumeLayout(false);
        this.grpTimKiem.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dgvChiTieu)).EndInit();
        this.ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.GroupBox grpThongTin;
    private System.Windows.Forms.Label lblNgayChi;
    private System.Windows.Forms.DateTimePicker dtpNgayChi;
    private System.Windows.Forms.Label lblNoiDung;
    private System.Windows.Forms.TextBox txtNoiDung;
    private System.Windows.Forms.Label lblLoaiChi;
    private System.Windows.Forms.ComboBox cboLoaiChi;
    private System.Windows.Forms.Label lblSoTien;
    private System.Windows.Forms.NumericUpDown numSoTien;
    private System.Windows.Forms.Label lblGhiChu;
    private System.Windows.Forms.TextBox txtGhiChu;
    private System.Windows.Forms.GroupBox grpTimKiem;
    private System.Windows.Forms.TextBox txtTimKiem;
    private System.Windows.Forms.Label lblLocLoaiChi;
    private System.Windows.Forms.ComboBox cboLocLoaiChi;
    private System.Windows.Forms.Label lblTuNgay;
    private System.Windows.Forms.DateTimePicker dtpTuNgay;
    private System.Windows.Forms.Label lblDenNgay;
    private System.Windows.Forms.DateTimePicker dtpDenNgay;
    private System.Windows.Forms.Button btnTim;
    private System.Windows.Forms.Button btnThem;
    private System.Windows.Forms.Button btnSua;
    private System.Windows.Forms.Button btnXoa;
    private System.Windows.Forms.Button btnLamMoi;
    private System.Windows.Forms.DataGridView dgvChiTieu;
    private System.Windows.Forms.DataGridViewTextBoxColumn colMa;
    private System.Windows.Forms.DataGridViewTextBoxColumn colNgayChi;
    private System.Windows.Forms.DataGridViewTextBoxColumn colNoiDung;
    private System.Windows.Forms.DataGridViewTextBoxColumn colLoaiChi;
    private System.Windows.Forms.DataGridViewTextBoxColumn colSoTien;
    private System.Windows.Forms.DataGridViewTextBoxColumn colGhiChu;
    private System.Windows.Forms.Label lblTongChi;
}
