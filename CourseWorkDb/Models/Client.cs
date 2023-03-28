using System;
using System.Collections.Generic;

namespace CourseWorkDb.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public string? FirstName { get; set; }

    public string? Surname { get; set; }

    public string? SecondName { get; set; }

    public string? PassportNumber { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<CheckPayment> CheckPayments { get; } = new List<CheckPayment>();
}
