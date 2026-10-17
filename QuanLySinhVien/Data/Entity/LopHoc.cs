using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QuanLySinhVien.Data.Entity
{
    public class LopHoc
    {
        [Required(ErrorMessage = "Mã lớp không được để trống.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Mã lớp phải có độ dài từ 3 đến 20 ký tự.")]
        public string MaLop { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên lớp không được để trống.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Tên lớp phải có độ dài từ 2 đến 100 ký tự.")]
        public string TenLop { get; set; } = string.Empty;

        /// <summary>
        /// Quan hệ 1 - n: Một lớp học có nhiều sinh viên
        /// </summary>
        public List<SinhVien> DanhSachSinhVien { get; set; } = new List<SinhVien>();
        public List<SinhVien> SinhViens => DanhSachSinhVien;

        public LopHoc() { }

        public LopHoc(string maLop, string tenLop)
        {
            MaLop = maLop;
            TenLop = tenLop;
        }

        /// <summary>
        /// Phương thức kiểm tra các thuộc tính của đối tượng có hợp lệ hay không bằng Data Annotations
        /// </summary>
        public bool KiemTraHopLe(out List<string> danhSachLoi)
        {
            var context = new ValidationContext(this);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(this, context, results, true);
            danhSachLoi = results.Select(r => r.ErrorMessage ?? "Lỗi không xác định").ToList();
            return isValid;
        }

        public bool IsValid()
        {
            return KiemTraHopLe(out _);
        }

        public List<ValidationResult> LayLoiValidation()
        {
            var context = new ValidationContext(this);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(this, context, results, true);
            return results;
        }

        public override string ToString()
        {
            return TenLop;
        }
    }
}