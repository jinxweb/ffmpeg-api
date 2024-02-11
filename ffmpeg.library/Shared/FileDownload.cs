using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ffmpeg.Library.Shared
{
    public class FileDownload
    {
        public long MaxFileSizeAllowed { get; set; } = 104857600;

        string _targetDirectory = string.Empty;

        ILogger<FileDownload>? _logger = null;



        public FileDownload(string downloadPath, ILogger<FileDownload>? logger)
        {
            _targetDirectory = downloadPath;
            _logger = logger;
        }

        public async Task<string> DownloadFile(string fileUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                // Send a HEAD request to get the file headers
                var request = new HttpRequestMessage(HttpMethod.Head, fileUrl);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                // Retrieve the Content-Length header
                long? contentLength = response.Content.Headers.ContentLength;

                // Check if the Content-Length exceeds 100MB (104857600 bytes)
                if (contentLength.HasValue && contentLength.Value > this.MaxFileSizeAllowed)
                {
                    throw new Exception("File size exceeds the 100MB limit.");
                }
                // Download the file data
                byte[] fileData = await client.GetByteArrayAsync(fileUrl);

                // Extract the file extension
                string fileExtension = Path.GetExtension(fileUrl);
                // Generate a new file name using a GUID, while keeping the original extension
                string newFileName = Guid.NewGuid().ToString() + fileExtension;
                string fullPath = Path.Combine(_targetDirectory, newFileName);

                // Save the file to the target directory with the new name
                await File.WriteAllBytesAsync(fullPath, fileData);

                _logger?.Log(LogLevel.Information, $"File downloaded and saved as {fullPath}");

                return fullPath;
            }


        }
    }
}
