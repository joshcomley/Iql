using System;
using System.Collections.Generic;

namespace Iql.Entities.Services
{
    public class IqlServiceProvider
    {
        public IqlServiceProvider BaseProvider { get; set; }

        public IqlServiceProvider(IqlServiceProvider baseProvider = null)
        {
            BaseProvider = baseProvider;
        }
        private bool _serviceRegistrationsDelayedInitialized;
        private Dictionary<Type, IqlServiceRegistration> _serviceRegistrationsDelayed;

        private Dictionary<Type, IqlServiceRegistration> _serviceRegistrations { get { if(!_serviceRegistrationsDelayedInitialized) { _serviceRegistrationsDelayedInitialized = true; _serviceRegistrationsDelayed = new Dictionary<Type, IqlServiceRegistration>(); } return _serviceRegistrationsDelayed; } set { _serviceRegistrationsDelayedInitialized = true; _serviceRegistrationsDelayed = value; } }

        public IqlServiceProvider Clear(bool onlySelf = false)
        {
            _serviceRegistrations = new Dictionary<Type, IqlServiceRegistration>();
            if (!onlySelf)
            {
                BaseProvider?.Clear();
            }
            return this;
        }

        public IqlServiceProvider Unregister<T>()
            where T : class
        {
            while (true)
            {
                var registration = ResolveByType(typeof(T));
                if (registration != null)
                {
                    registration.Remove();
                }
                else
                {
                    break;
                }
            }
            return this;
        }

        public IqlServiceProvider RegisterInstance<T>(T instance)
            where T : class
        {
            EnsureRegistration<T>().Instance = instance;
            return this;
        }

        public IqlServiceProvider RegisterFactory<T>(Func<T> factory)
            where T : class
        {
            EnsureRegistration<T>().Factory = () => factory();
            return this;
        }

        public IqlServiceProvider Register<T>()
            where T : class
        {
            EnsureRegistration<T>().Factory = () => Activator.CreateInstance(typeof(T));
            return this;
        }

        public T Resolve<T>()
            where T : class
        {
            var registration = ResolveByType(typeof(T));
            if (registration != null)
            {
                return (T) registration.ResolveInstance();
            }
            return null;
        }

        private IqlServiceRegistration ResolveByType(Type type)
        {
            var currentType = type;
            IqlServiceRegistration resolved = null;
            while (currentType != null && currentType != typeof(object) && resolved == null)
            {
                if (_serviceRegistrations.ContainsKey(currentType))
                {
                    resolved = _serviceRegistrations[currentType];
                }
                if (resolved == null && BaseProvider != null)
                {
                    resolved = BaseProvider.ResolveByType(currentType);
                }
                currentType = currentType.BaseType;
            }

            if (resolved == null)
            {
                foreach (var registration in _serviceRegistrations)
                {
                    currentType = registration.Value.Type;
                    while (currentType != null && currentType != typeof(object))
                    {
                        if (currentType == type)
                        {
                            resolved = registration.Value;
                            break;
                        }
                        currentType = currentType.BaseType;
                    }

                    if (resolved != null)
                    {
                        break;
                    }
                }
            }
            return resolved;
        }

        private IqlServiceRegistration EnsureRegistration<T>()
            where T : class
        {
            return EnsureRegistrationByType(typeof(T));
        }

        private IqlServiceRegistration EnsureRegistrationByType(Type type)
        {
            if (!_serviceRegistrations.ContainsKey(type))
            {
                _serviceRegistrations.Add(type, new IqlServiceRegistration(type, _serviceRegistrations));
            }

            return _serviceRegistrations[type];
        }
    }
}
