﻿#region License
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

namespace HomeCinema.Global
{
    public class GlobalVars
    {
        // Variables ############################################################################################################
        public static string HOMECINEMA_NAME = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyTitleAttribute>().Title;
        public static string HOMECINEMA_VERSION = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public static int HOMECINEMA_BUILD = 30;

        public static string SEARCHBOX_PLACEHOLDER = "Type your Search query here...";

        public static string PREFIX_MOVIEINFO = "movieInfo";

        public static string CAPTION_DIALOG = HOMECINEMA_NAME + " v" + HOMECINEMA_VERSION;

        // Links for external websites
        public static string TMDB_KEY = "";
        public static string LINK_IMDB = "https://www.imdb.com/title/";
        public static string LINK_YT = "https://www.youtube.com/watch?v=";

        // Paths for Files and Folders
        public static string PATH_START = Application.StartupPath + @"\";
        public static string PATH_RES = PATH_START + @"Resources\";
        public static string PATH_IMG = PATH_START + @"covers\";
        public static string PATH_DATA = PATH_START + @"data\";
        public static string PATH_TEMP = PATH_START + @"temp\";
        public static string PATH_GETVIDEO { get; set; } = "";
        public static string PATH_GETCOVER { get; set; } = "";

        public static string FILE_ICON = PATH_RES + @"HomeCinema.ico"; // Icon
        public static string FILE_DEFIMG = PATH_IMG + @"0.jpg"; // default cover image

        public static string FILE_LOG_APP = PATH_START + @"App_Log.log"; // Log all messages and actions
        public static string FILE_LOG_ERROR = PATH_START + @"App_ErrorLog.log"; // Contains only error Messages
        public static string FILE_SETTINGS = PATH_DATA + @"settings.json"; // settings used in App
        public static string FILE_COUNTRY = PATH_DATA + @"country.hc_data"; // list of countries
        public static string FILE_GENRE = PATH_DATA + @"genre.hc_data"; // List of genres
        public static string FILE_NOTRAILER = PATH_DATA + @"NoTrailer.jpg"; // default picture if no trailer link
        public static string FILE_MEDIALOC = PATH_DATA + @"medialocation.hc_data"; // For movies, folder locations
        public static string FILE_MEDIA_EXT = PATH_DATA + "media_ext.hc_data"; // Extensions to check for movies
        public static string FILE_SERIESLOC = PATH_DATA + "serieslocation.hc_data"; // For series, root folder.

        // Image files loaded from disk
        public static Icon HOMECINEMA_ICON = new Icon(FILE_ICON); // Icon as a resource, used by forms

        // Database Vars
        public static string DB_NAME = "HomeCinemaDB.db";
        public static string DB_PATH = PATH_START + DB_NAME;
        public static string DB_DATAPATH =  @"URI=file:" + DB_PATH;
        public static string DB_DBLOGPATH = PATH_START + @"App_DB.log"; // Log all messages and actions

        public static string DB_TNAME_INFO = "info";
        public static string DB_TNAME_FILEPATH = "filepath";
        public static string[] DB_TABLE_INFO = new string[] { "Id", "imdb", "name", "name_ep", "name_series", "season", "episode", "country", "category", "genre", "studio", "producer", "director", "artist", "year", "summary" };
        public static string[] DB_TABLE_FILEPATH = new string[] { "Id", "file", "sub", "trailer" };

        public static string[] DB_INFO_CATEGORY = new string[] { "None", "Movie", "TV Series", "Anime Movie", "Anime Series", "Animated Movie", "Cartoon Series" };

        // For the tiles in frmMain
        public static ImageList MOVIE_IMGLIST = new ImageList();
        public static int IMGTILE_WIDTH = 96;
        public static int IMGTILE_HEIGHT = 128;
        public static int IMG_WIDTH = 192;
        public static int IMG_HEIGHT = 256;
        public static Font TILE_FONT = new Font("Calibri", 10f);

        // String arrays for extensions
        public static List<string> MOVIE_EXTENSIONS = new List<string>();

        // Filter for OpenDialogs
        public static string FILTER_VIDEO = "MP4 Video files (*.mp4)|*.mp4";

        // String Arrays for Controls
        public static string[] TEXT_SORTBY = { "Sort Default", "Sort [A-Z]", "Sort by Year" };
        public static string[] TEXT_COUNTRY = { "" };
        public static string[] TEXT_GENRE = { "" };

        // Settings
        public static long BYTES = 1000000;
        public static long SET_LOGMAXSIZE { get; set; } = 1 * BYTES; // Maximum file size of Logfile. (in MB Mega Bytes).
        public static bool SET_OFFLINE { get; set; } = true; // Use [automatic online functionalities] or not.
        public static bool SET_AUTOUPDATE { get; set; } = false; // auto check for update
        public static bool SET_AUTOPLAY { get; set; } = false; // auto play movie
        public static int SET_ITEMLIMIT { get; set; } = 0; // limit the max items to query

        // FORMS
        public static Form formSetting = null; // Check if settings is already open
        public static Form formAbout = null;

//######################################################################################################## Functions 
        // Log database-related functions, to text file.
        public static void LogDb(string codefrom, string log)
        {
            try
            {
                using (StreamWriter w = File.AppendText(DB_DBLOGPATH))
                {
                    w.Write(LogFormatted(codefrom, log));
                }
            }
            catch (Exception ex)
            {
                ShowError("GlobalVars-LogDb", ex, false);
            }
        }
        // Log messages to file
        // Base Log function
        public static void Log(string filePath, string codefrom, string log)
        {
            try
            {
                using (StreamWriter w = File.AppendText(filePath))
                {
                    w.Write(LogFormatted(codefrom, log));
                }
            }
            catch (Exception ex)
            {
                ShowError("GlobalVars-Log", ex, false);
            }
        }
        public static void Log(string codefrom, string log)
        {
            Log(FILE_LOG_APP, codefrom, log);
        }
        public static string LogFormatted(string codefrom, string logMessage)
        {
            try
            {
                return ($"[{DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss:fff tt")}]: [{ codefrom }] { logMessage }\n");
            }
            catch { return $"[Unknown DateTime][{ codefrom }] { logMessage }\n"; }
        }
        // LOG Error Message, seperate from main Log
        public static void LogErr(string codefrom, string log)
        {
            // Call Base Log
            Log(FILE_LOG_ERROR, codefrom, log);
        }
        // SHOW MessageBox with different setttings
        public static void ShowMsg(string msg, string caption, MessageBoxButtons mbbtn, MessageBoxIcon mbIcon)
        {
            try
            {
                MessageBox.Show(new Form { TopMost = true }, msg, caption, mbbtn, mbIcon);
            }
            catch (Exception ex)
            {
                // Log Error
                ShowError("GlobalVars-ShowMsg", ex, false);
            }
        }
        public static void ShowInfo(string msg)
        {
            ShowMsg(msg, CAPTION_DIALOG, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void ShowWarning(string msg)
        {
            ShowMsg(msg, CAPTION_DIALOG, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void ShowWarning(string msg, string caption)
        {
            ShowMsg(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void ShowError(string codeFrom, Exception error, bool ShowAMsg = true)
        {
            string err = $"Source: {error.Source.ToString()}\n\tError string:\n\t{error.ToString()}";
            string file = FILE_LOG_ERROR;
            LogErr(codeFrom, err);

            if (ShowAMsg)
            {
                ShowMsg($"An error occured!\nError message: {err}\nError File Location:\n{file}", CAPTION_DIALOG, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Open file in explorer
                try
                {
                    Process.Start("explorer.exe", @"/select," + $"{ file }" + '"');
                }
                catch (Exception ex)
                {
                    ShowWarning($"Cannot open folder containing error file!\n{ex.ToString()}");
                }
            }
        }
        public static bool ShowYesNo(string msg)
        {
            try
            {
                return (MessageBox.Show(msg, CAPTION_DIALOG, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes);
            }
            catch (Exception ex)
            {
                ShowError("GlobalVars-ShowYesNo", ex, false);
            }
            return false;
        }
        // Run GC to Clean Memory and Res
        public static void CleanMemory(string codeFrom)
        {
            if (String.IsNullOrWhiteSpace(codeFrom) == false)
            {
                Log(codeFrom, "Forced Runs GC");
            }
            GC.Collect();
        }
        // Check Log File if exceed limit and delete it
        public static void CheckLogFile(string logFile, string calledFrom, string log)
        {
            if (File.Exists(logFile))
            {
                try
                {
                    FileInfo f = new FileInfo(logFile);
                    if (f.Length > SET_LOGMAXSIZE)
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
        // Check all FILEs on Startup
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
            CopyFromRes(FILE_SERIESLOC);

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
        public static void CopyFromRes(string fPath)
        {
            if (!File.Exists(fPath))
            {
                string fName = Path.GetFileName(fPath);
                // try Copying
                try
                {
                    File.Copy(PATH_RES + fName, fPath);
                }
                catch (Exception ex)
                {
                    ShowError("(GlobalVars-CopyFromRes) Copying required files error. File: " + fName, ex);
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
            catch (Exception ex)
            {
                ShowError(errFrom, ex);
            }
            return ret;
        }
        // Write to TextFile
        public static void WriteAppend(string fName, string toWrite)
        {
            using (StreamWriter w = File.AppendText(fName))
            {
                w.Write(toWrite);
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
                    tmpCat = new HashSet<string>(tmpCat).ToArray();

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
        // Check if the column is a numeric column
        public static bool QryColNumeric(string colName)
        {
            string s = colName.ToLower();
            return ((s == "category") || (s == "year") || (s == "season") || (s == "episode"));
        }
        // Build a query, Use WHERE or AND on Filter
        public static string QryWhere(string qry)
        {
            return (qry.Contains("WHERE") == false) ? " WHERE " : " AND ";
        }
        // Build a query, If value is string or not
        public static string QryString(string text, bool UseSingleQuote)
        {
            if (String.IsNullOrWhiteSpace(text))
            {
                return (UseSingleQuote ? "''" : "null");
            }
            else
            {
                return (UseSingleQuote ? $"'{text}'" : $"{text}");
            }
        }
        // Validation for Strings or Int
        public static string ValidateZero(int param)
        {
            return String.Format("{0:0000}", param);
        }
        public static String ValidateNum(string param)
        {
            return String.Format("{0:00}", param);
        }
        public static String ValidateEmptyOrNull(String param)
        {
            if (String.IsNullOrWhiteSpace(param))
            {
                return "";
            }
            return param.Replace("'", "''");
        }
        // Return [none] if string is Empty
        public static String ValidateAndReturn(String param)
        {
            if (String.IsNullOrWhiteSpace(param))
            {
                return " (None)";
            }
            return param.Trim();
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
            if (File.Exists(PATH_IMG + "0.jpg"))
            {
                return PATH_IMG + "0.jpg";
            }
            return PATH_RES + "0.jpg";
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
            int key = MOVIE_IMGLIST.Images.IndexOfKey(MOVIE_ID + ".jpg");
            key = (key < 1) ? 0 : key;
            return MOVIE_IMGLIST.Images[key];
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
        // Get All Files on MULTIPLE Directories
        public static List<String> SearchFilesMultipleDir(string[] dirArray, string errFrom)
        {
            List<String> files = new List<String>();
            try
            {
                foreach (string sDir in dirArray)
                {
                    foreach (string f in Directory.GetFiles(sDir.TrimEnd('\\')))
                    {
                        files.Add(f);
                    }
                    foreach (string d in Directory.GetDirectories(sDir.TrimEnd('\\')))
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
        // Return List<String> of folder directories, from "seriesloc" File
        public static List<string> GetSeriesLocations()
        {
            string errFrom = "GlobalVars-GetSeriesLocations";
            List<string> list = new List<string>();
            string[] arr = GlobalVars.BuildDirArrFromFile(FILE_SERIESLOC, errFrom, '*');
            string directory;
            try
            {
                foreach (string path in arr)
                {
                    directory = path.TrimEnd('\\');
                    if (Directory.Exists(directory))
                    {
                        foreach (string foldertoAdd in Directory.GetDirectories(directory))
                        {
                            if (Directory.Exists(foldertoAdd))
                            {
                                // Add to list
                                list.Add(foldertoAdd);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error
                ShowError(errFrom, ex, false);
            }
            return list;
        }
        // Get folder using file dialog
        public static string GetDirectoryFolder(string caption)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = caption;
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    return fbd.SelectedPath;
                }
            }
            return "";
        }
        // Remove Newline characters from string
        public static string RemoveLine(string s)
        {
            return Regex.Replace(s, @"\t|\n|\r", "");
        }
        // Create / Open frmMovieInfo form [EDIT Movie details]
        public static bool OpenFormMovieInfo(Form formParent, string formName, string MOVIE_ID, string MOVIE_NAME, string From, ListViewItem lvItem = null)
        {
            Form fc = Application.OpenForms[formName];
            if (fc != null)
            {
                fc.Focus();
                return true;
            }
            else
            {
                Form formNew = new frmMovieInfo(formParent, MOVIE_ID, MOVIE_NAME, formName, lvItem);
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
        // Invoke a method on form
        public static bool CallMethod(string FORM_NAME, string MethodName, object[] Params, string LogFrom, string LogInvokeFrom)
        {
            // Log who call this, the form in which the method is invoked from
            Log(LogFrom, "Invoked method (" + MethodName  + ") in  " + LogInvokeFrom);
            string LogExceptions = "GlobalVars-CallMethod ";

            // Refresh parent form
            Form f = Application.OpenForms[FORM_NAME];
            if (f != null)
            {
                // do something with f.Text;
                var classObj = f;
                MethodInfo method = classObj.GetType().GetMethod(MethodName, BindingFlags.Instance | BindingFlags.Public);
                try
                {
                    method.Invoke(classObj, Params);
                    return true;
                }
                catch (Exception tex)
                {
                    ShowError(LogExceptions + " (Error)", tex);
                }
            }
            return false;
        }
        // Check if there is an active Internet connection
        public static bool CheckConnection(String URL)
        {
            bool ret = false;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Timeout = 3000;
                request.Credentials = CredentialCache.DefaultNetworkCredentials;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    ret = true;
                }
                response.Dispose();
                return ret;
            }
            catch (Exception ex)
            {
                ShowError("(GlobalVars-CheckConnection)", ex, false);
                return false;
            }
        }
        // Download file from the web
        public static int DownloadFrom(string link, string saveTo, bool showAMsg = true)
        {
            string errFrom = "GlobalVars-DownloadFrom";
            using (var client = new WebClient())
            {
                if (CheckConnection(link))
                {
                    try
                    {
                        client.DownloadFile(link, saveTo);
                        Thread.Sleep(10);
                        return 1;
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
                            return 404;
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowError(errFrom, ex, showAMsg);
                    }
                }
            }
            return 0;
        }
        // Keep downloading file until successful, except during certain status codes
        public static bool DownloadLoop(string filePath, string urlFrom, string calledFrom, bool showAMsg = false)
        {
            if (string.IsNullOrWhiteSpace(urlFrom))
            {
                return false;
            }
            // Keep downloading
            string errFrom = $"GlobalVars-DownloadLoop [calledFrom: {calledFrom}]";
            int DLStatus;
            while (File.Exists(filePath) == false)
            {
                DLStatus = DownloadFrom(urlFrom, filePath, showAMsg);
                if ((DLStatus==404)  || (DLStatus == 0))
                {
                    // Error codes and meaning
                    // 0 Not connected to the internet
                    // 404 = file not Found on server
                    return false;
                }
            }
            return true;
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
                    FileSystem.DeleteFile(file, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                ShowWarning("File cannot be moved to Recycle Bin!\n" + file);
                ShowError("GlobalVars-DeleteMove (" + errFrom + ")", ex, false);
            }
            return false;
        }
        // Delete Files from a folder (including subfolder), with specified file extension
        public static bool DeleteFilesExt(string directory, string extension, string calledFrom)
        {
            string errFrom = "GlobalVars-DeleteFilesExt [" + calledFrom + "]";
            List<String> items = SearchFilesSingleDir(directory, errFrom);
            if (items.Count > 0)
            {
                foreach (string file in items)
                {
                    if (Path.GetExtension(file).ToLower() == extension)
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
        public static void CheckForUpdate()
        {
            string errFrom = "GlobalVars - CheckForUpdate";
            frmLoading form = new frmLoading("Checking for Update..", "Loading");
            form.BackgroundWorker.DoWork += (sender1, e1) =>
            {
                if ((SET_OFFLINE == false) && (SET_AUTOUPDATE))
                {
                    GlobalVars.Log(errFrom, "Will Check for Updates..");
                    string fileName = PATH_TEMP + "version";
                    string link = @"https://raw.githubusercontent.com/JerloPH/HomeCinema/master/data/version";
                    string linkRelease = @"https://github.com/JerloPH/HomeCinema/releases";
                    int tryCount = 3;

                    if (File.Exists(fileName))
                    {
                        TryDelete(fileName, errFrom);
                    }
                    // Keep trying to download version file, to check for update
                    while (tryCount > 0)
                    {
                        GlobalVars.Log(errFrom, $"Fetching update version.. (Tries Left: {tryCount.ToString()})");
                        DownloadFrom(link, fileName, false);
                        tryCount -= 1;
                        tryCount = File.Exists(fileName) ? 0 : tryCount;
                    }
                    // Done downloading version file
                    if (File.Exists(fileName))
                    {
                        GlobalVars.Log(errFrom, "Compare version..");
                        string vString = ReadStringFromFile(fileName, "GlobalVars-CheckForUpdate");
                        int version;

                        try { version = Convert.ToInt32(vString); }
                        catch { version = 0; }

                        if (version > HOMECINEMA_BUILD)
                        {
                            GlobalVars.Log(errFrom, "Update found!");
                            // there is an update, goto page of releases
                            try
                            {
                                if (ShowYesNo("There is an update!\nGo to Download Page?\nNOTE: It will open a Link in your Default Web Browser"))
                                {
                                    Process.Start(linkRelease);//Process.Start("chrome.exe", linkRelease);
                                }
                            }
                            catch (Exception ex)
                            {
                                ShowWarning("Update Error!\nTry Updating Later..");
                                ShowError(errFrom, ex, false);
                            }
                        }
                    }
                    else
                    {
                        GlobalVars.Log(errFrom, "Cannot check for update!");
                    }
                }
            };
            form.ShowDialog();
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
                GlobalVars.ShowWarning("File or folder not Found! \nIt may have been Moved or Deleted!", "File not Found!");
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
        // Return IMDB Id of Movie from TMDB, by searching movie title
        public static string GetIMDBId(string Movie_Title, string MOVIE_ID, string mediatype, bool showAMsg = false)
        {
            string ret = "";
            string errFrom = $"GlobalVars-GetIMDBId";
            // Setup vars and links
            string KEY = TMDB_KEY;
            // GET TMDB MOVIE ID
            string urlJSONgetId = @"https://api.themoviedb.org/3/search/" + mediatype + "?api_key=" + KEY + "&query=" + Movie_Title;
            string JSONgetID = PATH_TEMP + MOVIE_ID + "_id.json";
            string JSONgetImdb = "", MovieID = "";
            string JSONContents = "", urlJSONgetImdb;

            mediatype = (String.IsNullOrWhiteSpace(mediatype)) ? "movie" : mediatype;

            // Download file , force overwrite (TO GET TMDB MOVIE ID)
            if (DownloadAndReplace(JSONgetID, urlJSONgetId, errFrom, showAMsg))
            {

                // Get TMDB Movie ID from JSON File (JSONgetID) downloaded
                JSONContents = ReadStringFromFile(JSONgetID, errFrom);
                var objPageResult = JsonConvert.DeserializeObject<TmdbPageResult>(JSONContents);
                try { MovieID = objPageResult.results[0].id.ToString(); }
                catch { MovieID = ""; }
                //ShowInfo("Movie ID: " + MovieID);

                // Check if MovieID is not empty
                if (String.IsNullOrWhiteSpace(MovieID) == false)
                {
                    // GET IMDB
                    urlJSONgetImdb = @"https://api.themoviedb.org/3/" + mediatype + "/" + MovieID + "?api_key=" + KEY;
                    urlJSONgetImdb += (mediatype != "movie") ? "&append_to_response=external_ids" : ""; // Append external_ids param for non-movie
                    JSONgetImdb = PATH_TEMP + MOVIE_ID + "_imdb.json";

                    // Download file if not existing (TO GET IMDB Id)
                    if (DownloadAndReplace(JSONgetImdb, urlJSONgetImdb, errFrom, showAMsg))
                    {
                        JSONContents = ReadStringFromFile(JSONgetImdb, errFrom);
                        try
                        {
                            var objMovieInfo = JsonConvert.DeserializeObject<MovieInfo>(JSONContents);
                            ret = (mediatype == "movie") ? objMovieInfo.imdb_id : objMovieInfo.external_ids.imdb_id;
                        }
                        catch { ret = ""; }
                    }
                }
            }

            // Delete files if MOVIE_ID = dummy
            if (MOVIE_ID == "dummy")
            {
                // Clean up
                TryDelete(JSONgetID, errFrom);
                TryDelete(JSONgetImdb, errFrom);
            }
            return ret;
        }
        // Return a List<string> of informations regarding the Movie, based on IMDB Id
        public static List<string> GetMovieInfoByImdb(string IMDB_ID, string mediatype, bool showAMsg = false)
        {
            string errFrom = "GlobalVars-GetMovieInfoByImdb";
            // Setup vars and links
            List<string> list = new List<string>();
            list.AddRange(new string[] { "", "", "", "", "", "", "", "", "", "", "" });
            list[0] = ""; // json file full path
            list[1] = ""; // trailer link
            list[2] = ""; // title
            list[3] = ""; // original title
            list[4] = ""; // summary / overview
            list[5] = ""; // release date
            list[6] = ""; // poster_path
            list[7] = ""; // Actors/actresses [Artist
            list[8] = ""; // Director
            list[9] = ""; // Producer
            list[10] = ""; // origin country

            string TMDB_MovieID = "";
            // File paths
            string JSONmovieinfo = PATH_TEMP + IMDB_ID + ".json";
            string JSONfindmovie = PATH_TEMP + IMDB_ID + "_info.json";
            string JSONcrewcast = PATH_TEMP + IMDB_ID + "_crewcast.json";
            string JSONfindtrailer = PATH_TEMP + IMDB_ID + "_videos.json";
            // Links
            string urlJSONMovieInfo, urlJSONFindMovie, urlJSONcrewcast, urlJSONtrailer;

            string JSONContents;

            // If JSON File DOES not exists, Download it
            urlJSONMovieInfo = @"https://api.themoviedb.org/3/find/" + $"{ IMDB_ID }?api_key={ TMDB_KEY }&language=en-US&external_source=imdb_id";
            // JSON - MOVIE WITH GIVEN IMDB
            DownloadAndReplace(JSONmovieinfo, urlJSONMovieInfo, errFrom + " [JSONmovieinfo]", showAMsg);
            if (File.Exists(JSONmovieinfo))
            {
                try
                {
                    JSONContents = ReadStringFromFile(JSONmovieinfo, errFrom);
                    var objJson = JsonConvert.DeserializeObject<ImdbResult>(JSONContents);
                    TMDB_MovieID = (mediatype == "movie") ? objJson.movie_results[0].id : objJson.tv_results[0].id;
                }
                catch { TMDB_MovieID = ""; }
            }

            // Find main movie info
            if (!String.IsNullOrWhiteSpace(TMDB_MovieID))
            {
                // JSON - FIND USING IMDB
                urlJSONFindMovie = @"https://api.themoviedb.org/3/" + mediatype + "/" + TMDB_MovieID + "?api_key=" + TMDB_KEY;
                // FETCH TRAILER
                urlJSONtrailer = @"https://api.themoviedb.org/3/" + mediatype + "/" + TMDB_MovieID + "/videos?api_key=" + TMDB_KEY;

                // Download file, if not existing, to fetch info
                DownloadAndReplace(JSONfindmovie, urlJSONFindMovie, errFrom, showAMsg);

                // Download file, if not existing, to fetch trailer
                DownloadAndReplace(JSONfindtrailer, urlJSONtrailer, errFrom, showAMsg);

                // Get contents from JSON File, Deserialize it into MovieInfo class
                if (File.Exists(JSONfindmovie))
                {
                    // Save to list  the json file  path
                    list[0] = JSONfindmovie;

                    // Get contents of JSON file
                    string contents = ReadStringFromFile(JSONfindmovie, errFrom + " [JSONfindmovie]");

                    // Deserialize JSON and Parse contents
                    MovieInfo movie = JsonConvert.DeserializeObject<MovieInfo>(contents);
                    if (movie != null)
                    {
                        if (mediatype == "movie")
                        {
                            list[2] = movie.title; // title
                            list[3] = movie.original_title; // original title
                            list[5] = movie.release_date; // release date
                        }
                        else
                        {
                            list[2] = movie.name; // series title
                            list[3] = movie.original_name;
                            list[5] = movie.first_air_date; // release date
                        }
                        list[4] = movie.overview; // summary / overview
                        list[6] = movie.poster_path; // poster_path

                        string tmp = "";
                        foreach (ProdCom c in movie.production_companies)
                        {
                            tmp += c.origin_country += ",";
                        }
                        list[10] = tmp.TrimEnd(','); // country
                    }
                }

                // Get Trailer
                if (File.Exists(JSONfindtrailer))
                {
                    // Get contents of JSON file
                    string contents = ReadStringFromFile(JSONfindtrailer, errFrom + " [JSONfindtrailer]");
                    try
                    {
                        var objJson = JsonConvert.DeserializeObject<TmdbVideos>(contents);
                        if (objJson.results[0].site.ToLower() == "youtube")
                        {
                            list[1] = objJson.results[0].key;
                        }
                    }
                    catch
                    {
                        list[1] = "";
                    }
                }

                // Get crew and cast
                urlJSONcrewcast = @"https://api.themoviedb.org/3/" + mediatype + "/" + TMDB_MovieID + "/credits?api_key=" + TMDB_KEY;
                DownloadAndReplace(JSONcrewcast, urlJSONcrewcast, errFrom + " [JSONcrewcast]", showAMsg);
                if (File.Exists(JSONcrewcast))
                {
                    // Unparse json into object list
                    string contents = ReadStringFromFile(JSONcrewcast, errFrom + " [JSONcrewcast]");
                    CastCrew castcrew = JsonConvert.DeserializeObject<CastCrew>(contents);

                    string tmp = ""; // temporary string var. Use to get strings
                    foreach (Cast c in castcrew.cast)
                    {
                        // Get informations
                        tmp += c.name + ", ";
                    }
                    // Save to list as artist
                    list[7] = tmp.TrimEnd().TrimEnd(',');

                    // get Director and producer
                    string tmpDir = "", tmpProd = ""; // Use to get director and producer
                    foreach (Crew c in castcrew.crew)
                    {
                        // Get informations
                        if (c.job == "Director")
                        {
                            tmpDir += c.name + ", ";
                        }
                        else if (c.job == "Producer")
                        {
                            tmpProd += c.name + ", ";
                        }
                    }
                    // Save to list as director and producer
                    list[8] = tmpDir.TrimEnd().TrimEnd(',');
                    list[9] = tmpProd.TrimEnd().TrimEnd(',');
                }
            }
            return list;
        }
        // Get Genres from JSON File
        public static List<string> GetGenresByJsonFile(string json_fullpath, string calledFrom)
        {
            List<string> listGenre = new List<string>();

            // Exit if there is no jsonFile
            if ((String.IsNullOrWhiteSpace(json_fullpath)) || (File.Exists(json_fullpath) ==false))
            {
                return listGenre;
            }

            string contents = ReadStringFromFile(json_fullpath, $"GlobalVars-GetGenresByJsonFile [calledFrom: {calledFrom}]");
            if (contents.Contains("genres"))
            {
                var jObj = (JObject)JsonConvert.DeserializeObject(contents);
                var result = jObj["genres"]
                                .Select(item => new
                                {
                                    name = (string)item["name"],
                                })
                                .ToList();
                if (result.Count > 0)
                {
                    foreach (var valGenre in result)
                    {
                        string valString = valGenre.ToString();
                        string valTrim = valString.Substring(valString.IndexOf("name = ") + 7);
                        valTrim = valTrim.TrimEnd('}');
                        valTrim.Trim();
                        listGenre.Add(valTrim);
                    }
                }
            }
            return listGenre;
        }
        // Return string MOVIE name WITHOUT the special characters
        public static string TrimMovieName(string movieFileName)
        {
            // Exit if movieFileName is null
            if (String.IsNullOrWhiteSpace(movieFileName))
            {
                return "";
            }
            // Start to trim
            string mName;
            try
            {
                mName = movieFileName.Replace('_', ' ').Replace('.', ' ');
                mName = mName.ToLower();
                mName = mName.Replace("(", "");
                mName = mName.Replace(")", "");
                mName = mName.Replace("-", "");
            }
            catch (Exception ex)
            {
                // If all the trimming failed, revert back to original
                mName = movieFileName;
                // LogError
                ShowError("GlobalVars-TrimMovieName", ex, false);
            }
            return mName;
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
        public static int GetCategoryByFilter(string genre, string country, string mediatype)
        {
            if (genre.ToLower().Contains("animation"))
            {
                if (country.ToLower().Contains("japan"))
                {
                    return (mediatype == "tv" ? 4 : 3);
                }
                else
                {
                    return (mediatype == "tv" ? 6 : 5);
                }
            }
            return (mediatype == "tv" ? 2 : 1);
        }
        // Download Movie cover image from TMB
        public static bool DownloadCoverFromTMDB(string MOVIE_ID, string linkPoster, string calledFrom)
        {
            string errFrom = $"GlobalVars-DownloadCoverFromTMDB [calledFrom: {calledFrom}]";
            // Parse image link from JSON and download it
            if (String.IsNullOrWhiteSpace(linkPoster) == false)
            {
                string moviePosterDL = PATH_TEMP + MOVIE_ID + ".jpg";
                return DownloadLoop(moviePosterDL, "https://image.tmdb.org/t/p/original/" + linkPoster, errFrom, false);
            }
            return false;
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
            SQLHelper dbCon = new SQLHelper("GlobalVars");
            string calledFrom = "GlobalVars-CleanCoversNotInDb()";
            string filepath;
            List<string> listId = dbCon.DbQrySingle(DB_TNAME_INFO , DB_TABLE_INFO[0].ToString(), calledFrom);
            List<string> listCover = SearchFilesSingleDir(PATH_IMG, calledFrom, false);
            foreach (string i in listId)
            {
                // Remove element if its in listId
                filepath = Path.Combine(PATH_IMG, $"{i}.jpg");
                listCover.RemoveAt(listCover.IndexOf(filepath));
            }
            // Remove 0.jpg
            filepath = Path.Combine(PATH_IMG, "0.jpg");
            listCover.RemoveAt(listCover.IndexOf(filepath));
            foreach (string file in listCover)
            {
                DeleteMove(file, calledFrom);
            }
        }
        // ######################################################################## END - Add code above
    }
}
