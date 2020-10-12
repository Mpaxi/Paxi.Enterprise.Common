using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Paxi.Enterprise.Common.Domain.Contract.Base;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Paxi.Enterprise.Common.Infra.Database.Generic
{
    public abstract class DataContext : DbContext
    {
        public DataContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges()
        {
            TrackChanges();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            TrackChanges();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void TrackChanges()
        {
            foreach (EntityEntry entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                if (entry.Entity is IEntityBase)
                {
                    IEntityBase auditable = entry.Entity as IEntityBase;
                    if (entry.State == EntityState.Added)
                    {
                        //auditable.CreatedBy = UserProvider;//
                        auditable.Created = TimestampProvider();
                        auditable.Active = true;
                        auditable.Updated = TimestampProvider();
                    }
                    else
                    {
                        //auditable.UpdatedBy = UserProvider;
                        auditable.Updated = TimestampProvider();
                    }
                }
            }
        }

        public Func<DateTime> TimestampProvider { get; set; } = () => DateTime.UtcNow;
    }
}
