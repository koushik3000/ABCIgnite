using ABCIgnite.DTO;
using ABCIgnite.Models;

namespace ABCIgnite.Repository.Interface
{
    public interface IABCRepository
    {
        Task<IEnumerable<ClassDTO>> GetAllGymClasses();

        Task CreateClassAsync(Class classEntity);


        Task<List<BookingResultsDTO>> GetAllBookingsWithClassInfoAsync();

        Task<IEnumerable<BookingResultsDTO>> GetBookingsByMemberName(string memberName);

        Task<IEnumerable<BookingResultsDTO>> GetBookingsByDateRangeAsync(DateOnly startDate, DateOnly endDate);


        Task<List<BookingResultsDTO>> GetBookingsWithClassInfoAsync(string memberName, DateOnly startDate, DateOnly endDate);

        Task<Booking> CreateAsync(Booking booking);

        Task<Class?> GetClassByIdAsync(int classId);

        Task UpdateClassAsync(Class classEntity);

    }
}
