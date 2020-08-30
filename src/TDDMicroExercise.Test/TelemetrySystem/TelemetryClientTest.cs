using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TDDMicroExercises.TelemetrySystem;

namespace TDDMicroExercise.Test.TelemetrySystem
{
    [TestClass]
    public class TelemetryClientTest
    {
        #region Simulate existing clients
        [TestMethod]
        public void SimulateTelemetryClientClient()
        {
            var tc = new TelemetryClient();
            Assert.IsFalse(tc.OnlineStatus);
            tc.Connect("a connection string");
            Assert.IsTrue(tc.OnlineStatus);
            tc.Send("some message");
            Assert.IsTrue(tc.OnlineStatus);
            var response = tc.Receive();
            Assert.IsTrue(tc.OnlineStatus);
            Assert.AreEqual(@"Tfe9o|pt4QS@IwX|IFP:LHeai`ARvwVZ^|BA(?w=wqrwcZ0Q9/3", response);
            tc.Disconnect();
        }

        [TestMethod]
        public void SimulateTelemetryDiagnosticsControlClient1_2_3()
        {
            var teleDiagnostic = new TelemetryDiagnosticControls();
            Assert.AreEqual(string.Empty, teleDiagnostic.DiagnosticInfo);
            teleDiagnostic.CheckTransmission();
            var expected = TelemetryClient.DiagnosticResponse;
            var result = teleDiagnostic.DiagnosticInfo;
            Assert.AreEqual(expected, result);
        }
        #endregion


        #region Connect / Disconnect
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConnectWithNullConnectionStringThrowsArgumentNullException()
        {
            var tc = new TelemetryClient();
            tc.Connect(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConnectWithEmptyConnectionStringThrowsArgumentNullException()
        {
            var tc = new TelemetryClient();
            tc.Connect(string.Empty);
        }

        [TestMethod]
        public void SimulateDistributionofSuccessfulAndFailedConnections()
        {
            const int MaxConnections = 10000;
            var tc = new TelemetryClient();
            Assert.IsFalse(tc.OnlineStatus);

            #region Simulate N connections 
            int numberOfSuccessfulConnections = 0;
            int numberOfFailedConnections = 0;
            for (int i = 0; i < MaxConnections; i++)
            {
                tc.Connect("a connection string");
                if (tc.OnlineStatus)
                {
                    numberOfSuccessfulConnections++;
                }
                else
                {
                    numberOfFailedConnections++;
                }
                tc.Disconnect();
            }
            #endregion

            #region Check the distribution of the successful and failed connections
            Assert.AreEqual(numberOfSuccessfulConnections, 2000, 300);
            Assert.AreEqual(numberOfFailedConnections, 8000, 300);
            #endregion
        }
        #endregion

        #region Send diagnostic message and receive response
        [TestMethod]
        public void SendDiagnosticMessageSetDiagnosticMessageJustSentToTrueAndSetsDiagnosticMessageJustSentBackToFalse()
        {
            var tc = new TelemetryClientForTesting();
            tc.Connect("connectionString");
            Assert.IsFalse(tc.DiagnosticMessageJustSent);
            tc.Send(TelemetryClient.DiagnosticMessage);
            Assert.IsTrue(tc.DiagnosticMessageJustSent);
            var response = tc.Receive();
            Assert.IsFalse(tc.DiagnosticMessageJustSent);
        }

        [TestMethod]
        public void SendDiagnosticMessageLeavesDiagnosticMessageJustSentToFalse()
        {
            var tc = new TelemetryClientForTesting();
            tc.Connect("connectionString");
            Assert.IsFalse(tc.DiagnosticMessageJustSent);
            tc.Send("random message");
            Assert.IsFalse(tc.DiagnosticMessageJustSent);
            var response = tc.Receive();
            Assert.IsFalse(string.IsNullOrEmpty(response));
            Assert.IsTrue(response.Length >= 50 && response.All(c => c >= 40 && c <= 126));
        }
        #endregion

    }
}
