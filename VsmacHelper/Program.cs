using System;
using System.Collections;
using System.Collections.Generic;
using McMaster.Extensions.CommandLineUtils;

namespace VsmacHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            using var app = new CommandLineApplication();
            app.Name = "vsmachelper";
            app.HelpOption(inherited:true);
            app.UsePagerForHelpText = false;
//            app.ExtendedHelpText = @"
//vsmachelper <COMMAND>

//Arguments:
//  Commands
//    CleanLogFolder
//    CompressLogs
//    OpenLogFolder
//    EnableTelemetry
//    DisableTelemetry
//    OpenTelemetryFolder";

//            var optionSubject = app.Option("-s|--subject <SUBJECT>", "The subject", CommandOptionType.SingleValue);
//            var optionRepeat = app.Option<int>("-n|--count <N>", "Repeat", CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                //var subject = optionSubject.HasValue()
                //    ? optionSubject.Value()
                //    : "world";
                //var count = optionRepeat.HasValue() ? optionRepeat.ParsedValue : 1;
                //for (var i = 0; i < count; i++)
                //{
                //    Console.WriteLine($"Hello {subject}");
                //}
                Console.WriteLine("in main execute");
            });

            //app.Commands.AddRange(GetCommands());

            //app.Command("tester", testerCmd =>
            //{
            //    testerCmd.UsePagerForHelpText = false;
            //    testerCmd.ExtendedHelpText = "*** tester ***";
            //});
            app.Commands.Add(new CleanLogFolderCommand());


            app.Execute(args);
        }

        static IList<CommandLineApplication> GetCommands()
        {
            var cmds = new List<CommandLineApplication>();
            using (var testcommand = new CommandLineApplication())
            {
                testcommand.Name = "tester from list";
                testcommand.HelpOption();
                testcommand.UsePagerForHelpText = false;
                testcommand.ExtendedHelpText = "*** test command help ***";

                testcommand.OnExecute(() =>
                {
                    Console.WriteLine("in test command222");
                });
            }

            return cmds;
        }
    }
}
