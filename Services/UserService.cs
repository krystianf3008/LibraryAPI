using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryAPI.Models.Users;
using LibraryAPI.Entities;
using LibraryAPI.Exceptions;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using LibraryAPI.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryAPI.Services
{
    public class UserService : IUserService
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public UserService(LibraryDbContext dbContext, IMapper mapper, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            return await _dbContext.User
                .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
        public async Task<UserDTO> GetById(Guid id)
        {
            var user = await _dbContext.User
                .Include(u => u.Role).ProjectTo<UserDTO>(_mapper.ConfigurationProvider).
                FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new NotFoundException("The user you are trying to get does not exist");
            }

            return user;
        }

        public async Task<Guid> CreateUser(RegisterUserDTO userDTO)
        {
            User user = _mapper.Map<User>(userDTO);
            user.PasswordHash = _passwordHasher.HashPassword(user, userDTO.Password);
            user.VerificationToken = Guid.NewGuid().ToString();
            user.RoleId = 1;
            _dbContext.User.Add(user);
            await _dbContext.SaveChangesAsync();
            return user.Id;
        }
        public async Task VerifyUser(string token)
        {
            User user = await _dbContext.User.FirstOrDefaultAsync(u => u.VerificationToken == token);
            if (user == null)
            {
                throw new BadRequestException("Invalid token");
            }
            user.IsVerified = true;
            user.VerificationToken = null;
            await _dbContext.SaveChangesAsync();
        }
        public async Task CreateResetPasswordToken(string email)
        {
            User user = _dbContext.User.FirstOrDefault(u => u.Email == email);
            if(user == null)
            {
                throw new BadRequestException("Invalid e-mail");
            }
            user.ResetPasswordToken = Guid.NewGuid().ToString();
            user.ResetPasswordTokenExpires = DateTime.Now.AddHours(1);
            await _dbContext.SaveChangesAsync();
        }
        public async Task ResetPassword(UserResetPasswordDTO userDTO)
        {
            User user = await _dbContext.User.FirstOrDefaultAsync(u => u.Email == userDTO.Email);
            if (user == null || user.ResetPasswordTokenExpires<=DateTime.Now || user.ResetPasswordToken != userDTO.Token)
            {
                throw new BadRequestException("Invalid e-mail or token");
            }
            user.PasswordHash = _passwordHasher.HashPassword(user, userDTO.Password);
            user.ResetPasswordToken = null;
            user.ResetPasswordTokenExpires = null;
            await _dbContext.SaveChangesAsync();
        }
        public async Task<string> GenerateJwtToken(UserLoginDTO userDTO)
        {
            User user = await _dbContext.User.Include(u => u.Role).FirstOrDefaultAsync(u =>u.Email == userDTO.Email);
            if (user == null)
            {
                throw new BadRequestException("Invalid e-mail or password");
            }
            if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, userDTO.Password) == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid e-mail or password");
            }
            if (!user.IsVerified)
            {
                throw new AccountNotVerifiedException("Account is not verified");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.Name),

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); 
            var expireHours = DateTime.Now.AddHours(_authenticationSettings.JwtExpireHours);
            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer,
                claims, expires: expireHours,
                signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);  
        }
        public async Task AddModeratorRole(Guid id)
        {
            User user = await _dbContext.User.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new NotFoundException("The user you are trying to get does not exist");
            }
            user.RoleId = 3; 
            await _dbContext.SaveChangesAsync();
        }
    }

    }

