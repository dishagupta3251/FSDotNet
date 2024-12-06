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

    public async Task<Seats> SeatById(int id)
    {
        try
        {
            var seat = await _seatRepository.Get(id);
            if (seat == null) throw new Exception("Cannot get seat");
            return seat;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting seat by id");
            throw new Exception();
        }
    }

    public async Task<SeatsResponseDTO> UpdateSeatStatus(int seatId)
    {
        try
        {

           
                var seat = await _seatRepository.Get(seatId);
                seat.IsBooked = true; 
                await _seatRepository.Update(seat,seat.SeatsId);
                SeatsResponseDTO dTO = new SeatsResponseDTO
                {
                    SeatId = seatId,
                    Seat = seat.SeatNumber + seat.SeatType.ToString(),
                    Price = seat.Price,
                    Status=seat.IsBooked,
                };

                
            

            return dTO; 
        }
        catch (Exception ex)
        {
            
            _logger.LogError(ex, "Error occurred while updating seats status");
            throw new Exception("Cannot update seat");
        }
    }
}
