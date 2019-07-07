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

        [Fact]
        public void TestGetFullPathWithTilda() {
            string path = "~/Library/Logs/VisualStudio/";

            string fullpath = new PathHelper().GetFullpath(path);

            Assert.NotEmpty(fullpath);
            Assert.True(fullpath.Length > path.Length);
        }

        [Fact]
        public void TestGetFullPathWithoutTilda() {
            string path = "/Library/Logs/VisualStudio/";

            string fullpath = new PathHelper().GetFullpath(path);

            Assert.NotEmpty(fullpath);
            Assert.Equal(path, fullpath);
        }
    }
}
