using System;
using System.Collections.Generic;

namespace First_Project.Models;

public partial class Visa
{
    public decimal VisaId { get; set; }

    public decimal? VisaNumber { get; set; }

    public decimal? Cvc { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public decimal? Balance { get; set; }
}
