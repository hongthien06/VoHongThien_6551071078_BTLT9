namespace Cau4_QuanTraSua;

partial class FormChinh
{
    private System.ComponentModel.IContainer components = null;
    private Button btnThucDon;
    private Button btnBanHang;
    private Button btnHoaDon;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        btnThucDon = new Button();
        btnBanHang = new Button();
        btnHoaDon = new Button();
        SuspendLayout();
        btnThucDon.Location = new Point(30, 25);
        btnThucDon.Name = "btnThucDon";
        btnThucDon.Size = new Size(220, 40);
        btnThucDon.Text = "Thực đơn";
        btnThucDon.Click += btnThucDon_Click;
        btnBanHang.Location = new Point(30, 80);
        btnBanHang.Name = "btnBanHang";
        btnBanHang.Size = new Size(220, 40);
        btnBanHang.Text = "Bán hàng";
        btnBanHang.Click += btnBanHang_Click;
        btnHoaDon.Location = new Point(30, 135);
        btnHoaDon.Name = "btnHoaDon";
        btnHoaDon.Size = new Size(220, 40);
        btnHoaDon.Text = "Danh sách hóa đơn";
        btnHoaDon.Click += btnHoaDon_Click;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(280, 200);
        Controls.Add(btnThucDon);
        Controls.Add(btnBanHang);
        Controls.Add(btnHoaDon);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        Name = "FormChinh";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Quán trà sữa";
        ResumeLayout(false);
    }
}
