using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack.Text;

namespace ServiceStackServiceTemplate.Tests.UnitTests
{
    [TestClass]
    public class AppHostShould
    {
        [TestMethod]
        public void Configure_AllScenarios_JsonConfigured()
        {
            //Arrange
            var appHost = new AppHost();
            var funqContainer = new Funq.Container();

            //Act
            appHost.Configure(funqContainer);

            //Assert
            Assert.IsTrue(JsConfig.EmitCamelCaseNames);
            Assert.AreEqual(DateHandler.ISO8601, JsConfig.DateHandler);
        }
    }
}
