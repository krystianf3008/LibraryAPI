namespace LibraryAPI.Entities
{
    public class Author
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Country { get; set; }
        public int BirthYear { get; set; }
        public string Description { get; set; }
        public string Base64Photo { get; set; }
        public Guid? AddedBy { get; set; }
        public bool IsVerified { get; set; }
    }
}
