using System.Collections.Generic;
using Iql.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class EventTests
    {
        public EventEmitter<int> _emitter = new EventEmitter<int>(BackfireMode.Last);
        public EventEmitter<int> _emitterAll = new EventEmitter<int>(BackfireMode.All);
        public EventEmitter<int> _emitterNone = new EventEmitter<int>();

        [TestMethod]
        public void BackfireNone()
        {
            _emitterNone.Emit(() => 1);
            _emitterNone.Emit(() => 2);
            _emitterNone.Emit(() => 3);
            var list = new List<int>();
            _emitterNone.Subscribe(_ => { list.Add(_); });
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void BackfireLast()
        {
            _emitter.Emit(() => 1);
            _emitter.Emit(() => 2);
            _emitter.Emit(() => 3);
            var list = new List<int>();
            _emitter.Subscribe(_ => { list.Add(_); });
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(3, list[0]);
        }

        [TestMethod]
        public void BackfireAll()
        {
            _emitterAll.Emit(() => 1);
            _emitterAll.Emit(() => 2);
            _emitterAll.Emit(() => 3);
            var list = new List<int>();
            _emitterAll.Subscribe(_ => { list.Add(_); });
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(1, list[0]);
            Assert.AreEqual(2, list[1]);
            Assert.AreEqual(3, list[2]);
        }
    }
}