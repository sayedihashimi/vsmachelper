using McMaster.Extensions.CommandLineUtils;
using VsmacHelper.Shared;

namespace VsmacHelper {
    public class OpenLogFolderCommand : BaseCommandLineApplication {
        public OpenLogFolderCommand() : base("OpenLogFolder", "Opens the log folder in Finder") {
            // options
            var logFolderPath = this.Option(
                "-p|--path",
                $"Path to the log folder which will be opened. Default value: '{KnownStrings.VsmacLogsFolderPath}'",
                CommandOptionType.SingleOrNoValue);

            this.OnExecute(async () => {
                var logFolderPathFull = logFolderPath.HasValue()
                    ? new PathHelper().GetFullPath(logFolderPath.Value())
                    : new PathHelper().GetFullPath(KnownStrings.VsmacLogsFolderPath);

                await new FolderHelper().OpenFolderInFinder(logFolderPathFull, VerboseEnabled);

            });
        }
    }

    /*
         function VSMac-OpenTelemetryConfigFolder{
            [cmdletbinding()]
            param(
                [string]$vstelfolderpath = (get-fullpathnormalized '~/VSTelemetry/')
            )
            process{
                if(test-path $vstelfolderpath){
                    open ($vstelfolderpath)
                }
                else{
                    "Tel config folder not found at '$vstelfolderpath'" |Write-Output
                }
            }
        }
     */

}
