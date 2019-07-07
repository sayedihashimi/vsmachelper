using System;
using System.IO;
using McMaster.Extensions.CommandLineUtils;
using VsmacHelper.Shared;

namespace VsmacHelper {
    public class OpenLogFolderCommand : BaseCommandLineApplication {
        public OpenLogFolderCommand() : base("OpenLogFolder","Opens the log folder in Finder") {
            // options
            var logFolderPath = this.Option("-p|--path", $"Path to the log folder which will be opened. Default value: '{KnownStrings.VsmacLogsFolderPath}'", CommandOptionType.SingleOrNoValue);

            this.OnExecute(async () => {
                var logFolderPathFull = logFolderPath.HasValue()
                    ? new PathHelper().GetFullPath(logFolderPath.Value())
                    : new PathHelper().GetFullPath(KnownStrings.VsmacLogsFolderPath);

                var openCommand = new CliCommand {
                    Command = "open",
                    Arguments = logFolderPathFull
                };

                if (VerboseOption.HasValue()) Console.WriteLine($"Opening log folder {logFolderPathFull}");

                var cmdresult = await openCommand.RunCommand();
                if (!string.IsNullOrEmpty(cmdresult.StandardError)) {
                    Console.WriteLine($"Error: {cmdresult.StandardError}");
                }

            });
        }
    }
}
