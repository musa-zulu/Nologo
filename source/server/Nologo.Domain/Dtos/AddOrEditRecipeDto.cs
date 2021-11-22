using Nologo.Domain.Common;
using System;

namespace Nologo.Domain.Dtos
{
    public class AddOrEditRecipeDto : BaseEntity
    {
        public Guid RecipeId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string RecipeFileName { get; set; }
        public string Instructions { get; set; }
        public IngredientDto[] Ingredients { get; set; }
    }

    public class IngredientDto
    {
        public string Name { get; set; }
    }
}
