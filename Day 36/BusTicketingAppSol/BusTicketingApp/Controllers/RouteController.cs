﻿using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusTicketingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRoutingService _routingService;
        public RouteController(IRoutingService routingService)
        {
            _routingService=routingService;
        }
        [HttpPost("CreateRoute")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AvailableRoute>> CreateRoute(AvailableRouteDTO availableRouteDTO)
        {
            try
            {
                var route = await _routingService.AddNewRoutes(availableRouteDTO);
                return Ok(route);
            }
            catch
            {
                throw new Exception("Creation failed");
            }
        }
        [HttpGet("GetAllRoutes")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<ActionResult<IEnumerable<AvailableRoute>>> AvailableRoutes()
        {
            try
            {
                var routes = await _routingService.GetAllRoutes();
                return Ok(routes);
            }
            catch 
            {
               
                return StatusCode(500, "An error occurred while retrieving routes.");
            }
        }
    }
}
