using QuanLySinhVien.Data.DAL;
using QuanLySinhVien.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace QuanLySinhVien.BUL;

public class SinhVienBUL
{
    private readonly SinhVienDAL dal;
    private readonly LopBus lopBus;

    public SinhVienBUL() : this(new LopBus()) { }

    public SinhVienBUL(LopBus lopBus)
    {
        this.lopBus = lopBus;
        dal = new SinhVienDAL();
    }

    public List<LopHoc> LayDanhSachLop() => lopBus.LayDanhSach();
    public List<SinhVien> LayDanhSach() => dal.LayDanhSach();
    public SinhVien? TimTheoMa(string maSV) => dal.TimTheoMa(maSV.Trim());

    public List<ValidationResult> LayLoiValidation(SinhVien sv)
    {
        sv.MaSV = sv.MaSV.Trim();
        sv.HoTen = Regex.Replace(sv.HoTen.Trim(), @"\s+", " ");
        sv.Email = sv.Email.Trim();
        sv.DienThoai = sv.DienThoai.Trim();

        if (sv.Diem >= 0 && sv.Diem <= 10)
            sv.Diem = Math.Round(sv.Diem, 1, MidpointRounding.AwayFromZero);
        var ketQua = sv.LayLoiValidation();

        if (!string.IsNullOrEmpty(sv.MaLop))
        {
            LopHoc? lop = lopBus.TimTheoMa(sv.MaLop);
            if (lop == null)
                ketQua.Add(new ValidationResult("Lớp học không tồn tại.", new[] { nameof(SinhVien.MaLop) }));
            else
                sv.LopHoc = lop;
        }
        return ketQua;
    }

    public bool KiemTra(SinhVien sv, out string loi)
    {
        var ketQua = LayLoiValidation(sv);
        loi = string.Join(Environment.NewLine, ketQua.Select(x => x.ErrorMessage));
        return ketQua.Count == 0;
    }

    public bool Them(SinhVien sv, out string loi)
    {
        if (!KiemTra(sv, out loi)) return false;
        if (TimTheoMa(sv.MaSV) != null)
        {
            loi = "Mã sinh viên đã tồn tại.";
            return false;
        }
        dal.Them(sv);
        return true;
    }

    public bool Sua(SinhVien sv, out string loi)
    {
        if (!KiemTra(sv, out loi)) return false;
        SinhVien? cu = TimTheoMa(sv.MaSV);
        if (cu == null)
        {
            loi = "Không tìm thấy sinh viên cần sửa.";
            return false;
        }
        dal.Sua(cu, sv);
        return true;
    }

    public bool Xoa(string maSV, out string loi)
    {
        SinhVien? sv = TimTheoMa(maSV);
        if (sv == null)
        {
            loi = "Không tìm thấy sinh viên cần xóa.";
            return false;
        }
        dal.Xoa(sv);
        loi = "";
        return true;
    }

    public List<SinhVien> TimKiem(string tuKhoa, string maLop, double diemTu)
    {
        tuKhoa = tuKhoa.Trim();
        return dal.LayDanhSach().Where(sv =>
            (string.IsNullOrEmpty(tuKhoa)
             || sv.MaSV.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)
             || sv.HoTen.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)
             || sv.Email.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase)
             || (sv.DienThoai ?? "").Contains(tuKhoa, StringComparison.OrdinalIgnoreCase))
            && (string.IsNullOrEmpty(maLop) || string.Equals(sv.MaLop, maLop, StringComparison.OrdinalIgnoreCase))
            && sv.Diem >= diemTu).ToList();
    }
}