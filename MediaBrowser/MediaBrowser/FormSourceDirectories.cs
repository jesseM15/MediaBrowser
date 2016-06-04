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
    public partial class FormSourceDirectories : Form
    {
        public List<string> currentDirectories;
        public List<string> directoriesToAdd;
        public List<string> directoriesToRemove;

        public FormSourceDirectories(List<string> sourceDirectories)
        {
            InitializeComponent();
            currentDirectories = sourceDirectories;
            directoriesToAdd = new List<string>();
            directoriesToRemove = new List<string>();
        }

        private void SourceDirectories_Load(object sender, EventArgs e)
        {
            // add directories to listbox
            foreach (string directory in currentDirectories)
            {
                lbxSourceDirectories.Items.Add(directory);
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
            UpdateDirectories();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // code to return directoriesToAdd and directoriesToRemove
        private void UpdateDirectories()
        {
            // adds all the items in the listbox that are not in currentDirectories
            foreach (string item in lbxSourceDirectories.Items)
            {
                bool present = false;
                foreach (string directory in currentDirectories)
                {
                    if (item.Equals(directory))
                    {
                        present = true;
                    }
                }
                if (!present)
                {
                    directoriesToAdd.Add(item);
                }
            }
            // adds all the directories in currentDirectories that are not in listbox
            foreach (string directory in currentDirectories)
            {
                bool present = false;
                foreach (string item in lbxSourceDirectories.Items)
                {
                    if (directory.Equals(item))
                    {
                        present = true;
                    }
                }
                if (!present)
                {
                    directoriesToRemove.Add(directory);
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
