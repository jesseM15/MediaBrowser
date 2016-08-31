using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;

namespace MediaBrowser
{
    public static class ListViewWorker
    {
        public static ListView lvwMedia { get; set; }
        private static ImageList imageListSmall = new ImageList();
        private static ImageList imageListLarge = new ImageList();
        private static List<Video> currentVideos = new List<Video>();

        static ListViewWorker()
        {
            imageListSmall.ImageSize = new Size(20, 20);
            imageListLarge.ImageSize = new Size(50, 100);
        }

        public static void UpdateListView(List<Video> videos, string broad, string narrow = "")
        {
            lvwMedia.Items.Clear();
            imageListSmall.Images.Clear();
            imageListLarge.Images.Clear();
            currentVideos = GetCurrentVideos(videos, broad, narrow);
            for (int v = 0; v < currentVideos.Count; v++)
            {
                imageListSmall.Images.Add(currentVideos[v].MediaImage);
                imageListLarge.Images.Add(currentVideos[v].MediaImage);
                ListViewItem item = new ListViewItem();
                item.ImageIndex = v;
                item.Text = currentVideos[v].Title;
                lvwMedia.Items.Add(item);
                ListViewItem.ListViewSubItem subitem = new ListViewItem.ListViewSubItem();
                subitem.Text = currentVideos[v].Year;
                item.SubItems.Add(subitem);
                subitem = new ListViewItem.ListViewSubItem();
                subitem.Text = ListHelper.CreateCommaSeperatedString(currentVideos[v].Genre);
                item.SubItems.Add(subitem);
                subitem = new ListViewItem.ListViewSubItem();
                subitem.Text = currentVideos[v].Rating.ToString();
                item.SubItems.Add(subitem);
                subitem = new ListViewItem.ListViewSubItem();
                subitem.Text = currentVideos[v].Length;
                item.SubItems.Add(subitem);
            }
            lvwMedia.SmallImageList = imageListSmall;
            lvwMedia.LargeImageList = imageListLarge;
        }

        public static void CreateDetailViewColumns()
        {
            ColumnHeader videoHeader = new ColumnHeader();
            videoHeader.Text = "Video";
            videoHeader.Width = 150;
            lvwMedia.Columns.Add(videoHeader);
            ColumnHeader yearHeader = new ColumnHeader();
            yearHeader.Text = "Year";
            yearHeader.Width = 50;
            lvwMedia.Columns.Add(yearHeader);
            ColumnHeader genreHeader = new ColumnHeader();
            genreHeader.Text = "Genres";
            genreHeader.Width = 160;
            lvwMedia.Columns.Add(genreHeader);
            ColumnHeader ratingHeader = new ColumnHeader();
            ratingHeader.Text = "Rating";
            ratingHeader.Width = 50;
            lvwMedia.Columns.Add(ratingHeader);
            ColumnHeader lengthHeader = new ColumnHeader();
            lengthHeader.Text = "Run Time";
            lengthHeader.Width = 80;
            lvwMedia.Columns.Add(lengthHeader);
        }

        public static void SelectedIndexChanged(FormMediaBrowser FMB)
        {
            foreach (Video video in currentVideos)
            {
                if (lvwMedia.SelectedItems.Count == 0)
                {
                    FMB.videoInfoPanel.Hide();
                }
                if (lvwMedia.SelectedItems.Count > 0 && lvwMedia.SelectedItems[0].Text == video.Title)
                {
                    FMB.videoInfoPanel.Show();
                    FMB.videoInfoTitle.Text = video.Title;
                    FMB.videoInfoPoster.Image = video.MediaImage;
                    FMB.videoInfoYear.Text = "Year: " + video.Year;
                    FMB.videoInfoLength.Text = "Run Time: " + video.Length;
                    FMB.videoInfoRating.Text = "Rating: " + video.Rating;
                    FMB.videoInfoGenre.Text = "Genres: " + ListHelper.CreateCommaSeperatedString(video.Genre);
                    FMB.videoInfoDirector.Text = "Directors: " + ListHelper.CreateCommaSeperatedString(video.Director);
                    FMB.videoInfoWriter.Text = "Writers: " + ListHelper.CreateCommaSeperatedString(video.Writer);
                    FMB.videoInfoActor.Text = "Actors: " + ListHelper.CreateCommaSeperatedString(video.Actor);
                    FMB.videoInfoPlot.Text = video.Plot;
                    FMB.lastSelectedVideo = video;
                }
            }
        }

        private static List<Video> GetCurrentVideos(List<Video> videos, string broad, string narrow)
        {
            List<Video> currentVideos = new List<Video>();
            if (broad.Equals("All"))
            {
                List<Video> list =
                    (from video in videos
                     select video).ToList();
                currentVideos = list;
            }
            else if (broad.Equals("Year"))
            {
                List<Video> list =
                    (from video in videos
                     where video.Year.Equals(narrow)
                     select video).ToList();
                currentVideos = list;
            }
            else if (broad.Equals("Genre"))
            {
                List<Video> list =
                    (from video in videos
                     where video.Genre.Contains(narrow)
                     select video).ToList();
                currentVideos = list;
            }
            else if (broad.Equals("Director"))
            {
                List<Video> list =
                    (from video in videos
                     where video.Director.Contains(narrow)
                     select video).ToList();
                currentVideos = list;
            }
            else if (broad.Equals("Writer"))
            {
                List<Video> list =
                    (from video in videos
                     where video.Writer.Contains(narrow)
                     select video).ToList();
                currentVideos = list;
            }
            else if (broad.Equals("Actor"))
            {
                List<Video> list =
                    (from video in videos
                     where video.Actor.Contains(narrow)
                     select video).ToList();
                currentVideos = list;
            }
            else if (broad.Equals("Rating"))
            {
                float rating =
                    float.Parse(narrow,
                    System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                List<Video> list =
                    (from video in videos
                     where Math.Floor(video.Rating).Equals(rating)
                     select video).ToList();
                currentVideos = list;
            }
            return currentVideos;
        }


    }
}
