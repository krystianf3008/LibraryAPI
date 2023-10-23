using LibraryAPI.Models.Users;

namespace LibraryAPI.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAll();
        Task<UserDTO> GetById(Guid id);
        Task<Guid> CreateUser(RegisterUserDTO userDTO);
        Task VerifyUser(string token);
        Task CreateResetPasswordToken(string email);
        Task ResetPassword(UserResetPasswordDTO userDto);
        Task<string> GenerateJwtToken(UserLoginDTO userDTO);
        Task AddModeratorRole(Guid id);
    }
}