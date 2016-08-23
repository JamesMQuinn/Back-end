using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStackServiceTemplate.ServiceDefinition;

namespace ServiceStackServiceTemplate.Tests.UnitTests.Services
{
    [TestClass]
    public class ServiceDefinitionInfoShould
    {
        [TestMethod]
        public void Name_AllScenarios_CorrectName()
        {
            //Assert
            Assert.AreEqual("ServiceDefinitionInfo", ServiceDefinitionInfo.Name);
        }

        [TestMethod]
        public void Assembly_AllScenarios_CorrectAssembly()
        {
            //Assert
            Assert.AreEqual(typeof(ServiceDefinitionInfo).Assembly, ServiceDefinitionInfo.Assembly);
        }
    }
}
