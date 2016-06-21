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
        public List<Video> videosToResolve;

        public FormUnresolvedVideos(List<Video> unresolvedVideos)
        {
            InitializeComponent();
            videosToResolve = unresolvedVideos;
        }

        private void FormUnresolvedVideos_Load(object sender, EventArgs e)
        {
            foreach (Video video in videosToResolve)
            {
                lbxUnresolvedFiles.Items.Add(video.FileName);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lbxUnresolvedFiles.SelectedItem != null)
            {
                foreach (Video video in videosToResolve)
                {
                    if (lbxUnresolvedFiles.SelectedItem.Equals(video.FileName))
                    {
                        FormEditVideoData evd = new FormEditVideoData(video);
                        var result = evd.ShowDialog();
                        if (result == System.Windows.Forms.DialogResult.OK && evd.currentVideo.Title != null)
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
                            lbxUnresolvedFiles.Items.Remove(lbxUnresolvedFiles.SelectedItem);
                        }
                        break;
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
