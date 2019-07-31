using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Validation;
using VsmacHelper.Shared;
using VsmacHelper.Shared.Extensions;

namespace VsmacHelper {
    public class CleanLogsCommand : BaseCommandLineApplication {
        public CleanLogsCommand() : base("CleanLogs", "This will clean the log folder") {
            // command options
            var optionLogFolderPath = this.Option(
                "-l|--logfolderroot <PATH>",
                $"defines that path to the root folder containg the log files. Default value: '{KnownStrings.VsmacLogsFolderPath}'",
                CommandOptionType.SingleValue);

            var optionVersionNumber = Option(
                "-v|--version <VERSION>",
                "version number for VSMac [7.0 or 8.0]",
                CommandOptionType.SingleValue);
            optionVersionNumber.Validators.Add(new VsmacVersionValidator());

            this.OnExecute(() => {
                var logFolderRootPath = optionLogFolderPath.HasValue()
                    ? optionLogFolderPath.Value()
                    : new PathHelper().GetFullPath(KnownStrings.VsmacLogsFolderPath);

                var versionNumber = optionVersionNumber.HasValue()
                    ? optionVersionNumber.Value()
                    : "8.0";
                
                if (VerboseOption.HasValue()) {
                    Console.WriteLine($"CleanLogFolder called with:\n\tlogFolderRootPath:\t{logFolderRootPath}\n\tversionNumber:\t\t{versionNumber}");
                }

                var logfolderfullpath = Path.GetFullPath(Path.Combine(logFolderRootPath, versionNumber));

                var filesToDelete = Directory.GetFiles(logfolderfullpath, "*", SearchOption.TopDirectoryOnly);

                if (filesToDelete.Length > 0) {
                    if (VerboseOption.HasValue()) Console.WriteLine("Deleting the following files:");

                    foreach (var file in filesToDelete) {
                        if (VerboseOption.HasValue()) Console.WriteLine($"\t{file}");
                        File.Delete(file);
                    }
                }
                else {
                    Console.WriteLine($"No files to delete in folder: {logfolderfullpath}");
                }
            });
        }
    }
}
