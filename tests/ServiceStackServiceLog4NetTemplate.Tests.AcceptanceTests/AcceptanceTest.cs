using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CHR.ServiceStack.Plugins.Models.HealthCheckFeature;
using ServiceStack;

namespace ServiceStackServiceLog4NetTemplate.Tests.AcceptanceTests
{
    [TestClass]
    public class AcceptanceTest
    {
        private IJsonServiceClient _jsonClient;

        [TestInitialize]
        public void InitializeTestData()
        {
            Console.WriteLine("--AppSettings--");
            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                Console.WriteLine($"{key}: '{ConfigurationManager.AppSettings[key]}'");
            }
            Console.WriteLine("--End AppSettings--");
            _jsonClient = new JsonServiceClient(ConfigurationManager.AppSettings["ServiceUrl"]);
        }
        [TestCleanup]
        public void Cleanup()
        {
            _jsonClient.Dispose();
        }
        [TestMethod, TestCategory("AutomatedAcceptance")]
        public void CheckServiceHealth_ShouldBeHealthy()
        {
            HealthCheckRequest request = new HealthCheckRequest();

            Console.WriteLine($"Calling Health Check...");
            HealthCheckResponse response = _jsonClient.Get(request);

            if (response.AmIHealthy)
            {
                Console.WriteLine("Healthy Health Check Returned.");
                Assert.IsTrue(true);
            }
            else
            {
                Console.WriteLine($"Found {response?.UnresolvedUrls?.Count} unresolved urls");
                if (response?.UnresolvedUrls != null)
                {
                    foreach (var unresolvedUrl in response.UnresolvedUrls)
                    {
                        Console.WriteLine($"Url/DB that was unresolved: {unresolvedUrl}");
                    }
                    Assert.IsTrue(false);
                }
                else
                {
                    Console.WriteLine($"AmIHealthy is False, but UnResolved URLS are Null");
                    Assert.IsTrue(false);
                }
            }
        }
    }
}
