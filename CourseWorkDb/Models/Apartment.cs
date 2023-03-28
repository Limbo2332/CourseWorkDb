using System;
using System.Collections.Generic;

namespace CourseWorkDb.Models;

public partial class Apartment
{
    public int ApartmentId { get; set; }

    public int? Number { get; set; }

    public int? Capacity { get; set; }

    public string? Comfort { get; set; }

    public decimal? Price { get; set; }

    public string? AppartmentType { get; set; }

    public virtual ICollection<CheckPayment> CheckPayments { get; } = new List<CheckPayment>();
}
