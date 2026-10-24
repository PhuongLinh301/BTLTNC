using Microsoft.EntityFrameworkCore;
using QuanLyThuVienDAO.Entity;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyThuVienDAO.Reposistory
{
    public class CategoryDAO
    {
        public List<Category> GetAll()
        {
            using var context = new QLTVDBContext();
            return context.Categories.Include(c => c.Books).ToList();
        }

        public Category? GetById(int categoryId)
        {
            using var context = new QLTVDBContext();
            return context.Categories.Include(c => c.Books).FirstOrDefault(c => c.CategoryId == categoryId);
        }

        public bool Add(Category category)
        {
            using var context = new QLTVDBContext();
            context.Categories.Add(category);
            return context.SaveChanges() > 0;
        }

        public bool Update(Category category)
        {
            using var context = new QLTVDBContext();
            context.Categories.Update(category);
            return context.SaveChanges() > 0;
        }

        public bool Delete(int categoryId)
        {
            using var context = new QLTVDBContext();
            var category = context.Categories.Find(categoryId);
            if (category == null) return false;

            context.Categories.Remove(category);
            return context.SaveChanges() > 0;
        }
    }
}
