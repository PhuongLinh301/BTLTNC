CREATE DATABASE IF NOT EXISTS quanlysinhvien CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE quanlysinhvien;

-- 1. Bảng Lớp học
CREATE TABLE IF NOT EXISTS LopHoc (
    MaLop VARCHAR(20) PRIMARY KEY,
    TenLop VARCHAR(100) NOT NULL
);

-- 2. Bảng Sinh viên
CREATE TABLE IF NOT EXISTS SinhVien (
    MaSV VARCHAR(20) PRIMARY KEY,
    HoTen VARCHAR(100) NOT NULL,
    NgaySinh DATE NOT NULL,
    GioiTinh VARCHAR(10) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    DienThoai VARCHAR(20),
    Diem DOUBLE NOT NULL DEFAULT 0.0,
    MaLop VARCHAR(20) NOT NULL,
    TrangThai VARCHAR(50) DEFAULT 'Đang học',
    FOREIGN KEY (MaLop) REFERENCES LopHoc(MaLop) ON DELETE RESTRICT ON UPDATE CASCADE
);

-- Dữ liệu mẫu
INSERT INTO LopHoc (MaLop, TenLop) VALUES 
('Lop 1', 'Công nghệ thông tin 1'),
('Lop 2', 'Kỹ thuật phần mềm 1'),
('Lop 3', 'Hệ thống thông tin 1')
ON DUPLICATE KEY UPDATE TenLop = VALUES(TenLop);

INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, Email, DienThoai, Diem, MaLop, TrangThai) VALUES
('SV000001', 'Nguyễn Văn A', '2003-05-15', 'Nam', 'vana@gmail.com', '0901234567', 8.5, 'Lop 1', 'Đang học'),
('SV000002', 'Trần Thị B', '2003-10-20', 'Nữ', 'thib@gmail.com', '0901234568', 7.0, 'Lop 2', 'Đang học'),
('SV000003', 'Lê Văn C', '2002-12-01', 'Nam', 'vanc@gmail.com', '0901234569', 9.0, 'Lop 1', 'Đang học')
ON DUPLICATE KEY UPDATE HoTen = VALUES(HoTen);
