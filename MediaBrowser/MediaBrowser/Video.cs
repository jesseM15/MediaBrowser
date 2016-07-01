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
        private int _videoID;
        private string _title;
        private string _year;
        private List<string> _genre;
        private List<string> _director;
        private List<string> _writer;
        private List<string> _actor;
        private Bitmap _mediaImage;
        private string _mediaImagePath;
        private string _length;
        private float _rating;
        private string _plot;

        public int VideoID
        {
            get { return _videoID; }
            set { _videoID = value; }
        }

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

        public List<string> Director
        {
            get { return _director; }
            set { _director = value; }
        }

        public List<string> Writer
        {
            get { return _writer; }
            set { _writer = value; }
        }

        public List<string> Actor
        {
            get { return _actor; }
            set { _actor = value; }
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

        public string Length
        {
            get { return _length; }
            set { _length = value; }
        }

        public float Rating
        {
            get { return _rating; }
            set { _rating = value; }
        }

        public string Plot
        {
            get { return _plot; }
            set { _plot = value; }
        }

        public Video()
        {
            _title = "";
            _year = "";
            _genre = new List<string>();
            _director = new List<string>();
            _writer = new List<string>();
            _actor = new List<string>();
            _mediaImage = new Bitmap(100, 200);
            _mediaImagePath = "";
            _length = "";
            _rating = 0;
            _plot = "";
        }

        public void DownloadVideoData(string searchTitle, string searchYear="")
        {
            try
            {
                Logger.Info("Media image not found for " + searchTitle + ". Attempting download...", "Video.cs");
                
                XDocument doc = QueryOMDBAPI(searchTitle, searchYear);
                string imageURL = doc.Root.Element("movie").Attribute("poster").Value;
                this.Title = doc.Root.Element("movie").Attribute("title").Value;
                string year = doc.Root.Element("movie").Attribute("year").Value;
                this.Year = year.Substring(0, 4);
                this.Genre = doc.Root.Element("movie").Attribute("genre").Value.Split(',').ToList();
                this.Genre = ListHelper.ListTrim(this.Genre);
                this.Director = doc.Root.Element("movie").Attribute("director").Value.Split(',').ToList();
                this.Director = ListHelper.ListTrim(this.Director);
                this.Writer = doc.Root.Element("movie").Attribute("writer").Value.Split(',').ToList();
                this.Writer = ListHelper.RemoveParentheses(this.Writer);
                this.Writer = ListHelper.ListTrim(this.Writer);
                this.Writer = this.Writer.Distinct().ToList();
                this.Actor = doc.Root.Element("movie").Attribute("actors").Value.Split(',').ToList();
                this.Actor = ListHelper.ListTrim(this.Actor);
                this.Length = doc.Root.Element("movie").Attribute("runtime").Value;
                string rating = doc.Root.Element("movie").Attribute("imdbRating").Value;
                this.Rating = float.Parse(rating, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                this.Plot = doc.Root.Element("movie").Attribute("plot").Value;
                this.Plot = this.Plot.Replace("&quot;", "\"");

                Bitmap poster = null;
                poster = new Bitmap(DownloadImage(imageURL));

                if (poster != null)
                {
                    this.MediaImage = poster;
                    SaveImage();
                    Logger.Info("Media image download successful for " + searchTitle + ".", "Video.cs");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString(), "Video.cs");
            }
        }

        private XDocument QueryOMDBAPI(string title, string year)
        {
            string requestURL = "http://www.omdbapi.com/?t=" + title + "&y=" + year + "&plot=full&r=xml";
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

        public void SaveImage()
        {
            try
            {
                this.MediaImagePath = Browser.PosterImagesPath + this.FileName+".jpg";
                if (File.Exists(this.MediaImagePath))
                {
                    File.Delete(this.MediaImagePath);
                }
                this.MediaImage.Save(this.MediaImagePath);
            }
            catch (Exception ex)
            {
                Logger.Error("Unable to save file: " + ex.Message, "Video.cs");
            }
        }

    }
}
