using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace TDDMicroExercises.TelemetrySystem.Tests
{
    /// <summary>Telemetry system tests</summary>
    [TestClass]
    public class TelemetrySystemTest
    {
        [TestMethod]
        public void CheckTransmission()
        {
            var mockTelemetryClient = new Mock<ITelemetryClient>();
            var isDisconnected = false;
            var isConnected = false;
            var usedConnectionString = string.Empty;
            var isSent = false;
            var diagnosticMessage = string.Empty;
            var isReceived = false;
            var diagnosticInfo = string.Empty;
            mockTelemetryClient.SetupSequence(s => s.OnlineStatus).
                Returns(false).
                Returns(true).
                Returns(true); // returns sequence { false, true, true }
            mockTelemetryClient.Setup(s => s.Disconnect()).Callback(() => { isDisconnected = true; });
            mockTelemetryClient.Setup(s => s.Connect(It.IsAny<string>())).Callback<string>(s => { isConnected = true; usedConnectionString = s; });
            mockTelemetryClient.Setup(s => s.Send(It.IsAny<string>())).Callback<string>(s => { isSent = true; diagnosticMessage = s; });
            mockTelemetryClient.Setup(s => s.Receive()).Callback(() => { isReceived = true; diagnosticInfo = "Received"; }).Returns("Received");

            var telemetryDiagnosticControls = new TelemetryDiagnosticControls(mockTelemetryClient.Object);
            telemetryDiagnosticControls.CheckTransmission();
            Assert.IsTrue(isDisconnected); // CheckTransmissions disconnects first the connection
            Assert.IsTrue(isConnected);    // CheckTransmissions call connect
            Assert.AreEqual("*111#", usedConnectionString);  // CheckTransmissions connects with the expected connection string
            Assert.IsTrue(isSent);         // CheckTransmissions call Send
            Assert.AreEqual(TelemetryClient.DiagnosticMessage, diagnosticMessage);
            Assert.IsTrue(isReceived);     // CheckTransmissions call Received
            Assert.AreEqual("Received", diagnosticInfo);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CheckTransmissionWithRetryCountReachedThrowsException()
        {
            var mockTelemetryClient = new Mock<ITelemetryClient>();
            mockTelemetryClient.SetupSequence(s => s.OnlineStatus).
                Returns(false).
                Returns(false).
                Returns(false); // returns sequence { false, false, false }
            mockTelemetryClient.Setup(s => s.Disconnect());
            mockTelemetryClient.Setup(s => s.Connect(It.IsAny<string>()));

            var telemetryDiagnosticControls = new TelemetryDiagnosticControls(mockTelemetryClient.Object);
            telemetryDiagnosticControls.CheckTransmission();
        }


    }
}
