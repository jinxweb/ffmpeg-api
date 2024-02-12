using Ffmpeg.Library.Abstract;
using Ffmpeg.Library.Shared;
using FFMpegCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ffmpeg.Library.Concrete
{
    public class SubtitlesService : ISubtitlesService
    {
        Logger<ISubtitlesService> _logger = null;

        public SubtitlesService(string pathToffmpeg ="/usr/bin", Logger<ISubtitlesService> logger = null)
        {
            GlobalFFOptions.Configure(options => options.BinaryFolder = pathToffmpeg);
            _logger = logger;
        }



        public async Task<string> ExtractSubTitles(string videoFilePath)
        {
            _logger?.LogInformation($"Extracting subtitles from {videoFilePath}");  

            string tmpDir = Environment.GetEnvironmentVariable("TMP") ?? "/tmp";

            if (!Directory.Exists(tmpDir))
            {
                Directory.CreateDirectory(tmpDir);
            }

            string file = videoFilePath;
            string subtitlesFilePath = Path.Combine(tmpDir, Guid.NewGuid().ToString() + ".srt");

            if (videoFilePath.ToLower().StartsWith("http"))
            {
                var fileDownload = new FileDownload(tmpDir, null);
                file = await fileDownload.DownloadFile(videoFilePath);
            }

            await FFMpegArguments
                .FromFileInput(file)
                .OutputToFile(subtitlesFilePath, false, options => options
                    .WithCustomArgument("-map 0:s:0") // This argument specifies the first subtitle stream found in the file.
                    .WithCustomArgument("-c:s text")) // This copies the subtitles as they are, without re-encoding.
                .ProcessAsynchronously();

            string subtitles = string.Empty;

            if (File.Exists(subtitlesFilePath))
            {
                subtitles = File.ReadAllText(subtitlesFilePath);
            }

            _logger?.LogInformation($"Subtitles extracted and saved as {subtitlesFilePath}");
            return subtitles;

        }
    }
}
