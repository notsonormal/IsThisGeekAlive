using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using IsThisGeekAliveMonitor.MvvmLightViewService;
using IsThisGeekAliveMonitor.Services;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace IsThisGeekAliveMonitor.ViewModels
{
    public class NotifyIconViewModel : ViewModelBase
    {
        GeekPingService _pingService;

        public NotifyIconViewModel()
        {
            _pingService = new GeekPingService();
            _pingService.Start();
        }

        public void ShowWindow()
        {
            bool? result = ServiceManager.OpenDialog(new ConfigurationViewModel());

            if (result == true)
            {
                // Restart to immediately send a ping request with the new settings
                _pingService.Stop();
                _pingService.Start();
            }
        }

        public void CloseWindow()
        {
            _pingService.Stop();
            Application.Current.Shutdown();
        }

        public ICommand ShowWindowCommand { get { return new RelayCommand(ShowWindow); } }
        public ICommand CloseApplicationCommand { get { return new RelayCommand(CloseWindow); } }
    }
}