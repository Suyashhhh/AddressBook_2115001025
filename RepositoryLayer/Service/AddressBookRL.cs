using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Services
{
    public class AddressBookRL : IAddressBookRL
    {
        private readonly AddressBookContext _context;

        public AddressBookRL(AddressBookContext context)
        {
            _context = context;
        }

        public AddressBookEntity Add(AddressBookEntity addressBookEntity)
        {
            // Check if the UserId exists in the Users table
            //var userExists = _context.Users.Any(u => u.UserId == addressBookEntity.UserId);

            // Add the address book entry
            _context.AddressBookEntries.Add(addressBookEntity);
            _context.SaveChanges();

            return addressBookEntity;
        }


        public AddressBookEntity Update(int id, AddressBookEntity addressBookEntity)
        {
            var existingEntity = _context.AddressBookEntries.FirstOrDefault(c => c.Id == id);
            if (existingEntity != null)
            {
                existingEntity.Name = addressBookEntity.Name;
                existingEntity.PhoneNumber = addressBookEntity.PhoneNumber;
                existingEntity.Email = addressBookEntity.Email;
                existingEntity.Address = addressBookEntity.Address;

                _context.AddressBookEntries.Update(existingEntity);
                _context.SaveChanges();
            }
            return existingEntity;
        }

        public AddressBookEntity GetById(int id)
        {
            return _context.AddressBookEntries.Find(id);
        }

        public IEnumerable<AddressBookEntity> GetAll()
        {
            return _context.AddressBookEntries.ToList();
        }

        public bool Delete(int id)
        {
            var entity = _context.AddressBookEntries.Find(id);
            if (entity != null)
            {
                _context.AddressBookEntries.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}