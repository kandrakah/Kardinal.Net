using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kardinal.Net.Web.Auth
{
    public class DataProtectionKeyConfigurations : IEntityTypeConfiguration<DataProtectionKey>
    {
        public void Configure(EntityTypeBuilder<DataProtectionKey> builder)
        {
            builder.ToTable(TableNames.System.Security.PROTECTION_KEYS, TableNames.System.SCHEMA);

            builder.HasKey(x => x.Id).HasName("PK_PROTECTION_KEY_ID");

            builder.Property(x => x.FriendlyName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Xml).HasColumnType("TEXT").IsRequired();
        }
    }
}
