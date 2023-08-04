using System.Collections.Generic;
using MallSuite.Models;

namespace MallSuite.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category GetById(int id);
        void Add(Category category);
        void Delete(int categoryId);
        void Edit(Category category);
    }
}
