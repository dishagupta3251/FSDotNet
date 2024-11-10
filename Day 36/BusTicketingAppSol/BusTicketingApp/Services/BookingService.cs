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
        private readonly ILogger _logger;
        private readonly IRepository<Booking,int> _repository;

        public BookingService(ISeatService seatService, IBusService busService, IRoutingService routingService, IRepository<Booking,int> repository,IPaymentService paymentService,IMapper mapper,ILogger logger)
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
                        BusNumber =bus.BusNumber,
                        BusType = bus.BusType.ToString(),
                        SeatsLeft = seats.Count(),
                        Status = bus.Status,
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



        public async Task<IEnumerable<SeatsResponseDTO>> SelectSeats(SeatSelectionRequestDTO requestDTO)
        {
            try
            {
                var listOfSeats = requestDTO.SelectedSeatIds;
                if (listOfSeats.Count == 0)
                    throw new Exception("Seats not selected");

           
                var bookingResponse = await _seatService.UpdateSeatStatus(listOfSeats);

                return (IEnumerable<SeatsResponseDTO>) bookingResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during seat selection");
                throw new Exception("Cannot perform seat selection");
            }
        }

        public async Task<string> PaymentIntiation(PaymentRequestDTO paymentRequestDTO)
        {
            try
            {
                
                var status = await _paymentService.AddPayment(_mapper.Map<Payment>(paymentRequestDTO));
                if (status == null) throw new Exception("Cannot retrive status of payment");
                return status;
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

        public Task<BookingResponseDTO> BookingConfirmation()
        {
            
        }
    }
}
