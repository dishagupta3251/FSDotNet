using BusTicketingApp.Interfaces;
using BusTicketingApp.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusTicketingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        
        [HttpGet("SeeAllBuses")]
        //[Authorize(Roles = "Customer")]
        public async Task<ActionResult> GetBusesOnRoute(string from, string to, DateTime dateTime, int pagenum, int pagesize)
        {
            try
            {
                var buses = await _bookingService.GetAllBusesOnRoute(from, to, dateTime,  pagenum,  pagesize);
                return Ok(buses);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpGet("busAndSeatsDetails")]
        //[Authorize(Roles = "Customer")]
        public async Task<ActionResult> GetBusDetailsWithSeats(int busId)
        {
            try
            {
                var busDetails = await _bookingService.ViewBusDetailsAlongWithSeats(busId);
                return Ok(new
                {
                    message = "A: Aisle W:Window",
                    data = busDetails
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

       
        [HttpPost("BookBus")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult> Booking(SeatSelectionRequestDTO seatSelectionRequest)
        {
            try
            {
                var bookingConfirmationId = await _bookingService.BookingConfirmation(seatSelectionRequest);
                return Ok(
                    new
                    {


                        message = "Your booking is pending, once payment is done it is confirmed. Please refer to your booking id to make payment",
                        id = bookingConfirmationId
                    }
                    );
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }




       
        [HttpPost("payment")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult> InitiatePayment(PaymentRequestDTO paymentRequest)
        {
            try
            {
                var paymentResponse = await _bookingService.PaymentIntiation(paymentRequest);
                return Ok(paymentResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpGet("history")]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult> GetBookingHistory(int customerId)
        {
            try
            {
                var bookingHistory = await _bookingService.BookingHistory(customerId);
                return Ok(bookingHistory);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
