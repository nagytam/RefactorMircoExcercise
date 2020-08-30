
using System;

namespace TDDMicroExercises.TelemetrySystem
{
    /// <summary>The telemetry diagnostics control</summary>
    public class TelemetryDiagnosticControls
    {
        #region Private members
        private const string DiagnosticChannelConnectionString = "*111#";
        private const int RetryCount = 3;
        private readonly ITelemetryClient _telemetryClient;
        #endregion

        #region Public properties
        public string DiagnosticInfo { get; set; } = string.Empty;
        #endregion

        #region Constructors
        /// <summary>Initializes a new instance of the <a onclick="return false;" href="TelemetryDiagnosticControls" originaltag="see">TelemetryDiagnosticControls</a> class.</summary>
        public TelemetryDiagnosticControls()
        {
            _telemetryClient = new TelemetryClient();
        }

        public TelemetryDiagnosticControls(ITelemetryClient telemetryClient)
        {
            if (telemetryClient == null)
            {
                throw new ArgumentNullException(nameof(telemetryClient));
            }
            
            _telemetryClient = telemetryClient;
        }
        #endregion

        /// <summary>Checks the transmission.</summary>
        /// <exception cref="Exception">Unable to connect.</exception>
        public void CheckTransmission()
        {
            DiagnosticInfo = string.Empty;

            Reconnect();

            _telemetryClient.Send(TelemetryClient.DiagnosticMessage);
            DiagnosticInfo = _telemetryClient.Receive();
        }

        /// <summary>Reconnects the connection.</summary>
        /// <exception cref="Exception">Unable to connect.</exception>
        private void Reconnect()
        {
            _telemetryClient.Disconnect();

            int retryLeft = RetryCount;
            while (_telemetryClient.OnlineStatus == false && retryLeft > 0)
            {
                _telemetryClient.Connect(DiagnosticChannelConnectionString);
                retryLeft--;
            }

            if (_telemetryClient.OnlineStatus == false)
            {
                throw new Exception("Unable to connect.");
            }
        }
    }
}
