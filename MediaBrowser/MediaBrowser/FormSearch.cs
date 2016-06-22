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
    public partial class FormSearch : Form
    {
        public Video searchVideo = new Video();

        public FormSearch(Video editVideo)
        {
            InitializeComponent();
            searchVideo = editVideo;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            searchVideo.DownloadVideoData(txtTitle.Text, txtYear.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
