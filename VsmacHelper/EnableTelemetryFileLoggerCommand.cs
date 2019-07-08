using System;
using System.IO;
using McMaster.Extensions.CommandLineUtils;
using VsmacHelper.Shared;

namespace VsmacHelper {
    public class EnableTelemetryFileLoggerCommand : BaseCommandLineApplication {
        public EnableTelemetryFileLoggerCommand() : base("EnableTelemetryFileLogger","enables telemetry file logger") {
            var optionTelFolderPath = Option(
                "-p|--folderPath",
                $"path to the telemetry config folder. Default path is '{KnownStrings.TelemetryConfigFolder}'",
                CommandOptionType.SingleValue);

            var optionFilename = Option(
                "-f|--filename",
                $"name of the file that will be modified. Default value is '{KnownStrings.TelemetryConfigFilename}'",
                CommandOptionType.SingleValue);
            
            this.OnExecute(() => {
                var telFolderPath = optionTelFolderPath.HasValue()
                    ? new PathHelper().GetFullPath(optionTelFolderPath.Value())
                    : new PathHelper().GetFullPath(KnownStrings.TelemetryConfigFolder);

                var filename = optionFilename.HasValue()
                    ? optionFilename.Value()
                    : KnownStrings.TelemetryConfigFilename;

                if (VerboseEnabled) Console.WriteLine($"parameters:\n\ttelFolderPath='{telFolderPath}'\n\tfilename:'{filename}'");

                // if the target folder doesn't exist create it
                if (!Directory.Exists(telFolderPath)) {
                    Console.WriteLine($"telemetry config folder not found, creating at '{telFolderPath}'");
                    Directory.CreateDirectory(telFolderPath);
                }

                string filepath = Path.Combine(telFolderPath, KnownStrings.TelemetryConfigFilename);
                // create the file with the contents to enable the file logger
                if (VerboseEnabled) Console.WriteLine($"Creating/replacing file to enable telemetry file logger, path='{filepath}'");
                using (var tw = new StreamWriter(filepath, false)) {
                    tw.Write(_enableTelemetryLoggerConfigFileContents);
                }
            });
        }

        private string _enableTelemetryLoggerConfigFileContents = @"{
    ""fileLogger"": ""enabled""
}";
    }
}
