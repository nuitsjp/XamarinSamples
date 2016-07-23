using System;
using System.Collections.Generic;

namespace XFStopwatch
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> Store = new Dictionary<Type, object>();
        public static void Register<T>(T value)
        {
            Store[typeof(T)] = value;
        }

        public static T Locate<T>()
        {
            object result;
            if (Store.TryGetValue(typeof(T), out result))
            {
                return (T)result;
            }
            else
            {
                throw new ArgumentException($"{typeof(T).FullName} is not registration.");
            }
        }

        public static void Clear()
        {
            Store.Clear();
        }
    }
}
