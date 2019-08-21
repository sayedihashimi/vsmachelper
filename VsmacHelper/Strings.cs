using System;
using System.Reflection.Metadata;
using VsmacHelper.Shared;

namespace VsmacHelper {
    internal static class Strings {
        private static PathHelper _pathHelper = new PathHelper();
        internal readonly static string InstallerLogDir = _pathHelper.GetFullPath("~/Library/Logs/VisualStudioInstaller");
    }
}
