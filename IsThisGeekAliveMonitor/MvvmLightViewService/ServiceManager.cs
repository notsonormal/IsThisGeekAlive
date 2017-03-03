using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsThisGeekAliveMonitor.MvvmLightViewService
{
    public class ServiceManager
    {
        static readonly ServiceManager _instance = new ServiceManager();
        static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        ServiceManager() { } // Private constructor

        public static T RegisterService<T>(object service) where T : class
        {
            return _instance.Register<T>(service);
        }

        public static T GetService<T>() where T : class
        {
            return _instance.Get<T>();
        }

        public static void RemoveService<T>() where T : class
        {
            _instance.Remove<T>();
        }

        public static void ClearServices()
        {
            _instance.Clear();
        }

        public static void OpenWindow(ViewModelBase viewModel)
        {
            _instance.Get<IViewService>().OpenWindow(viewModel);
        }

        public static bool? OpenDialog(ViewModelBase viewModel)
        {
            return _instance.Get<IViewService>().OpenDialog(viewModel);
        }

        T Register<T>(object service) where T : class
        {
            if (service == null)
                throw new ArgumentNullException("service");

            lock (_services)
            {
                if (_services.ContainsKey(typeof(T)))
                    throw new ArgumentException("Service already registered");
                _services[typeof(T)] = service;
            }
            return (T)service;
        }

        T Get<T>() where T : class
        {
            lock (_services)
            {
                if (_services.ContainsKey(typeof(T)))
                    return (T)_services[typeof(T)];
                else
                    throw new ArgumentException("Service not registered: " + typeof(T));
            }
        }

        void Remove<T>() where T : class
        {
            lock (_services)
            {
                if (_services.ContainsKey(typeof(T)))
                    _services.Remove(typeof(T));
            }
        }

        void Clear()
        {
            lock (_services)
            {
                _services.Clear();
            }
        }
    }
}
