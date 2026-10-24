using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVienDAO.Entity
{
    [Table("Books")]
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(255)]
        public string Title { get; set; } = string.Empty;

        [StringLength(150)]
        public string Publisher { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative.")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Copy count must be non-negative.")]
        public int CopyCount { get; set; }

        [StringLength(150)]
        public string Author { get; set; } = string.Empty;

        [Range(1000, 2100, ErrorMessage = "Publish year is invalid.")]
        public int PublishYear { get; set; }

        // Khóa ngoại liên kết tới Category
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category? Category { get; set; }

        /// <summary>
        /// Thuộc tính điều hướng: 1 Book có nhiều BookCopy
        /// </summary>
        public virtual ICollection<BookCopy> BookCopies { get; set; } = new List<BookCopy>();

        public Book()
        {
        }

        public Book(string title, string publisher, decimal price, int copyCount, string author, int publishYear, int categoryId)
        {
            Title = title;
            Publisher = publisher;
            Price = price;
            CopyCount = copyCount;
            Author = author;
            PublishYear = publishYear;
            CategoryId = categoryId;
        }

        public override string ToString()
        {
            return $"{BookId} - {Title}";
        }
    }
}
