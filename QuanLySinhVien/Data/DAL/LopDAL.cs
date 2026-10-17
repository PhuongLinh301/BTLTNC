using MySql.Data.MySqlClient;
using QuanLySinhVien.Data.Entity;
using System;
using System.Collections.Generic;

namespace QuanLySinhVien.Data.DAL
{
    public class LopDAL
    {
        public List<LopHoc> GetAllLopHoc() => LayDanhSach();

        public List<LopHoc> LayDanhSach()
        {
            var ds = new List<LopHoc>();
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();

            const string sql = "SELECT MaLop, TenLop FROM LopHoc ORDER BY MaLop";
            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ds.Add(new LopHoc
                {
                    MaLop = reader.GetString("MaLop"),
                    TenLop = reader.GetString("TenLop")
                });
            }
            return ds;
        }

        public LopHoc? TimTheoMa(string maLop)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();

            const string sql = "SELECT MaLop, TenLop FROM LopHoc WHERE MaLop = @MaLop";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MaLop", maLop);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new LopHoc
                {
                    MaLop = reader.GetString("MaLop"),
                    TenLop = reader.GetString("TenLop")
                };
            }
            return null;
        }

        public void Them(LopHoc lop)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();

            const string sql = "INSERT INTO LopHoc (MaLop, TenLop) VALUES (@MaLop, @TenLop)";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MaLop", lop.MaLop);
            cmd.Parameters.AddWithValue("@TenLop", lop.TenLop);

            cmd.ExecuteNonQuery();
        }

        public void Sua(LopHoc cu, LopHoc moi)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();

            const string sql = "UPDATE LopHoc SET TenLop = @TenLop WHERE MaLop = @MaLop";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@TenLop", moi.TenLop);
            cmd.Parameters.AddWithValue("@MaLop", cu.MaLop);

            cmd.ExecuteNonQuery();
        }

        public void Xoa(LopHoc lop)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();

            const string sql = "DELETE FROM LopHoc WHERE MaLop = @MaLop";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MaLop", lop.MaLop);

            cmd.ExecuteNonQuery();
        }
    }
}