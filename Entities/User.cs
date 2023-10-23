namespace LibraryAPI.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Base64Avatar { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public string? VerificationToken { get; set; } 
        public bool IsVerified { get; set; }
        public string? ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordTokenExpires { get; set; } 

    }
}
