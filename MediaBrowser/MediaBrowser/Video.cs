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
        private string _year;
        private List<string> _genre;
        private Bitmap _mediaImage;
        private string _mediaImagePath;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Year
        {
            get { return _year; }
            set { _year = value; }
        }

        public List<string> Genre
        {
            get { return _genre; }
            set { _genre = value; }
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
            _year = "";
            _genre = new List<string>();
            _mediaImage = new Bitmap(100, 200);
            _mediaImagePath = "";
        }

        public void DownloadVideoData()
        {
            try
            {
                Logger.Info("Media image not found for " + this.FileName + ". Attempting download...", "Video.cs");

                XDocument doc = QueryOMDBAPI(this.FileName, this.Year);
                string imageURL = doc.Root.Element("movie").Attribute("poster").Value;
                this.Title = doc.Root.Element("movie").Attribute("title").Value;
                string year = doc.Root.Element("movie").Attribute("year").Value;
                this.Year = year.Substring(0, 4);
                this.Genre = doc.Root.Element("movie").Attribute("genre").Value.Split(',').ToList();

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

        private XDocument QueryOMDBAPI(string title, string year)
        {
            string requestURL = "http://www.omdbapi.com/?t=" + title + "&y=" + year + "&plot=short&r=xml";
            WebClient wc = new WebClient();
            return XDocument.Parse(wc.DownloadString(requestURL));
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
            try
            {
                this.MediaImagePath = Browser.PosterImagesPath + this.FileName+".jpg";
                this.MediaImage.Save(this.MediaImagePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save file " + ex.Message);
            }
        }

    }
}
