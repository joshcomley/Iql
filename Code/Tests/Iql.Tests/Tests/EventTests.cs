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
        public void PriorityTest()
        {
            var text = "";
            var emitter = new EventEmitter<int>();
            var manager = new IqlEventSubscriberManager();
            manager.Subscribe(emitter, _ => text += "w", null, null, 4);
            manager.Subscribe(emitter, _ => text += " ", null, null, 3);
            manager.Subscribe(emitter, _ => text += "o", null, null, 5);
            manager.Subscribe(emitter, _ => text += "t", null, null, 2);
            manager.Subscribe(emitter, _ => text += "i", null, null, 1);
            manager.Subscribe(emitter, _ => text += "r", null, null, 6);
            manager.Subscribe(emitter, _ => text += "!", null, null, 9);
            manager.Subscribe(emitter, _ => text += "k", null, null, 7);
            manager.Subscribe(emitter, _ => text += "s", null, null, 8);
            emitter.Emit(() => 0);
            Assert.AreEqual("it works!", text);
            manager.UnsubscribeAll();
        }
        
        [TestMethod]
        public async Task PriorityTestAsync()
        {
            var text = "";
            var emitter = new AsyncEventEmitter<int>();
            var manager = new IqlEventSubscriberManager();
            manager.SubscribeAsync(emitter, async _ => text += "w", null, null, 4);
            manager.SubscribeAsync(emitter, async _ => text += " ", null, null, 3);
            manager.SubscribeAsync(emitter, async _ => text += "o", null, null, 5);
            manager.SubscribeAsync(emitter, async _ => text += "t", null, null, 2);
            manager.SubscribeAsync(emitter, async _ => text += "i", null, null, 1);
            manager.SubscribeAsync(emitter, async _ => text += "r", null, null, 6);
            manager.SubscribeAsync(emitter, async _ => text += "!", null, null, 9);
            manager.SubscribeAsync(emitter, async _ => text += "k", null, null, 7);
            manager.SubscribeAsync(emitter, async _ => text += "s", null, null, 8);
            await emitter.EmitAsync(() => 0);
            Assert.AreEqual("it works!", text);
            manager.UnsubscribeAll();
        }

        [TestMethod]
        public void SubscriptionConfigureTest()
        {
            var hasSubscriptionsCounter = 0;
            var hasNoSubscriptionsCounter = 0;
            var emitter = new EventEmitter<int>();
            var manager = new IqlEventSubscriberManager();
            emitter.Configure(_ => { hasSubscriptionsCounter++; }, _ => { hasNoSubscriptionsCounter++; });
            Assert.AreEqual(0, hasSubscriptionsCounter);
            Assert.AreEqual(0, hasNoSubscriptionsCounter);
            manager.Subscribe(emitter, _ => { });
            Assert.AreEqual(1, hasSubscriptionsCounter);
            Assert.AreEqual(0, hasNoSubscriptionsCounter);
            manager.Subscribe(emitter, _ => { });
            manager.Subscribe(emitter, _ => { });
            manager.Subscribe(emitter, _ => { });
            manager.Subscribe(emitter, _ => { });
            Assert.AreEqual(1, hasSubscriptionsCounter);
            Assert.AreEqual(0, hasNoSubscriptionsCounter);
            manager.UnsubscribeAll();
            Assert.AreEqual(1, hasSubscriptionsCounter);
            Assert.AreEqual(1, hasNoSubscriptionsCounter);
            manager.UnsubscribeAll();
            manager.UnsubscribeAll();
            manager.UnsubscribeAll();
            manager.UnsubscribeAll();
            Assert.AreEqual(1, hasSubscriptionsCounter);
            Assert.AreEqual(1, hasNoSubscriptionsCounter);
        }

        [TestMethod]
        public void SubscriptionConfigureSubscribeTest()
        {
            var hasSubscriptionsCounter = 0;
            var hasNoSubscriptionsCounter = 0;
            var emitter = new EventEmitter<int>();
            var manager = new IqlEventSubscriberManager();
            emitter.Configure(_ => { hasSubscriptionsCounter++; }, _ => { hasNoSubscriptionsCounter++; });
            Assert.AreEqual(0, hasSubscriptionsCounter);
            Assert.AreEqual(0, hasNoSubscriptionsCounter);
            manager.Subscribe(emitter, _ => { });
            Assert.AreEqual(1, hasSubscriptionsCounter);
            Assert.AreEqual(0, hasNoSubscriptionsCounter);
            emitter.Emit(() => 0);
            Assert.AreEqual(1, hasSubscriptionsCounter);
            Assert.AreEqual(0, hasNoSubscriptionsCounter);
            emitter.Dispose();
            Assert.AreEqual(1, hasSubscriptionsCounter);
            Assert.AreEqual(1, hasNoSubscriptionsCounter);
        }

        [TestMethod]
        public void SubscriptionConfigureSubscribeOnceTest()
        {
            var hasSubscriptionsCounter = 0;
            var hasNoSubscriptionsCounter = 0;
            var emitter = new EventEmitter<int>();
            var manager = new IqlEventSubscriberManager();
            emitter.Configure(_ => { hasSubscriptionsCounter++; }, _ => { hasNoSubscriptionsCounter++; });
            Assert.AreEqual(0, hasSubscriptionsCounter);
            Assert.AreEqual(0, hasNoSubscriptionsCounter);
            manager.SubscribeOnce(emitter, _ => { });
            Assert.AreEqual(1, hasSubscriptionsCounter);
            Assert.AreEqual(0, hasNoSubscriptionsCounter);
            emitter.Emit(() => 0);
            Assert.AreEqual(1, hasSubscriptionsCounter);
            Assert.AreEqual(1, hasNoSubscriptionsCounter);
        }

        [TestMethod]
        public void SubscriptionCountTest()
        {
            var counter = 0;
            var emitter = new EventEmitter<int>();
            emitter.SubscriptionCountChanged.Subscribe(_ => { counter++; });
            Assert.AreEqual(0, emitter.SubscriptionCount);
            var manager = new IqlEventSubscriberManager();
            var sub1 = manager.SubscribeOnce(emitter, _ => {  });
            Assert.AreEqual(1, emitter.SubscriptionCount);
            var sub2 = manager.SubscribeOnce(emitter, _ => {  });
            Assert.AreEqual(2, emitter.SubscriptionCount);
            var sub3 = manager.Subscribe(emitter, _ => {  });
            var sub4 = manager.Subscribe(emitter, _ => {  }, "key1");
            var sub5 = manager.Subscribe(emitter, _ => {  }, "key2");
            var sub6 = manager.Subscribe(emitter, _ => {  });
            Assert.AreEqual(6, emitter.SubscriptionCount);
            emitter.Emit(() => 0);
            Assert.AreEqual(4, emitter.SubscriptionCount);
            emitter.Emit(() => 0);
            Assert.AreEqual(4, emitter.SubscriptionCount);
            manager.UnsubscribeAll("key1");
            Assert.AreEqual(3, emitter.SubscriptionCount);
            manager.UnsubscribeAll("key1");
            Assert.AreEqual(3, emitter.SubscriptionCount);
            sub6.Unsubscribe();
            Assert.AreEqual(2, emitter.SubscriptionCount);
            manager.UnsubscribeAll();
            Assert.AreEqual(0, emitter.SubscriptionCount);
            Assert.AreEqual(12, counter);
        }

        [TestMethod]
        public void EventSubscribeOnceTest()
        {
            var counter = 0;
            var emitter = new EventEmitter<int>();
            var manager = new IqlEventSubscriberManager();
            var sub1 = manager.SubscribeOnce(emitter, _ => { counter++; });
            var sub2 = manager.SubscribeOnce(emitter, _ => { counter++; });
            var sub3 = manager.Subscribe(emitter, _ => { counter++; });
            emitter.Emit(() => 0);
            Assert.AreEqual(3, counter);
            emitter.Emit(() => 0);
            Assert.AreEqual(4, counter);
        }

        [TestMethod]
        public async Task EventSubscribeOnceAsyncTest()
        {
            var counter = 0;
            var emitter = new AsyncEventEmitter<int>();
            var manager = new IqlEventSubscriberManager();
            var sub1 = manager.SubscribeOnceAsync(emitter, async _ => { counter++; });
            var sub2 = manager.SubscribeOnceAsync(emitter, async _ => { counter++; });
            var sub3 = manager.SubscribeAsync(emitter, async _ => { counter++; });
            await emitter.EmitAsync(() => 0);
            Assert.AreEqual(3, counter);
            await emitter.EmitAsync(() => 0);
            Assert.AreEqual(4, counter);
        }

        [TestMethod]
        public void EventMaxCallCountTest()
        {
            var counter = 0;
            var emitter = new EventEmitter<int>();
            var manager = new IqlEventSubscriberManager();
            var sub1 = manager.Subscribe(emitter, _ => { counter++; }, null, 1);
            var sub2 = manager.Subscribe(emitter, _ => { counter++; }, null, 1);
            var sub3 = manager.Subscribe(emitter, _ => { counter++; });
            emitter.Emit(() => 0);
            Assert.AreEqual(3, counter);
            emitter.Emit(() => 0);
            Assert.AreEqual(4, counter);
        }

        [TestMethod]
        public async Task EventMaxCallCountAsyncTest()
        {
            var counter = 0;
            var emitter = new AsyncEventEmitter<int>();
            var manager = new IqlEventSubscriberManager();
            var sub1 = manager.SubscribeAsync(emitter, async _ => { counter++; }, null, 1);
            var sub2 = manager.SubscribeAsync(emitter, async _ => { counter++; }, null, 1);
            var sub3 = manager.SubscribeAsync(emitter, async _ => { counter++; });
            await emitter.EmitAsync(() => 0);
            Assert.AreEqual(3, counter);
            await emitter.EmitAsync(() => 0);
            Assert.AreEqual(4, counter);
        }

        [TestMethod]
        public void EventManagerUnsubscribingNonExistantKeyTest()
        {
            var counter = 0;
            var emitter = new EventEmitter<int>();
            var manager = new IqlEventSubscriberManager();
            var sub1 = manager.Subscribe(emitter, _ => { counter++; }, "a");
            var sub2 = manager.Subscribe(emitter, _ => { counter++; }, "a");
            var sub3 = manager.Subscribe(emitter, _ => { counter++; }, "b");
            emitter.Emit(() => 0);
            Assert.AreEqual(3, counter);
            manager.UnsubscribeAll("c");
            emitter.Emit(() => 0);
            Assert.AreEqual(6, counter);
        }

        [TestMethod]
        public void EventManagerKeyTest()
        {
            var counter = 0;
            var emitter = new EventEmitter<int>();
            var manager = new IqlEventSubscriberManager();
            var sub1 = manager.Subscribe(emitter, _ => { counter++; }, "a");
            var sub2 = manager.Subscribe(emitter, _ => { counter++; }, "a");
            var sub3 = manager.Subscribe(emitter, _ => { counter++; }, "b");
            emitter.Emit(() => 0);
            Assert.AreEqual(3, counter);
            manager.UnsubscribeAll("a");
            emitter.Emit(() => 0);
            Assert.AreEqual(4, counter);
        }

        [TestMethod]
        public void PauseEventManagerTest()
        {
            var counter = 0;
            var emitter = new EventEmitter<int>();
            var manager = new IqlEventSubscriberManager();
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
        public void PauseKeyEventManagerTest()
        {
            var counter = 0;
            var emitter = new EventEmitter<int>();
            var manager = new IqlEventSubscriberManager();
            var sub1 = manager.Subscribe(emitter, _ => { counter++; }, "a");
            var sub2 = manager.Subscribe(emitter, _ => { counter++; }, "a");
            var sub3 = manager.Subscribe(emitter, _ => { counter++; }, "b");
            emitter.Emit(() => 0);
            Assert.AreEqual(3, counter);
            manager.Pause("a");
            emitter.Emit(() => 0);
            Assert.AreEqual(4, counter);
        }

        [TestMethod]
        public async Task PauseEventManagerAsyncTest()
        {
            var counter = 0;
            var emitter = new AsyncEventEmitter<int>();
            var manager = new IqlEventSubscriberManager();
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
        public async Task PauseWhileEventAsyncTest()
        {
            var counter = 0;
            var emitter = new AsyncEventEmitter<int>();
            var manager = new IqlEventSubscriberManager();
            var sub1 = manager.SubscribeAsync(emitter, async _ => { counter++; }, "a");
            var sub2 = manager.SubscribeAsync(emitter, async _ => { counter++; }, "a");
            var sub3 = manager.SubscribeAsync(emitter, async _ => { counter++; }, "b");
            await emitter.EmitAsync(() => 0);
            Assert.AreEqual(3, counter);
            var pauseWhileCount = 0;
            await manager.PauseWhileAsync(async () =>
            {
                pauseWhileCount++;
                await emitter.EmitAsync(() => 0);
            });
            Assert.AreEqual(1, pauseWhileCount);
            Assert.AreEqual(3, counter);
            await sub2.PauseWhileAsync(async () =>
            {
                pauseWhileCount++;
                await emitter.EmitAsync(() => 0);
            });
            Assert.AreEqual(2, pauseWhileCount);
            Assert.AreEqual(5, counter);
            await emitter.EmitAsync(() => 0);
            Assert.AreEqual(2, pauseWhileCount);
            Assert.AreEqual(8, counter);
            await manager.PauseWhileAsync(async () =>
            {
                pauseWhileCount++;
                await emitter.EmitAsync(() => 0);
            }, "a");
            Assert.AreEqual(3, pauseWhileCount);
            Assert.AreEqual(9, counter);
            await emitter.EmitAsync(() => 0);
            Assert.AreEqual(3, pauseWhileCount);
            Assert.AreEqual(12, counter);
        }

        [TestMethod]
        public void PauseWhileEventTest()
        {
            var counter = 0;
            var emitter = new EventEmitter<int>();
            var manager = new IqlEventSubscriberManager();
            var sub1 = manager.Subscribe(emitter, _ => { counter++; }, "a");
            var sub2 = manager.Subscribe(emitter, _ => { counter++; }, "a");
            var sub3 = manager.Subscribe(emitter, _ => { counter++; }, "b");
            emitter.Emit(() => 0);
            Assert.AreEqual(3, counter);
            var pauseWhileCount = 0;
            manager.PauseWhile(() =>
            {
                pauseWhileCount++;
                emitter.Emit(() => 0);
            });
            Assert.AreEqual(1, pauseWhileCount);
            Assert.AreEqual(3, counter);
            sub2.PauseWhile(() =>
            {
                pauseWhileCount++;
                emitter.Emit(() => 0);
            });
            Assert.AreEqual(2, pauseWhileCount);
            Assert.AreEqual(5, counter);
            emitter.Emit(() => 0);
            Assert.AreEqual(2, pauseWhileCount);
            Assert.AreEqual(8, counter);
            manager.PauseWhile(() =>
            {
                pauseWhileCount++;
                emitter.Emit(() => 0);
            }, "a");
            Assert.AreEqual(3, pauseWhileCount);
            Assert.AreEqual(9, counter);
            emitter.Emit(() => 0);
            Assert.AreEqual(3, pauseWhileCount);
            Assert.AreEqual(12, counter);
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
            emitter.OnSubscribe.Subscribe(_ => { emitter.Emit(() => 999, null, new[] { _, otherSub }); });
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