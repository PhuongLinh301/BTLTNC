using QuanLySinhVien.Data.DAL;
using QuanLySinhVien.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLySinhVien.BUL;

public class LopBus
{
    private readonly LopDAL dal = new();

    public List<LopHoc> LayDanhSach() => dal.LayDanhSach();
    public LopHoc? TimTheoMa(string maLop) => dal.TimTheoMa(maLop.Trim());

    public bool KiemTra(LopHoc lop, out string loi)
    {
        lop.MaLop = lop.MaLop.Trim();
        lop.TenLop = lop.TenLop.Trim();
        bool hopLe = lop.KiemTraHopLe(out var dsLoi);
        loi = string.Join(Environment.NewLine, dsLoi);
        return hopLe;
    }

    public bool Them(LopHoc lop, out string loi)
    {
        if (!KiemTra(lop, out loi)) return false;
        if (TimTheoMa(lop.MaLop) != null)
        {
            loi = "Mã lớp đã tồn tại.";
            return false;
        }
        dal.Them(lop);
        return true;
    }

    public bool Sua(LopHoc lop, out string loi)
    {
        if (!KiemTra(lop, out loi)) return false;
        LopHoc? cu = TimTheoMa(lop.MaLop);
        if (cu == null)
        {
            loi = "Không tìm thấy lớp cần sửa.";
            return false;
        }
        // Mã lớp dùng để tìm; thao tác sửa chỉ thay đổi tên lớp.
        dal.Sua(cu, lop);
        return true;
    }

    public bool Xoa(string maLop, out string loi)
    {
        LopHoc? lop = TimTheoMa(maLop);
        if (lop == null)
        {
            loi = "Không tìm thấy lớp cần xóa.";
            return false;
        }
        if (lop.SinhViens.Count > 0)
        {
            loi = "Không thể xóa lớp đang có sinh viên. Hãy chuyển hoặc xóa các sinh viên trước.";
            return false;
        }
        dal.Xoa(lop);
        loi = "";
        return true;
    }
}