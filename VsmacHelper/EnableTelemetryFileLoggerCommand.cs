using System;
using System.IO;
using McMaster.Extensions.CommandLineUtils;
using VsmacHelper.Shared;

namespace VsmacHelper {
    public class EnableTelemetryFileLoggerCommand : BaseTelemetryFileLoggerCommand {
        public EnableTelemetryFileLoggerCommand() : base("EnableTelemetryFileLogger", "enables telemetry file logger") {

        }

        protected override string FileLoggerConfigValue => "enabled";
    }
}
