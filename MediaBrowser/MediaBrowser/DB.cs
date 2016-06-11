using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MediaBrowser
{
    public static class DB
    {
        private static string _con;
        private static SqlCommand _cmd;

        static DB()
        {
            _con = ConfigurationManager.ConnectionStrings["MediaBrowserTestConnectionString"].ConnectionString;
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
                    "Year varchar(4));";
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
                    tempVideo.FilePath = row["FilePath"].ToString();
                    tempVideo.FileName = row["FileName"].ToString();
                    tempVideo.MediaImagePath = row["MediaImagePath"].ToString();
                    tempVideo.Title = row["Title"].ToString();
                    tempVideo.Year = row["Year"].ToString();
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

        public static Video GetVideoData(int id)
        {
            try
            {
                var dataSet = new DataSet();
                InitCommand();
                _cmd.CommandText =
                    "SELECT * FROM Video " +
                    "WHERE VideoID = @id;";
                _cmd.Parameters.AddWithValue("@id", id);
                _cmd.Connection.Open();
                var dataAdapter = new SqlDataAdapter { SelectCommand = _cmd };
                dataAdapter.Fill(dataSet);
                Video tempVideo = null;
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    tempVideo = new Video();
                    tempVideo.FilePath = row["FilePath"].ToString();
                    tempVideo.FileName = row["FileName"].ToString();
                    tempVideo.MediaImagePath = row["MediaImagePath"].ToString();
                    tempVideo.Title = row["Title"].ToString();
                    tempVideo.Year = row["Year"].ToString();
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

        // adds a video
        public static int AddVideo(Video video)
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "INSERT INTO Video VALUES (@filePath, @fileName, @mediaImagePath, @title, @year);" +
                    "SELECT SCOPE_IDENTITY();";
                _cmd.Parameters.AddWithValue("@filePath", video.FilePath);
                _cmd.Parameters.AddWithValue("@fileName", video.FileName);
                _cmd.Parameters.AddWithValue("@mediaImagePath", video.MediaImagePath);
                _cmd.Parameters.AddWithValue("@title", video.Title);
                _cmd.Parameters.AddWithValue("@year", video.Year);
                _cmd.Connection.Open();
                //_cmd.ExecuteNonQuery();
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

        // returns a list of all videos from a specified year
        public static List<Video> GetVideosByYear(string year)
        {
            try
            {
                var dataSet = new DataSet();
                InitCommand();
                _cmd.CommandText =
                    "SELECT * FROM Video " +
                    "WHERE Year = @year;";
                _cmd.Parameters.AddWithValue("@year", year);
                _cmd.Connection.Open();
                var dataAdapter = new SqlDataAdapter { SelectCommand = _cmd };
                dataAdapter.Fill(dataSet);
                List<Video> result = new List<Video>();
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Video tempVideo = new Video();
                    tempVideo.FilePath = row["FilePath"].ToString();
                    tempVideo.FileName = row["FileName"].ToString();
                    tempVideo.MediaImagePath = row["MediaImagePath"].ToString();
                    tempVideo.Title = row["Title"].ToString();
                    tempVideo.Year = row["Year"].ToString();
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
                    "Genre varchar(255)," +
                    "VideoID int NOT NULL FOREIGN KEY REFERENCES Video(VideoID));";
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

        // adds a genre
        public static void AddGenre(string genre, int videoID)
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "INSERT INTO Genre VALUES (@Genre, @VideoID);";
                _cmd.Parameters.AddWithValue("@Genre", genre);
                _cmd.Parameters.AddWithValue("@VideoID", videoID);
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

        // returns a list of the distinct genres
        public static List<string> GetDistinctGenres()
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "SELECT DISTINCT Genre FROM Genre;";
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

        // returns a list of videoIDs for a specified genre
        public static List<int> GetVideoIDsByGenre(string genre)
        {
            try
            {
                InitCommand();
                _cmd.CommandText =
                    "SELECT VideoID FROM Genre " +
                    "WHERE Genre = @Genre;";
                _cmd.Parameters.AddWithValue("@Genre", genre);
                _cmd.Connection.Open();
                List<int> videoIDs = new List<int>();
                SqlDataReader reader = _cmd.ExecuteReader();
                while (reader.Read())
                {
                    videoIDs.Add(reader.GetInt32(0));
                }
                reader.Close();
                return videoIDs;
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

    }
}
