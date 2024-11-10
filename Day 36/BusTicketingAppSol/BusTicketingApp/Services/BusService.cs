using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BusTicketingApp.Contexts;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;
using DayOfWeek = BusTicketingApp.Models.DaysOfWeek;
using System;

namespace BusTicketingApp.Services
{
    public class BusService:IBusService
    {
        private readonly IRepository<Bus, int> _busRepository;
        private readonly IRepository<BusSchedule, int> _busScheduleRepository;
        private readonly IRepository<Seats, int> _seatRepository;
        private readonly IRepository<AvailableRoute, int> _availableRouteRepository;
        private readonly IRepository<BusOperator, int>
            _busOperatorRepository;
        private readonly IMapper _mapper;
        public BusService(IRepository<Bus,int> repository,IRepository<BusSchedule,int> repository1,IMapper mapper, IRepository<Seats,int> repository2,IRepository<AvailableRoute,int> repository3)
        {
            _busRepository = repository;
            _busScheduleRepository = repository1;
            _seatRepository= repository2;
            _availableRouteRepository= repository3;
            _mapper = mapper;
        }
        public async Task<Bus> BuildBus(BusCreateDTO bus)
        {
            try
            {
                // mapping explicitly

                var newBus = new Bus()
                {
                    BusNumber=bus.BusNumber,
                    BusType=bus.BusType,
                    NumberOfSeats=bus.NumberOfSeats,
                    Status=bus.Status,
                    StandardFare=bus.StandardFare,
                    PremiumFare=bus.PremiumFare,
                    OperatorID=bus.OperatorId,
                    RouteId=bus.RouteId,
                };
                var addedBus=await _busRepository.Add(newBus);

                var busSchedule = _mapper.Map<BusSchedule>(newBus);

               await _busScheduleRepository.Add(busSchedule);

                // dividing total seats in two halves

                int halfSeats = addedBus.NumberOfSeats / 2;
                for (int i = 1; i <= addedBus.NumberOfSeats; i++)
                {
                    var seat = new Seats()
                    {
                        SeatNumber = i,
                        Side = i <= halfSeats ? "L" : "R",
                        SeatType = i % 2 == 0 ? "A" : "W",
                        IsBooked = false,
                        Price = i % 2 == 0 ? addedBus.StandardFare : addedBus.PremiumFare,
                        BusId = addedBus.BusId,


                    };

                    //adding data to seat table

                   await _seatRepository.Add(seat);
                }
             


                return addedBus;

            }
            catch {
                throw new Exception("Could not add bus");
            }
        }

        public async Task<BusWithSeatsResponseDTO> GetBusWithSeats(int id)
        {
            try
            {
                var bus =await _busRepository.Get(id);
                var seats = (await _seatRepository.GetAll()).Where(s => s.BusId == id).ToList();
                if (bus == null || seats == null) throw new Exception();

                List<SeatsResponseDTO> seatsResponseDTOs = new List<SeatsResponseDTO>();
                foreach (var seat in seats) 
                {
                    var response = new SeatsResponseDTO()
                    {
                        Seat = seat.SeatNumber + seat.SeatType,
                        IsBooked=seat.IsBooked,
                        Price=seat.Price,
                    };
                    seatsResponseDTOs.Add(response);

                }
                
                BusWithSeatsResponseDTO busWithSeatsResponseDTO = new BusWithSeatsResponseDTO()
                {
                    BusNumber=bus.BusNumber,
                    BusType=bus.BusType.ToString(),
                    StandardFare=bus.StandardFare,
                    PremiumFare=bus.PremiumFare,
                    Seats= seatsResponseDTOs
                };
                return busWithSeatsResponseDTO;
            }
            catch
            {
                throw new Exception("Cannot find the bus");
            }
            
        }

        public async Task<IEnumerable<Bus>> GetBusesByRouteAndDay(int routeId,DateTime dateTime)
        {
            try
            {
                DaysOfWeek day =(DaysOfWeek) dateTime.DayOfWeek;

              
                var schedules = (await _busScheduleRepository.GetAll())
                    .Where(s => s.RouteId == routeId && s.Day == day)
                    .ToList();

               
                var busIds = schedules.Select(s => s.BusId).Distinct();


                List<Bus> buses = new List<Bus>();
                foreach(var id in busIds)
                {
                    var bus = await _busRepository.Get(id);
                    buses.Add(bus);
                }

                return buses;
            }
            catch {
                throw new Exception("Not Available");
            }
        }

        public async Task<Bus> UpdateBus(BusUpdateDTO busDTO,int id)
        {
            try
            {
                var bus=_mapper.Map<Bus>(busDTO);
                var updatedBus = await _busRepository.Update(bus, id);
                return updatedBus;
            }
            catch {
                throw new Exception("Could not update infomation");
            }

        }

       
    }
}
