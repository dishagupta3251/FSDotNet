using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportClaim.Interfaces;
using ReportClaim.Models;
using ReportClaim.Models.DTO;

namespace ReportClaim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policyService;
        public PolicyController(IPolicyService policyService)
        {
            _policyService = policyService;
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> InputPolicy(PolicyDTO policyDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var policy = await _policyService.CreatePolicy(policyDTO);
                    return Ok(policy);
                }
                else
                {
                    return BadRequest(new ErrorReponseDTO
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


