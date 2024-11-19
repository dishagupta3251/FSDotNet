﻿using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusTicketingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusOperatorsController : ControllerBase
    {
        private readonly IBusOperatorService _busOperatorService;

        public BusOperatorsController(IBusOperatorService busOperatorService)
        {
            _busOperatorService = busOperatorService;
        }
       

        [HttpPatch("BusOperatorProfileUpdate")]
        [Authorize(Roles = "BusOperator")]

        public async Task<IActionResult> UpdateBusOperator(int id, BusOperatorCreateDTO busOperatorCreateDTO)
        {
            try
            {
                var updatedBusOperator = await _busOperatorService.UpdateBusOperator(id, busOperatorCreateDTO);
                return Ok(updatedBusOperator);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "BusOperator")]

        public async Task<ActionResult<BusOperator>> GetBusOperatorById(int id)
        {
            try
            {
                var busOperator = await _busOperatorService.GetBusOperatorById(id);
                return Ok(busOperator);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<IEnumerable<BusOperator>>> GetAllBusOperators()
        {
            try
            {
                var busOperators = await _busOperatorService.GetAllBusOperators();
                return Ok(busOperators);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        

    


    }
}
