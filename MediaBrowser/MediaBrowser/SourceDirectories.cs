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
    public partial class SourceDirectories : Form
    {
        public SourceDirectories()
        {
            InitializeComponent();
        }

        private void SourceDirectories_Load(object sender, EventArgs e)
        {
            // add directories to listbox
            foreach (string directory in MediaBrowser.browser.SourceDirectories)
            {
                lbxSourceDirectories.Items.Add(FormatWithBackSlashes(directory));
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            if (fbd.SelectedPath != null)
            {
                lbxSourceDirectories.Items.Add(fbd.SelectedPath);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            lbxSourceDirectories.Items.Remove(lbxSourceDirectories.SelectedItem);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // code to return source directories
            MediaBrowser.browser.SourceDirectories = new List<string>();
            foreach (string item in lbxSourceDirectories.Items)
            {
                MediaBrowser.browser.SourceDirectories.Add(FormatWithForwardSlashes(item));
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // formats directory path to be saved and used to get files
        private string FormatWithForwardSlashes(string directory)
        {
            directory.Replace(@"\", @"/");
            directory += @"/";
            return directory;
        }

        // formats directory path to be displayed like FolderBrowserDialog
        private string FormatWithBackSlashes(string directory)
        {
            directory.Replace(@"/", @"\");
            directory = directory.Substring(0, directory.Length - 1);
            return directory;
        }
    }
}
