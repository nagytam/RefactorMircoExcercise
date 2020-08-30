
using System;

namespace TDDMicroExercises.TelemetrySystem
{
    /// <summary>The telemetry diagnostics control</summary>
    public class TelemetryDiagnosticControls
    {
        #region Private members
        private const string DiagnosticChannelConnectionString = "*111#";        
        private readonly TelemetryClient _telemetryClient;
        #endregion

        #region Public properties
        public string DiagnosticInfo { get; set; } = string.Empty;
        #endregion

        /// <summary>Initializes a new instance of the <a onclick="return false;" href="TelemetryDiagnosticControls" originaltag="see">TelemetryDiagnosticControls</a> class.</summary>
        public TelemetryDiagnosticControls()
        {
            _telemetryClient = new TelemetryClient();
        }

        /// <summary>Checks the transmission.</summary>
        /// <exception cref="Exception">Unable to connect.</exception>
        public void CheckTransmission()
        {
            DiagnosticInfo = string.Empty;

            _telemetryClient.Disconnect();

            int retryLeft = 3;
            while (_telemetryClient.OnlineStatus == false && retryLeft > 0)
            {
                _telemetryClient.Connect(DiagnosticChannelConnectionString);
                retryLeft -= 1;
            }
             
            if(_telemetryClient.OnlineStatus == false)
            {
                throw new Exception("Unable to connect.");
            }

            _telemetryClient.Send(TelemetryClient.DiagnosticMessage);
            DiagnosticInfo = _telemetryClient.Receive();
        }
    }
}
