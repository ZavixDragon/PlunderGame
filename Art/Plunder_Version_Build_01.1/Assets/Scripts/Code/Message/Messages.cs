using System;
using System.Collections.Generic;

namespace Assets.Scripts.Code.Message
{
    public static class Messages 
    {
        private static readonly Dictionary<Type, List<object>> _listeners = new Dictionary<Type, List<object>>();

        public static void Send<T>(T message)
        {
            var type = typeof(T);
            if (_listeners.ContainsKey(type))
                _listeners[type].ForEach(x => ((Listener<T>)x).Hear(message));
        }

        public static void ListenFor<T>(Action<T> onHear, object owner)
        {
            var type = typeof(T);
            if (!_listeners.ContainsKey(type))
                _listeners[type] = new List<object>();
           _listeners[type].Add(new Listener<T>(onHear, owner));
        }

        public static void StopListening<T>(object owner)
        {
            var type = typeof(T);
            if (!_listeners.ContainsKey(type))
                _listeners[type] = new List<object>();
            _listeners[typeof(T)].RemoveAll(x => ((Listener<T>)x).Owner == owner);
        }

        private class Listener<T>
        {
            private readonly Action<T> _onHear;
            public object Owner { get; }

            public Listener(Action<T> onHear, object owner)
            {
                _onHear = onHear;
                Owner = owner;
            }

            public void Hear(T message)
            {
                _onHear(message);
            }
        }
    }
}
