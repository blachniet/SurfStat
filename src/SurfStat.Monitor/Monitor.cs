using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SurfStat.Monitor
{
    class Monitor
    {
        public bool OnlineLastCheck { get; set; }

        public Monitor()
        {
            _surfStat = new SurfStat();
            _timer = new Timer();
            _timer.Elapsed += Timer_Elapsed;
        }

        public void Start()
        {
            _surfStat.BaseAddress = ConfigurationManager.AppSettings["ModemUri"];
            _surfStat.ModemStatusUri = new Uri(ConfigurationManager.AppSettings["ModemStatusUri"]);
            _surfStat.TRIAStatusUri = new Uri(ConfigurationManager.AppSettings["TriaStatusUri"]);

            double interval;
            if (double.TryParse(ConfigurationManager.AppSettings["CheckInterval"], out interval))
            {
                _timer.Stop();
                _timer.Interval = interval;
                _timer.Start();
            }
            else
            {
                var ex = new ConfigurationErrorsException("Invalid configuration for CheckInterval setting. It must be a decimal number.");
                _log.Error(ex);
                throw ex;
            }
        }

        public void Stop()
        {
            _timer.Stop();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            CheckStatus();
        }

        private void HandleReconnected()
        {
            _log.Info("Modem satellite connection restored.");
        }

        private void HandleDisconnected()
        {
            _log.Info("Modem satellite connection lost.");
        }

        private async void CheckStatus()
        {
            var status = await _surfStat.GetModemStatusAsync();
            if (status != null)
            {
                var onlineNow = status.Status.Equals("online", StringComparison.OrdinalIgnoreCase);
                if (OnlineLastCheck != onlineNow)
                {
                    if (onlineNow)
                        HandleReconnected();
                    else
                        HandleDisconnected();
                }
            }
        }

        #region Fields

        private static readonly ILog _log = LogManager.GetLogger(typeof(Monitor));
        private readonly Timer _timer;
        private readonly SurfStat _surfStat;

        #endregion
    }
}
