using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

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

        public void Initialize()
        {
            List<string> videoFiles = new List<string>();
            // add full-path files to the list
            foreach (string directory in _sourceDirectories)
            {
                videoFiles.AddRange(Directory.GetFiles(directory));
            }

            // Hard-coded GetFiles for reference. Delete when tested working.
            //videoFiles.AddRange(Directory.GetFiles(@"E:/Videos/Movies/"));

            // iterate through files and assign Media properties
            for (int n = 0; n < videoFiles.Count(); n++)
            {
                Video tempvideo = new Video();
                tempvideo.FilePath = videoFiles[n];
                tempvideo.FileName = Path.GetFileNameWithoutExtension(videoFiles[n]);

            }
        }

        public void ShowSourceDirectoryDialog()
        {
            FormSourceDirectories sd = new FormSourceDirectories(SourceDirectories);
            var result = sd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                SourceDirectories = sd.directories;
            }
        }
    }
}
