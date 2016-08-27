using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace MediaBrowser
{
    public class SourceDirectory
    {
        public List<string> SourceDirectories { get; set; }
        public List<Video> Videos { get; set; }

        public event Action<bool> SourceDirectoriesUpdated;

        private void OnSourceDirectoryUpdate(bool updated)
        {
            var eh = SourceDirectoriesUpdated;
            if (eh != null)
            {
                eh(updated);
            }
        }

        public SourceDirectory()
        {
            Videos = new List<Video>();
            SourceDirectories = new List<string>();
        }

        // shows FormSourceDirectories and adds/removes chosen directories
        public void ShowSourceDirectoryDialog()
        {
            FormSourceDirectories sd = new FormSourceDirectories(SourceDirectories);
            var result = sd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                RemoveSourceDirectories(sd.directoriesToRemove);
                AddSourceDirectories(sd.directoriesToAdd);
            }
        }

        // removes a list of directories from the database and SourceDirectories property
        private void RemoveSourceDirectories(List<string> directoriesToRemove)
        {
            List<string> filePaths = new List<string>();
            foreach (string directory in directoriesToRemove)
            {
                filePaths.AddRange(Directory.GetFiles(directory));
                string[] subdirectories = Directory.GetDirectories(directory);
                foreach (string subdirectory in subdirectories)
                {
                    filePaths.AddRange(Directory.GetFiles(subdirectory));
                }
                foreach (string filePath in filePaths)
                {
                    int index = DB.GetVideoID(filePath);
                    DB.RemoveVideoGenre(index);
                    DB.RemoveVideoDirector(index);
                    DB.RemoveVideoWriter(index);
                    DB.RemoveVideoActor(index);
                    DB.RemoveVideo(index);
                }
                DB.RemoveUnusedGenres();
                DB.RemoveUnusedDirectors();
                DB.RemoveUnusedWriters();
                DB.RemoveUnusedActors();
                DB.RemoveSourceDirectory(directory);
                SourceDirectories.Remove(directory);
            }
        }

        // add a list of directories to the database and SourceDirectories property
        private void AddSourceDirectories(List<string> directoriesToAdd)
        {
            foreach (string directory in directoriesToAdd)
            {
                DB.AddSourceDirectory(directory);
                SourceDirectories.Add(directory);
            }
            OnSourceDirectoryUpdate(true);
        }
    }
}
