using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportClaim.Interfaces;
using ReportClaim.Models.DTO;
using ReportClaim.Services;

namespace ReportClaim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private readonly IClaimService _claimService;
        public ClaimController(IClaimService claimService)
        {
            _claimService = claimService;
        }
        [HttpPost]
        public async Task<ActionResult> CreateClaim(ClaimDTO claimDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var claim = await _claimService.CreateClaim(claimDTO);
                    return Ok(claim);
                }
                else
                {
                    return BadRequest(new ErrorResponseDTO
                    {
                        ErrorCode = 400,
                        ErrorMessage = "one or more fields validate error"
                    });
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
       
    }
}
