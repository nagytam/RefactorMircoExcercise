using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
            var alarm = new Alarm();
            Assert.IsFalse(alarm.AlarmOn);
            alarm.Check();
            Assert.IsTrue(alarm.AlarmOn);
        }
        #endregion

        #region 
        [TestMethod]
        public void CheckAlarms()
        {
            const int MaxChecks = 10000;
            var alarm = new Alarm();
            Assert.IsFalse(alarm.AlarmOn);
            for (int i = 0; i < MaxChecks; i++)
            {
                alarm.Check();
                Assert.IsTrue(alarm.AlarmOn);
            }
        }

        [TestMethod]
        public void CheckAlarmsWithMockSensorReturnsPressureLowerThanLowPressureThresholdTriggersAlarm()
        {
            var mockSensor = new Mock<ISensor>();
            mockSensor.Setup(s => s.PopNextPressurePsiValue()).Returns(Alarm.LowPressureThreshold);

            var alarm = new Alarm(mockSensor.Object);
            Assert.IsFalse(alarm.AlarmOn);
            alarm.Check();
            Assert.IsFalse(alarm.AlarmOn);
        }

        [TestMethod]
        public void CheckAlarmsWithMockSensorReturnsLowPressureThresholdKeepsAlarmOff()
        {
            var mockSensor = new Mock<ISensor>();
            mockSensor.Setup(s => s.PopNextPressurePsiValue()).Returns(Alarm.LowPressureThreshold - 0.1);

            var alarm = new Alarm(mockSensor.Object);
            Assert.IsFalse(alarm.AlarmOn);
            alarm.Check();
            Assert.IsTrue(alarm.AlarmOn);
        }

        [TestMethod]
        public void CheckAlarmsWithMockSensorReturnsAveragePressureKeepsAlarmOff()
        {
            var mockSensor = new Mock<ISensor>();
            mockSensor.Setup(s => s.PopNextPressurePsiValue()).Returns((Alarm.LowPressureThreshold + Alarm.HighPressureThreshold) / 2);

            var alarm = new Alarm(mockSensor.Object);
            Assert.IsFalse(alarm.AlarmOn);
            alarm.Check();
            Assert.IsFalse(alarm.AlarmOn);
        }

        [TestMethod]
        public void CheckAlarmsWithMockSensorReturnsHighPressureThresholdKeepsAlarmOff()
        {
            var mockSensor = new Mock<ISensor>();
            mockSensor.Setup(s => s.PopNextPressurePsiValue()).Returns(Alarm.HighPressureThreshold);

            var alarm = new Alarm(mockSensor.Object);
            Assert.IsFalse(alarm.AlarmOn);
            alarm.Check();
            Assert.IsFalse(alarm.AlarmOn);
        }
        [TestMethod]
        public void CheckAlarmsWithMockSensorReturnsHigherPressureThanThresholdTriggersAlarm()
        {
            var mockSensor = new Mock<ISensor>();
            mockSensor.Setup(s => s.PopNextPressurePsiValue()).Returns(Alarm.HighPressureThreshold + 0.1);

            var alarm = new Alarm(mockSensor.Object);
            Assert.IsFalse(alarm.AlarmOn);
            alarm.Check();
            Assert.IsTrue(alarm.AlarmOn);
        }
        #endregion
    }
}
