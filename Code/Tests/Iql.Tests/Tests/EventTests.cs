using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class EventTests
    {
        [TestMethod]
        public void PauseEventManagerTest()
        {
            var counter = 0;
            var emitter = new EventEmitter<int>();
            var manager = new IqlEventManager();
            var sub1 = manager.Subscribe(emitter, _ => { counter++; });
            var sub2 = manager.Subscribe(emitter, _ => { counter++; });
            var sub3 = manager.Subscribe(emitter, _ => { counter++; });
            emitter.Emit(() => 0);
            Assert.AreEqual(3, counter);
            sub2.Pause();
            emitter.Emit(() => 0);
            Assert.AreEqual(5, counter);
            manager.Pause();
            emitter.Emit(() => 0);
            emitter.Emit(() => 0);
            emitter.Emit(() => 0);
            emitter.Emit(() => 0);
            Assert.AreEqual(5, counter);
            manager.Resume();
            emitter.Emit(() => 0);
            emitter.Emit(() => 0);
            Assert.AreEqual(11, counter);
        }

        [TestMethod]
        public async Task PauseEventManagerAsyncTest()
        {
            var counter = 0;
            var emitter = new AsyncEventEmitter<int>();
            var manager = new IqlEventManager();
            var sub1 = manager.SubscribeAsync(emitter, async _ => { counter++; });
            var sub2 = manager.SubscribeAsync(emitter, async _ => { counter++; });
            var sub3 = manager.SubscribeAsync(emitter, async _ => { counter++; });
            await emitter.EmitAsync(() => 0);
            Assert.AreEqual(3, counter);
            sub2.Pause();
            await emitter.EmitAsync(() => 0);
            Assert.AreEqual(5, counter);
            manager.Pause();
            await emitter.EmitAsync(() => 0);
            await emitter.EmitAsync(() => 0);
            await emitter.EmitAsync(() => 0);
            await emitter.EmitAsync(() => 0);
            Assert.AreEqual(5, counter);
            manager.Resume();
            await emitter.EmitAsync(() => 0);
            await emitter.EmitAsync(() => 0);
            Assert.AreEqual(11, counter);
        }

        [TestMethod]
        public void PauseEventTest()
        {
            var counter = 0;
            var emitter = new EventEmitter<int>();
            var sub = emitter.Subscribe(_ => { counter++; });
            emitter.Emit(() => 0);
            emitter.Emit(() => 0);
            sub.Pause();
            Assert.AreEqual(2, counter);
            emitter.Emit(() => 0);
            Assert.AreEqual(2, counter);
            emitter.Emit(() => 0);
            emitter.Emit(() => 0);
            emitter.Emit(() => 0);
            Assert.AreEqual(2, counter);
            sub.Resume();
            emitter.Emit(() => 0);
            Assert.AreEqual(3, counter);
        }

        [TestMethod]
        public async Task PauseAsyncEventTest()
        {
            var counter = 0;
            var emitter = new AsyncEventEmitter<int>();
            var sub = emitter.SubscribeAsync(async _ => { counter++; });
            await emitter.EmitAsync(() => 0);
            await emitter.EmitAsync(() => 0);
            sub.Pause();
            Assert.AreEqual(2, counter);
            await emitter.EmitAsync(() => 0);
            Assert.AreEqual(2, counter);
            await emitter.EmitAsync(() => 0);
            await emitter.EmitAsync(() => 0);
            await emitter.EmitAsync(() => 0);
            Assert.AreEqual(2, counter);
            sub.Resume();
            await emitter.EmitAsync(() => 0);
            Assert.AreEqual(3, counter);
        }

        [TestMethod]
        public void OnSubscribe()
        {
            var counter = 0;
            var emitter = new EventEmitter<int>();
            emitter.OnSubscribe.Subscribe(_ => { counter++; });
            emitter.Emit(() => 1);
            emitter.Emit(() => 2);
            emitter.Emit(() => 3);
            Assert.AreEqual(0, counter);
            emitter.Subscribe(_ => { });
            Assert.AreEqual(1, counter);
            emitter.Subscribe(_ => { });
            emitter.Subscribe(_ => { });
            Assert.AreEqual(3, counter);
        }

        [TestMethod]
        public void OnSubscribeInstantEvent()
        {
            var counter1 = 0;
            var counter2 = 0;
            var counter3 = 0;
            var otherEmitter = new EventEmitter<int>();
            var otherSub = otherEmitter.Subscribe(_ => { counter3++; });
            var emitter = new EventEmitter<int>();
            emitter.Subscribe(_ => counter1++);
            emitter.OnSubscribe.Subscribe(_ => { emitter.Emit(() => 999, null, new[]{ _, otherSub }); });
            emitter.Subscribe(_ => counter2++);
            Assert.AreEqual(0, counter1);
            Assert.AreEqual(1, counter2);
            Assert.AreEqual(0, counter3);
        }

        [TestMethod]
        public void BackfireNone()
        {
            var emitter = new EventEmitter<int>();
            emitter.Emit(() => 1);
            emitter.Emit(() => 2);
            emitter.Emit(() => 3);
            var list = new List<int>();
            emitter.Subscribe(_ => { list.Add(_); });
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void BackfireLast()
        {
            var emitter = new EventEmitter<int>(BackfireMode.Last);
            emitter.Emit(() => 1);
            emitter.Emit(() => 2);
            emitter.Emit(() => 3);
            var list = new List<int>();
            emitter.Subscribe(_ => { list.Add(_); });
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(3, list[0]);
        }

        [TestMethod]
        public void BackfireLastCleared()
        {
            var emitter = new EventEmitter<int>(BackfireMode.Last);
            emitter.Emit(() => 1);
            emitter.Emit(() => 2);
            emitter.Emit(() => 3);
            var list = new List<int>();
            emitter.ClearBackfires();
            emitter.Subscribe(_ => { list.Add(_); });
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void BackfireAll()
        {
            var emitter = new EventEmitter<int>(BackfireMode.All);
            emitter.Emit(() => 1);
            emitter.Emit(() => 2);
            emitter.Emit(() => 3);
            var list = new List<int>();
            emitter.Subscribe(_ => { list.Add(_); });
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(1, list[0]);
            Assert.AreEqual(2, list[1]);
            Assert.AreEqual(3, list[2]);
        }


        [TestMethod]
        public void BackfireAllCleared()
        {
            var emitter = new EventEmitter<int>(BackfireMode.All);
            emitter.Emit(() => 1);
            emitter.Emit(() => 2);
            emitter.Emit(() => 3);
            var list = new List<int>();
            emitter.ClearBackfires();
            emitter.Subscribe(_ => { list.Add(_); });
            Assert.AreEqual(0, list.Count);
        }
    }
}