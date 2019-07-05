using System;
using System.Diagnostics;
using System.Threading.Tasks;
using VsmacHelper.Shared;

namespace VsmacHelper
{
    public class CleanLogFolderCommand : CliCommand
    {
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
        public CleanLogFolderCommand()
        {
            Command = "";
        }
    }
}
