using Microsoft.EntityFrameworkCore;
using Paxi.Enterprise.Common.Infra.Database.Generic;
using Paxi.Enterprise.Common.WebApi.Model;
using System.Diagnostics.CodeAnalysis;

namespace Paxi.Enterprise.Common.WebApi.Context
{
    public class ApplicationDbContext : DataContext
    {
        public virtual DbSet<User> User { get; set; }
        public ApplicationDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    //modelBuilder.ApplyConfiguration(new UserConfiguration());
        //}
    }
}
