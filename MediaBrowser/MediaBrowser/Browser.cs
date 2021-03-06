﻿using System;
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
        public string State { get; set; }
        public List<Video> Videos { get; set; }
        public List<string> BroadCategories { get; set; }
        public static string PosterImagesPath { get; set; }
        public SourceDirectory SourceDirectory { get; set; }
        public FormMediaBrowser FMB { get; set; }

        public Browser()
        {
            State = "";
            Videos = new List<Video>();
            BroadCategories = new List<string>();
            PosterImagesPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\PosterImages\";
            SourceDirectory = new SourceDirectory();
            FMB = new FormMediaBrowser(this);
            FMB.Show();
        }

        // creates the database/tables and populates Videos
        public void Initialize()
        {
            SourceDirectory.SourceDirectories = DB.GetSourceDirectories();
            CreatePosterImagesDirectory();
            SetBroadCategories();
        }

        // returns a list of file paths for every video in the source directories
        public List<string> GetVideoFiles()
        {
            List<string> filePaths = new List<string>();
            foreach (string directory in SourceDirectory.SourceDirectories)
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
            filePaths.Sort();
            return filePaths;
        }

        public void PopulateVideo(string filePath)
        {
            Video tempVideo = new Video();
            IVideoData vr = new VideoRepository();
            // attempt to gather video data from database
            tempVideo = vr.GetVideo(filePath);
            if (tempVideo == null)
            {
                IVideoData vo = new VideoOMDB();
                // attempt to gather video data from OMDB
                tempVideo = vo.GetVideo(filePath);
            }
            if (tempVideo.Title != "")
            {
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
            BroadCategories.Add("All");
            BroadCategories.Add("Year");
            BroadCategories.Add("Genre");
            BroadCategories.Add("Director");
            BroadCategories.Add("Writer");
            BroadCategories.Add("Actor");
            BroadCategories.Add("Rating");
        }
        
    }
}
