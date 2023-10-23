using LibraryAPI.Models.Authors;
using LibraryAPI.Models.Books;
using LibraryAPI.Entities;
using LibraryAPI.Models;

namespace LibraryAPI.Interfaces
{
    public interface IAuthorService
    {
        Task<Guid> CreateAuthor(CreateAuthorDto aurhorDTO,string id);
        Task DeleteAuthor(Guid id, string userId);
        Task<IEnumerable<Author>> GetAll();
        Task<IEnumerable<BookDTO>> GetBooks(Guid id);
        Task<Author> GetById(Guid id);
        Task UpdateAuthor(Guid id, CreateAuthorDto authorDTO, string userId);
        Task VerifyAuthor(Guid id);
        Task<IEnumerable<Author>> GetAuthorsToVerify();
    }
}