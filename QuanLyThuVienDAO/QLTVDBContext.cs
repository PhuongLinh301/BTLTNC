using Microsoft.EntityFrameworkCore;
using QuanLyThuVienDAO.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuanLyThuVienDAO
{
    public class QLTVDBContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }

        public QLTVDBContext()
        {
        }

        public QLTVDBContext(DbContextOptions<QLTVDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Chuỗi kết nối mặc định đến MySQL
                optionsBuilder.UseMySQL("Server=localhost;Port=3306;Database=quanlythuvien;Uid=root;Pwd=phuonglinh3001;Charset=utf8mb4;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình quan hệ: 1 Category có nhiều Book
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình quan hệ: 1 Book có nhiều BookCopy
            modelBuilder.Entity<BookCopy>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookCopies)
                .HasForeignKey(bc => bc.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

    // Lớp tương thích tên cũ nếu cần
    public class QLSVDGContext : QLTVDBContext
    {
        public QLSVDGContext() : base() { }
        public QLSVDGContext(DbContextOptions<QLTVDBContext> options) : base(options) { }
    }
}
