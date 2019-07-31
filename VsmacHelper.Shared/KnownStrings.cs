using System.IO;

namespace VsmacHelper.Shared {
    public static class KnownStrings {
        private static readonly PathHelper _pathHelper = new PathHelper();
        public const string DefaultVsmacVersion = "8.0";
        public const string VsmacLogsFolderPath = "~/Library/Logs/VisualStudio";

        public static readonly string TelemetryLogFolder = Path.Combine(Path.GetTempPath(), "VSTelemetryLog");
        public static readonly string TelemetryConfigFolder = _pathHelper.GetFullPath(Path.Combine(_pathHelper.GetHomeFolder(), "VSTelemetry"));
        public const string TelemetryConfigFilename = "channels.json";

        public const string VisualStudioProcessName = "VisualStudio";
    }
}
