using LibraryAPI.DTOs;
using LibraryAPI.Models;

namespace LibraryAPI.Interfaces
{
    public interface IBookService 
    {
        public Task<IEnumerable<Book>> GetAll();
        public Task<Book> GetById(Guid id);
        public Task<Guid> CreateBook(CreateBookDTO createBookDTO);
        public Task UpdateBook(Guid id, UpdateBookDTO updateBookDTO);
        public Task DeleteBook(Guid id);
    }
}
