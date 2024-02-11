using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ffmpeg.Library.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Ffmpeg.Library.Concrete.Tests
{
    [TestClass()]
    public class SubtitlesServiceTests
    {
        [TestMethod()]
        public void ExtractSubTitlesTest()
        {
            var service = new SubtitlesService();
            string videoFile = "TestData/jwbvod24_E_04_r240P.mp4";
            var results = service.ExtractSubTitles(videoFile).Result;

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Length > 0);

        }

        [TestMethod()]
        public void ExtractSubTitlesTest_DOWNLOAD()
        {
            var service = new SubtitlesService();
            string videoFile = "https://d34ji3l0qn3w2t.cloudfront.net/2138c762-4879-4053-8f59-4e792ca7b66c/1/pkon_E_032_r240P.mp4";
            var results = service.ExtractSubTitles(videoFile).Result;

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Length > 0);

        }
    }
}