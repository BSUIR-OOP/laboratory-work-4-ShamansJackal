using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiLib
{
    public partial class DIContainer
    {
        public void AddSingelton<TService>() where TService : class
        {
            AddToTree(typeof(TService));

            _services[typeof(TService)] = GetSingleton;
            _ctors[typeof(TService)] = typeof(TService).GetConstructors()[0];
        }

        public void AddSingelton<TService, TImplementaion>() where TService : class where TImplementaion : class, TService
        {
            AddToTree(typeof(TService));

            _services[typeof(TImplementaion)] = GetSingleton;
            _ctors[typeof(TImplementaion)] = typeof(TService).GetConstructors()[0];
        }

        public void AddTransition<TService>() where TService : class
        {
            AddToTree(typeof(TService));

            _services[typeof(TService)] = GetTransition;
            _ctors[typeof(TService)] = typeof(TService).GetConstructors()[0];
        }

        public void AddTransition<TService, TImplementaion>() where TService : class where TImplementaion : class, TService
        {
            AddToTree(typeof(TService));

            _services[typeof(TImplementaion)] = GetTransition;
            _ctors[typeof(TImplementaion)] = typeof(TService).GetConstructors()[0];
        }

        public void AddTransition<TService>(Func<TService> func) where TService: class
        {
            dependencies[typeof(TService)] = new();

            _services[typeof(TService)] = GetTransition;
            _ctors[typeof(TService)] = func;
        }

        public void AddSingleton<TService>(Func<TService> func) where TService : class
        {
            dependencies[typeof(TService)] = new();

            _services[typeof(TService)] = GetSingleton;
            _ctors[typeof(TService)] = func;
        }
    }
}
