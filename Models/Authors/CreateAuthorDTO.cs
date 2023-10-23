namespace LibraryAPI.Models.Authors
{
    public class CreateAuthorDto
    {
        public string FullName { get; set; }
        public string Country { get; set; }
        public int BirthYear { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
    }
}
