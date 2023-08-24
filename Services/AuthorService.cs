using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryAPI.DTOs;
using LibraryAPI.Entities;
using LibraryAPI.Exceptions;
using LibraryAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public AuthorService(LibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            return await _dbContext.Author
                .ToListAsync();
        }
        public async Task<Author> GetById(Guid id)
        {
            var author = await _dbContext.Author
                .FirstOrDefaultAsync(c => c.Id == id);

            if (author == null)
            {
                throw new NotFoundException("The author you are trying to get does not exist");
            }

            return author;
        }

        public async Task<Guid> CreateAuthor(CreateAuthorDto aurhorDTO)
        {
            Author author = _mapper.Map<Author>(aurhorDTO);
            _dbContext.Author.Add(author);
            await _dbContext.SaveChangesAsync();
            return author.Id;
        }

        public async Task UpdateAuthor(Guid id, CreateAuthorDto authorDTO)
        {
            var authorToUpdate = await _dbContext.Author.FirstOrDefaultAsync(c => c.Id == id);

            if (authorToUpdate == null)
            {
                throw new NotFoundException("The author you are trying to edit does not exist");
            }

            _mapper.Map(authorDTO, authorToUpdate);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAuthor(Guid id)
        {
            var author = await _dbContext.Author.FirstOrDefaultAsync(c => c.Id == id);

            if (author == null)
            {
                throw new NotFoundException("The author you are trying to delete does not exist");
            }

            _dbContext.Author.Remove(author);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<BookDTO>> GetBooks(Guid id)
        {
            var author = await _dbContext.Author.FirstOrDefaultAsync(c => c.Id == id);

            if (author == null)
            {
                throw new NotFoundException("The author you are trying to delete does not exist");
            }

            return await _dbContext.Book.ProjectTo<BookDTO>(_mapper.ConfigurationProvider).Where(b => b.AuthorFullName == author.FullName).ToListAsync();
        }
    }
}

