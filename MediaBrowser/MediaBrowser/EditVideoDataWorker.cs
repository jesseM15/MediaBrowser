using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace MediaBrowser
{
    public static class EditVideoDataWorker
    {
        public static void EditClicked(Video lastSelectedVideo, ListView lvwMedia, List<Video> videos, ListBox lbxBroad, ListBox lbxNarrow)
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
                    ListViewWorker.UpdateListView(lvwMedia, videos, "All");
                }
                else
                {
                    ListViewWorker.UpdateListView(lvwMedia, videos, lbxBroad.SelectedItem.ToString(), lbxNarrow.SelectedItem.ToString());
                }
            }
        }
    }
}
