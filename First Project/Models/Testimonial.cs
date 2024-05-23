using System;
using System.Collections.Generic;

namespace First_Project.Models;

public partial class Testimonial
{
    public decimal TestimonialId { get; set; }

    public string? Status { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? Message { get; set; }

    public decimal? Rating { get; set; }

    public decimal? UserId { get; set; }

    public virtual User? User { get; set; }
}
