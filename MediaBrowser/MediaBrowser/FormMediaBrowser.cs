using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaBrowser
{
    public partial class FormMediaBrowser : Form
    {
        public static Browser browser = new Browser();
        private List<Video> currentVideos = new List<Video>();
        ImageList imageListSmall = new ImageList();
        ImageList imageListLarge = new ImageList();

        public FormMediaBrowser()
        {
            InitializeComponent();
            browser.SourceDirectoriesUpdated += SourceDirectoriesUpdated;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeForm();
        }

        private void FormMediaBrowser_Shown(object sender, EventArgs e)
        {
            InitializeVideos();
        }

        private void InitializeForm()
        {
            browser.Initialize();
            lbxBroad.DataSource = browser.BroadCategories;
            CreateDetailViewColumns();
            lvwMedia.View = View.LargeIcon;
            lvwMedia.MultiSelect = false;
            imageListSmall.ImageSize = new Size(20,20);
            imageListLarge.ImageSize = new Size(50, 100);
            lbxBroad.SetSelected(0, true);
            pnlVideoInfo.Hide();
        }

        private void InitializeVideos()
        {
            List<string> videoFiles = browser.GetVideoFiles();
            sprGatheringVideoData.ProgressBar.Show();
            slbUnresolvedVideos.IsLink = false;
            slbUnresolvedVideos.Text = "Gathering Video Data...";
            bgwPopulateVideos.RunWorkerAsync(videoFiles);
        }

        private void sourceDirectoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            browser.ShowSourceDirectoryDialog();
        }

        private void lbxBroad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxBroad.SelectedItem.Equals("All"))
            {
                lbxNarrow.DataSource = new List<Video>();
                UpdateListView("All");
            }
            else if (lbxBroad.SelectedItem.Equals("Year"))
            {
                lbxNarrow.DataSource = DB.GetDistinctYears();
            }
            else if (lbxBroad.SelectedItem.Equals("Genre"))
            {
                lbxNarrow.DataSource = DB.GetDistinctGenres();
            }
            else if (lbxBroad.SelectedItem.Equals("Director"))
            {
                lbxNarrow.DataSource = 
                    new BindingSource(ListHelper.OrderByLastNames(DB.GetDistinctDirectors()), null);
                lbxNarrow.DisplayMember = "Key";
                lbxNarrow.ValueMember = "Value";
            }
            else if (lbxBroad.SelectedItem.Equals("Writer"))
            {
                lbxNarrow.DataSource = 
                    new BindingSource(ListHelper.OrderByLastNames(DB.GetDistinctWriters()), null);
                lbxNarrow.DisplayMember = "Key";
                lbxNarrow.ValueMember = "Value";
            }
            else if (lbxBroad.SelectedItem.Equals("Actor"))
            {
                lbxNarrow.DataSource = 
                    new BindingSource(ListHelper.OrderByLastNames(DB.GetDistinctActors()), null);
                lbxNarrow.DisplayMember = "Key";
                lbxNarrow.ValueMember = "Value";
            }
            else if (lbxBroad.SelectedItem.Equals("Rating"))
            {
                //lbxNarrow.DataSource = DB.GetDistinctRatings();
                List<float> ratings = new List<float>();
                for (float n = 1; n < 11; n++)
                {
                    ratings.Add(n);
                }
                lbxNarrow.DataSource = ratings;
            }
        }

        private void lbxNarrow_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateListView(lbxBroad.SelectedItem.ToString(), lbxNarrow.SelectedValue.ToString());
        }

        private void UpdateListView(string broad, string narrow="")
        {
            lvwMedia.Items.Clear();
            imageListLarge.Images.Clear();
            currentVideos = browser.GetCurrentVideos(broad, narrow);
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

        private void CreateDetailViewColumns()
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

        private void lvwMedia_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Video video in currentVideos)
            {
                if (lvwMedia.SelectedItems.Count == 0)
                {
                    pnlVideoInfo.Hide();
                }
                if (lvwMedia.SelectedItems.Count > 0 && lvwMedia.SelectedItems[0].Text == video.Title)
                {
                    pnlVideoInfo.Show();
                    lblTitle.Text = video.Title;
                    picPoster.Image = video.MediaImage;
                    lblYear.Text = "Year: " + video.Year;
                    lblLength.Text = "Run Time: " + video.Length;
                    lblRating.Text = "Rating: " + video.Rating;
                    rtxGenre.Text = "Genres: " + ListHelper.CreateCommaSeperatedString(video.Genre);
                    rtxDirector.Text = "Directors: " + ListHelper.CreateCommaSeperatedString(video.Director);
                    rtxWriter.Text = "Writers: " + ListHelper.CreateCommaSeperatedString(video.Writer);
                    rtxActor.Text = "Actors: " + ListHelper.CreateCommaSeperatedString(video.Actor);
                    rtxPlot.Text = video.Plot;
                }
            }
        }

        private void btnEditVideoData_Click(object sender, EventArgs e)
        {
            foreach (Video video in currentVideos)
            {
                if (lvwMedia.SelectedItems[0].Text == video.Title)
                {
                    int index = lvwMedia.SelectedItems[0].Index;
                    FormEditVideoData evd = new FormEditVideoData(video);
                    var result = evd.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        evd.currentVideo.SaveImage();
                        DB.RemoveGenre(evd.currentVideo.VideoID);
                        DB.RemoveDirector(evd.currentVideo.VideoID);
                        DB.RemoveWriter(evd.currentVideo.VideoID);
                        DB.RemoveActor(evd.currentVideo.VideoID);
                        foreach (string genre in evd.currentVideo.Genre)
                        {
                            DB.AddGenre(genre, evd.currentVideo.VideoID);
                        }
                        foreach (string director in evd.currentVideo.Director)
                        {
                            DB.AddDirector(director, evd.currentVideo.VideoID);
                        }
                        foreach (string writer in evd.currentVideo.Writer)
                        {
                            DB.AddWriter(writer, evd.currentVideo.VideoID);
                        }
                        foreach (string actor in evd.currentVideo.Actor)
                        {
                            DB.AddActor(actor, evd.currentVideo.VideoID);
                        }
                        DB.UpdateVideo(evd.currentVideo);
                        if (lbxBroad.SelectedItem.ToString().Equals("All"))
                        {
                            UpdateListView("All");
                        }
                        else
                        {
                            UpdateListView(lbxBroad.SelectedItem.ToString(), lbxNarrow.SelectedItem.ToString());
                        }
                        lvwMedia.Items[index].Selected = true;
                        lvwMedia.Select();
                        break;
                    }
                }
            }
        }

        private void slbUnresolvedVideos_Click(object sender, EventArgs e)
        {
            FormUnresolvedVideos uv = new FormUnresolvedVideos(DB.GetAllUnresolvedVideos());
            uv.Show();
        }

        private void largeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvwMedia.View = View.LargeIcon;
        }

        private void smallIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvwMedia.View = View.SmallIcon;
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvwMedia.View = View.List;
        }

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvwMedia.View = View.Details;
        }        

        private void bgwPopulateVideos_DoWork(object sender, DoWorkEventArgs e)
        {
            List<string> videoFiles = (List<string>)e.Argument;
            for (int n = 0; n < videoFiles.Count; n++)
            {
                browser.PopulateVideo(videoFiles[n]);
                double progress = (double)n / videoFiles.Count * 100;
                bgwPopulateVideos.ReportProgress((int)progress);
            }
        }

        private void bgwPopulateVideos_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            sprGatheringVideoData.Value = e.ProgressPercentage;
        }

        private void bgwPopulateVideos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            sprGatheringVideoData.ProgressBar.Hide();
            slbUnresolvedVideos.IsLink = true;
            slbUnresolvedVideos.Text = DB.GetAllUnresolvedVideos().Count.ToString() +
                " unresolved videos";
        }

        private void SourceDirectoriesUpdated(bool updated)
        {
            InitializeVideos();
        }


    }
}
