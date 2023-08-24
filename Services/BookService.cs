using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryAPI.DTOs;
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
            return await _dbContext.Book.Include(b => b.Category).ProjectTo<BookDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
        public async Task<OneBookDTO> GetById(Guid id)
        {
            var book = _dbContext.Book.Include(b => b.Category).ProjectTo<OneBookDTO>(_mapper.ConfigurationProvider).FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                throw new NotFoundException("The book you are trying to get does not exist");
            }
            return book ;
        }
        public async Task<Guid> CreateBook(CreateBookDTO createBookDTO)
        {
                Book book = _mapper.Map<Book>(createBookDTO);
                _dbContext.Book.Add(book);
                _dbContext.SaveChanges();
                return book.Id;
        }
        public async Task UpdateBook(Guid id, CreateBookDTO updateBookDTO)
        {
            var bookToUpdate = _dbContext.Book.FirstOrDefault(b => b.Id == id);
            if (bookToUpdate == null)
            {
                throw new NotFoundException("The book you are trying to edit does not exist");
            }
            _mapper.Map(updateBookDTO, bookToUpdate);

            _dbContext.SaveChanges();
        }
        public async Task DeleteBook(Guid id)
        {
            var book = _dbContext.Book.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                throw new NotFoundException("The book you are trying to delete does not exist");
            }
            _dbContext.Book.Remove(book);
            _dbContext.SaveChanges();
        }
    }
    
}
