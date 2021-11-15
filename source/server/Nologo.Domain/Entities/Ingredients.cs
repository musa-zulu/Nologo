using Nologo.Domain.Common;
using System;

namespace Nologo.Domain.Entities
{
    public class Ingredients : BaseEntity
    {
        public Guid IngredientsId { get; set; }
        public string Description { get; set; }

        public Guid RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
