using System;
namespace VsmacHelper {
    public class DisableTelemetryFileLoggerCommand : BaseTelemetryFileLoggerCommand {
        public DisableTelemetryFileLoggerCommand() : base("DisableTelemetryFileLogger", "disables telemetry file logger") {

        }

        protected override string FileLoggerConfigValue => "disabled";
    }
}
