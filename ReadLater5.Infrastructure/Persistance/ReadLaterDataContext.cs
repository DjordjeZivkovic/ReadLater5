using ReadLater5.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System;
using ReadLater5.Application.Interfaces;

namespace ReadLater5.Infrastructure.Persistance
{
    public class ReadLaterDataContext : IdentityDbContext<AppUser>, IDataContext

    {
        public ReadLaterDataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.Updated = DateTime.UtcNow;
                        break;
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.UtcNow;
                        entry.Entity.Updated = DateTime.UtcNow;
                        break;
                    default:
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReadLaterDataContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
