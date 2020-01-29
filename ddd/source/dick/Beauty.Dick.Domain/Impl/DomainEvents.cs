using Beauty.Dick.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beauty.Dick.Domain.Impl
{
    /// <summary>
    /// http://www.udidahan.com/2009/06/14/domain-events-salvation/
    /// https://www.codeproject.com/Articles/1176046/Domain-Events-with-Convention-Based-Registration-a
    /// </summary>
    public static class DomainEvents
    {
        [ThreadStatic] //so that each thread has its own callbacks
        private static List<Delegate> _actions;

        private static IServiceProvider _container;

        public static void Init(IServiceProvider container)
        {
            _container = container;
        }

        //Registers a callback for the given domain event, used for testing only
        public static void Register<T>(Action<T> callback) where T : DomainEvent
        {
            if (_actions == null)
                _actions = new List<Delegate>();

            _actions.Add(callback);
        }

        //Clears callbacks passed to Register on the current thread
        public static void ClearCallbacks()
        {
            _actions = null;
        }

        //Raises the given domain event
        public static void Raise<T>(T args) where T : DomainEvent
        {
            var concrets = new List<IHandles<T>>(); //_container.GetServices<IHandles<T>>();

            if (_container != null)
                foreach (var handler in concrets)
                    handler.Handle(args);

            if (_actions != null)
                foreach (var action in _actions)
                    if (action is Action<T>)
                        ((Action<T>)action)(args);
        }
    }
}
