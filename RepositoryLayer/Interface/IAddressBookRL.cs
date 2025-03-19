using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;

namespace RepositoryLayer.Interface
{
    public interface IAddressBookRL
    {
        Task<IEnumerable<AddressBookEntry>> GetAllAsync();
        Task<AddressBookEntry?> GetByIdAsync(int id);
        Task AddAsync(AddressBookEntry entry);
        Task UpdateAsync(AddressBookEntry entry);
        Task DeleteAsync(int id);
    }
}
