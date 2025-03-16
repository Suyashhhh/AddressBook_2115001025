using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using AutoMapper;
using RepositoryLayer.Entity;


namespace AddressBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressBookController : ControllerBase
    {
        private readonly IMapper _mapper;
        public AddressBookController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "AddressBook - Fetch all contacts",
                Data = "List of contacts displayed"
            };
            return Ok(responseModel);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = $"Contact with ID {id} fetched",
                Data = $"Contact Details for ID: {id}"
            };
            return Ok(responseModel);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddressBookModel addressBookModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addressBookEntity = _mapper.Map<AddressBookEntity>(addressBookModel); //mapping through automapper here to the entity

            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "Contact added successfully",
                Data = $"Name: {addressBookEntity.Name}, Phone: {addressBookEntity.PhoneNumber}" //displaying through entity to check correct mapping
                //as the values are being displayed from the Entity hence the AutoMapper is working correctly
            };

            return Ok(responseModel);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] AddressBookModel addressBookModel)
        {
            if (!ModelState.IsValid) // using fluent validation to check weather the Name or phone number is empty
            {
                return BadRequest(ModelState);
            }

            var addressBookEntity = _mapper.Map<AddressBookEntity>(addressBookModel);

            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = $"Contact with ID {id} updated",
                Data = $"Updated Name: {addressBookEntity.Name}, Updated Phone: {addressBookEntity.PhoneNumber}"
            };
            return Ok(responseModel);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = $"Contact with ID {id} deleted successfully",
                Data = "Deletion successful"
            };
            return Ok(responseModel);
        }
    }
}