using System;
using System.Collections.Generic;
using System.Reflection;

namespace DiLib
{
    delegate object GetServiceDelegate(Type type);

    public class DIContainer
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

        public void AddSingelton<TService>() where TService: class
        {
            if (typeof(TService).GetConstructors().Length > 1) throw new Exception("To many ctor's");

            _services[typeof(TService)] = GetSingleton;
            _ctors[typeof(TService)] = typeof(TService).GetConstructors()[0];
        }

        public void AddSingelton<TService, TImplementaion>() where TService: class where TImplementaion: class, TService
        {
            if (typeof(TService).GetConstructors().Length > 1) throw new Exception("To many ctor's");

            _services[typeof(TImplementaion)] = GetSingleton;
            _ctors[typeof(TImplementaion)] = typeof(TService).GetConstructors()[0];
        }

        public void AddTransition<TService>() where TService : class
        {
            if (typeof(TService).GetConstructors().Length > 1) throw new Exception("To many ctor's");

            _services[typeof(TService)] = GetTransition;
            _ctors[typeof(TService)] = typeof(TService).GetConstructors()[0];
        }

        public void AddTransition<TService, TImplementaion>() where TService : class where TImplementaion : class, TService
        {
            if (typeof(TService).GetConstructors().Length > 1) throw new Exception("To many ctor's");

            _services[typeof(TImplementaion)] = GetTransition;
            _ctors[typeof(TImplementaion)] = typeof(TService).GetConstructors()[0];
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
