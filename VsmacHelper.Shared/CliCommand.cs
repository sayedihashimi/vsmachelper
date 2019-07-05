using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;

namespace VsmacHelper.Shared
{
    public class CliCommand : ICliCommand
    {
        public string FileName { get; set; }
        public string Arguments { get; set; }
        public string WorkingDirectory { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ProcessWindowStyle WindowStyle { get; set; } = ProcessWindowStyle.Hidden;
        public bool CreateNoWindow { get; set; } = true;
        public int TimeoutMilliseconds { get; set; }


        public async Task<ICliCommandResult> RunCommand()
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = FileName,
                CreateNoWindow = CreateNoWindow,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            if (!string.IsNullOrWhiteSpace(Arguments))
            {
                startInfo.Arguments = Arguments;
            }

            if (!string.IsNullOrWhiteSpace(Username))
            {
                startInfo.UserName = Username;
            }
            if (!string.IsNullOrWhiteSpace(Password))
            {
                startInfo.PasswordInClearText = Password;
            }

            var cmdProcess = Process.Start(startInfo);


            if (TimeoutMilliseconds > 0)
            {
                cmdProcess.WaitForExit(TimeoutMilliseconds);
            }
            else
            {
                cmdProcess.WaitForExit();
            }
            
            var stdout = await cmdProcess.StandardOutput.ReadToEndAsync();
            var stderr = await cmdProcess.StandardError.ReadToEndAsync();

            ICliCommandResult result = new CliCommandResult
            {
                ExitCode = cmdProcess.ExitCode,
                StandardOutput = stdout,
                StandardError = stderr
            };
            return result;
        }

        
    }
}
