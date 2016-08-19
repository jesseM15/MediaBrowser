using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace MediaBrowser
{
    public class Browser
    {
        private string _state;
        private List<string> _sourceDirectories;
        private List<Video> _videos;
        private List<string> _broadCategories;
        private static string _posterImagesPath;

        public event Action<bool> SourceDirectoriesUpdated;

        private void OnSourceDirectoryUpdate(bool updated)
        {
            var eh = SourceDirectoriesUpdated;
            if (eh != null)
            {
                eh(updated);
            }
        }

        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        public List<string> SourceDirectories
        {
            get { return _sourceDirectories; }
            set { _sourceDirectories = value; }
        }

        public List<Video> Videos
        {
            get { return _videos; }
            set { _videos = value; }
        }

        public List<string> BroadCategories
        {
            get { return _broadCategories; }
            set { _broadCategories = value; }
        }

        public static string PosterImagesPath
        {
            get { return _posterImagesPath; }
            set { _posterImagesPath = value; }
        }

        public Browser()
        {
            _state = "";
            _sourceDirectories = new List<string>();
            _videos = new List<Video>();
            _broadCategories = new List<string>();
            _posterImagesPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\PosterImages\";
        }

        // creates the database/tables and populates Videos
        public void Initialize()
        {
            DB.CreateMediaBrowserDB();
            DB.CreateSourceDirectoryTable();
            DB.CreateVideoTable();
            DB.CreateGenreTable();
            DB.CreateVideoGenreTable();
            DB.CreateDirectorTable();
            DB.CreateVideoDirectorTable();
            DB.CreateWriterTable();
            DB.CreateVideoWriterTable();
            DB.CreateActorTable();
            DB.CreateVideoActorTable();
            SourceDirectories = DB.GetSourceDirectories();

            CreatePosterImagesDirectory();
            SetBroadCategories();
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
                foreach (string filePath in filePaths)
                {
                    int index = DB.GetVideoID(filePath);
                    DB.RemoveVideoGenre(index);
                    DB.RemoveVideoDirector(index);
                    DB.RemoveVideoWriter(index);
                    DB.RemoveVideoActor(index);
                    DB.RemoveVideo(index);
                    for (int n = 0; n < Videos.Count; n++)
                    {
                        if (Videos[n].VideoID == index)
                        {
                            Videos.RemoveAt(n);
                        }
                    }
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

        // returns a list of file paths for every video in the source directories
        public List<string> GetVideoFiles()
        {
            List<string> filePaths = new List<string>();
            foreach (string directory in SourceDirectories)
            {
                filePaths.AddRange(Directory.GetFiles(directory));
                string[] subdirectories = Directory.GetDirectories(directory);
                foreach (string subdirectory in subdirectories)
                {
                    filePaths.AddRange(Directory.GetFiles(subdirectory));
                }
            }
            List<string> allowedExtensions = new List<string>
            {".avi", ".flv", ".mpeg", ".mp4", ".m4v", ".mkv", ".mov", ".mp4", ".mts", ".wmv"};
            for (int n = 0; n < filePaths.Count(); n++)
            {
                if (!allowedExtensions.Contains(Path.GetExtension(filePaths[n]).ToLower()))
                {
                    filePaths.RemoveAt(n);
                }
            }
            return filePaths;
        }

        public void PopulateVideo(string filePath)
        {
            Video tempVideo = DB.GetVideoData(filePath);
            if (tempVideo != null)
            {
                // Database already contains video
                try
                {
                    if (tempVideo.MediaImagePath != "")
                    {
                        tempVideo.MediaImage = new Bitmap(tempVideo.MediaImagePath);
                    }
                    tempVideo.Genre = DB.GetVideoGenres(tempVideo.VideoID);
                    tempVideo.Director = DB.GetVideoDirectors(tempVideo.VideoID);
                    tempVideo.Writer = DB.GetVideoWriters(tempVideo.VideoID);
                    tempVideo.Actor = DB.GetVideoActors(tempVideo.VideoID);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.ToString() + "FOR: " + tempVideo.Title, "Browser.cs");
                }
            }
            else
            {
                // Video not in database, data needs to be gathered
                tempVideo = new Video();
                tempVideo.FilePath = filePath;
                tempVideo.FileName = Path.GetFileNameWithoutExtension(filePath);

                tempVideo.DownloadVideoData(tempVideo.FileName);
                int primaryKey = DB.AddVideo(tempVideo);
                foreach (string genre in tempVideo.Genre)
                {
                    DB.AddGenre(genre);
                    DB.AddVideoGenre(primaryKey, genre);
                }
                foreach (string director in tempVideo.Director)
                {
                    DB.AddDirector(director);
                    DB.AddVideoDirector(primaryKey, director);
                }
                foreach (string writer in tempVideo.Writer)
                {
                    DB.AddWriter(writer);
                    DB.AddVideoWriter(primaryKey, writer);
                }
                foreach (string actor in tempVideo.Actor)
                {
                    DB.AddActor(actor);
                    DB.AddVideoActor(primaryKey, actor);
                }
            }
            if (tempVideo.Title != "")
            {
                Videos.Add(tempVideo);
            }
        }

        //create directory to store downloaded poster images in
        private void CreatePosterImagesDirectory()
        {
            if (Directory.Exists(PosterImagesPath) == false)
            {
                Directory.CreateDirectory(PosterImagesPath);
            }
        }

        // broad categories for lbxBroad listbox on FormMediaBrowser
        private void SetBroadCategories()
        {
            BroadCategories.Add("All");
            BroadCategories.Add("Year");
            BroadCategories.Add("Genre");
            BroadCategories.Add("Director");
            BroadCategories.Add("Writer");
            BroadCategories.Add("Actor");
            BroadCategories.Add("Rating");
        }

        public List<Video> GetCurrentVideos(string broad, string narrow)
        {
            List<Video> currentVideos = new List<Video>();
            if (broad.Equals("All"))
            {
                List<Video> list =
                    (from video in Videos
                     select video).ToList();
                currentVideos = list;
            }
            else if (broad.Equals("Year"))
            {
                List<Video> list =
                    (from video in Videos
                     where video.Year.Equals(narrow)
                     select video).ToList();
                currentVideos = list;
            }
            else if (broad.Equals("Genre"))
            {
                List<Video> list =
                    (from video in Videos
                     where video.Genre.Contains(narrow)
                     select video).ToList();
                currentVideos = list;
            }
            else if (broad.Equals("Director"))
            {
                List<Video> list =
                    (from video in Videos
                     where video.Director.Contains(narrow)
                     select video).ToList();
                currentVideos = list;
            }
            else if (broad.Equals("Writer"))
            {
                List<Video> list =
                    (from video in Videos
                     where video.Writer.Contains(narrow)
                     select video).ToList();
                currentVideos = list;
            }
            else if (broad.Equals("Actor"))
            {
                List<Video> list =
                    (from video in Videos
                     where video.Actor.Contains(narrow)
                     select video).ToList();
                currentVideos = list;
            }
            else if (broad.Equals("Rating"))
            {
                float rating =
                    float.Parse(narrow,
                    System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                List<Video> list =
                    (from video in Videos
                     where Math.Floor(video.Rating).Equals(rating)
                     select video).ToList();
                currentVideos = list;
            }
            return currentVideos;
        }
        
    }
}
