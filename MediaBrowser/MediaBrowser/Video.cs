using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Net;
using System.Xml.Linq;
using System.IO;
using System.Windows.Forms;

namespace MediaBrowser
{
    public class Video : Media
    {
        private string _title;
        private Bitmap _mediaImage;
        private string _mediaImagePath;

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

        public string MediaImagePath
        {
            get { return _mediaImagePath; }
            set { _mediaImagePath = value; }
        }

        public Video()
        {
            _title = "";
            _mediaImage = new Bitmap(100, 200);
            _mediaImagePath = "";
        }

        public void DownloadVideoData()
        {
            try
            {
                Logger.Info("Media image not found for " + this.FileName + ". Attempting download...", "Video.cs");

                string requestURL = "http://www.omdbapi.com/?t=" + this.FileName + "&plot=short&r=xml";
                WebClient wc = new WebClient();
                XDocument doc = XDocument.Parse(wc.DownloadString(requestURL));
                string imageURL = doc.Root.Element("movie").Attribute("poster").Value;
                this.Title = doc.Root.Element("movie").Attribute("title").Value;

                Bitmap poster = null;
                poster = new Bitmap(DownloadImage(imageURL));

                if (poster != null)
                {
                    this.MediaImage = poster;
                    SaveImage();
                    Logger.Info("Media image download successful for " + this.FileName + ".", "Video.cs");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString(), "Video.cs");
            }
        }

        private Image DownloadImage(string URL)
        {
            Image tempImage = null;
            try
            {
                System.Net.HttpWebRequest httpWebRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                httpWebRequest.AllowWriteStreamBuffering = true;
                httpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
                httpWebRequest.Referer = "http://www.google.com/";
                httpWebRequest.Timeout = 20000;
                System.Net.WebResponse webResponse = httpWebRequest.GetResponse();
                System.IO.Stream webStream = webResponse.GetResponseStream();
                tempImage = Image.FromStream(webStream);
                webResponse.Close();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString(), "Video.cs");
                return null;
            }
            return tempImage;
        }

        private void SaveImage()
        {
            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\PosterImages\";
            if (Directory.Exists(appPath) == false)
            {
                Directory.CreateDirectory(appPath);
            }
            try
            {
                this.MediaImagePath = appPath + this.FileName+".jpg";
                this.MediaImage.Save(this.MediaImagePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save file " + ex.Message);
            }
        }

    }
}
