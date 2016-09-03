using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Windows.Forms;

namespace MediaBrowser
{
    public static class AsyncWorker
    {
        public static void DoWork(DoWorkEventArgs e, BackgroundWorker bgwPopulateVideos, Browser browser)
        {
            List<string> videoFiles = (List<string>)e.Argument;
            for (int n = 0; n < videoFiles.Count; n++)
            {
                if (bgwPopulateVideos.CancellationPending)
                {
                    break;
                }
                browser.PopulateVideo(videoFiles[n]);
                double progress = (double)n / videoFiles.Count * 100;
                bgwPopulateVideos.ReportProgress((int)progress);
            }
        }

        public static void RunWorkerCompleted(FormMediaBrowser FMB, bool closePending, 
            ListBox lbxBroad, ListBox lbxNarrow, ListView lvwMedia, Browser browser, 
            ToolStripProgressBar sprGatheringVideoData, ToolStripStatusLabel slbUnresolvedVideos)
        {
            if (closePending)
            {
                FMB.Close();
                return;
            }
            if (lbxBroad.SelectedItems.Count > 0 && lbxNarrow.SelectedItems.Count > 0)
            {
                ListViewWorker.UpdateListView(lvwMedia, browser.Videos, lbxBroad.SelectedItem.ToString(), lbxNarrow.SelectedValue.ToString());
            }
            else
            {
                ListBoxWorker.BroadFilterSelected("All", lbxNarrow, lvwMedia, browser.Videos);
            }
            sprGatheringVideoData.ProgressBar.Hide();
            sprGatheringVideoData.ProgressBar.Value = 0;
            int unresolvedVideos = DB.GetAllUnresolvedVideos().Count;
            if (unresolvedVideos > 0)
            {
                slbUnresolvedVideos.Text = unresolvedVideos.ToString() + " unresolved videos";
                slbUnresolvedVideos.IsLink = true;
            }
        }
    }
}
