using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace MediaBrowser
{
    public static class DB
    {
        private static string _con;
        private static SqlCommand _cmd;

        static DB()
        {
            _con = ConfigurationManager.ConnectionStrings["MediaBrowserConnectionString"].ConnectionString;
        }

        // initializes a command for sql
        private static void InitCommand()
        {
            _cmd = new SqlCommand();
            _cmd.Connection = new SqlConnection(_con);
            _cmd.CommandType = CommandType.Text;
        }

        // creates the MediaBrowser database
        public static void CreateMediaBrowserDB()
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "DECLARE @dbname nvarchar(128);" +
                    "SET @dbname = N'MediaBrowser';" +
                    "IF (NOT EXISTS (SELECT name FROM master.dbo.sysdatabases " +
                    "WHERE ('[' + name + ']' = @dbname " +
                    "OR name = @dbname))) " +
                    "CREATE DATABASE MediaBrowser;";
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(_con);
                builder["Database"] = "MediaBrowser";
                _con = builder.ConnectionString;
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // creates SourceDirectory table if it does not exist
        public static void CreateSourceDirectoryTable()
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "IF NOT EXISTS(SELECT * FROM sysobjects " +
                    "WHERE name='SourceDirectory' AND xtype='U') " +
                    "CREATE TABLE SourceDirectory(" +
                    "SourceDirectoryID int NOT NULL IDENTITY(1,1) PRIMARY KEY, " + 
                    "DirectoryPath varchar(255));";
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // adds a directory path
        public static void AddSourceDirectory(string directoryPath)
        {
            try
            {
                InitCommand();
                _cmd.CommandText = 
                    "INSERT INTO SourceDirectory VALUES (@directoryPath);";
                _cmd.Parameters.AddWithValue("@directoryPath", directoryPath);
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
                _cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // removes a directory path
        public static void RemoveSourceDirectory(string directoryPath)
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "DELETE FROM SourceDirectory " +
                    "WHERE DirectoryPath=@directoryPath;";
                _cmd.Parameters.AddWithValue("@directoryPath", directoryPath);
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
                _cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // returns all the source directories
        public static List<string> GetSourceDirectories()
        {
            try
            {
                var dataSet = new DataSet();
                InitCommand();
                _cmd.CommandText =
                    "SELECT * FROM SourceDirectory;";
                _cmd.Connection.Open();
                var dataAdapter = new SqlDataAdapter { SelectCommand = _cmd };
                dataAdapter.Fill(dataSet);
                List<string> result = new List<string>();    
                foreach(DataRow row in dataSet.Tables[0].Rows)
                {
                    result.Add(row["DirectoryPath"].ToString());
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Sql Query Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // creates the Video table if it does not exist
        public static void CreateVideoTable()
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "IF NOT EXISTS(SELECT * FROM sysobjects " +
                    "WHERE name='Video' AND xtype='U') " +
                    "CREATE TABLE Video(" +
                    "VideoID int NOT NULL IDENTITY(1,1) PRIMARY KEY, " +
                    "FilePath varchar(255), " +
                    "FileName varchar(255), " +
                    "MediaImagePath varchar(255), " +
                    "Title varchar(255), " +
                    "Year varchar(4), " +
                    "Length varchar(32), " +
                    "Rating float(24), " +
                    "Plot varchar(4096));";
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // returns a video from a specified filePath
        public static Video GetVideoData(string filePath)
        {
            try
            {
                var dataSet = new DataSet();
                InitCommand();
                _cmd.CommandText =
                    "SELECT * FROM Video " +
                    "WHERE FilePath = @filePath;";
                _cmd.Parameters.AddWithValue("@filePath", filePath);
                _cmd.Connection.Open();
                var dataAdapter = new SqlDataAdapter { SelectCommand = _cmd };
                dataAdapter.Fill(dataSet);
                Video tempVideo = null;
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    tempVideo = new Video();
                    tempVideo.VideoID = (int)row["VideoID"];
                    tempVideo.FilePath = row["FilePath"].ToString();
                    tempVideo.FileName = row["FileName"].ToString();
                    tempVideo.MediaImagePath = row["MediaImagePath"].ToString();
                    tempVideo.Title = row["Title"].ToString();
                    tempVideo.Year = row["Year"].ToString();
                    tempVideo.Length = row["Length"].ToString();
                    tempVideo.Rating = 
                        float.Parse(row["Rating"].ToString(), 
                        CultureInfo.InvariantCulture.NumberFormat);
                    tempVideo.Plot = row["Plot"].ToString();
                }
                return tempVideo;
            }
            catch (Exception ex)
            {
                Logger.Error("Sql Query Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // adds a video and returns the id of the added video
        public static int AddVideo(Video video)
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "INSERT INTO Video VALUES " +
                    "(@filePath, @fileName, @mediaImagePath, " +
                    "@title, @year, @length, @rating, @plot);" +
                    "SELECT SCOPE_IDENTITY();";
                _cmd.Parameters.AddWithValue("@filePath", video.FilePath);
                _cmd.Parameters.AddWithValue("@fileName", video.FileName);
                _cmd.Parameters.AddWithValue("@mediaImagePath", video.MediaImagePath);
                _cmd.Parameters.AddWithValue("@title", video.Title);
                _cmd.Parameters.AddWithValue("@year", video.Year);
                _cmd.Parameters.AddWithValue("@length", video.Length);
                _cmd.Parameters.AddWithValue("@rating", video.Rating);
                _cmd.Parameters.AddWithValue("@plot", video.Plot);
                _cmd.Connection.Open();
                int pk = Convert.ToInt32(_cmd.ExecuteScalar());
                return pk;
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // updates a video from the specified videoID
        public static void UpdateVideo(Video video)
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "UPDATE Video SET " +
                    "FilePath = @filePath, FileName = @fileName, " +
                    "MediaImagePath = @mediaImagePath, Title = @title," +
                    "Year = @year, Length = @length, " +
                    "Rating = @rating, Plot = @plot " +
                    "WHERE VideoID = @videoID;";
                _cmd.Parameters.AddWithValue("@videoID", video.VideoID);
                _cmd.Parameters.AddWithValue("@filePath", video.FilePath);
                _cmd.Parameters.AddWithValue("@fileName", video.FileName);
                _cmd.Parameters.AddWithValue("@mediaImagePath", video.MediaImagePath);
                _cmd.Parameters.AddWithValue("@title", video.Title);
                _cmd.Parameters.AddWithValue("@year", video.Year);
                _cmd.Parameters.AddWithValue("@length", video.Length);
                _cmd.Parameters.AddWithValue("@rating", video.Rating);
                _cmd.Parameters.AddWithValue("@plot", video.Plot);
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // returns a VideoID from a specified FilePath
        public static int GetVideoID(string filePath)
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "SELECT VideoID FROM Video " +
                    "WHERE FilePath = @filePath;";
                _cmd.Parameters.AddWithValue("@filePath", filePath);
                _cmd.Connection.Open();
                int videoID = 0;
                SqlDataReader reader = _cmd.ExecuteReader();
                while (reader.Read())
                {
                    videoID = reader.GetInt32(0);
                }
                reader.Close();
                return videoID;
            }
            catch (Exception ex)
            {
                Logger.Error("Sql Query Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // removes a video with the specified VideoID
        public static void RemoveVideo(int videoID)
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "DELETE FROM Video " +
                    "WHERE VideoID=@videoID;";
                _cmd.Parameters.AddWithValue("@videoID", videoID);
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
                _cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // returns a list of all videos that have NOT resolved a title
        public static List<Video> GetAllUnresolvedVideos()
        {
            try
            {
                var dataSet = new DataSet();
                InitCommand();
                _cmd.CommandText =
                    "SELECT * FROM Video " +
                    "WHERE Title = '';";
                _cmd.Connection.Open();
                var dataAdapter = new SqlDataAdapter { SelectCommand = _cmd };
                dataAdapter.Fill(dataSet);
                List<Video> result = new List<Video>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Video tempVideo = new Video();
                    tempVideo.VideoID = (int)row["VideoID"];
                    tempVideo.FilePath = row["FilePath"].ToString();
                    tempVideo.FileName = row["FileName"].ToString();
                    tempVideo.MediaImagePath = row["MediaImagePath"].ToString();
                    tempVideo.Title = row["Title"].ToString();
                    tempVideo.Year = row["Year"].ToString();
                    tempVideo.Length = row["Length"].ToString();
                    tempVideo.Rating = 
                        float.Parse(row["Rating"].ToString(), 
                        CultureInfo.InvariantCulture.NumberFormat);
                    tempVideo.Plot = row["Plot"].ToString();
                    result.Add(tempVideo);
                }
                return result;
                
            }
            catch (Exception ex)
            {
                Logger.Error("Sql Query Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // returns a list of the distinct years of all the videos
        public static List<string> GetDistinctYears()
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "SELECT DISTINCT Year FROM Video;";
                _cmd.Connection.Open();
                List<string> years = new List<string>();
                SqlDataReader reader = _cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(0) != "")
                    {
                        years.Add(reader.GetString(0));
                    }
                }
                reader.Close();
                return years;
            }
            catch (Exception ex)
            {
                Logger.Error("Sql Query Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // returns a list of all videos from a specified rating
        public static List<Video> GetVideosByRating(string rating)
        {
            try
            {
                var dataSet = new DataSet();
                InitCommand();
                _cmd.CommandText =
                    "SELECT * FROM Video " +
                    "WHERE Rating = @rating;";
                _cmd.Parameters.AddWithValue("@rating", rating);
                _cmd.Connection.Open();
                var dataAdapter = new SqlDataAdapter { SelectCommand = _cmd };
                dataAdapter.Fill(dataSet);
                List<Video> result = new List<Video>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Video tempVideo = new Video();
                    tempVideo.VideoID = (int)row["VideoID"];
                    tempVideo.FilePath = row["FilePath"].ToString();
                    tempVideo.FileName = row["FileName"].ToString();
                    tempVideo.MediaImagePath = row["MediaImagePath"].ToString();
                    tempVideo.Title = row["Title"].ToString();
                    tempVideo.Year = row["Year"].ToString();
                    tempVideo.Length = row["Length"].ToString();
                    tempVideo.Rating =
                        float.Parse(row["Rating"].ToString(),
                        CultureInfo.InvariantCulture.NumberFormat);
                    tempVideo.Plot = row["Plot"].ToString();
                    result.Add(tempVideo);
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Sql Query Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // creates the Genre table if it does not exist
        public static void CreateGenreTable()
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "IF NOT EXISTS(SELECT * FROM sysobjects " +
                    "WHERE name='Genre' AND xtype='U') " +
                    "CREATE TABLE Genre(" +
                    "GenreID int NOT NULL IDENTITY(1,1) PRIMARY KEY, " +
                    "Genre varchar(255));";
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // returns a list of the genres
        public static List<string> GetGenres()
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "SELECT  Genre FROM Genre;";
                _cmd.Connection.Open();
                List<string> genres = new List<string>();
                SqlDataReader reader = _cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(0) != "")
                    {
                        genres.Add(reader.GetString(0));
                    }
                }
                reader.Close();
                return genres;
            }
            catch (Exception ex)
            {
                Logger.Error("Sql Query Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // adds a genre if it does not already exist
        public static void AddGenre(string genre)
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "IF NOT EXISTS (SELECT Genre FROM Genre WHERE Genre=@genre) " +
                    "INSERT INTO Genre VALUES (@genre);";
                _cmd.Parameters.AddWithValue("@genre", genre);
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // removes all records not referenced in VideoGenre
        public static void RemoveUnusedGenres()
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "DELETE FROM Genre " +
                    "WHERE GenreID NOT IN " +
                    "(SELECT GenreID FROM VideoGenre);";
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
                _cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // creates the VideoGenre table if it does not exist
        public static void CreateVideoGenreTable()
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "IF NOT EXISTS(SELECT * FROM sysobjects " +
                    "WHERE name='VideoGenre' AND xtype='U') " +
                    "CREATE TABLE VideoGenre( " +
                    "VideoID int NOT NULL, " +
                    "GenreID int NOT NULL, " +
                    "constraint fk_Video_Genre primary key (VideoID, GenreID)" +
                    ")ON [PRIMARY]";
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // returns a list of Genres for a specified VideoID
        public static List<string> GetVideoGenres(int videoID)
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "SELECT Genre FROM Genre g " +
                    "JOIN VideoGenre vg ON vg.GenreID=g.GenreID " +
                    "WHERE vg.VideoID=@videoID;";
                _cmd.Parameters.AddWithValue("@videoID", videoID);
                _cmd.Connection.Open();
                List<string> genres = new List<string>();
                SqlDataReader reader = _cmd.ExecuteReader();
                while (reader.Read())
                {
                    genres.Add(reader.GetString(0));
                }
                reader.Close();
                return genres;
            }
            catch (Exception ex)
            {
                Logger.Error("Sql Query Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // adds a video genre
        public static void AddVideoGenre(int videoID, string genre)
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "INSERT INTO VideoGenre VALUES (@videoID, " +
                    "(SELECT GenreID FROM Genre WHERE Genre=@genre));";
                _cmd.Parameters.AddWithValue("@videoID", videoID);
                _cmd.Parameters.AddWithValue("@genre", genre);
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // removes a video genre
        public static void RemoveVideoGenre(int videoID)
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "DELETE FROM VideoGenre " +
                    "WHERE VideoID=@videoID;";
                _cmd.Parameters.AddWithValue("@videoID", videoID);
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
                _cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

       // creates the Director table if it does not exist
        public static void CreateDirectorTable()
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "IF NOT EXISTS(SELECT * FROM sysobjects " +
                    "WHERE name='Director' AND xtype='U') " +
                    "CREATE TABLE Director(" +
                    "DirectorID int NOT NULL IDENTITY(1,1) PRIMARY KEY, " +
                    "Director varchar(255));";
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // returns a list of the directors
        public static List<string> GetDirectors()
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "SELECT  Director FROM Director;";
                _cmd.Connection.Open();
                List<string> directors = new List<string>();
                SqlDataReader reader = _cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(0) != "")
                    {
                        directors.Add(reader.GetString(0));
                    }
                }
                reader.Close();
                return directors;
            }
            catch (Exception ex)
            {
                Logger.Error("Sql Query Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // adds a director if it does not already exist
        public static void AddDirector(string director)
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "IF NOT EXISTS (SELECT Director FROM Director WHERE Director=@director) " +
                    "INSERT INTO Director VALUES (@director);";
                _cmd.Parameters.AddWithValue("@director", director);
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // removes all records not referenced in VideoDirector
        public static void RemoveUnusedDirectors()
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "DELETE FROM Director " +
                    "WHERE DirectorID NOT IN " +
                    "(SELECT DirectorID FROM VideoDirector);";
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
                _cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // creates the VideoDirector table if it does not exist
        public static void CreateVideoDirectorTable()
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "IF NOT EXISTS(SELECT * FROM sysobjects " +
                    "WHERE name='VideoDirector' AND xtype='U') " +
                    "CREATE TABLE VideoDirector( " +
                    "VideoID int NOT NULL, " +
                    "DirectorID int NOT NULL, " +
                    "constraint fk_Video_Director primary key (VideoID, DirectorID)" +
                    ")ON [PRIMARY]";
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // returns a list of Directors for a specified VideoID
        public static List<string> GetVideoDirectors(int videoID)
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "SELECT Director FROM Director d " +
                    "JOIN VideoDirector vd ON vd.DirectorID=d.DirectorID " +
                    "WHERE vd.VideoID=@videoID;";
                _cmd.Parameters.AddWithValue("@videoID", videoID);
                _cmd.Connection.Open();
                List<string> directors = new List<string>();
                SqlDataReader reader = _cmd.ExecuteReader();
                while (reader.Read())
                {
                    directors.Add(reader.GetString(0));
                }
                reader.Close();
                return directors;
            }
            catch (Exception ex)
            {
                Logger.Error("Sql Query Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // adds a video director
        public static void AddVideoDirector(int videoID, string director)
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "INSERT INTO VideoDirector VALUES (@videoID, " +
                    "(SELECT DirectorID FROM Director WHERE Director=@director));";
                _cmd.Parameters.AddWithValue("@videoID", videoID);
                _cmd.Parameters.AddWithValue("@director", director);
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // removes a video director
        public static void RemoveVideoDirector(int videoID)
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "DELETE FROM VideoDirector " +
                    "WHERE VideoID=@videoID;";
                _cmd.Parameters.AddWithValue("@videoID", videoID);
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
                _cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // creates the Writer table if it does not exist
        public static void CreateWriterTable()
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "IF NOT EXISTS(SELECT * FROM sysobjects " +
                    "WHERE name='Writer' AND xtype='U') " +
                    "CREATE TABLE Writer(" +
                    "WriterID int NOT NULL IDENTITY(1,1) PRIMARY KEY, " +
                    "Writer varchar(255));";
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // returns a list of the writers
        public static List<string> GetWriters()
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "SELECT  Writer FROM Writer;";
                _cmd.Connection.Open();
                List<string> writers = new List<string>();
                SqlDataReader reader = _cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(0) != "")
                    {
                        writers.Add(reader.GetString(0));
                    }
                }
                reader.Close();
                return writers;
            }
            catch (Exception ex)
            {
                Logger.Error("Sql Query Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // adds a writer if it does not already exist
        public static void AddWriter(string writer)
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "IF NOT EXISTS (SELECT Writer FROM Writer WHERE Writer=@writer) " +
                    "INSERT INTO Writer VALUES (@writer);";
                _cmd.Parameters.AddWithValue("@writer", writer);
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // removes all records not referenced in VideoWriter
        public static void RemoveUnusedWriters()
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "DELETE FROM Writer " +
                    "WHERE WriterID NOT IN " +
                    "(SELECT WriterID FROM VideoWriter);";
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
                _cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // creates the VideoWriter table if it does not exist
        public static void CreateVideoWriterTable()
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "IF NOT EXISTS(SELECT * FROM sysobjects " +
                    "WHERE name='VideoWriter' AND xtype='U') " +
                    "CREATE TABLE VideoWriter( " +
                    "VideoID int NOT NULL, " +
                    "WriterID int NOT NULL, " +
                    "constraint fk_Video_Writer primary key (VideoID, WriterID)" +
                    ")ON [PRIMARY]";
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // returns a list of Writers for a specified VideoID
        public static List<string> GetVideoWriters(int videoID)
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "SELECT Writer FROM Writer w " +
                    "JOIN VideoWriter vw ON vw.WriterID=w.WriterID " +
                    "WHERE vw.VideoID=@videoID;";
                _cmd.Parameters.AddWithValue("@videoID", videoID);
                _cmd.Connection.Open();
                List<string> writers = new List<string>();
                SqlDataReader reader = _cmd.ExecuteReader();
                while (reader.Read())
                {
                    writers.Add(reader.GetString(0));
                }
                reader.Close();
                return writers;
            }
            catch (Exception ex)
            {
                Logger.Error("Sql Query Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // adds a video writer
        public static void AddVideoWriter(int videoID, string writer)
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "INSERT INTO VideoWriter VALUES (@videoID, " +
                    "(SELECT WriterID FROM Writer WHERE Writer=@writer));";
                _cmd.Parameters.AddWithValue("@videoID", videoID);
                _cmd.Parameters.AddWithValue("@writer", writer);
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // removes a video writer
        public static void RemoveVideoWriter(int videoID)
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "DELETE FROM VideoWriter " +
                    "WHERE VideoID=@videoID;";
                _cmd.Parameters.AddWithValue("@videoID", videoID);
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
                _cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // creates the Actor table if it does not exist
        public static void CreateActorTable()
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "IF NOT EXISTS(SELECT * FROM sysobjects " +
                    "WHERE name='Actor' AND xtype='U') " +
                    "CREATE TABLE Actor(" +
                    "ActorID int NOT NULL IDENTITY(1,1) PRIMARY KEY, " +
                    "Actor varchar(255));";
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // returns a list of the actors
        public static List<string> GetActors()
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "SELECT  Actor FROM Actor;";
                _cmd.Connection.Open();
                List<string> actors = new List<string>();
                SqlDataReader reader = _cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(0) != "")
                    {
                        actors.Add(reader.GetString(0));
                    }
                }
                reader.Close();
                return actors;
            }
            catch (Exception ex)
            {
                Logger.Error("Sql Query Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // adds a actor if it does not already exist
        public static void AddActor(string actor)
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "IF NOT EXISTS (SELECT Actor FROM Actor WHERE Actor=@actor) " +
                    "INSERT INTO Actor VALUES (@actor);";
                _cmd.Parameters.AddWithValue("@actor", actor);
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // removes all records not referenced in VideoActor
        public static void RemoveUnusedActors()
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "DELETE FROM Actor " +
                    "WHERE ActorID NOT IN " +
                    "(SELECT ActorID FROM VideoActor);";
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
                _cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // creates the VideoActor table if it does not exist
        public static void CreateVideoActorTable()
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "IF NOT EXISTS(SELECT * FROM sysobjects " +
                    "WHERE name='VideoActor' AND xtype='U') " +
                    "CREATE TABLE VideoActor( " +
                    "VideoID int NOT NULL, " +
                    "ActorID int NOT NULL, " +
                    "constraint fk_Video_Actor primary key (VideoID, ActorID)" +
                    ")ON [PRIMARY]";
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // returns a list of Actors for a specified VideoID
        public static List<string> GetVideoActors(int videoID)
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "SELECT Actor FROM Actor a " +
                    "JOIN VideoActor va ON va.ActorID=a.ActorID " +
                    "WHERE va.VideoID=@videoID;";
                _cmd.Parameters.AddWithValue("@videoID", videoID);
                _cmd.Connection.Open();
                List<string> actors = new List<string>();
                SqlDataReader reader = _cmd.ExecuteReader();
                while (reader.Read())
                {
                    actors.Add(reader.GetString(0));
                }
                reader.Close();
                return actors;
            }
            catch (Exception ex)
            {
                Logger.Error("Sql Query Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // adds a video actor
        public static void AddVideoActor(int videoID, string actor)
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "INSERT INTO VideoActor VALUES (@videoID, " +
                    "(SELECT ActorID FROM Actor WHERE Actor=@actor));";
                _cmd.Parameters.AddWithValue("@videoID", videoID);
                _cmd.Parameters.AddWithValue("@actor", actor);
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }

        // removes a video actor
        public static void RemoveVideoActor(int videoID)
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "DELETE FROM VideoActor " +
                    "WHERE VideoID=@videoID;";
                _cmd.Parameters.AddWithValue("@videoID", videoID);
                _cmd.Connection.Open();
                _cmd.ExecuteNonQuery();
                _cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                Logger.Error("Sql NonQuery Exception: " + ex.Message, "DB.cs");
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_cmd.Connection != null)
                {
                    _cmd.Connection.Close();
                }
            }
        }


    }
}
