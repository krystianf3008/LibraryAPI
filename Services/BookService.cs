using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryAPI.Models.Books;
using LibraryAPI.Entities;
using LibraryAPI.Exceptions;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IMapper _mapper;
        public BookService(LibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BookDTO>> GetAll()
        {
            return await _dbContext.Book.Where(b=>b.IsVerified==true)
                .Include(b => b.Category)
                .ProjectTo<BookDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
        public async Task<IEnumerable<Book>> GetBooksToVerify()
        {
            return await _dbContext.Book.Where(b => b.IsVerified==false)
                .Include(b => b.Category)
                .ToListAsync();
        }
        public async Task<OneBookDTO> GetById(Guid id)
        {
            var book = _dbContext.Book.Include(b => b.Category)
                .ProjectTo<OneBookDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                throw new NotFoundException("The book you are trying to get does not exist");
            }
            return book ;
        }
        public async Task<Guid> CreateBook(CreateBookDTO createBookDTO,string userId)
        {
                Book book = _mapper.Map<Book>(createBookDTO);
                book.IsVerified = false;
                book.AddedBy = Guid.Parse(userId);
                _dbContext.Book.Add(book);
            await _dbContext.SaveChangesAsync();
            return book.Id;
        }
        public async Task UpdateBook(Guid id, CreateBookDTO updateBookDTO, string userId)
        {
            var bookToUpdate = _dbContext.Book.FirstOrDefault(b => b.Id == id);
            if (bookToUpdate == null)
            {
                throw new NotFoundException("The book you are trying to edit does not exist");
            }
            if (bookToUpdate.AddedBy.ToString() != userId)
            {
                throw new UnauthorizedAccessException();
            }
            _mapper.Map(updateBookDTO, bookToUpdate);
            bookToUpdate.AddedBy = Guid.Parse(userId);
            bookToUpdate.IsVerified = false;

            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteBook(Guid id, string userId)
        {
            var book = _dbContext.Book.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                throw new NotFoundException("The book you are trying to delete does not exist");
            }
            if (book.AddedBy.ToString() != userId)
            {
                throw new UnauthorizedAccessException();
            }
            _dbContext.Book.Remove(book);
            await _dbContext.SaveChangesAsync();
        }
        public async Task VerifyBook(Guid id)
        {
            var book = _dbContext.Book.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                throw new NotFoundException("The book you are trying to verify does not exist");
            }

            book.IsVerified= true;
            await _dbContext.SaveChangesAsync();
        }
    }
    
}
