using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryAPI.Models.Books;
using LibraryAPI.Models.Categories;
using LibraryAPI.Entities;
using LibraryAPI.Exceptions;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryService(LibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _dbContext.Category
                .ToListAsync();
        }
        public async Task<Category> GetById(int id)
        {
            var category = await _dbContext.Category
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                throw new NotFoundException("The category you are trying to get does not exist");
            }

            return category;
        }

        public async Task<int> CreateCategory(CreateCategoryDTO categoryDTO)
        {
            Category category = _mapper.Map<Category>(categoryDTO);
            _dbContext.Category.Add(category);
            await _dbContext.SaveChangesAsync();
            return category.Id;
        }

        public async Task UpdateCategory(int id, CreateCategoryDTO categoryDTO)
        {
            var categoryToUpdate = await _dbContext.Category.FirstOrDefaultAsync(c => c.Id == id);

            if (categoryToUpdate == null)
            {
                throw new NotFoundException("The category you are trying to edit does not exist");
            }

            _mapper.Map(categoryDTO, categoryToUpdate);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _dbContext.Category.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                throw new NotFoundException("The category you are trying to delete does not exist");
            }

            _dbContext.Category.Remove(category);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<BookDTO>> GetBooks(int id)
        {
            var category = await _dbContext.Category.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                throw new NotFoundException("The category you are trying to delete does not exist");
            }

            return await _dbContext.Book.Include(b => b.Category).ProjectTo<BookDTO>(_mapper.ConfigurationProvider).Where(b => b.CategoryName == category.Name).ToListAsync();
        }
    }
}
