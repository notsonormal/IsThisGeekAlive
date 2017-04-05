using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using IsThisGeekAliveMonitor.MvvmLightViewService;
using IsThisGeekAliveMonitor.Services;
using IsThisGeekAliveMonitor.Utils;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace IsThisGeekAliveMonitor.ViewModels
{
    public class NotifyIconViewModel : ViewModelBase
    {
        GeekPingService _pingService;
        ConfigurationViewModel _configurationWindow;        

        public NotifyIconViewModel()
        {
            _pingService = new GeekPingService();
            _pingService.Start();

            var settings = MonitorSettings.Load();

            if (string.IsNullOrWhiteSpace(settings.GeekUsername))
            {
                ShowWindow();
            }
        }

        public void ShowWindow()
        {
            if (_configurationWindow == null)
            {
                _configurationWindow = new ConfigurationViewModel();

                bool? result = ServiceManager.OpenDialog(_configurationWindow);

                if (result == true)
                {
                    // Restart to immediately send a ping request with the new settings
                    _pingService.Stop();
                    _pingService.Start();
                }

                _configurationWindow = null;
            }
            else
            {
                // The configuration window is a modal dialog so if it is already opened 
                // push it to the foreground instead of creating a new window
                ServiceManager.ActivateWindow(_configurationWindow);
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