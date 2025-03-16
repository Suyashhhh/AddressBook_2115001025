using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface IAddressBookRL
    {
        AddressBookEntity Add(AddressBookEntity addressBookEntity);
        AddressBookEntity Update(int id, AddressBookEntity addressBookEntity);
        AddressBookEntity GetById(int id);
        IEnumerable<AddressBookEntity> GetAll();
        bool Delete(int id);
    }
}