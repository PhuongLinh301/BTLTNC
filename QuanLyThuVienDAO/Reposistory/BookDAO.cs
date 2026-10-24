using Microsoft.EntityFrameworkCore;
using QuanLyThuVienDAO.Entity;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyThuVienDAO.Reposistory
{
    public class BookDAO
    {
        public List<Book> GetAll()
        {
            using var context = new QLTVDBContext();
            return context.Books
                .Include(b => b.Category)
                .Include(b => b.BookCopies)
                .ToList();
        }

        public Book? GetById(int bookId)
        {
            using var context = new QLTVDBContext();
            return context.Books
                .Include(b => b.Category)
                .Include(b => b.BookCopies)
                .FirstOrDefault(b => b.BookId == bookId);
        }

        public List<Book> GetByCategoryId(int categoryId)
        {
            using var context = new QLTVDBContext();
            return context.Books
                .Where(b => b.CategoryId == categoryId)
                .Include(b => b.BookCopies)
                .ToList();
        }

        public bool Add(Book book)
        {
            using var context = new QLTVDBContext();
            context.Books.Add(book);
            return context.SaveChanges() > 0;
        }

        public bool Update(Book book)
        {
            using var context = new QLTVDBContext();
            context.Books.Update(book);
            return context.SaveChanges() > 0;
        }

        public bool Delete(int bookId)
        {
            using var context = new QLTVDBContext();
            var book = context.Books.Find(bookId);
            if (book == null) return false;

            context.Books.Remove(book);
            return context.SaveChanges() > 0;
        }
    }
}
