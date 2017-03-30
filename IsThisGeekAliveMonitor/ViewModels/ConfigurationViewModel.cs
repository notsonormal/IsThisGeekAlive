using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using IsThisGeekAliveMonitor.MvvmLightViewService;
using IsThisGeekAliveMonitor.Utils;
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
        MonitorSettings _settings;

        public ConfigurationViewModel()
        {
            _settings = MonitorSettings.Load();
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
                return _settings.IsThisGeekAliveApiUrl;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("API Url is required");

                _settings.IsThisGeekAliveApiUrl = value;
            }
        }

        public string GeekUsername
        {
            get
            {
                return _settings.GeekUsername;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Geek username is required");

                if (value.Length < 5)
                    throw new ArgumentException("Geek username must be at least 5 characters");

                _settings.GeekUsername = value;
            }
        }

        public string GeekLoginCode
        {
            get
            {
                return _settings.GeekLoginCode;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Login code is required");

                if (value.Length < 5)
                    throw new ArgumentException("Login code must be at least 5 characters");

                _settings.GeekLoginCode = value;
            }
        }

        public int LoginInterval
        {
            get
            {
                return _settings.LoginInterval;
            }
            set
            {
                if (value <= 1 || value > 60)
                    throw new ArgumentException("Ping interval must be between 1 and 60 minutes");

                _settings.LoginInterval = value;
            }
        }

        public int NotAliveWarningWindow
        {
            get
            {
                return _settings.NotAliveWarningWindow;
            }
            set
            {
                if (value <= 12 || value > 60)
                    throw new ArgumentException("The not alive warning window must be at least 12 hours");

                _settings.NotAliveWarningWindow = value;
            }
        }        

        public int NotAliveDangerWindow
        {
            get
            {
                return _settings.NotAliveDangerWindow;
            }
            set
            {
                if (value <= 12 || value > 60)
                    throw new ArgumentException("The not alive danger window must be at least 12 hours");

                if (value < NotAliveWarningWindow)
                    throw new ArgumentException("The not alive danger window must be equal to or greater than the warning window");

                _settings.NotAliveDangerWindow = value;
            }
        }        

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    _settings.Save();                            

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
