﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportClaim.Interfaces;
using ReportClaim.Models;
using ReportClaim.Models.DTO;
using System.Net;
using System.Security.Claims;

namespace ReportClaim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IPolicyService _policyService;
        private readonly IClaimService _claimService;
        private readonly IReportService _reportService;

        public ReportController(IPolicyService policyService, IClaimService claimService, IReportService reportService)
        {
            _policyService = policyService;
            _claimService = claimService;
            _reportService = reportService;
        }

        [HttpGet]
        [Route("getPolicy")]
        public async Task<ActionResult<IEnumerable<PolicyDTO>>> GetPolicies()
        {
            List<string> policyNumbers = new List<string>();
            try
            {
                var policies = await _policyService.GetAllPolicies();
                foreach (var policy in policies) { policyNumbers.Add(policy.PolicyNumber); }
                if (policies == null || !policies.Any())
                {
                    return NotFound("No policies found.");
                }
                return Ok(policyNumbers);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error retrieving policies: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getClaim")]
        public async Task<ActionResult<IEnumerable<ClaimDTO>>> GetClaims()
        {
            List<string> claimTypes = new List<string>();
            try
            {
                var claims= await _claimService.GetAllClaims();
                foreach (var claimType in claims) { claimTypes.Add(claimType.ClaimType); }
                if (claimTypes == null || !claimTypes.Any())
                {
                    return NotFound("No claims found.");
                }
                return Ok(claimTypes);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error retrieving claims: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateReport([FromForm] ReportDTO reportDTO)
        {
            try
            {
                var report = await _reportService.CreateReport(reportDTO);
                return Ok(report.Id);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
    }
}
