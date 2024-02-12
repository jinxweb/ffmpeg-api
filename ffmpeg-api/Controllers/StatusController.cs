using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ffmpeg_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        public StatusController()
        {
                
        }

        /// <summary>   
        /// Gets the status of the API and the ffmpeg library.  Runs other system checks
        /// </summary>  
        /// <returns>status</returns>
        [HttpGet]
         public Dictionary<string, string> Get()
        {
            var status = new Dictionary<string, string>();

            status.Add("status", "ok");
            string ffmpegPath = Environment.GetEnvironmentVariable("FFMPEG_PATH") ?? "/usr/bin";
            
            bool ffmpegInstalled = Ffmpeg.Library.Shared.SystemChecks.IsFFMpegInstalled(ffmpegPath); 
            
            status.Add("ffmpeg", ffmpegInstalled ? "installed" : "not installed");

            return status;
        }
    }
}
