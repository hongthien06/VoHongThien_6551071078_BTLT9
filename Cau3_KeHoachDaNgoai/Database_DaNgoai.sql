-- =============================================
-- BÀI TẬP CHƯƠNG 11 - CÂU 3: KẾ HOẠCH DÃ NGOẠI
-- =============================================

IF DB_ID('QL_DaNgoai') IS NOT NULL
BEGIN
    ALTER DATABASE QL_DaNgoai SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QL_DaNgoai;
END
GO

CREATE DATABASE QL_DaNgoai;
GO

USE QL_DaNgoai;
GO

CREATE TABLE ChuyenDi (
    MaChuyen INT IDENTITY(1,1) PRIMARY KEY,
    TenChuyen NVARCHAR(100) NOT NULL,
    DiaDiem NVARCHAR(100) NOT NULL,
    NgayDi DATE NOT NULL,
    SoNguoi INT NOT NULL,
    NganSach DECIMAL(18,0) NOT NULL,
    PhuongTien NVARCHAR(30) NOT NULL, -- Xe máy / Ô tô / Xe khách / Tàu hỏa
    DaHoanThanh BIT NOT NULL DEFAULT 0,
    GhiChu NVARCHAR(200) NULL
);
GO

-- Dữ liệu mẫu: ngày đi tính từ hôm nay (vài ngày đến vài tuần tới)
INSERT INTO ChuyenDi (TenChuyen, DiaDiem, NgayDi, SoNguoi, NganSach, PhuongTien, DaHoanThanh, GhiChu) VALUES
(N'Cắm trại đồi chè',      N'Đà Lạt',   DATEADD(DAY, 3,  CAST(GETDATE() AS DATE)), 6,  3500000, N'Ô tô',     0, N'Mang theo lều và đồ nướng'),
(N'Khám phá Fansipan',     N'Sa Pa',    DATEADD(DAY, 6,  CAST(GETDATE() AS DATE)), 4,  6000000, N'Xe khách', 0, N'Đặt vé cáp treo trước'),
(N'Tắm biển Bãi Sau',      N'Vũng Tàu', DATEADD(DAY, 2,  CAST(GETDATE() AS DATE)), 8,  2800000, N'Xe máy',   0, NULL),
(N'Dạo phố cổ',            N'Hội An',   DATEADD(DAY, 10, CAST(GETDATE() AS DATE)), 5,  5200000, N'Tàu hỏa',  0, N'Thuê xe đạp đi chợ đêm'),
(N'Chèo thuyền Vịnh Hạ Long', N'Hạ Long', DATEADD(DAY, 14, CAST(GETDATE() AS DATE)), 10, 9000000, N'Xe khách', 0, NULL),
(N'Cắm trại hồ Tuyền Lâm', N'Đà Lạt',   DATEADD(DAY, 21, CAST(GETDATE() AS DATE)), 6,  3200000, N'Ô tô',     0, N'Chuẩn bị áo ấm'),
(N'Thăm đảo Phú Quốc',     N'Phú Quốc', DATEADD(DAY, 28, CAST(GETDATE() AS DATE)), 3,  12000000, N'Ô tô',    0, N'Đặt vé máy bay trước'),
(N'Ngắm hoa tam giác mạch', N'Hà Giang', DATEADD(DAY, 5,  CAST(GETDATE() AS DATE)), 4,  4500000, N'Xe máy',   1, N'Đã đi, lưu lại làm kỷ niệm');
GO
