using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QuanLySinhVien.Data.Entity
{
    public class SinhVien
    {
        [Required(ErrorMessage = "Mã sinh viên không được để trống.")]
        [RegularExpression(@"^SV\d{6,8}$", ErrorMessage = "Mã sinh viên phải có dạng SV kèm 6 đến 8 chữ số (ví dụ: SV000123).")]
        public string MaSV { get; set; } = string.Empty;

        // Cho phép truy cập qua maSV tương thích mã cũ
        public string maSV
        {
            get => MaSV;
            set => MaSV = value;
        }

        [Required(ErrorMessage = "Họ và tên không được để trống.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Họ tên phải có độ dài từ 2 đến 50 ký tự.")]
        public string HoTen { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ngày sinh không được để trống.")]
        public DateTime NgaySinh { get; set; } = new DateTime(2006, 1, 1);

        [Required(ErrorMessage = "Vui lòng chọn giới tính.")]
        [RegularExpression(@"^(Nam|Nữ|Khác)$", ErrorMessage = "Giới tính phải là 'Nam', 'Nữ' hoặc 'Khác'.")]
        public string GioiTinh { get; set; } = "Nam";

        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Định dạng email không hợp lệ.")]
        public string Email { get; set; } = string.Empty;

        // Cho phép truy cập qua email tương thích mã cũ
        public string email
        {
            get => Email;
            set => Email = value;
        }

        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [RegularExpression(@"^0\d{9,10}$", ErrorMessage = "Số điện thoại phải bắt đầu bằng 0 và có từ 10 đến 11 chữ số.")]
        public string DienThoai { get; set; } = string.Empty;

        // Cho phép truy cập qua Dienthoai tương thích mã cũ
        public string Dienthoai
        {
            get => DienThoai;
            set => DienThoai = value;
        }

        public string SDT
        {
            get => DienThoai;
            set => DienThoai = value;
        }

        [Required(ErrorMessage = "Điểm không được để trống.")]
        [Range(0.0, 10.0, ErrorMessage = "Điểm phải nằm trong thang điểm từ 0.0 đến 10.0.")]
        public double Diem { get; set; } = 0.0;

        public string DiemFormatted => Diem.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture);
        public string NgaySinhFormatted => NgaySinh.ToString("dd/MM/yyyy");

        [Required(ErrorMessage = "Vui lòng chọn lớp học.")]
        public string MaLop { get; set; } = string.Empty;

        // Cho phép truy cập qua lop tương thích mã cũ
        public string lop
        {
            get => MaLop;
            set => MaLop = value;
        }

        public string TenLop { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn trạng thái.")]
        public string TrangThai { get; set; } = "Đang học";

        /// <summary>
        /// Thuộc tính điều hướng: Sinh viên thuộc về một lớp học (Quan hệ n - 1)
        /// </summary>
        public LopHoc? LopHoc { get; set; }

        public SinhVien() { }

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

        public List<ValidationResult>? IsInValid()
        {
            var valcontext = new ValidationContext(this);
            var results = new List<ValidationResult>();
            var kt = Validator.TryValidateObject(this, valcontext, results, true);
            return kt ? null : results;
        }

        public List<ValidationResult> LayLoiValidation()
        {
            var context = new ValidationContext(this);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(this, context, results, true);
            return results;
        }
    }
}
