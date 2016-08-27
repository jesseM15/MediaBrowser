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
        public int VideoID { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public List<string> Genre { get; set; }
        public List<string> Director { get; set; }
        public List<string> Writer { get; set; }
        public List<string> Actor { get; set; }
        public Bitmap MediaImage { get; set; }
        public string MediaImagePath { get; set; }
        public string Length { get; set; }
        public float Rating { get; set; }
        public string Plot { get; set; }

        public Video()
        {
            Title = "";
            Year = "";
            Genre = new List<string>();
            Director = new List<string>();
            Writer = new List<string>();
            Actor = new List<string>();
            MediaImage = Properties.Resources.default_movie;
            MediaImagePath = "";
            Length = "";
            Rating = 0;
            Plot = "";
        }

        public void DownloadVideoData(string searchTitle, string searchYear="")
        {
            try
            {
                Logger.Info("Media image not found for " + searchTitle + ". Attempting download...", "Video.cs");
                
                XDocument doc = QueryOMDBAPI(searchTitle, searchYear);
                if (doc != null)
                {
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

                    Bitmap poster = new Bitmap(DownloadImage(imageURL));
                    if (poster != null)
                    {
                        this.MediaImage = poster;
                        Logger.Info("Media image download successful for " + searchTitle + ".", "Video.cs");
                    }
                    SaveImage();
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
