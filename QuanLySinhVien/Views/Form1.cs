using QuanLySinhVien.BUL;
using QuanLySinhVien.Data.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class Form1 : Form
    {
        private readonly SinhVienBUL sinhVienBUL = new();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Cấu hình trạng thái
                comboBox2.Items.Clear();
                comboBox2.Items.AddRange(new object[] { "Đang học", "Đã tốt nghiệp", "Bảo lưu", "Đình chỉ" });
                comboBox2.SelectedIndex = 0;

                radioButton1.Checked = true;

                // 2. Cấu hình DataGridView
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.ReadOnly = true;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;
                dataGridView1.AllowUserToAddRows = false;
                CauHinhCotDataGridView();

                // 3. Đăng ký sự kiện các nút
                button1.Click += ButtonThem_Click;
                button2.Click += ButtonSua_Click;
                button3.Click += ButtonXoa_Click;
                button4.Click += ButtonLamMoi_Click;
                button5.Click += ButtonTim_Click;
                button6.Click += ButtonHienThiTatCa_Click;
                dataGridView1.CellClick += DataGridView1_CellClick;

                // 4. Load dữ liệu từ MySQL
                LoadDanhSachLop();
                LoadDanhSachSinhVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CauHinhCotDataGridView()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaSV", HeaderText = "Mã SV", Width = 110 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "HoTen", HeaderText = "Họ và tên", Width = 180 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NgaySinhFormatted", HeaderText = "Ngày sinh", Width = 110 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "GioiTinh", HeaderText = "Giới tính", Width = 90 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenLop", HeaderText = "Lớp học", Width = 160 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DiemFormatted", HeaderText = "Điểm", Width = 80 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Email", HeaderText = "Email", Width = 180 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DienThoai", HeaderText = "Điện thoại", Width = 120 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TrangThai", HeaderText = "Trạng thái", Width = 120 });
        }

        private void LoadDanhSachLop()
        {
            var dsLop = sinhVienBUL.LayDanhSachLop();

            comboBox1.DataSource = dsLop.ToList();
            comboBox1.DisplayMember = "TenLop";
            comboBox1.ValueMember = "MaLop";

            // ComboBox lọc lớp học
            var dsLopLoc = dsLop.Select(l => new LopHoc(l.MaLop, l.TenLop)).ToList();
            dsLopLoc.Insert(0, new LopHoc("", "-- Tất cả lớp --"));
            comboBox3.DataSource = dsLopLoc;
            comboBox3.DisplayMember = "TenLop";
            comboBox3.ValueMember = "MaLop";
            comboBox3.SelectedIndex = 0;
        }

        private void LoadDanhSachSinhVien()
        {
            var ds = sinhVienBUL.LayDanhSach();
            HienThiDanhSach(ds);
        }

        private void HienThiDanhSach(List<SinhVien> ds)
        {
            dataGridView1.DataSource = ds;
            textBox7.Text = ds.Count.ToString();
        }

        private SinhVien ThuThapDuLieuForm()
        {
            return new SinhVien
            {
                MaSV = textBox1.Text.Trim(),
                HoTen = textBox5.Text.Trim(),
                NgaySinh = dateTimePicker1.Value.Date,
                GioiTinh = radioButton1.Checked ? "Nam" : (radioButton2.Checked ? "Nữ" : "Khác"),
                MaLop = comboBox1.SelectedValue?.ToString() ?? "",
                Diem = (double)numericUpDown1.Value,
                Email = textBox3.Text.Trim(),
                DienThoai = textBox2.Text.Trim(),
                TrangThai = comboBox2.SelectedItem?.ToString() ?? "Đang học"
            };
        }

        private void ButtonThem_Click(object? sender, EventArgs e)
        {
            try
            {
                var sv = ThuThapDuLieuForm();
                if (!sinhVienBUL.Them(sv, out string loi))
                {
                    MessageBox.Show(loi, "Lỗi kiểm tra thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachSinhVien();
                LamMoiForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonSua_Click(object? sender, EventArgs e)
        {
            try
            {
                var sv = ThuThapDuLieuForm();
                if (!sinhVienBUL.Sua(sv, out string loi))
                {
                    MessageBox.Show(loi, "Lỗi kiểm tra thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Cập nhật thông tin sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachSinhVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonXoa_Click(object? sender, EventArgs e)
        {
            string maSV = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(maSV))
            {
                MessageBox.Show("Vui lòng chọn hoặc nhập mã sinh viên cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var xacNhan = MessageBox.Show($"Bạn có chắc chắn muốn xóa sinh viên '{maSV}' không?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (xacNhan != DialogResult.Yes) return;

            try
            {
                if (!sinhVienBUL.Xoa(maSV, out string loi))
                {
                    MessageBox.Show(loi, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Xóa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachSinhVien();
                LamMoiForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonLamMoi_Click(object? sender, EventArgs e)
        {
            LamMoiForm();
        }

        private void LamMoiForm()
        {
            textBox1.Clear();
            textBox5.Clear();
            dateTimePicker1.Value = new DateTime(2003, 1, 1);
            radioButton1.Checked = true;
            radioButton2.Checked = false;
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
            numericUpDown1.Value = 0;
            textBox3.Clear();
            textBox2.Clear();
            if (comboBox2.Items.Count > 0) comboBox2.SelectedIndex = 0;
            textBox1.Focus();
        }

        private void ButtonTim_Click(object? sender, EventArgs e)
        {
            string tuKhoa = textBox4.Text.Trim();
            string maLop = comboBox3.SelectedValue?.ToString() ?? "";
            double diemTu = 0.0;

            if (!string.IsNullOrEmpty(textBox6.Text.Trim()) && double.TryParse(textBox6.Text.Trim(), out double d))
            {
                diemTu = d;
            }

            var ketQua = sinhVienBUL.TimKiem(tuKhoa, maLop, diemTu);
            HienThiDanhSach(ketQua);
        }

        private void ButtonHienThiTatCa_Click(object? sender, EventArgs e)
        {
            textBox4.Clear();
            textBox6.Clear();
            if (comboBox3.Items.Count > 0) comboBox3.SelectedIndex = 0;
            LoadDanhSachSinhVien();
        }

        private void DataGridView1_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count) return;

            var row = dataGridView1.Rows[e.RowIndex];
            if (row.DataBoundItem is SinhVien sv)
            {
                textBox1.Text = sv.MaSV;
                textBox5.Text = sv.HoTen;
                dateTimePicker1.Value = sv.NgaySinh >= dateTimePicker1.MinDate && sv.NgaySinh <= dateTimePicker1.MaxDate
                    ? sv.NgaySinh
                    : DateTime.Today;

                if (string.Equals(sv.GioiTinh, "Nữ", StringComparison.OrdinalIgnoreCase))
                {
                    radioButton2.Checked = true;
                }
                else
                {
                    radioButton1.Checked = true;
                }

                if (!string.IsNullOrEmpty(sv.MaLop))
                {
                    comboBox1.SelectedValue = sv.MaLop;
                }

                decimal diem = (decimal)Math.Clamp(sv.Diem, (double)numericUpDown1.Minimum, (double)numericUpDown1.Maximum);
                numericUpDown1.Value = diem;

                textBox3.Text = sv.Email;
                textBox2.Text = sv.DienThoai;

                if (!string.IsNullOrEmpty(sv.TrangThai))
                {
                    comboBox2.SelectedItem = sv.TrangThai;
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void radioButton2_CheckedChanged(object sender, EventArgs e) { }
    }
}
