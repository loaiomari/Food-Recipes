using System;
using System.Collections.Generic;

namespace First_Project.Models;

public partial class UserRecipe
{
    public decimal Id { get; set; }

    public decimal? Quantity { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public decimal? UserId { get; set; }

    public decimal? RecipeId { get; set; }

    public virtual Recipe? Recipe { get; set; }

    public virtual User? User { get; set; }
}
