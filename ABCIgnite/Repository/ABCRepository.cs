using ABCIgnite.datab;
using ABCIgnite.DTO;
using ABCIgnite.Models;
using ABCIgnite.Repository.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ABCIgnite.Repository
{
    public class ABCRepository : IABCRepository
    {
        private readonly AbcclientDatabaseContext _dbcontext;
        private readonly IMapper _mapper;

        public ABCRepository(AbcclientDatabaseContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        // Method to retrieve all gym classes and map them to DTO
        public async Task<IEnumerable<ClassDTO>> GetAllGymClasses()
        {
            try
            {
                var gymClasses = await _dbcontext.Classes.ToListAsync(); // Fetch all GymClasses from database

                // Map GymClass entities to GymCLassDTO using AutoMapper
                var gymClassDtos = _mapper.Map<List<ClassDTO>>(gymClasses);

                return gymClassDtos; // Return mapped DTOs
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving gym classes: " + ex.Message);
            }
        }


        public async Task CreateClassAsync(Class classEntity)
        {
            _dbcontext.Classes.Add(classEntity);
            await _dbcontext.SaveChangesAsync();
            
        }

        // Method to retrieve all gym classes and map them to DTO
        public async Task<List<BookingResultsDTO>> GetAllBookingsWithClassInfoAsync()
        {
            try
            {
                // Fetch all bookings and their related class details by joining with the Class table
                var bookings = await _dbcontext.Bookings
                    .Join(_dbcontext.Classes,
                          booking => booking.ClassId,
                          classEntity => classEntity.ClassId,
                          (booking, classEntity) => new
                          {
                              booking.BookingId,
                              booking.MemberName,
                              booking.ClassId,
                              booking.ParticipationDate,
                              ClassName = classEntity.Name,  // Fetch Class Name
                              ClassStartTime = classEntity.StartTime // Fetch Class Start Time
                          })
                    .ToListAsync();

                // Map the result to BookingResultsDTO
                var bookingResultsDTOs = bookings.Select(b => new BookingResultsDTO
                {
                    BookingId = b.BookingId,
                    MemberName = b.MemberName,
                    ClassId = b.ClassId,
                    ParticipationDate = b.ParticipationDate,
                    Name = b.ClassName,  // Map ClassName to Name
                    StartTime = b.ClassStartTime // Map ClassStartTime to StartTime
                }).ToList();

                return bookingResultsDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving bookings with class information: " + ex.Message);
            }
        }

        public async Task<IEnumerable<BookingResultsDTO>> GetBookingsByMemberName(string memberName)
        {
            try
            {
                // Fetch bookings and their related class details by joining with the Class table
                var bookings = await _dbcontext.Bookings
                    .Where(b => b.MemberName == memberName)
                    .Join(_dbcontext.Classes,
                          booking => booking.ClassId,
                          classEntity => classEntity.ClassId,
                          (booking, classEntity) => new
                          {
                              booking.BookingId,
                              booking.MemberName,
                              booking.ClassId,
                              booking.ParticipationDate,
                              ClassName = classEntity.Name,  // Fetch Class Name
                              ClassStartTime = classEntity.StartTime // Fetch Class Start Time
                          })
                    .ToListAsync();

              
                var bookingDTOs = bookings.Select(b => new BookingResultsDTO
                {
                    BookingId = b.BookingId,
                    MemberName = b.MemberName,
                    ClassId = b.ClassId,
                    ParticipationDate = b.ParticipationDate,
                    Name = b.ClassName,  // Map ClassName to Name
                    StartTime = b.ClassStartTime // Map ClassStartTime to StartTime
                }).ToList();

                return bookingDTOs; // Return mapped DTOs
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving bookings for the member: " + ex.Message);
            }
        }

        public async Task<IEnumerable<BookingResultsDTO>> GetBookingsByDateRangeAsync(DateOnly startDate, DateOnly endDate)
        {
            try
            {
                // Fetch bookings and their related class details by joining with the Class table
                var bookings = await _dbcontext.Bookings
                    .Where(b => b.ParticipationDate >= startDate && b.ParticipationDate <= endDate)
                    .Join(_dbcontext.Classes,
                          booking => booking.ClassId,
                          classEntity => classEntity.ClassId,
                          (booking, classEntity) => new
                          {
                              booking.BookingId,
                              booking.MemberName,
                              booking.ClassId,
                              booking.ParticipationDate,
                              ClassName = classEntity.Name,  // Fetch Class Name
                              ClassStartTime = classEntity.StartTime // Fetch Class Start Time
                          })
                    .ToListAsync();

                
                var bookingDTOs = bookings.Select(b => new BookingResultsDTO
                {
                    BookingId = b.BookingId,
                    MemberName = b.MemberName,
                    ClassId = b.ClassId,
                    ParticipationDate = b.ParticipationDate,
                    Name = b.ClassName,  // Map ClassName to Name
                    StartTime = b.ClassStartTime // Map ClassStartTime to StartTime
                }).ToList();

                return bookingDTOs; // Return mapped DTOs
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
                // Fetch bookings and their related class details by joining with the Class table
                var bookings = await _dbcontext.Bookings
                    .Where(b => b.MemberName == memberName &&
                                b.ParticipationDate >= startDate &&
                                b.ParticipationDate <= endDate)
                    .Join(_dbcontext.Classes,
                          booking => booking.ClassId,
                          classEntity => classEntity.ClassId,
                          (booking, classEntity) => new
                          {
                              booking.BookingId,
                              booking.MemberName,
                              booking.ClassId,
                              booking.ParticipationDate,
                              ClassName = classEntity.Name,  // Fetch Class Name
                              ClassStartTime = classEntity.StartTime // Fetch Class Start Time
                          })
                    .ToListAsync();

                // Map the result to BookingResultsDTO
                var bookingResultsDTOs = bookings.Select(b => new BookingResultsDTO
                {
                    BookingId = b.BookingId,
                    MemberName = b.MemberName,
                    ClassId = b.ClassId,
                    ParticipationDate = b.ParticipationDate,
                    Name = b.ClassName,  // Map ClassName to Name
                    StartTime = b.ClassStartTime // Map ClassStartTime to StartTime
                }).ToList();

                return bookingResultsDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving bookings with class information: " + ex.Message);
            }
        }


        public async Task<Booking> CreateAsync(Booking booking)
        {
            _dbcontext.Bookings.Add(booking);
            await _dbcontext.SaveChangesAsync();
            return booking;
        }


        public async Task<Class?> GetClassByIdAsync(int classId)
        {
            return await _dbcontext.Classes
                .Where(c => c.ClassId == classId)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateClassAsync(Class classEntity)
        {
            _dbcontext.Classes.Update(classEntity);
            await _dbcontext.SaveChangesAsync();
        }

    }
}




