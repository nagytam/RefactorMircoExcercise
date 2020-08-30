namespace TDDMicroExercises.TirePressureMonitoringSystem
{
    /// <summary>The alarm</summary>
    public class Alarm
    {
        #region
        private const double LowPressureThreshold = 17;
        private const double HighPressureThreshold = 21;
        readonly Sensor _sensor = new Sensor();
        public bool AlarmOn { get; private set; } = false;
        #endregion

        /// <summary>Checks the pressure and set the alarm if it is below the low pressure threshold or above the high pressure threshold.</summary>
        public void Check()
        {
            double psiPressureValue = _sensor.PopNextPressurePsiValue();

            if (psiPressureValue < LowPressureThreshold || HighPressureThreshold < psiPressureValue)
            {
                AlarmOn = true;
            }
        }

    }
}
