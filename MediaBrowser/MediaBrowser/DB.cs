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
                    "MediaImagePath varchar(255));";
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

        public static DataSet GetVideoData(string filePath)
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
                return dataSet;
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
