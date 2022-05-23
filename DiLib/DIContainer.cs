using System;
using System.Collections.Generic;

namespace DiLib
{
    delegate object GetServiceDelegate();

    public class DIContainer
    {
        private Dictionary<Type, GetServiceDelegate> _services = new Dictionary<Type, DiLib.GetServiceDelegate>();


        public void AddSingelton<TService>() where TService: class
        {

        }

        public void AddSingelton<TService, TImplementaion>() where TService: class where TImplementaion: class, TService
        {

        }

        public void AddTransition<TService>() where TService : class
        {

        }

        public void AddTransition<TService, TImplementaion>() where TService : class where TImplementaion : class, TService
        {

        }
    }
}
