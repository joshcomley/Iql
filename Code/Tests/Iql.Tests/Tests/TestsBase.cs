﻿using System;
using System.Collections;
using System.Collections.Generic;
using Iql.Conversion;
using Iql.Data.DataStores.InMemory;
using Iql.Parsing.Types;
#if TypeScript
using Iql.JavaScript.JavaScriptExpressionToIql;
#else
using Iql.DotNet;
#endif
using Iql.Queryable;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

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
        public static ITypeResolver TypeResolver => Db.EntityConfigurationContext;

        [ClassInitialize]
        public static void SetUp(TestContext textContext)
        {
        }

        [ClassCleanup]
        public static void CleanUp()
        {

        }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            // Boot
            var c = Db.EntityConfigurationContext;
            if (Db.DataStore is InMemoryDataStore)
            {
                (Db.DataStore as InMemoryDataStore).Clear();
            }
        }

        [TestCleanup]
        public virtual void TestCleanUp()
        {
            TestsBlock.TestCleanUp();
            PostTestCleanUp();
        }

        protected virtual void PostTestCleanUp()
        {
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
            Assert.AreEqual(exceptionCount, 1, $"Expected exception {typeof(TException).Name} not thrown.");
        }

        public void AssertAllTrue<T>(IEnumerable<T> collection, Func<T, bool> action)
        {
            foreach (var item in collection)
            {
                Assert.IsTrue(action(item));
            }
        }

        public void AssertCollection(IList collection, params object[] args)
        {
            Assert.AreEqual(args == null ? 0 : args.Length, collection.Count);
            if (args != null)
            {
                for (var i = 0; i < args.Length; i++)
                {
                    var arg = args[i];
                    Assert.IsTrue(collection.Contains(arg));
                }
            }
        }
    }
}