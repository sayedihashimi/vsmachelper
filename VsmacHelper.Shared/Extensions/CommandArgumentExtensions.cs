using System;
using McMaster.Extensions.CommandLineUtils;

namespace VsmacHelper.Shared.Extensions
{
    public static class CommandArgumentExtensions
    {
        public static bool HasValue(this CommandArgument argument)
        {
            return argument.Value != null && !string.IsNullOrEmpty(argument.Value);
        }
    }
}
