using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.Core.Internal;
using Funq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStackServiceLog4NetTemplate.Interfaces;

namespace ServiceStackServiceLog4NetTemplate.Tests.UnitTests
{
    [TestClass]
    public class ContainerManagerShould : IExampleInterface
    {
        /// <summary>
        /// Tests if there is something registered but not that it is the correct thing
        /// This proactively checks if a new interface has been registered so you catch problems with unit tests and not in AATs
        /// </summary>
        [TestMethod]
        public void Initialize_AllScenarios_AllInterfacesRegisteredWithSomething()
        {
            //Arrange
            // Add interfaces that shouldn't be registered here
            var exceptions = new List<string>()
            {
                "IExampleInterface" // The namespace needs at least one interface that is being used for the test to pass
            };
            var container = new Container();

            var nameSpace = "ServiceStackServiceLog4NetTemplate.Interfaces";

            // Get the interface assembly name
            var interfaceAssemblyName =
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(x => x.GetReferencedAssemblies())
                    .FirstOrDefault(x => x.Name == nameSpace);

            if (interfaceAssemblyName == null)
            {
                Assert.IsNotNull(interfaceAssemblyName, "No interface project found. Either the namespace used by the test is wrong or it has no interfaces in it.");
            }
            // so that then we can load it
            Assembly.Load(interfaceAssemblyName);

            // so then we can get all the interfaces!
            var interfaces = (from t in AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(t => t.GetTypes())
                              where t.IsInterface && t.Namespace != null && t.Namespace.StartsWith(nameSpace) && exceptions.All(e => t.Name != e)
                              select t);

            // Act
            ContainerManager.Register(container);

            //Assert
            // Check if the interfaces are registered with the container
            interfaces.ForEach(t =>
            {
                var resolveMethodInfo = typeof(Container).GetMethods().Single(m => m.Name == "Resolve" && !m.GetParameters().Any());

                var genericMethod = resolveMethodInfo.MakeGenericMethod(t);

                var result = genericMethod.Invoke(container, null);

                // (although it probably threw an exception if it failed)
                Assert.IsNotNull(result, $"{t.Name} should be registered with the container");
            });
        }

    }
}
