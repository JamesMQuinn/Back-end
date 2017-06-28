using FluentAssertions;
using Funq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStackServiceLog4NetTemplate.Interfaces;
using System;
using System.Linq;

namespace ServiceStackServiceLog4NetTemplate.Tests.UnitTests
{
    [TestClass]
    public class ContainerManagerShould
    {
        /// <summary>
        /// Verifies all interfaces defined in the Interfaces assembly are registered in the Container.
        /// </summary>
        [TestMethod]
        public void RegisterEveryInterfaceInTheInterfacesAssembly()
        {
            // Arrange
            var container = new Container();
            var interfacesAssemblyInfoType = typeof(InterfacesAssemblyInfo);
            var interfacesAssembly = interfacesAssemblyInfoType.Assembly;
            var interfacesNamespace = interfacesAssemblyInfoType.Namespace;

            var interfacesThatDoNotNeedToBeRegistered = new[]
            {
                typeof(IExampleInterface)
            };

            //var expectedGenericInterfaces = new[]
            //{
            //    typeof(IExampleGenericInterface<ExampleType>)
            //};

            var expectedInterfaces = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsInterface)
                .Where(type => !type.IsGenericType)
                .Where(type => type.Namespace.StartsWith(interfacesNamespace))
                .Except(interfacesThatDoNotNeedToBeRegistered);
            //  .Union(expectedGenericInterfaces);

            // Act
            ContainerManager.Register(container);

            // Assert
            foreach (var expectedInterface in expectedInterfaces)
            {
                var actualInterface = container.TryResolve(expectedInterface);

                actualInterface.Should().NotBeNull();
            }
        }
    }
}
