using LibraryAPI.DTOs;
using LibraryAPI.Entities;

namespace LibraryAPI.Interfaces
{
    public interface IAuthorService
    {
        Task<Guid> CreateAuthor(CreateAuthorDto aurhorDTO);
        Task DeleteAuthor(Guid id);
        Task<IEnumerable<Author>> GetAll();
        Task<IEnumerable<BookDTO>> GetBooks(Guid id);
        Task<Author> GetById(Guid id);
        Task UpdateAuthor(Guid id, CreateAuthorDto authorDTO);
    }
}