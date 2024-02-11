using Ffmpeg.Library.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ffmpeg_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtractSubTitlesController : ControllerBase
    {
        ISubtitlesService _subtitlesService = null;

        public ExtractSubTitlesController(ISubtitlesService subtitlesService)
        {
                _subtitlesService = subtitlesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string videoFile)
        {
            if (string.IsNullOrEmpty(videoFile))
            {
                return BadRequest("videoFile is required");
            }

            try
            {
                var subtitles = await _subtitlesService.ExtractSubTitles(videoFile);
                return Ok(subtitles);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
