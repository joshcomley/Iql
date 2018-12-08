using Iql.Entities.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests
{
    public class ServiceProviderTestBaseClass
    {

    }
    public class ServiceProviderTestClass : ServiceProviderTestBaseClass
    {

    }

    [TestClass]
    public class ServiceProviderTests
    {
        [TestMethod]
        public void TestInstance()
        {
            var source1 = new ServiceProviderTestClass();
            var serviceProvider = new IqlServiceProvider();
            serviceProvider.RegisterInstance(source1);
            var copy1 = serviceProvider.Resolve<ServiceProviderTestClass>();
            var copy2 = serviceProvider.Resolve<ServiceProviderTestClass>();
            var copy3 = serviceProvider.Resolve<ServiceProviderTestClass>();
            var copy4 = serviceProvider.Resolve<ServiceProviderTestClass>();
            var copy5 = serviceProvider.Resolve<ServiceProviderTestClass>();
            var copy6 = serviceProvider.Resolve<ServiceProviderTestClass>();
            Assert.AreEqual(copy1, source1);
            Assert.AreEqual(copy1, copy2);
            Assert.AreEqual(copy1, copy3);
            Assert.AreEqual(copy1, copy4);
            Assert.AreEqual(copy1, copy5);
            Assert.AreEqual(copy1, copy6);
        }

        [TestMethod]
        public void TestFactory()
        {
            var count = 0;
            var serviceProvider = new IqlServiceProvider();
            var source1 = new ServiceProviderTestClass();
            var source2 = new ServiceProviderTestClass();
            serviceProvider.RegisterFactory(() =>
            {
                count++;
                if (count < 3)
                {
                    return source1;
                }
                return source2;
            });
            var copy1 = serviceProvider.Resolve<ServiceProviderTestClass>();
            var copy2 = serviceProvider.Resolve<ServiceProviderTestClass>();
            var copy3 = serviceProvider.Resolve<ServiceProviderTestClass>();
            var copy4 = serviceProvider.Resolve<ServiceProviderTestClass>();
            Assert.AreEqual(source1, copy1);
            Assert.AreEqual(source1, copy2);
            Assert.AreEqual(source2, copy3);
            Assert.AreEqual(source2, copy4);
            Assert.AreNotEqual(copy1, copy3);
        }

        [TestMethod]
        public void TestAutoFactory()
        {
            var serviceProvider = new IqlServiceProvider();
            serviceProvider.Register<ServiceProviderTestClass>();
            var copy1 = serviceProvider.Resolve<ServiceProviderTestClass>();
            var copy2 = serviceProvider.Resolve<ServiceProviderTestClass>();
            var copy3 = serviceProvider.Resolve<ServiceProviderTestClass>();
            Assert.AreNotEqual(copy1, copy2);
            Assert.AreNotEqual(copy1, copy3);
            Assert.AreNotEqual(copy2, copy3);
        }

        [TestMethod]
        public void TestBaseProvider()
        {
            var source1 = new ServiceProviderTestClass();
            var serviceProvider1 = new IqlServiceProvider();
            var serviceProvider2 = new IqlServiceProvider();
            serviceProvider1.RegisterInstance(source1);
            var copy1 = serviceProvider1.Resolve<ServiceProviderTestClass>();
            var copy2 = serviceProvider2.Resolve<ServiceProviderTestClass>();
            Assert.IsNull(copy2);
            serviceProvider2.BaseProvider = serviceProvider1;
            copy2 = serviceProvider2.Resolve<ServiceProviderTestClass>();
            Assert.AreEqual(copy1, copy2);
            serviceProvider2.RegisterFactory(() => new ServiceProviderTestClass());
            var copy1b = serviceProvider1.Resolve<ServiceProviderTestClass>();
            var copy2b = serviceProvider2.Resolve<ServiceProviderTestClass>();
            Assert.AreEqual(copy1, copy1b);
            Assert.AreNotEqual(copy1b, copy2b);
            Assert.AreNotEqual(copy2b, copy1);
        }

        [TestMethod]
        public void TestResolveBaseClass()
        {
            var serviceProvider1 = new IqlServiceProvider();
            serviceProvider1.Register<ServiceProviderTestClass>();
            var copy1 = serviceProvider1.Resolve<ServiceProviderTestBaseClass>();
            Assert.IsNotNull(copy1);
        }
    }
}