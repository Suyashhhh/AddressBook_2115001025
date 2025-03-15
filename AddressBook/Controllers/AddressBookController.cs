using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;


namespace AddressBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressBookController : ControllerBase
    {
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
        public IActionResult Post(RequestModel requestModel)
        {
            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "Contact added successfully",
                Data = $"Name: {requestModel.key}, Phone: {requestModel.value}"
            };
            return Ok(responseModel);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, RequestModel requestModel)
        {
            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = $"Contact with ID {id} updated",
                Data = $"Updated Name: {requestModel.key}, Updated Phone: {requestModel.value}"
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