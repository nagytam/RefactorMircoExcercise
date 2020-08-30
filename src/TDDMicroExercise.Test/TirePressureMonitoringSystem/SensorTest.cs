using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDDMicroExercises.TirePressureMonitoringSystem;

namespace TDDMicroExercise.Test.TirePressureMonitoringSystem
{
    [TestClass]
    public class SensorTest
    {
        #region Simulating existing client behaviour
        [TestMethod]
        public void SensorClient()
        {
            var sensor = new Sensor();

            double value = sensor.PopNextPressurePsiValue();
            Assert.AreEqual(value, 16.07, 0.01);
            value = sensor.PopNextPressurePsiValue();
            Assert.AreEqual(value, 19.09, 0.01);
            value = sensor.PopNextPressurePsiValue();
            Assert.AreEqual(value, 17.03, 0.01);
            value = sensor.PopNextPressurePsiValue();
            Assert.AreEqual(value, 20.96, 0.01);
        }
        #endregion

        #region Check properties of pressure values
        [TestMethod]
        public void PressureValuesOffset()
        {
            const int MaxValues = 100000;
            var sensor = new Sensor();

            for (int i = 0; i < MaxValues; i++)
            {
                double value = sensor.PopNextPressurePsiValue();
                Assert.IsTrue(value >= Sensor.Offset);
            }
        }
        #endregion
    }
}
