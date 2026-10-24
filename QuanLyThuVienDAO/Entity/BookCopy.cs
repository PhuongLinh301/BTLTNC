using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVienDAO.Entity
{
    [Table("BookCopies")]
    public class BookCopy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookCopyId { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(50)]
        public string Status { get; set; } = "Available";

        [StringLength(100)]
        public string Condition { get; set; } = "Good";

        // Khóa ngoại liên kết tới Book
        public int BookId { get; set; }

        [ForeignKey(nameof(BookId))]
        public virtual Book? Book { get; set; }

        public BookCopy()
        {
        }

        public BookCopy(string status, string condition, int bookId)
        {
            Status = status;
            Condition = condition;
            BookId = bookId;
        }

        public override string ToString()
        {
            return $"Copy #{BookCopyId} (Book #{BookId}) - [{Status}] - {Condition}";
        }
    }
}
