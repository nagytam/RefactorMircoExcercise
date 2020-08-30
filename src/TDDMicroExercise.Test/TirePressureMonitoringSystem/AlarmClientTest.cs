using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDDMicroExercises.TirePressureMonitoringSystem;

namespace TDDMicroExercise.Test.TirePressureMonitoringSystem
{
    [TestClass]
    public class AlarmClientTest
    {
        #region Simulate existing clients
        [TestMethod]
        public void AlarmClient1()
        {
            var anAlarm = new Alarm();
            Assert.IsFalse(anAlarm.AlarmOn);
            anAlarm.Check();
            Assert.IsTrue(anAlarm.AlarmOn);
        }
        #endregion
        
        #region 
        [TestMethod]
        public void a()
        {

        }
        #endregion
    }
}
