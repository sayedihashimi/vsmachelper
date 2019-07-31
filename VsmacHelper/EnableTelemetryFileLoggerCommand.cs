namespace VsmacHelper {
    public class EnableTelemetryFileLoggerCommand : BaseTelemetryFileLoggerCommand {
        public EnableTelemetryFileLoggerCommand() : base("EnableTelemetryFileLogger", "enables telemetry file logger") {

        }

        protected override string FileLoggerConfigValue => "enabled";
    }
}
