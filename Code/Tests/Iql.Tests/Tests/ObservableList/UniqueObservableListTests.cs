using System.Collections.Generic;
using Iql.Entities.Extensions;
using Iql.Entities.Lists;
using Iql.Entities.Lists.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.DataContextTests
{
    [TestClass]
    public class UniqueObservableListTests
    {
        public void AssertEvent(IList<IObservableListChangeEvent> events, int index, ObservableListChangeKind kind, object item)
        {
            Assert.AreEqual(events[index].Kind, kind);
            Assert.AreEqual(events[index].Item, item);
        }

        [TestMethod]
        public void TestListAddRange()
        {
            var l = new UniqueObservableList<string>();
            l.Add("a");
            l.Add("b");
            List<IObservableListChangeEvent> events = new List<IObservableListChangeEvent>();
            l.Change.Subscribe(_ => { events.Add(_); });
            l.AddRange(new[] { "a", "d" });
            Assert.AreEqual(2, events.Count);
            Assert.AreEqual(3, l.Count);
            AssertEvent(events, 0, ObservableListChangeKind.Adding, "d");
            AssertEvent(events, 1, ObservableListChangeKind.Added, "d");
        }

        [TestMethod]
        public void TestListAddRangeNotWithSubClass()
        {
            var l = new ObservableList<string>().SetEnsureUnique();
            l.Add("a");
            l.Add("b");
            List<IObservableListChangeEvent> events = new List<IObservableListChangeEvent>();
            l.Change.Subscribe(_ => { events.Add(_); });
            l.AddRange(new[] { "a", "d" });
            Assert.AreEqual(2, events.Count);
            Assert.AreEqual(3, l.Count);
            AssertEvent(events, 0, ObservableListChangeKind.Adding, "d");
            AssertEvent(events, 1, ObservableListChangeKind.Added, "d");
        }

        [TestMethod]
        public void TestListAdd()
        {
            var l = new UniqueObservableList<string>();
            l.Add("a");
            l.Add("b");
            List<IObservableListChangeEvent> events = new List<IObservableListChangeEvent>();
            l.Change.Subscribe(_ => { events.Add(_); });
            l.Add("a");
            l.Add("d");
            Assert.AreEqual(2, events.Count);
            Assert.AreEqual(3, l.Count);
            AssertEvent(events, 0, ObservableListChangeKind.Adding, "d");
            AssertEvent(events, 1, ObservableListChangeKind.Added, "d");
        }

        //[TestMethod]
        //public void TestRemoveValue()
        //{
        //    var l = new ObservableList<string>();
        //    l.Add("a");
        //    l.Add("b");
        //    List<IObservableListChangeEvent> events = new List<IObservableListChangeEvent>();
        //    l.Change.Subscribe(_ => { events.Add(_); });
        //    l.Remove("a");
        //    l.Remove("d");
        //    Assert.AreEqual(2, events.Count);
        //    AssertEvent(events, 0, ObservableListChangeKind.Removing);
        //    AssertEvent(events, 1, ObservableListChangeKind.Removed);
        //}

        //[TestMethod]
        //public void TestRemoveAt()
        //{
        //    var l = new ObservableList<string>();
        //    l.Add("a");
        //    l.Add("b");
        //    List<IObservableListChangeEvent> events = new List<IObservableListChangeEvent>();
        //    l.Change.Subscribe(_ => { events.Add(_); });
        //    l.RemoveAt(1);
        //    Assert.AreEqual(2, events.Count);
        //    AssertEvent(events, 0, ObservableListChangeKind.Removing);
        //    AssertEvent(events, 1, ObservableListChangeKind.Removed);
        //}

        //[TestMethod]
        //public void TestClear()
        //{
        //    var l = new ObservableList<string>();
        //    l.Add("a");
        //    l.Add("b");
        //    List<IObservableListChangeEvent> events = new List<IObservableListChangeEvent>();
        //    l.Change.Subscribe(_ => { events.Add(_); });
        //    l.Clear();
        //    Assert.AreEqual(4, events.Count);
        //    AssertEvent(events, 0, ObservableListChangeKind.Removing);
        //    AssertEvent(events, 1, ObservableListChangeKind.Removed);
        //    AssertEvent(events, 2, ObservableListChangeKind.Removing);
        //    AssertEvent(events, 3, ObservableListChangeKind.Removed);
        //}

        [TestMethod]
        public void TestIndexReplace()
        {
            var l = new UniqueObservableList<string>();
            l.Add("a");
            l.Add("b");
            List<IObservableListChangeEvent> events = new List<IObservableListChangeEvent>();
            l.Change.Subscribe(_ => { events.Add(_); });
            l[1] = "a";
            Assert.AreEqual(0, events.Count);
            Assert.AreEqual(l[1], "b");
            l[1] = "d";
            Assert.AreEqual(4, events.Count);
            AssertEvent(events, 0, ObservableListChangeKind.Removing, "b");
            AssertEvent(events, 1, ObservableListChangeKind.Removed, "b");
            AssertEvent(events, 2, ObservableListChangeKind.Adding, "d");
            AssertEvent(events, 3, ObservableListChangeKind.Added, "d");
            Assert.AreEqual(l[1], "d");
        }
    }
}