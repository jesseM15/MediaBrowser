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
        private bool closePending;
        public Video lastSelectedVideo { get; set; }
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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeForm();
        }

        private void FormMediaBrowser_Shown(object sender, EventArgs e)
        {
            InitializeVideos();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (bgwPopulateVideos.IsBusy)
            {
                closePending = true;
                bgwPopulateVideos.CancelAsync();
                e.Cancel = true;
                this.Enabled = false;
                return;
            }
            base.OnFormClosing(e);
        }

        private void FormMediaBrowser_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void InitializeForm()
        {
            browser.Initialize();
            lbxBroad.DataSource = browser.BroadCategories;
            ListViewWorker.CreateDetailViewColumns(lvwMedia);
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

        private void lbxBroad_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBoxWorker. BroadFilterSelected(lbxBroad.SelectedItem.ToString(), lbxNarrow, lvwMedia, browser.Videos);
        }

        private void lbxNarrow_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewWorker.UpdateListView(lvwMedia, browser.Videos, lbxBroad.SelectedItem.ToString(), lbxNarrow.SelectedValue.ToString());
        }

        private void lvwMedia_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewWorker.SelectedIndexChanged(lvwMedia, browser.FMB);
        }

        private void btnEditVideoData_Click(object sender, EventArgs e)
        {
            EditVideoDataWorker.EditClicked(lastSelectedVideo, lvwMedia, browser.Videos, lbxBroad, lbxNarrow);
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
            AsyncWorker.DoWork(e, bgwPopulateVideos, browser);
        }

        private void bgwPopulateVideos_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            sprGatheringVideoData.Value = e.ProgressPercentage;
        }

        private void bgwPopulateVideos_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AsyncWorker.RunWorkerCompleted(this, closePending, lbxBroad, lbxNarrow, lvwMedia, browser, sprGatheringVideoData, slbUnresolvedVideos);
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
