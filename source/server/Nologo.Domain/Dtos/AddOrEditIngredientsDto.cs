using Nologo.Domain.Common;
using System;

namespace Nologo.Domain.Dtos
{
    public class AddOrEditIngredientsDto : BaseEntity
    {
        public Guid IngredientsId { get; set; }
        public string Description { get; set; }
        public Guid RecipeId { get; set; }
    }
}
