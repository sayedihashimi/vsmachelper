using McMaster.Extensions.CommandLineUtils;
using System;
using VsmacHelper.Shared;

namespace VsmacHelper {
    public class OpenTelemetryLogsFolderCommand : BaseCommandLineApplication {
        public OpenTelemetryLogsFolderCommand() : base("OpenTelemetryLogsFolder", "opens the folder that contains the telemetry log files") {
            var telLogFolderPath = this.Option("-p|--path", $"opens the folder that contains the telemetry log files. Default value: '{KnownStrings.TelemetryLogFolder}'", CommandOptionType.SingleOrNoValue);

            this.OnExecute(async () => {
                var folderFullpath = telLogFolderPath.HasValue()
                    ? new PathHelper().GetFullPath(telLogFolderPath.Value())
                    : new PathHelper().GetFullPath(KnownStrings.TelemetryLogFolder);

                var openCommand = new CliCommand {
                    Command = "open",
                    Arguments = folderFullpath
                };

                if (VerboseOption.HasValue()) Console.WriteLine($"Opening telemetry log folder {folderFullpath}");

                var cmdresult = await openCommand.RunCommand();
                if (!string.IsNullOrEmpty(cmdresult.StandardError)) {
                    Console.WriteLine($"Error: {cmdresult.StandardError}");
                }
            });
        }
    }
}
