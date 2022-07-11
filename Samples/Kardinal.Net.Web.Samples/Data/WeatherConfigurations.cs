using Kardinal.Net.Data;
using Kardinal.Net.Web.Samples.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography;

namespace Kardinal.Net.Web.Samples.Data
{
    public class WeatherConfigurations : IEntityTypeConfiguration<WeatherEntity>
    {
        private readonly string _key = "<RSAKeyValue><Modulus>0vSTzAC1XgCMUspBmc2QxCB4QAOZUsPHKGLEmcSZ7xKj8jtDPnuz6OhCHeB5fkP21l/Gri4pmrIgestumUyGFGxfGcxqeOrfI/um/gP/9dtH6fONkFoXQyj0tfbCRRjTwU0KueWZ7arO6qtzE4jv5FW9/AbaH4AMc9S5/uQBtPoWmnw4iuLNoq+oTnJC1lfrVG7rWJGOu8JKIPKeYZjpWO20sw0N817FotXzHk36kc+OZhBmrS3ZeHAW6gwOB/mtK56DhYbCrtZbBHzcY2cbWBZkaCQ6xku1oUFuIEKaNHWHt8+sUwv6cO5RcH6CqXRdBb8XrhDflHJOPKwN9ACd1Q==</Modulus><Exponent>AQAB</Exponent><P>1gts5erOvyvToG2pqRXLUYG4g6Q3fEQ7e8bdLPV8ohq8xyMfXwmynmarNYcpQdQH+ZkY/p2u8WSo/IFo4guX/SyApjnR89I34gnaERYPgsx7Xy+gXptEq+SSCxyTXHE4I7kdow9OX1nO8iOkcDa5ul21asNWhywW7wI2yU0pQk8=</P><Q>/E4i06vKlfUyj/GbOE1PaEo78K6JD+GQmEDkQ5lLwLBnK+orewDF/hGN2EGZofj9P2SFF9dv7TMOh4aT2nFyzXUVndYscOZKd8BKMldKIKlkYrs9ZgCRkcbBIeNDm0taPNo8cuzO67t8HYLZbuG0U1rvLXi2JJVijJHFgeEdCJs=</Q><DP>ug7kcldRsEvggavRtvEC867XPVcaBCMrscSDrfkWhbLINjIJqws7AQYt/TGoY1h0Njsmxu06jQr6+cNj8FoznBd7HQNVxaQOf6YgsImEoiYtd+hmdtZxMxpU+OO2FL02F5dqc7tyXOAsS78/yWtItv+OG2gBZy6kIM7D4Of53XE=</DP><DQ>RkFXr0tR4Qni53QmfboBV4/8OtvO7K4pvkurhCtGjcRRJkhRjTG88C9hKQuzRQf5NNK/wNDLqOgjrpdJynT2u8FToJzHyTRHHitcPcJSsN+aRWfQA/w6jLYkh1QqOi70VKeMV5AbWkbdm7YY56hPtzpGCYpoZa13QJ9CuRS/Kes=</DQ><InverseQ>DuRuH6+bsc/HJuU8MXdSVkZjJJ4FtM6zyj+qacVUgcAO3KFDKR068nYloyulM/Q3aL+CPi2dQURTbiZq4SSo38Bg4WcM62DbY19quM6dgqdFTfotf5wB9PDMqrFaUi7toxVBRDVYdY+mXI9vv8bwvhzGczy1eRgTdKqCHiyC8y0=</InverseQ><D>DijqjsPxaJji0l/PHXLF59RCHhHfxcVsZdKTXbrrljeLF6RQtC37eoO0hGrf3NDVDbiqDmD82+xe778d8l5HlbFchM/KyckCJ1kp8Vz1XlxUrGhztR/G9fQW0Ov6SEcNV6lDMg0uUtynPOs7MzF64Lm8oeEX3QtsSo4Z2fkWAD613iHBt8YCt7lXWo+SPfj6jS2jNZm88bHwGarKZ58lmPjU7bpmX5irZLfZUO+gHJ90MimSJuEih9j7yv66O3gjbp51G/+GRxWGa5Ms2T84L8/5Ai2rmtf9sF1Is9NluWRe6TyXVch5rs37iYKXeBGTWxqPOWj8zYl0ImyBx2HlVQ==</D></RSAKeyValue>";
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="provider">Instância do provedor de proteção.</param>
        public WeatherConfigurations()
        {
        }

        /// <summary>
        /// Método de construção da configuração.
        /// </summary>
        /// <param name="builder">Instância do construtor de configurações.</param>
        public void Configure(EntityTypeBuilder<WeatherEntity> builder)
        {
            using (var rsa = RSA.Create())
            {
                rsa.FromXmlString(this._key);
                builder.HasKey(x => x.Id).HasName("PK_WEATHER_ID");

                //builder.Property(x => x.Version).IsConcurrencyToken().IsRowVersion().IsRequired();
                builder.Property(x => x.ProtectedSampleProperty).IsProtected(rsa.ExportParameters(true));
            }
        }
    }
}
