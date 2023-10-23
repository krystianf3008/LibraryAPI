using FluentValidation;
using LibraryAPI.Models.Users;
using LibraryAPI.Entities;

namespace LibraryAPI.Models.Validators
{
    public class RegisterUserDTOValidator : AbstractValidator<RegisterUserDTO>
    {
        public RegisterUserDTOValidator(LibraryDbContext dbContext) 
        {
            RuleFor(u => u.Email).
                NotEmpty().
                EmailAddress();
            RuleFor(u => u.Password)
                .Equal(x => x.ConfirmPassword)
                .MinimumLength(10);
            RuleFor(u => u.Email).Custom((value, context) =>
            {
                if(dbContext.User.Any(u => u.Email == value)) 
                 {
                    context.AddFailure("Email", "That email adress is taken");  
                }
                
            });
        }
    }
}
