using QuanLySinhVien.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuanLySinhVien.Data.DAL
{
    public class LopDAL
    {
        private readonly List<LopHoc> lopHocs = new();

        public LopDAL()
        {
            lopHocs.Add(new LopHoc
            {
                MaLop = "Lop 1",
                TenLop = "Lớp 1"
            });
            lopHocs.Add(new LopHoc
            {
                MaLop = "Lop 2",
                TenLop = "Lớp 2"
            });
        }

        public List<LopHoc> GetAllLopHoc() => lopHocs.ToList();
        public List<LopHoc> LayDanhSach() => lopHocs.ToList();

        public LopHoc? TimTheoMa(string maLop)
        {
            return lopHocs.FirstOrDefault(l => string.Equals(l.MaLop, maLop, StringComparison.OrdinalIgnoreCase));
        }

        public void Them(LopHoc lop)
        {
            lopHocs.Add(lop);
        }

        public void Sua(LopHoc cu, LopHoc moi)
        {
            cu.TenLop = moi.TenLop;
        }

        public void Xoa(LopHoc lop)
        {
            lopHocs.Remove(lop);
        }
    }
}