
using AutoMapper;
using BusTicketingApp.Exceptions;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;


namespace BusTicketingApp.Services
{
    public class BusService:IBusService
    {
        private readonly IRepository<Bus, int> _busRepository;
        private readonly IRepository<BusSchedule, int> _busScheduleRepository;
        private readonly IRepository<Seats, int> _seatRepository;
        private readonly IRepository<AvailableRoute, int> _availableRouteRepository;
        private readonly IRepository<SeatsBooked, int> _seatsBookedRespository;
        private readonly IMapper _mapper;

        public BusService(IRepository<Bus,int> repository,IRepository<BusSchedule,int> repository1,IMapper mapper, IRepository<Seats,int> repository2,IRepository<AvailableRoute,int> repository3, IRepository<SeatsBooked,int> repository4)
        {
            _busRepository = repository;
            _busScheduleRepository = repository1;
            _seatRepository= repository2;
            _seatsBookedRespository = repository4;
            _availableRouteRepository= repository3;
            _mapper = mapper;
        }
        public async Task<Bus> BuildBus(BusCreateDTO bus)
        {
            try
            {
                

                var newBus = _mapper.Map<Bus>(bus);
                var addedBus=await _busRepository.Add(newBus);

                var busSchedule = new BusSchedule()
                {
                    BusId=addedBus.BusId,
                    Day=bus.Day,
                    RouteId=bus.RouteId,
                    Arrival=bus.Arrival,
                    Departure=bus.Departure,


                };

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
                var seats = (await _seatRepository.GetAll()).Where(s => s.BusId == id && s.IsBooked==false).ToList();
                if (bus == null || seats == null) throw new Exception();

                List<SeatsResponseDTO> seatsResponseDTOs = new List<SeatsResponseDTO>();

                
                var seatsBooked = (await _seatsBookedRespository.GetAll()).Where(s => s.SeatStatus.ToString()=="Pending"&& s.BusId==id).ToList();

                if(seatsBooked.Count==0)
                {
                    foreach(var seat in seats)
                    {
                        var response = new SeatsResponseDTO()
                        {
                            SeatId = seat.SeatsId,
                            Seat = seat.SeatNumber + seat.SeatType,
                            Price = seat.Price,
                        };
                        seatsResponseDTOs.Add(response);
                    }
                }
                else
                {
                    foreach (var seat in seats)
                    {
                        var seatBook = seatsBooked.FirstOrDefault(s => s.SeatId == seat.SeatsId);
                        if (seatBook == null)
                        {
                            var response = new SeatsResponseDTO()
                            {
                                SeatId = seat.SeatsId,
                                Seat = seat.SeatNumber + seat.SeatType,
                                Price = seat.Price,
                            };
                            seatsResponseDTOs.Add(response);
                        }


                    }

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

                if (schedules == null) throw new Exception("No buses can be found on the given date");

               
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

        public async Task<Bus> GetBus(int id)
        {
            try
            {
                var bus=await _busRepository.Get(id);
                return bus;
            }
            catch { 
                throw new Exception();
            }
        }

        public async Task<IEnumerable<Bus>> GetAllBuses()
        {
            try
            {
                var buses = await _busRepository.GetAll();
                if (buses.Count() == 0) throw new CollectionEmptyException("Bus");
                return buses;
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
