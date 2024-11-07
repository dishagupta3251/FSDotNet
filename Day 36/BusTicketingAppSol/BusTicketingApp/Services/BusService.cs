using System.Runtime.CompilerServices;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;

namespace BusTicketingApp.Services
{
    public class BusService:IBusService
    {
        private readonly IRepository<Bus, int> _busRepository;
        

        public Task<Bus> BuildBus(BusCreateDTO bus)
        {
            throw new NotImplementedException();
        }

        public Task<Bus> UpdateBus(BusUpdateDTO busDTO)
        {
            throw new NotImplementedException();
        }
    }
}
