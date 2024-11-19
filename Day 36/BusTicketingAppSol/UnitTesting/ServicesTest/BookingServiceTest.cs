using AutoMapper;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models.DTO;
using BusTicketingApp.Models;
using Microsoft.Extensions.Logging;
using BusTicketingApp.EmailInterface;
using BusTicketingApp.EmailModels;

public class BookingService 
{
    private readonly ISeatService _seatService;
    private readonly IBusService _busService;
    private readonly IRoutingService _routingService;
    private readonly IPaymentService _paymentService;
    private readonly IMapper _mapper;
    private readonly ICustomerService _customerService;
    private readonly ILogger<BookingService> _logger;
    private readonly IRepository<Booking, int> _repository;
    private readonly IRepository<SeatsBooked, int> _seatsBookedRepository;
    private readonly IEmailSender _emailSender;

    public BookingService(ISeatService seatService, IBusService busService, IRoutingService routingService,
                          IRepository<Booking, int> repository, IPaymentService paymentService,
                          IMapper mapper, ILogger<BookingService> logger,
                          IRepository<SeatsBooked, int> seatsBookedRepository,
                          IEmailSender emailSender, ICustomerService customerService)
    {
        _seatService = seatService;
        _busService = busService;
        _routingService = routingService;
        _repository = repository;
        _seatsBookedRepository = seatsBookedRepository;
        _mapper = mapper;
        _logger = logger;
        _customerService = customerService;
        _paymentService = paymentService;
        _emailSender = emailSender;
    }

    public async Task<IEnumerable<BusResponseDTO>> GetAllBusesOnRoute(string from, string to, DateTime dateTime)
    {
        try
        {
            _logger.LogInformation("Fetching buses from {from} to {to} on {dateTime}", from, to, dateTime);

            var routes = (await _routingService.GetAllRoutes()).ToList();
            var checkSource = routes.FirstOrDefault(r => r.Origin == from)?.Origin
                ?? throw new Exception("Source not available");
            var checkDestination = routes.FirstOrDefault(l => l.Destination == to)?.Destination
                ?? throw new Exception("Destination not available");

            var routeId = await _routingService.GetIdByJourney(checkSource, checkDestination);
            var buses = await _busService.GetBusesByRouteAndDay(routeId, dateTime);

            List<BusResponseDTO> result = new List<BusResponseDTO>();
            foreach (var bus in buses)
            {
                var seats = (await _seatService.GetAllSeats()).Where(s => !s.IsBooked && s.BusId == bus.BusId);
                var response = new BusResponseDTO()
                {
                    BusId = bus.BusId,
                    BusNumber = bus.BusNumber,
                    BusType = bus.BusType.ToString(),
                    SeatsLeft = seats.Count(),
                    Status = bus.Status.ToString(),
                    StandardFare = bus.StandardFare,
                    PremiumFare = bus.PremiumFare,
                    JourneyDetails = $"{from.ToUpper()} TO {to.ToUpper()}",
                };
                result.Add(response);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching buses from {from} to {to}", from, to);
            throw new Exception("An error occurred while fetching bus details");
        }
    }

    public Task<BusWithSeatsResponseDTO> ViewBusDetailsAlongWithSeats(int busId)
    {
        try
        {
            _logger.LogInformation("Fetching bus details for BusId: {busId}", busId);
            var busDetails = _busService.GetBusWithSeats(busId);
            return busDetails;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching bus details for BusId: {busId}", busId);
            throw new Exception("Cannot retrieve bus details");
        }
    }

    public async Task<int> BookingConfirmation(SeatSelectionRequestDTO seatSelectionRequestDTO)
    {
        try
        {
            _logger.LogInformation("Booking confirmation started for customer: {customerId}", seatSelectionRequestDTO.CustomerId);

            var bus = await _busService.GetBus(seatSelectionRequestDTO.BusId);
            decimal calculateCost = 0;
            string seatsBookedForCustomer = "";

            var seatsId = seatSelectionRequestDTO.SelectedSeatIds;
            foreach (var id in seatsId)
            {
                var seat = await _seatService.SeatById(id);
                calculateCost += seat.Price;
                seatsBookedForCustomer = $"{seatsBookedForCustomer},{id}";
            }

            var booking = new Booking()
            {
                BusId = bus.BusId,
                BookingDate = DateTime.Now,
                BusNumber = bus.BusNumber,
                RouteId = bus.RouteId,
                BookedForDate = seatSelectionRequestDTO.DateTime,
                BookedForDay = (DaysOfWeek)seatSelectionRequestDTO.DateTime.DayOfWeek,
                BookedSeats = seatsBookedForCustomer,
                TotalFare = calculateCost,
                IsConfirmed = "Pending",
                CustomerId = seatSelectionRequestDTO.CustomerId,
            };

            var customerEmail = (await _customerService.GetCustomerById(booking.CustomerId)).Email;
            var addedBooking = await _repository.Add(booking);

            foreach (var id in seatsId)
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

            string body = $@"<html><body>Booking Confirmation Email Body</body></html>";
            await SendMail(customerEmail, "Booking Confirmation Upon Payment", body);

            _logger.LogInformation("Booking confirmation successful for customer: {customerId}, BookingId: {addedBooking.BookingId}");
            return booking.BookingId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during booking confirmation for customer: {customerId}", seatSelectionRequestDTO.CustomerId);
            throw new Exception("Cannot add booking");
        }
    }

    public async Task<BookingResponseDTO> PaymentIntiation(PaymentRequestDTO paymentRequestDTO)
    {
        try
        {
            _logger.LogInformation("Payment initiation started for BookingId: {bookingId}", paymentRequestDTO.BookingId);

            var payment = await _paymentService.AddPayment(_mapper.Map<Payment>(paymentRequestDTO));

            if (payment == null) throw new Exception("Payment could not be processed");

            var seats = (await _seatsBookedRepository.GetAll()).Where(s => s.BookingId == payment.BookingId).ToList();

            List<SeatsResponseDTO> listOfSeatsDTO = new List<SeatsResponseDTO>();
            foreach (var seatBooked in seats)
            {
                seatBooked.SeatStatus = Status.Confirmed;
                var seat = await _seatService.SeatById(seatBooked.SeatId);
                seat.IsBooked = true;
                await _seatService.UpdateSeatStatus(seat.SeatsId);

                listOfSeatsDTO.Add(new SeatsResponseDTO
                {
                    SeatId = seatBooked.Id,
                    Seat = $"{seat.SeatNumber}{seat.SeatType}",
                    Price = seat.Price,
                });
            }

            var booking = await _repository.Get(paymentRequestDTO.BookingId);
            booking.IsConfirmed = "Confirmed";
            await _repository.Update(booking, booking.BookingId);

            var route = await _routingService.GetRoute(booking.RouteId);
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

            string body = $@"<html><body>Payment Success Email Body</body></html>";
            await SendMail((await _customerService.GetCustomerById(booking.CustomerId)).Email, "Thank You for Your Payment!", body);

            _logger.LogInformation("Payment initiation successful for BookingId: {bookingId}", booking.BookingId);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during payment initiation for BookingId: {bookingId}", paymentRequestDTO.BookingId);
            throw new Exception("Payment initiation failed");
        }
    }

    private async Task SendMail(string mailTo, string title, string body)
    {
        try
        {
            _logger.LogInformation("Sending email to {mailTo} with title: {title}", mailTo, title);
            var message = new Message(new string[] { mailTo }, title, body);
            _emailSender.SendEmail(message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending email to {mailTo} with title: {title}", mailTo, title);
            throw new Exception("Error sending email");
        }
    }
}
