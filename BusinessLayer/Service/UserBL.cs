using BusinessLayer.Helper;
using BusinessLayer.Interface;
using ModelLayer.DTO;
using RepositoryLayer.Interface;
using ModelLayer.Model;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL _userRepository;
        private readonly JwtHelper _jwtHelper;
        private readonly IRedisCacheService _redisCache;

        public UserBL(IUserRL userRepository, JwtHelper jwtHelper, IRedisCacheService redisCache)
        {
            _userRepository = userRepository;
            _jwtHelper = jwtHelper;
            _redisCache = redisCache;
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
                PasswordHash = hashedPassword
            };

            await _userRepository.AddUserAsync(user);
            string token = _jwtHelper.GenerateToken(user.Email, user.Id);

            await _redisCache.SetAsync($"user_{user.Email}", user, TimeSpan.FromMinutes(10));

            return new ApiResponse<string>(true, "User registered successfully", token);
        }

        public async Task<ApiResponse<string>> LoginUserAsync(LoginDto loginDto)
        {
            var cacheKey = $"user_{loginDto.Email}";
            var user = await _redisCache.GetAsync<User>(cacheKey);

            if (user == null)
            {
                user = await _userRepository.GetByEmailAsync(loginDto.Email);
                if (user != null)
                    await _redisCache.SetAsync(cacheKey, user, TimeSpan.FromMinutes(10));
            }

            if (user == null || !PasswordHasher.VerifyPassword(loginDto.Password, user.PasswordHash))
                return new ApiResponse<string>(false, "Invalid credentials");

            string token = _jwtHelper.GenerateToken(user.Email, user.Id);
            return new ApiResponse<string>(true, "Login successful", token);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var cacheKey = $"user_{email}";
            var user = await _redisCache.GetAsync<User>(cacheKey);

            if (user == null)
            {
                user = await _userRepository.GetByEmailAsync(email);
                if (user != null)
                    await _redisCache.SetAsync(cacheKey, user, TimeSpan.FromMinutes(10));
            }

            return user;
        }

        public async Task UpdatePasswordAsync(int userId, string newPassword)
        {
            await _userRepository.UpdatePasswordAsync(userId, newPassword);

            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                user.PasswordHash = PasswordHasher.HashPassword(newPassword);
                await _redisCache.SetAsync($"user_{user.Email}", user, TimeSpan.FromMinutes(10));
            }
        }
    }
}
