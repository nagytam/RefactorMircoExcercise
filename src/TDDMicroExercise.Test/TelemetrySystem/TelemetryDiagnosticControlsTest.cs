using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TDDMicroExercises.TelemetrySystem.Tests
{
    [TestClass]
    public class TelemetryDiagnosticControlsTest
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
            var expected = $@"LAST TX rate................ 100 MBPS
HIGHEST TX rate............. 100 MBPS
LAST RX rate................ 100 MBPS
HIGHEST RX rate............. 100 MBPS
BIT RATE.................... 100000000
WORD LEN.................... 16
WORD/FRAME.................. 511
BITS/FRAME.................. 8192
MODULATION TYPE............. PCM/FM
TX Digital Los.............. 0.75
RX Digital Los.............. 0.10
BEP Test.................... -5
Local Rtrn Count............ 00
Remote Rtrn Count........... 00";
            var result = teleDiagnostic.DiagnosticInfo;
            Assert.AreEqual(expected, result);
        }
        #endregion

    }
}
