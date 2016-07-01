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
            DB.CreateDirectorTable();
            DB.CreateWriterTable();
            DB.CreateActorTable();
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
                    DB.RemoveGenre(index);
                    DB.RemoveDirector(index);
                    DB.RemoveWriter(index);
                    DB.RemoveActor(index);
                    DB.RemoveVideo(index);
                    for (int n = 0; n < Videos.Count; n++)
                    {
                        if (Videos[n].VideoID == index)
                        {
                            Videos.RemoveAt(n);
                        }
                    }
                }
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
                    tempVideo.MediaImage = new Bitmap(tempVideo.MediaImagePath);
                    tempVideo.Genre = DB.GetGenresByVideoID(tempVideo.VideoID);
                    tempVideo.Director = DB.GetDirectorsByVideoID(tempVideo.VideoID);
                    tempVideo.Writer = DB.GetWritersByVideoID(tempVideo.VideoID);
                    tempVideo.Actor = DB.GetActorsByVideoID(tempVideo.VideoID);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.ToString(), "Browser.cs");
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
                    DB.AddGenre(genre, primaryKey);
                }
                foreach (string director in tempVideo.Director)
                {
                    DB.AddDirector(director, primaryKey);
                }
                foreach (string writer in tempVideo.Writer)
                {
                    DB.AddWriter(writer, primaryKey);
                }
                foreach (string actor in tempVideo.Actor)
                {
                    DB.AddActor(actor, primaryKey);
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
