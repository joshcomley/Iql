using Iql.Events;

namespace Iql.Data.Context
{
    public static class StateEvents
    {
        private static EventEmitter<AbandonChangeEvent> _abandonedPropertyChange;
        private static EventEmitter<AbandonChangeEvent> _abandoningPropertyChange;
        private static EventEmitter<AbandonChangeEvent> _abandonedEntityChanges;
        private static EventEmitter<AbandonChangeEvent> _abandoningEntityChanges;

        public static EventEmitter<AbandonChangeEvent> AbandonedPropertyChange =>
            _abandonedPropertyChange = _abandonedPropertyChange ?? new EventEmitter<AbandonChangeEvent>();
        public static EventEmitter<AbandonChangeEvent> AbandoningPropertyChange =>
            _abandoningPropertyChange = _abandoningPropertyChange ?? new EventEmitter<AbandonChangeEvent>();
        public static EventEmitter<AbandonChangeEvent> AbandonedEntityChanges =>
            _abandonedEntityChanges = _abandonedEntityChanges ?? new EventEmitter<AbandonChangeEvent>();
        public static EventEmitter<AbandonChangeEvent> AbandoningEntityChanges =>
            _abandoningEntityChanges = _abandoningEntityChanges ?? new EventEmitter<AbandonChangeEvent>();
    }
}