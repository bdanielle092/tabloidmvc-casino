using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        void AddCategory(Category category);
        Category GetCategoryById(int id);
        void DeleteCategory(int categoryId);
        void UpdateCategory(Category category);
    }
}