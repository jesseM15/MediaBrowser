using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Drawing;

namespace MediaBrowser
{
    public class Browser
    {
        private string _state;
        private List<string> _sourceDirectories;
        private List<Video> _videos;

        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        public List<string> SourceDirectories
        {
            get { return _sourceDirectories; }
            set { _sourceDirectories = value; }
        }

        public List<Video> Videos
        {
            get { return _videos; }
            set { _videos = value; }
        }

        public Browser()
        {
            _state = "";
            _sourceDirectories = new List<string>();
            _videos = new List<Video>();
        }

        // creates the database/tables and populates Videos
        public void Initialize()
        {
            DB.CreateMediaBrowserDB();
            DB.CreateSourceDirectoryTable();
            DB.CreateVideoTable();
            SourceDirectories = DB.GetSourceDirectories();

            PopulateVideos(GetVideoFiles());

        }

        // shows FormSourceDirectories and adds/removes chosen directories
        public void ShowSourceDirectoryDialog()
        {
            FormSourceDirectories sd = new FormSourceDirectories(SourceDirectories);
            var result = sd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                AddSourceDirectories(sd.directoriesToAdd);
                RemoveSourceDirectories(sd.directoriesToRemove);
            }
        }

        // add a list of directories to the database and SourceDirectories property
        private void AddSourceDirectories(List<string> directoriesToAdd)
        {
            foreach (string directory in directoriesToAdd)
            {
                DB.AddSourceDirectory(directory);
                SourceDirectories.Add(directory);
            }
        }

        // removes a list of directories from the database and SourceDirectories property
        private void RemoveSourceDirectories(List<string> directoriesToRemove)
        {
            foreach (string directory in directoriesToRemove)
            {
                DB.RemoveSourceDirectory(directory);
                SourceDirectories.Remove(directory);
            }
        }

        // returns a list of file paths for every video in the source directories
        private List<string> GetVideoFiles()
        {
            List<string> filePaths = new List<string>();
            foreach (string directory in SourceDirectories)
            {
                filePaths.AddRange(Directory.GetFiles(directory));
            }
            return filePaths;
        }

        private void PopulateVideos(List<string> videoFiles)
        {
            // iterate through files and assign Media properties
            for (int n = 0; n < videoFiles.Count(); n++)
            {
                Video tempVideo = DB.GetVideoData(videoFiles[n]);
                if (tempVideo != null)
                {
                    tempVideo.MediaImage = new Bitmap(tempVideo.MediaImagePath);
                }
                else
                {
                    tempVideo = new Video();
                    tempVideo.FilePath = videoFiles[n];
                    tempVideo.FileName = Path.GetFileNameWithoutExtension(videoFiles[n]);

                    tempVideo.DownloadVideoData();
                }
                Videos.Add(tempVideo);
            }
        }

    }
}
