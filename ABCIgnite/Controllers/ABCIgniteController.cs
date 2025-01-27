using ABCIgnite.datab;
using ABCIgnite.DTO;
using ABCIgnite.Service;
using ABCIgnite.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABCIgnite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ABCIgniteController : ControllerBase
    {
        private readonly IABCService _aBCService;

        public ABCIgniteController(IABCService aBCService)
        {
             _aBCService = aBCService;
        }

        [HttpGet("getAllGymClasses")]
        public async Task<IActionResult> GetAllGymClasses()
        {
            try
            {
                var gymClasses = await _aBCService.GetAllGymClasses(); // Call service to get gym classes
                return Ok(gymClasses); // Return the mapped DTOs with HTTP 200 status
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving gym classes: " + ex.Message); // Handle errors
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateClass([FromBody] ClassDTO classDTO)
        {
            try
            {
                var createdClass = await _aBCService.CreateClassAsync(classDTO);
                return  Ok();

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getAllBookings")]
        public async Task<IActionResult> GetAllBookingsWithClassInfoAsync()
        {
            try
            {
                var gymClasses = await _aBCService.GetAllBookingsWithClassInfoAsync(); // Call service to get gym classes
                return Ok(gymClasses); // Return the mapped DTOs with HTTP 200 status
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving Bookings: " + ex.Message); // Handle errors
            }
        }

        [HttpGet("GetBookingsByMemberName")]
        public async Task<IActionResult> GetBookingsByMemberName(string memberName)
        {
            try
            {
                var gymClasses = await _aBCService.GetBookingsByMemberName(memberName); // Call service to get gym classes
                return Ok(gymClasses); // Return the mapped DTOs with HTTP 200 status
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving Bookings: " + ex.Message); // Handle errors
            }
        }

        [HttpGet("GetBookingsByDateRange")]
        public async Task<IActionResult> GetBookingsByDateRange([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
        {
            try
            {
                // Call the service method to get bookings within the specified date range
                var bookings = await _aBCService.GetBookingsByDateRangeAsync(startDate, endDate);
                return Ok(bookings); // Return the filtered bookings as a response
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

       

        [HttpGet("bookings")]
        public async Task<IActionResult> GetBookingsWithClassInfo([FromQuery] string memberName, [FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
        {
            try
            {
                // Call the service method to get bookings with class information for the given member and date range
                var bookings = await _aBCService.GetBookingsWithClassInfoAsync(memberName, startDate, endDate);
                return Ok(bookings); // Return the filtered bookings as a response
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("createBooking")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDTO bookingDto)
        {
            try
            {
                // Validate the incoming booking model (you can do more validations here if needed)
                if (bookingDto == null)
                {
                    return BadRequest("Booking data is required.");
                }

                // Create the booking through the service layer
                var createdBooking = await _aBCService.CreateAsync(bookingDto);

                // Return the created booking with a 201 status code
                return CreatedAtAction(nameof(CreateBooking), new { id = createdBooking.BookingId }, createdBooking);
            }
            catch (Exception ex)
            {
                // Return an error response if something goes wrong
                return BadRequest(new { message = ex.Message });
            }
        }
    }


   


}
