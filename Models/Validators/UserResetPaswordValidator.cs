using FluentValidation;
using LibraryAPI.Entities;
using LibraryAPI.Models.Users;

namespace LibraryAPI.Models.Validators
{
    public class UserResetPasswordDTOValidator : AbstractValidator<UserResetPasswordDTO>
    {
        public UserResetPasswordDTOValidator(LibraryDbContext dbContext)
        {
            RuleFor(u => u.Email).
                NotEmpty().
                EmailAddress();
            RuleFor(u => u.Password)
                .Equal(x => x.ConfirmPassword)
                .MinimumLength(10);
            RuleFor(u => u.Token).
                NotEmpty();
        }
    }
}
