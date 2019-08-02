using System;
using McMaster.Extensions.CommandLineUtils;
using VsmacHelper.Shared;

namespace VsmacHelper {
    public class TailLogFileCommand : BaseCommandLineApplication {
        public TailLogFileCommand() :base(
            "TailLog",
            "Tails the IDE log") {

            // options
            var optionIdeVersion
                = this.Option<string>(
                "-v|--ideversion",
                $"Version of VSmac - default is {KnownStrings.DefaultVsmacVersion}",
                CommandOptionType.SingleValue);

            var optionFilePath = this.Option<string>(
                "-f|--filepath",
                $"File path to the log file - default is {KnownStrings.VsMacLogFilePath}",
                CommandOptionType.SingleValue);

            this.OnExecute(async () => {
                var ideVersion = optionIdeVersion.HasValue()
                                    ? optionIdeVersion.ParsedValue
                                    : KnownStrings.DefaultVsmacVersion;

                var logfilePath = optionFilePath.HasValue()
                                ? optionFilePath.ParsedValue
                                : KnownStrings.VsMacLogFilePath;
                // tail -f /Users/sayedhashimi/Library/Logs/VisualStudio/8.0/Ide.log
                var tailCommand = new CliCommand {
                    Command = "tail",
                    Arguments = $"-f {logfilePath}"
                };

                await tailCommand.RunCommand(captureStdOutput: false, captureStdError: false);
            });
        }
    }
}
