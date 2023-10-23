using LibraryAPI.Models;
using LibraryAPI.Models.Books;

namespace LibraryAPI.Interfaces
{
    public interface IBookService 
    {
         Task<IEnumerable<BookDTO>> GetAll();
         Task<IEnumerable<Book>> GetBooksToVerify();
         Task<OneBookDTO> GetById(Guid id);
         Task<Guid> CreateBook(CreateBookDTO createBookDTO, string userId);
         Task UpdateBook(Guid id, CreateBookDTO updateBookDTO, string userId);
         Task DeleteBook(Guid id,string userId);
         Task VerifyBook(Guid id);
    }
}
