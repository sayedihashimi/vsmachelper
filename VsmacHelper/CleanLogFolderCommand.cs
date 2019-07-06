using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Validation;
using VsmacHelper.Shared;
using VsmacHelper.Shared.Extensions;

namespace VsmacHelper
{
    public class CleanLogFolderCommand : BaseCommandLineApplication
    {
        public CleanLogFolderCommand():base("CleanLogFolder", "This will clean the log folder")
        {
            var argLogFolderPath = this.Option(
                "-l|--logfolderroot <PATH>",
                "defines that path to the root folder containg the log files. Default value: '~/Library/Logs/VisualStudio/'",
                CommandOptionType.SingleValue);

            var optionVersionNumber = Option(
                "-v|--version <VERSION>",
                "version number for VSMac [7.0 or 8.0]",
                CommandOptionType.SingleValue);
            optionVersionNumber.Validators.Add(new VsmacVersionValidator());

            this.OnExecute(() =>
            {
                // arguments
                var logFolderRootPath = argLogFolderPath.HasValue()
                    ? argLogFolderPath.Value()
                    : Path.GetFullPath(@"~/Library/Logs/VisualStudio/");

                // options
                var versionNumber = optionVersionNumber.HasValue()
                    ? optionVersionNumber.Value()
                    : "8.0";

                Console.WriteLine($"in cleanlogfolder\tlogFolderRootPath: '{logFolderRootPath}'\tversion: {versionNumber}");
            });
        }

        //[Option("-ver|--version",CommandOptionType.SingleValue,Description = "version number for VSMac [7.0 or 8.0]",ShowInHelpText =true)]
        //[Required]
        public string VsmacVersion
        { get; set; }


        /*
         *
        function VSMac-CleanLogFolder{
        [cmdletbinding()]
        param(
            [string]$logRootFolderPath = '~/Library/Logs/VisualStudio/',
            
            [ValidateSet('7.0','8.0')]
            [string]$version = '8.0'
        )
        process{
            $logFolderPath = (Join-Path -Path $logRootFolderPath -ChildPath $version)

            if(test-path $logFolderPath){
                $files = (Get-ChildItem -Path $logFolderPath -Filter '*.log').FullName
                "Deleting files:`n" + ($files -join "`n") | Write-Output
                Remove-Item -LiteralPath $files
            }
            else{
                "Log folder not found at $logFolderPath" | Write-Warning
            }
        }
    }
         */
    }


    public class VsmacVersionValidator : IOptionValidator
    {
        public ValidationResult GetValidationResult(CommandOption option, ValidationContext context)
        {
            if (!option.HasValue()) return ValidationResult.Success;

            var val = option.Value();

            if (val != "7.0" && val != "8.0")
            {
                return new ValidationResult($"The value for --{option.LongName} must be either '7.0' or '8.0'");
            }

            return ValidationResult.Success;
        }
    }

}
