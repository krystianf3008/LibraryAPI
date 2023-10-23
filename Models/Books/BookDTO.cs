using LibraryAPI.Models;

namespace LibraryAPI.Models.Books
{
    //This DTO was created to request `GetAll` for optimization purposes
    public class BookDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string AuthorFullName { get; set; }
        public string CategoryName { get; set; }
        public string Base64Cover { get; set; }
    }
}
