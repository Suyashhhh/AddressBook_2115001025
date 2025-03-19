using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using ModelLayer.Model;

namespace RepositoryLayer.Service
{
    public class AddressBookRL : IAddressBookRL

    {
        private readonly AppDbContext _context;

        public AddressBookRL(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AddressBookEntry>> GetAllAsync() => await _context.AddressBookEntries.ToListAsync();

        public async Task<AddressBookEntry?> GetByIdAsync(int id) => await _context.AddressBookEntries.FindAsync(id);

        public async Task AddAsync(AddressBookEntry entry)
        {
            await _context.AddressBookEntries.AddAsync(entry);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AddressBookEntry entry)
        {
            _context.AddressBookEntries.Update(entry);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entry = await GetByIdAsync(id);
            if (entry != null)
            {
                _context.AddressBookEntries.Remove(entry);
                await _context.SaveChangesAsync();
            }
        }
    }
}
