using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Models.DTO;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
        public class EventController : ControllerBase
        {
            private readonly IEventService _eventService;
            private readonly IUserService _userService;

            public EventController(IEventService eventService, IUserService userService)
            {
                _eventService = eventService;
                _userService = userService;
            }

            [HttpGet]
            public async Task<IActionResult> GetAllEvents()
            {
                try
                {
                    var events = await _eventService.GetAll();
                    return Ok(events);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
                }
            }
            [HttpGet]
            [Route("/eventName")]
            public async Task<IActionResult> getEventNames()
            {
                try
                {
                    var events = await _eventService.GetAll();
                    //linq query to fetch to only event names
                    var eventsnames = (from enames in events
                                       select enames.Name).ToList();
                    return Ok(eventsnames);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
                }
            }
            [HttpGet]
            [Route("/getEventRegisteredByUser")]
            public async Task<IActionResult> getEvenRegisteredByUser()
            {
                try
                {
                    var events = await _eventService.GetAll();
                    var employees = await _userService.GetAll();
                    //select UserName, EventName, Email from Users cross join Events

                    //linq query to fetch to cross join 
                    var details = from u in employees
                                  from e in events
                                  select new
                                  {
                                      UserName = u.Name,
                                      EventName = e.Name,
                                      Email = u.Email
                                  };

                    return Ok(details);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
                }
            }


            [HttpPost]
            public async Task<IActionResult> CreateEvent(EventDTO eventDTO)
            {
                try
                {

                    var user = await _eventService.Add(eventDTO);
                    return Ok(user);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
        }
}
