using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;
using BusTicketingApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusTicketingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
      
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        [HttpPost("book-seats")]
        public async Task<ActionResult<string>> BookSeats([FromBody] SeatSelectionRequestDTO seatSelectionRequestDTO)
        {
            if (seatSelectionRequestDTO.SelectedSeatIds == null || !seatSelectionRequestDTO.SelectedSeatIds.Any())
            {
                return BadRequest("No seats selected.");
            }

            // You can call your booking service to handle the logic.
            var booking = await _bookingService.BookSeats(seatSelectionRequestDTO);

            if (booking == null)
            {
                return StatusCode(500, "Failed to book seats.");
            }

            return Ok(booking);
        }
    }

}
}
