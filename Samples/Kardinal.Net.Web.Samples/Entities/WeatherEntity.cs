using Kardinal.Net.Data;
using System;

namespace Kardinal.Net.Web.Samples.Entities
{
    public class WeatherEntity : Entity
    {
        [Protected]
        public string ProtectedSampleProperty { get; set; }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}
