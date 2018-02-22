using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.OData
{
    public static class RequestLogExtensions
    {
        public static void AssertEmpty(this RequestLog log)
        {
            Assert.AreEqual(0, log.Posts.Count);
            Assert.AreEqual(0, log.Patches.Count);
            Assert.AreEqual(0, log.Deletes.Count);
        }
    }
}