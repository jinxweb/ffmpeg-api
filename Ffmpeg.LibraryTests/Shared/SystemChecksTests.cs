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
        public void IsFFMpegInstalledTest()
        {
            bool result = SystemChecks.IsFFMpegInstalled("c:/apps/ffmpeg-6.0/bin"); 

            Assert.IsTrue(result);
        }
    }
}