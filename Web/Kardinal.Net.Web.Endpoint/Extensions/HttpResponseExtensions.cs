using Microsoft.AspNetCore.Http;

namespace Kardinal.Net.Web
{
    /// <summary>
    /// Extensões para <see cref="HttpResponse"/>
    /// </summary>
    internal static class HttpResponseExtensions
    {
        internal static void SetNoCache(this HttpResponse response)
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
