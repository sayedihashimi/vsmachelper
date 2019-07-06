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
    public class CleanLogFolderCommand : BaseCommandLineApplication {
        public CleanLogFolderCommand() : base("CleanLogFolder", "This will clean the log folder") {
            var argLogFolderPath = this.Option(
                "-l|--logfolderroot <PATH>",
                "defines that path to the root folder containg the log files. Default value: '~/Library/Logs/VisualStudio/'",
                CommandOptionType.SingleValue);

            var optionVersionNumber = Option(
                "-v|--version <VERSION>",
                "version number for VSMac [7.0 or 8.0]",
                CommandOptionType.SingleValue);
            optionVersionNumber.Validators.Add(new VsmacVersionValidator());

            this.OnExecute(() => {
                // options
                var logFolderRootPath = argLogFolderPath.HasValue()
                    ? argLogFolderPath.Value()
                    : Path.GetFullPath(Path.Combine(new PathHelper().GetHomeFolder(), "Library/Logs/VisualStudio/"));

                var versionNumber = optionVersionNumber.HasValue()
                    ? optionVersionNumber.Value()
                    : "8.0";

                Console.WriteLine($"in cleanlogfolder\tlogFolderRootPath: '{logFolderRootPath}'\tversion: {versionNumber}");

                var logfolderfullpath = Path.GetFullPath(Path.Combine(logFolderRootPath, versionNumber));

                var filesToDelete = Directory.GetFiles(logfolderfullpath, "*", SearchOption.TopDirectoryOnly);


                if(VerboseOption.HasValue()) Console.WriteLine("Deleting the following files:");

                foreach (var file in filesToDelete) {
                    if (VerboseOption.HasValue()) Console.WriteLine($"\t{file}");
                    File.Delete(file);
                }
            });
        }
    }
}
