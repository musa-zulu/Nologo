using Microsoft.EntityFrameworkCore;
using Nologo.Domain.Entities;
using System.Threading.Tasks;

namespace Nologo.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Recipe> Recipes { get; set; }
        Task<int> SaveChangesAsync();
    }
}
