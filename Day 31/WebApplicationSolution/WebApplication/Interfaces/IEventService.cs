using WebApplication1.Models.DTO;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IEventService
    {
        public Task<Event> Add(EventDTO eventDTO);
        public Task<IEnumerable<Event>> GetAll();
    }
}
