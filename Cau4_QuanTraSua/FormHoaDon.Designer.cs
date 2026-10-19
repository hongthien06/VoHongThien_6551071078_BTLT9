namespace Cau4_QuanTraSua;

partial class FormHoaDon
{
    private System.ComponentModel.IContainer components = null;
    private Label lblNgay, lblDoanhThu;
    private DateTimePicker dtpNgay;
    private DataGridView dgvHoaDon, dgvChiTiet;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblNgay = new Label(); lblDoanhThu = new Label();
        dtpNgay = new DateTimePicker();
        dgvHoaDon = new DataGridView(); dgvChiTiet = new DataGridView();
        ((System.ComponentModel.ISupportInitialize)dgvHoaDon).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvChiTiet).BeginInit();
        SuspendLayout();

        lblNgay.AutoSize = true; lblNgay.Location = new Point(15, 18); lblNgay.Text = "Ngày:";
        dtpNgay.Format = DateTimePickerFormat.Custom; dtpNgay.CustomFormat = "dd/MM/yyyy"; dtpNgay.Location = new Point(65, 14); dtpNgay.Size = new Size(120, 23);
        dtpNgay.Name = "dtpNgay"; dtpNgay.ValueChanged += dtpNgay_ValueChanged;
        lblDoanhThu.AutoSize = true; lblDoanhThu.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblDoanhThu.Location = new Point(240, 14); lblDoanhThu.Name = "lblDoanhThu"; lblDoanhThu.Text = "Doanh thu ngày: 0 đ";

        dgvHoaDon.AllowUserToAddRows = false; dgvHoaDon.AllowUserToDeleteRows = false; dgvHoaDon.ReadOnly = true;
        dgvHoaDon.MultiSelect = false; dgvHoaDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; dgvHoaDon.RowHeadersVisible = false;
        dgvHoaDon.Location = new Point(15, 50); dgvHoaDon.Size = new Size(620, 200); dgvHoaDon.Name = "dgvHoaDon";
        dgvHoaDon.SelectionChanged += dgvHoaDon_SelectionChanged;

        dgvChiTiet.AllowUserToAddRows = false; dgvChiTiet.AllowUserToDeleteRows = false; dgvChiTiet.ReadOnly = true;
        dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; dgvChiTiet.RowHeadersVisible = false;
        dgvChiTiet.Location = new Point(15, 265); dgvChiTiet.Size = new Size(620, 180); dgvChiTiet.Name = "dgvChiTiet";

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(654, 462);
        Controls.AddRange(new Control[] { lblNgay, dtpNgay, lblDoanhThu, dgvHoaDon, dgvChiTiet });
        Name = "FormHoaDon";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Danh sách hóa đơn - Quán trà sữa";
        Load += FormHoaDon_Load;
        ((System.ComponentModel.ISupportInitialize)dgvHoaDon).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvChiTiet).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
