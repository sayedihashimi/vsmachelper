using System;
using System.IO;
using McMaster.Extensions.CommandLineUtils;
using VsmacHelper.Shared;

namespace VsmacHelper {
    public class OpenLogFolderCommand : BaseCommandLineApplication {
        public OpenLogFolderCommand() : base("OpenLogFolder","Opens the log folder in Finder") {
            // options
            var logFolderPath = this.Option("-p|--path", "Path to the log folder which will be opened. Default value: ~/Library/Logs/VisualStudio/", CommandOptionType.SingleOrNoValue);


            this.OnExecute(async () => {
                var logFolderPathFull = logFolderPath.HasValue()
                    ? Path.GetFullPath(logFolderPath.Value())
                    : Path.GetFullPath(Path.Combine(new PathHelper().GetHomeFolder(), "Library/Logs/VisualStudio/"));

                var openCommand = new CliCommand {
                    Command = "open",
                    Arguments = logFolderPathFull
                };

                _ = await openCommand.RunCommand();
            });
        }
    }
}
