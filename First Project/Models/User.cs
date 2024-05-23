using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace First_Project.Models;

public partial class User
{
    public decimal UserId { get; set; }

    public string? Fname { get; set; }

    public string? Lname { get; set; }

    public string? Email { get; set; }

    public string? ImageName { get; set; }
    [NotMapped]
    public IFormFile ImageFile { get; set; }

    public virtual ICollection<Login> Logins { get; set; } = new List<Login>();

    public virtual ICollection<Testimonial> Testimonials { get; set; } = new List<Testimonial>();

    public virtual ICollection<UserRecipe> UserRecipes { get; set; } = new List<UserRecipe>();
}
