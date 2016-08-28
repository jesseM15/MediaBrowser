using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace MediaBrowser
{
    class VideoOMDB : IVideoData
    {
        public Video GetVideo(string filePath)
        {
            return GetVideoFromOMDB(filePath);
        }

        private Video GetVideoFromOMDB(string filePath)
        {
            Video tempVideo = new Video();
            tempVideo.FilePath = filePath;
            tempVideo.FileName = Path.GetFileNameWithoutExtension(filePath);

            tempVideo.DownloadVideoData(tempVideo.FileName);
            int primaryKey = DB.AddVideo(tempVideo);
            foreach (string genre in tempVideo.Genre)
            {
                DB.AddGenre(genre);
                DB.AddVideoGenre(primaryKey, genre);
            }
            foreach (string director in tempVideo.Director)
            {
                DB.AddDirector(director);
                DB.AddVideoDirector(primaryKey, director);
            }
            foreach (string writer in tempVideo.Writer)
            {
                DB.AddWriter(writer);
                DB.AddVideoWriter(primaryKey, writer);
            }
            foreach (string actor in tempVideo.Actor)
            {
                DB.AddActor(actor);
                DB.AddVideoActor(primaryKey, actor);
            }
            return tempVideo;
        }
    }
}
