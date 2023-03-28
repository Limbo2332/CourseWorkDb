using System;
using System.Collections.Generic;

namespace CourseWorkDb.Models;

public partial class CheckPayment
{
    public int CheckPaymentId { get; set; }

    public DateTime? DateOfCreation { get; set; }

    public DateTime? DateOfSettlement { get; set; }

    public DateTime? DateOfEviction { get; set; }

    public bool? CheckState { get; set; }

    public bool? TypeOfPladge { get; set; }

    public int ClientId { get; set; }

    public int ApartmentId { get; set; }

    public virtual Apartment Apartment { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;
}
