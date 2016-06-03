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
    public partial class MediaBrowserForm : Form
    {
        public static Browser browser = new Browser();

        public MediaBrowserForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void sourceDirectoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SourceDirectoriesForm sd = new SourceDirectoriesForm();
            //sd.Show();
            //SourceDirectories sd = new SourceDirectories();
            //sd.ShowForm();
            browser.SD.ShowForm();
        }
    }
}
