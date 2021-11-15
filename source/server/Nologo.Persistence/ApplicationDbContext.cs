using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Nologo.Domain.Common;

namespace Nologo.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        // This constructor is used of runit testing
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(150)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                .UseSqlServer("DataSource=app.db");
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            //var userId = Microsoft.AspNetCore.Mvc.HttpContext.User?.Identity?.Name ?? "SYS";
            var userId = "SYS";
            var addedAuditedEntities = ChangeTracker.Entries<BaseEntity>().Where(p => p.State == EntityState.Added)
                .Select(p => p.Entity);
            var modifiedAuditedEntities = ChangeTracker.Entries<BaseEntity>().Where(p => p.State == EntityState.Modified)
                .Select(p => p.Entity);
            var now = DateTime.UtcNow;
            foreach (var added in addedAuditedEntities)
            {
                added.DateCreated = now;
                added.AddedBy = userId;
                added.DateUpdated = now;
                added.UpdatedBy = userId;
            }

            foreach (var modified in modifiedAuditedEntities)
            {
                if (modified.DateCreated == DateTime.MinValue)
                    modified.DateCreated = now;
                if (string.IsNullOrEmpty(modified.AddedBy))
                    modified.AddedBy = userId;
                modified.DateUpdated = now;
                modified.UpdatedBy = userId;
            }
            return await base.SaveChangesAsync();
        }

    }
}
