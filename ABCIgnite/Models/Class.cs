using System;
using System.Collections.Generic;

namespace ABCIgnite.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public TimeOnly StartTime { get; set; }

    public int Duration { get; set; }

    public int Capacity { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
