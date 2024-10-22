using AutoMapper;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Models.DTO;

namespace WebApplication1.Services
{
    public class BookingServices:IBookingService
    {
        private readonly IRepository<int, Booking> _repository;
        private readonly IMapper _mapper;
        public BookingServices(IRepository<int, Booking> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
       

        public async Task<Booking> Add(BookingDTO bookDTO)
        {
            try
            {
                var newBook = _mapper.Map<Booking>(bookDTO);
                await _repository.Add(newBook);
                return newBook;
            }
            catch (Exception ex) { throw new Exception("Cannot add booking"); }
        }

       

        public async Task<IEnumerable<Booking>> GetAll()
        {
            try
            {
                var bookings = await _repository.GetAll();
                return bookings;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
}
