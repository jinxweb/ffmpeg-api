using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ffmpeg.Library.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ffmpeg.Library.Shared.Tests
{
    [TestClass()]
    public class SystemChecksTests
    {
        [TestMethod()]
        [TestCategory("Integration")]
        public void IsFFMpegInstalledTest()
        {
            bool result = SystemChecks.IsFFMpegInstalled("/usr/bin"); 

            Assert.IsTrue(result);
        }
    }
}