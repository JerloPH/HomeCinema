#region License
/* #####################################################################################
 * LICENSE - GPL v3
* HomeCinema - Organize your Movie Collection
* Copyright (C) 2020  JerloPH (https://github.com/JerloPH)

* This program is free software: you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation, either version 3 of the License, or
* (at your option) any later version.

* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.

* You should have received a copy of the GNU General Public License
* along with this program.  If not, see <https://www.gnu.org/licenses/>.
##################################################################################### */
#endregion
using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAPICodePack.Shell;
using HomeCinema.SQLFunc;
using System.Text;

namespace HomeCinema
{
    public static class GlobalVars
    {
        // Variables ############################################################################################################
        public static string HOMECINEMA_NAME = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyTitleAttribute>().Title;
        public static string HOMECINEMA_VERSION = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public static int HOMECINEMA_BUILD = 41; // build use for updater
        public static int HOMECINEMA_DBVER = 5; // for database, and data changes

        public static string PREFIX_MOVIEINFO = "movieInfo";
        public static string CAPTION_DIALOG = HOMECINEMA_NAME + " v" + HOMECINEMA_VERSION;
        public static bool HAS_TMDB_KEY { get; set; } = false;
        public static string MSG_NO_TMDB = "No TMDB Key!\nSome features are disabled";
        public static bool DEBUGGING { get; set; } = false;

        // Links for external websites
        public static string TMDB_KEY = "";
        public static string LINK_IMDB = "https://www.imdb.com/title/";
        public static string LINK_ANILIST = "https://anilist.co/anime/";
        public static string LINK_YT = "https://www.youtube.com/watch?v=";

        // Paths for Files and Folders
        public static string PATH_START = AppContext.BaseDirectory;
        public static string PATH_RES = Path.Combine(PATH_START, "Resources") + "\\";
        public static string PATH_IMG = Path.Combine(PATH_START, "covers") + "\\";
        public static string PATH_DATA = Path.Combine(PATH_START, "data") + "\\";
        public static string PATH_TEMP = Path.Combine(PATH_START, "temp") + "\\";
        public static string PATH_LOG = Path.Combine(PATH_START, "logs");

        public static string FILE_ICON = PATH_RES + @"HomeCinema.ico"; // Icon
        public static string FILE_DEFIMG = PATH_IMG + @"0.jpg"; // default cover image

        public static string FILE_LOG_APP = Path.Combine(PATH_LOG, "App_Log.log");// Log all messages and actions
        public static string FILE_LOG_ERROR = Path.Combine(PATH_LOG, "App_ErrorLog.log"); // Contains only error Messages
        public static string FILE_LOG_DB = Path.Combine(PATH_LOG, "App_DB.log"); // Log all messages and actions for db-related

        // Data and files
        public static string FILE_CONFIG = Path.Combine(PATH_DATA, "config.json"); // configuration file to use for APIs
        public static string FILE_SETTINGS = Path.Combine(PATH_DATA, "settings.json"); // settings used in App
        public static string FILE_COUNTRY = Path.Combine(PATH_DATA, "country.hc_data"); // list of countries
        public static string FILE_GENRE = Path.Combine(PATH_DATA, "genre.hc_data"); // List of genres
        public static string FILE_NOTRAILER = Path.Combine(PATH_DATA, "NoTrailer.jpg"); // default picture if no trailer link
        public static string FILE_MEDIALOC = Path.Combine(PATH_DATA, "medialocation.hc_data"); // For movies, folder locations
        public static string FILE_MEDIA_EXT = Path.Combine(PATH_DATA, "media_ext.hc_data"); // Extensions to check for movies

        public static Icon HOMECINEMA_ICON = new Icon(FILE_ICON); // Icon as a resource, used by forms

        // Database Vars
        public static string[] DB_INFO_CATEGORY = new string[] { "None", "Movie", "TV Series", "Anime Movie", "Anime Series", "Animated Movie", "Cartoon Series" };

        // For the items in frmMain media listview
        public static ImageList MOVIE_IMGLIST = new ImageList();
        public static Font TILE_FONT = new Font("Calibri", 10f);

        // List objects
        public static List<string> MOVIE_EXTENSIONS = new List<string>(); // Supported extensions
        public static List<MediaLocations> MEDIA_LOC = new List<MediaLocations>(); // Folders to search media files from

        // Filter for OpenDialogs
        public static string FILTER_VIDEO = "MP4 Video files (*.mp4)|*.mp4";

        // String Arrays for Controls
        public static string[] TEXT_COUNTRY = { "" };
        public static string[] TEXT_GENRE = { "" };

        // FORMS
        public static Form formSetting = null; // Check if settings is already open
        public static Form formAbout = null;

        // Other objects
        public static JsonSerializerSettings JSON_SETTING = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        //######################################################################################################## Functions
        /// <summary>
        /// Log messages to text file
        /// </summary>
        /// <param name="filePath">full filepath of log file</param>
        /// <param name="codefrom">form or method that calls</param>
        /// <param name="log">string to log</param>
        public static void Log(string filePath, string codefrom, string log)
        {
            string toLog = log;
            if (!String.IsNullOrWhiteSpace(TMDB_KEY))
            {
                toLog = toLog.Replace(TMDB_KEY, "TMDB_KEY");
            }
            if (!File.Exists(filePath)) { WriteToFile(filePath, ""); }
            try
            {
                using (StreamWriter w = File.AppendText(filePath))
                {
                    w.Write(LogFormatted(codefrom, toLog));
                }
            }
            catch (Exception ex)
            {
                ShowError("GlobalVars-Log", ex, false);
            }
        }
        /// <summary>
        /// Format string for logging
        /// </summary>
        /// <param name="codefrom">form or method that calls</param>
        /// <param name="logMessage">string to format</param>
        /// <returns>Formatted message with time/date</returns>
        public static string LogFormatted(string codefrom, string logMessage)
        {
            try
            {
                return ($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff")}]: [{ codefrom }] { logMessage }\n");
            }
            catch { return $"[Unknown DateTime][{ codefrom }] { logMessage }\n"; }
        }
        /// <summary>
        /// Log database-related functions, to text file.
        /// </summary>
        /// <param name="codefrom">form or method that calls</param>
        /// <param name="log">string to log</param>
        public static void LogDb(string codefrom, string log)
        {
            Log(FILE_LOG_DB, codefrom, log);
        }
        /// <summary>
        /// Log text to App_Log.log file
        /// </summary>
        /// <param name="codefrom">form or method that calls</param>
        /// <param name="log">string to log</param>
        public static void Log(string codefrom, string log)
        {
            Log(FILE_LOG_APP, codefrom, log);
        }
        /// <summary>
        /// LOG Error Message to App_ErrorLog.log
        /// </summary>
        /// <param name="codefrom">Method caller</param>
        /// <param name="log">string to log</param>
        public static void LogErr(string codefrom, string log)
        {
            Log(FILE_LOG_ERROR, codefrom, log);
        }
        public static void LogDebug(string log)
        {
            if (!DEBUGGING) { return; }
            Log(Path.Combine(PATH_LOG, "DEBUG.log"), "", log);
        }
        /// <summary>
        /// Show default message box from Windows
        /// </summary>
        /// <param name="msg">Message</param>
        /// <param name="caption">Caption</param>
        /// <param name="mbbtn">Buttons</param>
        /// <param name="mbIcon">Icon</param>
        public static void ShowMsg(string msg, string caption, MessageBoxButtons mbbtn, MessageBoxIcon mbIcon)
        {
            try {  MessageBox.Show(new Form { TopMost = true }, msg, caption, mbbtn, mbIcon); }
            catch (Exception ex) { ShowError("GlobalVars-ShowMsg", ex, false); }
        }
        public static void ShowNoParent(string msg, string caption = "HomeCinema")
        {
            var form = new frmAlert(msg, caption, 0, false);
            form.TopMost = true;
            form.ShowDialog();
            form.Dispose();
        }
        public static void ShowInfo(string msg, string caption = "", Form parent = null)
        {
            try
            {
                Form caller = (parent == null) ? Program.FormMain : parent;
                if (Program.FormMain == null)
                {
                    ShowNoParent(msg, caption);
                    return;
                }
                if (caller == Program.FormMain)
                {
                    if (Program.FormMain.InvokeRequired)
                    {
                        Program.FormMain.BeginInvoke((Action) delegate
                        {
                            var form = new frmAlert(msg, caption, 0, true);
                            form.ShowDialog(caller);
                        });
                    }
                    else
                    {
                        var form = new frmAlert(msg, caption, 0, true);
                        form.ShowDialog(caller);
                    }
                }
                else
                {
                    if (caller.InvokeRequired)
                    {
                        caller.BeginInvoke((Action) delegate
                        {
                            var form = new frmAlert(msg, caption, 0, true);
                            form.ShowDialog(caller);
                        });
                    }
                    else
                    {
                        var form = new frmAlert(msg, caption, 0, true);
                        form.ShowDialog(caller);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("GlobalVars-ShowInfo", ex, false);
                ShowNoParent(msg, caption);
            }
        }
        public static void ShowWarning(string msg, string caption = "", Form parent = null)
        {
            ShowInfo(msg, caption, parent);
        }
        public static void ShowError(string codeFrom, Exception error, bool ShowAMsg = true, Form parent = null)
        {
            if (error == null) { return; }
            string err = "";
            try
            {
                err = $"Source: {error.Source?.ToString()}\n\tError string:\n\t{error.ToString()}";
            }
            catch
            {
                err = $"Error string:\n\t{error}";
            }
            string file = FILE_LOG_ERROR;
            LogErr(codeFrom, err);

            if (ShowAMsg)
            {
                ShowInfo($"An error occured!\nError message: {err}\nError File Location:\n{file}", "Error occured!", parent);
                // Open file in explorer
                try
                {
                    Process.Start("explorer.exe", @"/select," + $"{ file }" + '"');
                }
                catch (Exception ex)
                {
                    ShowWarning($"Cannot open folder containing error file!\n{ex.ToString()}", "Error occured", parent);
                }
            }
        }
        public static bool ShowYesNo(string msg, Form caller = null)
        {
            try
            {
                if (caller == null) { caller = Program.FormMain; }
                return (new frmAlert(msg, CAPTION_DIALOG, 1).ShowDialog(caller) == DialogResult.Yes);
                //return (MessageBox.Show(msg, CAPTION_DIALOG, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes);
            }
            catch (Exception ex)
            {
                ShowError("GlobalVars-ShowYesNo", ex, false);
            }
            return false;
        }
        // Check Log File if exceed limit and delete it
        public static void CheckLogFile(string logFile, string calledFrom, string log)
        {
            if (File.Exists(logFile))
            {
                try
                {
                    FileInfo f = new FileInfo(logFile);
                    if (f.Length > Settings.MaxLogSize)
                    {
                        File.Delete(logFile); // Delete LogFile permanently
                        Log(calledFrom, log);
                    }
                }
                catch (Exception ex)
                {
                    // Log error
                    ShowError(calledFrom, ex, false);
                }
            }
        }
        // Check all FILES on Startup
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
            if (!File.Exists(FILE_LOG_APP)) { WriteToFile(FILE_LOG_APP, ""); }
            if (!File.Exists(FILE_LOG_ERROR)) { WriteToFile(FILE_LOG_ERROR, ""); }
            if (!File.Exists(FILE_LOG_DB)) { WriteToFile(FILE_LOG_DB, ""); }
            // Default config
            if (!File.Exists(FILE_CONFIG))
            {
                var json = JsonConvert.SerializeObject(new ConfigJson(), Formatting.Indented);
                WriteToFile(FILE_CONFIG, json);
            }
            else
            {
                var jsonContent = ReadStringFromFile(FILE_CONFIG, "GlobalVar-CheckAllFiles");
                var config = JsonConvert.DeserializeObject<ConfigJson>(jsonContent, JSON_SETTING);
                if (!String.IsNullOrWhiteSpace(config.TmdbApiKey))
                {
                    TMDB_KEY = config.TmdbApiKey;
                }
            }
            // Contains TMDB_KEY ?
            HAS_TMDB_KEY = !String.IsNullOrWhiteSpace(TMDB_KEY);
            DEBUGGING = Debugger.IsAttached;
        }
        // Create a directory, if not existing
        public static void CreateDir(string fPath)
        {
            try
            {
                Directory.CreateDirectory(fPath);
            }
            catch (Exception ex)
            {
                ShowError("(GlobalVars-CreateDir) Create Folder: " + fPath, ex);
            }
        }
        // Copy from Resources if not on Root
        public static bool CopyFromRes(string fPath)
        {
            if (!File.Exists(fPath))
            {
                string fName = Path.GetFileName(fPath);
                // try Copying
                try
                {
                    File.Copy(PATH_RES + fName, fPath);
                    return true;
                }
                catch (Exception ex)
                {
                    ShowError("(GlobalVars-CopyFromRes) Copying required files error. File: " + fName, ex, false);
                }
            }
            return false;
        }
        // Load Media Location file
        public static void LoadMediaLocations()
        {
            string medialocContent = ReadStringFromFile(FILE_MEDIALOC, "frmMain");
            if (!String.IsNullOrWhiteSpace(medialocContent))
            {
                MEDIA_LOC?.Clear();
                var arr = medialocContent.Split('|');
                foreach (string arrFolder in arr)
                {
                    var arr2 = arrFolder.Split('*');
                    if (arr2.Length > 2)
                    {
                        MEDIA_LOC.Add(new MediaLocations(arr2[0], arr2[1], arr2[2]));
                    }
                }
            }
        }
        // Read String From File
        public static string ReadStringFromFile(string localFile, string calledFrom)
        {
            string ret = "";
            string errFrom = "GlobalVars-ReadStringFromFile (" + calledFrom + ")";
            Log(errFrom, "Reading File: " + localFile);
            try
            {
                using (StreamReader r = new StreamReader(localFile))
                {
                    ret = r.ReadToEnd();
                }
            }
            catch (FileNotFoundException)
            {
                return "";
            }
            catch (Exception ex)
            {
                ShowError(errFrom, ex, false);
            }
            return ret;
        }
        // Write to TextFile
        public static void WriteAppend(string fName, string toWrite, bool newline = true)
        {
            using (StreamWriter w = File.AppendText(fName))
            {
                w.Write(toWrite + (newline ? "\n" : ""));
            }
        }
        // Write to TextFile
        public static bool WriteToFile(string fName, string toWrite)
        {
            try
            {
                using (StreamWriter w = File.CreateText(fName))
                {
                    w.Write(toWrite);
                }
                return true;
            }
            catch (Exception ex)
            {
                ShowError("GlobalVars-WriteToFile", ex, false);
            }
            return false;
        }
        // Write Array to File
        public static bool WriteArray(string[] arr, string FilePath)
        {
            try
            {
                // Variable to hold the string to write to file
                string sw = "";

                // Trim whitespaces
                for (int i=0; i<arr.Length; i++)
                {
                    arr[i] = arr[i].Trim();
                }

                // Remove Duplicates
                string[] arrFixed = new HashSet<string>(arr).ToArray();

                // Sort Alphabetically
                var sorted = arrFixed.OrderBy(items => items, StringComparer.OrdinalIgnoreCase);

                // Get the string, joined
                sw = String.Join(",", sorted.ToArray());
                return WriteToFile(FilePath, sw);
            }
            catch (Exception ex)
            {
                ShowError("GlobalVars-WriteArray", ex, false);
            }
            return false;
        }
        public static bool WriteListBoxToFile(string filepath, ListBox lb = null, string sep = "*")
        {
            try
            {
                var list = lb.Items.Cast<String>().ToList();
                var toWrite = (list.Count > 0) ? list.Aggregate((a, b) => a + sep + b) : "";
                return WriteToFile(filepath, toWrite);
            }
            catch { return false; }
        }
        // Build directory string array from file
        public static string[] BuildDirArrFromFile(string fileToread, string calledFrom, char sep = '*')
        {
            // Try build array
            string errFrom = "GlobalVars-BuildDirArrFromFile";
            try
            {
                string x = ReadStringFromFile(fileToread, $"{errFrom} [calledFrom: {calledFrom}]");
                if (!String.IsNullOrWhiteSpace(x))
                {
                    return x.Split(sep);
                }
            }
            catch (Exception ex)
            {
                ShowError($"{errFrom} [calledFrom: {calledFrom}]", ex);
            }
            return new string[0];
        }
        // Build string array from File Text
        public static string[] BuildArrFromFile(string fileToread, string calledFrom, char sep = ',')
        {
            // Try build array
            string errFrom = "GlobalVars-BuildArrFromFile";
            try
            {
                string x = ReadStringFromFile(fileToread, $"{errFrom} [calledFrom: {calledFrom}]");

                // Cut all unnecessary strings
                x = x.TrimEnd(',');
                x = x.Trim();
                if (!String.IsNullOrWhiteSpace(x))
                {
                    string[] tmpCat = x.Split(sep);

                    // Remove duplicates
                    tmpCat = new HashSet<string>(tmpCat, StringComparer.OrdinalIgnoreCase).ToArray();

                    // Sort Alphabetically
                    string[] ret = tmpCat.OrderBy(item => item, StringComparer.Ordinal).ToArray();

                    // Check strings from array
                    for (int i = 0; i < tmpCat.Length; i++)
                    {
                        string c = tmpCat[i].Trim();
                        if (String.IsNullOrWhiteSpace(c) == false)
                        {
                            ret[i] = c.ToString();
                        }
                    }
                    return ret;
                }
            }
            catch (Exception ex)
            {
                ShowError($"{errFrom} [calledFrom: {calledFrom}]", ex);
            }
            return new string[0];
        }
        // Validation for Strings or Int
        public static string ValidateZero(int param)
        {
            return String.Format("{0:0000}", param);
        }
        public static string ValidateNum(long num, int pad = 2)
        {
            return ValidateNum(num.ToString(), pad);
        }
        public static String ValidateNum(string param, int pad = 2)
        {
            return param.PadLeft(pad, '0');
        }
        public static String ValidateEmptyOrNull(String param)
        {
            return (String.IsNullOrWhiteSpace(param)) ? "" : param.Replace("'", "''");
        }
        // Return [none] if string is Empty
        public static String ValidateAndReturn(String param)
        {
            return (String.IsNullOrWhiteSpace(param)) ? " (None)" : param.Trim();
        }
        // Get string formatted on picture filepath
        public static string ImgFullPath(string movieID)
        {
            return PATH_IMG + movieID + ".jpg";
        }
        // Return valid Picture filepath string
        public static string ImgFullPathWithDefault(string movieID)
        {
            string path = PATH_IMG + movieID + ".jpg";
            if (File.Exists(path))
            {
                return path;
            }
            return (File.Exists(PATH_IMG + "0.jpg")) ? PATH_IMG + "0.jpg" : PATH_RES + "0.jpg";
        }
        // Get Image Key from ImageList
        public static string ImgGetKey(string MovieID)
        {
            string imgKey = MovieID + ".jpg";
            return (MOVIE_IMGLIST.Images.ContainsKey(imgKey)) ? imgKey : "0.jpg";
        }
        // Get Image from IMG LIST
        public static Image ImgGetImageFromList(string MOVIE_ID)
        {
            try
            {
                int key = MOVIE_IMGLIST.Images.IndexOfKey(MOVIE_ID + ".jpg");
                key = (key < 1) ? 0 : key;
                return MOVIE_IMGLIST.Images[key];
            }
            catch { return null; }
        }
        // Delete Image from ImageList, with thread safety
        private delegate bool DeleteImageFromListDelegate(Form parent, string movieID, string logFrom);
        public static bool DeleteImageFromList(Form parent, string movieID, string logFrom)
        {
            string errFrom = "GlobalVars-DeleteImageFromList";
            if (parent.InvokeRequired)
            {
                return (bool)parent.Invoke(new DeleteImageFromListDelegate(DeleteImageFromList), new object[] { parent, movieID, logFrom });
            }
            else
            {
                string image_key = movieID + ".jpg";
                int index = MOVIE_IMGLIST.Images.IndexOfKey(image_key);
                if (index > 0)
                {
                    Image prevImg = MOVIE_IMGLIST.Images[index];
                    if (prevImg != null)
                    {
                        Log($"{errFrom} - { logFrom } [Delete Image]", image_key);
                        prevImg.Dispose();
                        MOVIE_IMGLIST.Images.RemoveByKey(image_key);
                        return true;
                    }
                    Log($"{errFrom} - { logFrom } [Delete Image]", image_key + " [Index exists but Image object is null]");
                }
                Log($"{errFrom} - { logFrom } [Delete Image]", image_key + " [No such Image index on the list]");
            }
            return false;
        }
        // Get Category text from Int in DB
        public static string GetCategory(string cat)
        {
            if (Convert.ToInt32(cat) < 1)
            {
                return DB_INFO_CATEGORY[0];
            }
            if (Convert.ToInt32(cat) < DB_INFO_CATEGORY.Length)
            {
                return DB_INFO_CATEGORY[Convert.ToInt32(cat)];
            }
            else
            {
                return DB_INFO_CATEGORY[0];
            }
        }
        // Get All Files on single FOLDER, include SUBFOLDERS (FULL Path WITHOUT Final BACKSLASH)
        public static List<String> SearchFilesSingleDir(string sDir, string errFrom, bool recursive = true)
        {
            List<String> files = new List<String>();
            try
            {
                foreach (string f in Directory.GetFiles(sDir))
                {
                    files.Add(f);
                }
                if (recursive)
                {
                    foreach (string d in Directory.GetDirectories(sDir))
                    {
                        files.AddRange(SearchFilesSingleDir(d, errFrom));
                    }
                }
            }
            catch (Exception excpt)
            {
                ShowError(errFrom, excpt, false);
            }
            return files;
        }
        // Get all folders inside a directory
        public static List<string> SearchFoldersFromDirectory(string sDir, string errFrom)
        {
            List<string> list = new List<string>();
            string directory = sDir.TrimEnd('\\');
            try
            {
                foreach (string foldertoAdd in Directory.GetDirectories(directory))
                {
                    if (Directory.Exists(foldertoAdd))
                    {
                        list.Add(foldertoAdd); // Add to list
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"GlobalVars-SearchFoldersFromDirectory(), called by: {errFrom}", ex, false);
            }
            return list;
        }
        // Remove Newline characters from string
        public static string RemoveLine(string s)
        {
            return Regex.Replace(s, @"\t|\n|\r", "");
        }
        // Create / Open frmMovieInfo form [EDIT Movie details]
        public static bool OpenFormMovieInfo(Form formParent, string formName, string MOVIE_ID, string From, ListViewItem lvItem = null)
        {
            Form fc = Application.OpenForms[formName];
            if (fc != null)
            {
                fc.Focus();
                return true;
            }
            else
            {
                Form formNew = new frmMovieInfo(formParent, MOVIE_ID, formName, lvItem);
                Log($"{From} (PARENT: {formParent.Name})", "childOfThis formName: " + formNew.Name);
                return false;
            }
        }
        // Try to delete file
        public static bool TryDelete(string fName, string calledFrom)
        {
            string errFrom = "GlobalVars-TryDelete";
            if (File.Exists(fName))
            {
                try
                {
                    File.Delete(fName);
                    return true;
                }
                catch (Exception ex)
                {
                    ShowError($"{errFrom} [calledFrom: {calledFrom}]", ex, false);
                }
            }
            return false;
        }
        public static string GetAFile(string Title, string filter, string InitialDir)
        {
            string ret = "";
            OpenFileDialog selectFile = new OpenFileDialog
            {
                InitialDirectory = InitialDir,
                Filter = filter,
                Title = Title,
                CheckFileExists = true,
                CheckPathExists = true,
                RestoreDirectory = true,
                Multiselect = false
            };
            selectFile.ShowDialog();
            if (String.IsNullOrWhiteSpace(selectFile.FileName) == false)
            {
                ret = selectFile.FileName;
            }
            selectFile.Dispose();
            return ret;
        }
        // Check if there is an active Internet connection
        public static bool CheckConnection(String URL, int timeOutSec = 3)
        {
            if (String.IsNullOrWhiteSpace(URL)) { return false; }
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Timeout = timeOutSec * 1000;
                request.Credentials = CredentialCache.DefaultNetworkCredentials;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                bool ret = (response.StatusCode == HttpStatusCode.OK);
                response.Dispose();
                return ret;
            }
            catch (Exception ex)
            {
                ShowError($"(GlobalVars-CheckConnection)\nURL: {URL}\n", ex, false);
                return false;
            }
        }
        // Download file from the web
        public static int DownloadFrom(string link, string saveTo, bool showAMsg = true)
        {
            string errFrom = "GlobalVars-DownloadFrom";
            using (var client = new WebClient())
            {
                try
                {
                    client.DownloadFile(link, saveTo);
                    Thread.Sleep(5);
                    return 200;
                }
                catch (WebException wex)
                {
                    // Return status code error
                    try
                    {
                        HttpStatusCode status = ((HttpWebResponse)wex.Response).StatusCode;
                        return Convert.ToInt32(status);
                    }
                    catch (Exception exw)
                    {
                        // Log Error and Exit
                        ShowError(errFrom, exw, showAMsg);
                        return -1;
                    }
                }
                catch (Exception ex)
                {
                    ShowError(errFrom, ex, showAMsg);
                }
            }
            return 0;
        }
        // Keep downloading file until successful, except during certain status codes
        public static bool DownloadLoop(string filePath, string urlFrom, string calledFrom, bool showAMsg = false)
        {
            if (string.IsNullOrWhiteSpace(urlFrom)) { return false; }
            // Keep downloading
            string errFrom = $"GlobalVars-DownloadLoop [calledFrom: {calledFrom}]";
            int DLStatus = 0;
            int retry = 5;
            while (retry > 0)
            {
                Log(errFrom, $"Downloading file: {urlFrom}, (retry left: {retry-1})");
                DLStatus = DownloadFrom(urlFrom, filePath, showAMsg);
                if (File.Exists(filePath) || DLStatus==404) // File download success || File not found on server
                {
                    retry = -1;
                    break;
                }
                retry -= 1;
            }
            return (DLStatus == 200);
        }
        // Download a File, replacing prev file, and Keep trying to download it
        public static bool DownloadAndReplace(string filePath, string urlFrom, string calledFrom, bool showAMsg = false)
        {
            string errFrom = $"GlobalVars-DownloadAndReplace [calledFrom: {calledFrom}]";
            // Delete previous file
            if (File.Exists(filePath))
            {
                TryDelete(filePath, errFrom);
            }
            return DownloadLoop(filePath, urlFrom, errFrom, showAMsg);
        }
        // Move file to RecycleBin, instead of permanent delete
        public static bool DeleteMove(string file, string errFrom)
        {
            try
            {
                // Send to Recycle Bin
                if (File.Exists(file))
                {
                    Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(file, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                ShowWarning("File cannot be moved to Recycle Bin!\n" + file);
                ShowError($"GlobalVars-DeleteMove ({errFrom})", ex, false);
            }
            return false;
        }
        // Delete Files from a folder (including subfolder), with specified file extension
        public static bool DeleteFilesExt(string directory, string extension, string calledFrom)
        {
            string errFrom = "GlobalVars-DeleteFilesExt [" + calledFrom + "]";
            string text = extension.ToCharArray()[0] == '.' ? extension.ToLower() : "." + extension.ToLower();
            List <String> items = SearchFilesSingleDir(directory, errFrom);
            if (items.Count > 0)
            {
                foreach (string file in items)
                {
                    if (Path.GetExtension(file).ToLower() == text)
                    {
                        DeleteMove(file, errFrom);
                    }
                }
                // cleanup
                items.Clear();
                return true;
            }
            return false;
        }
        // Auto-check for updates
        public static void CheckForUpdate(Form parent = null, bool showMsg = false)
        {
            string errFrom = "GlobalVars - CheckForUpdate";
            Form caller = (parent == null) ? Program.FormMain : parent;
            int UpdateStatus = 0; // 0-default, 1=update, 2=latest ver, 3=error
            string fileName = PATH_TEMP + "version";
            string link = @"https://raw.githubusercontent.com/JerloPH/HomeCinema/master/data/version";
            string linkRelease = @"https://github.com/JerloPH/HomeCinema/releases";
            int tryCount = 3;

            try
            {
                frmLoading form = new frmLoading("Checking for Update..", "Update check");
                form.BackgroundWorker.DoWork += (sender1, e1) =>
                {
                    Log(errFrom, "Will Check for Updates..");

                    if (File.Exists(fileName))
                    {
                        TryDelete(fileName, errFrom);
                    }
                    // Keep trying to download version file, to check for update
                    while (tryCount > 0)
                    {
                        Log(errFrom, $"Fetching update version.. (Tries Left: {tryCount.ToString()})");
                        DownloadFrom(link, fileName, false);
                        tryCount -= 1;
                        tryCount = File.Exists(fileName) ? 0 : tryCount;
                    }
                    // Done downloading version file
                    if (File.Exists(fileName))
                    {
                        string vString = ReadStringFromFile(fileName, errFrom);
                        int version;

                        try { version = Convert.ToInt32(vString); }
                        catch { version = 0; }

                        if (version > HOMECINEMA_BUILD)
                        {
                            form.SetIcon((int)HCIcons.Check);
                            form.Message = "Update available!";
                            Log(errFrom, "Update found!");
                            UpdateStatus = 1;
                        }
                        else
                        {
                            form.SetIcon((int)HCIcons.Check);
                            form.Message = "No updates available!";
                            UpdateStatus = 2;
                        }
                    }
                    else
                    {
                        form.SetIcon((int)HCIcons.Warning);
                        form.Message = "Error on checking update!";
                        Log(errFrom, "Cannot check for update!");
                        UpdateStatus = 3;
                    }
                };
                form.ShowDialog(caller);
            }
            catch { UpdateStatus = 0; }

            switch (UpdateStatus)
            {
                case 1:
                    // there is an update, goto page of releases
                    if (ShowYesNo("There is an update!\nGo to Download Page?\nNOTE: It will open a Link in your\nDefault Web Browser", caller))
                    {
                        try
                        {
                            Process.Start(linkRelease);//Process.Start("chrome.exe", linkRelease);
                        }
                        catch (Exception ex)
                        {
                            ShowWarning("Update Error!\nTry Updating Later..", "Error occured during update", caller);
                            ShowError(errFrom, ex, false);
                        }
                    }
                    break;
                case 2:
                    if (showMsg)
                    {
                        ShowInfo("You are using the latest version!", "Update check", caller);
                    }
                    break;
                default:
                    if (showMsg)
                    {
                        ShowWarning("Cannot check for update!\nTry again later", "Update error", caller);
                    }
                    break;
            }
        }
        // Play media file / Open it in default player
        public static void PlayMedia(string MOVIE_FILEPATH)
        {
            string errFrom = "GlobalVars-PlayMedia";
            try
            {
                FileAttributes attr = File.GetAttributes(MOVIE_FILEPATH);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    FileOpeninExplorer(MOVIE_FILEPATH, errFrom);
                }
                else
                {
                    Process.Start(MOVIE_FILEPATH);
                }
            }
            catch (Exception ex)
            {
                ShowError(errFrom, ex, false);
                ShowWarning("File or folder not Found! \nIt may have been Moved or Deleted!", "File not Found!");
            }
        }
        // Return a string with Limited characters
        public static string LimitString(string text, int limit)
        {
            if (text.Length > limit)
            {
                return text.Substring(0, limit) + "...";
            }
            return text;
        }
        public static string ConvertListToString(List<string> list, string sep, string calledFrom)
        {
            try
            {
                return list.Aggregate((a, b) => a + sep + b).Trim();
            }
            catch (Exception ex)
            {
                ShowError($"GlobalVar-ConvertListToString ({calledFrom})", ex, false);
                return "";
            }
        }
        // Open file in explorer, highlighted
        public static bool FileOpeninExplorer(string filePath, string calledFrom)
        {
            string errFrom = $"GlobalVars-FileOpeninExplorer [calledFrom: {calledFrom}]";
            try
            {
                Process.Start("explorer.exe", @"/select," + $"{ filePath }" + '"');
                return true;
            }
            catch (Exception ex)
            {
                ShowError(errFrom, ex, false);
            }
            return false;
        }
        // Return int category, based on various filters
        public static int GetCategoryByFilter(string genre, string country, string mediatype, string source)
        {
            if (source.Equals(HCSource.anilist))
            {
                return ((mediatype=="series" || mediatype=="tv") ? 4 : 3);
            }
            else
            {
                if (genre.ToLower().Contains("animation"))
                {
                    if (country.ToLower().Contains("japan"))
                    {
                        return ((mediatype == "series" || mediatype == "tv") ? 4 : 3);
                    }
                    else
                    {
                        return ((mediatype == "series" || mediatype == "tv") ? 6 : 5);
                    }
                }
                return ((mediatype == "series" || mediatype == "tv") ? 2 : 1);
            }
        }
        // Return string File Size abbrev
        public static string GetFileSize(string filename)
        {
            string errFrom = "GlobalVars-GetFileSize";
            try
            {
                string[] sizes = { "B", "KB", "MB", "GB", "TB" };
                double len = 0;
                int order;

                // Check if directory or a single file
                FileAttributes attr = File.GetAttributes(filename);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    // Get all files and add them to length
                    List<string> files = SearchFilesSingleDir(filename, errFrom);
                    foreach (string file in files)
                    {
                        len += new FileInfo(file).Length;
                    }
                }
                else
                {
                    len = new FileInfo(filename).Length;
                }

                order = 0;
                while (len >= 1024 && order < sizes.Length - 1)
                {
                    order++;
                    len = len / 1024;
                }

                // Adjust the format string to your preferences.
                // For example "{0:0.#}{1}" would show a single decimal place, and no space.
                return String.Format("{0:0.##} {1}", len, sizes[order]);
            }
            catch (Exception ex)
            {
                ShowError(errFrom, ex, false);
                return "Unknown";
            }
        }
        public static bool SaveMetadata(string filename, List<string> data)
        {
            frmLoading form = new frmLoading("Saving Metadata..", "Loading");
            form.BackgroundWorker.DoWork += (sender1, e1) =>
            {
                var file = ShellFile.FromFilePath(filename);

                try { file.Properties.System.Title.Value = data[0]; }
                catch { }

                try { file.Properties.System.Media.Year.Value = Convert.ToUInt16(data[1]); }
                catch { }

                try { file.Properties.System.Music.Genre.Value = data[2].Replace(", ", ",").Split(','); }
                catch { }

                try { file.Properties.System.Video.Director.Value = data[3].Replace(", ", ",").Split(','); }
                catch { }
            };
            form.ShowDialog();
            return true;
        }
        // Delete all covers not in database
        public static void CleanCoversNotInDb()
        {
            string calledFrom = "GlobalVars-CleanCoversNotInDb()";
            string filepath;
            List<string> listId = SQLHelper.DbQrySingle(HCTable.info, HCInfo.Id, calledFrom);
            List<string> listCover = SearchFilesSingleDir(PATH_IMG, calledFrom, false);
            foreach (string i in listId)
            {
                try
                {
                    // Remove element if its in listId
                    filepath = Path.Combine(PATH_IMG, $"{i}.jpg");
                    listCover.RemoveAt(listCover.IndexOf(filepath));
                }
                catch { }
            }
            // Remove 0.jpg
            try
            {
                filepath = Path.Combine(PATH_IMG, "0.jpg");
                listCover.RemoveAt(listCover.IndexOf(filepath));
            }
            catch { }
            // Delete files on the list
            foreach (string file in listCover)
            {
                DeleteMove(file, calledFrom);
            }
        }
        // Get string from InputBox
        public static string GetStringInputBox(string caption, string defaultVal = "")
        {
            var form = new Form();
            var label = new Label();
            var txtBox = new TextBox();
            var buttonOk = new Button();
            var buttonCancel = new Button();
            string value;

            form.Text = HOMECINEMA_NAME;
            label.Text = caption;
            txtBox.Text = "";

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            txtBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            txtBox.Anchor = txtBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, txtBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = (!String.IsNullOrWhiteSpace(txtBox.Text)) ? txtBox.Text.Trim() : String.Empty;
            form.Dispose();
            return (dialogResult == DialogResult.OK) ? value : defaultVal;
        }
        public static string GetStringInputBox(List<string> items, string caption = "Input item")
        {
            var form = new Form();
            var label = new Label();
            var comboBox = new ComboBox();
            var buttonOk = new Button();
            var buttonCancel = new Button();
            var font = new Font(TILE_FONT.FontFamily, 14, FontStyle.Regular);
            string value;
            int bx, by, bw, bh, formWClient;
            formWClient = 396;
            bx = 9;
            by = 20;
            bw = 372;

            form.Text = HOMECINEMA_NAME;
            label.Text = caption;
            comboBox.Text = "";

            buttonOk.Text = "OK";
            buttonCancel.Text = "CANCEL";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(bx, by, bw, 13);
            comboBox.SetBounds(bx + 3, by + 28, bw, 20);
            by = comboBox.Bottom + 16;
            bw = (int)(bw * 0.3);
            bh = 32;
            buttonOk.SetBounds((int)(formWClient*0.4), by, bw, bh);
            buttonCancel.SetBounds(buttonOk.Right + 6, by, bw, bh);

            label.AutoSize = true;
            comboBox.Anchor = comboBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            label.Font = font;
            comboBox.Font = font;
            buttonOk.Font = font;
            buttonCancel.Font = font;

            // Edit and Populate comboBox
            comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox.DropDownStyle = ComboBoxStyle.DropDown;
            foreach (string item in items)
            {
                comboBox.Items.Add(item);
            }

            form.ClientSize = new Size(formWClient, buttonOk.Bottom + 16);
            form.Controls.AddRange(new Control[] { label, comboBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;
            form.ForeColor = Settings.ColorFont;
            form.BackColor = Settings.ColorBg;
            form.Font = TILE_FONT;

            DialogResult dialogResult = form.ShowDialog();
            value = (!String.IsNullOrWhiteSpace(comboBox.Text)) ? comboBox.Text.Trim() : String.Empty;
            font?.Dispose();
            form.Dispose();
            return (dialogResult == DialogResult.OK) ? value : String.Empty;
        }
        public static void CleanAppDirectory(bool showMsg = false)
        {
            string calledFrom = "GlobalVars-CleanAppDirectory";
            frmLoading form = new frmLoading("Cleaning App..", "Loading");
            form.BackgroundWorker.DoWork += (sender1, e1) =>
            {
                form.Message = "Removing images not in database..";
                CleanCoversNotInDb();
                form.Message = "Removing temporary image files..";
                DeleteFilesExt(PATH_TEMP, ".jpg", calledFrom);
                form.Message = "Removing temporary json files..";
                DeleteFilesExt(PATH_TEMP, ".json", calledFrom);
                form.Message = "Removing logs..";
                DeleteFilesExt(PATH_LOG, ".log", calledFrom);
                DeleteFilesExt(PATH_TEMP, ".log", calledFrom);
                form.Message = "Removing old version file..";
                TryDelete(PATH_TEMP + "version", calledFrom);
                form.Message = "Done!";
            };
            form.ShowDialog();
            CheckAllFiles(); // re-check files if some are missing
            if (showMsg)
            {
                ShowInfo("Cleanup Done!");
            }
        }
        public static string ConvertListBoxToString(ListBox lb)
        {
            var list = lb.Items.Cast<String>().ToList();
            return (list.Count > 0) ? list.Aggregate((a, b) => a + "," + b) : "";
        }
        public static string TrailerLocalHtml(string sourcelink)
        {
            var sb = new StringBuilder();
            string src;
            if (String.IsNullOrWhiteSpace(sourcelink))
            {
                return "";
            }
            src = sourcelink;
            sb.Append("<html>");
            sb.Append("    <head>");
            sb.Append("        <meta name=\"viewport\" content=\"width=device-width; height=device-height;\">");
            sb.Append("    </head>");
            sb.Append("    <body marginheight=\"0\" marginwidth=\"0\" leftmargin=\"0\" topmargin=\"0\" style=\"overflow-y: hidden\">");
            sb.Append("        <object width=\"100%\" height=\"100%\">");
            sb.Append("            <param name=\"movie\" value=\"" + src + "\" />");
            sb.Append("            <param name=\"allowFullScreen\" value=\"true\" />");
            sb.Append("            <param name=\"allowscriptaccess\" value=\"always\" />");
            sb.Append("            <embed src=\"" + src + "\" type=\"application/x-shockwave-flash\"");
            sb.Append("                   width=\"100%\" height=\"100%\" allowscriptaccess=\"always\" allowfullscreen=\"true\" />");
            sb.Append("        </object>");
            sb.Append("    </body>");
            sb.Append("</html>");
            return sb.ToString();
        }
        public static string TrailerYoutubeEmbed(string code)
        {
            string url = "https://www.youtube.com/embed/" + code + "?rel=0;autoplay=1;loop=1;showinfo=0;controls=0;playlist=" + code + ";listType=playlist;autohide=1;version=3"; // " ?autoplay=1;version=3&amp;rel=0;html5=1"
            var sb = new StringBuilder();
            sb.Append(@"<style>
                iframe {
                position: absolute;
                top: 0;
                left: 0;
                width: 100 %;
                height: 100 %;
                } </style>");
            sb.Append("<html>");
            sb.Append("    <head>");
            sb.Append("        <meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge\"/>");
            sb.Append("    </head>");
            sb.Append("    <body marginheight=\"0\" marginwidth=\"0\" leftmargin=\"0\" topmargin=\"0\" style=\"overflow-y: hidden\">");
            sb.Append($"        <iframe width=\"100%\" height=\"100%\" src=\"{url}\"");
            sb.Append("         frameborder = \"0\" allow = \"autoplay; encrypted-media\" id=\"Overlayvideo\" allowfullscreen></iframe>");
            sb.Append("    </body>");
            sb.Append("</html>");

            return sb.ToString();
        }
        // ######################################################################## END - Add code above
    }
}
