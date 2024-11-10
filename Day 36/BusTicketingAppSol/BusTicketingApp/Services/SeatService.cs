using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;

public class SeatService : ISeatService
{
    private readonly IRepository<Seats, int> _seatRepository;
    private readonly ILogger<SeatService> _logger;

    public SeatService(IRepository<Seats, int> repository, ILogger<SeatService> logger)
    {
        _seatRepository = repository;
        _logger = logger;
    }

   
    public async Task<IEnumerable<Seats>> GetAllSeats()
    {
        try
        {
            var seats = await _seatRepository.GetAll();
            return seats;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving seats");
            throw new Exception("Cannot view Seats");
        }
    }

  


    public async Task<IEnumerable<SeatsResponseDTO>> UpdateSeatStatus(List<int> seatIds)
    {
        try
        {
            List<SeatsResponseDTO> selectedSeats = new List<SeatsResponseDTO>();

            foreach (var id in seatIds)
            {
                var seat = await _seatRepository.Get(id);
                seat.IsBooked = true; 
                SeatsResponseDTO dTO = new SeatsResponseDTO
                {
                    Seat = seat.SeatNumber + seat.SeatType.ToString(),
                    IsBooked = seat.IsBooked,
                    Price = seat.Price
                };

                selectedSeats.Add(dTO);
            }

            return selectedSeats; 
        }
        catch (Exception ex)
        {
            
            _logger.LogError(ex, "Error occurred while updating seats status");
            throw new Exception("Cannot update seat");
        }
    }
}
