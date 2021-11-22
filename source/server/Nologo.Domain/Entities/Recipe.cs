using Nologo.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nologo.Domain.Entities
{
    public class Recipe : BaseEntity
    {
        public Guid RecipeId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string RecipeFileName { get; set; }
        [Column(TypeName="varchar(MAX)")]
        [MaxLength]
        public string Instructions { get; set; }
        [Column(TypeName = "varchar(MAX)")]
        [MaxLength]
        public string Ingredients { get; set; }
    }
}
