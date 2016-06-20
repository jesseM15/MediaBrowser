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
    public partial class FormUnresolvedVideos : Form
    {
        public List<Video> videos;

        public FormUnresolvedVideos(List<Video> unresolvedVideos)
        {
            InitializeComponent();
            videos = unresolvedVideos;
        }

        private void FormUnresolvedVideos_Load(object sender, EventArgs e)
        {
            foreach (Video video in videos)
            {
                lbxUnresolvedFiles.Items.Add(video.FileName);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Video video = new Video();
            video.FilePath = lbxUnresolvedFiles.SelectedItem.ToString();
            video.DownloadVideoData(txtTitle.Text);
            picPoster.Image = video.MediaImage;
        }
    }
}
