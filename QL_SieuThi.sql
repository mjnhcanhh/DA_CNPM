USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'QL_BigC')
BEGIN
    ALTER DATABASE QL_BigC SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QL_BigC;
END
GO

CREATE DATABASE QL_BigC
GO

USE QL_BigC
GO

--------------------------------------------------
-- I. KHAI BÁO CẤU TRÚC BẢNG (SCHEMA)
--------------------------------------------------

-- 1. BẢNG CHI NHÁNH
CREATE TABLE ChiNhanh (
    MaChiNhanh CHAR(10) PRIMARY KEY,
    TenChiNhanh NVARCHAR(30) UNIQUE NOT NULL,
    DiaChi NVARCHAR(100) UNIQUE NOT NULL
)
GO

-- 2. BẢNG QUẢN LÝ
CREATE TABLE QuanLy (
    MaQuanLy CHAR(10) PRIMARY KEY,
    TenQuanLy NVARCHAR(50) NOT NULL,
    NgaySinh DATE NOT NULL,
    DiaChi NVARCHAR(100) NOT NULL,
    SDT CHAR(13) UNIQUE,
    CCCD CHAR(15) UNIQUE,
    NgayVaoLam DATE NOT NULL,
    SoNgayLam INT CHECK (SoNgayLam > 0),
    MaChiNhanh CHAR(10),
    SoNhanVienQuanLy INT CHECK (SoNhanVienQuanLy > 0),
    CONSTRAINT FK_QuanLy_ChiNhanh
        FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh)
)
GO
-- 3. BẢNG NHÂN VIÊN
CREATE TABLE NhanVien (
    MaNhanVien CHAR(10) PRIMARY KEY,
    TenNhanVien NVARCHAR(50) NOT NULL,
    NgaySinh DATE NOT NULL,
    DiaChi NVARCHAR(100) NOT NULL,
    SDT CHAR(13) UNIQUE,
    CCCD CHAR(15) UNIQUE,
    NgayVaoLam DATE NOT NULL,
    SoNgayLam INT CHECK (SoNgayLam > 0),
    MaQuanLy CHAR(10),
    DaThanhToanLuong BIT DEFAULT 0,
    CONSTRAINT FK_NhanVien_QuanLy
        FOREIGN KEY (MaQuanLy) REFERENCES QuanLy(MaQuanLy)
)
GO
-- 4. BẢNG QL_NHANVIEN (Quan hệ N-N giữa NV và QL)
CREATE TABLE QL_NhanVien
(
    MaNhanVien char(10) NOT NULL,
    MaQuanLy char(10) NOT NULL,
    CONSTRAINT PK_QLNhanVien PRIMARY KEY (MaNhanVien, MaQuanLy),
    CONSTRAINT FK_QL_NhanVien FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien),
    CONSTRAINT FK_QL_QuanLy FOREIGN KEY (MaQuanLy) REFERENCES QuanLy(MaQuanLy)
)
GO

-- 5. BẢNG LƯƠNG
CREATE TABLE Luong (
    MaLuong CHAR(10) PRIMARY KEY,
    MaNhanVien CHAR(10),
    MaQuanLy CHAR(10),
    MaChiNhanh CHAR(10),
    Luong INT,
    Thue FLOAT,
    ChietKhau FLOAT,
    TongLuong INT,
    CONSTRAINT FK_Luong_NV FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien),
    CONSTRAINT FK_Luong_QL FOREIGN KEY (MaQuanLy) REFERENCES QuanLy(MaQuanLy),
    CONSTRAINT FK_Luong_CN FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh)
)
GO
-- 6. BẢNG NHÀ CUNG CẤP
CREATE TABLE NhaCungCap (
    MaNhaCungCap CHAR(10) PRIMARY KEY,
    TenNhaCungCap NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(100)
)
GO


-- 7. BẢNG HÀNG HÓA
CREATE TABLE HangHoa (
    MaHangHoa CHAR(10) PRIMARY KEY,
    TenHangHoa NVARCHAR(100) NOT NULL,
    DonGia INT CHECK (DonGia > 0),
    MaNhaCungCap CHAR(10),
    CONSTRAINT FK_HangHoa_NCC
        FOREIGN KEY (MaNhaCungCap) REFERENCES NhaCungCap(MaNhaCungCap)
)
GO
-- 8. BẢNG PHIẾU MUA HÀNG
CREATE TABLE PhieuMuaHang (
    MaPhieu CHAR(10) PRIMARY KEY,
    MaHangHoa CHAR(10),
    MaNhaCungCap CHAR(10),
    NgayDat DATE,
    NgayGiao DATE,
    SoLuong INT CHECK (SoLuong > 0),
    TongTien INT,
    Mota NVARCHAR(100),
    CONSTRAINT FK_PMH_HH FOREIGN KEY (MaHangHoa) REFERENCES HangHoa(MaHangHoa),
    CONSTRAINT FK_PMH_NCC FOREIGN KEY (MaNhaCungCap) REFERENCES NhaCungCap(MaNhaCungCap)
)

-- Thêm cột TrangThai cho bảng PhieuMuaHang (được nhắc đến ở cuối script gốc)
ALTER TABLE PhieuMuaHang
ADD TrangThai INT DEFAULT 0;
GO

-- 9. BẢNG LỊCH LÀM
CREATE TABLE LichLam (
    MaNhanVien CHAR(10),
    NgayLam DATE,
    CaLam NVARCHAR(50),
    GhiChu NVARCHAR(100),
    CONSTRAINT PK_LichLam PRIMARY KEY (MaNhanVien, NgayLam),
    CONSTRAINT FK_LichLam_NV FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
)
GO

-- 10. BẢNG PHẢN HỒI HỖ TRỢ
CREATE TABLE PhanHoiHoTro (
    MaPhanHoi INT IDENTITY PRIMARY KEY,
    MaNhanVien CHAR(10),
    TieuDe NVARCHAR(100),
    NoiDung NVARCHAR(MAX),
    ThoiGianGui DATETIME DEFAULT GETDATE(),
    TrangThai NVARCHAR(50) DEFAULT N'Mới',
    CONSTRAINT FK_PhanHoi_NV FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
)
GO
--11. Bảng tài khoản
CREATE TABLE TaiKhoan (
    MaNV CHAR(10) PRIMARY KEY,
    TenDangNhap NVARCHAR(50),
    MatKhau NVARCHAR(50),
    VaiTro NVARCHAR(20),
    CONSTRAINT FK_TaiKhoan_NV
        FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNhanVien)
)
GO

--12. Chấm công
CREATE TABLE ChamCong (
    MaNV CHAR(10),
    Ngay DATE,
    GioVao TIME,
    GioRa TIME,
    CONSTRAINT PK_ChamCong PRIMARY KEY (MaNV, Ngay),
    CONSTRAINT FK_ChamCong_NV
        FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNhanVien)
)
GO

-- 13. Chi tiết phiếu mua hàng
CREATE TABLE PhieuMuaHangChiTiet
(
    MaPhieu CHAR(10),
    MaHangHoa CHAR(10),
    SoLuong INT CHECK (SoLuong > 0),
    DonGia INT CHECK (DonGia > 0),
    ThanhTien AS (SoLuong * DonGia),

    CONSTRAINT PK_PMH_CT PRIMARY KEY (MaPhieu, MaHangHoa),
    CONSTRAINT FK_CT_PMH FOREIGN KEY (MaPhieu) REFERENCES PhieuMuaHang(MaPhieu),
    CONSTRAINT FK_CT_HH FOREIGN KEY (MaHangHoa) REFERENCES HangHoa(MaHangHoa)
);
GO


--------------------------------------------------
-- II. DỮ LIỆU MẪU (INSERT DATA)
--------------------------------------------------

-- 1. ChiNhanh
INSERT INTO ChiNhanh (MaChiNhanh, TenChiNhanh, DiaChi) VALUES
('CN01', N'Big C Trường Chinh', N'144 Trường Chinh , Tân Phú, TPHCM')
GO

-- 2. QuanLy
INSERT INTO QuanLy (MaQuanLy, TenQuanLy, NgaySinh, DiaChi, SDT, CCCD, NgayVaoLam, SoNgayLam, MaChiNhanh, SoNhanVienQuanLy) VALUES
('QL01', N'Hồ Thanh Hải', '2003-08-27', N'55a Nguyễn Đỗ Cung', '0384631254', '052203011677', '2021-05-15', 28, 'CN01', 21)
GO

-- 3. NhanVien
INSERT INTO NhanVien VALUES
('NV01', N'Nguyễn Thị Mai', '2001-05-10', N'Hóc Môn', '0901234567', '111111111111', '2023-01-15', 22, 'QL01', 0),
('NV02', N'Trần Văn Hùng', '1999-12-01', N'Tân Bình', '0901234568', '222222222222', '2022-08-20', 18, 'QL01', 0),
('NV03', N'Lê Thu Phương', '2002-03-20', N'Phú Nhuận', '0901234569', '333333333333', '2024-02-10', 25, 'QL01', 0),
('NV04', N'Phạm Minh Đức', '1998-07-25', N'Gò Vấp', '0901234570', '444444444444', '2021-11-05', 20, 'QL01', 0),
('NV05', N'Đặng Ngọc An', '2000-09-18', N'Bình Thạnh', '0901234571', '555555555555', '2023-05-01', 15, 'QL01', 0),
('NV06', N'Hồ Thanh Trí', '2001-02-14', N'Tân Phú', '0901234572', '666666666666', '2022-03-02', 22, 'QL01', 0)

-- Thêm tài khoản Admin vào bảng NhanVien
INSERT INTO NhanVien (
    MaNhanVien, TenNhanVien, NgaySinh, DiaChi,
    SDT, CCCD, NgayVaoLam, SoNgayLam, MaQuanLy, DaThanhToanLuong
) VALUES (
    'AD01', N'Quản trị hệ thống', '1990-01-01', N'Văn phòng Big C',
    '000000000000', '000000000000', GETDATE(), 1, 'QL01', 0
)
GO

-- Tài khoản
INSERT INTO TaiKhoan VALUES
('AD01', 'admin', '123', 'Admin'),
('NV01', 'nv01', '123', 'NhanVien'),
('NV02', 'nv02', '123', 'NhanVien'),
('NV03', 'nv03', '123', 'NhanVien'),
('NV04', 'nv04', '123', 'NhanVien'),
('NV05', 'nv05', '123', 'NhanVien'),
('NV06', 'nv06', '123', 'NhanVien')
GO

-- 4. QL_NhanVien (Relationship between NV and QL)
INSERT INTO QL_NhanVien (MaNhanVien, MaQuanLy) VALUES
('NV01', 'QL01'),
('NV02', 'QL01'),
('NV03', 'QL01'),
('NV04', 'QL01'),
('NV05', 'QL01'),
('NV06', 'QL01'),
('AD01', 'QL01')
GO

-- 5. NhaCungCap
INSERT INTO NhaCungCap (MaNhaCungCap, TenNhaCungCap, DiaChi) VALUES
('NC01', N'RichsHai', N'98 Phạm Văn Đồng'),
('NC02', N'NhatNhatThanh', N'66 Nguyễn Kiệm'),
('NC03', N'TripleLike', N'56e Ung Văn Khiêm'),
('NC04', N'TanKhaiHa', N'88 Nguyễn Văn Trỗi'),
('NC05', N'Mega', N'66a Tân Thế Giới')
GO

-- 6. HangHoa
INSERT INTO HangHoa (MaHangHoa, TenHangHoa, DonGia, MaNhaCungCap) VALUES
('HH01', N'Kem Richs', 28000, 'NC01'),
('HH02', N'Sữa Đặc Ngôi Sao', 20000, 'NC02'),
('HH03', N'Khăn Giấy Gấu', 15000, 'NC03'),
('HH04', N'Sữa Tươi Vinamilk', 23000, 'NC04'),
('HH05', N'Sa Tế Ớt', 8000, 'NC05'),
('HH06', N'Chanh Giấy', 23000, 'NC05'),
('HH07', N'Muối Hầm', 23000, 'NC05'),
('HH08', N'Bánh Oreo Cay', 23000, 'NC05')
GO

-- 7. PhieuMuaHang
INSERT INTO PhieuMuaHang (MaPhieu, MaHangHoa, MaNhaCungCap, NgayDat, NgayGiao, SoLuong, TongTien, Mota, TrangThai) VALUES
('PMH001', 'HH01', 'NC01', '2025-11-01', '2025-11-05', 100, 2800000, N'Đơn hàng đầu tháng', 1), -- Đã thêm TrangThai
('PMH002', 'HH04', 'NC04', '2025-11-03', '2025-11-07', 250, 5750000, N'Bổ sung hàng sữa tươi', 0),
('PMH003', 'HH05', 'NC05', '2025-11-10', '2025-11-12', 50, 400000, N'Đơn hàng gấp', 1),
('PMH004', 'HH02', 'NC02', '2025-11-15', '2025-11-18', 120, 2400000, N'Sữa đặc', 0),
('PMH005', 'HH08', 'NC05', '2025-11-20', '2025-11-25', 80, 1840000, N'Bánh kẹo', 1)
GO

-- 8. PhieuMuaHangChiTiet (Thêm dữ liệu cho bảng chi tiết)
INSERT INTO PhieuMuaHangChiTiet (MaPhieu, MaHangHoa, SoLuong, DonGia) VALUES
('PMH001', 'HH01', 100, 28000),
('PMH002', 'HH04', 250, 23000),
('PMH003', 'HH05', 50, 8000),
('PMH004', 'HH02', 120, 20000),
('PMH005', 'HH08', 80, 23000)
GO

-- 9. LichLam
INSERT INTO LichLam (MaNhanVien, NgayLam, CaLam, GhiChu) VALUES
('NV01', '2025-11-25', N'Sáng (7h-12h)', N'Đã làm'),
('NV01', '2025-11-26', N'Chiều (12h-17h)', N'Đã làm'),
('NV02', '2025-11-25', N'Hành chính (8h-17h)', N'Nghỉ có phép'),
('NV03', '2025-11-27', N'Tối (17h-22h)', N'Đã làm'),
('NV04', '2025-11-28', N'Sáng (7h-12h)', N'Đã làm'),
('NV05', '2025-11-28', N'Chiều (12h-17h)', N'Đã làm'),
('NV06', '2025-11-29', N'Hành chính (8h-17h)', N'Đã làm')
GO

-- 10. PhanHoiHoTro
INSERT INTO PhanHoiHoTro (MaNhanVien, TieuDe, NoiDung) VALUES
('NV01', N'Lỗi Chấm Công', N'Không chấm công được ca sáng ngày 25/11. Hệ thống bị đơ.'),
('NV03', N'Yêu cầu Hỗ trợ', N'Cần hướng dẫn sử dụng chức năng quản lý hàng tồn kho.')
GO

-- 11. ChamCong (Thêm dữ liệu để truy vấn tính tổng giờ làm chạy được)
INSERT INTO ChamCong (MaNV, Ngay, GioVao, GioRa) VALUES
('NV01', '2025-11-25', '07:00:00', '12:00:00'), -- 5 giờ
('NV01', '2025-11-26', '12:00:00', '17:00:00'), -- 5 giờ
('NV02', '2025-11-27', '08:00:00', '16:00:00')  -- 8 giờ
GO


/* =====================================================
    III. TRUY VẤN TÍNH TỔNG GIỜ LÀM
    (Ví dụ tính tổng giờ làm của NV01)
===================================================== */
DECLARE @MaNV CHAR(10) = 'NV01'

SELECT
    ISNULL(
        SUM(
            CASE
                -- Trường hợp ca trong ngày (GioRa >= GioVao)
                WHEN GioRa >= GioVao
                    THEN DATEDIFF(MINUTE, GioVao, GioRa)
                -- Trường hợp ca qua đêm (GioRa < GioVao)
                ELSE
                    DATEDIFF(MINUTE, GioVao, '23:59:59')
                    + DATEDIFF(MINUTE, '00:00:00', GioRa)
            END
        ) / 60.0,
    0
    ) AS TongGioLam
FROM ChamCong
WHERE MaNV = @MaNV
    AND GioRa IS NOT NULL
GO

/* =====================================================
    IV. TRUY VẤN THÔNG TIN PHIẾU MUA HÀNG
===================================================== */
SELECT PMH.MaPhieu, NCC.TenNhaCungCap, PMH.NgayDat, PMH.NgayGiao, SUM(CT.SoLuong) AS TongSoLuong, PMH.TongTien, PMH.MoTa, PMH.TrangThai
FROM PhieuMuaHang PMH
JOIN NhaCungCap NCC ON PMH.MaNhaCungCap = NCC.MaNhaCungCap
LEFT JOIN PhieuMuaHangChiTiet CT ON PMH.MaPhieu = CT.MaPhieu
GROUP BY PMH.MaPhieu, NCC.TenNhaCungCap, PMH.NgayDat, PMH.NgayGiao, PMH.TongTien, PMH.MoTa, PMH.TrangThai
ORDER BY PMH.MaPhieu
GO

UPDATE PhieuMuaHang 
SET TrangThai = 1
WHERE MaPhieu = @MaPhieu;

-- Them nhân viên
GO
CREATE PROCEDURE sp_ThemNhanVienVaTaiKhoan
(
    @MaNV CHAR(10),
    @TenNV NVARCHAR(50),
    @NgaySinh DATE,
    @DiaChi NVARCHAR(100),
    @SDT CHAR(13),
    @CCCD CHAR(15),
    @NgayVaoLam DATE,
    @SoNgayLam INT,
    @MaQuanLy CHAR(10),

    @TenDangNhap NVARCHAR(50),
    @MatKhau NVARCHAR(50),
    @VaiTro NVARCHAR(20)
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRAN

        INSERT INTO NhanVien
        (
            MaNhanVien, TenNhanVien, NgaySinh, DiaChi,
            SDT, CCCD, NgayVaoLam, SoNgayLam, MaQuanLy
        )
        VALUES
        (
            @MaNV, @TenNV, @NgaySinh, @DiaChi,
            @SDT, @CCCD, @NgayVaoLam, @SoNgayLam, @MaQuanLy
        )

        INSERT INTO TaiKhoan
        (
            MaNV, TenDangNhap, MatKhau, VaiTro
        )
        VALUES
        (
            @MaNV, @TenDangNhap, @MatKhau, @VaiTro
        )

        COMMIT TRAN
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN
        THROW
    END CATCH
END
GO


USE QL_BigC
GO

CREATE PROCEDURE dbo.sp_ThemNhanVienVaTaiKhoan
(
    @MaNV CHAR(10),
    @TenNV NVARCHAR(50),
    @NgaySinh DATE,
    @DiaChi NVARCHAR(100),
    @SDT CHAR(13),
    @CCCD CHAR(15),
    @NgayVaoLam DATE,
    @SoNgayLam INT,
    @MaQuanLy CHAR(10),

    @TenDangNhap NVARCHAR(50),
    @MatKhau NVARCHAR(50),
    @VaiTro NVARCHAR(20)
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRAN;

        IF EXISTS (SELECT 1 FROM NhanVien WHERE MaNhanVien = @MaNV)
            RAISERROR (N'Mã nhân viên đã tồn tại', 16, 1);

        IF EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap)
            RAISERROR (N'Tên đăng nhập đã tồn tại', 16, 1);

        INSERT INTO NhanVien
        (
            MaNhanVien, TenNhanVien, NgaySinh, DiaChi,
            SDT, CCCD, NgayVaoLam, SoNgayLam, MaQuanLy
        )
        VALUES
        (
            @MaNV, @TenNV, @NgaySinh, @DiaChi,
            @SDT, @CCCD, @NgayVaoLam, @SoNgayLam, @MaQuanLy
        );

        INSERT INTO TaiKhoan
        (
            MaNV, TenDangNhap, MatKhau, VaiTro
        )
        VALUES
        (
            @MaNV, @TenDangNhap, @MatKhau, @VaiTro
        );

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO


USE QL_BigC
GO

SELECT name FROM sys.procedures;

--TỰ CẤP TÀI KHOẢN
ALTER PROCEDURE dbo.sp_ThemNhanVienVaTaiKhoan
(
    @MaNV CHAR(10),
    @TenNV NVARCHAR(50),
    @NgaySinh DATE,
    @DiaChi NVARCHAR(100),
    @SDT CHAR(13),
    @CCCD CHAR(15),
    @NgayVaoLam DATE,
    @SoNgayLam INT,
    @MaQuanLy CHAR(10)
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @TenDangNhap NVARCHAR(50);
    DECLARE @MatKhau NVARCHAR(50) = '123';
    DECLARE @VaiTro NVARCHAR(20) = 'NhanVien';

    -- username = lower(MaNV)
    SET @TenDangNhap = LOWER(RTRIM(@MaNV));

    BEGIN TRY
        BEGIN TRAN;

        IF EXISTS (SELECT 1 FROM NhanVien WHERE MaNhanVien = @MaNV)
            THROW 50001, N'Mã nhân viên đã tồn tại', 1;

        IF EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap)
            THROW 50002, N'Tên đăng nhập đã tồn tại', 1;

        INSERT INTO NhanVien
        (
            MaNhanVien, TenNhanVien, NgaySinh, DiaChi,
            SDT, CCCD, NgayVaoLam, SoNgayLam, MaQuanLy
        )
        VALUES
        (
            @MaNV, @TenNV, @NgaySinh, @DiaChi,
            @SDT, @CCCD, @NgayVaoLam, @SoNgayLam, @MaQuanLy
        );

        INSERT INTO TaiKhoan
        (
            MaNV, TenDangNhap, MatKhau, VaiTro
        )
        VALUES
        (
            @MaNV, @TenDangNhap, @MatKhau, @VaiTro
        );

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO

--Kiểm tra
EXEC dbo.sp_ThemNhanVienVaTaiKhoan
    'NV203',
    N'Nhân viên auto',
    '2002-06-12',
    N'TPHCM',
    '0911111111',
    '999999999997',
    '2025-01-01',
    1,
    'QL01';

SELECT * FROM TaiKhoan WHERE MaNV = 'NV203';  

