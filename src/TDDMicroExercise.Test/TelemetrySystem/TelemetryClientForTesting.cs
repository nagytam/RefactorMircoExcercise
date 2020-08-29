using TDDMicroExercises.TelemetrySystem;

namespace TDDMicroExercise.Test.TelemetrySystem
{
    /// <summary>Helper class for testing the telemetry client</summary>
    public class TelemetryClientForTesting : TelemetryClient
    {
        /// <summary>Gets a value indicating whether the diagnostic message just sent.</summary>
        /// <value>
        ///   <c>true</c> if the diagnostic message just sent; otherwise, <c>false</c>.</value>
        public bool DiagnosticMessageJustSent => _diagnosticMessageJustSent;
    }
}
