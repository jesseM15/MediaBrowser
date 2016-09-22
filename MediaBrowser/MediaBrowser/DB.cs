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

        // executes non query
        private static void ExecuteNonQuery()
        {
            try
            {
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

        // executes scalar
        private static int ExecuteScalar()
        {
            try
            {
                _cmd.Connection.Open();
                int pk = Convert.ToInt32(_cmd.ExecuteScalar());
                return pk;
            }
            catch (Exception ex)
            {
                Logger.Error("Sql Scalar Exception: " + ex.Message, "DB.cs");
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

        // returns a List<string> 
        //NOTE: Query MUST return only 1 column
        private static List<string> GetStringList()
        {
            try
            {
                _cmd.Connection.Open();
                List<string> result = new List<string>();
                SqlDataReader reader = _cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(0) != "")
                    {
                        result.Add(reader.GetString(0));
                    }
                }
                reader.Close();
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

        // returns a List<Video>
        private static List<Video> GetVideoList()
        {
            try
            {
                var dataSet = new DataSet();
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

        // returns an int
        // NOTE: Query MUST return only 1 column and 1 row
        private static int GetID()
        {
            try
            {
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

        // returns a video
        private static Video GetVideo()
        {
            try
            {
                var dataSet = new DataSet();
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

        // adds a directory path
        public static void AddSourceDirectory(string directoryPath)
        {
            InitCommand();
            _cmd.CommandText = @"INSERT INTO SourceDirectory VALUES (@directoryPath);";
            _cmd.Parameters.AddWithValue("@directoryPath", directoryPath);
            ExecuteNonQuery();
        }

        // adds a video and returns the id of the added video
        public static int AddVideo(Video video)
        {
            InitCommand();
            _cmd.CommandText = @"INSERT INTO Video VALUES 
                                 (@filePath, @fileName, @mediaImagePath, 
                                 @title, @year, @length, @rating, @plot);
                                 SELECT SCOPE_IDENTITY();";
            _cmd.Parameters.AddWithValue("@filePath", video.FilePath);
            _cmd.Parameters.AddWithValue("@fileName", video.FileName);
            _cmd.Parameters.AddWithValue("@mediaImagePath", video.MediaImagePath);
            _cmd.Parameters.AddWithValue("@title", video.Title);
            _cmd.Parameters.AddWithValue("@year", video.Year);
            _cmd.Parameters.AddWithValue("@length", video.Length);
            _cmd.Parameters.AddWithValue("@rating", video.Rating);
            _cmd.Parameters.AddWithValue("@plot", video.Plot);
            return ExecuteScalar();
        }

        // adds a genre if it does not already exist
        public static void AddGenre(string genre)
        {
            InitCommand();
            _cmd.CommandText = @"IF NOT EXISTS (SELECT Genre FROM Genre WHERE Genre=@genre) 
                                 INSERT INTO Genre VALUES (@genre);";
            _cmd.Parameters.AddWithValue("@genre", genre);
            ExecuteNonQuery();
        }

        // adds a video genre
        public static void AddVideoGenre(int videoID, string genre)
        {
            InitCommand();
            _cmd.CommandText = @"INSERT INTO VideoGenre VALUES (@videoID, 
                                 (SELECT GenreID FROM Genre WHERE Genre=@genre));";
            _cmd.Parameters.AddWithValue("@videoID", videoID);
            _cmd.Parameters.AddWithValue("@genre", genre);
            ExecuteNonQuery();
        }

        // adds a director if it does not already exist
        public static void AddDirector(string director)
        {
            InitCommand();
            _cmd.CommandText = @"IF NOT EXISTS (SELECT Director FROM Director WHERE Director=@director) 
                                 INSERT INTO Director VALUES (@director);";
            _cmd.Parameters.AddWithValue("@director", director);
            ExecuteNonQuery();
        }

        // adds a video director
        public static void AddVideoDirector(int videoID, string director)
        {
            InitCommand();
            _cmd.CommandText = @"INSERT INTO VideoDirector VALUES (@videoID, 
                                 (SELECT DirectorID FROM Director WHERE Director=@director));";
            _cmd.Parameters.AddWithValue("@videoID", videoID);
            _cmd.Parameters.AddWithValue("@director", director);
            ExecuteNonQuery();
        }

        // adds a writer if it does not already exist
        public static void AddWriter(string writer)
        {
            InitCommand();
            _cmd.CommandText = @"IF NOT EXISTS (SELECT Writer FROM Writer WHERE Writer=@writer) 
                                 INSERT INTO Writer VALUES (@writer);";
            _cmd.Parameters.AddWithValue("@writer", writer);
            ExecuteNonQuery();
        }

        // adds a video writer
        public static void AddVideoWriter(int videoID, string writer)
        {
            InitCommand();
            _cmd.CommandText = @"INSERT INTO VideoWriter VALUES (@videoID, 
                                 (SELECT WriterID FROM Writer WHERE Writer=@writer));";
            _cmd.Parameters.AddWithValue("@videoID", videoID);
            _cmd.Parameters.AddWithValue("@writer", writer);
            ExecuteNonQuery();
        }

        // adds a actor if it does not already exist
        public static void AddActor(string actor)
        {
            InitCommand();
            _cmd.CommandText = @"IF NOT EXISTS (SELECT Actor FROM Actor WHERE Actor=@actor) 
                                 INSERT INTO Actor VALUES (@actor);";
            _cmd.Parameters.AddWithValue("@actor", actor);
            ExecuteNonQuery();
        }

        // adds a video actor
        public static void AddVideoActor(int videoID, string actor)
        {
            InitCommand();
            _cmd.CommandText = @"INSERT INTO VideoActor VALUES (@videoID, 
                                 (SELECT ActorID FROM Actor WHERE Actor=@actor));";
            _cmd.Parameters.AddWithValue("@videoID", videoID);
            _cmd.Parameters.AddWithValue("@actor", actor);
            ExecuteNonQuery();
        }


        // removes a directory path
        public static void RemoveSourceDirectory(string directoryPath)
        {
            InitCommand();
            _cmd.CommandText = @"DELETE FROM SourceDirectory 
                                 WHERE DirectoryPath=@directoryPath;";
            _cmd.Parameters.AddWithValue("@directoryPath", directoryPath);
            ExecuteNonQuery();
        }

        // removes a video with the specified VideoID
        public static void RemoveVideo(int videoID)
        {
            InitCommand();
            _cmd.CommandText = @"DELETE FROM Video 
                                 WHERE VideoID=@videoID;";
            _cmd.Parameters.AddWithValue("@videoID", videoID);
            ExecuteNonQuery();
        }

        // removes all records not referenced in VideoGenre
        public static void RemoveUnusedGenres()
        {
            InitCommand();
            _cmd.CommandText = @"DELETE FROM Genre 
                                 WHERE GenreID NOT IN 
                                 (SELECT GenreID FROM VideoGenre);";
            ExecuteNonQuery();
        }

        // removes a video genre
        public static void RemoveVideoGenre(int videoID)
        {
            InitCommand();
            _cmd.CommandText = @"DELETE FROM VideoGenre 
                                 WHERE VideoID=@videoID;";
            _cmd.Parameters.AddWithValue("@videoID", videoID);
            ExecuteNonQuery();
        }

        // removes all records not referenced in VideoDirector
        public static void RemoveUnusedDirectors()
        {
            InitCommand();
            _cmd.CommandText = @"DELETE FROM Director 
                                 WHERE DirectorID NOT IN 
                                 (SELECT DirectorID FROM VideoDirector);";
            ExecuteNonQuery();
        }

        // removes a video director
        public static void RemoveVideoDirector(int videoID)
        {
            InitCommand();
            _cmd.CommandText = @"DELETE FROM VideoDirector 
                                 WHERE VideoID=@videoID;";
            _cmd.Parameters.AddWithValue("@videoID", videoID);
            ExecuteNonQuery();
        }

        // removes all records not referenced in VideoWriter
        public static void RemoveUnusedWriters()
        {
            InitCommand();
            _cmd.CommandText = @"DELETE FROM Writer 
                                 WHERE WriterID NOT IN 
                                 (SELECT WriterID FROM VideoWriter);";
            ExecuteNonQuery();
        }

        // removes a video writer
        public static void RemoveVideoWriter(int videoID)
        {
            InitCommand();
            _cmd.CommandText = @"DELETE FROM VideoWriter 
                                 WHERE VideoID=@videoID;";
            _cmd.Parameters.AddWithValue("@videoID", videoID);
            ExecuteNonQuery();
        }

        // removes all records not referenced in VideoActor
        public static void RemoveUnusedActors()
        {
            InitCommand();
            _cmd.CommandText = @"DELETE FROM Actor 
                                 WHERE ActorID NOT IN 
                                 (SELECT ActorID FROM VideoActor);";
            ExecuteNonQuery();
        }

        // removes a video actor
        public static void RemoveVideoActor(int videoID)
        {
            InitCommand();
            _cmd.CommandText = @"DELETE FROM VideoActor 
                                 WHERE VideoID=@videoID;";
            _cmd.Parameters.AddWithValue("@videoID", videoID);
            ExecuteNonQuery();
        }


        // updates a video from the specified videoID
        public static void UpdateVideo(Video video)
        {
            InitCommand();
            _cmd.CommandText = @"UPDATE Video SET 
                                 FilePath = @filePath, FileName = @fileName, 
                                 MediaImagePath = @mediaImagePath, Title = @title,
                                 Year = @year, Length = @length, 
                                 Rating = @rating, Plot = @plot 
                                 WHERE VideoID = @videoID;";
                _cmd.Parameters.AddWithValue("@videoID", video.VideoID);
                _cmd.Parameters.AddWithValue("@filePath", video.FilePath);
                _cmd.Parameters.AddWithValue("@fileName", video.FileName);
                _cmd.Parameters.AddWithValue("@mediaImagePath", video.MediaImagePath);
                _cmd.Parameters.AddWithValue("@title", video.Title);
                _cmd.Parameters.AddWithValue("@year", video.Year);
                _cmd.Parameters.AddWithValue("@length", video.Length);
                _cmd.Parameters.AddWithValue("@rating", video.Rating);
                _cmd.Parameters.AddWithValue("@plot", video.Plot);
                ExecuteNonQuery();
        }


        // returns a list of the source directories
        public static List<string> GetSourceDirectories()
        {
            InitCommand();
            _cmd.CommandText = @"SELECT DirectoryPath FROM SourceDirectory;";
            return GetStringList();
        }

        // returns a video from a specified filePath
        public static Video GetVideoData(string filePath)
        {
            InitCommand();
            _cmd.CommandText = @"SELECT * FROM Video 
                                 WHERE FilePath = @filePath;";
            _cmd.Parameters.AddWithValue("@filePath", filePath);
            return GetVideo();
        }

        // returns a VideoID from a specified FilePath
        public static int GetVideoID(string filePath)
        {
            InitCommand();
            _cmd.CommandText = @"SELECT VideoID FROM Video 
                                 WHERE FilePath = @filePath;";
            _cmd.Parameters.AddWithValue("@filePath", filePath);
            return GetID();
        }

        // returns a list of all videos that have NOT resolved a title
        public static List<Video> GetAllUnresolvedVideos()
        {
            InitCommand();
            _cmd.CommandText = @"SELECT * FROM Video 
                                 WHERE Title = '';";
            return GetVideoList();
        }

        // returns a list of all videos from a specified rating
        public static List<Video> GetVideosByRating(string rating)
        {
            InitCommand();
            _cmd.CommandText = @"SELECT * FROM Video 
                                 WHERE Rating = @rating;";
            _cmd.Parameters.AddWithValue("@rating", rating);
            return GetVideoList();
        }

        // returns a list of the distinct years of all the videos
        public static List<string> GetDistinctYears()
        {
            InitCommand();
            _cmd.CommandText = @"SELECT DISTINCT Year FROM Video;";
            return GetStringList();
        }

        // returns a list of the genres
        public static List<string> GetGenres()
        {
            InitCommand();
            _cmd.CommandText = @"SELECT  Genre FROM Genre;";
            return GetStringList();
        }

        // returns a list of Genres for a specified VideoID
        public static List<string> GetVideoGenres(int videoID)
        {
            InitCommand();
            _cmd.CommandText = @"SELECT Genre FROM Genre g 
                                 JOIN VideoGenre vg ON vg.GenreID=g.GenreID 
                                 WHERE vg.VideoID=@videoID;";
            _cmd.Parameters.AddWithValue("@videoID", videoID);
            return GetStringList();
        }

        // returns a list of the directors
        public static List<string> GetDirectors()
        {
            InitCommand();
            _cmd.CommandText = @"SELECT  Director FROM Director;";
            return GetStringList();
        }

        // returns a list of Directors for a specified VideoID
        public static List<string> GetVideoDirectors(int videoID)
        {
            InitCommand();
            _cmd.CommandText = @"SELECT Director FROM Director d 
                                 JOIN VideoDirector vd ON vd.DirectorID=d.DirectorID 
                                 WHERE vd.VideoID=@videoID;";
            _cmd.Parameters.AddWithValue("@videoID", videoID);
            return GetStringList();
        }

        // returns a list of the writers
        public static List<string> GetWriters()
        {
            InitCommand();
            _cmd.CommandText = @"SELECT  Writer FROM Writer;";
            return GetStringList();
        }

        // returns a list of Writers for a specified VideoID
        public static List<string> GetVideoWriters(int videoID)
        {
            InitCommand();
            _cmd.CommandText = @"SELECT Writer FROM Writer w 
                                 JOIN VideoWriter vw ON vw.WriterID=w.WriterID 
                                 WHERE vw.VideoID=@videoID;";
            _cmd.Parameters.AddWithValue("@videoID", videoID);
            return GetStringList();
        }

        // returns a list of the actors
        public static List<string> GetActors()
        {
            InitCommand();
            _cmd.CommandText = @"SELECT  Actor FROM Actor;";
            return GetStringList();
        }

        // returns a list of Actors for a specified VideoID
        public static List<string> GetVideoActors(int videoID)
        {
            InitCommand();
            _cmd.CommandText = @"SELECT Actor FROM Actor a 
                                 JOIN VideoActor va ON va.ActorID=a.ActorID 
                                 WHERE va.VideoID=@videoID;";
            _cmd.Parameters.AddWithValue("@videoID", videoID);
            return GetStringList();
        }

    }
}
