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
        public BookService(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Book>> GetAll()
        {

            return _dbContext.Book.Include(b => b.Category).ToList();
        }
        public async Task<Book> GetById(Guid id)
        {
            var book = _dbContext.Book.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                throw new NotFoundException("The book you are trying to get does not exist");
            }
            return book ;
        }
        public async Task<Guid> CreateBook(CreateBookDTO createBookDTO)
        {
            string base64Image;
            using (var memoryStream = new MemoryStream())
            {
                createBookDTO.Cover.CopyTo(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();
                base64Image = Convert.ToBase64String(imageBytes);
            }

            Book book = new Book()
                {
                    Id = Guid.NewGuid(),
                    Title = createBookDTO.Title,
                    Description = createBookDTO.Description,
                    Author = createBookDTO.Author,
                    PublicationYear = createBookDTO.PublicationYear,
                    NumberOfPages = createBookDTO.NumberOfPages,
                    CategoryID = createBookDTO.CategoryID,
                    Base64Cover = base64Image

                };
                _dbContext.Book.Add(book);
                _dbContext.SaveChanges();
                return book.Id;
        }
        public async Task UpdateBook(Guid id, UpdateBookDTO updateBookDTO)
        {
            var book = _dbContext.Book.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                throw new NotFoundException("The book you are trying to edit does not exist");
            }
            string base64Image;
            using (var memoryStream = new MemoryStream())
            {
                updateBookDTO.Cover.CopyTo(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();
                base64Image =  Convert.ToBase64String(imageBytes);
            }

            book.Title = updateBookDTO.Title;
            book.Description = updateBookDTO.Description;
            book.Author = updateBookDTO.Author;
            book.PublicationYear = updateBookDTO.PublicationYear;
            book.NumberOfPages = updateBookDTO.NumberOfPages;
            book.CategoryID = updateBookDTO.CategoryID;
            book.Base64Cover= base64Image;

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
