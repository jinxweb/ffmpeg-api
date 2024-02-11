using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ffmpeg.Library.Abstract
{
    public interface ISubtitlesService
    {
        Task<string> ExtractSubTitles(string filePath);
    }
}
