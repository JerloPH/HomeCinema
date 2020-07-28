﻿/* #####################################################################################
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
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using HomeCinema.SQLFunc;
using HomeCinema.Global;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace HomeCinema
{
    public partial class frmMain : Form
    {
        bool Start = true; // Startup of App, to prevent startup event to repeat
        TimeSpan LoadingStart, LoadingEnd; // Record App load time
        SQLHelper DBCON = new SQLHelper("frmMain"); // Make an SQLite helper instance
        Form formLoading = null; // Make a form of : "Please wait while loading..."
        // Strings
        static string LVMovieItemsColumns = "[Id],[name],[name_ep],[name_series],[season],[episode],[year],[summary],[genre]";
        string SEARCH_QUERY = "";
        string SEARCH_COLS = LVMovieItemsColumns;
        string SEARCH_QUERY_PREV = "";
        string[] FOLDERTOSEARCH = { "" };
        // Objects
        ListViewColumnSorter lvSorter = new ListViewColumnSorter();
        BackgroundWorker bgWorkInsertMovie = new BackgroundWorker();
        BackgroundWorker bgSearchInDB = new BackgroundWorker();

        ToolStripItem toolMenuView, toolMenuEdit;

        public frmMain()
        {
            //Record time start
            LoadingStart = DateTime.Now.TimeOfDay;

            // Create directories
            GlobalVars.CreateDir(GlobalVars.PATH_IMG);
            GlobalVars.CreateDir(GlobalVars.PATH_DATA);
            GlobalVars.CreateDir(GlobalVars.PATH_TEMP);

            // Check files first
            GlobalVars.CheckAllFiles();

            // Start app
            InitializeComponent();

            // Form properties
            FormClosing += new FormClosingEventHandler(frmMain_FormClosing);
            Icon = GlobalVars.HOMECINEMA_ICON;

            // Change Caption and Title
            Text = $"{GlobalVars.HOMECINEMA_NAME} - Media Organizer (v {GlobalVars.HOMECINEMA_VERSION} r{GlobalVars.HOMECINEMA_BUILD.ToString()})";

            // Load App Settings
            LoadSettings();

            // Add events to controls
            txtSearch.Text = GlobalVars.SEARCHBOX_PLACEHOLDER;
            txtSearch.GotFocus += new EventHandler(SearchBoxPlaceholderClear);
            txtSearch.LostFocus += new EventHandler(SearchBoxPlaceholder);

            // Set tooltips for controls
            ToolTip ttShowNew = new ToolTip();
            ttShowNew.SetToolTip(this.btnShowNew, "Show Recently Added Movies");
            ttShowNew.SetToolTip(this.btnClean, "Clean temporary files");
            ttShowNew.SetToolTip(this.btnAddMovie, "Add Single Movie to List");
            ttShowNew.SetToolTip(this.cbSort, "Change Sorting By");
            ttShowNew.SetToolTip(this.btnChangeView, "Change List view of Items");

            // Setup Sort and SortingOrder
            cbSort.Items.AddRange(GlobalVars.TEXT_SORTBY);
            cbSort.SelectedIndex = 1;
            cbSortOrder.Items.Add("Ascending");
            cbSortOrder.Items.Add("Descending");
            cbSortOrder.SelectedIndex = 0;

            // Setup listview lvSearchResult
            lvSearchResult.ShowItemToolTips = true;
            lvSearchResult.Columns.Add("ColName");
            lvSearchResult.Columns.Add("ColSeriesName");
            lvSearchResult.Columns.Add("ColEpName"); // Either [Episode Name] OR [Season Num + Episode Num]
            lvSearchResult.Columns.Add("ColYear");
            lvSearchResult.Columns[0].Width = lvSearchResult.ClientRectangle.Width / 3;
            lvSearchResult.Columns[1].Width = lvSearchResult.ClientRectangle.Width / 3;
            lvSearchResult.Columns[2].Width = lvSearchResult.ClientRectangle.Width / 3;

            lvSearchResult.LargeImageList = GlobalVars.MOVIE_IMGLIST;
            lvSearchResult.SmallImageList = GlobalVars.MOVIE_IMGLIST;
            int sizeW = (lvSearchResult.ClientRectangle.Width / 2) - GlobalVars.IMGTILE_WIDTH;
            lvSearchResult.TileSize = new Size(sizeW, GlobalVars.IMGTILE_HEIGHT + 2); // lvSearchResult.Width - (GlobalVars.IMGTILE_WIDTH + 120)
            GlobalVars.MOVIE_IMGLIST.ImageSize = new Size(GlobalVars.IMGTILE_WIDTH, GlobalVars.IMGTILE_HEIGHT);
            GlobalVars.MOVIE_IMGLIST.ColorDepth = ColorDepth.Depth32Bit;
            lvSearchResult.AllowDrop = false;
            lvSearchResult.AllowColumnReorder = false;
            lvSearchResult.MultiSelect = false;
            foreach (ListViewItem lv in lvSearchResult.Items)
            {
                lv.Font = GlobalVars.TILE_FONT;
            }
            lvSearchResult.View = View.Tile;

            // Populate Context Menu for ListView item Rightclick
            toolMenuView = cmLV.Items.Add("&View Details"); // View Movie Info
            toolMenuEdit = cmLV.Items.Add("&Edit Information"); // Edit Movie Info
            cmLV.ItemClicked += new ToolStripItemClickedEventHandler(cmLV_ItemCLicked);

            // Populate combobox cbCategory
            cbCategory.Items.AddRange(GlobalVars.DB_INFO_CATEGORY);
            cbCategory.Items[0] = "All";
            cbCategory.SelectedIndex = 0;

            // Populate combobox cbCountry, from file
            cbCountry.Items.Add("All");
            foreach (string c in GlobalVars.BuildArrFromFile(GlobalVars.FILE_COUNTRY, "frmMain-frmMain[FILE_COUNTRY]"))
            {
                if ((String.IsNullOrWhiteSpace(c) == false) && c != "All")
                {
                    cbCountry.Items.Add(c.Trim());
                }
            }
            cbCountry.SelectedIndex = 0;

            // Populate Genre from File
            GlobalVars.TEXT_GENRE = GlobalVars.BuildArrFromFile(GlobalVars.FILE_GENRE, "frmMain-frmMain[FILE_GENRE]");
            // Populate combobox cbCategory
            cbGenre.Items.Add("All");
            foreach (string c in GlobalVars.TEXT_GENRE)
            {
                if (String.IsNullOrWhiteSpace(c) == false)
                {
                    cbGenre.Items.Add(c.Trim());
                }
            }
            cbGenre.SelectedIndex = 0;

            // Setup extensions for media files, load supported ext from file
            string[] tempMediaExt = GlobalVars.BuildArrFromFile(GlobalVars.FILE_MEDIA_EXT, "frmMain-frmMain[FILE_MEDIA_EXT]");
            string tempExtToBrowse = "";
            string extToAdd = "";
            foreach (string c in tempMediaExt)
            {
                if (String.IsNullOrWhiteSpace(c) == false)
                {
                    extToAdd = "." + c.Trim().ToLower();
                    GlobalVars.MOVIE_EXTENSIONS.Add(extToAdd);
                    tempExtToBrowse += $"*{ extToAdd };";
                }
            }
            GlobalVars.FILTER_VIDEO = "Supported Media Files|" + tempExtToBrowse;

            // Perform background worker that Automatically inserts all movies from designated folder
            // Check if directory exists first, by readling from file
            string[] tempFolder = GlobalVars.BuildArrFromFile(GlobalVars.FILE_MEDIALOC, "frmMain"); // Get directory to start search
            if (tempFolder.Length < 1)
            {
                FOLDERTOSEARCH[0] = GlobalVars.GetDirectoryFolder("Select folder to search for media files"); // Browse for Dir
                GlobalVars.WriteToFile(GlobalVars.FILE_MEDIALOC, FOLDERTOSEARCH[0]);
            }
            else
            {
                FOLDERTOSEARCH = tempFolder; // Load existing dir from file
            }

            // Add events to FORM
            KeyDown += new KeyEventHandler(Form_KeyDown);

            // Add events to BG Worker for Searching movie files in a folder
            bgWorkInsertMovie.DoWork += new DoWorkEventHandler(bgw_SearchFileinFolder);
            bgWorkInsertMovie.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_DoneSearchFileinFolder);

            // Add events to BG Worker for Searching movie in Database
            bgSearchInDB.WorkerReportsProgress = true;
            bgSearchInDB.ProgressChanged += bgwMovie_ProgressChanged;
            bgSearchInDB.DoWork += new DoWorkEventHandler(bgwMovie_SearchMovie);
            bgSearchInDB.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwMovie_DoneSearchMovie);
        }
        // ############################################################################## Database Functions
        bool InsertToDB(List<string> listofFiles, string errFrom)
        {
            string callFrom = $"frmMain ({Name})-InsertToDB-({errFrom})";

            // Insert to DB if NEW MOVIE
            // Build new string[] from INFO, FIlepath
            string[] combined = new string[GlobalVars.DB_TABLE_INFO.Length + GlobalVars.DB_TABLE_FILEPATH.Length - 2];
            Array.Copy(GlobalVars.DB_TABLE_INFO, 1, combined, 0, GlobalVars.DB_TABLE_INFO.Length - 1);
            Array.Copy(GlobalVars.DB_TABLE_FILEPATH, 1, combined, GlobalVars.DB_TABLE_INFO.Length - 1, 3);

            // Create DT
            DataTable dt = DBCON.InitializeDT(false, combined);
            foreach (string filePath in listofFiles)
            {
                DataRow row = dt.NewRow();
                row[0] = "0"; // IMDB
                row[1] = Path.GetFileNameWithoutExtension(filePath).Replace('_', ' ').Replace('.', ' '); // name
                row[2] = ""; // episode name
                row[3] = ""; // series name
                row[4] = ""; // season number
                row[5] = ""; // episode num
                row[6] = ""; // country
                row[7] = "0"; // category
                row[8] = ""; // genre
                row[9] = ""; // studio
                row[10] = ""; // producer
                row[11] = ""; // director
                row[12] = ""; // artist
                row[13] = "0"; // year
                row[14] = ""; // summary
                row[15] = filePath; // filepath
                row[16] = GetSubtitleFile(filePath); // file sub
                row[17] = ""; // trailer
                dt.Rows.Add(row);
            }
            dt.AcceptChanges();

            if (DBCON.DbInsertMovie(dt, callFrom) > 0)
            {
                return true;
            }
            return false;
        }
        // ############################################################################## Functions
        // Play Movie or Open Movie Details
        public void OpenFormPlayMovie()
        {
            // A movie/show is selected
            if (lvSearchResult.SelectedItems.Count > 0)
            {
                // Validate ID
                string ID = lvSearchResult.SelectedItems[0].Tag.ToString().TrimStart('0');
                // Exit if not a valid ID
                if (Convert.ToInt16(ID) < 1)
                {
                    return;
                }
                // Otherwise, continue

                // Just play the media
                if (GlobalVars.SET_AUTOPLAY)
                {
                    string qry = $"SELECT [Id],[file] FROM { GlobalVars.DB_TNAME_FILEPATH } WHERE [Id]={ ID }";
                    DataTable dtFile = DBCON.DbQuery(qry, "[Id],[file]", "frmMain-OpenNewFormMovie");
                    foreach (DataRow r in dtFile.Rows)
                    {
                        string filePath = r[GlobalVars.DB_TABLE_FILEPATH[1]].ToString();
                        GlobalVars.PlayMedia(filePath);
                        break;
                    }
                    dtFile.Clear();
                    dtFile.Dispose();
                    return;
                }
                else
                {
                    OpenNewFormMovie(); // Open Movie Details form
                }
            }
        }
        // Open Movie Details form
        private void OpenNewFormMovie()
        {
            // Validate ID
            string ID = lvSearchResult.SelectedItems[0].Tag.ToString().TrimStart('0');
            // Exit if not a valid ID
            if (Convert.ToInt16(ID) < 1)
            {
                return;
            }
            // Otherwise, continue
            if (lvSearchResult.SelectedItems.Count > 0)
            {
                // Create form to View Movie Details / Info
                string text = Convert.ToString(lvSearchResult.SelectedItems[0].Text);
                string formName = "movie" + ID;
                Form fc = Application.OpenForms[formName];
                if (fc != null)
                {
                    fc.Focus();
                }
                else
                {
                    Form form = new frmMovie(this, ID, text, lvSearchResult.SelectedItems[0]);
                    form.Name = formName;
                    GlobalVars.Log(Name + " (OPEN a MOVIE)", "MOVIE formName: " + form.Name);
                }
            }
        }
        // Get all Media files from folder in medialocation file
        private void getAllMediaFiles()
        {
            // Display the loading form.
            DisplayLoading();

            // BGworker for: fetching all media filepaths
            try
            {
                bgWorkInsertMovie.RunWorkerAsync();

            } catch (Exception ex)
            {
                // Show error
                GlobalVars.ShowError("frmMain-getAllMediaFiles", ex);
                // Close loading form
                CloseLoading();
            }
        }
        public void SearchBoxPlaceholder(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = GlobalVars.SEARCHBOX_PLACEHOLDER;
            }
        }
        public void SearchBoxPlaceholderClear(object sender, EventArgs e)
        {
            if (txtSearch.Text == GlobalVars.SEARCHBOX_PLACEHOLDER)
            {
                txtSearch.Text = "";
            }
        }
        // Update ListView lvSearchResult Item
        public void UpdateMovieItemOnLV(ListViewItem lvItem)
        {
            if (lvItem != null)
            {
                // Get Id
                string MOVIEID = lvItem.Tag.ToString().TrimStart('0');

                // Change info of the item
                string qry = $"SELECT {LVMovieItemsColumns} FROM {GlobalVars.DB_TNAME_INFO} WHERE [Id]={MOVIEID} LIMIT 1;";

                DataTable dtFile = DBCON.DbQuery(qry, LVMovieItemsColumns, "frmMain-UpdateMovieItemOnLV"); // run the query

                // Check if there are results
                if (dtFile.Rows.Count > 0)
                {
                    foreach (DataRow r in dtFile.Rows)
                    {
                        // Get all strings from the DataRow, passed by the BG worker
                        string r1 = r[1].ToString(); // name
                        string r2 = r[2].ToString(); // name_ep
                        string r3 = r[3].ToString(); // name_series
                        string r4 = r[4].ToString(); // season
                        string r5 = r[5].ToString(); // episode
                        string r6 = r[6].ToString(); // year
                        string r7 = r[7].ToString(); // summary
                        string r8 = r[8].ToString(); // genre

                        // Edit Information on ListView Item
                        LVItemSetDetails(lvItem, new string[] { MOVIEID.ToString(), r1, r2, r3, r4, r5, r6, r7, r8 });

                        break;
                    }
                }
                dtFile.Clear();
                dtFile.Dispose();

                // Refresh imagelist and lvSearchResult to refelct changes to Image
                lvSearchResult.Refresh();

            }
        }
        // Execute the query, by running bgWorker bgSearchInDB
        public void RefreshMovieList()
        {
            if (String.IsNullOrWhiteSpace(SEARCH_QUERY_PREV))
            {
                btnSearch.PerformClick();
                return;
            }
            else
            {
                SEARCH_QUERY = SEARCH_QUERY_PREV;

                // Display the loading form.
                DisplayLoading();

                // Run BG Worker: bgSearchInDB, for Searching movies in database
                try
                {
                    bgSearchInDB.RunWorkerAsync();

                }
                catch (Exception ex)
                {
                    // Show error
                    GlobalVars.ShowError($"frmMain-getAllMediaFiles", ex);
                    // Close loading form
                    CloseLoading();
                }

            }
        }
        // Display and close loading form
        public void DisplayLoading()
        {
            // Show loading form, if not already visible
            if (formLoading == null)
            {
                formLoading = new frmLoading(this);
                formLoading.Show(this);
            }
            else
            {
                formLoading.Visible = true;
                formLoading.Focus();
            }
        }
        public void CloseLoading()
        {
            // Close the loading form.
            if (formLoading != null)
            {
                formLoading.Dispose();
                formLoading = null;
            }

            // Set Focus to searchbox
            txtSearch.Focus();

            // Run GC to clean
            GlobalVars.CleanMemory("frmMain-CloseLoading");
        }
        // Check Settings and Load values to App
        private void LoadSettings()
        {
            // If file does not exist, create it with default values from [Config.cs]
            if (File.Exists(GlobalVars.FILE_SETTINGS) == false)
            {
                Config newconfig = new Config();
                string json = JsonConvert.SerializeObject(newconfig, Formatting.Indented);
                GlobalVars.WriteToFile(GlobalVars.FILE_SETTINGS, json);
            }
            // Load file contents to Config
            string contents = GlobalVars.ReadStringFromFile(GlobalVars.FILE_SETTINGS, "frmMain-LoadSettings");
            Config config = JsonConvert.DeserializeObject<Config>(contents);

            // Get Max log file size
            GlobalVars.SET_LOGMAXSIZE = config.logsize * GlobalVars.BYTES;
            // Get last path of poster image
            string stringVal = config.lastPathCover;
            if (String.IsNullOrWhiteSpace(stringVal) == false)
            {
                GlobalVars.PATH_GETCOVER = stringVal;
            }
            // Get last path of media file when adding new one
            string strGetVideo = config.lastPathVideo;
            if (String.IsNullOrWhiteSpace(strGetVideo) == false)
            {
                GlobalVars.PATH_GETVIDEO = strGetVideo;
            }
            // Get Offline Mode
            GlobalVars.SET_OFFLINE = Convert.ToBoolean(config.offlineMode);
            // Get auto update
            GlobalVars.SET_AUTOUPDATE = Convert.ToBoolean(config.autoUpdate);
            // AutoPlay Movie, instead of Viewing its Info / Details
            GlobalVars.SET_AUTOPLAY = Convert.ToBoolean(config.instantPlayMovie);
        }
        // Save settings to replace old
        private void SaveSettings()
        {
            Config config = new Config();
            config.logsize = (int)(GlobalVars.SET_LOGMAXSIZE / GlobalVars.BYTES);
            config.offlineMode = Convert.ToInt16(GlobalVars.SET_OFFLINE);
            config.lastPathCover = GlobalVars.PATH_GETCOVER;
            config.lastPathVideo = GlobalVars.PATH_GETVIDEO;
            config.autoUpdate = Convert.ToInt16(GlobalVars.SET_AUTOUPDATE);
            config.instantPlayMovie = Convert.ToInt16(GlobalVars.SET_AUTOPLAY);

            // Seriliaze to JSON
            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            GlobalVars.WriteToFile(GlobalVars.FILE_SETTINGS, json);
        }
        // Sort Items in lvSearchResult ListView
        private void SortItemsInListView(int toggle)
        {
            // Change Sort Order
            SortOrder Sorting = SortOrder.None;
            if (cbSortOrder.SelectedIndex > 0)
            {
                Sorting = SortOrder.Descending;
            }
            else
            {
                Sorting = SortOrder.Ascending;
            }

            // Peform sort
            switch (toggle)
            {
                case 0:
                    //GlobalVars.ShowInfo("Sorted default");
                    lvSearchResult.Sorting = Sorting;
                    lvSearchResult.ListViewItemSorter = null;
                    var items = lvSearchResult.Items.Cast<ListViewItem>().OrderBy(x => x.Tag.ToString()).ToList();
                    lvSearchResult.Items.Clear();
                    lvSearchResult.Items.AddRange(items.ToArray());
                    break;
                case 1:
                    //GlobalVars.ShowInfo("Sorted AZ");
                    lvSorter.SortColumn = 0;
                    lvSorter.Order = Sorting;
                    lvSearchResult.ListViewItemSorter = lvSorter;
                    lvSearchResult.Sort();
                    break;
                case 2:
                    //GlobalVars.ShowInfo("Sorted year");
                    lvSorter.SortColumn = 3;
                    lvSorter.Order = Sorting;
                    lvSearchResult.ListViewItemSorter = lvSorter;
                    lvSearchResult.Sort();
                    break;
            }
        }
        // Get subtitle file automatically
        public string GetSubtitleFile(string filePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string fileDir = Path.GetDirectoryName(filePath);
            string fileSub = "";
            string[] ext = { "sub", "srt", "ass" };

            foreach (string r in ext)
            {
                fileSub = fileDir + "\\" + fileName + "." + r;
                if (File.Exists(fileSub))
                {
                    return fileSub;
                }
            }
            return "";
        }
        // Set Informations and Details for LVItem on lvSearchResult
        public void LVItemSetDetails(ListViewItem temp, string[] InfoString)
        {
            // Unparse InfoString
            string MovieId = InfoString[0]; // ID
            string r1 = InfoString[1]; // name
            string r2 = InfoString[2]; // name_ep
            string r3 = InfoString[3]; // name_series
            string r4 = InfoString[4]; // season
            string r5 = InfoString[5]; // episode
            string r6 = InfoString[6]; // year
            string r7 = InfoString[7]; // summary
            string r8 = InfoString[8]; // genre

            try
            {
                // Convert MovieId string to int
                int MOVIEID = Convert.ToInt32(MovieId);

                // Set default text for item
                temp.Text = r1;

                // Append ToolTip on it
                temp.ToolTipText = "Summary: \n" + GlobalVars.LimitString(r7, 350) + "\n\nGenre:\n" + r8.Replace(",", ", ");

                // Is it a Movie? (by checking if there are no season)
                // Add sub-item for Series Name, or Episode Name
                if (String.IsNullOrWhiteSpace(r4))
                {
                    temp.SubItems.Add(r3); // Series Name
                    temp.SubItems.Add(r2); // Episode Name
                }
                else
                {
                    // Set Series Name
                    if (String.IsNullOrWhiteSpace(r3))
                    {
                        temp.Text = r3; //Set Series Name
                    }
                    temp.SubItems.Add(r2); // Episode Name
                    temp.SubItems.Add("S" + GlobalVars.ValidateNum(r4) + " E" + GlobalVars.ValidateNum(r5));
                }
                temp.SubItems.Add(r6); // Year

                // Display image (From ImageList) based on ImageKey
                temp.ImageKey = GlobalVars.ImgGetKey(MOVIEID.ToString());

                // Add year to name/title of MOVIE
                temp.Text = temp.Text + $" ({r6})";

                // Save the ID as Tag, to NOT SHOW it on LIST
                temp.Name = Convert.ToString(MOVIEID);
                temp.Tag = GlobalVars.ValidateZero(MOVIEID);

            } catch (Exception ex)
            {
                // Log error
                GlobalVars.ShowError($"frmMain-LVItemSetDetails", ex);
            }
        }
        // ############################################################################## BACKGROUND WORKERS
        private void bgwMovie_SearchMovie(object sender, DoWorkEventArgs e)
        {
            // Get query from variable, set by background worker
            string qry = SEARCH_QUERY;
            string cols = SEARCH_COLS;
            string errFrom = "frmMain-bgwMovie_SearchMovie";

            // If no query
            if (String.IsNullOrWhiteSpace(qry))
            {
                // Exit
                GlobalVars.Log(errFrom, $"Query is Empty!");
                e.Result = null;
                return;
            }

            // SET as Previous query
            SEARCH_QUERY_PREV = SEARCH_QUERY;

            // Count progress
            int progress = 0;
            BackgroundWorker worker = sender as BackgroundWorker;
            //BackgroundWorker worker = bgSearchInDB;

            // Log Query
            GlobalVars.Log(errFrom, $"START Background worker from: {Name}");
            DataTable dt = DBCON.DbQuery(qry, cols, errFrom);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    worker.ReportProgress(progress, r);
                    progress += 1;
                }
                e.Result = dt;
            }
            else
            {
                e.Result = null;
            }
        }
        private void bgwMovie_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Error log
            string errFrom = "frmMain-bgwMovie_ProgressChanged";

            // Check if progress is the start
            if (e.ProgressPercentage < 1)
            {
                // Clear previous list
                lvSearchResult.Items.Clear();

                // Log progress Start [0]
                GlobalVars.Log(errFrom + $" [First Progress] ({ e.ProgressPercentage.ToString() })", "RETRIEVES data from Previous Search query in bgWorker");
            }

            // Retrieve Progress DataRow
            try
            {
                // Get The DataRow progress
                if (e.UserState is DataRow)
                {
                    // Set the e.Result from BgWorker as DT
                    DataRow r = e.UserState as DataRow;

                    // Convert ID object to ID int
                    int MOVIEID;
                    try
                    {
                        MOVIEID = Convert.ToInt32(r[0]);
                    }
                    catch (Exception exint)
                    {
                        // Error Log
                        MOVIEID = 0;
                        GlobalVars.ShowError($"{errFrom} [MovieID obj to Int]\n\tID: { MOVIEID.ToString() }", exint, false);
                    }

                    // Add to listview lvSearchResult
                    if (MOVIEID > 0)
                    {
                        // Load 'cover' Image from 'cover' folder
                        string Imagefile = GlobalVars.ImgFullPath(MOVIEID.ToString());
                        try
                        {
                            Image imgFromFile = Image.FromFile(Imagefile);
                            GlobalVars.MOVIE_IMGLIST.Images.Add(Path.GetFileName(Imagefile), imgFromFile);

                        }
                        catch (Exception exImg)
                        {
                            // Error Log
                            GlobalVars.ShowError($"{errFrom}\n\tFile:\n\t{Imagefile}", exImg, false);
                        }

                        // Get all strings from the DataRow, passed by the BG worker
                        string r1 = r[1].ToString(); // name
                        string r2 = r[2].ToString(); // name_ep
                        string r3 = r[3].ToString(); // name_series
                        string r4 = r[4].ToString(); // season
                        string r5 = r[5].ToString(); // episode
                        string r6 = r[6].ToString(); // year
                        string r7 = r[7].ToString(); // summary
                        string r8 = r[8].ToString(); // genre

                        // Make new ListView item, and assign properties to it
                        ListViewItem temp = new ListViewItem() { Text = r1 };

                        // Edit Information on ListView Item
                        LVItemSetDetails(temp, new string[] { MOVIEID.ToString(), r1, r2, r3, r4, r5, r6, r7, r8 });

                        // Add Item to ListView lvSearchResult
                        lvSearchResult.Items.Add(temp);
                    }
                }
            }
            catch (Exception exc)
            {
                GlobalVars.ShowError(errFrom, exc);
            }
        }
        private void bgwMovie_DoneSearchMovie(object sender, RunWorkerCompletedEventArgs e)
        {
            // Error log
            string errFrom = "frmMain-bgwMovie_DoneSearchMovie";

            // If result is null, exit;
            if (e.Result == null)
            {
                // Clear previous list
                lvSearchResult.Items.Clear();

                ListViewItem temp = new ListViewItem() { Text = "No Search Results!" };
                temp.Tag = "0";
                temp.ImageIndex = 0;
                lvSearchResult.Items.Add(temp);
                CloseLoading(); // Close loading form
                return;
            }

            // Starting, opening of App?
            if (Start)
            {
                // Perform click on Change View
                btnChangeView.PerformClick();
                // Toggle Start variable
                Start = false;

                //Record time end
                try
                {
                    LoadingEnd = DateTime.Now.TimeOfDay;
                    TimeSpan duration = LoadingEnd.Subtract(LoadingStart);
                    double TimeMS = Convert.ToDouble(duration.TotalMilliseconds);
                    double TimeSec = TimeMS / 1000;
                    string TimeitTook = $"Took {TimeMS} milliseconds to load App!\n\tIn seconds : {TimeSec}\n\tIn minutes : {duration.ToString("g")}";
                    GlobalVars.Log("frmMain", TimeitTook);

                } catch (Exception ex)
                {
                    // Log Error
                    GlobalVars.ShowError("frmMain-bgwMovie_DoneSearchMovie", ex, false);
                }
            }

            // Get result as DataTable, and Dispose it
            if (e.Result is DataTable)
            {
                DataTable dt = e.Result as DataTable;
                // Dispose the table after iterating thru its contents
                GlobalVars.Log(errFrom, $"Done with results!\n\tTotal Count of Processed LV Items: { lvSearchResult.Items.Count.ToString() }\n\tTotal number of Rows: { dt.Rows.Count.ToString() }");
                dt.Clear();
                dt.Dispose();
            }

            // If there are no results, show message
            if (lvSearchResult.Items.Count < 1)
            {
                ListViewItem temp = new ListViewItem() { Text = "No Search Results!" };
                temp.Tag = "0";
                temp.ImageIndex = 0;
                lvSearchResult.Items.Add(temp);
            }

            // Clear previous values of variables
            SEARCH_QUERY = "";

            // Close loading and refresh Memory
            CloseLoading();
        }
        // Search all Movie files in folder
        private void bgw_SearchFileinFolder(object sender, DoWorkEventArgs e)
        {
            // Error from
            string calledFrom = $"frmMain-bgw_SearchFileinFolder";

            // Get Movie files on Folder, even subFolder
            // Create variables
            int count = 0;
            int countVoid = 0;

            // Build a list of Directories from medialocation.hc-data
            List<string> DirListFrom = GlobalVars.DirSearch(FOLDERTOSEARCH, calledFrom +"- DirSearch (Exception)");

            // Check first if there are directories to search from.
            // Find all files that match criteria
            if (DirListFrom.Count > 0)
            {
                // List already in db
                List<string> listAlreadyinDB = DBCON.DbQrySingle(GlobalVars.DB_TNAME_FILEPATH, "[file]", calledFrom + "-listAlreadyinDB");

                // List of files valid to add
                List<string> listToAdd = new List<string>();

                string res = "";
                string nonres = "";
                string prevF = "";

                // If file is a movie,
                foreach (string file in DirListFrom)
                {
                    // Check if file have an extension of MOVIE_EXTENSIONS
                    foreach (string ext in GlobalVars.MOVIE_EXTENSIONS)
                    {
                        if (Path.GetExtension(file).ToLower() == ext)
                        {
                            // Check if file is already in the database
                            bool canAdd = true;

                            // If there is existing movies to check from
                            if (listAlreadyinDB.Count > 0)
                            {
                                // Go each movie filepath and check if it already exists
                                foreach (string pathEx in listAlreadyinDB)
                                {
                                    // If it exists, don't add this movie and continue to next
                                    if (pathEx == file)
                                    {
                                        canAdd = false;
                                        break;
                                    }
                                }
                            }

                            // If possible to add, add to list
                            if (canAdd)
                            {
                                listToAdd.Add(file);
                                res += file + "\n";
                                count += 1;
                                prevF = file;
                            }
                            break;
                        }
                    }
                    if (String.IsNullOrWhiteSpace(prevF))
                    {
                        nonres += file + "\n";
                        countVoid += 1;
                    }
                    prevF = "";
                }

                // Clear previous lists
                DirListFrom.Clear();
                listAlreadyinDB.Clear();

                GlobalVars.WriteToFile(GlobalVars.PATH_START + "MovieResult.Log", res);
                GlobalVars.WriteToFile(GlobalVars.PATH_START + "MovieResult_Void.Log", nonres);

                // Add now to database
                if (InsertToDB(listToAdd, calledFrom + "-listToAdd"))
                {
                    // Clear prev list
                    listToAdd.Clear();

                    // Send total count of results
                    List<int> listRes = new List<int>();
                    listRes.Add(count);
                    listRes.Add(countVoid);
                    e.Result = listRes;
                }
            }
            else
            {
                // Clear previous lists
                DirListFrom.Clear();
                e.Result = null;
            }
        }
        private void bgw_DoneSearchFileinFolder(object sender, RunWorkerCompletedEventArgs e)
        {
            // Close loading and refresh Memory
            CloseLoading();

            // Get result from BGworker
            if (e.Result != null)
            {
                // Counter for files
                int count, countVoid;

                List<int> res = e.Result as List<int>;
                count = res[0];
                countVoid = res[1];

                // Show message
                GlobalVars.ShowInfo($"Total files added: {count.ToString()}\nFiles skipped: {countVoid.ToString()}");
            }

            // Run GC to clean
            GlobalVars.CleanMemory("frmMain-bgw_DoneSearchFileinFolder");

            // Perform click on search button: btnSearch by calling RefreshMovieList()
            SEARCH_QUERY_PREV = "";
            RefreshMovieList();
        }
        // ############################################################################## Form CUSTOM events
        void Form_KeyDown(object sender, KeyEventArgs e)
        {
            // Ctrl-S = Opens settings form
            if (e.Control && e.KeyCode == Keys.S)
            {
                // Create Form
                if (GlobalVars.formSetting == null)
                {
                    Form form = new frmSettings();
                    form.Show(this);
                    GlobalVars.formSetting = form;
                }
                else
                {
                    GlobalVars.formSetting.Focus();
                }
                e.SuppressKeyPress = true;  // Stops other controls on the form receiving event.
            }
        }
        // Rightclicked on ListView Item event
        void cmLV_ItemCLicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem;
            // check which item is clicked
            if (item == toolMenuView)
            {
                // Open Movie Details form
                OpenNewFormMovie();
            } else if (item == toolMenuEdit)
            {
                // Create form for editing Movie information
                string MOVIE_ID = lvSearchResult.SelectedItems[0].Tag.ToString();
                string MOVIE_NAME = lvSearchResult.SelectedItems[0].Text.ToString();
                string childForm = GlobalVars.PREFIX_MOVIEINFO + MOVIE_ID;
                GlobalVars.OpenFormMovieInfo(this, childForm, MOVIE_ID, MOVIE_NAME, "frmMain-cmLV_ItemCLicked", lvSearchResult.SelectedItems[0]);
            }
        }
        // ############################################################################## Form Control events
        private void frmMain_Load(object sender, EventArgs e)
        {
            // Startup events

            // Auto check update
            GlobalVars.CheckForUpdate();

            // Delete previous log file, if exceeds file size limit
            GlobalVars.CheckLogFile(GlobalVars.FILE_APPLOG, "frmMain-frmMain", Text + "\n  : Start of LogFile");

            // Put default Image on ImageList
            GlobalVars.MOVIE_IMGLIST.Images.Clear();
            string Imagefile = GlobalVars.ImgFullPath("0");
            Image imgFromFile = Image.FromFile(Imagefile);
            GlobalVars.MOVIE_IMGLIST.Images.Add(Path.GetFileName(Imagefile), imgFromFile);

            // Start finding files in folder
            getAllMediaFiles();
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalVars.Log("frmMain.Designer-Dispose", "Exiting....");
            // Save settings
            SaveSettings();
            // Replace genre file
            GlobalVars.WriteArray(GlobalVars.TEXT_GENRE, GlobalVars.FILE_GENRE);
            // Replace country file
            string[] arrCountry = GlobalVars.BuildStringArrayFromCB(cbCountry);
            GlobalVars.WriteArray(arrCountry, GlobalVars.FILE_COUNTRY);

            // Dispose All Resources
            if (formLoading != null)
            {
                formLoading.Dispose();
            }
            // Clean each image 1 by 1
            GlobalVars.Log("frmMain-frmMain_FormClosing", "Disposing MOVIE_IMGLIST");
            //foreach (Image img in GlobalVars.MOVIE_IMGLIST.Images)
            //{
            //    if (img != null)
            //    {
            //        img.Dispose();
            //    }
            //}
            if (GlobalVars.MOVIE_IMGLIST != null)
            {
                GlobalVars.MOVIE_IMGLIST.Dispose();
            }
            // Run GC to clean
            GlobalVars.CleanMemory("frmMain_FormClosing");

            GlobalVars.Log("frmMain-frmMain_FormClosing", "Closed the program");
            //MessageBox.Show("now exiting...");
            Dispose();
        }
        
        private void btnChangeView_Click(object sender, EventArgs e)
        {
            if (lvSearchResult.View == View.Tile)
            {
                lvSearchResult.View = View.LargeIcon;
            }
            else
            {
                lvSearchResult.View = View.Tile;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Search the db for movie with filters
            // Setup columns needed
            string qry = "";
            SEARCH_COLS = LVMovieItemsColumns;

            SEARCH_QUERY = "";
            // If there is NO existing query for search,
            if (String.IsNullOrWhiteSpace(SEARCH_QUERY))
            {
                // Default SELECT Query
                qry = $"SELECT {SEARCH_COLS} FROM {GlobalVars.DB_TNAME_INFO}";

                // Build Filter for Query
                // Name Text search
                if ((txtSearch.Text != GlobalVars.SEARCHBOX_PLACEHOLDER) && (String.IsNullOrWhiteSpace(txtSearch.Text) ==false))
                {
                    qry += " WHERE ";
                    qry += $"[name] LIKE '%{txtSearch.Text}%' ";
                    qry += $"OR [name_ep] LIKE '%{txtSearch.Text}%' ";
                    qry += $"OR [name_series] LIKE '%{txtSearch.Text}%'";
                }
                // Year range
                if (String.IsNullOrEmpty(txtYearFrom.Text) == false)
                {
                    qry += GlobalVars.QryWhere(qry);
                    if (String.IsNullOrEmpty(txtYearTo.Text) == false)
                    {
                        qry += $"[year] BETWEEN {txtYearFrom.Text} AND {txtYearTo.Text}";
                    }
                    else
                    {
                        qry += $"[year] BETWEEN {txtYearFrom.Text} AND {DateTime.Now.Year.ToString()}";
                    }
                }
                // Category
                if (cbCategory.SelectedIndex > 0)
                {
                    qry += GlobalVars.QryWhere(qry);
                    qry += $"[category]={cbCategory.SelectedIndex}";
                }
                // Studio
                if (String.IsNullOrWhiteSpace(txtStudio.Text) == false)
                {
                    qry += GlobalVars.QryWhere(qry);
                    qry += $"[studio] LIKE '%{txtStudio.Text}%'";
                }
                // Cast
                if (String.IsNullOrWhiteSpace(txtCast.Text) == false)
                {
                    qry += GlobalVars.QryWhere(qry);
                    qry += $"[artist] LIKE '%{txtCast.Text}%'";
                }
                // Director
                if (String.IsNullOrWhiteSpace(txtDirector.Text) == false)
                {
                    qry += GlobalVars.QryWhere(qry);
                    qry += $"[director] LIKE '%{txtDirector.Text}%'";
                }
                // Country
                string CountryText = GlobalVars.RemoveLine(cbCountry.SelectedItem.ToString());
                if ((String.IsNullOrWhiteSpace(CountryText) == false) && cbCountry.SelectedIndex > 0)
                {
                    qry += GlobalVars.QryWhere(qry);
                    qry += $"[country] LIKE '%{CountryText}%'";
                }
                // Genre
                if (cbGenre.SelectedIndex > 0)
                {
                    qry += GlobalVars.QryWhere(qry);
                    qry += $"[genre] LIKE '%{cbGenre.Text}%'";
                }

                // Remove Previous Filters and focus on IMDB Code
                if (String.IsNullOrWhiteSpace(txtIMDB.Text) == false)
                {
                    qry = $"SELECT {SEARCH_COLS} FROM {GlobalVars.DB_TNAME_INFO} WHERE ";
                    qry += $"[imdb] = '{txtIMDB.Text}'";
                }

                // Append to end
                qry += ";";

                // Set query to perform on search
                SEARCH_QUERY = qry;
            }

            // Display the loading form.
            DisplayLoading();

            // Run BG Worker: bgSearchInDB, for Searching movies in database
            try
            {
                bgSearchInDB.RunWorkerAsync();

            } catch (Exception ex)
            {
                // Show error
                GlobalVars.ShowError($"frmMain-getAllMediaFiles", ex);
                // Close loading form
                CloseLoading();
            }
        }
        // When double-clicked on an item, open it in new form
        private void lvSearchResult_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Call function OpenFormPlayMovie
            OpenFormPlayMovie();
        }
        // Clear textboxes on filter
        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear searchbox and Filter
            txtSearch.Text = GlobalVars.SEARCHBOX_PLACEHOLDER;
            txtIMDB.Text = "";
            txtStudio.Text = "";
            txtDirector.Text = "";
            txtYearFrom.Text = "";
            txtYearTo.Text = DateTime.Now.Year.ToString();
            cbCategory.SelectedIndex = 0;
            cbCountry.SelectedIndex = 0;
            cbGenre.SelectedIndex = 0;

            // Clear values of vars
            SEARCH_QUERY = "";

            // Perform click on search button: btnSearch
            if (cbClearSearch.CheckState == CheckState.Checked)
            {
                btnSearch.PerformClick();
            }
        }
        // Add new movie
        private void btnAddMovie_Click(object sender, EventArgs e)
        {
            string formName = GlobalVars.PREFIX_MOVIEINFO + "0";
            // Create NEW MOVIE and (frmMovieInfo) SHOW Edit Information form
            GlobalVars.OpenFormMovieInfo(this, formName, "0", "New Movie ", "frmMain-btnAddMovie_Click (ADD MOVIE)");
        }
        // Auto search if Enter key is pressed
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            if (e.KeyCode == Keys.Enter)
            {
                // Check if searchbox is empty
                if ((String.IsNullOrWhiteSpace(txtSearch.Text)==false) && (txtSearch.Text != GlobalVars.SEARCHBOX_PLACEHOLDER))
                {
                    // Perform click on search button: btnSearch
                    btnSearch.PerformClick();
                }
            }
        }
        // Show ONLY Newly-added media files
        private void btnShowNew_Click(object sender, EventArgs e)
        {
            // Set Search Query
            SEARCH_COLS = LVMovieItemsColumns;
            SEARCH_QUERY = $"SELECT {SEARCH_COLS} FROM {GlobalVars.DB_TNAME_INFO} WHERE imdb=0 OR category=0;";
            SEARCH_QUERY_PREV = SEARCH_QUERY;

            // Perform click on search button: btnSearch
            RefreshMovieList();
        }
        // Delete files from temp
        private void btnClean_Click(object sender, EventArgs e)
        {
            if (GlobalVars.DeleteFilesExt(GlobalVars.PATH_TEMP, ".jpg", "frmMain-btnClean_Click"))
            {
                GlobalVars.ShowInfo("Cleanup done!");
            }
        }
        // When ENTER Key is pressed on ListView
        private void lvSearchResult_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Call function
                OpenFormPlayMovie();
            }
        }
        // Change lvSearchResult Sort by
        private void cbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Change lvSearchResult Sort by
            try
            {
                SortItemsInListView(cbSort.SelectedIndex);

            } catch (Exception ex)
            {
                // Log Error
                SortItemsInListView(0);
                GlobalVars.ShowError("frmMain-cbSort_SelectedIndexChanged", ex, false);
            }
        }
        private void cbSortOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SortItemsInListView(cbSort.SelectedIndex);

            }
            catch (Exception ex)
            {
                // Log Error
                SortItemsInListView(0);
                GlobalVars.ShowError("frmMain-cbSortOrder_SelectedIndexChanged", ex, false);
            }
        }
        // When ListView lvSearchResult is clicked on
        private void lvSearchResult_MouseClick(object sender, MouseEventArgs e)
        {
            // Right clicked? Show Context menu
            if (e.Button == MouseButtons.Right)
            {
                // Check if something is focused
                if (lvSearchResult.FocusedItem != null)
                {
                    // check if mouse is on lv Item Bounds
                    if (lvSearchResult.FocusedItem.Bounds.Contains(e.Location))
                    {
                        // Show pop-up context menu
                        cmLV.Show(Cursor.Position);
                    }
                }
            }
        }
    }
}
