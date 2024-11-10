using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusTicketingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusController : ControllerBase
    {
        private readonly IBusService _busService;
        public BusController(IBusService busService)
        {
            _busService = busService;
        }
        
        [HttpPost]
      
        public async Task<ActionResult<int>> AddBus(BusCreateDTO busCreateDTO)
        {
            try
            {
                var bus = await _busService.BuildBus(busCreateDTO);
                return Ok(bus.BusId);
            }
            catch
            {
                throw new Exception("Cannot create bus entity");
            }
        }
        
    
        
    }
}
