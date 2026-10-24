using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVienDAO.Entity
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(255)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Thuộc tính điều hướng: 1 Category có nhiều Book
        /// </summary>
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();

        public Category()
        {
        }

        public Category(string description)
        {
            Description = description;
        }

        public Category(int categoryId, string description)
        {
            CategoryId = categoryId;
            Description = description;
        }

        public override string ToString()
        {
            return $"{CategoryId} - {Description}";
        }
    }
}
