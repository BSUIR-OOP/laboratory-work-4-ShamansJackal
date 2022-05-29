using DiLib.Exptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DiLib
{
    delegate object GetServiceDelegate(Type type);

    delegate T AddServiceStatic<T>(); 

    public partial class DIContainer
    {
        private readonly Dictionary<Type, HashSet<Type>> dependencies = new();

        private readonly Dictionary<Type, GetServiceDelegate> _services = new();

        private readonly Dictionary<Type, object> _cache = new();
        private readonly Dictionary<Type, object> _ctors = new();

        private void AddToTree(Type dependency)
        {
            var ctors = dependency.GetConstructors();

            var parameters = ctors[0].GetParameters();
            foreach (var parm in parameters)
            {
                if(!dependencies.ContainsKey(parm.ParameterType)) throw new Exception("No such type");
                if (HasCycles(parm.ParameterType, new HashSet<Type>() { dependency })) throw new CycleExeption("Has cycles");                
            }
            dependencies[dependency] = new HashSet<Type>(parameters.Select(x => x.ParameterType));
        }

        private bool HasCycles(Type type, HashSet<Type> NeedType)
        {
            if (NeedType.Contains(type)) return true;
            NeedType.Add(type);
            foreach (var item in dependencies[type])
                if (HasCycles(item, new HashSet<Type>(NeedType))) return true;
            return false;
        }

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
            if (ctor is ConstructorInfo info) {
                var param = info.GetParameters();
                List<object> list = new();
                foreach (var par in param)
                {
                    list.Add(GetService(par.ParameterType));
                }

                return info.Invoke(list.ToArray()); 
            }
            else
            {
                return ((Func<object>)ctor)();
            }
        }
    }
}
