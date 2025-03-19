using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Helper;
using BusinessLayer.Interface;
using ModelLayer.DTO;
using RepositoryLayer.Interface;
using ModelLayer.Model;

namespace BusinessLayer.Service
{
    public class UserBL:IUserBL
    {
        private readonly IUserRL _userRepository;
        private readonly JwtHelper _jwtHelper;

        public UserBL(IUserRL userRepository, JwtHelper jwtHelper)
        {
            _userRepository = userRepository;
            _jwtHelper = jwtHelper;
        }

        public async Task<ApiResponse<string>> RegisterUserAsync(UserDto userDto)
        {
            if (await _userRepository.GetByEmailAsync(userDto.Email) != null)
                return new ApiResponse<string>(false, "User already exists");

            
            string hashedPassword = PasswordHasher.HashPassword(userDto.Password);

            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                PasswordHash = hashedPassword // Store the hashed and salted password
            };

            await _userRepository.AddUserAsync(user);
            string token = _jwtHelper.GenerateToken(user.Email, user.Id);
            return new ApiResponse<string>(true, "User registered successfully", token);
        }

        public async Task<ApiResponse<string>> LoginUserAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            if (user == null || !PasswordHasher.VerifyPassword(loginDto.Password, user.PasswordHash))
                return new ApiResponse<string>(false, "Invalid credentials");

            string token = _jwtHelper.GenerateToken(user.Email, user.Id);
            return new ApiResponse<string>(true, "Login successful", token);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task UpdatePasswordAsync(int userId, string newPassword)
        {
            await _userRepository.UpdatePasswordAsync(userId, newPassword);
        }
    }
}
