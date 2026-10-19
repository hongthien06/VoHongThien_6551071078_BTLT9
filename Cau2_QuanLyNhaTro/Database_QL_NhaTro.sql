-- =============================================
-- BÀI TẬP CHƯƠNG 11 - CÂU 2: QUẢN LÝ NHÀ TRỌ
-- =============================================

USE master;
GO

IF DB_ID('QL_NhaTro') IS NOT NULL
BEGIN
    ALTER DATABASE QL_NhaTro SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QL_NhaTro;
END
GO

CREATE DATABASE QL_NhaTro;
GO

USE QL_NhaTro;
GO

CREATE TABLE PhongTro (
    MaPhong INT IDENTITY(1,1) PRIMARY KEY,
    TenPhong NVARCHAR(20) NOT NULL,
    GiaThue DECIMAL(18,0) NOT NULL,
    TinhTrang NVARCHAR(20) NOT NULL DEFAULT N'Trống', -- Trống / Đang thuê
    CONSTRAINT CK_PhongTro_TinhTrang CHECK (TinhTrang IN (N'Trống', N'Đang thuê'))
);
GO

CREATE TABLE KhachThue (
    MaKhach INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    GioiTinh NVARCHAR(5),
    NgaySinh DATE,
    SoDienThoai VARCHAR(15),
    MaTinh INT,          -- code tỉnh/thành từ provinces.open-api.vn (NULL nếu nhập tay)
    TenTinh NVARCHAR(100),
    MaPhuongXa INT,      -- code phường/xã từ API (NULL nếu nhập tay)
    TenPhuongXa NVARCHAR(100),
    DaDangKyTamTru BIT NOT NULL DEFAULT 0,
    MaPhong INT NOT NULL,
    CONSTRAINT FK_KhachThue_PhongTro FOREIGN KEY (MaPhong) REFERENCES PhongTro(MaPhong)
);
GO

-- Dữ liệu mẫu
INSERT INTO PhongTro (TenPhong, GiaThue, TinhTrang) VALUES
(N'P01', 1500000, N'Đang thuê'),
(N'P02', 1600000, N'Đang thuê'),
(N'P03', 1800000, N'Đang thuê'),
(N'P04', 2000000, N'Đang thuê'),
(N'P05', 1700000, N'Đang thuê'),
(N'P06', 1900000, N'Trống');

INSERT INTO KhachThue (HoTen, GioiTinh, NgaySinh, SoDienThoai, MaTinh, TenTinh, MaPhuongXa, TenPhuongXa, DaDangKyTamTru, MaPhong) VALUES
(N'Nguyễn Văn',     N'Nam', '2004-02-02', '0912345600', 1,  N'Thành phố Hà Nội',    8,     N'Phường Ngọc Hà',   1, 3),
(N'Nguyễn Văn An',  N'Nam', '2004-05-12', '0912345678', 1,  N'Thành phố Hà Nội',    4,     N'Phường Ba Đình',   1, 3),
(N'Trần Thị Bình',  N'Nữ',  '2003-09-20', '0876543210', 37, N'Tỉnh Ninh Bình',      13285, N'Phường Phủ Lý',    0, 1),
(N'Lê Văn Cường',   N'Nam', '2002-01-15', '0901234567', 38, N'Tỉnh Thanh Hóa',      14758, N'Phường Hàm Rồng',  0, 2),
(N'Phạm Minh Đức',  N'Nam', '2001-11-30', '0987654321', 38, N'Tỉnh Thanh Hóa',      14797, N'Phường Hạc Thành', 1, 4),
(N'Hoàng Thị Dung', N'Nữ',  '2005-03-08', '0765432109', 24, N'Thành phố Bắc Ninh',  7210,  N'Phường Bắc Giang', 0, 5);
GO
