using System;

namespace Iql.Entities.Sanitization
{
    public class EntitySanitizer<T> : IEntitySanitizer
    {
        private Action<T> _run;
        public string Key { get; set; }

        public Action<T> Run
        {
            get => _run;
            set
            {
                _run = value;
                UntypedRun = _ => value((T)_);
            }
        }

        Action<object> IEntitySanitizer.Run
        {
            get => UntypedRun;
            set
            {
                _run = _ => value(_);
                UntypedRun = value;
            }
        }

        private Action<object> UntypedRun { get; set; }
    }
}