﻿using System;
#if TypeScript
using Iql.JavaScript.JavaScriptExpressionToIql;
#else
using Iql.DotNet;
#endif
using Iql.Queryable;
using Iql.Queryable.Expressions;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests
{
    public class TestsBase
    {
        static TestsBase()
        {
#if TypeScript
            IqlExpressionConversion.DefaultExpressionConverter = () => new JavaScriptExpressionConverter();
#else
            IqlExpressionConversion.DefaultExpressionConverter = () => new DotNetExpressionConverter();
#endif
            new HazceptionDataStore().GetData();
        }
        public static AppDbContext Db => TestsBlock.Db;

        [ClassInitialize]
        public static void SetUp(TestContext textContext)
        {
        }

        [ClassCleanup]
        public static void CleanUp()
        {

        }

        [TestInitialize]
        public void TestInitialize()
        {
            // Boot
            var c = Db.EntityConfigurationContext;
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            TestsBlock.TestCleanUp();
        }

        public virtual void ShouldThrowException<TException>(Action action)
        {
            var exceptionCount = 0;
            try
            {
                action();
            }
            catch (Exception e)
            {
                exceptionCount++;
                Assert.IsTrue(e is TException || e.InnerException is TException);
            }
            Assert.AreEqual(exceptionCount, 1);
        }
    }
}