using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaBrowser
{
    interface IVideoData
    {
        Video GetVideo(string filePath);
    }
}
