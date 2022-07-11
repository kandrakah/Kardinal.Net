using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kardinal.Net.Web.Auth
{
    public class PermissionGroupConfigurations : IEntityTypeConfiguration<PermissionGroupEntity>
    {
        public void Configure(EntityTypeBuilder<PermissionGroupEntity> builder)
        {
            builder.ToTable(TableNames.System.PermissionGroup.PERMISSIONS_GROUPS, TableNames.System.SCHEMA);

            builder.HasKey(x => x.Id).HasName("PK_PERMISSION_GROUP_ID");
            builder.Property(x => x.Version).IsConcurrencyToken().IsRowVersion().IsRequired();

            builder.Property(x => x.DisplayName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(200).IsRequired();
            builder.Property(x => x.IsRestricted).HasDefaultValue(false).IsRequired();

            builder.HasMany(x => x.Permissions).WithOne(x => x.Group).OnDelete(DeleteBehavior.Restrict).HasConstraintName("FK_PERMISSION_GROUP_PERMISSIONS");

        }
    }
}
