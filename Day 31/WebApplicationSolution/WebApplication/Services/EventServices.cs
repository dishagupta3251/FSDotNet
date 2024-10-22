using AutoMapper;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Models.DTO;

namespace WebApplication1.Services
{
    public class EventServices : IEventService
    {
        private readonly IRepository<int, Event> _repository;
        private readonly IMapper _mapper;
        public EventServices(IRepository<int, Event> respository, IMapper mapper)
        {
            _repository = respository;
            _mapper = mapper;
        }

        public async Task<Event> Add(EventDTO eventDTO)
        {
            try
            {
                var newEvent = _mapper.Map<Event>(eventDTO);
                await _repository.Add(newEvent);
                return newEvent;
            }
            catch (Exception ex) { throw new Exception("Cannot add user"); }
        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            try
            {
                var events = await _repository.GetAll();
                return events;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
