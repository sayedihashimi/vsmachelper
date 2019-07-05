using System;
using System.Threading.Tasks;
using VsmacHelper.Shared;
using Xunit;

namespace VsmacHelper.Tests
{
    public class CliCommandTests
    {
        [Fact]
        public async Task TestBasicPassingCommand()
        {
            CliCommand cmd = new CliCommand
            {
                FileName="dotnet",
                Arguments = "--version"
            };

            try
            {
                var result = await cmd.RunCommand();
                Assert.NotNull(result);
                Assert.Equal(0, result.ExitCode);
                Assert.NotEmpty(result.StandardOutput);
            }
            catch(Exception ex)
            {
                var msg = ex.ToString();
                Console.WriteLine(msg);
            }
        }
    }
}
