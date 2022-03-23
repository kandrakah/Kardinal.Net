using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kardinal.Net.MediaTypes.Tests
{
    [TestClass]
    public class TypeLoaderTests
    {
        [TestMethod]
        public void Test()
        {
            var assembly = typeof(FileDetector).Assembly;
            var types = TypeLoader.GetTypeInfos(assembly);

            var str = string.Empty;
            foreach(var type in types)
            {
                str += $"{type}\n";
            }

            Console.WriteLine(str);
        }
    }
}
