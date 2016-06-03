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

        
    }
}
