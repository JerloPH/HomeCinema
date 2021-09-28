using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;

namespace HomeCinema
{
    // Class containing paths and data objects required by App
    public static class DataFile
    {
        public static string PATH_START = "";
        public static string PATH_RES = "";
        public static string PATH_IMG = "";
        public static string PATH_DATA = "";
        public static string PATH_TEMP = "";
        public static string PATH_LOG = "";

        public static string FILE_LOG_APP = "";
        public static string FILE_LOG_ERROR = "";
        public static string FILE_LOG_DB = "";
        public static string FILE_DEFIMG = "";
        public static string FILE_CONFIG = "";
        public static string FILE_SETTINGS = "";
        public static string FILE_COUNTRY = "";
        public static string FILE_GENRE = "";
        public static string FILE_NOTRAILER = "";
        public static string FILE_MEDIALOC = "";
        public static string FILE_MEDIA_EXT = "";

        public static string FILE_LOG_SKIPPED = "";

        // Check all FILES on Startup
        public static bool Initialize()
        {
            try  // Paths for Files and Folders
            {
                PATH_START = AppContext.BaseDirectory;
                PATH_RES = Path.Combine(PATH_START, "Resources") + "\\";
                PATH_IMG = Path.Combine(PATH_START, "covers") + "\\";
                PATH_DATA = Path.Combine(PATH_START, "data") + "\\";
                PATH_TEMP = Path.Combine(PATH_START, "temp") + "\\";
                PATH_LOG = Path.Combine(PATH_START, "logs");
                // Create directories
                CreateDir(PATH_IMG);
                CreateDir(PATH_DATA);
                CreateDir(PATH_TEMP);
                CreateDir(PATH_LOG);
                var date = DateTime.Now.ToString("yyyy-MM-dd");
                FILE_LOG_APP = Path.Combine(PATH_LOG, $"{date}_App.log");// Log all messages and actions
                FILE_LOG_ERROR = Path.Combine(PATH_LOG, $"{date}_Error.log"); // Contains only error Messages
                FILE_LOG_DB = Path.Combine(PATH_LOG, $"{date}_DB.log"); // Log all messages and actions for db-related
                FILE_LOG_SKIPPED = Path.Combine(PATH_LOG, $"{date}_SkippedEntries.log"); // Entries skipped
                // Data and files
                FILE_DEFIMG = PATH_IMG + @"0.jpg"; // default cover image
                FILE_CONFIG = Path.Combine(PATH_DATA, "config.json"); // configuration file to use for APIs
                FILE_SETTINGS = Path.Combine(PATH_DATA, "settings.json"); // settings used in App
                FILE_COUNTRY = Path.Combine(PATH_DATA, "country.hc_data"); // list of countries
                FILE_GENRE = Path.Combine(PATH_DATA, "genre.hc_data"); // List of genres
                FILE_NOTRAILER = Path.Combine(PATH_DATA, "NoTrailer.jpg"); // default picture if no trailer link
                FILE_MEDIALOC = Path.Combine(PATH_DATA, "medialocation.hc_data"); // For movies, folder locations
                FILE_MEDIA_EXT = Path.Combine(PATH_DATA, "media_ext.hc_data"); // Extensions to check for movies
                return true;
            }
            catch (Exception ex)
            {
                Msg.ShowError("DataFile-Initialize", ex, "Required files are not loaded!");
            }
            return false;
        }
        // Check required files
        public static void CheckAllFiles()
        {
            // resources
            CopyFromRes(FILE_DEFIMG); // covers
            CopyFromRes(FILE_COUNTRY);
            CopyFromRes(FILE_GENRE);
            CopyFromRes(FILE_NOTRAILER);
            CopyFromRes(FILE_MEDIALOC);
            CopyFromRes(FILE_MEDIA_EXT);
            // Create empty Logs
            if (!File.Exists(FILE_LOG_APP)) { GlobalVars.WriteToFile(DataFile.FILE_LOG_APP, ""); }
            if (!File.Exists(FILE_LOG_ERROR)) { GlobalVars.WriteToFile(DataFile.FILE_LOG_ERROR, ""); }
            if (!File.Exists(FILE_LOG_DB)) { GlobalVars.WriteToFile(DataFile.FILE_LOG_DB, ""); }
            // Default config
            if (!File.Exists(FILE_CONFIG))
            {
                var json = JsonConvert.SerializeObject(new ConfigJson(), Formatting.Indented);
                GlobalVars.WriteToFile(FILE_CONFIG, json);
            }
            else
            {
                var jsonContent = GlobalVars.ReadStringFromFile(FILE_CONFIG, "DataFile-CheckAllFiles");
                var config = JsonConvert.DeserializeObject<ConfigJson>(jsonContent, GlobalVars.JSON_SETTING);
                if (!String.IsNullOrWhiteSpace(config.TmdbApiKey))
                {
                    GlobalVars.TMDB_KEY = config.TmdbApiKey;
                }
            }
            // Contains TMDB_KEY ?
            GlobalVars.HAS_TMDB_KEY = !String.IsNullOrWhiteSpace(GlobalVars.TMDB_KEY);
            GlobalVars.DEBUGGING = Debugger.IsAttached;
            if (Debugger.IsAttached)
                GlobalVars.WriteToFile(Path.Combine(PATH_LOG, "DEBUG.log"), ""); // delete debug.log
        }
        // Copy from Resources if not on Root
        public static bool CopyFromRes(string fPath)
        {
            if (!File.Exists(fPath))
            {
                string fName = Path.GetFileName(fPath);
                // try Copying
                try {
                    File.Copy(PATH_RES + fName, fPath);
                    return true;
                }
                catch (Exception ex)
                {
                    Msg.ShowError($"(DataFile-CopyFromRes) Copying required file: {fName}", ex, "Required file is not loaded!");
                }
                return false;
            }
            return true;
        }
        // Create a directory, if not existing
        public static void CreateDir(string fPath)
        {
            try { Directory.CreateDirectory(fPath); }
            catch (Exception ex)
            {
                Msg.ShowError($"(DataFile-CreateDir) Create Folder: {fPath}", ex, "Required folder not loaded!");
            }
        }
    }
}
