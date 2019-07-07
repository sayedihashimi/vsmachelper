using System;
using System.IO;

namespace VsmacHelper.Shared {
    public static class KnownStrings {
        public const string DefaultVsmacVersion = "8.0";
        public const string VsmacLogsFolderPath = "~/Library/Logs/VisualStudio";
        public static readonly string TelemetryLogFolder = Path.Combine(Path.GetTempPath(), "VSTelemetryLog");
    }
}
