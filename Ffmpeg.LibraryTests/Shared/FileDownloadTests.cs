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
    public class FileDownloadTests
    {
      

        [TestMethod()]
        public void DownloadFileTest()
        {
            FileDownload fileDownload = new FileDownload(Environment.GetEnvironmentVariable("TMP") ?? "./tmp", null);

            string urlToDownload = "https://d34ji3l0qn3w2t.cloudfront.net/2138c762-4879-4053-8f59-4e792ca7b66c/1/pkon_E_032_r240P.mp4";

            string downloadedFilePath = fileDownload.DownloadFile(urlToDownload).Result;

            Assert.IsTrue(File.Exists(downloadedFilePath));
            




        }
    }
}