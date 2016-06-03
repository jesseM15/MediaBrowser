using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace MediaBrowser
{
    public class Video : Media
    {
        private string _title;
        private Bitmap _mediaImage;


        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public Bitmap MediaImage
        {
            get { return _mediaImage; }
            set { _mediaImage = value; }
        }

        public Video()
        {
            _title = "";
            _mediaImage = new Bitmap(100, 200);
        }
    }
}
