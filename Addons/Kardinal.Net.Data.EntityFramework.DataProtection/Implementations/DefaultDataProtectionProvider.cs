using Microsoft.AspNetCore.DataProtection;
using System.Security.Cryptography;

namespace Kardinal.Net.Data
{
    public sealed class DefaultDataProtectionProvider : IDataProtectionProvider, IDisposable
    {
        private readonly RSACryptoServiceProvider _provider;

        public DefaultDataProtectionProvider(RSAParameters parameters)
        {
            this._provider = new RSACryptoServiceProvider();
            this._provider.ImportParameters(parameters);
        }

        public IDataProtector CreateProtector(string purpose)
        {
            return new DefaultDataProtector(this._provider, purpose);
        }

        public void Dispose()
        {
            this._provider.Dispose();
        }
    }
}
