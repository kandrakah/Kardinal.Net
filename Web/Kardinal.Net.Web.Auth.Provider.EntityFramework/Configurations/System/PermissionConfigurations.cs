using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kardinal.Net.Web.Auth
{
    public class PermissionConfigurations : IEntityTypeConfiguration<PermissionEntity>
    {
        public void Configure(EntityTypeBuilder<PermissionEntity> builder)
        {
            builder.ToTable(TableNames.System.PermissionGroup.PERMISSIONS, TableNames.System.SCHEMA);

            builder.HasKey(x => x.Id).HasName("PK_PERMISSION_ID");
            builder.HasIndex(x => x.GroupId).IsUnique(false).HasDatabaseName("IDX_PERMISSION_GROUP_ID");
            builder.HasIndex(x => x.Key).IsUnique().HasDatabaseName("IDX_PERMISSION_KEY");
            builder.Property(x => x.Version).IsConcurrencyToken().IsRowVersion().IsRequired();

            builder.Property(x => x.Key).HasMaxLength(150).IsRequired();
            builder.Property(x => x.DisplayName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(150).IsRequired();
        }
    }
}
