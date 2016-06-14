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

            PopulateVideos(GetVideoFiles());

        }

        // shows FormSourceDirectories and adds/removes chosen directories
        public void ShowSourceDirectoryDialog()
        {
            FormSourceDirectories sd = new FormSourceDirectories(SourceDirectories);
            var result = sd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                AddSourceDirectories(sd.directoriesToAdd);
                RemoveSourceDirectories(sd.directoriesToRemove);
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
        }

        // removes a list of directories from the database and SourceDirectories property
        private void RemoveSourceDirectories(List<string> directoriesToRemove)
        {
            foreach (string directory in directoriesToRemove)
            {
                DB.RemoveSourceDirectory(directory);
                SourceDirectories.Remove(directory);
            }
        }

        // returns a list of file paths for every video in the source directories
        private List<string> GetVideoFiles()
        {
            List<string> filePaths = new List<string>();
            foreach (string directory in SourceDirectories)
            {
                filePaths.AddRange(Directory.GetFiles(directory));
            }
            return filePaths;
        }

        private void PopulateVideos(List<string> videoFiles)
        {
            // iterate through files and assign Media properties
            for (int n = 0; n < videoFiles.Count(); n++)
            {
                Video tempVideo = DB.GetVideoData(videoFiles[n]);
                if (tempVideo != null)
                {
                    // Database already contains video
                    try
                    {
                        tempVideo.MediaImage = new Bitmap(tempVideo.MediaImagePath);
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
                    tempVideo.FilePath = videoFiles[n];
                    tempVideo.FileName = Path.GetFileNameWithoutExtension(videoFiles[n]);

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
                Videos.Add(tempVideo);
            }
        }

        // create directory to store downloaded poster images in
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
            // temporarily hard-coded
            BroadCategories.Add("All");
            BroadCategories.Add("Year");
            BroadCategories.Add("Genre");
            BroadCategories.Add("Director");
            BroadCategories.Add("Writer");
            BroadCategories.Add("Actor");
        }

        public List<Video> GetCurrentVideos(string broad, string narrow)
        {
            List<Video> currentVideos = new List<Video>();
            if (broad.Equals("All"))
            {
                currentVideos = DB.GetAllVideos();
            }
            else if (broad.Equals("Year"))
            {
                currentVideos = DB.GetVideosByYear(narrow);
            }
            else if (broad.Equals("Genre"))
            {
                currentVideos = DB.GetVideosByGenre(narrow);
            }
            else if (broad.Equals("Director"))
            {
                currentVideos = DB.GetVideosByDirector(narrow);
            }
            else if (broad.Equals("Writer"))
            {
                currentVideos = DB.GetVideosByWriter(narrow);
            }
            else if (broad.Equals("Actor"))
            {
                currentVideos = DB.GetVideosByActor(narrow);
            }
            LoadGenres(currentVideos);
            LoadDirectors(currentVideos);
            LoadWriters(currentVideos);
            LoadActors(currentVideos);
            LoadCurrentImages(currentVideos);
            return currentVideos;
        }

        private void LoadCurrentImages(List<Video> currentVideos)
        {
            foreach (Video video in currentVideos)
            {
                video.LoadImage();
            }
        }

        private void LoadGenres(List<Video> currentVideos)
        {
            foreach (Video video in currentVideos)
            {
                video.Genre = DB.GetGenresByVideoID(video.VideoID);
            }
        }

        private void LoadDirectors(List<Video> currentVideos)
        {
            foreach (Video video in currentVideos)
            {
                video.Director = DB.GetDirectorsByVideoID(video.VideoID);
            }
        }

        private void LoadWriters(List<Video> currentVideos)
        {
            foreach (Video video in currentVideos)
            {
                video.Writer = DB.GetWritersByVideoID(video.VideoID);
            }
        }

        private void LoadActors(List<Video> currentVideos)
        {
            foreach (Video video in currentVideos)
            {
                video.Actor = DB.GetActorsByVideoID(video.VideoID);
            }
        }
        
    }
}
