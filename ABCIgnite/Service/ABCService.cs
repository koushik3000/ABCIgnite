using ABCIgnite.DTO;
using ABCIgnite.Models;
using ABCIgnite.Repository.Interface;
using ABCIgnite.Service.Interface;
using AutoMapper;

namespace ABCIgnite.Service
{
    public class ABCService : IABCService

    {
        private readonly IABCRepository _abcRepository;
        private readonly IMapper _mapper;

        public ABCService(IABCRepository abcRepository, IMapper mapper)
        {
            _abcRepository = abcRepository;
            _mapper = mapper;
        }

        // Service method to get all gym classes
        public async Task<IEnumerable<ClassDTO>> GetAllGymClasses()
        {
            return await _abcRepository.GetAllGymClasses(); // Fetch gym classes from repository
        }


        public async Task<ClassDTO> CreateClassAsync(ClassDTO classDTO)
        {
            // Validate Capacity
            if (classDTO.Capacity < 1)
                throw new ArgumentException("Capacity must be at least 1.");

            // Validate EndDate
            if (classDTO.EndDate <= DateTime.Now)
                throw new ArgumentException("End Date must be in the future.");

            // Map DTO to Entity
            var classEntity = _mapper.Map<Class>(classDTO);

            // Add the class to the repository
            await _abcRepository.CreateClassAsync(classEntity);

            // Return the DTO
            return _mapper.Map<ClassDTO>(classEntity);
        }


        // Service method to get all Bookings
        public async Task<IEnumerable<BookingResultsDTO>> GetAllBookingsWithClassInfoAsync()
        {
            return await _abcRepository.GetAllBookingsWithClassInfoAsync(); // Fetch gym classes from repository
        }

        public async Task<IEnumerable<BookingResultsDTO>> GetBookingsByMemberName(string memberName)
        {
            return await _abcRepository.GetBookingsByMemberName(memberName);
        }

        public async Task<IEnumerable<BookingResultsDTO>> GetBookingsByDateRangeAsync(DateOnly startDate, DateOnly endDate)
        {
            try
            {
                // Fetch bookings from the repository (which already maps entities to DTO)
                var bookings = await _abcRepository.GetBookingsByDateRangeAsync(startDate, endDate);

                return bookings;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving bookings within the date range: " + ex.Message);
            }
        }
      

        public async Task<List<BookingResultsDTO>> GetBookingsWithClassInfoAsync(string memberName, DateOnly startDate, DateOnly endDate)
        {
            try
            {
                // Fetch bookings from the repository (with class information included)
                var bookings = await _abcRepository.GetBookingsWithClassInfoAsync(memberName, startDate, endDate);

                return bookings;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving bookings with class information: " + ex.Message);
            }
        }


        public async Task<CreateBookingDTO> CreateAsync(CreateBookingDTO bookingDto)
        {
            // Validate that the ParticipationDate is in the future (date only)
            var currentDate = DateOnly.FromDateTime(DateTime.UtcNow);
            if (bookingDto.ParticipationDate <= currentDate)
            {
                throw new ArgumentException("Participation date must be in the future.");
            }

            // Retrieve the class from the database to check capacity
            var classEntity = await _abcRepository.GetClassByIdAsync(bookingDto.ClassId);

            if (classEntity == null)
            {
                throw new ArgumentException("Class not found.");
            }

            // Check if there is available capacity
            if (classEntity.Capacity <= 0)
            {
                // Return a response indicating the class is full
                throw new InvalidOperationException("The class is full, booking cannot be made.");
            }

            // Create the booking if there is available capacity
            var booking = new Booking
            {
                MemberName = bookingDto.MemberName,
                ClassId = bookingDto.ClassId,
                ParticipationDate = bookingDto.ParticipationDate
            };

            // Add the booking to the Booking table
            var createdBooking = await _abcRepository.CreateAsync(booking);

            // Decrease the capacity of the class by 1
            classEntity.Capacity -= 1;
            await _abcRepository.UpdateClassAsync(classEntity);

            // Return the created booking as a DTO
            return new CreateBookingDTO
            {
                BookingId = createdBooking.BookingId,
                MemberName = createdBooking.MemberName,
                ClassId = createdBooking.ClassId,
                ParticipationDate = createdBooking.ParticipationDate
            };
        }

    }
}
