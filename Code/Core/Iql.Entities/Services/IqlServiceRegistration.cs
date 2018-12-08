using System;
using System.Collections.Generic;

namespace Iql.Entities.Services
{
    internal class IqlServiceRegistration
    {
        private object _instance;
        private Func<object> _factory;
        public Type Type { get; }
        private Dictionary<Type, IqlServiceRegistration> Owner { get; }

        public IqlServiceRegistration(Type type, Dictionary<Type, IqlServiceRegistration> owner)
        {
            Type = type;
            Owner = owner;
        }

        public void Remove()
        {
            if (Owner.ContainsKey(Type))
            {
                var reg = Owner[Type];
                if (reg == this)
                {
                    Owner.Remove(Type);
                }
            }
        }

        public object Instance
        {
            get => _instance;
            set
            {
                _instance = value;
                if (value != null)
                {
                    Factory = null;
                }
            }
        }

        public Func<object> Factory
        {
            get => _factory;
            set
            {
                _factory = value;
                if (value != null)
                {
                    Instance = null;
                }
            }
        }

        public object ResolveInstance()
        {
            if (Factory != null)
            {
                return Factory();
            }

            return Instance;
        }
    }
}