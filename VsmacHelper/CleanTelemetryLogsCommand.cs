using McMaster.Extensions.CommandLineUtils;
using System;
using System.IO;
using System.Runtime.InteropServices;
using VsmacHelper.Shared;

namespace VsmacHelper {
    public class CleanTelemetryLogsCommand : BaseCommandLineApplication {
        public CleanTelemetryLogsCommand() : base("CleanTelemetryLogs","removes the telemetry log files") {
            var optionTelFolderPath = Option("-p|--folderPath", "path to the folder that contains log files that will be deleted", CommandOptionType.SingleValue);

            this.OnExecute(() => {
                var telFolderFullPath = optionTelFolderPath.HasValue()
                    ? new PathHelper().GetFullPath(optionTelFolderPath.Value())
                    : new PathHelper().GetFullPath(KnownStrings.VsmacLogsFolderPath);

                if (!Directory.Exists(telFolderFullPath)) {
                    Console.WriteLine($"Folder not found at: '{telFolderFullPath}'");
                    return;
                }

                var filesToDelete = Directory.GetFiles(telFolderFullPath, "*", SearchOption.TopDirectoryOnly);
                if(filesToDelete.Length <=0) {
                    Console.WriteLine("Folder is empty");
                }
                else {
                    if (VerboseOption.HasValue()) Console.WriteLine($"Cleaning tel folder at: '{telFolderFullPath}'");

                    foreach(var file in filesToDelete) {
                        var fileFullPath = Path.GetFullPath(file);
                        if (VerboseOption.HasValue()) Console.WriteLine($"Removing file {fileFullPath}");
                        File.Delete(fileFullPath);
                    }
                }

            });
        }
    }
}
