using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.DTO;
using RepositoryLayer.Interface;
using ModelLayer.Model;
using BusinessLayer.Interface;

namespace RepositoryLayer.Service
{
    public class AddressBookBL : IAddressBookBL

    {
        private readonly IAddressBookRL _repository;

        public AddressBookBL(IAddressBookRL repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AddressBookEntry>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<AddressBookEntry?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task AddAsync(AddressBookDto dto)
        {
            var entry = new AddressBookEntry
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address
            };
            await _repository.AddAsync(entry);
        }

        public async Task UpdateAsync(int id, AddressBookDto dto)
        {
            var entry = await _repository.GetByIdAsync(id);
            if (entry != null)
            {
                entry.Name = dto.Name;
                entry.Email = dto.Email;
                entry.Phone = dto.Phone;
                entry.Address = dto.Address;
                await _repository.UpdateAsync(entry);
            }
        }

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
    }


}

