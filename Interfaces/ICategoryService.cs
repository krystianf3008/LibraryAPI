using LibraryAPI.Models.Books;
using LibraryAPI.Models.Categories;
using LibraryAPI.Models;

namespace LibraryAPI.Interfaces
{
    public interface ICategoryService
    {
        Task<int> CreateCategory(CreateCategoryDTO categoryDTO);
        Task DeleteCategory(int id);
        Task<IEnumerable<Category>> GetAll();
        Task<IEnumerable<BookDTO>> GetBooks(int ud);
        Task<Category> GetById(int id);
        Task UpdateCategory(int id, CreateCategoryDTO categoryDTO);
    }
}