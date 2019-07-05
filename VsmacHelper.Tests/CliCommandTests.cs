using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
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
                Command ="dotnet",
                Arguments = "--version"
            };


            var result = await cmd.RunCommand();
            Assert.NotNull(result);
            Assert.Equal(0, result.ExitCode);
            Assert.NotEmpty(result.StandardOutput);
            Assert.Empty(result.StandardError);
        }

        [Fact]
        public async Task TestMissingCommand()
        {
            CliCommand cmd = new CliCommand
            {
                Command = "doesntexistsayedha",
                Arguments = "something here"
            };

            var result = await cmd.RunCommand();
            Assert.NotNull(result);
            Assert.NotNull(result.Exception);
        }

        [Fact]
        public async Task TestInvalidArgsCommand()
        {
            var cmd = new CliCommand
            {
                Command = "dotnet",
                Arguments = "invalidargshere"
            };
            var result = await cmd.RunCommand();
            Assert.NotNull(result);
            Assert.NotNull(result.StandardError);
            Assert.NotEqual(0, result.ExitCode);
        }
    }
}
