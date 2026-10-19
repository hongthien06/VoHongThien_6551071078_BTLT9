namespace Cau4_QuanTraSua;

/// <summary>Màn hình chính: mở 3 form chức năng.</summary>
public partial class FormChinh : Form
{
    public FormChinh()
    {
        InitializeComponent();
    }

    private void btnThucDon_Click(object sender, EventArgs e) => new FormThucDon().ShowDialog(this);

    private void btnBanHang_Click(object sender, EventArgs e) => new FormBanHang().ShowDialog(this);

    private void btnHoaDon_Click(object sender, EventArgs e) => new FormHoaDon().ShowDialog(this);
}
