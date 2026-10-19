using System;
using System.Collections.Generic;
using System.Globalization;

namespace Cau3_KeHoachDaNgoai;

/// <summary>Từ điển đa ngôn ngữ Việt / Anh; đổi ngôn ngữ có hiệu lực ngay, không cần khởi động lại.</summary>
public static class Lang
{
    public static string Current { get; private set; } = "vi";

    public static CultureInfo Culture => CultureInfo.GetCultureInfo(Current == "en" ? "en-US" : "vi-VN");

    /// <summary>Đặt ngôn ngữ ("vi" hoặc "en") và cập nhật CultureInfo cho toàn ứng dụng.</summary>
    public static void SetLanguage(string code)
    {
        Current = code == "en" ? "en" : "vi";
        var ci = Culture;
        CultureInfo.CurrentCulture = ci;
        CultureInfo.CurrentUICulture = ci;
        CultureInfo.DefaultThreadCurrentCulture = ci;
        CultureInfo.DefaultThreadCurrentUICulture = ci;
    }

    public static string T(string key)
    {
        var d = Current == "en" ? En : Vi;
        return d.TryGetValue(key, out var v) ? v : key;
    }

    public static string T(string key, params object[] args) => string.Format(Culture, T(key), args);

    /// <summary>Dịch tên phương tiện lưu trong CSDL (tiếng Việt) sang ngôn ngữ hiện tại.</summary>
    public static string Vehicle(string dbValue) => T("veh." + dbValue);

    public static readonly string[] Vehicles = { "Xe máy", "Ô tô", "Xe khách", "Tàu hỏa" };

    private static readonly Dictionary<string, string> Vi = new()
    {
        ["form.title"] = "Kế hoạch dã ngoại cuối tuần",
        ["lbl.title"] = "Kế hoạch dã ngoại cuối tuần",
        ["lbl.language"] = "Ngôn ngữ",
        ["grp.info"] = "Thông tin chuyến đi",
        ["lbl.name"] = "Tên chuyến",
        ["lbl.place"] = "Địa điểm",
        ["lbl.date"] = "Ngày đi",
        ["lbl.people"] = "Số người",
        ["lbl.budget"] = "Ngân sách",
        ["lbl.vehicle"] = "Phương tiện",
        ["chk.done"] = "Đã hoàn thành",
        ["btn.add"] = "Thêm",
        ["btn.edit"] = "Sửa",
        ["btn.delete"] = "Xóa",
        ["btn.reset"] = "Làm mới",
        ["btn.weather"] = "Xem thời tiết",
        ["grp.weather"] = "Dự báo thời tiết",
        ["grp.weather.for"] = "Dự báo thời tiết ngày {0} - {1}",
        ["txt.search.hint"] = "Tìm tên chuyến hoặc địa điểm...",
        ["chk.onlyPending"] = "Chỉ hiện chuyến chưa hoàn thành",
        ["lbl.from"] = "Từ ngày",
        ["lbl.to"] = "Đến ngày",
        ["btn.search"] = "Tìm",
        ["col.id"] = "Mã",
        ["col.name"] = "Tên chuyến",
        ["col.place"] = "Địa điểm",
        ["col.date"] = "Ngày đi",
        ["col.people"] = "Số người",
        ["col.budget"] = "Ngân sách",
        ["col.vehicle"] = "Phương tiện",
        ["col.done"] = "Hoàn thành",
        ["veh.Xe máy"] = "Xe máy",
        ["veh.Ô tô"] = "Ô tô",
        ["veh.Xe khách"] = "Xe khách",
        ["veh.Tàu hỏa"] = "Tàu hỏa",
        ["wx.clear"] = "Trời quang",
        ["wx.cloudy"] = "Có mây",
        ["wx.fog"] = "Sương mù",
        ["wx.rain"] = "Mưa",
        ["wx.thunder"] = "Dông",
        ["wx.other"] = "Thời tiết khác",
        ["wx.temp"] = "Nhiệt độ: {0:0}°C - {1:0}°C",
        ["wx.rainProb"] = "Khả năng mưa: {0}%",
        ["wx.rainProbNA"] = "Khả năng mưa: --",
        ["wx.advice"] = "Nên mang áo mưa hoặc cân nhắc dời lịch",
        ["wx.noData"] = "Chưa có dữ liệu dự báo",
        ["msg.title"] = "Thông báo",
        ["msg.error"] = "Lỗi",
        ["msg.confirm"] = "Xác nhận",
        ["msg.nameRequired"] = "Vui lòng nhập tên chuyến.",
        ["msg.placeRequired"] = "Vui lòng nhập địa điểm.",
        ["msg.datePast"] = "Ngày đi không được nhỏ hơn hôm nay.",
        ["msg.peopleInvalid"] = "Số người phải từ 1 trở lên.",
        ["msg.budgetInvalid"] = "Ngân sách phải từ 0 trở lên.",
        ["msg.pickRow"] = "Vui lòng chọn một chuyến đi trong danh sách.",
        ["msg.added"] = "Đã thêm chuyến đi.",
        ["msg.updated"] = "Đã cập nhật chuyến đi.",
        ["msg.deleted"] = "Đã xóa chuyến đi.",
        ["msg.confirmDelete"] = "Bạn có chắc muốn xóa chuyến \"{0}\"?",
        ["msg.notFoundRow"] = "Không tìm thấy chuyến đi (có thể đã bị xóa).",
        ["msg.dbError"] = "Lỗi cơ sở dữ liệu: {0}",
        ["msg.fromAfterTo"] = "Ngày bắt đầu không được lớn hơn ngày kết thúc.",
        ["msg.pickToWeather"] = "Vui lòng chọn một chuyến đi để xem thời tiết.",
        ["msg.placeNotFound"] = "Không tìm thấy địa danh \"{0}\". Hãy thử nhập tên không dấu, ví dụ \"Da Lat\".",
        ["msg.timeout"] = "Hết thời gian chờ (8 giây). Vui lòng thử lại sau.",
        ["msg.network"] = "Không thể kết nối mạng hoặc máy chủ thời tiết. Vui lòng kiểm tra kết nối Internet.",
        ["msg.badData"] = "Dữ liệu thời tiết trả về không hợp lệ.",
    };

    private static readonly Dictionary<string, string> En = new()
    {
        ["form.title"] = "Weekend Picnic Planner",
        ["lbl.title"] = "Weekend Picnic Planner",
        ["lbl.language"] = "Language",
        ["grp.info"] = "Trip information",
        ["lbl.name"] = "Trip name",
        ["lbl.place"] = "Destination",
        ["lbl.date"] = "Date",
        ["lbl.people"] = "People",
        ["lbl.budget"] = "Budget",
        ["lbl.vehicle"] = "Transport",
        ["chk.done"] = "Completed",
        ["btn.add"] = "Add",
        ["btn.edit"] = "Edit",
        ["btn.delete"] = "Delete",
        ["btn.reset"] = "Reset",
        ["btn.weather"] = "Check weather",
        ["grp.weather"] = "Weather forecast",
        ["grp.weather.for"] = "Weather forecast for {0} - {1}",
        ["txt.search.hint"] = "Search trip name or destination...",
        ["chk.onlyPending"] = "Only show incomplete trips",
        ["lbl.from"] = "From",
        ["lbl.to"] = "To",
        ["btn.search"] = "Search",
        ["col.id"] = "ID",
        ["col.name"] = "Trip name",
        ["col.place"] = "Destination",
        ["col.date"] = "Date",
        ["col.people"] = "People",
        ["col.budget"] = "Budget",
        ["col.vehicle"] = "Transport",
        ["col.done"] = "Completed",
        ["veh.Xe máy"] = "Motorbike",
        ["veh.Ô tô"] = "Car",
        ["veh.Xe khách"] = "Coach",
        ["veh.Tàu hỏa"] = "Train",
        ["wx.clear"] = "Clear sky",
        ["wx.cloudy"] = "Cloudy",
        ["wx.fog"] = "Fog",
        ["wx.rain"] = "Rain",
        ["wx.thunder"] = "Thunderstorm",
        ["wx.other"] = "Other weather",
        ["wx.temp"] = "Temperature: {0:0}°C - {1:0}°C",
        ["wx.rainProb"] = "Chance of rain: {0}%",
        ["wx.rainProbNA"] = "Chance of rain: --",
        ["wx.advice"] = "Bring a raincoat or consider rescheduling",
        ["wx.noData"] = "No forecast data yet",
        ["msg.title"] = "Notice",
        ["msg.error"] = "Error",
        ["msg.confirm"] = "Confirm",
        ["msg.nameRequired"] = "Please enter the trip name.",
        ["msg.placeRequired"] = "Please enter the destination.",
        ["msg.datePast"] = "The trip date cannot be earlier than today.",
        ["msg.peopleInvalid"] = "Number of people must be at least 1.",
        ["msg.budgetInvalid"] = "Budget must be 0 or greater.",
        ["msg.pickRow"] = "Please select a trip in the list.",
        ["msg.added"] = "Trip added.",
        ["msg.updated"] = "Trip updated.",
        ["msg.deleted"] = "Trip deleted.",
        ["msg.confirmDelete"] = "Are you sure you want to delete the trip \"{0}\"?",
        ["msg.notFoundRow"] = "Trip not found (it may have been deleted).",
        ["msg.dbError"] = "Database error: {0}",
        ["msg.fromAfterTo"] = "The start date cannot be later than the end date.",
        ["msg.pickToWeather"] = "Please select a trip to check the weather.",
        ["msg.placeNotFound"] = "Location \"{0}\" was not found. Try the name without diacritics, e.g. \"Da Lat\".",
        ["msg.timeout"] = "Request timed out (8 seconds). Please try again later.",
        ["msg.network"] = "Cannot reach the network or the weather server. Please check your Internet connection.",
        ["msg.badData"] = "The weather data returned is invalid.",
    };
}
