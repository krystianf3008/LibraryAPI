using LibraryAPI.Models;

namespace LibraryAPI.DTOs
{
    public class OneBookDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public int NumberOfPages { get; set; }
        public string CategoryName { get; set; }
        public string Base64Cover { get; set; }
    }
}
