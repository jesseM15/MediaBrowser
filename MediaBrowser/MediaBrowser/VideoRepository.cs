using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace MediaBrowser
{
    class VideoRepository : IVideoData
    {
        public Video GetVideo(string filePath)
        {
            return GetVideoFromDB(filePath);
        }

        private Video GetVideoFromDB(string filePath)
        {
            Video tempVideo = new Video();
            try
            {
                tempVideo = DB.GetVideoData(filePath);
                if (tempVideo.MediaImagePath != "")
                {
                    tempVideo.MediaImage = new Bitmap(tempVideo.MediaImagePath);
                }
                tempVideo.Genre = DB.GetVideoGenres(tempVideo.VideoID);
                tempVideo.Director = DB.GetVideoDirectors(tempVideo.VideoID);
                tempVideo.Writer = DB.GetVideoWriters(tempVideo.VideoID);
                tempVideo.Actor = DB.GetVideoActors(tempVideo.VideoID);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString(), "VideoRepository.cs");
            }
            return tempVideo;
        }

    }
}
