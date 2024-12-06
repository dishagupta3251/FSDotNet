using AutoMapper;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;
using MimeKit.Tnef;


namespace BusTicketingApp.Services
{
    public class BusOperatorService : IBusOperatorService
    {
        private readonly IRepository<BusOperator,int> _busOperatorRepository;
        private readonly IRepository<Booking,int> _bookingRepository;
        private readonly IBusService _busService;
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;
        private readonly ILogger<BusOperatorService> _logger;

        public BusOperatorService(IRepository<BusOperator, int> busOperatorRepository, IMapper mapper, ILogger<BusOperatorService> logger, IReviewService reviewService, IRepository<Booking, int> bookingRepository,IBusService busService)
        {
            _busOperatorRepository = busOperatorRepository;
            _reviewService = reviewService;
            _busService = busService;
            _mapper = mapper;
            _logger = logger;
            _bookingRepository = bookingRepository;
        }

        public async Task<BusOperator> AddBusOperator(BusOperatorCreateDTO busOperatorCreateDTO)
        {
            try
            {
                _logger.LogInformation("Starting to add a new bus operator.");
                var busOperator = _mapper.Map<BusOperator>(busOperatorCreateDTO); 
                var addedBusOperator = await _busOperatorRepository.Add(busOperator);
                _logger.LogInformation($"Successfully added new bus operator: {addedBusOperator.OperatorName}");
                return addedBusOperator;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while adding bus operator: {ex.Message}");
                throw new Exception("An error occurred while adding the bus operator.");
            }
        }

        public async Task<BusOperator> UpdateBusOperator(int id, BusOperatorCreateDTO busOperatorCreateDTO)
        {
            try
            {
                _logger.LogInformation($"Starting to update bus operator with ID: {id}");
              
                var updatedBusOperator = await _busOperatorRepository.Update(_mapper.Map<BusOperator>(busOperatorCreateDTO), id);
                _logger.LogInformation($"Successfully updated bus operator with ID: {id}");
                return updatedBusOperator;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while updating bus operator: {ex.Message}");
                throw new Exception("An error occurred while updating the bus operator.");
            }
        }

      
        public async Task<BusOperator> GetBusOperatorById(int id)
        {
            try
            {
                _logger.LogInformation($"Starting to fetch bus operator with ID: {id}");
                var busOperator = await _busOperatorRepository.Get(id);
                if (busOperator == null)
                {
                    _logger.LogWarning($"Bus operator with ID {id} not found.");
                    throw new Exception("Bus operator not found.");
                }
                _logger.LogInformation($"Successfully fetched bus operator with ID: {id}");
                return busOperator;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while fetching bus operator: {ex.Message}");
                throw new Exception("An error occurred while fetching the bus operator.");
            }
        }

       
        public async Task<IEnumerable<BusOperator>> GetAllBusOperators()
        {
            try
            {
                _logger.LogInformation("Starting to fetch all bus operators.");
                var busOperators = await _busOperatorRepository.GetAll();
                if (busOperators == null || !busOperators.Any())
                {
                    _logger.LogWarning("No bus operators found.");
                    throw new Exception("No bus operators found.");
                }
                _logger.LogInformation($"Successfully fetched {busOperators.Count()} bus operators.");
                return busOperators;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while fetching bus operators: {ex.Message}");
                throw new Exception("An error occurred while fetching bus operators.");
            }
        }

        public async Task<IEnumerable<Booking>>  GetBookingsByBus(int busId)
        {
            try
            {
                var bookings = (await _bookingRepository.GetAll()).Where(b => b.BusId == busId);
                if (bookings == null) throw new Exception("No bookings available with this bus");
                return bookings;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<IEnumerable<Bus>> GetBusesByOperator(int userId)
        {
            try
            {
                var id = (await _busOperatorRepository.GetAll()).FirstOrDefault(o => o.UserId == userId).OperatorId;
                var buses = (await _busService.GetAllBuses()).Where(b => b.OperatorID == id);
                if (buses == null) throw new Exception("No buses found with operator id");
                return buses;
            }
            catch
            {
                throw new Exception();
            }
        }
        public async Task<ReviewResponseDTO> GetOperatorReview(int id)
        {
            try
            {
                var reviews=await _reviewService.GetAllReviewsByOperatorId(id);
                if (reviews == null) throw new Exception("No reviews can be found");
                List<string> reviewsList = new List<string>();
                foreach (var review in reviews) {
                    reviewsList.Add(review.Reviews);
                }
                var busOperator= await _busOperatorRepository.Get(id);
                return new ReviewResponseDTO
                {
                    OperatorName = busOperator.OperatorName,
                    Reviews = reviewsList,
                    LicenseNumber = busOperator.LicenseNumber,
                    CompanyName = busOperator.CompanyName,
                    OperatorContact= busOperator.OperatorContact
                   
                };
            }
            catch  { 
                throw new Exception();
            }
        }

        public async Task<BusOperator> GetBusOperatorByUsername(string username)
        {
            try
            {
                var busOperator = (await _busOperatorRepository.GetAll()).FirstOrDefault(o => o.Username == username);
                if (busOperator == null) throw new Exception("Cannot find user");
                return busOperator;

            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
