using McMaster.Extensions.CommandLineUtils;
using System;
using System.IO;
using VsmacHelper.Shared;

namespace VsmacHelper {
    public class CompressLogsCommand : BaseCommandLineApplication {
        public CompressLogsCommand() :
            base("CompressLogs",
                "This will create a .zip of the existing log files. The file will be created by default in the log directory") {

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

            var optionDestFilename = Option(
                "-f|--filename",
                "defines the filename of the log file that will be created. Default filename has a timestamp and version.",
                CommandOptionType.SingleValue);

            this.OnExecute(() => {
                string logfolder = optionLogFolderPath.HasValue()
                    ? new PathHelper().GetFullPath(optionLogFolderPath.Value())
                    : new PathHelper().GetFullPath(KnownStrings.VsmacLogsFolderPath);

                string version = optionVersionNumber.HasValue()
                    ? optionVersionNumber.Value()
                    : KnownStrings.DefaultVsmacVersion;

                if (!Directory.Exists(logfolder)) throw new DirectoryNotFoundException($"log directory not found at {logfolder}");

                string destfilename = optionDestFilename.HasValue()
                    ? optionDestFilename.Value()
                    : $"ide-{version}-log-{DateTime.Now.ToString("yyyy.MM.dd.ss.ff")}.zip";

                if (VerboseOption.HasValue()) {
                    Console.WriteLine($@"CompressLogFiles called with
    folder:    '{logfolder}'
    version:   '{version}'
    filename:  '{destfilename}'");
                }

                if (VerboseOption.HasValue()) Console.WriteLine("Creating zip file now");

                System.IO.Compression.ZipFile.CreateFromDirectory(
                    Path.Combine(logfolder, version),
                    Path.Combine(logfolder, destfilename));
                Console.WriteLine($"Zip file created at {Path.Combine(logfolder, destfilename)}");
            });
        }
    }
}
