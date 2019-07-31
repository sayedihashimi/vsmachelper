using McMaster.Extensions.CommandLineUtils;

namespace VsmacHelper.Shared {
    public class BaseCommandLineApplication : CommandLineApplication {
        public BaseCommandLineApplication(string name, string description, bool enableVerboseOption = true) {
            Name = name;
            Description = description;
            this.HelpOption();
            UsePagerForHelpText = false;

            if (enableVerboseOption) {
                VerboseOption = this.Option<bool>("--verbose", "Enable verbose logging", CommandOptionType.NoValue);
            }
        }

        protected CommandOption<bool> VerboseOption { get; private set; }
        protected bool VerboseEnabled {
            get {
                return VerboseOption != null && VerboseOption.HasValue();
            }
        }
    }
}
