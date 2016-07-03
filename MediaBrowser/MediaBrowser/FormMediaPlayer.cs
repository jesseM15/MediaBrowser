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
    public partial class FormMediaPlayer : Form
    {
        private string filePath;
        private WMPLib.IWMPPlaylist playlist;

        public FormMediaPlayer(Video fileToPlay)
        {
            InitializeComponent();
            filePath = fileToPlay.FilePath;
            this.Text = fileToPlay.Title;
            playlist = wmpPlayer.playlistCollection.newPlaylist("Media Browser Playlist");
        }

        private void FormMediaPlayer_Load(object sender, EventArgs e)
        {
            wmpPlayer.URL = filePath;
            wmpPlayer.Ctlcontrols.play();
        }
    }
}
