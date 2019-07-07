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

        public string GetFullpath(string path) {
            if (string.IsNullOrWhiteSpace(path)) return null;

            var pathstr = path.Trim();
            if (pathstr.StartsWith("~")) {
                pathstr = pathstr.Substring(1, pathstr.Length - 1);
                pathstr = new PathHelper().GetHomeFolder() + pathstr;
            }

            return Path.GetFullPath(pathstr);
        }
    }
}
