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
        ImageList imageList = new ImageList();

        public FormMediaBrowser()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            browser.Initialize();
            lbxBroad.DataSource = browser.BroadCategories;
            lvwMedia.View = View.LargeIcon;
            imageList.ImageSize = new Size(50, 100);
            lbxBroad.SetSelected(0,true);
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
                UpdateListView("All", "All");
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
            UpdateListView(lbxBroad.SelectedItem.ToString(), lbxNarrow.SelectedItem.ToString());
        }

        private void UpdateListView(string broad, string narrow)
        {
            lvwMedia.Items.Clear();
            imageList.Images.Clear();
            currentVideos = browser.GetCurrentVideos(broad, narrow);
            for (int v = 0; v < currentVideos.Count; v++)
            {
                imageList.Images.Add(currentVideos[v].MediaImage);
                ListViewItem item = new ListViewItem();
                item.ImageIndex = v;
                item.Text = currentVideos[v].Title;
                lvwMedia.Items.Add(item);
            }
            lvwMedia.LargeImageList = imageList;
        }
    }
}
