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

        public FormMediaBrowser()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            browser.Initialize();
            lbxBroad.DataSource = browser.BroadCategories;
            lvwMedia.View = View.List;
        }

        private void sourceDirectoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            browser.ShowSourceDirectoryDialog();
        }

        private void lbxBroad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxBroad.SelectedItem.Equals("All"))
            {
                lvwMedia.Items.Clear();
                foreach (Video video in browser.Videos)
                {
                    lvwMedia.Items.Add(video.Title);
                }
            }
            else if (lbxBroad.SelectedItem.Equals("Year"))
            {
                lbxNarrow.DataSource = DB.GetDistinctYears();
            }
            else if (lbxBroad.SelectedItem.Equals("Genre"))
            {
                lbxNarrow.DataSource = DB.GetDistinctGenres();
            }
        }

        private void lbxNarrow_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxBroad.SelectedItem.Equals("Year"))
            {
                currentVideos = DB.GetVideosByYear(lbxNarrow.SelectedItem.ToString());
                lvwMedia.Items.Clear();
                foreach (Video video in currentVideos)
                {
                    lvwMedia.Items.Add(video.Title);
                }
            }
            else if (lbxBroad.SelectedItem.Equals("Genre"))
            {
                lvwMedia.Items.Clear();
                currentVideos.Clear();
                List<int> videoIDs = DB.GetVideoIDsByGenre(lbxNarrow.SelectedItem.ToString());
                foreach (int id in videoIDs)
                {
                    currentVideos.Add(DB.GetVideoData(id));
                }
                foreach (Video video in currentVideos)
                {
                    lvwMedia.Items.Add(video.Title);
                }
            }
        }
    }
}
