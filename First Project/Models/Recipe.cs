using System;
using System.Collections.Generic;

namespace First_Project.Models;

public partial class Recipe
{
    public decimal RecipeId { get; set; }

    public string? RecipeName { get; set; }

    public decimal? Sale { get; set; }

    public decimal? Price { get; set; }

    public decimal? CatId { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? Status { get; set; }

    public string? Ingredients { get; set; }

    public virtual Category? Cat { get; set; }

    public virtual ICollection<UserRecipe> UserRecipes { get; set; } = new List<UserRecipe>();
}
