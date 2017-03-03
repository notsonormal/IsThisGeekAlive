using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using IsThisGeekAliveMonitor.MvvmLightViewService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IsThisGeekAliveMonitor.ViewModels
{
    public class ConfigurationViewModel : ViewModelBase
    {
        public ConfigurationViewModel()
        {

        }

        public string IsThisGeekAliveApiUrl
        {
            get
            {
                return IsThisGeekAliveMonitor.Properties.Settings.Default.IsThisGeekAliveApiUrl;
            }
            set
            {
                IsThisGeekAliveMonitor.Properties.Settings.Default.IsThisGeekAliveApiUrl = value;
            }
        }

        public string GeekUsername
        {
            get
            {
                return IsThisGeekAliveMonitor.Properties.Settings.Default.GeekUsername;
            }
            set
            {
                IsThisGeekAliveMonitor.Properties.Settings.Default.GeekUsername = value;
            }
        }

        public int PingInterval
        {
            get
            {
                return IsThisGeekAliveMonitor.Properties.Settings.Default.PingInterval;
            }
            set
            {
                IsThisGeekAliveMonitor.Properties.Settings.Default.PingInterval = value;
            }
        }

        public int NotAliveWarningWindow
        {
            get
            {
                return IsThisGeekAliveMonitor.Properties.Settings.Default.NotAliveWarningWindow;
            }
            set
            {
                IsThisGeekAliveMonitor.Properties.Settings.Default.NotAliveWarningWindow = value;
            }
        }

        public int NotAliveDangerWindow
        {
            get
            {
                return IsThisGeekAliveMonitor.Properties.Settings.Default.NotAliveDangerWindow;
            }
            set
            {
                IsThisGeekAliveMonitor.Properties.Settings.Default.NotAliveDangerWindow = value;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    IsThisGeekAliveMonitor.Properties.Settings.Default.Save();

                    Messenger.Default.Send<CloseViewMessage>(new CloseViewMessage(this, true), this);
                });
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Messenger.Default.Send<CloseViewMessage>(new CloseViewMessage(this, false), this);
                });
            }
        }
    }
}
