using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using Hardcodet.Wpf.TaskbarNotification;
using IsThisGeekAliveMonitor.MvvmLightViewService;
using IsThisGeekAliveMonitor.Services;
using IsThisGeekAliveMonitor.Utils;
using IsThisGeekAliveMonitor.ViewModels;
using IsThisGeekAliveMonitor.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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

            SetupMvvmLightViewService();
            SetupNotifyIcon();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _notifyIcon.Dispose();
            base.OnExit(e);
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

            Messenger.Default.Register<PingFailedMessage>(this, (m) =>
            {
                if (_notifyIcon.IsDisposed)
                    return;

                _notifyIcon.ShowBalloonTip(ProjectUtils.GetProjectName(), m.ErrorMessage, BalloonIcon.Error);
            });
        }
    }
}
