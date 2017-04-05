using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using Hardcodet.Wpf.TaskbarNotification;
using IsThisGeekAliveMonitor.MvvmLightViewService;
using IsThisGeekAliveMonitor.Services;
using IsThisGeekAliveMonitor.Utils;
using IsThisGeekAliveMonitor.ViewModels;
using IsThisGeekAliveMonitor.Views;
using SimpleLogger.Logging.Handlers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace IsThisGeekAliveMonitor
{
    public partial class App : Application
    {        
        private TaskbarIcon _notifyIcon;

        static App()
        {
            DispatcherHelper.Initialize();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            InitializeComponent();
            base.OnStartup(e);

            SetupLogging();

            Application.Current.DispatcherUnhandledException += OnUnhandledException;

            SetupMvvmLightViewService();
            SetupNotifyIcon();                  
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _notifyIcon.Dispose();
            base.OnExit(e);
        }

        void SetupLogging()
        {
            var fileHandler = new FileLoggerHandler("IsThisGeekAliveMonitor.log.txt", ProjectUtils.GetSettingsDirectory());
            SimpleLogger.Logger.LoggerHandlerManager.AddHandler(fileHandler);
        }

        void SetupMvvmLightViewService()
        {
            IViewService viewService = ServiceManager.RegisterService<IViewService>(new ViewService());
            viewService.RegisterView(typeof(ConfigurationWindow), typeof(ConfigurationViewModel));
        }

        void SetupNotifyIcon()
        {
            _notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
            _notifyIcon.DataContext = new NotifyIconViewModel();
            _notifyIcon.TrayBalloonTipClicked += OnTrayBalloonTipClicked;

            Messenger.Default.Register<PingFailedMessage>(this, (m) =>
            {
                if (!_notifyIcon.IsDisposed)
                {
                    _notifyIcon.ShowBalloonTip(null, m.ErrorMessage.ToString(), BalloonIcon.Error);
                }
            });
        }

        void OnTrayBalloonTipClicked(object sender, RoutedEventArgs e)
        {
            ((NotifyIconViewModel)_notifyIcon.DataContext).ShowWindow();
        }

        void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Debug.WriteLine(string.Format("Unhandled exception: {0}", e.Exception.ToString()));
            SimpleLogger.Logger.Log(string.Format("Unhandled exception: {0}", e.Exception.ToString()));

            if (!_notifyIcon.IsDisposed)
            {
                e.Dispatcher.Invoke(() =>
                {
                    _notifyIcon.ShowBalloonTip(null, e.Exception.ToString(), BalloonIcon.Error);
                });
            };

            e.Handled = true;
        }
    }
}
