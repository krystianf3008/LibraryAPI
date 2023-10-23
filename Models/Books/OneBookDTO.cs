using LibraryAPI.Models;

namespace LibraryAPI.Models.Books
{
    public class OneBookDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorFullName { get; set; }
        public Guid AuthorID { get; set; }
        public int PublicationYear { get; set; }
        public int NumberOfPages { get; set; }
        public string CategoryName { get; set; }
        public int CategoryID { get; set; }
        public string Base64Cover { get; set; }
    }
}
