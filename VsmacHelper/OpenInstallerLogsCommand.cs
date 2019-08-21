using System;
using VsmacHelper.Shared;
using McMaster.Extensions.CommandLineUtils;

namespace VsmacHelper {
    public class OpenInstallerLogsCommand : BaseCommandLineApplication{
        public OpenInstallerLogsCommand():base(
                "OpenInstallerLogFolder",
                "Opens the installer log directory") {

            // options
            var logFolderPath = this.Option(
                "-p|--path",
                $"Path to the log folder which will be opened. Default value: '{Strings.InstallerLogDir}'",
                CommandOptionType.SingleOrNoValue);

            this.OnExecute(async () => {
                var logFolderPathFull = logFolderPath.HasValue()
                    ? new PathHelper().GetFullPath(logFolderPath.Value())
                    : new PathHelper().GetFullPath(Strings.InstallerLogDir);

                await new FolderHelper().OpenFolderInFinder(logFolderPathFull, VerboseEnabled);
            });
        }
    }
}
