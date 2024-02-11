using FFMpegCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ffmpeg.Library.Shared
{
    public static class SystemChecks
    {
        public static bool IsFFMpegInstalled(string pathToffmpeg = "")
        {
            string ffmpegPath = Path.Combine(pathToffmpeg, "ffmpeg.exe");
            
            try
            {
                using (var process = new Process())
                {
                    process.StartInfo.FileName = ffmpegPath;
                    process.StartInfo.Arguments = "-version";
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    if (!string.IsNullOrEmpty(output) && output.Contains("ffmpeg version"))
                    {
                        Console.WriteLine("FFmpeg is installed.");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred while checking for FFmpeg: {ex.Message}");
            }

            return false;
        }
    }
}
