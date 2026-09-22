using System;
using System.Collections.Generic;
using System.Text;

namespace Buổi_1
{
    public class HinhTron
    {
        public double BanKinh { get; set; }
        public HinhTron(double banKinh)
        {
            BanKinh = banKinh;
        }

        //tính chu vi: C = 2 * PI * r
        public double TinhChuVi()
        {
            return 2 * Math.PI * BanKinh;
        }

        //tính diện tích: S = PI * r^2
        public double TinhDienTich()
        {
            return Math.PI * Math.Pow(BanKinh, 2);
        }
    }
}