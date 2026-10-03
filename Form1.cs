using System;
using System.Windows.Forms;

namespace CalculatorApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Các sự kiện click label/textbox bị thừa trong designer
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e) { }

        // Code xử lý các nút bấm máy tính
        private void Cong_Click(object sender, EventArgs e)
        {
            if (double.TryParse(A.Text, out double a) && double.TryParse(B.Text, out double b))
                KQ.Text = (a + b).ToString();
            else MessageBox.Show("Vui lòng nhập số hợp lệ.");
        }

        private void Tru_Click(object sender, EventArgs e)
        {
            if (double.TryParse(A.Text, out double a) && double.TryParse(B.Text, out double b))
                KQ.Text = (a - b).ToString();
            else MessageBox.Show("Vui lòng nhập số hợp lệ.");
        }

        private void Nhan_Click(object sender, EventArgs e)
        {
            if (double.TryParse(A.Text, out double a) && double.TryParse(B.Text, out double b))
                KQ.Text = (a * b).ToString();
            else MessageBox.Show("Vui lòng nhập số hợp lệ.");
        }

        private void Chia_Click(object sender, EventArgs e)
        {
            if (double.TryParse(A.Text, out double a) && double.TryParse(B.Text, out double b))
            {
                if (b != 0) KQ.Text = (a / b).ToString();
                else MessageBox.Show("Lỗi: Không thể chia cho 0.");
            }
            else MessageBox.Show("Vui lòng nhập số hợp lệ.");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); // Đóng chương trình
        }
    }
}
