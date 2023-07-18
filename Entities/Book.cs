namespace LibraryAPI.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public int NumberOfPages { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryID { get; set; }
        public string Base64Cover { get; set; }

    }
}
