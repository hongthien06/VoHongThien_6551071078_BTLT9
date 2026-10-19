namespace Cau3_KeHoachDaNgoai;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FormChuyenDi());
    }
}
