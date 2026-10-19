-- =============================================
-- CAU 4 (BAI TAP CHUONG 11): BAN HANG QUAN TRA SUA - QL_TraSua
-- =============================================

IF DB_ID('QL_TraSua') IS NOT NULL
BEGIN
    ALTER DATABASE QL_TraSua SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QL_TraSua;
END
GO

CREATE DATABASE QL_TraSua;
GO

USE QL_TraSua;
GO

CREATE TABLE DanhMucMon (
    MaDM INT IDENTITY(1,1) PRIMARY KEY,
    TenDM NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE MonUong (
    MaMon INT IDENTITY(1,1) PRIMARY KEY,
    TenMon NVARCHAR(100) NOT NULL,
    GiaBan DECIMAL(18,0) NOT NULL,
    DangBan BIT NOT NULL DEFAULT 1,
    MaDM INT NOT NULL FOREIGN KEY REFERENCES DanhMucMon(MaDM)
);
GO

CREATE TABLE HoaDon (
    MaHD INT IDENTITY(1,1) PRIMARY KEY,
    NgayLap DATETIME NOT NULL,
    TongTien DECIMAL(18,0) NOT NULL,
    HinhThucTT NVARCHAR(20) NOT NULL, -- Tiền mặt / Chuyển khoản
    DaThanhToan BIT NOT NULL
);
GO

CREATE TABLE ChiTietHoaDon (
    MaHD INT NOT NULL FOREIGN KEY REFERENCES HoaDon(MaHD),
    MaMon INT NOT NULL FOREIGN KEY REFERENCES MonUong(MaMon),
    SoLy INT NOT NULL,
    DonGia DECIMAL(18,0) NOT NULL,
    CONSTRAINT PK_ChiTietHoaDon PRIMARY KEY (MaHD, MaMon)
);
GO

-- Dữ liệu mẫu: 4 danh mục
INSERT INTO DanhMucMon (TenDM) VALUES
(N'Trà sữa'),
(N'Trà trái cây'),
(N'Topping'),
(N'Đồ ăn vặt');

-- 16 món
INSERT INTO MonUong (TenMon, GiaBan, DangBan, MaDM) VALUES
(N'Trà sữa trân châu', 35000, 1, 1),
(N'Trà sữa matcha', 40000, 1, 1),
(N'Hồng trà sữa', 32000, 1, 1),
(N'Trà sữa khoai môn', 38000, 1, 1),
(N'Trà đào cam sả', 38000, 1, 2),
(N'Trà vải hoa hồng', 36000, 1, 2),
(N'Trà chanh dây', 30000, 1, 2),
(N'Trà dâu tằm', 34000, 0, 2),
(N'Trân châu trắng', 5000, 1, 3),
(N'Trân châu đen', 5000, 1, 3),
(N'Thạch dừa', 6000, 1, 3),
(N'Pudding trứng', 8000, 1, 3),
(N'Khoai tây chiên', 25000, 1, 4),
(N'Gà popcorn', 30000, 1, 4),
(N'Bánh tráng trộn', 20000, 1, 4),
(N'Xúc xích nướng', 15000, 0, 4);
GO
