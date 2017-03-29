using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using IsThisGeekAliveMonitor.MvvmLightViewService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public string CurrentTimeZone
        {
            get
            {
                return string.Format("{0}", TimeZoneInfo.Local.DisplayName);
            }
        }

        public string GitHubPageUrl { get { return "https://github.com/notsonormal/IsThisGeekAlive"; } }

        public string IsThisGeekAliveApiUrl
        {
            get
            {
                return IsThisGeekAliveMonitor.Properties.Settings.Default.IsThisGeekAliveApiUrl;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("API Url is required");

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
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Geek username is required");

                if (value.Length < 5)
                    throw new ArgumentException("Geek username must be at least 5 characters");

                IsThisGeekAliveMonitor.Properties.Settings.Default.GeekUsername = value;
            }
        }

        public string GeekLoginCode
        {
            get
            {
                return IsThisGeekAliveMonitor.Properties.Settings.Default.GeekLoginCode;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Login code is required");

                if (value.Length < 5)
                    throw new ArgumentException("Login code must be at least 5 characters");

                IsThisGeekAliveMonitor.Properties.Settings.Default.GeekLoginCode = value;
            }
        }

        public int LoginInterval
        {
            get
            {
                return IsThisGeekAliveMonitor.Properties.Settings.Default.LoginInterval;
            }
            set
            {
                if (value <= 1 || value > 60)
                    throw new ArgumentException("Ping interval must be between 1 and 60 minutes");

                IsThisGeekAliveMonitor.Properties.Settings.Default.LoginInterval = value;
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
                if (value <= 12 || value > 60)
                    throw new ArgumentException("The not alive warning window must be at least 12 hours");

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
                if (value <= 12 || value > 60)
                    throw new ArgumentException("The not alive danger window must be at least 12 hours");

                if (value < NotAliveWarningWindow)
                    throw new ArgumentException("The not alive danger window must be equal to or greater than the warning window");

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

        public ICommand OpenGitHubPageCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Process.Start(new ProcessStartInfo(GitHubPageUrl));
                });
            }
        }
    }
}
