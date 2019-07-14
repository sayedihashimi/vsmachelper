﻿using McMaster.Extensions.CommandLineUtils;

namespace VsmacHelper {
    class Program {
        static void Main(string[] args) {
            using var app = new CommandLineApplication();
            app.Name = "vsmachelper";
            app.HelpOption(inherited: true);
            app.UsePagerForHelpText = false;

            // add commands
            app.Commands.Add(new CleanLogFolderCommand());
            app.Commands.Add(new OpenLogFolderCommand());
            app.Commands.Add(new CompressLogFilesCommand());
            app.Commands.Add(new OpenTelemetryLogsFolderCommand());
            app.Commands.Add(new CleanTelemetryLogsCommand());
            app.Commands.Add(new EnableTelemetryFileLoggerCommand());
            app.Commands.Add(new DisableTelemetryFileLoggerCommand());
            app.Commands.Add(new OpenTelemetryConfigFolder());
            app.Commands.Add(new KillVsmacCommand());

            app.Execute(args);
        }
    }
}
