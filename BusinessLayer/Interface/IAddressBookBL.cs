using System.Collections.Generic;
using ModelLayer.Models;

namespace BusinessLayer.Interface
{
    public interface IAddressBookBL
    {
        public ResponseAddressBookModel Add(RequestModel requestModel);
        ResponseAddressBookModel Update(int id, ResponseAddressBookModel addressBookModel);
        ResponseAddressBookModel GetById(int id);
        IEnumerable<ResponseAddressBookModel> GetAll();
        bool Delete(int id);
    }
}