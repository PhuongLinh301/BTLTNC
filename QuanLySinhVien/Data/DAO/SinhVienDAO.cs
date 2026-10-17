using MySql.Data.MySqlClient;
using QuanLySinhVien.Data.DAL;
using QuanLySinhVien.Data.Entity;
using System.Data;

namespace QuanLySinhVien.Data.DAO;


public class SinhVienDAO
{
   
    private DataHelper Helper => new DataHelper();

    private const string SelectSinhVien = @"
        SELECT sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.Email,
               sv.SDT, sv.MaLop, sv.Diem, sv.TrangThai, lh.TenLop
        FROM sinhvien sv INNER JOIN lophoc lh ON sv.MaLop = lh.MaLop";

    public List<SinhVien> LayDanhSach()
    {
        using var reader = Helper.ExecuteReader(SelectSinhVien + " ORDER BY sv.MaSV");
        return DocDanhSach(reader);
    }

    
    public List<SinhVien> LayDanhSachPhiKetNoi()
    {
        DataTable bang = Helper.ExecuteQuery(SelectSinhVien + " ORDER BY sv.MaSV");
        var ketQua = new List<SinhVien>();
        var cacLop = new Dictionary<string, LopHoc>(StringComparer.OrdinalIgnoreCase);
        foreach (DataRow dong in bang.Rows)
        {
            string maLop = (string)dong["MaLop"];
            if (!cacLop.TryGetValue(maLop, out LopHoc? lop))
            {
                lop = new LopHoc { MaLop = maLop, TenLop = (string)dong["TenLop"] };
                cacLop.Add(maLop, lop);
            }
            var sv = new SinhVien
            {
                MaSV = (string)dong["MaSV"],
                HoTen = (string)dong["HoTen"],
                NgaySinh = Convert.ToDateTime(dong["NgaySinh"]),
                GioiTinh = (string)dong["GioiTinh"],
                Email = (string)dong["Email"],
                SDT = (string)dong["SDT"],
                LopHoc = lop,
                Diem = Convert.ToDouble(dong["Diem"]),
                TrangThai = (string)dong["TrangThai"]
            };
            ketQua.Add(sv);
            lop.SinhViens.Add(sv);
        }
        return ketQua;
    }

    
    public List<SinhVien> LayTheoMaLop(string maLop)
    {
        using var reader = Helper.ExecuteReader(SelectSinhVien + " WHERE sv.MaLop = @MaLop ORDER BY sv.MaSV",
            new MySqlParameter("@MaLop", MySqlDbType.VarChar) { Value = maLop });
        return DocDanhSach(reader);
    }

    public SinhVien? TimTheoMa(string maSV)
    {
        
        using var reader = Helper.ExecuteReader(SelectSinhVien + " WHERE sv.MaSV = @MaSV",
            new MySqlParameter("@MaSV", MySqlDbType.VarChar) { Value = maSV });
        return DocDanhSach(reader).FirstOrDefault();
    }

    public List<SinhVien> TimKiem(string tuKhoa, string maLop, decimal diemTu)
    {
        
        const string dieuKien = @"
            WHERE (@TuKhoa = '' OR LOCATE(@TuKhoa, sv.MaSV) > 0
                OR LOCATE(@TuKhoa, sv.HoTen) > 0 OR LOCATE(@TuKhoa, sv.Email) > 0
                OR LOCATE(@TuKhoa, sv.SDT) > 0)
              AND (@MaLop = '' OR sv.MaLop = @MaLop) AND sv.Diem >= @DiemTu
            ORDER BY sv.MaSV";
        using var reader = Helper.ExecuteReader(SelectSinhVien + dieuKien,
            new MySqlParameter("@TuKhoa", MySqlDbType.VarChar) { Value = tuKhoa },
            new MySqlParameter("@MaLop", MySqlDbType.VarChar) { Value = maLop },
            new MySqlParameter("@DiemTu", MySqlDbType.Decimal) { Value = diemTu });
        return DocDanhSach(reader);
    }

    public int AddSinhVien(SinhVien sv)
    {
        const string sql = @"
            INSERT INTO sinhvien (MaSV, HoTen, NgaySinh, GioiTinh, Email, SDT, MaLop, Diem, TrangThai)
            VALUES (@MaSV, @HoTen, @NgaySinh, @GioiTinh, @Email, @SDT, @MaLop, @Diem, @TrangThai)";
        return Helper.ExecuteNonQuery(sql, TaoThamSo(sv));
    }

    public int UpdateSinhVien(SinhVien sv)
    {
        
        const string sql = @"
            UPDATE sinhvien SET HoTen = @HoTen, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh,
                Email = @Email, SDT = @SDT, MaLop = @MaLop, Diem = @Diem, TrangThai = @TrangThai
            WHERE MaSV = @MaSV";
        return Helper.ExecuteNonQuery(sql, TaoThamSo(sv));
    }

    public int DeleteSinhVien(string maSV)
    {
        const string sql = "DELETE FROM sinhvien WHERE MaSV = @MaSV";
        return Helper.ExecuteNonQuery(sql,
            new MySqlParameter("@MaSV", MySqlDbType.VarChar) { Value = maSV });
    }

    private static MySqlParameter[] TaoThamSo(SinhVien sv)
    {
       
        return new MySqlParameter[]
        {
            new("@MaSV", MySqlDbType.VarChar) { Value = sv.MaSV },
            new("@HoTen", MySqlDbType.VarChar) { Value = sv.HoTen },
            new("@NgaySinh", MySqlDbType.Date) { Value = sv.NgaySinh.Date },
            new("@GioiTinh", MySqlDbType.VarChar) { Value = sv.GioiTinh },
            new("@Email", MySqlDbType.VarChar) { Value = sv.Email },
            new("@SDT", MySqlDbType.VarChar) { Value = sv.SDT },
            new("@MaLop", MySqlDbType.VarChar) { Value = sv.LopHoc?.MaLop ?? sv.MaLop },
            new("@Diem", MySqlDbType.Double) { Value = sv.Diem },
            new("@TrangThai", MySqlDbType.VarChar) { Value = sv.TrangThai }
        };
    }

    
    private static List<SinhVien> DocDanhSach(MySqlDataReader reader)
    {
        var ketQua = new List<SinhVien>();
        var cacLop = new Dictionary<string, LopHoc>(StringComparer.OrdinalIgnoreCase);
        while (reader.Read())
        {
            string maLop = (string)reader["MaLop"];
            if (!cacLop.TryGetValue(maLop, out LopHoc? lop))
            {
                lop = new LopHoc { MaLop = maLop, TenLop = (string)reader["TenLop"] };
                cacLop.Add(maLop, lop);
            }
            var sv = new SinhVien
            {
                MaSV = (string)reader["MaSV"],
                HoTen = (string)reader["HoTen"],
                NgaySinh = Convert.ToDateTime(reader["NgaySinh"]),
                GioiTinh = (string)reader["GioiTinh"],
                Email = (string)reader["Email"],
                SDT = (string)reader["SDT"],
                LopHoc = lop,
                Diem = Convert.ToDouble(reader["Diem"]),
                TrangThai = (string)reader["TrangThai"]
            };
            ketQua.Add(sv);
            lop.SinhViens.Add(sv);
        }
        return ketQua;
    }
}