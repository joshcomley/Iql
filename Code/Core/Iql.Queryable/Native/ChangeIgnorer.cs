using System;
using System.Collections.Generic;

namespace Iql.Queryable.Native
{
    public class ChangeIgnorer
    {
        private readonly Dictionary<object, object> _changingEntities =
            new Dictionary<object, object>();

        public bool AreAnyIgnored(params object[] entities)
        {
            var isIgnored = false;
            foreach (var entity in entities)
            {
                if (entity != null && _changingEntities.ContainsKey(entity))
                {
                    isIgnored = true;
                    break;
                }
            }

            return isIgnored;
        }

        public void IgnoreAndRunIfNotAlreadyIgnored(Action action, params object[] entities)
        {
            if (!AreAnyIgnored(entities))
            {
                IgnoreChanges(action, entities);
            }
        }

        public void IgnoreAndRunEvenIfAlreadyIgnored(Action action, params object[] entities)
        {
            IgnoreChanges(action, entities);
        }

        public void IgnoreChanges(Action action, params object[] entities)
        {
            var ignored = new List<object>();
            foreach (var entity in entities)
            {
                if (entity != null && !AreAnyIgnored(entity))
                {
                    ignored.Add(entity);
                    _changingEntities.Add(entity, entity);
                }
            }

            action();
            foreach (var entity in ignored)
            {
                _changingEntities.Remove(entity);
            }
        }
    }
}