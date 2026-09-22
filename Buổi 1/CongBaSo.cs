using System;
using System.Collections.Generic;
using System.Text;

namespace Buổi_1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Thiết lập hiển thị tiếng Việt trên Console
            Console.OutputEncoding = Encoding.UTF8;

            int a;
            int b;
            int c;

            Console.Write("Nhập a: ");
            a = Convert.ToInt32(Console.ReadLine());

            while (true)
            {
                try
                {
                    Console.Write("Nhập b: ");
                    // Thêm dấu ! để hết cảnh báo CS8604
                    b = int.Parse(Console.ReadLine()!);
                    break;
                }
                catch
                {
                    Console.WriteLine("Nhập sai, vui lòng nhập lại!");
                }
            }

            bool k;
            while (true)
            {
                Console.Write("Nhập c: ");
                k = int.TryParse(Console.ReadLine(), out c);

                if (k == true)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Nhập sai, vui lòng nhập lại!");
                }
            }

            int tong = a + b + c;
            Console.WriteLine($"\nKết quả: {a} + {b} + {c} = {tong}");

            Console.ReadLine();
        }
    }
}