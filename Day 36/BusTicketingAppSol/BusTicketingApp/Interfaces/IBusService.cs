using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;

namespace BusTicketingApp.Interfaces
{
    public interface IBusService
    {
        public Task<Bus> BuildBus(BusCreateDTO bus);
        public Task<Bus> UpdateBus(BusUpdateDTO busDTO, int id);
        public Task<BusWithSeatsResponseDTO> GetBusWithSeats(int id);
        public Task<Bus> GetBus(int id);
        public Task<IEnumerable<Bus>> GetBusesByRouteAndDay(int routeId, DateTime dateTime);
    }
}
