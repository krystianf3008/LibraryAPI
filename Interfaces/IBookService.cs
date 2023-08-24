using LibraryAPI.DTOs;
using LibraryAPI.Models;

namespace LibraryAPI.Interfaces
{
    public interface IBookService 
    {
        public Task<IEnumerable<BookDTO>> GetAll();
        public Task<OneBookDTO> GetById(Guid id);
        public Task<Guid> CreateBook(CreateBookDTO createBookDTO);
        public Task UpdateBook(Guid id, CreateBookDTO updateBookDTO);
        public Task DeleteBook(Guid id);
    }
}
