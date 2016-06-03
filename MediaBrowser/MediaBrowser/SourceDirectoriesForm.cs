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
    public partial class SourceDirectoriesForm : Form
    {
        public List<string> sd;

        public SourceDirectoriesForm(List<string> directories)
        {
            InitializeComponent();
            List<string> sd = new List<string>();
            sd = directories;
        }

        private void SourceDirectories_Load(object sender, EventArgs e)
        {
            // add directories to listbox
            //foreach (string directory in MediaBrowserForm.browser.SourceDirectories)
            foreach (string directory in sd)
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
            //MediaBrowserForm.browser.SourceDirectories = new List<string>();
            /*
            MediaBrowserForm.browser.SD.Directories = new List<string>();
            foreach (string item in lbxSourceDirectories.Items)
            {
                //MediaBrowserForm.browser.SourceDirectories.Add(FormatWithForwardSlashes(item));
                MediaBrowserForm.browser.SD.Directories.Add(FormatWithForwardSlashes(item));
            }
            this.Close();
            */
            List<string> activeDirectories = new List<string>();
            foreach (string item in lbxSourceDirectories.Items)
            {
                activeDirectories.Add(item);
            }
            this.DialogResult = DialogResult.OK;
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
