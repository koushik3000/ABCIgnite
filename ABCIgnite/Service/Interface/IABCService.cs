using ABCIgnite.datab;
using ABCIgnite.DTO;

namespace ABCIgnite.Service.Interface
{
    public interface IABCService
    {
        Task<IEnumerable<ClassDTO>> GetAllGymClasses();
        Task<ClassDTO> CreateClassAsync(ClassDTO classDTO);

        Task<IEnumerable<BookingResultsDTO>> GetAllBookingsWithClassInfoAsync();

        Task<IEnumerable<BookingResultsDTO>> GetBookingsByMemberName(string memberName);

        Task<IEnumerable<BookingResultsDTO>> GetBookingsByDateRangeAsync(DateOnly startDate, DateOnly endDate);


        Task<List<BookingResultsDTO>> GetBookingsWithClassInfoAsync(string memberName, DateOnly startDate, DateOnly endDate);


        Task<CreateBookingDTO> CreateAsync(CreateBookingDTO bookingDto);


    }

}
