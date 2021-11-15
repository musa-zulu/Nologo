using Nologo.Domain.Entities;
using Nologo.Persistence;
using Nologo.Service.Contracts;

namespace Nologo.Service.Implementation
{
    public class IngredientService : GenericService<Ingredients>, IIngredientService
    {
        public IngredientService(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
