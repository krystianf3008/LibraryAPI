namespace LibraryAPI.Models.Users
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public string Base64Avatar { get; set; }
    }
}
