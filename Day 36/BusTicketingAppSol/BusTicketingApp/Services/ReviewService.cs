using AutoMapper;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;

namespace BusTicketingApp.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IRepository<Review,int> _reviewsRepository;
        private readonly IMapper _mapper;
        public ReviewService(IRepository<Review, int> reviewsRepository, IMapper mapper)
        {
            _reviewsRepository = reviewsRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Review>> GetAllReviewsByOperatorId(int operatorId)
        {
            try
            {
                var reviews = (await _reviewsRepository.GetAll()).Where(r=>r.OperatorId==operatorId);
                if (reviews.Count() == 0) throw new Exception("No reviews for this operator");
                return reviews;
            }
            catch {
                throw new Exception();
            }
           
        }

        public async Task<Review> Post(ReviewRequestDTO requestDTO)
        {
            try
            {
                var review=_mapper.Map<Review>(requestDTO);
                var addedReview=await  _reviewsRepository.Add(review);
                if (addedReview == null) throw new Exception("Cannot post review");
                return review;
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
