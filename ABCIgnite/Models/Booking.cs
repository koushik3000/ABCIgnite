using System;
using System.Collections.Generic;

namespace ABCIgnite.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public string MemberName { get; set; } = null!;

    public int ClassId { get; set; }

    public DateOnly ParticipationDate { get; set; }

    public virtual Class Class { get; set; } = null!;


}
