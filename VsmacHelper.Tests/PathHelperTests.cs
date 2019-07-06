using System;
using System.IO;
using VsmacHelper.Shared;
using Xunit;

namespace VsmacHelper.Tests {
    public class PathHelperTests {

        [Fact]
        public void TestGetHomeDirectory() {
            string homeFolder = new PathHelper().GetHomeFolder();
            Assert.NotEmpty(homeFolder);
            Assert.True(Directory.Exists(homeFolder));
        }
    }
}
