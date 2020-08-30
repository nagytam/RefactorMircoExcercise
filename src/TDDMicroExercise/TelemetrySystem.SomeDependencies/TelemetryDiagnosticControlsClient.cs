namespace TDDMicroExercises.TelemetrySystem.SomeDependencies
{
    public class TelemetryDiagnosticControlsClient
    {
		// A class with the only goal of simulating a dependency on TelemetryDiagnosticControls
		// that has impact on the refactoring.

		public TelemetryDiagnosticControlsClient()
        {
            var teleDiagnostic = new TelemetryDiagnosticControls();

            teleDiagnostic.CheckTransmission();

            var result = teleDiagnostic.DiagnosticInfo;
        }
    }
}
