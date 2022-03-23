using Kardinal.Net.Web.Samples.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kardinal.Net.Web.Samples.Data
{
    public class WeatherConfigurations : IEntityTypeConfiguration<WeatherEntity>
    {
        /// <summary>
        /// Instância do provedor de proteção de dados.
        /// </summary>
        private readonly IDataProtectionProvider _provider;

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="provider">Instância do provedor de proteção.</param>
        public WeatherConfigurations(IDataProtectionProvider provider)
        {
            this._provider = provider;
        }

        /// <summary>
        /// Método de construção da configuração.
        /// </summary>
        /// <param name="builder">Instância do construtor de configurações.</param>
        public void Configure(EntityTypeBuilder<WeatherEntity> builder)
        {
            builder.HasKey(x => x.Id).HasName("PK_WEATHER_ID");

            //builder.Property(x => x.Version).IsConcurrencyToken().IsRowVersion().IsRequired();
            builder.Property(x => x.ProtectedSampleProperty).IsProtected(this._provider);
        }
    }
}
