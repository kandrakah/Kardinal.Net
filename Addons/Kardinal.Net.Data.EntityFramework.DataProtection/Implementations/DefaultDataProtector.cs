using Microsoft.AspNetCore.DataProtection;
using System.Security.Cryptography;

namespace Kardinal.Net.Data
{
    public class DefaultDataProtector : IDataProtector
    {
        private readonly RSACryptoServiceProvider _provider;

        private readonly string _purpose;

        internal DefaultDataProtector(RSACryptoServiceProvider provider, string purpose)
        {
            this._provider = provider;
            this._purpose = purpose;
        }

        public IDataProtector CreateProtector(string purpose)
        {
            return new DefaultDataProtector(this._provider, purpose);
        }

        public byte[] Protect(byte[] plaintext)
        {
            return this._provider.Encrypt(plaintext, true);
        }

        public byte[] Unprotect(byte[] protectedData)
        {
            return this._provider.Decrypt(protectedData, true);
        }
    }
}
