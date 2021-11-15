using Nologo.Domain.Entities;
using Nologo.Persistence;
using Nologo.Service.Contracts;

namespace Nologo.Service.Implementation
{
    public class RecipeService : GenericService<Recipe>, IRecipeService
    {
        public RecipeService(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
