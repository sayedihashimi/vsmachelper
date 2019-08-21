using System;
using System.IO;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace VsmacHelper.Shared {
    public class FolderHelper {
         
        public async Task OpenFolderInFinder(string path, bool verbose) {
            var openCommand = new CliCommand {
                Command = "open",
                Arguments = path
            };

            if (string.IsNullOrEmpty(path)) {
                Console.Error.WriteLine($"ERROR: path to folder is empty");
                return;
            }
            if (!Directory.Exists(path)) {
                Console.Error.WriteLine($"ERROR: cannot open folder because it doesn't exist. Folder path: '{path}'");
                return;
            }

            if (verbose) Console.WriteLine($"Opening log folder '{path}'");

            var cmdresult = await openCommand.RunCommand();
            if (!string.IsNullOrEmpty(cmdresult.StandardError)) {
                Console.WriteLine($"Error: {cmdresult.StandardError}");
            }
        }

    }
}
