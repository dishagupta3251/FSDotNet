using AutoMapper;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;




namespace BusTicketingApp.Services
{
    public class BookingService:IBookingService
    {
        private readonly ISeatService _seatService;
        private readonly IBusService _busService;
        private readonly IRoutingService _routingService;
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        private readonly ILogger<BookingService> _logger;
        private readonly IRepository<Booking,int> _repository;
        private readonly IRepository<SeatsBooked, int> _seatsBookedRepository;

        public BookingService(ISeatService seatService, IBusService busService, IRoutingService routingService, IRepository<Booking,int> repository,IPaymentService paymentService,IMapper mapper,ILogger<BookingService> logger)
        {

            _busService = busService;
            _seatService = seatService;
            _routingService = routingService;
            _repository = repository;
            _mapper=mapper;
            _logger = logger;
            _paymentService = paymentService;
        }

        public async Task<BookingHistoryDTO> BookingHistory(int customerId)
        {
            try
            {
                var bookings=(await _repository.GetAll()).Where(b=>b.CustomerId==customerId);
                var pastBookings = bookings
                                  .Where(b => b.BookingDate < DateTime.UtcNow)
                                  .ToList();

                var upcomingBookings = bookings
                    .Where(b => b.BookingDate >= DateTime.UtcNow)
                    .ToList();
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

        public async Task<IEnumerable<BusResponseDTO>> GetAllBusesOnRoute(string from, string to, DateTime dateTime)
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
                    var seats = (await _seatService.GetAllSeats()).Where(s => s.IsBooked == false);
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

                
                return result;
                

            }
            catch
            {
                throw new Exception("Any of the entries is wrong");
            }

        }



       

        public async Task<BookingResponseDTO> PaymentIntiation(PaymentRequestDTO paymentRequestDTO)
        {
            try
            {
                
                var payment = await _paymentService.AddPayment(_mapper.Map<Payment>(paymentRequestDTO));

                if (payment == null) throw new Exception("Cannot retrive  add payment");


                //updating booking confirm status
                var booking = await _repository.Get(paymentRequestDTO.BookingId);
                booking.IsConfirmed = "Confirmed";

                var seats=(await _seatsBookedRepository.GetAll()).Where(s=>s.BookingId==payment.BookingId);

                //creating seatresponseDTO to response to api call
                List<SeatsResponseDTO> listOfSeatsDTO = new List<SeatsResponseDTO>();

                //updating seats status
                foreach( var seat_booked in seats)
                {

                    seat_booked.SeatStatus = Status.Confirmed;
                    var seat=await _seatService.SeatById(seat_booked.SeatId);
                    seat.IsBooked = true;
                    var seatResponseDTO = new SeatsResponseDTO()
                    {
                        Seat = seat.SeatNumber + seat.SeatType.ToString(),
                        Price= seat.Price,
                    };
                    listOfSeatsDTO.Add(seatResponseDTO);
                }
                var route =await _routingService.GetRoute(booking.RouteId);
                var response = new BookingResponseDTO()
                {
                    BookingId=booking.BookingId,
                    BookingDate=booking.BookingDate,
                    BusNumber=booking.BusNumber,
                    Source=route.Origin,
                    Destination=route.Destination,
                    CustomerId=booking.CustomerId,
                    BookedForDate=booking.BookedForDate,
                    BookedForDay=booking.BookedForDay,
                    SeatsBooked=listOfSeatsDTO,
                    TotalFare=booking.TotalFare,
                    IsConfirmed=booking.IsConfirmed
                };
                return response;   
               

                
            }
            catch
            {
                throw new Exception();
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

        public async Task<int> BookingConfirmation(SeatSelectionRequestDTO seatSelectionRequestDTO,DateTime date)
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
                   
                    seatsBookedForCustomer = seatsBookedForCustomer + id;
                   

            }
            var booking = new Booking()
            {
                BusId=bus.BusId,
                BookingDate=DateTime.Now,
                BusNumber = bus.BusNumber,
                RouteId = bus.RouteId,
                BookedForDate = date,
                BookedForDay = (DaysOfWeek)date.DayOfWeek,
                BookedSeats = seatsBookedForCustomer,
                TotalFare = calculateCost,
                IsConfirmed = "Pending",
                CustomerId = seatSelectionRequestDTO.CustomerId,
                


            };
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
             
             
                return booking.BookingId; 
            }
            catch
            {
                throw new Exception("Cannot add booking");
            }
        }

    

    }
}
