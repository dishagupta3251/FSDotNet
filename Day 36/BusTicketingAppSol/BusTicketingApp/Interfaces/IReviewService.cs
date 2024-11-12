using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;

namespace BusTicketingApp.Interfaces
{
    public interface IReviewService
    {
        public Task<Review> Post(ReviewRequestDTO requestDTO);
        public Task<IEnumerable<Review>> GetAllReviewsByOperatorId(int operatorId);
    }
}
