using System;
using System.Threading;
using McMaster.Extensions.CommandLineUtils;

namespace VsmacHelper.Shared
{
    public class BaseCommandLineApplication : CommandLineApplication
    {
        public BaseCommandLineApplication(string name, string description,bool enableVerboseOption = true)
        {
            Name = name;
            Description = description;
            this.HelpOption();
            UsePagerForHelpText = false;

            if (enableVerboseOption)
            {
                VerboseOption = this.Option("--verbose", "Enable verbose logging", CommandOptionType.NoValue);
            }
        }
        protected CommandOption VerboseOption { get; private set; }
    }
}
