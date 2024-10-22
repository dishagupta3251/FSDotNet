using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Models.DTO;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService) { 
            _bookingService = bookingService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBooking(BookingDTO bookingDTO)
        {
            try
            {
                var booking= await _bookingService.Add(bookingDTO);
                return Ok(booking);

            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            try
            {
                var bookings =await _bookingService.GetAll();
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                    return StatusCode(500, ex.Message);
            }
        }

    }
}
