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
        public static Browser browser;
        public Video lastSelectedVideo { get; set; }
        public ListView mediaListView { get { return lvwMedia; } set { lvwMedia = value; } }
        public Panel videoInfoPanel { get { return pnlVideoInfo; } set { pnlVideoInfo = value; } }
        public Label videoInfoTitle { get { return lblTitle; } set { lblTitle = value; } }
        public PictureBox videoInfoPoster { get { return picPoster; } set { picPoster = value; } }
        public Label videoInfoYear { get { return lblYear; } set { lblYear = value; } }
        public Label videoInfoRating { get { return lblRating; } set { lblRating = value; } }
        public Label videoInfoLength { get { return lblLength; } set { lblLength = value; } }
        public RichTextBox videoInfoGenre { get { return rtxGenre; } set { rtxGenre = value; } }
        public RichTextBox videoInfoDirector { get { return rtxDirector; } set { rtxDirector = value; } }
        public RichTextBox videoInfoWriter { get { return rtxWriter; } set { rtxWriter = value; } }
        public RichTextBox videoInfoActor { get { return rtxActor; } set { rtxActor = value; } }
        public RichTextBox videoInfoPlot { get { return rtxPlot; } set { rtxPlot = value; } }

        public FormMediaBrowser(Browser _browser)
        {
            InitializeComponent();
            browser = _browser;
            browser.SourceDirectory.SourceDirectoriesUpdated += SourceDirectoriesUpdated;
            ListViewWorker.lvwMedia = lvwMedia;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeForm();
        }

        private void FormMediaBrowser_Shown(object sender, EventArgs e)
        {
            InitializeVideos();
        }

        private void FormMediaBrowser_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void InitializeForm()
        {
            browser.Initialize();
            lbxBroad.DataSource = browser.BroadCategories;
            ListViewWorker.CreateDetailViewColumns();
            lvwMedia.View = View.LargeIcon;
            lvwMedia.MultiSelect = false;
            pnlVideoInfo.Hide();
        }

        private void InitializeVideos()
        {
            List<string> videoFiles = browser.GetVideoFiles();
            sprGatheringVideoData.ProgressBar.Show();
            slbUnresolvedVideos.IsLink = false;
            if (DB.GetSourceDirectories().Count < 1)
            {
                slbUnresolvedVideos.Text = "No Source Directories Found!";
            }
            else
            {
                slbUnresolvedVideos.Text = "Gathering Video Data...";
            }
            bgwPopulateVideos.RunWorkerAsync(videoFiles);
        }

        private void BroadFilterSelected(string filter)
        {
            if (filter.Equals("All"))
            {
                lbxNarrow.DataSource = new List<Video>();
                ListViewWorker.UpdateListView(browser.Videos, "All");
            }
            else if (filter.Equals("Year"))
            {
                lbxNarrow.DataSource = DB.GetDistinctYears();
            }
            else if (filter.Equals("Genre"))
            {
                lbxNarrow.DataSource = DB.GetGenres();
            }
            else if (filter.Equals("Director"))
            {
                List<string> directors = DB.GetDirectors();
                if (directors.Count > 0)
                {
                    lbxNarrow.DataSource =
                    new BindingSource(ListHelper.OrderByLastNames(directors), null);
                    lbxNarrow.DisplayMember = "Key";
                    lbxNarrow.ValueMember = "Value";
                }
            }
            else if (filter.Equals("Writer"))
            {
                List<string> writers = DB.GetWriters();
                if (writers.Count > 0)
                {
                    lbxNarrow.DataSource =
                    new BindingSource(ListHelper.OrderByLastNames(writers), null);
                    lbxNarrow.DisplayMember = "Key";
                    lbxNarrow.ValueMember = "Value";
                }
            }
            else if (filter.Equals("Actor"))
            {
                List<string> actors = DB.GetActors();
                if (actors.Count > 0)
                {
                    lbxNarrow.DataSource =
                    new BindingSource(ListHelper.OrderByLastNames(actors), null);
                    lbxNarrow.DisplayMember = "Key";
                    lbxNarrow.ValueMember = "Value";
                }
            }
            else if (filter.Equals("Rating"))
            {
                List<float> ratings = new List<float>();
                for (float n = 1; n < 11; n++)
                {
                    ratings.Add(n);
                }
                lbxNarrow.DataSource = ratings;
            }
        }

        private void lbxBroad_SelectedIndexChanged(object sender, EventArgs e)
        {
            BroadFilterSelected(lbxBroad.SelectedItem.ToString());
        }

        private void lbxNarrow_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewWorker.UpdateListView(browser.Videos, lbxBroad.SelectedItem.ToString(), lbxNarrow.SelectedValue.ToString());
        }

        private void lvwMedia_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewWorker.SelectedIndexChanged(browser.FMB);
        }

        private void btnEditVideoData_Click(object sender, EventArgs e)
        {
            FormEditVideoData evd = new FormEditVideoData(lastSelectedVideo);
            var result = evd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                evd.currentVideo.SaveImage();
                DB.RemoveVideoGenre(evd.currentVideo.VideoID);
                DB.RemoveVideoDirector(evd.currentVideo.VideoID);
                DB.RemoveVideoWriter(evd.currentVideo.VideoID);
                DB.RemoveVideoActor(evd.currentVideo.VideoID);
                DB.RemoveUnusedGenres();
                DB.RemoveUnusedDirectors();
                DB.RemoveUnusedWriters();
                DB.RemoveUnusedActors();
                foreach (string genre in evd.currentVideo.Genre)
                {
                    DB.AddGenre(genre);
                    DB.AddVideoGenre(evd.currentVideo.VideoID, genre);
                }
                foreach (string director in evd.currentVideo.Director)
                {
                    DB.AddDirector(director);
                    DB.AddVideoDirector(evd.currentVideo.VideoID, director);
                }
                foreach (string writer in evd.currentVideo.Writer)
                {
                    DB.AddWriter(writer);
                    DB.AddVideoWriter(evd.currentVideo.VideoID, writer);
                }
                foreach (string actor in evd.currentVideo.Actor)
                {
                    DB.AddActor(actor);
                    DB.AddVideoActor(evd.currentVideo.VideoID, actor);
                }
                DB.UpdateVideo(evd.currentVideo);
                if (lbxBroad.SelectedItem.ToString().Equals("All"))
                {
                    ListViewWorker.UpdateListView(browser.Videos, "All");
                }
                else
                {
                    ListViewWorker.UpdateListView(browser.Videos, lbxBroad.SelectedItem.ToString(), lbxNarrow.SelectedItem.ToString());
                }
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            FormMediaPlayer mp = new FormMediaPlayer(lastSelectedVideo);
            mp.Show();
        }

        private void slbUnresolvedVideos_Click(object sender, EventArgs e)
        {
            if (slbUnresolvedVideos.IsLink == false)
            {
                return;
            }
            using (FormUnresolvedVideos uv = new FormUnresolvedVideos(DB.GetAllUnresolvedVideos()))
            {
                uv.ShowDialog();
                int unresolvedVideos = DB.GetAllUnresolvedVideos().Count;
                if (unresolvedVideos > 0)
                {
                    slbUnresolvedVideos.Text = unresolvedVideos.ToString() + " unresolved videos";
                }
            }
        }

        private void sourceDirectoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            browser.SourceDirectory.ShowSourceDirectoryDialog();
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
            if (lbxBroad.SelectedItems.Count > 0 && lbxNarrow.SelectedItems.Count > 0)
            {
                ListViewWorker.UpdateListView(browser.Videos, lbxBroad.SelectedItem.ToString(), lbxNarrow.SelectedValue.ToString());
            }
            else
            {
                BroadFilterSelected("All");
            }
            sprGatheringVideoData.ProgressBar.Hide();
            int unresolvedVideos = DB.GetAllUnresolvedVideos().Count;
            if (unresolvedVideos > 0)
            {
                slbUnresolvedVideos.Text = unresolvedVideos.ToString() + " unresolved videos";
                slbUnresolvedVideos.IsLink = true;
            }
        }

        private void SourceDirectoriesUpdated(bool updated)
        {
            browser.Videos.Clear();
            InitializeVideos();
            lbxBroad.SelectedItem = "";
            lbxNarrow.DataSource = new List<string>();
            lvwMedia.Clear();
            pnlVideoInfo.Hide();
        }

        

    }
}
