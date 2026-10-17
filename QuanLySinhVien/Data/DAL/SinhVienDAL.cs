using QuanLySinhVien.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLySinhVien.Data.DAL
{
    public class SinhVienDAL
    {
        private readonly List<SinhVien> sinhViens = new();

        public SinhVienDAL()
        {
            sinhViens.Add(new SinhVien
            {
                MaSV = "SV000001",
                HoTen = "Nguyen Van A",
                NgaySinh = new DateTime(2000, 1, 1),
                GioiTinh = "Nam",
                Email = "a@gmail.com",
                DienThoai = "0901234567",
                Diem = 8.5,
                MaLop = "Lop 1",
                TrangThai = "Đang học"
            });
            sinhViens.Add(new SinhVien
            {
                MaSV = "SV000002",
                HoTen = "Tran Thi B",
                NgaySinh = new DateTime(2001, 2, 2),
                GioiTinh = "Nữ",
                Email = "b@gmail.com",
                DienThoai = "0901234568",
                Diem = 7.0,
                MaLop = "Lop 2",
                TrangThai = "Đang học"
            });
            sinhViens.Add(new SinhVien
            {
                MaSV = "SV000003",
                HoTen = "Le Van C",
                NgaySinh = new DateTime(2002, 3, 3),
                GioiTinh = "Nam",
                Email = "c@gmail.com",
                DienThoai = "0901234569",
                Diem = 9.0,
                MaLop = "Lop 1",
                TrangThai = "Đang học"
            });
        }

        public SinhVienDAL(LopHoc? lop) : this()
        {
            if (lop != null && sinhViens.Count > 0)
            {
                sinhViens[0].LopHoc = lop;
            }
        }

        public List<SinhVien> GetAllSinhVien() => sinhViens.ToList();
        public List<SinhVien> LayDanhSach() => sinhViens.ToList();

        public SinhVien? GetSinhVienById(string maSV) => TimTheoMa(maSV);
        public SinhVien? TimTheoMa(string maSV)
        {
            return sinhViens.FirstOrDefault(s => string.Equals(s.MaSV, maSV, StringComparison.OrdinalIgnoreCase));
        }

        public void AddSinhVien(SinhVien sv) => Them(sv);
        public void Them(SinhVien sv)
        {
            sinhViens.Add(sv);
        }

        public bool UpdateSinhVien(SinhVien sv)
        {
            var svToUpdate = TimTheoMa(sv.MaSV);
            if (svToUpdate != null)
            {
                Sua(svToUpdate, sv);
                return true;
            }
            return false;
        }

        public void Sua(SinhVien cu, SinhVien moi)
        {
            cu.HoTen = moi.HoTen;
            cu.NgaySinh = moi.NgaySinh;
            cu.GioiTinh = moi.GioiTinh;
            cu.Email = moi.Email;
            cu.DienThoai = moi.DienThoai;
            cu.Diem = moi.Diem;
            cu.MaLop = moi.MaLop;
            cu.TrangThai = moi.TrangThai;
            cu.LopHoc = moi.LopHoc;
        }

        public void Xoa(SinhVien sv)
        {
            sinhViens.Remove(sv);
        }
    }
}