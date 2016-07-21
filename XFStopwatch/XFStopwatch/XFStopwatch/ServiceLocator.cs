using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFStopwatch
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> Resolvers = new Dictionary<Type, object>();

        public static T Resolve<T>()
        {
            return (T)Resolvers[typeof(T)];
        }

        public static void Register<T>(T value)
        {
            Resolvers[typeof(T)] = value;
        }
    }
}
