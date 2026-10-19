-- =============================================
-- CÂU 5: QUẢN LÝ ĐẶT TOUR (QL_DatTour)
-- =============================================

IF DB_ID('QL_DatTour') IS NULL
    CREATE DATABASE QL_DatTour;
GO

USE QL_DatTour;
GO

IF OBJECT_ID('DatTour', 'U') IS NOT NULL DROP TABLE DatTour;
IF OBJECT_ID('KhachHang', 'U') IS NOT NULL DROP TABLE KhachHang;
IF OBJECT_ID('Tour', 'U') IS NOT NULL DROP TABLE Tour;
GO

CREATE TABLE Tour (
    MaTour INT IDENTITY(1,1) PRIMARY KEY,
    TenTour NVARCHAR(100) NOT NULL,
    DiemDen NVARCHAR(50),
    NgoaiTe CHAR(3) NOT NULL CHECK (NgoaiTe IN ('THB', 'KRW', 'JPY', 'USD')),
    GiaNgoaiTe DECIMAL(18,2) NOT NULL,
    NgayKhoiHanh DATE,
    SoChoToiDa INT NOT NULL
);
GO

CREATE TABLE KhachHang (
    MaKH INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15),
    Email NVARCHAR(100)
);
GO

CREATE TABLE DatTour (
    MaDat INT IDENTITY(1,1) PRIMARY KEY,
    MaKH INT NOT NULL FOREIGN KEY REFERENCES KhachHang(MaKH),
    MaTour INT NOT NULL FOREIGN KEY REFERENCES Tour(MaTour),
    SoNguoi INT NOT NULL,
    NgayDat DATETIME NOT NULL,
    TyGia DECIMAL(18,4) NOT NULL,   -- tỷ giá tại thời điểm đặt
    TongTienVND DECIMAL(18,0) NOT NULL
);
GO

-- Dữ liệu mẫu
INSERT INTO Tour (TenTour, DiemDen, NgoaiTe, GiaNgoaiTe, NgayKhoiHanh, SoChoToiDa) VALUES
(N'Bangkok - Pattaya 5N4Đ', N'Thái Lan', 'THB', 9500, '2026-11-15', 20),
(N'Seoul - Nami 5N4Đ', N'Hàn Quốc', 'KRW', 1250000, '2026-11-22', 15),
(N'Tokyo - Phú Sĩ 5N4Đ', N'Nhật Bản', 'JPY', 85000, '2026-12-05', 12),
(N'Phuket - Đảo Phi Phi 4N3Đ', N'Thái Lan', 'THB', 8200, '2026-12-12', 18),
(N'Los Angeles - Las Vegas 7N6Đ', N'Hoa Kỳ', 'USD', 1450, '2027-01-10', 10);

INSERT INTO KhachHang (HoTen, SoDienThoai, Email) VALUES
(N'Trần Thị Mai', '0987654321', N'mai.tran@gmail.com'),
(N'Lê Văn An', '0912345678', N'an.le@gmail.com'),
(N'Phạm Minh Hùng', '0903112233', N'hung.pham@gmail.com'),
(N'Hoàng Thu Thảo', '0977889900', N'thao.hoang@gmail.com');

INSERT INTO DatTour (MaKH, MaTour, SoNguoi, NgayDat, TyGia, TongTienVND) VALUES
(1, 1, 3, '2026-10-01 09:00', 778.0000, 22173000),
(2, 2, 2, '2026-10-02 10:30', 18.5000, 46250000),
(3, 3, 4, '2026-10-05 14:00', 170.0000, 57800000);
GO
