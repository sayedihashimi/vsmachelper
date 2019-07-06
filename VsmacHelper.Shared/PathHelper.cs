using System;
using System.IO;
using System.Runtime.InteropServices;

namespace VsmacHelper.Shared {
    public class PathHelper {
        public string GetHomeFolder() {
            var envHome = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "HOMEPATH" : "HOME";
            var home = Environment.GetEnvironmentVariable(envHome);
            return Path.GetFullPath(home);
        }
    }
}
