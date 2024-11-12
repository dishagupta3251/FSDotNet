using BusTicketingApp.Interfaces;
using BusTicketingApp.Models.DTO;
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

        [HttpGet("history")]
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

        [HttpGet("BusesOnRoute")]
        public async Task<ActionResult> GetBusesOnRoute(string from,  string to,  DateTime dateTime)
        {
            try
            {
                var buses = await _bookingService.GetAllBusesOnRoute(from, to, dateTime);
                return Ok(buses);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("payment")]
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

        [HttpGet("bus-details")]
        public async Task<ActionResult> GetBusDetailsWithSeats(int busId)
        {
            try
            {
                var busDetails = await _bookingService.ViewBusDetailsAlongWithSeats(busId);
                return Ok(new {
                    message="A: Aisle W:Window",
                    data=busDetails});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("confirm")]
        public async Task<ActionResult> ConfirmBooking(SeatSelectionRequestDTO seatSelectionRequest, DateTime date)
        {
            try
            {
                var bookingConfirmationId = await _bookingService.BookingConfirmation(seatSelectionRequest, date);
                return Ok(
                    new
                    {
                        

                        message="Your booking is pending, once payment is done it is confirmed. Please refer to your booking id to make payment",
                        id=bookingConfirmationId
                    }
                    );
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
