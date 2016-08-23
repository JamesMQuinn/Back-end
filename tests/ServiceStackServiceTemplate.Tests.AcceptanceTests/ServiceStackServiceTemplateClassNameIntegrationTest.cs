using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ServiceStackServiceTemplate.Tests.IntegrationTests
{
    [TestClass]
    public class ServiceStackServiceTemplateClassNameIntegrationTest
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
