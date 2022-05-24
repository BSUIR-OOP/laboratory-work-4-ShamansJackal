using System;
using System.Collections.Generic;
using System.Reflection;

namespace DiLib
{
    delegate object GetServiceDelegate(Type type);

    delegate T AddServiceStatic<T>(); 

    public partial class DIContainer
    {
        private readonly Dictionary<Type, GetServiceDelegate> _services = new Dictionary<Type, DiLib.GetServiceDelegate>();

        private readonly Dictionary<Type, object> _cache = new Dictionary<Type, object>();
        private readonly Dictionary<Type, ConstructorInfo> _ctors = new Dictionary<Type, ConstructorInfo>();

        public object GetService(Type type)
        {
            if (!_services.ContainsKey(type)) throw new Exception("No such type");

            return _services[type](type);
        }

        public T GetService<T>()
        {
            if (!_services.ContainsKey(typeof(T))) throw new Exception("No such type");

            return (T)_services[typeof(T)](typeof(T));
        }

        private object GetSingleton(Type type)
        {
            if (!_cache.ContainsKey(type))
            {
                var obj = GetTransition(type);
                _cache[type] = obj;
            }
            return _cache[type];
        }

        private object GetTransition(Type type)
        {
            var ctor = _ctors[type];

            var param = ctor.GetParameters();
            List<object> list = new();
            foreach(var par in param)
            {
                list.Add(this.GetService(par.ParameterType));
            }
            return ctor.Invoke(list.ToArray());
        }
    }
}
