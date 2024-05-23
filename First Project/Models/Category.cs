using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace First_Project.Models;

public partial class Category
{
    public decimal CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public string? ImageName { get; set; }
    [NotMapped]
    public IFormFile ImageFile {  get; set; }

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
