using ABCIgnite.datab;

namespace ABCIgnite.DTO
{
    public class BookingResultsDTO
    {
        public int BookingId { get; set; }

        public string MemberName { get; set; } = null!;

        public int ClassId { get; set; }

        public DateOnly ParticipationDate { get; set; }
                
        public string Name { get; set; } = null!;

        public TimeOnly StartTime { get; set; }
    }
}
