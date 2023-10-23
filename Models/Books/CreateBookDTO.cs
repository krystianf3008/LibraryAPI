namespace LibraryAPI.Models.Books
{
    public class CreateBookDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid AuthorID { get; set; }
        public int PublicationYear { get; set; }
        public int NumberOfPages { get; set; }
        public int CategoryID { get; set; }
        public IFormFile Cover { get; set; }

    }
}
