using AutoMapper;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;


namespace BusTicketingApp.Services
{
    public class BusOperatorService : IBusOperatorService
    {
        private readonly IRepository<BusOperator,int> _busOperatorRepository;
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;
        private readonly ILogger<BusOperatorService> _logger;

        public BusOperatorService(IRepository<BusOperator, int> busOperatorRepository, IMapper mapper, ILogger<BusOperatorService> logger, IReviewService reviewService)
        {
            _busOperatorRepository = busOperatorRepository;
            _reviewService = reviewService;
            _mapper = mapper;
            _logger = logger;
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
                //var existingBusOperator = await _busOperatorRepository.Get(id);
                //if (existingBusOperator == null)
                //{
                //    _logger.LogWarning($"Bus operator with ID {id} not found for update.");
                //    throw new Exception("Bus operator not found.");
                //}

                var updateBusOperator = new BusOperator
                {
                    CompanyName = busOperatorCreateDTO.CompanyName,
                    Email = busOperatorCreateDTO.Email,
                    LicenseNumber = busOperatorCreateDTO.LicenseNumber,
                    OperatorContact = busOperatorCreateDTO.OperatorContact,
                    OperatorName = busOperatorCreateDTO.OperatorName,
                };
              
                //_mapper.Map(busOperatorCreateDTO, existingBusOperator);
                var updatedBusOperator = await _busOperatorRepository.Update(updateBusOperator, id);
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
    }
}
