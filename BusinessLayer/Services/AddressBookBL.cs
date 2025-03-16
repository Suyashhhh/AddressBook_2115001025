using AutoMapper;
using BusinessLayer.Interface;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using Newtonsoft.Json;

namespace BusinessLayer.Services
{
    public class AddressBookBL : IAddressBookBL
    {
        private readonly IAddressBookRL _addressBookRL;
        private readonly IMapper _mapper;

        public AddressBookBL(IAddressBookRL addressBookRL, IMapper mapper)
        {
            _addressBookRL = addressBookRL;
            _mapper = mapper;
        }
        public ResponseAddressBookModel Add(RequestModel requestModel)
        {
            var entity = _mapper.Map<AddressBookEntity>(requestModel);
            var addedEntity = _addressBookRL.Add(entity);


            return _mapper.Map<ResponseAddressBookModel>(addedEntity);
        }





        public ResponseAddressBookModel Update(int id, ResponseAddressBookModel addressBookModel)
        {
            var addressBookEntity = _mapper.Map<AddressBookEntity>(addressBookModel);
            var updatedEntity = _addressBookRL.Update(id, addressBookEntity);
            addressBookEntity.Id = id;  // Ensure ID consistency
            return _mapper.Map<ResponseAddressBookModel>(updatedEntity);
        }

        public ResponseAddressBookModel GetById(int id)
        {
            var addressBookEntity = _addressBookRL.GetById(id);
            return _mapper.Map<ResponseAddressBookModel>(addressBookEntity);
        }

        public IEnumerable<ResponseAddressBookModel> GetAll()
        {
            var entities = _addressBookRL.GetAll();
            return _mapper.Map<IEnumerable<ResponseAddressBookModel>>(entities);
        }

        public bool Delete(int id)
        {
            return _addressBookRL.Delete(id);
        }
    }
}