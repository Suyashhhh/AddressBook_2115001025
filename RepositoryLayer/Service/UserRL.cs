﻿using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using ModelLayer.Model;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly AppDbContext _context;

        public UserRL(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email) =>
            await _context.Users.SingleOrDefaultAsync(u => u.Email == email);

        public async Task<User?> GetByIdAsync(int userId) => 
            await _context.Users.FindAsync(userId);

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePasswordAsync(int userId, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.PasswordHash = newPassword;
                await _context.SaveChangesAsync();
            }
        }
    }
}
