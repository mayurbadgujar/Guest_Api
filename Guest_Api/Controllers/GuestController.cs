using Guest_Api.Model;
using Guest_Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace Guest_Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class GuestController : ControllerBase
    {

        private readonly GuestService _guestService;
        private readonly ILogger<GuestController> _logger;

        public GuestController(GuestService guestService, ILogger<GuestController> logger)
        {
            _guestService = guestService;
            _logger = logger;
        }

        [HttpPost("AddGuest")]
        public IActionResult AddGuest([FromBody] Guest guest)
        {
            if (string.IsNullOrWhiteSpace(guest.Firstname) && string.IsNullOrWhiteSpace(guest.Lastname))
            {
                return BadRequest("Please provide First Name or Last Name.");
            }
            if (guest.PhoneNumbers == null || guest.PhoneNumbers.Count == 0)
            {
                return BadRequest("Please provide at least one phone number.");
            }

            _logger.LogInformation("Adding a new guest...");
            Guest newGuest = null;
            try
            {
                newGuest = _guestService.AddGuest(guest);

            }
            catch (Exception ex) { }
            return Ok(newGuest);

        }

        [HttpPost("AddPhone/{id}")]
        public IActionResult AddPhone(Guid id, [FromBody] string phoneNumber)
        {
            _logger.LogInformation($"Adding phone number for guest with ID {id}.");
            try
            {
                if (!_guestService.AddPhone(id, phoneNumber))
                {
                    return BadRequest("Phone number is already exists or guest not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}", ex);
            }

            return Ok("Phone number added successfully.");
        }

        [HttpGet("GetGuestById/{id}")]
        public IActionResult GetGuestById(Guid id)
        {
            _logger.LogInformation($"Getting guest by ID {id}.");

            var guest = _guestService.GetGuestById(id);
            if (guest == null)
            {
                return NotFound();
            }

            return Ok(guest);
        }

        [HttpGet("GetAllGuests")]
        public IActionResult GetAllGuests()
        {
            _logger.LogInformation("Getting all guests.");

            var guests = _guestService.GetAllGuests();
            return Ok(guests);
        }
    }
}
