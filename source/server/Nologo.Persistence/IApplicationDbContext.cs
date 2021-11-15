using System.Threading.Tasks;

namespace Nologo.Persistence
{
    public interface IApplicationDbContext
    {
        Task<int> SaveChangesAsync();
    }
}
