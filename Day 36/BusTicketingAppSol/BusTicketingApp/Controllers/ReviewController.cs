using BusTicketingApp.Interfaces;
using BusTicketingApp.Models.DTO;
using BusTicketingApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusTicketingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {

        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(ReviewRequestDTO reviewRequestDTO)
        {
            try
            {
                var review =await _reviewService.Post(reviewRequestDTO);
                if (review != null) throw new Exception("Review not added");
                return Ok(review);

            }
            catch (Exception ex) {

                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpGet("{operatorid}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReview(int operatorid)
        {
            try
            {
                var reviews =await _reviewService.GetAllReviewsByOperatorId(operatorid);
                return Ok(reviews);
            }
            catch(Exception ex) {

                return NotFound(new { message = ex.Message });
            }
        }

        
    }
}
