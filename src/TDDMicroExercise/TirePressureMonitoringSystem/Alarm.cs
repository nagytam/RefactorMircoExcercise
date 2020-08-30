using System;

namespace TDDMicroExercises.TirePressureMonitoringSystem
{
    /// <summary>The alarm</summary>
    public class Alarm
    {
        #region
        public const double LowPressureThreshold = 17;
        public const double HighPressureThreshold = 21;
        private readonly ISensor _sensor;
        public bool AlarmOn { get; private set; } = false;
        #endregion

        #region Constructors
        /// <summary>Initializes a new instance of the <a onclick="return false;" href="Alarm" originaltag="see">Alarm</a> class.</summary>
        public Alarm()
        {
            _sensor = new Sensor();
        }
        
        /// <summary>Initializes a new instance of the <a onclick="return false;" href="Alarm" originaltag="see">Alarm</a> class.</summary>
        /// <param name="sensor">The sensor.</param>
        public Alarm(ISensor sensor)
        {
            if (sensor==null)
            {
                throw new ArgumentNullException(nameof(sensor));
            }

            _sensor = sensor;
        }
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
