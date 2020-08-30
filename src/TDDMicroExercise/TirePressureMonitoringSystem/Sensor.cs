using System;

namespace TDDMicroExercises.TirePressureMonitoringSystem
{
    //
    // The reading of the pressure value from the sensor is simulated in this implementation.
    // Because the focus of the exercise is on the other class.
    //
    public class Sensor : ISensor
    {
        #region
        public const double Offset = 16;
        private readonly Random _randomPressureSampleSimulator = new Random(2020);
        #endregion

        /// <summary>Pops the next pressure PSI value.</summary>
        /// <returns>The pressure value</returns>
        public double PopNextPressurePsiValue()
        {
            double pressureTelemetryValue = ReadPressureSample();

            return Offset + pressureTelemetryValue;
        }

        /// <summary>Reads the pressure sample.</summary>
        /// <returns>The pressure sample</returns>
        private double ReadPressureSample()
        {
            // Simulate info read from a real sensor in a real tire
            return 6 * _randomPressureSampleSimulator.NextDouble() * _randomPressureSampleSimulator.NextDouble();
        }
    }
}
