using System;

namespace TDDMicroExercises.TelemetrySystem
{
    //
    // The communication with the server is simulated in this implementation.
    // Because the focus of the exercise is on the other class.
    //
    
    public class TelemetryClient
    {
        /// <summary>The diagnostic message</summary>
        public const string DiagnosticMessage = "AT#UD";
        /// <summary>The diagnostic response</summary>
        public const string DiagnosticResponse = "LAST TX rate................ 100 MBPS\r\n"
                            + "HIGHEST TX rate............. 100 MBPS\r\n"
                            + "LAST RX rate................ 100 MBPS\r\n"
                            + "HIGHEST RX rate............. 100 MBPS\r\n"
                            + "BIT RATE.................... 100000000\r\n"
                            + "WORD LEN.................... 16\r\n"
                            + "WORD/FRAME.................. 511\r\n"
                            + "BITS/FRAME.................. 8192\r\n"
                            + "MODULATION TYPE............. PCM/FM\r\n"
                            + "TX Digital Los.............. 0.75\r\n"
                            + "RX Digital Los.............. 0.10\r\n"
                            + "BEP Test.................... -5\r\n"
                            + "Local Rtrn Count............ 00\r\n"
                            + "Remote Rtrn Count........... 00";

        #region Private and protected members
        protected bool _diagnosticMessageJustSent = false;
        private readonly Random _connectionEventsSimulator = new Random(2020); // Seed added for unittesting purposes
        private readonly Random _randomMessageSimulator = new Random(2020); // Seed added for unittesting purposes
        #endregion

        #region Public properties
        /// <summary>Gets a value indicating whether the connection was successful and the client is online.</summary>
        /// <value>
        ///   <c>true</c> if online; otherwise, <c>false</c>.</value>
        public bool OnlineStatus { get; private set; }
        #endregion

        #region Connection handling
        /// <summary>Connects with the specified telemetry server connection string.</summary>
        /// <param name="telemetryServerConnectionString">The telemetry server connection string.</param>
        /// <exception cref="ArgumentNullException">if connection string is null or empty</exception>
        public void Connect(string telemetryServerConnectionString)
        {
            if (string.IsNullOrEmpty(telemetryServerConnectionString))
            {
                throw new ArgumentNullException();
            }

            // Fake the connection with 20% chances of success
            bool success = _connectionEventsSimulator.Next(1, 10) <= 2;
            OnlineStatus = success;
        }

        /// <summary>Disconnects this instance. Note, that even if the connection is disconnected already, no exceptions will be thrown.</summary>
        public void Disconnect()
        {
            OnlineStatus = false;
        }
        #endregion

        #region Send / Receive messages
        /// <summary>Sends the specified message.</summary>
        /// <param name="message">The message.</param>
        /// <exception cref="ArgumentNullException">message is null or empty</exception>
        public void Send(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException();
            }

            // The simulation of Send() actually just remember if the last message sent was a diagnostic message.
            // This information will be used to simulate the Receive(). Indeed there is no real server listening.
            _diagnosticMessageJustSent = (message == DiagnosticMessage);
        }

        /// <summary>Receive the diagnostic message or a random string</summary>
        /// <returns>diagnostic message or random string</returns>
        public string Receive()
        {
            string message;

            if (_diagnosticMessageJustSent)
            {
                // Simulate the reception of the diagnostic message
                message = DiagnosticResponse;
                // reset the diagnostic message just sent member
                _diagnosticMessageJustSent = false;
            }
            else
            {
                // Simulate the reception of a response message returning a random message.
                message = string.Empty;
                int messageLength = _randomMessageSimulator.Next(50, 110);
                for (int i = messageLength; i > 0; --i)
                {
                    message += (char)_randomMessageSimulator.Next(40, 126);
                }
            }

            return message;
        }
        #endregion
    }
}
