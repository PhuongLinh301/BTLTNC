namespace QuanLySinhVienApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            So_A = new Label();
            A = new TextBox();
            Cong = new Button();
            Nhan = new Button();
            Tru = new Button();
            Chia = new Button();
            So_B = new Label();
            B = new TextBox();
            Ket_qua = new Label();
            KQ = new TextBox();
            btnClose = new Button();
            SuspendLayout();
            // 
            // So_A
            // 
            So_A.AutoSize = true;
            So_A.Location = new Point(41, 29);
            So_A.Name = "So_A";
            So_A.Size = new Size(42, 20);
            So_A.TabIndex = 0;
            So_A.Text = "Số A"; // Đã sửa thành có dấu
            So_A.Click += label1_Click;
            // 
            // A
            // 
            A.Location = new Point(121, 26);
            A.Name = "A";
            A.Size = new Size(125, 27);
            A.TabIndex = 1;
            A.TextChanged += textBox1_TextChanged;
            // 
            // Cong
            // 
            Cong.Location = new Point(290, 26);
            Cong.Name = "Cong";
            Cong.Size = new Size(94, 29);
            Cong.TabIndex = 2;
            Cong.Text = "Cộng"; // Đã sửa thành có dấu
            Cong.UseVisualStyleBackColor = true;
            Cong.Click += Cong_Click;
            // 
            // Nhan
            // 
            Nhan.Location = new Point(290, 71);
            Nhan.Name = "Nhan";
            Nhan.Size = new Size(94, 29);
            Nhan.TabIndex = 3;
            Nhan.Text = "Nhân"; // Đã sửa thành có dấu
            Nhan.UseVisualStyleBackColor = true;
            Nhan.Click += Nhan_Click;
            // 
            // Tru
            // 
            Tru.Location = new Point(433, 25);
            Tru.Name = "Tru";
            Tru.Size = new Size(94, 29);
            Tru.TabIndex = 4;
            Tru.Text = "Trừ"; // Đã sửa thành có dấu
            Tru.UseVisualStyleBackColor = true;
            Tru.Click += Tru_Click;
            // 
            // Chia
            // 
            Chia.Location = new Point(433, 71);
            Chia.Name = "Chia";
            Chia.Size = new Size(94, 29);
            Chia.TabIndex = 5;
            Chia.Text = "Chia";
            Chia.UseVisualStyleBackColor = true;
            Chia.Click += Chia_Click;
            // 
            // So_B
            // 
            So_B.AutoSize = true;
            So_B.Location = new Point(41, 75);
            So_B.Name = "So_B";
            So_B.Size = new Size(39, 20);
            So_B.TabIndex = 6;
            So_B.Text = "Số B"; // Đã sửa thành có dấu
            So_B.Click += label2_Click;
            // 
            // B
            // 
            B.Location = new Point(121, 71);
            B.Name = "B";
            B.Size = new Size(125, 27);
            B.TabIndex = 7;
            // 
            // Ket_qua
            // 
            Ket_qua.AutoSize = true;
            Ket_qua.Location = new Point(41, 121);
            Ket_qua.Name = "Ket_qua";
            Ket_qua.Size = new Size(62, 20);
            Ket_qua.TabIndex = 8;
            Ket_qua.Text = "Kết quả"; // Đã sửa thành có dấu
            // 
            // KQ
            // 
            KQ.Location = new Point(121, 118);
            KQ.Name = "KQ";
            KQ.Size = new Size(125, 27);
            KQ.TabIndex = 9;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(290, 118);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(94, 29);
            btnClose.TabIndex = 10;
            btnClose.Text = "Đóng"; // Đổi Close thành Đóng cho đồng bộ tiếng Việt
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(551, 179);
            Controls.Add(btnClose);
            Controls.Add(KQ);
            Controls.Add(Ket_qua);
            Controls.Add(B);
            Controls.Add(So_B);
            Controls.Add(Chia);
            Controls.Add(Tru);
            Controls.Add(Nhan);
            Controls.Add(Cong);
            Controls.Add(A);
            Controls.Add(So_A);
            Name = "Form1";
            Text = "Máy Tính Cơ Bản"; // Thêm tiêu đề cho Form
            FormClosing += Form1_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label So_A;
        private TextBox A;
        private Button Cong;
        private Button Nhan;
        private Button Tru;
        private Button Chia;
        private Label So_B;
        private TextBox B;
        private Label Ket_qua;
        private TextBox KQ;
        private Button btnClose;
    }
}