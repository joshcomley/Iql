using System.Collections.Generic;
using Iql.Entities.Lists;
using Iql.Entities.Lists.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.DataContextTests
{
    [TestClass]
    public class ObservableListTests
    {
        public void AssertEvent(IList<IObservableListChangeEvent> events, int index, ObservableListChangeKind kind, object item)
        {
            Assert.AreEqual(events[index].Kind, kind);
        }

        [TestMethod]
        public void TestListAddRange()
        {
            var l = new ObservableList<string>();
            l.Add("a");
            l.Add("b");
            List<IObservableListChangeEvent> events = new List<IObservableListChangeEvent>();
            l.Change.Subscribe(_ => { events.Add(_); });
            l.AddRange(new[] { "c", "d" });
            Assert.AreEqual(4, events.Count);
            Assert.AreEqual(4, l.Count);
            AssertEvent(events, 0, ObservableListChangeKind.Adding, "c");
            AssertEvent(events, 1, ObservableListChangeKind.Added, "c");
            AssertEvent(events, 2, ObservableListChangeKind.Adding, "d");
            AssertEvent(events, 3, ObservableListChangeKind.Added, "d");
        }

        [TestMethod]
        public void TestListAdd()
        {
            var l = new ObservableList<string>();
            l.Add("a");
            l.Add("b");
            List<IObservableListChangeEvent> events = new List<IObservableListChangeEvent>();
            l.Change.Subscribe(_ => { events.Add(_); });
            l.Add("c");
            l.Add("d");
            Assert.AreEqual(4, events.Count);
            Assert.AreEqual(4, l.Count);
            AssertEvent(events, 0, ObservableListChangeKind.Adding, "c");
            AssertEvent(events, 1, ObservableListChangeKind.Added, "c");
            AssertEvent(events, 2, ObservableListChangeKind.Adding, "d");
            AssertEvent(events, 3, ObservableListChangeKind.Added, "d");
        }

        [TestMethod]
        public void TestRemoveValue()
        {
            var l = new ObservableList<string>();
            l.Add("a");
            l.Add("b");
            List<IObservableListChangeEvent> events = new List<IObservableListChangeEvent>();
            l.Change.Subscribe(_ => { events.Add(_); });
            l.Remove("a");
            l.Remove("d");
            Assert.AreEqual(2, events.Count);
            AssertEvent(events, 0, ObservableListChangeKind.Removing, "a");
            AssertEvent(events, 1, ObservableListChangeKind.Removed, "a");
        }

        [TestMethod]
        public void TestRemoveAt()
        {
            var l = new ObservableList<string>();
            l.Add("a");
            l.Add("b");
            List<IObservableListChangeEvent> events = new List<IObservableListChangeEvent>();
            l.Change.Subscribe(_ => { events.Add(_); });
            l.RemoveAt(1);
            Assert.AreEqual(2, events.Count);
            AssertEvent(events, 0, ObservableListChangeKind.Removing, "b");
            AssertEvent(events, 1, ObservableListChangeKind.Removed, "b");
        }

        [TestMethod]
        public void TestClear()
        {
            var l = new ObservableList<string>();
            l.Add("a");
            l.Add("b");
            List<IObservableListChangeEvent> events = new List<IObservableListChangeEvent>();
            l.Change.Subscribe(_ => { events.Add(_); });
            l.Clear();
            Assert.AreEqual(4, events.Count);
            AssertEvent(events, 0, ObservableListChangeKind.Removing, "a");
            AssertEvent(events, 1, ObservableListChangeKind.Removed, "a");
            AssertEvent(events, 2, ObservableListChangeKind.Removing, "b");
            AssertEvent(events, 3, ObservableListChangeKind.Removed, "b");
        }

        [TestMethod]
        public void TestIndexReplace()
        {
            var l = new ObservableList<string>();
            l.Add("a");
            l.Add("b");
            List<IObservableListChangeEvent> events = new List<IObservableListChangeEvent>();
            l.Change.Subscribe(_ => { events.Add(_); });
            l[1] = "c";
            Assert.AreEqual(4, events.Count);
            AssertEvent(events, 0, ObservableListChangeKind.Removing, "b");
            AssertEvent(events, 1, ObservableListChangeKind.Removed, "b");
            AssertEvent(events, 2, ObservableListChangeKind.Adding, "c");
            AssertEvent(events, 3, ObservableListChangeKind.Added, "c");
        }
    }
}