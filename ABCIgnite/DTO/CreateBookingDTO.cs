namespace ABCIgnite.DTO
{
    public class CreateBookingDTO
    {
        public int BookingId { get; set; }
        public string MemberName { get; set; } = null!;
        public int ClassId { get; set; }
        public DateOnly ParticipationDate { get; set; }
    }
}
