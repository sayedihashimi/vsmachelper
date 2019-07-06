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
            //app.OnExecute(() =>
            //{
            //    Console.WriteLine("in main execute");
            //});

            app.Commands.Add(new CleanLogFolderCommand());
            app.Commands.Add(new OpenLogFolderCommand());

            app.Execute(args);
        }
    }
}
