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
        public static string FILE_ICON = "";
        public static string FILE_DEFIMG = "";
        public static string FILE_CONFIG = "";
        public static string FILE_SETTINGS = "";
        public static string FILE_COUNTRY = "";
        public static string FILE_GENRE = "";
        public static string FILE_NOTRAILER = "";
        public static string FILE_MEDIALOC = "";
        public static string FILE_MEDIA_EXT = "";

        // Check all FILES on Startup
        public static void Initialize()
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

                FILE_LOG_APP = Path.Combine(PATH_LOG, "App_Log.log");// Log all messages and actions
                FILE_LOG_ERROR = Path.Combine(PATH_LOG, "App_ErrorLog.log"); // Contains only error Messages
                FILE_LOG_DB = Path.Combine(PATH_LOG, "App_DB.log"); // Log all messages and actions for db-related

                FILE_ICON = PATH_RES + @"HomeCinema.ico"; // Icon
                FILE_DEFIMG = PATH_IMG + @"0.jpg"; // default cover image

                // Data and files
                FILE_CONFIG = Path.Combine(PATH_DATA, "config.json"); // configuration file to use for APIs
                FILE_SETTINGS = Path.Combine(PATH_DATA, "settings.json"); // settings used in App
                FILE_COUNTRY = Path.Combine(PATH_DATA, "country.hc_data"); // list of countries
                FILE_GENRE = Path.Combine(PATH_DATA, "genre.hc_data"); // List of genres
                FILE_NOTRAILER = Path.Combine(PATH_DATA, "NoTrailer.jpg"); // default picture if no trailer link
                FILE_MEDIALOC = Path.Combine(PATH_DATA, "medialocation.hc_data"); // For movies, folder locations
                FILE_MEDIA_EXT = Path.Combine(PATH_DATA, "media_ext.hc_data"); // Extensions to check for movies
            }
            catch (Exception ex)
            {
                GlobalVars.ShowError("DataFile-Initialize", ex, "Required files are not loaded!");
            }
        }
        // Check required files
        public static void CheckAllFiles()
        {
            // resources
            CopyFromRes(FILE_ICON);
            // covers
            CopyFromRes(FILE_DEFIMG);
            // data
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
                    GlobalVars.ShowError($"(DataFile-CopyFromRes) Copying required file: {fName}", ex, "Required file is not loaded!");
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
                GlobalVars.ShowError($"(DataFile-CreateDir) Create Folder: {fPath}", ex, "Required folder not loaded!");
            }
        }
    }
}
