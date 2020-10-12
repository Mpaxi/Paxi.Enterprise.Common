using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paxi.Enterprise.Common.WebApi.Model;

namespace Paxi.Enterprise.Common.WebApi.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Make the Primary Key associated with the property UniqueKey
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Active).IsRequired();
            builder.Property(o => o.Created).IsRequired();
            builder.Property(o => o.Updated).IsRequired();

            builder.HasKey(o => o.Name);

            builder.ToTable("User");

        }
    }
}
