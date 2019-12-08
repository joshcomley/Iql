using System;
using System.Threading.Tasks;

namespace Iql.Events
{
    public abstract class EventEmitterRoot : IEventEmitterRoot
    {
        private bool _isPaused;

        public bool IsPaused => _isPaused;

        public string Key { get; set; }
        public void Pause()
        {
            _isPaused = true;
        }

        public abstract void ResumeInternal();
        public abstract Task ResumeInternalAsync();

        public void Resume()
        {
            _isPaused = false;
            ResumeInternal();
        }

        public Task ResumeAsync()
        {
            _isPaused = false;
            return ResumeInternalAsync();
        }

        public abstract void Dispose();
    }
}