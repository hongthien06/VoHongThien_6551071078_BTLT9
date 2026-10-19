-- =============================================
-- BÀI TẬP CHƯƠNG 11 - CÂU 1: SỔ CHI TIÊU CÁ NHÂN
-- =============================================

IF DB_ID('QL_ChiTieu') IS NOT NULL
BEGIN
    ALTER DATABASE QL_ChiTieu SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QL_ChiTieu;
END
GO

CREATE DATABASE QL_ChiTieu;
GO

USE QL_ChiTieu;
GO

CREATE TABLE ChiTieu (
    MaCT INT IDENTITY(1,1) PRIMARY KEY,
    NgayChi DATE NOT NULL,
    NoiDung NVARCHAR(100) NOT NULL,
    LoaiChi NVARCHAR(30) NOT NULL,
    SoTien DECIMAL(18,0) NOT NULL,
    GhiChu NVARCHAR(200) NULL
);
GO

-- Dữ liệu mẫu
INSERT INTO ChiTieu (NgayChi, NoiDung, LoaiChi, SoTien, GhiChu) VALUES
('2026-10-01', N'Cơm trưa', N'Ăn uống', 35000, NULL),
('2026-10-02', N'Xăng xe', N'Đi lại', 60000, NULL),
('2026-10-03', N'Photo giáo trình', N'Học tập', 25000, NULL),
('2026-10-04', N'Xem phim', N'Giải trí', 90000, N'Đi cùng bạn'),
('2026-10-05', N'Trà sữa', N'Ăn uống', 40000, NULL),
('2026-10-06', N'Mua sắm', N'Khác', 150000, N'Quần áo'),
('2026-10-07', N'Tiền điện', N'Khác', 500000, N'Hóa đơn tháng 9'),
('2026-10-08', N'Ăn sáng', N'Ăn uống', 25000, NULL),
('2026-10-09', N'Vé xe buýt', N'Đi lại', 30000, NULL),
('2026-10-10', N'Mua sách tham khảo', N'Học tập', 120000, NULL),
('2026-10-12', N'Cà phê với bạn', N'Ăn uống', 45000, NULL),
('2026-09-28', N'Chơi game', N'Giải trí', 50000, N'Chi tháng trước');
GO
