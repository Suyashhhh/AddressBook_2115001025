using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.DTO;
using ModelLayer.Model;

namespace BusinessLayer.Interface
{
    public interface IAddressBookBL
    {
        Task<IEnumerable<AddressBookEntry>> GetAllAsync();
        Task<AddressBookEntry?> GetByIdAsync(int id);
        Task AddAsync(AddressBookDto dto);
        Task UpdateAsync(int id, AddressBookDto dto);
        Task DeleteAsync(int id);
    }
}
