using System;
using System.IO;
using System.Text.Json;

namespace Cau3_KeHoachDaNgoai;

/// <summary>Cài đặt người dùng, lưu ở %LocalAppData%\KeHoachDaNgoai\usersettings.json.</summary>
public class UserSettings
{
    public string Language { get; set; } = "vi";

    // Cho phép đổi đường dẫn khi kiểm thử
    public static string FilePath { get; set; } = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "KeHoachDaNgoai", "usersettings.json");

    /// <summary>Đọc cài đặt; thiếu file hoặc file hỏng thì trả về mặc định.</summary>
    public static UserSettings Load()
    {
        try
        {
            if (File.Exists(FilePath))
            {
                var s = JsonSerializer.Deserialize<UserSettings>(File.ReadAllText(FilePath));
                if (s != null && (s.Language == "vi" || s.Language == "en"))
                    return s;
            }
        }
        catch { /* file hỏng -> dùng mặc định */ }
        return new UserSettings();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
            File.WriteAllText(FilePath, JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true }));
        }
        catch { /* không ghi được thì bỏ qua, không làm hỏng ứng dụng */ }
    }
}
