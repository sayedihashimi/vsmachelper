using McMaster.Extensions.CommandLineUtils;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using VsmacHelper.Shared;

namespace VsmacHelper {
    public class KillVsmacCommand : BaseCommandLineApplication {
        public KillVsmacCommand() : base("KillVsmac", "This will kill the vsmac process and callstack will be written to the log file.") {
            // parameters
            // var paramProcId = this.Argument<int>("processid", "Id of the process to kill").IsRequired();

            // options
            var optionProcessName = this.Option<string>(
                "-n|--name",
                $"Name of the process that will be killed. Default: '{KnownStrings.VisualStudioProcessName}'",
                CommandOptionType.SingleValue);

            var optionProcessId = this.Option<int>(
                "-i|--id",
                "Process ID of the process to kill. If this and Process Name are passed, this will take precedent.",
                CommandOptionType.SingleValue);

            this.OnExecute(async () => {
                var processName = optionProcessName.HasValue()
                    ? optionProcessName.ParsedValue
                    : KnownStrings.VisualStudioProcessName;

                Process processToKill = null;

                // try to assign by id first
                if (optionProcessId.HasValue()) {
                    processToKill = Process.GetProcessById(optionProcessId.ParsedValue);
                }
                else {
                    // only look for it by name if processId is not passed in
                    var findByNameResults = Process.GetProcessesByName(processName);
                    if (findByNameResults.Length <= 0) {
                        Console.WriteLine($"No process found with name '{processName}'");
                    }
                    else if (findByNameResults.Length > 1) {
                        Console.WriteLine($"More than one process found with name '{processName}'. Rerun this command and pass the process id.");
                        ShowHelp();
                    }
                    else if (findByNameResults.Length == 1) {
                        processToKill = findByNameResults[0];
                    }
                }

                // kill it here
                if (processToKill != null) {
                    var killCliCommand = new CliCommand {
                        Command = "kill",
                        Arguments = $"-QUIT {processToKill.Id}"
                    };

                    Console.WriteLine(
                        $"Killing VisualStudio process with command '{killCliCommand.Command} {killCliCommand.Arguments}'");

                    var killResult = await killCliCommand.RunCommand();

                    if (VerboseEnabled) Console.WriteLine($"kill command exited with code: '{killResult.ExitCode}'");

                    if (killResult.Exception == null) {
                        Console.WriteLine(killResult.ToString());
                    }

                    // wait a second, and see if the process still exists
                    await Task.Delay(1000);
                    Process p2 = null;
                    try {
                        p2 = Process.GetProcessById(processToKill.Id);
                    }
                    catch (ArgumentException) {
                        // do nothing, the process was not found by that id
                    }

                    if (p2 != null) {
                        await new CliCommand {
                            Command = "kill",
                            Arguments = $"-KILL {processToKill.Id}"
                        }.RunCommand();
                    }
                }
            });
        }
    }
}
