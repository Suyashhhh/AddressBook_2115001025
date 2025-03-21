//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ModelLayer.Model;

//namespace RepositoryLayer.Interface
//{
//    public interface IUserRL
//    {
//        Task<User?> GetByEmailAsync(string email);
//        Task AddUserAsync(User user);

//        Task UpdatePasswordAsync(int userId, string newPassword);

//    }
//}
using ModelLayer.Model;

public interface IUserRL
{
    Task<User?> GetByEmailAsync(string email);
    Task AddUserAsync(User user);
    Task UpdatePasswordAsync(int userId, string newPassword);
    Task<User?> GetByIdAsync(int userId); 
}

