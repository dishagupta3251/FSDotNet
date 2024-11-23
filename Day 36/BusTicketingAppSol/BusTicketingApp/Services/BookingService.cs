using AutoMapper;
using BusTicketingApp.EmailInterface;
using BusTicketingApp.EmailModels;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;
using Microsoft.AspNetCore.Routing;




namespace BusTicketingApp.Services
{
    public class BookingService:IBookingService
    {
        private readonly ISeatService _seatService;
        private readonly IBusService _busService;
        private readonly IRoutingService _routingService;
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        private readonly ILogger<BookingService> _logger;
        private readonly IRepository<Booking,int> _repository;
        private readonly IRepository<SeatsBooked, int> _seatsBookedRepository;
        private readonly IEmailSender _emailSender;

        public BookingService(ISeatService seatService, IBusService busService, IRoutingService routingService, IRepository<Booking,int> repository,IPaymentService paymentService,IMapper mapper,ILogger<BookingService> logger, IRepository<SeatsBooked, int> seatsBookedRepository, IEmailSender emailSender,ICustomerService customerService)
        {

            _busService = busService;
            _seatService = seatService;
            _routingService = routingService;
            _repository = repository;
            _seatsBookedRepository = seatsBookedRepository;
            _mapper=mapper;
            _logger = logger;
            _customerService = customerService;
            _paymentService = paymentService;
            _emailSender = emailSender;
        }

       

        public async Task<IEnumerable<BusResponseDTO>> GetAllBusesOnRoute(string from, string to, DateTime dateTime, int pagenum, int pagesize)
        {
            try
            {
                var routes = (await _routingService.GetAllRoutes()).ToList();
                var checkSource = routes.FirstOrDefault(r => r.Origin == from).Origin ?? throw new Exception("Source not available");
                var checkDestination = routes.FirstOrDefault(l => l.Destination == to).Destination ?? throw new Exception("Destination not available");

                var routeId = await _routingService.GetIdByJourney(checkSource, checkDestination);
                var buses =await _busService.GetBusesByRouteAndDay(routeId, dateTime);

                List<BusResponseDTO> result = new List<BusResponseDTO>();
                foreach (var bus  in buses)
                {
                    var seats = (await _seatService.GetAllSeats()).Where(s => s.IsBooked == false && s.BusId==bus.BusId);
                    var response = new BusResponseDTO()
                    {
                        BusId=bus.BusId,
                        BusNumber =bus.BusNumber,
                        BusType = bus.BusType.ToString(),
                        SeatsLeft = seats.Count(),
                        Status = bus.Status.ToString(),
                        StandardFare = bus.StandardFare,
                        PremiumFare = bus.PremiumFare,
                        JourneyDetails=from.ToUpper()+" "+"TO"+" "+to.ToUpper(),
                    };
                    result.Add(response);
                }
                pagenum = Math.Max(pagenum, 1);
                pagesize = Math.Max(pagesize, 5);

                int total = result.Count();
                int pageTotal = (int)Math.Ceiling((double)total / pagesize);

                var returnBuses =       result
                                        .Skip((pagenum - 1) * pagesize)
                                        .Take(pagesize)
                                        .ToList();



                return returnBuses;
                

            }
            catch
            {
                throw new Exception("Any of the entries is wrong");
            }

        }

        public Task<BusWithSeatsResponseDTO> ViewBusDetailsAlongWithSeats(int busId)
        {
            try
            {
                var busDetails = _busService.GetBusWithSeats(busId);
                return busDetails;
            }
            catch
            {
                throw new Exception("Cannot retrieve bus details");
            }

        }




      
       
        public async Task<int> BookingConfirmation(SeatSelectionRequestDTO seatSelectionRequestDTO)
        {
            try
            {
            var bus = await _busService.GetBus(seatSelectionRequestDTO.BusId);
            decimal calculateCost = 0; string seatsBookedForCustomer="";

                var seatsId = seatSelectionRequestDTO.SelectedSeatIds;
                foreach (var id in seatsId)
            {
                var seat =await _seatService.SeatById(id);
                calculateCost = calculateCost + seat.Price;
            }
          
           
            foreach (var id in seatsId){
                   
                    seatsBookedForCustomer = seatsBookedForCustomer+","+ id;
                   

            }
            var booking = new Booking()
            {
                BusId=bus.BusId,
                BookingDate=DateTime.Now,
                BusNumber = bus.BusNumber,
                RouteId = bus.RouteId,
                BookedForDate = seatSelectionRequestDTO.DateTime,
                BookedForDay = (DaysOfWeek)seatSelectionRequestDTO.DateTime.DayOfWeek,
                BookedSeats = seatsBookedForCustomer,
                TotalFare = calculateCost,
                IsConfirmed = "Pending",
                CustomerId = seatSelectionRequestDTO.CustomerId,
                


            };
                var customerEmail=(await _customerService.GetCustomerById(booking.CustomerId)).Email;

              

                var addedBooking=await _repository.Add(booking);
               foreach(var id in seatsId)
                {
                    var seatsBooked = new SeatsBooked
                    {
                        SeatId = id,
                        CustomerId = seatSelectionRequestDTO.CustomerId,
                        BusId = seatSelectionRequestDTO.BusId,
                        BookingId = booking.BookingId,
                        SeatStatus = 0
                    };
                    await _seatsBookedRepository.Add(seatsBooked);
                }
                string body = $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{ font-family: Arial, sans-serif; color: #333; line-height: 1.6; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; background-color: #f9f9f9; border: 1px solid #ddd; border-radius: 8px; }}
        .header {{ text-align: center; padding-bottom: 20px; }}
        .header h1 {{ color: #4CAF50; }}
        .details {{ margin-top: 20px; }}
        .footer {{ text-align: center; font-size: 0.9em; margin-top: 20px; color: #666; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>Booking Successfull!</h1>
        </div>
        <p>Dear Customer,</p>
        <p>Confirmation upon payment . Below are your booking details:</p>
        <div class='details'>
            <p><strong>Booking ID:</strong> {addedBooking.BookingId}</p>
            <p><strong>Bus Number:</strong> {booking.BusNumber}</p>
            <p><strong>Source:</strong> {(await _routingService.GetRoute(booking.RouteId)).Origin}</p>
            <p><strong>Destination:</strong> {(await _routingService.GetRoute(booking.RouteId)).Destination}</p>
            <p><strong>Booked Date:</strong> {booking.BookedForDate:yyyy-MM-dd}</p>
            <p><strong>Total Fare:</strong> Rs.{booking.TotalFare:F2}</p>
        </div>
        <p>Thank you for choosing our service. Have a great journey!</p>
        <div class='footer'>
            <p>Regards,</p>
            <p><strong>The Bus Ticketing Team</strong></p>
        </div>
    </div>
</body>
</html>";




              await  SendMail(customerEmail, "Booking Confirmation Upon Payment", body);

                return booking.BookingId; 
            }
            catch
            {
                throw new Exception("Cannot add booking");
            }
        }

          public async Task<BookingResponseDTO> PaymentIntiation(PaymentRequestDTO paymentRequestDTO)
        {
            try
            {
                var addPayment = _mapper.Map<Payment>(paymentRequestDTO);
               

                //updating booking confirm status
                var booking = await _repository.Get(paymentRequestDTO.BookingId);
                booking.IsConfirmed = "Confirmed";
                await _repository.Update(booking, booking.BookingId);

                addPayment.TotalFare = booking.TotalFare;
                var payment = await _paymentService.AddPayment(addPayment);

                if (payment == null) throw new Exception("Cannot retrive  add payment");


               

                var seats=(await _seatsBookedRepository.GetAll()).Where(s=>s.BookingId==payment.BookingId);

                //creating seatresponseDTO to response to api call
                List<SeatsResponseDTO> listOfSeatsDTO = new List<SeatsResponseDTO>();

                //updating seats status
                foreach( var seat_booked in seats)
                {

                    seat_booked.SeatStatus = Status.Confirmed;
                    var seat=await _seatService.SeatById(seat_booked.SeatId);
                    seat.IsBooked = true;
                    await _seatService.UpdateSeatStatus(seat.SeatsId);
                    var seatResponseDTO = new SeatsResponseDTO()
                    {
                        SeatId = seat_booked.Id,
                        Seat = seat.SeatNumber + seat.SeatType.ToString(),
                        Price= seat.Price,
                    };
                    listOfSeatsDTO.Add(seatResponseDTO);
                }

               

                var route =await _routingService.GetRoute(booking.RouteId);
                var response = new BookingResponseDTO()
                {
                    BookingId = booking.BookingId,
                    BookingDate = booking.BookingDate,
                    BusNumber = booking.BusNumber,
                    Source = route.Origin,
                    Destination = route.Destination,
                    CustomerId = booking.CustomerId,
                    BookedForDate = booking.BookedForDate,
                    BookedForDay = booking.BookedForDay,
                    SeatsBooked = listOfSeatsDTO,
                    TotalFare = booking.TotalFare,
                    IsConfirmed = booking.IsConfirmed
                };
                string body = $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{ font-family: Arial, sans-serif; color: #333; line-height: 1.6; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; background-color: #f9f9f9; border: 1px solid #ddd; border-radius: 8px; }}
        .header {{ text-align: center; padding-bottom: 20px; }}
        .header h1 {{ color: #4CAF50; }}
        .details {{ margin-top: 20px; }}
        .footer {{ text-align: center; font-size: 0.9em; margin-top: 20px; color: #666; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>Payment Successful!</h1>
        </div>
        <p>Dear Customer,</p>
        <p>We have successfully received your payment. Your booking details are as follows:</p>
        <div class='details'>
            <p><strong>Payment ID:</strong> {payment.Id}</p>
            <p><strong>Booking ID:</strong> {booking.BookingId}</p>
            <p><strong>Amount Paid:</strong> Rs.{booking.TotalFare:F2}</p>
            <p><strong>Seats Booked:</strong> {booking.BookedSeats}</p>
        </div>
        <p>Thank you for your payment. You are all set for your journey!</p>
        <div class='footer'>
            <p>Safe travels,</p>
            <p><strong>The Bus Ticketing Team</strong></p>
        </div>
    </div>
</body>
</html>";
                await SendMail((await _customerService.GetCustomerById(booking.CustomerId)).Email,"Thank You for Your Payment! 🚀 Journey Secured", body);
                return response;
                




            }
            catch
            {
                throw new Exception();
            }
           

        }
        public async Task<BookingHistoryDTO> BookingHistory(int customerId)
        {
            try
            {
                var bookings = (await _repository.GetAll()).Where(b => b.CustomerId == customerId);
                var pastBookings = bookings
                                  .Where(b => b.BookingDate < DateTime.UtcNow)
                                  .ToList();

                var upcomingBookings = bookings
                    .Where(b => b.BookingDate >= DateTime.UtcNow)
                    .ToList();

                string body= $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{ font-family: Arial, sans-serif; color: #333; line-height: 1.6; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; background-color: #f9f9f9; border: 1px solid #ddd; border-radius: 8px; }}
        .header {{ text-align: center; padding-bottom: 20px; }}
        .header h1 {{ color: #4CAF50; }}
        .details {{ margin-top: 20px; }}
        .footer {{ text-align: center; font-size: 0.9em; margin-top: 20px; color: #666; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>Your Booking History</h1>
        </div>
        <p>Dear Customer,</p>
        <p>Here are your booking details:</p>
        <div class='details'>
            <h2>Past Bookings</h2>
            {string.Join("<br>", pastBookings.Select(b => $"Booking ID: {b.BookingId}, Date: {b.BookedForDate:yyyy-MM-dd}, Bus: {b.BusNumber}"))}
            <h2>Upcoming Bookings</h2>
            {string.Join("<br>", upcomingBookings.Select(b => $"Booking ID: {b.BookingId}, Date: {b.BookedForDate:yyyy-MM-dd}, Bus: {b.BusNumber}"))}
        </div>
        <p>We hope to serve you again soon!</p>
        <div class='footer'>
            <p>Thank you,</p>
            <p><strong>The Bus Ticketing Team</strong></p>
        </div>
    </div>
</body>
</html>";

                await SendMail((await _customerService.GetCustomerById(customerId)).Email, "Your Bus Booking History 🗓️", body);



                return new BookingHistoryDTO
                {
                    PastBookings = pastBookings,
                    UpcomingBookings = upcomingBookings
                };

            }
            catch
            {
                throw new Exception("No bookings");
            }
        }

        private async Task SendMail(string mailTo, string title, string body)
        {
            var rng = new Random();
            var message = new Message(new string[] {
                        mailTo },
                    title,
                    body);
            _emailSender.SendEmail(message);
        }

       
    }
}
