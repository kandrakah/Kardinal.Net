using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Kardinal.Net.Web.Auth
{
    /// <summary>
    /// Extensões para <see cref="HttpResponse"/>
    /// </summary>
    public static class HttpResponseExtensions
    {
        public static void SetCache(this HttpResponse response, int maxAge, params string[] varyBy)
        {
            if (maxAge == 0)
            {
                response.SetNoCache();
            }
            else if (maxAge > 0)
            {
                if (!response.Headers.ContainsKey("Cache-Control"))
                {
                    response.Headers.Add("Cache-Control", $"max-age={maxAge}");
                }

                if (varyBy?.Any() == true)
                {
                    var vary = varyBy.Aggregate((x, y) => x + "," + y);
                    if (response.Headers.ContainsKey("Vary"))
                    {
                        vary = response.Headers["Vary"].ToString() + "," + vary;
                    }
                    response.Headers["Vary"] = vary;
                }
            }
        }

        public static void SetNoCache(this HttpResponse response)
        {
            if (!response.Headers.ContainsKey("Cache-Control"))
            {
                response.Headers.Add("Cache-Control", "no-store, no-cache, max-age=0");
            }
            else
            {
                response.Headers["Cache-Control"] = "no-store, no-cache, max-age=0";
            }

            if (!response.Headers.ContainsKey("Pragma"))
            {
                response.Headers.Add("Pragma", "no-cache");
            }
        }
    }
}
