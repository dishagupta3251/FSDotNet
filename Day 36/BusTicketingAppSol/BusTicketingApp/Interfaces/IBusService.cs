using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;

namespace BusTicketingApp.Interfaces
{
    public interface IBusService
    {
        public Task<Bus> BuildBus(BusCreateDTO bus);
        public Task<Bus> UpdateBus(BusUpdateDTO busDTO);
    }
}
