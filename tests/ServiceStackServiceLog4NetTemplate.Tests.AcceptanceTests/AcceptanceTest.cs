using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ServiceStackServiceLog4NetTemplate.Tests.AcceptanceTests
{
    [TestClass]
    public class AcceptanceTest
    {
        [TestInitialize]
        public void InitializeTestData()
        {
            Console.WriteLine("--AppSettings--");
            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                Console.WriteLine($"{key}: '{ConfigurationManager.AppSettings[key]}'");
            }
            Console.WriteLine("--End AppSettings--");
        }
    }
}
