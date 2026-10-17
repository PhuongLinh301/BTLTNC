using MySql.Data.MySqlClient;
using QuanLySinhVien.Data.Entity;
using System;
using System.Collections.Generic;

namespace QuanLySinhVien.Data.DAL
{
    public class SinhVienDAL
    {
        public SinhVienDAL() { }

        public SinhVienDAL(LopHoc? lop) { }

        public List<SinhVien> GetAllSinhVien() => LayDanhSach();

        public List<SinhVien> LayDanhSach()
        {
            var ds = new List<SinhVien>();
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();

            const string sql = @"SELECT sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.Email, 
                                        sv.DienThoai, sv.Diem, sv.MaLop, sv.TrangThai, l.TenLop
                                 FROM SinhVien sv
                                 LEFT JOIN LopHoc l ON sv.MaLop = l.MaLop
                                 ORDER BY sv.MaSV";
            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var sv = DocSinhVien(reader);
                ds.Add(sv);
            }
            return ds;
        }

        public SinhVien? GetSinhVienById(string maSV) => TimTheoMa(maSV);

        public SinhVien? TimTheoMa(string maSV)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();

            const string sql = @"SELECT sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.Email, 
                                        sv.DienThoai, sv.Diem, sv.MaLop, sv.TrangThai, l.TenLop
                                 FROM SinhVien sv
                                 LEFT JOIN LopHoc l ON sv.MaLop = l.MaLop
                                 WHERE sv.MaSV = @MaSV";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MaSV", maSV);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return DocSinhVien(reader);
            }
            return null;
        }

        public void AddSinhVien(SinhVien sv) => Them(sv);

        public void Them(SinhVien sv)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();

            const string sql = @"INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, Email, DienThoai, Diem, MaLop, TrangThai)
                                 VALUES (@MaSV, @HoTen, @NgaySinh, @GioiTinh, @Email, @DienThoai, @Diem, @MaLop, @TrangThai)";
            using var cmd = new MySqlCommand(sql, conn);
            GanThamSo(cmd, sv);

            cmd.ExecuteNonQuery();
        }

        public bool UpdateSinhVien(SinhVien sv)
        {
            Sua(sv, sv);
            return true;
        }

        public void Sua(SinhVien cu, SinhVien moi)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();

            const string sql = @"UPDATE SinhVien 
                                 SET HoTen = @HoTen, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh,
                                     Email = @Email, DienThoai = @DienThoai, Diem = @Diem, 
                                     MaLop = @MaLop, TrangThai = @TrangThai
                                 WHERE MaSV = @MaSV";
            using var cmd = new MySqlCommand(sql, conn);
            GanThamSo(cmd, moi);

            cmd.ExecuteNonQuery();
        }

        public void Xoa(SinhVien sv)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();

            const string sql = "DELETE FROM SinhVien WHERE MaSV = @MaSV";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MaSV", sv.MaSV);

            cmd.ExecuteNonQuery();
        }

        private static SinhVien DocSinhVien(MySqlDataReader reader)
        {
            string maLop = reader.GetString("MaLop");
            string tenLop = reader.IsDBNull(reader.GetOrdinal("TenLop")) ? "" : reader.GetString("TenLop");

            return new SinhVien
            {
                MaSV = reader.GetString("MaSV"),
                HoTen = reader.GetString("HoTen"),
                NgaySinh = reader.GetDateTime("NgaySinh"),
                GioiTinh = reader.GetString("GioiTinh"),
                Email = reader.GetString("Email"),
                DienThoai = reader.IsDBNull(reader.GetOrdinal("DienThoai")) ? "" : reader.GetString("DienThoai"),
                Diem = reader.GetDouble("Diem"),
                MaLop = maLop,
                TenLop = tenLop,
                TrangThai = reader.GetString("TrangThai"),
                LopHoc = new LopHoc(maLop, tenLop)
            };
        }

        private static void GanThamSo(MySqlCommand cmd, SinhVien sv)
        {
            cmd.Parameters.AddWithValue("@MaSV", sv.MaSV);
            cmd.Parameters.AddWithValue("@HoTen", sv.HoTen);
            cmd.Parameters.AddWithValue("@NgaySinh", sv.NgaySinh.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@GioiTinh", sv.GioiTinh);
            cmd.Parameters.AddWithValue("@Email", sv.Email);
            cmd.Parameters.AddWithValue("@DienThoai", string.IsNullOrEmpty(sv.DienThoai) ? DBNull.Value : sv.DienThoai);
            cmd.Parameters.AddWithValue("@Diem", sv.Diem);
            cmd.Parameters.AddWithValue("@MaLop", sv.MaLop);
            cmd.Parameters.AddWithValue("@TrangThai", sv.TrangThai);
        }
    }
}