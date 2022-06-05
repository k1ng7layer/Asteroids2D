using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyECS2
{
    internal class ObjectContainer
    {
        public ObjectContainer()
        {
            _IoC_Container = new Dictionary<Type, object>();
        }

        private Dictionary<Type, object> _IoC_Container;

        internal void AddDependency<T>(T instance)
        {
            var key = typeof(T);
            if (_IoC_Container.TryGetValue(key, out object result))
            {
                throw new InvalidOperationException($"object of type {key} is already added to container");
            }
            _IoC_Container.Add(key, instance);
        }

        internal object ResolveDependency(Type type)
        {
            if (_IoC_Container.TryGetValue(type, out object instance))
            {
                return Convert.ChangeType(instance, type);
            }
            return default;
        }

        internal bool Contains(Type type)
        {
            if (_IoC_Container.ContainsKey(type))
                return true;

            else return false;
        }
    }
}
