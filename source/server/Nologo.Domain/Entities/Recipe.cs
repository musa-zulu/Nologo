using Nologo.Domain.Common;
using System;
using System.Collections.Generic;

namespace Nologo.Domain.Entities
{
    public class Recipe : BaseEntity
    {
        public Guid RecipeId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string RecipeFileName { get; set; }
        public string Instructions { get; set; }

        public virtual List<Ingredients> Ingredients { get; set; }
    }
}
