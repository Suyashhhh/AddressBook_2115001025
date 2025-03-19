using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using RepositoryLayer.Interface;
using ModelLayer.Model;
using BusinessLayer.Interface;

namespace AddressBookApplication.Controllers
{
    [ApiController]
    [Route("api/addressbook")]
    [Authorize]
    public class AddressBookController : ControllerBase
    {
        private readonly IAddressBookBL _addressBookBL;

        public AddressBookController(IAddressBookBL addressBookService)
        {
            _addressBookBL = addressBookService;
        }

        private int GetAuthenticatedUserId()
        {
            var token = Request.Headers["Authorization"].ToString();
            Console.WriteLine($"🔹 Received Token: {token}");

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                Console.WriteLine("❌ Token is missing or invalid.");
                throw new UnauthorizedAccessException("Token is missing or invalid.");
            }

            Console.WriteLine($"✅ Extracted User ID: {userIdClaim}");
            return int.Parse(userIdClaim);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var contacts = await _addressBookBL.GetAllAsync();
                return Ok(new ApiResponse<IEnumerable<AddressBookEntry>>(true, "Contacts retrieved successfully", contacts));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ApiResponse<string>(false, ex.Message, null));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(int id)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var contact = await _addressBookBL.GetByIdAsync(id);
                if (contact == null)
                    return NotFound(new ApiResponse<string>(false, "Contact not found", null));

                return Ok(new ApiResponse<AddressBookEntry>(true, "Contact retrieved successfully", contact));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ApiResponse<string>(false, ex.Message, null));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] AddressBookDto dto)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                await _addressBookBL.AddAsync(dto);
                return CreatedAtAction(nameof(GetAllContacts), new ApiResponse<string>(true, "Contact added successfully", null));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ApiResponse<string>(false, ex.Message, null));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] AddressBookDto dto)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                await _addressBookBL.UpdateAsync(id, dto);
                return Ok(new ApiResponse<string>(true, "Contact updated successfully", null));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ApiResponse<string>(false, ex.Message, null));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                await _addressBookBL.DeleteAsync(id);
                return Ok(new ApiResponse<string>(true, "Contact deleted successfully", null));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ApiResponse<string>(false, ex.Message, null));
            }
        }
    }
}
