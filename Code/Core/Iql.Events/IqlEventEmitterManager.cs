using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Iql.Events
{
    public class IqlEventEmitterManager
    {
        private bool _emittersDelayedInitialized;
        private List<IEventEmitterRoot> _subscriptionsDelayed;
        private List<IEventEmitterRoot> _emitters { get { if (!_emittersDelayedInitialized) { _emittersDelayedInitialized = true; _subscriptionsDelayed = new List<IEventEmitterRoot>(); } return _subscriptionsDelayed; } set { _emittersDelayedInitialized = true; _subscriptionsDelayed = value; } }

        public void Pause(string key = null)
        {
            for (var i = 0; i < _emitters.Count; i++)
            {
                if (key == null || _emitters[i].Key == key)
                {
                    _emitters[i].Pause();
                }
            }
        }

        //public void PauseWhile(Action action, string key = null)
        //{
        //    Pause(key);
        //    action();
        //    ResumeAsync(key);
        //}

        public async Task PauseWhileAsync(Func<Task> action, string key = null)
        {
            Pause(key);
            await action();
            await ResumeAsync(key);
        }

        public async Task ResumeAsync(string key = null)
        {
            for (var i = 0; i < _emitters.Count; i++)
            {
                if (key == null || _emitters[i].Key == key)
                {
                    await _emitters[i].ResumeAsync();
                }
            }
        }

        public async void Resume(string key = null)
        {
            for (var i = 0; i < _emitters.Count; i++)
            {
                if (key == null || _emitters[i].Key == key)
                {
                    _emitters[i].Resume();
                }
            }
        }

        public void Register(IEventEmitterRoot emitter)
        {
            _emitters.Add(emitter);
        }
    }
}