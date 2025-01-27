using ABCIgnite.Models;

namespace ABCIgnite.DTO
{
    public class BookingDTO
    {
        
            public int BookingId { get; set; }

            public string MemberName { get; set; } = null!;

            public int ClassId { get; set; }

            public DateOnly ParticipationDate { get; set; }

            public virtual Class Class { get; set; } = null!;

    }
}
