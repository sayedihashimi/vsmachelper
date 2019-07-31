using McMaster.Extensions.CommandLineUtils;
using VsmacHelper.Shared;

namespace VsmacHelper {
    public class OpenTelemetryConfigFolder : BaseCommandLineApplication {
        public OpenTelemetryConfigFolder() : base("OpenTelemetryConfigFolder", "opens the telemetry config folder in Finder") {
            // options
            var optionTelConfigPath = this.Option(
                "-p|--path",
                $"Path to the telemetry config folder. Default value: '{KnownStrings.TelemetryConfigFolder}'",
                CommandOptionType.SingleOrNoValue);

            this.OnExecute(async () => {
                var telConfigPath = optionTelConfigPath.HasValue()
                    ? new PathHelper().GetFullPath(optionTelConfigPath.Value())
                    : new PathHelper().GetFullPath(KnownStrings.TelemetryConfigFolder);

                await new FolderHelper().OpenFolderInFinder(telConfigPath, VerboseEnabled);
            });
        }
    }
}
