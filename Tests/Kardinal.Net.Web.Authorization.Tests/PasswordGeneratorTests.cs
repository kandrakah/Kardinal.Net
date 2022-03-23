using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kardinal.Net.Web.Authorization.Tests
{
    [TestClass]
    public class PasswordGeneratorTests
    {
        [TestMethod]
        public void GeneratePassword()
        {
            var generator = PasswordGeneratorService.Instance;
            var password = generator.Generate('o', 'O', '0', 'l', 'I');

            Console.WriteLine(password);
        }
    }
}
