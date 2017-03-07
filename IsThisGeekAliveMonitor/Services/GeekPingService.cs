using GalaSoft.MvvmLight.Messaging;
using IsThisGeekAliveMonitor.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Timers;

namespace IsThisGeekAliveMonitor.Services
{
    public class GeekPingService
    {
        Timer _pingTimer;
        BackgroundWorker _backgroundWorker;

        public GeekPingService()
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.DoWork += DoWork;

            int pingInterval = Properties.Settings.Default.LoginInterval;

            _pingTimer = new Timer();
            _pingTimer.Interval = TimeSpan.FromMinutes(pingInterval).TotalMilliseconds;
            _pingTimer.Elapsed += OnPingTimerElapsed;
            _pingTimer.AutoReset = false;     
        }

        public void Start()
        {            
            _pingTimer.Start();

            // Immediately send a ping request, rather than waiting for the timer
            _backgroundWorker.RunWorkerAsync();
        }

        public void Stop()
        {            
            _pingTimer.Stop();
            _backgroundWorker.CancelAsync();
        }

        void OnPingTimerElapsed(object sender, EventArgs e)
        {
            if (!_backgroundWorker.IsBusy)
            {
                _backgroundWorker.RunWorkerAsync();
            }

            int pingInterval = Properties.Settings.Default.LoginInterval;
            _pingTimer.Interval = TimeSpan.FromMinutes(pingInterval).TotalMilliseconds;
            _pingTimer.Start();
        }

        void DoWork(object sender, DoWorkEventArgs e)
        {
            if (_backgroundWorker.CancellationPending)
                return;

            string apiUrl = Properties.Settings.Default.IsThisGeekAliveApiUrl;
            string geekUsername = Properties.Settings.Default.GeekUsername;
            int notAliveWarningWindow = Properties.Settings.Default.NotAliveWarningWindow;
            int notAliveDangerWindow = Properties.Settings.Default.NotAliveDangerWindow;

            if (string.IsNullOrWhiteSpace(apiUrl))
            {
                Messenger.Default.Send(new PingFailedMessage("API URL has not been set"));
                return;
            }

            if (string.IsNullOrWhiteSpace(geekUsername))
            {
                Messenger.Default.Send(new PingFailedMessage("The geek's username has not been set"));
                return;
            }

            RestClient client = new RestClient(apiUrl);
            RestRequest request = CreatePingRequest(geekUsername, notAliveWarningWindow, notAliveDangerWindow);

            try
            {
                var response = client.Post(request);
                CheckForError(response);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(string.Format("Ping failed exception: {0}", ex.ToString()));
                Messenger.Default.Send(new PingFailedMessage(ex.Message));
            }
        }

        RestRequest CreatePingRequest(string geekUsername, int notAliveWarningWindow, int notAliveDangerWindow)
        {
            RestRequest request = new RestRequest("geeks/login")
            {
                DateFormat = DateFormat.ISO_8601,
                RequestFormat = DataFormat.Json,
            };

            request.AddBody(new GeekLogin()
            {
                Username = geekUsername,
                LocalTime = DateTimeOffset.Now,
                NotAliveWarningWindow = notAliveWarningWindow,
                NotAliveDangerWindow = notAliveDangerWindow
            });

            return request;
        }

        void CheckForError(IRestResponse response)
        {
            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new Exception("No API endpoint found at that url");
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.StatusDescription);
            }
        }

    }
}
