# So sánh Publish - Câu 5: Quản lý đặt tour (v1.0.0)

Đo thật trên máy phát triển (win-x64, Release, .NET 9).

| Tiêu chí | Framework-Dependent | Self-Contained (PublishSingleFile) |
|---|---|---|
| Lệnh | `dotnet publish -c Release -r win-x64 --self-contained false -o publish/FrameworkDependent_win-x64` | `dotnet publish -p:PublishProfile=SelfContained_win-x64` (tương đương `-r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true`) |
| Thư mục | `publish\FrameworkDependent_win-x64` | `publish\SelfContained_win-x64` |
| Số file | 46 | 3 (exe, pdb, appsettings.json) |
| Dung lượng tổng | 12.271.673 byte (~11,7 MB) | 125.091.979 byte (~119,3 MB) |
| File exe chính | 156.160 byte (apphost, cần .NET Desktop Runtime 9) | 125.065.192 byte (đã gồm runtime .NET + WinForms + EF Core + thư viện) |
| Cần cài .NET trên máy đích | Có (.NET Desktop Runtime 9 x64) | Không |
| appsettings.json | Nằm cạnh exe, sửa được | Nằm cạnh exe (không bị gói vào exe), sửa được |

Nhận xét: Self-Contained lớn gấp khoảng 10 lần nhưng chạy được trên máy chưa cài .NET và chỉ cần chép 1 exe + appsettings.json; Framework-Dependent nhẹ nhưng có nhiều file và đòi hỏi cài runtime.

## Hướng dẫn tự làm các phần không thể làm tự động

1. Chụp ảnh thư mục publish: mở `publish\SelfContained_win-x64` trong Explorer (thấy exe, appsettings.json).
2. Chụp thông tin dung lượng: chuột phải thư mục `publish\SelfContained_win-x64` > Properties (và bản FrameworkDependent để đối chiếu).
3. Chép thư mục `SelfContained_win-x64` sang máy chưa cài .NET (máy ảo hoặc Windows Sandbox), mở `appsettings.json` sửa `Server=` trong connection string cho khớp SQL Server của máy đó (cần có CSDL QL_DatTour, chạy `Database_QL_DatTour.sql` trước), chạy `Cau5_QuanLyDatTour.exe` rồi chụp ứng dụng đang chạy (tab Đặt tour, thấy tiêu đề "v1.0.0"). Máy đó cần có Internet để lấy tỷ giá.
