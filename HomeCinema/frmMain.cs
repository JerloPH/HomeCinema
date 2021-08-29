#region License
/* #####################################################################################
 * LICENSE - GPL v3
* HomeCinema - Organize your Movie Collection
* Copyright (C) 2021  JerloPH (https://github.com/JerloPH)

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
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using HomeCinema.SQLFunc;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;

namespace HomeCinema
{
    public partial class frmMain : Form
    {
        // Strings and others
        static string SEARCHBOX_PLACEHOLDER = "Type your Search query here...";
        static bool IsLoadedSuccess = true;

        // Objects
        ListViewColumnSorter lvSorter = new ListViewColumnSorter();
        ToolStripItem toolMenuView, toolMenuEdit, toolMenuFileExplorer;
        ToolStripRender render = new ToolStripRender();

        #region frmMain
        public frmMain()
        {
            // Start app
            InitializeComponent();

            // Change Caption and Title
            Text = $"{GlobalVars.HOMECINEMA_NAME} - Media Organizer (v{GlobalVars.HOMECINEMA_VERSION} r{GlobalVars.HOMECINEMA_BUILD.ToString()})";

            // Load App Settings
            Settings.LoadSettings();

            // Adjust controls
            txtSearch.Text = SEARCHBOX_PLACEHOLDER; // Set placeholder text to Search

            // Set tooltips for controls
            ToolTip ttShowNew = new ToolTip();
            ttShowNew.SetToolTip(this.cbHideAnim, "Filter out Animations from Search Results");
            ttShowNew.SetToolTip(this.cbSort, "Change Sorting By");
            ttShowNew.SetToolTip(this.btnChangeView, "Change Item view");

            // Setup Sort and SortingOrder
            cbSort.Items.AddRange(new string[] { "Sort Default", "Sort [A-Z]", "Sort by Year" });
            cbSort.SelectedIndex = 1;
            cbSortOrder.Items.Add("Ascending");
            cbSortOrder.Items.Add("Descending");
            cbSortOrder.SelectedIndex = 0;

            // Setup listview lvSearchResult
            lvSearchResult.ShowItemToolTips = true;
            lvSearchResult.Columns.Add("ColName");
            lvSearchResult.Columns.Add("ColSeriesName");
            lvSearchResult.Columns.Add("ColEpName"); // Either [Original Name] OR [Season Num + Episode Num]
            lvSearchResult.Columns.Add("ColYear");
            lvSearchResult.Columns[0].Width = lvSearchResult.ClientRectangle.Width / 3;
            lvSearchResult.Columns[1].Width = lvSearchResult.ClientRectangle.Width / 3;
            lvSearchResult.Columns[2].Width = lvSearchResult.ClientRectangle.Width / 3;

            GlobalVars.MOVIE_IMGLIST.ImageSize = new Size(Settings.ImgTileWidth, Settings.ImgTileHeight);
            GlobalVars.MOVIE_IMGLIST.ColorDepth = ColorDepth.Depth32Bit;
            lvSearchResult.LargeImageList = GlobalVars.MOVIE_IMGLIST;
            lvSearchResult.SmallImageList = GlobalVars.MOVIE_IMGLIST;
            int sizeW = (lvSearchResult.ClientRectangle.Width / 2) - Settings.ImgTileWidth;
            lvSearchResult.TileSize = new Size(sizeW, Settings.ImgTileHeight + 2); // lvSearchResult.Width - (GlobalVars.IMGTILE_WIDTH + 120)

            lvSearchResult.AllowDrop = false;
            lvSearchResult.AllowColumnReorder = false;
            lvSearchResult.MultiSelect = false;
            lvSearchResult.View = View.Tile;

            // Populate Context Menu for ListView item Rightclick
            toolMenuView = cmLV.Items.Add("&View Details"); // View Movie Info
            toolMenuEdit = cmLV.Items.Add("&Edit Information"); // Edit Movie Info
            toolMenuFileExplorer = cmLV.Items.Add("&Find File in Explorer"); // Open File in Explorer
            cmLV.ItemClicked += new ToolStripItemClickedEventHandler(cmLV_ItemCLicked);

            // Populate combobox cbCategory
            cbCategory.Items.AddRange(GlobalVars.DB_INFO_CATEGORY);
            cbCategory.Items[0] = "All";
            cbCategory.SelectedIndex = 0;
            PopulateCountryCB(); // Populate combobox cbCountry, from file
            PopulateGenreCB(); // Populate comboBox cbGenre from File

            lvSearchResult.BackColor = Settings.ColorBg; // Set background color of ListView, from settings 'background color'
            // Others
            expSearch.Left = 1;
            expSearch.Top = tlstripMenu.Bottom + 2;
            lvSearchResult.Top = expSearch.Bottom + 2;
            tlstripMenu.RenderMode = ToolStripRenderMode.Professional;
            tlstripMenu.Renderer = this.render;

            Logs.Log("frmMain", "All UIs initialized!");
        }
        #endregion
        // ####################################################################################### Database Functions
        #region Insert to Database
        long InsertToDB(List<Medias> dtNewFiles, string errFrom)
        {
            long maxProgress = (long)dtNewFiles?.Count;
            long progress = 0;
            // exit when no entry to insert
            if (maxProgress < 1) { return maxProgress; }

            string callFrom = $"frmMain ({Name})-InsertToDB-({errFrom})";
            string logFileInsert = Path.Combine(DataFile.PATH_LOG, "NewInsert_Success.log");
            string logFileInsertSkipped = Path.Combine(DataFile.PATH_LOG, "NewInsert_Skipped.log");
            string mediatype = "movie";
            string filePath;
            string src;
            bool IsDownloadCover = false;
            long count = 0; // count of successful inserts
            string logInsert = Logs.LogFormatted("Succesfully inserted files and/or folders", ""); // Log succesfully inserted
            GlobalVars.WriteAppend(logFileInsertSkipped, Logs.LogFormatted("Skipped files and folders", ""));

            var form = new frmLoading($"Inserting {maxProgress} new found media files..", "New Entries Found!", true);
            form.MaxProgress = maxProgress;
            form.BackgroundWorker.DoWork += (sender1, e1) =>
            {
                foreach (Medias item in dtNewFiles)
                {
                    progress += 1;
                    filePath = item.FilePath;
                    mediatype = item.MediaType;
                    src = item.Source;

                    // vars used for entries
                    MediaInfo Media = null;
                    string mName = "";
                    string yearFromFname = "";

                    string rCountry = "";
                    string rGenre = "";
                    string rStudio = "";
                    string rDirector = "";
                    string rProd = "";
                    string rCasts = "";
                    string rYear = "";
                    string prevSrc = "";

                    // Get proper name, without the folder paths
                    try
                    {
                        mName = (mediatype == "series") ? new DirectoryInfo(filePath).Name : Path.GetFileNameWithoutExtension(filePath);
                    }
                    catch (Exception ex)
                    {
                        GlobalVars.WriteAppend(logFileInsertSkipped, "Error: " + filePath);
                        Logs.LogErr(callFrom, ex);
                        continue; // skip when exception thrown
                    }

                    // Trim Movie Name
                    try
                    {
                        mName = mName.ToLower();
                        mName = mName.Replace('_', ' ').Replace('.', ' ');
                        mName = mName.Replace("(", " ");
                        mName = mName.Replace(")", " ");
                        // Remove "year" and "other strings" from movie file name
                        string regExPattern = @"\b\d{4}\b"; // Match 4-digit number in the title
                        Match r = Regex.Match(mName, @regExPattern);
                        yearFromFname = r.Groups[r.Groups.Count - 1].Value;
                        mName = (!String.IsNullOrWhiteSpace(yearFromFname)) ? mName.Substring(0, mName.IndexOf(yearFromFname)) : mName;
                    }
                    catch (Exception ex)
                    {
                        Logs.LogErr(errFrom, ex);
                    }

                    // Scrape from TMDB, for info and details
                    if (Settings.IsOffline == false)
                    {
                        if (src.Equals(HCSource.tmdb) && GlobalVars.HAS_TMDB_KEY)
                        {
                            if (prevSrc.Equals(HCSource.tmdb))
                                Thread.Sleep(100);

                            var movie = TmdbAPI.FindMovieTV(mName, "dummy", mediatype);
                            if (movie?.TotalResults == 1)
                            {
                                if (prevSrc.Equals(HCSource.tmdb))
                                    Thread.Sleep(100);

                                Media = TmdbAPI.GetMovieInfoFromTmdb(movie?.Results[0].Id.ToString(), mediatype);
                            }
                            prevSrc = HCSource.tmdb;
                        }
                        else if (src.Equals(HCSource.anilist))
                        {
                            if (prevSrc.Equals(HCSource.anilist))
                                Thread.Sleep(200);

                            var anime = AnilistAPI.SearchForAnime(mName);
                            if (anime != null)
                            {
                                try
                                {
                                    if (anime.Data.Page.PageInfo.Total == 1)
                                    {
                                        string getAnilist = anime.Data.Page.MediaList[0].Id.ToString();
                                        Thread.Sleep(20);
                                        Media = AnilistAPI.GetMovieInfoFromAnilist(getAnilist);
                                    }
                                }
                                catch { }
                            }
                            prevSrc = HCSource.anilist;
                        }
                        if (Media != null)
                        {
                            try { rYear = Media.ReleaseDate.Substring(0, 4); }
                            catch { rYear = ""; }
                            rCountry = GlobalVars.ConvertListToString(Media.Country, ",", callFrom); // Get Country
                            rGenre = GlobalVars.ConvertListToString(Media.Genre, ",", callFrom); // Get Genres
                            rStudio = GlobalVars.ConvertListToString(Media.Studio, ";", callFrom); // Studio
                            rDirector = GlobalVars.ConvertListToString(Media.Director, ";", callFrom); // Director
                            rProd = GlobalVars.ConvertListToString(Media.Producer, ";", callFrom); // Producer
                            rCasts = GlobalVars.ConvertListToString(Media.Casts, ";", callFrom); // Casts
                        }
                    }
                    if (Media == null)
                    {
                        // If cannot get info online, make use of defaults
                        Media = new MediaInfo();
                        Media.Title = mName.Trim();
                    }
                    // If Original title is the same as the main title, ignore it
                    Media.OrigTitle = (Media.OrigTitle.Equals(Media.Title)) ? String.Empty : Media.OrigTitle;
                    // If year is empty, use the year from regex
                    rYear = (String.IsNullOrWhiteSpace(rYear)) ? yearFromFname : rYear;

                    // Make the DataRow
                    var dtInfo = new Dictionary<string, string>();
                    var dtFilepath = new Dictionary<string, string>();
                    dtInfo.Add(HCInfo.imdb, Media.Imdb); // IMDB
                    dtInfo.Add(HCInfo.anilist, Media.Anilist); // Anilist
                    dtInfo.Add(HCInfo.name, Media.Title); // name
                    dtInfo.Add(HCInfo.name_orig, Media.OrigTitle); // episode name
                    dtInfo.Add(HCInfo.name_series, Media.SeriesName); // series name
                    dtInfo.Add(HCInfo.season, Media.Seasons.ToString()); // season number
                    dtInfo.Add(HCInfo.episode, Media.Episodes.ToString()); // episode num
                    dtInfo.Add(HCInfo.country, rCountry); // country
                    dtInfo.Add(HCInfo.category, GlobalVars.GetCategoryValue(mediatype, src).ToString()); // category
                    dtInfo.Add(HCInfo.genre, rGenre); // genre
                    dtInfo.Add(HCInfo.studio, rStudio); // studio
                    dtInfo.Add(HCInfo.producer, rProd); // producer
                    dtInfo.Add(HCInfo.director, rDirector); // director
                    dtInfo.Add(HCInfo.artist, rCasts); // casts
                    dtInfo.Add(HCInfo.year, rYear); // year
                    dtInfo.Add(HCInfo.summary, Media.Summary); // summary

                    dtFilepath.Add(HCFile.File, filePath); // filepath
                    dtFilepath.Add(HCFile.Sub, GetSubtitleFile(filePath)); // file sub
                    dtFilepath.Add(HCFile.Trailer, Media.Trailer); // trailer
                    dtFilepath.Add(HCFile.Root, item.RootFolder); // RootFolder

                    long insertResult = SQLHelper.DbInsertMovie(dtInfo, dtFilepath, callFrom);
                    if (insertResult > 0)
                    {
                        // Succesfully inserted
                        count += 1; // add to count
                        logInsert += $"{filePath}\n";

                        // Clear prev cover images
                        string movieId = insertResult.ToString();
                        string oldFile = $"{DataFile.PATH_TEMP}{movieId}.jpg"; // cover path for temporary cover
                        string newFile = GlobalVars.ImgFullPath(movieId);
                        if (File.Exists(oldFile))
                        {
                            GlobalVars.DeleteMove(oldFile, errFrom); // Delete cover in temp folder
                        }
                        if (File.Exists(newFile) && Settings.IsOffline == false)
                        {
                            GlobalVars.DeleteMove(newFile, errFrom); // Delete existing cover first
                        }
                        // Download cover, if not OFFLINE_MODE
                        if (Settings.IsOffline == false && (!String.IsNullOrWhiteSpace(Media.PosterPath)))
                        {
                            if (GlobalVars.HAS_TMDB_KEY && src.Equals(HCSource.tmdb)) // Use TMDB API
                            {
                                Thread.Sleep(50); // sleep to prevent overloading API
                                IsDownloadCover = TmdbAPI.DownloadCoverFromTMDB(movieId, Media.PosterPath, errFrom);
                            }
                            else if (src == HCSource.anilist) // Use ANILIST API
                            {
                                Thread.Sleep(50); // sleep to prevent overloading API
                                IsDownloadCover = AnilistAPI.DownloadCoverFromAnilist(movieId, Media.PosterPath, errFrom);
                            }
                            else { } //do nothing
                        }
                        if (IsDownloadCover)
                        {
                            try
                            {
                                // Move from temp folder to poster path
                                File.Move(oldFile, newFile);
                                GlobalVars.DeleteMove(oldFile, errFrom); // Delete temp cover afterwards
                            }
                            catch (Exception ex)
                            {
                                Logs.LogErr($"{errFrom}\nTemp File: {oldFile}\nCover File: {newFile}", ex);
                            }
                        }
                        Media.Dispose();
                    }
                    form.UpdateProgress(progress);
                }
                dtNewFiles.Clear();
                if (!String.IsNullOrWhiteSpace(logInsert))
                {
                    GlobalVars.WriteAppend(logFileInsert, logInsert);
                }
            };
            form.ShowDialog(this);
            return count;
        }
        // return filepath from DB
        private string GetFilePath(string ID, string calledFrom)
        {
            string ret = "";
            string errFrom = $"frmMain-GetFilePath [calledFrom: {calledFrom}]";
            string qry = $"SELECT `{HCFile.File}` FROM { HCTable.filepath } WHERE `{HCInfo.Id}`={ID} LIMIT 1;";
            using (DataTable dtFile = SQLHelper.DbQuery(qry, errFrom))
            {
                if (dtFile.Rows.Count > 0)
                {
                    try
                    {
                        DataRow r = dtFile.Rows[0];
                        ret = r[HCFile.File].ToString();
                    }
                    catch { }
                }
                return ret;
            }
        }
        #endregion
        // ####################################################################################### Thread-safe static functions
        #region Thread-safe static func
        private delegate void AddItemDelegate(ListView lv, ListViewItem item);
        public static void AddItem(ListView lv, ListViewItem item)
        {
            if (lv.InvokeRequired)
            {
                lv.Invoke(new AddItemDelegate(AddItem), new object[]{ lv, item });
            }
            else
            {
                lv.Items.Add(item);
                //GlobalVars.Log("frmMain-AddItem()", "Added: " + item.Text);
            }
        }
        public static void AfterPopulatingMovieLV(ListView lv, long count = 0)
        {
            // Error log
            string errFrom = "frmMain-bgwMovie_DoneSearchMovie";
            // If there are no results, show message
            if (count < 1)
            {
                ListViewItem temp = new ListViewItem() { Text = "No Search Results!" };
                temp.Tag = "0";
                temp.ImageIndex = 0;
                AddItem(lv, temp);
                Logs.Log(errFrom, $"ResultSet is null or empty!");
            }
            lv.EndUpdate(); // Draw the ListView
            lv.ResumeLayout();
        }
        #endregion
        // ####################################################################################### Functions
        #region Functions
        // Search for Entries filtered
        private void SearchEntries()
        {
            // Default SELECT Query
            string qry = $"SELECT * FROM {HCTable.info}";

            // Build Filter for Query
            // Name Text search
            if ((txtSearch.Text != SEARCHBOX_PLACEHOLDER) && (!String.IsNullOrWhiteSpace(txtSearch.Text)))
            {
                qry += " WHERE ";
                qry += $"(`{HCInfo.name}` LIKE '%{txtSearch.Text}%' ";
                qry += $"OR `{HCInfo.name_orig}` LIKE '%{txtSearch.Text}%' ";
                qry += $"OR `{HCInfo.name_series}` LIKE '%{txtSearch.Text}%')";
            }
            // Year range
            if (!String.IsNullOrWhiteSpace(txtYearFrom.Text))
            {
                qry += SQLHelper.QryWhere(qry);
                if (!String.IsNullOrWhiteSpace(txtYearTo.Text))
                {
                    qry += $"`{HCInfo.year}` BETWEEN {txtYearFrom.Text} AND {txtYearTo.Text}";
                }
                else
                {
                    qry += $"`{HCInfo.year}`={txtYearFrom.Text}";
                }
            }
            // Genre
            qry += (cbGenre.SelectedIndex > 0) ? SQLHelper.QryWhere(qry) + $"`{HCInfo.genre}` LIKE '%{cbGenre.Text}%'" : "";

            // Category
            if (cbCategory.SelectedIndex > 0)
            {
                int index = cbCategory.SelectedIndex;
                qry += SQLHelper.QryWhere(qry);
                qry += $"`{HCInfo.category}`={index}"; // Search by category
            }
            // Studio
            if (String.IsNullOrWhiteSpace(txtStudio.Text) == false)
            {
                qry += SQLHelper.QryWhere(qry) + $"`{HCInfo.studio}` LIKE '%{txtStudio.Text}%'";
            }
            // Cast
            if (String.IsNullOrWhiteSpace(txtCast.Text) == false)
            {
                qry += SQLHelper.QryWhere(qry) + $"`{HCInfo.artist}` LIKE '%{txtCast.Text}%'";
            }
            // Director
            if (String.IsNullOrWhiteSpace(txtDirector.Text) == false)
            {
                qry += SQLHelper.QryWhere(qry) + $"`{HCInfo.director}` LIKE '%{txtDirector.Text}%'";
            }
            // Country
            string CountryText = GlobalVars.RemoveLine(cbCountry.SelectedItem.ToString());
            if ((String.IsNullOrWhiteSpace(CountryText) == false) && cbCountry.SelectedIndex > 0)
            {
                qry += SQLHelper.QryWhere(qry) + $"`{HCInfo.country}` LIKE '%{CountryText}%'";
            }

            // Override filter string build-up and Use IMDB Code
            if (String.IsNullOrWhiteSpace(txtIMDB.Text) == false)
            {
                qry = $"SELECT * FROM {HCTable.info} WHERE `{HCInfo.imdb}` = '{txtIMDB.Text}'";
            }

            // Filter out all animations
            qry += (cbHideAnim.CheckState == CheckState.Checked) ? SQLHelper.QryWhere(qry) + $" (`{HCInfo.category}` <= 2 AND `{HCInfo.genre}` NOT LIKE '%animation%')" : "";

            // Append to end
            qry += (Settings.ItemLimit > 0) ? $" LIMIT {Settings.ItemLimit};" : "";

            // Re-populate ListView of movies
            PopulateMovieBG(qry, false);
        }
        // Play Movie or Open Movie Details
        public void OpenFormPlayMovie()
        {
            if (lvSearchResult.SelectedItems.Count > 0)
            {
                try
                {
                    if (long.TryParse(lvSearchResult.SelectedItems[0].Tag.ToString().TrimStart('0'), out long ID))
                    {
                        if (ID < 1) { return; }
                        if (Settings.IsAutoplay)
                        {
                            GlobalVars.PlayMedia(GetFilePath(ID.ToString(), "frmMain-OpenNewFormMovie"));
                            return;
                        }
                        else
                            OpenNewFormMovie(); // Open Movie Details form
                    }
                }
                catch (Exception ex) { Logs.LogErr("frmMain-OpenFormPlayMovie", ex); };
            }
        }
        // Open Movie Details form
        private void OpenNewFormMovie()
        {
            try
            {
                if (lvSearchResult.SelectedItems.Count < 1)
                {
                    Msg.ShowWarning("Select an item to view its media information!");
                    return;
                }
                if (long.TryParse(lvSearchResult.SelectedItems[0].Tag.ToString().TrimStart('0'), out long ID))
                {
                    if (ID > 0)
                    {
                        // Create form to View Movie Details / Info
                        string text = lvSearchResult.SelectedItems[0].Text;
                        GlobalVars.OpenFormMovie(this, lvSearchResult.SelectedItems[0], ID, text);
                    }
                }
            }
            catch (Exception ex) { Msg.ShowError("frmMain-OpenNewFormMovie", ex, "Error occured on viewing media info!", this); };
        }
        public void SearchBoxPlaceholder(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = SEARCHBOX_PLACEHOLDER;
                txtSearch.ForeColor = Color.Black;
            }
        }
        public void SearchBoxPlaceholderClear(object sender, EventArgs e)
        {
            if (txtSearch.Text == SEARCHBOX_PLACEHOLDER)
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
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
                string qry = $"SELECT * FROM {HCTable.info} WHERE `{HCInfo.Id}`={MOVIEID} LIMIT 1;";

                using (DataTable dtFile = SQLHelper.DbQuery(qry, "frmMain-UpdateMovieItemOnLV")) // run the query
                {
                    // Check if there are results
                    if (dtFile.Rows.Count > 0)
                    {
                        try
                        {
                            // Get DataRow from DataTable passed by the BG worker
                            DataRow r = dtFile.Rows[0];
                            // Edit Information on ListView Item
                            LVItemSetDetails(lvItem, new string[]
                            {
                                MOVIEID.ToString(),
                                r[HCInfo.name].ToString(), // name
                                r[HCInfo.name_orig].ToString(), // name_orig
                                r[HCInfo.name_series].ToString(), // name_series
                                r[HCInfo.season].ToString(), // season
                                r[HCInfo.episode].ToString(), // episode
                                r[HCInfo.year].ToString(), // year
                                r[HCInfo.summary].ToString(), // summary
                                r[HCInfo.genre].ToString() // genre
                            });
                        }
                        catch { }
                    }
                }
                // Refresh imagelist and lvSearchResult to reflect changes to info and Image
                lvSearchResult.Refresh();
            }
        }
        public void RemoveItemInMovieList(ListViewItem lv)
        {
            try { lvSearchResult.Items.Remove(lv); }
            catch (Exception ex)
            {
                Logs.LogErr("frmMain-RemoveItemInMovieList", ex);
                PopulateMovieBG("", false);
            }
        }
        // Sort Items in lvSearchResult ListView
        private void SortItemsInListView(int toggle)
        {
            // Change Sort Order
            SortOrder Sorting = (cbSortOrder.SelectedIndex > 0) ? SortOrder.Descending : SortOrder.Ascending;

            // Peform sort
            switch (toggle)
            {
                case 0:
                    //Msg.ShowInfo("Sorted default");
                    lvSearchResult.Sorting = Sorting;
                    lvSearchResult.ListViewItemSorter = null;
                    var items = lvSearchResult.Items.Cast<ListViewItem>().OrderBy(x => x.Tag.ToString()).ToList();
                    lvSearchResult.Items.Clear();
                    lvSearchResult.Items.AddRange(items.ToArray());
                    break;
                case 1:
                    //Msg.ShowInfo("Sorted AZ");
                    lvSorter.SortColumn = 0;
                    lvSorter.Order = Sorting;
                    lvSearchResult.ListViewItemSorter = lvSorter;
                    lvSearchResult.Sort();
                    break;
                case 2:
                    //Msg.ShowInfo("Sorted year");
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
            int MOVIEID;
            string MovieId = InfoString[0]; // ID
            string resName = InfoString[1]; // name
            string resNameOrig = InfoString[2]; // name_orig
            string resNameSer = InfoString[3]; // name_series
            string resSeason = InfoString[4]; // season
            string resEp = InfoString[5]; // episode
            string resYear = InfoString[6]; // year
            string resSummary = InfoString[7]; // summary
            string resGenre = InfoString[8]; // genre
            long epCount = 0;
            long.TryParse(resEp, out epCount);
            // Convert MovieId string to int
            try { MOVIEID = Convert.ToInt32(MovieId); }
            catch { MOVIEID = 0; }

            if (MOVIEID > 0)
            {
                // Clear previous subitems
                temp.SubItems.Clear();

                // Set default text for item
                temp.Text = resName;

                // Append ToolTip on it
                temp.ToolTipText = "Summary: \n" + GlobalVars.LimitString(resSummary, 255) + "\n\nGenre:\n" + resGenre.Replace(",", ", ");
                
                // Check episode count and decide what to show as title
                if (epCount < 2) // This is either a movie, or series with 1 ep only 
                {
                    temp.SubItems.Add(resNameOrig); // Original Name
                    temp.SubItems.Add(resNameSer); // Series Name, if its in a series
                }
                else // Otherwise, its series, or movie with multiple episodes
                {
                    // Add episode to Tooltip
                    temp.ToolTipText += $"\n\nEpisodes: {epCount}";
                    string seasonNum = GlobalVars.ValidateNum(resSeason);
                    temp.SubItems.Add(resNameOrig); // Original Name
                    if (!seasonNum.Equals("00"))
                    {
                        temp.SubItems.Add($"S{seasonNum} E{GlobalVars.ValidateNum(resEp)}");
                    }
                    else
                        temp.SubItems.Add($"Episodes {GlobalVars.ValidateNum(epCount)}");
                }
                // Year
                temp.SubItems.Add(resYear);
                // Display image (From ImageList) based on ImageKey
                temp.ImageKey = GlobalVars.ImgGetKey(MOVIEID.ToString());
                // Add year to name/title of MOVIE
                temp.Text += $" ({resYear})";

                // Save the ID as Tag, to NOT SHOW it on LIST
                temp.Name = Convert.ToString(MOVIEID);
                temp.Tag = GlobalVars.ValidateZero(MOVIEID);

                // Set font
                temp.Font = GlobalVars.TILE_FONT;
                temp.ForeColor = Settings.ColorFont;
            }
        }
        // Populate combobox cbCountry, from file
        public void PopulateCountryCB()
        {
            GlobalVars.TEXT_COUNTRY = GlobalVars.BuildArrFromFile(DataFile.FILE_COUNTRY, "frmMain-PopulateCountryCB [FILE_COUNTRY]");
            cbCountry.Items.Clear();
            cbCountry.Items.Add("All");
            foreach (string c in GlobalVars.TEXT_COUNTRY)
            {
                if ((String.IsNullOrWhiteSpace(c) == false) && c != "All")
                {
                    cbCountry.Items.Add(c.Trim());
                }
            }
            cbCountry.SelectedIndex = 0;
        }
        // Populate combobox cbGenre, from file
        public void PopulateGenreCB()
        {
            GlobalVars.TEXT_GENRE = GlobalVars.BuildArrFromFile(DataFile.FILE_GENRE, "frmMain-PopulateGenreCB [FILE_GENRE]");
            cbGenre.Items.Clear();
            cbGenre.Items.Add("All");
            foreach (string c in GlobalVars.TEXT_GENRE)
            {
                if (String.IsNullOrWhiteSpace(c) == false)
                {
                    cbGenre.Items.Add(c.Trim());
                }
            }
            cbGenre.SelectedIndex = 0;
        }
        // Create or Show frmSettings Form
        private void ShowSettingsForm()
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
        }
        // Create or Show frmAbout Form
        private void ShowAboutForm()
        {
            // Create Form
            if (GlobalVars.formAbout == null)
            {
                var form = new frmAbout();
                form.Show(this);
                GlobalVars.formAbout = form;
            }
            else
            {
                GlobalVars.formAbout.Focus();
            }
        }
        private void SaveComboBoxItemsToFile(ComboBox cb, string file)
        {
            // save cbCountry contents to FILE_COUNTRY
            string toWrite = "";
            var list = cb.Items.Cast<string>().OrderBy(s => s);
            foreach (string item in list)
            {
                if (item.Equals("All"))
                    continue;

                toWrite += item + ",";
            }
            toWrite = toWrite.TrimEnd(',');
            GlobalVars.WriteToFile(file, toWrite);
        }
        private int ControlLeftFromAttach(Control src, Control attachedTo) // return left property appropriate to control attached to another control
        {
            return attachedTo.Left - (src.Width+1);
        }
        #endregion
        // ####################################################################################### BACKGROUND WORKERS
        #region BG Worker: Get files in folders
        // Search all Movie files in folder
        private void GetMediaFromFolders()
        {
            // Get Movie files on Folder, even subFolder
            string calledFrom = $"frmMain-GetMediaFromFolders()";
            Logs.Log(calledFrom, "Search for Supported Media files in Folders..");

            // Declare vars
            frmLoading form = new frmLoading("Getting media files from directories..", "Loading");
            var listAlreadyinDB = new List<string>();
            var dtNewFiles = new List<Medias>();
            int countMediaLoc = 0;
            int countMediaLocMax = 0;
            long insertRes = 0;

            // Build list of folders to search from
            GlobalVars.LoadMediaLocations();
            if (GlobalVars.MEDIA_LOC?.Count < 1)
            {
                string folder = "", src = "", mediatype = "";
                // Browse for Dir
                while (String.IsNullOrWhiteSpace(folder))
                {
                    var formNewMediaLoc = new frmNewMediaLoc("Select folder to search for Movie files", null);
                    formNewMediaLoc.ShowDialog();
                    folder = formNewMediaLoc.Path;
                    src = formNewMediaLoc.Source;
                    mediatype = formNewMediaLoc.Type;
                    formNewMediaLoc.Dispose();
                }
                GlobalVars.MEDIA_LOC.Add(new MediaLocations(folder, mediatype, src));
                GlobalVars.WriteToFile(DataFile.FILE_MEDIALOC, $"{folder}*{mediatype}*{src}");
            }

            // Delegate task to frmLoading
            form.BackgroundWorker.DoWork += (sender1, e1) =>
            {
                // Build a list of Files in Directories from medialocation.hc-data
                // Setup extensions for media files, load supported ext from file
                string[] tempMediaExt = GlobalVars.BuildArrFromFile(DataFile.FILE_MEDIA_EXT, "frmMain-frmMain[FILE_MEDIA_EXT]");
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

                listAlreadyinDB = SQLHelper.DbQrySingle(HCTable.filepath, "[file]", calledFrom + "-listAlreadyinDB");
                countMediaLocMax = GlobalVars.MEDIA_LOC.Count;
                foreach (MediaLocations mediaLoc in GlobalVars.MEDIA_LOC)
                {
                    countMediaLoc += 1;
                    form.UpdateMessage($"Loading directories ({countMediaLoc}/{countMediaLocMax})..");
                    Logs.Log(calledFrom, $"Searching in: ({mediaLoc.Path}), Mediatype: {mediaLoc.MediaType}, Source: {mediaLoc.Source}");
                    if (Directory.Exists(mediaLoc.Path))
                    {
                        // Add movies
                        if ((mediaLoc.MediaType == "movie"))
                        {
                            var listMovieFiles = GlobalVars.SearchFilesSingleDir(mediaLoc.Path.TrimEnd('\\'), calledFrom);
                            if (listMovieFiles?.Count > 0)
                            {
                                foreach (var file in listMovieFiles)
                                {
                                    try
                                    {
                                        if (GlobalVars.MOVIE_EXTENSIONS.Contains(Path.GetExtension(file).ToLower()))
                                        {
                                            if (listAlreadyinDB?.Count > 0)
                                            {
                                                if (!listAlreadyinDB.Contains(file))
                                                {
                                                    dtNewFiles.Add(new Medias(file, mediaLoc.MediaType, mediaLoc.Source, mediaLoc.Path));
                                                }
                                                else
                                                {
                                                    // remove the item from list of already existing
                                                    int index = listAlreadyinDB.IndexOf(file);
                                                    try { listAlreadyinDB.RemoveAt(index); }
                                                    catch { }
                                                }
                                            }
                                            else
                                            {
                                                dtNewFiles.Add(new Medias(file, mediaLoc.MediaType, mediaLoc.Source, mediaLoc.Path));
                                            }
                                        }
                                        else
                                        {
                                            Logs.LogSkip($"Not supported ext: {file}");
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Logs.LogSkip($"Error: {file}");
                                        Logs.LogErr(calledFrom, ex);
                                        continue;
                                    }
                                }
                            }
                        }
                        // Add series
                        else
                        {
                            var listSeries = GlobalVars.SearchFoldersFromDirectory(mediaLoc.Path, calledFrom);
                            if (listSeries?.Count > 0)
                            {
                                foreach (var folderName in listSeries)
                                {
                                    if (Directory.Exists(folderName))
                                    {
                                        if (!listAlreadyinDB.Contains(folderName))
                                        {
                                            dtNewFiles.Add(new Medias(folderName, mediaLoc.MediaType, mediaLoc.Source, mediaLoc.Path));
                                        }
                                        else
                                        {
                                            // remove the item from list of already existing
                                            int index = listAlreadyinDB.IndexOf(folderName);
                                            try { listAlreadyinDB.RemoveAt(index); }
                                            catch { }
                                        }
                                    }
                                    else
                                    {
                                        Logs.LogSkip($"Directory does not exist: {folderName}");
                                    }
                                }
                            }
                        }
                    }
                }
                // Clear previous lists
                listAlreadyinDB?.Clear();
            };
            form.ShowDialog(this);
            insertRes = InsertToDB(dtNewFiles, calledFrom + "-dtNewFiles");
            if (insertRes > 0)
            {
                Msg.ShowInfo($"Successfully inserted {insertRes} new entries!");
            }
            // Load ListView collection
            PopulateMovieBG($"SELECT * FROM {HCTable.info}", true);
        }
        #endregion
        #region BG Worker: Populate MOVIE ListView
        private void PopulateMovieBG(string query, bool AppStart)
        {
            // Stop ListView form Drawing
            lvSearchResult.BeginUpdate(); // Pause drawing events on ListView
            lvSearchResult.SuspendLayout();
            lvSearchResult.Items.Clear(); // Clear previous list
            // Populate movie listview with new entries, from another form thread
            frmLoading form = new frmLoading(AppStart ? "Loading collection.." : "Searching..", "Loading", true);
            string qry = query;
            string errFrom = "frmMain-PopulateMovieBG()";
            string fileNamePath, fileRootFolder;
            long progress = 0;
            long progressMax = 0;
            bool AddEntry = true;
            Dictionary<string, Medias> filepaths = new Dictionary<string, Medias>();
            // If no query
            if (String.IsNullOrWhiteSpace(qry))
            {
                qry = $"SELECT * FROM {HCTable.info}";
            }
            Logs.Debug("Start Adding Items to ListView");
            Logs.Debug("Fetching.. filepaths");
            // Execute query to fetch file paths
            using (var dtFile = SQLHelper.DbQuery($"SELECT `{HCFile.Id}`,`{HCFile.File}`,`{HCFile.Root}` FROM {HCTable.filepath};", errFrom))
            {
                if (dtFile != null)
                {
                    foreach (DataRow item in dtFile.Rows)
                    {
                        var fileMedia = new Medias(item[HCFile.File].ToString(), "", "", item[HCFile.Root].ToString());
                        filepaths.Add(item[HCFile.Id].ToString(), fileMedia);
                    }
                    Logs.Debug("Fetched filepaths!");
                }
            }
            Logs.Debug("Fetching.. info");
            // Execute query to fetch movie info
            using (DataTable dt = SQLHelper.DbQuery(qry, errFrom)) // Get DataTable from query
            {
                // Set Max Progress
                if (dt != null)
                {
                    progressMax = (long)dt.Rows.Count;
                    Logs.Debug("Fetched info!\n");
                }
                // Iterate thru all DataRows
                if (progressMax > 0)
                {
                    Logs.Debug($"Will go through items: {progressMax}");
                    form.MaxProgress = progressMax;
                    form.BackgroundWorker.DoWork += (sender1, e1) =>
                    {
                        Logs.Debug("Perform foreach Iterate on DataRows\n\n");
                        foreach (DataRow r in dt.Rows)
                        {
                            AddEntry = true;
                            // Add Item to ListView
                            long MOVIEID;
                            if (!long.TryParse(r[HCInfo.Id].ToString(), out MOVIEID)) // Convert ID object to ID int
                            {
                                MOVIEID = 0;
                                Logs.Log(errFrom, $"Invalid MovieID: {r[HCInfo.Id].ToString()}");
                            }
                            Logs.Debug($"Initializing item with ID: {MOVIEID}");
                            // Add to listview lvSearchResult
                            if (MOVIEID > 0)
                            {
                                // Skip entry if file does not exist
                                filepaths.TryGetValue(MOVIEID.ToString(), out Medias fileMedia);
                                fileNamePath = fileMedia.FilePath;
                                fileRootFolder = fileMedia.RootFolder;

                                if (String.IsNullOrWhiteSpace(fileRootFolder))
                                {
                                    try
                                    {
                                        foreach (var item in GlobalVars.MEDIA_LOC)
                                        {
                                            if (fileNamePath.Contains(item.Path))
                                            {
                                                fileRootFolder = item.Path;
                                                break;
                                            }
                                        }
                                        if (!String.IsNullOrWhiteSpace(fileRootFolder))
                                        {
                                            var dict = new Dictionary<String, String>();
                                            dict.Add(HCFile.Id, MOVIEID.ToString());
                                            dict.Add(HCFile.Root, fileRootFolder);
                                            SQLHelper.DbUpdateTable(HCTable.filepath, dict, errFrom);
                                        }
                                        else
                                        {
                                            if (Settings.IsSkipNotMediaLoc)
                                            {
                                                Logs.LogSkip($"Not in Media Locations: {fileNamePath}");
                                                AddEntry = false;
                                            }
                                        }
                                    }
                                    catch (Exception ex) { Logs.LogErr(errFrom, ex); }
                                }
                                if (!String.IsNullOrWhiteSpace(fileNamePath) && AddEntry)
                                {
                                    try
                                    {
                                        FileAttributes attr = File.GetAttributes(fileNamePath);
                                        if (attr.HasFlag(FileAttributes.Directory))
                                        {
                                            AddEntry = Directory.Exists(fileNamePath); // Non existing directory, skip it
                                        }
                                        else
                                        {
                                            AddEntry = File.Exists(fileNamePath); // Non existing file, skip it
                                        }
                                    }
                                    catch (DirectoryNotFoundException)
                                    {
                                        AddEntry = false;
                                    }
                                    catch (FileNotFoundException)
                                    {
                                        AddEntry = false;
                                    }
                                    catch (Exception ex)
                                    {
                                        Logs.LogErr($"{errFrom}\nFile: {fileNamePath}", ex);
                                        AddEntry = false;
                                    }

                                    if (!AddEntry)
                                        Logs.LogSkip($"File or Directory not existing! : {fileNamePath}");
                                }
                                // Load 'cover' Image from 'cover' folder
                                if (AppStart && AddEntry)
                                {
                                    Logs.Debug($"Loading image for ID: {MOVIEID}");
                                    string Imagefile = GlobalVars.ImgFullPath(MOVIEID.ToString());
                                    try
                                    {
                                        if (File.Exists(Imagefile))
                                        {
                                            this.Invoke(new Action(() =>
                                            {
                                                using (Image imageFromFile = Image.FromFile(Imagefile))
                                                {
                                                    GlobalVars.MOVIE_IMGLIST.Images.Add(Path.GetFileName(Imagefile), imageFromFile);
                                                }
                                            }));
                                            Logs.Debug($"Image loaded for ID: {MOVIEID}, file: {Imagefile}");
                                        }
                                    }
                                    catch (Exception exImg)
                                    {
                                        Logs.LogErr($"{errFrom}\n\tFile:\n\t{Imagefile}", exImg);
                                    }
                                }

                                if (AddEntry)
                                {
                                    try
                                    {
                                        // Get all strings from the DataRow, passed by the BG worker
                                        Logs.Debug($"Fetching DataRow info with ID: {MOVIEID}");
                                        string resName = r[HCInfo.name].ToString(); // name
                                        string resNameEp = r[HCInfo.name_orig].ToString(); // name_ep
                                        string resNameSer = r[HCInfo.name_series].ToString(); // name_series
                                        string resSeason = r[HCInfo.season].ToString(); // season
                                        string resEp = r[HCInfo.episode].ToString(); // episode
                                        string resYear = r[HCInfo.year].ToString(); // year
                                        string resSum = r[HCInfo.summary].ToString(); // summary
                                        string resGenre = r[HCInfo.genre].ToString(); // genre

                                        // Make new ListView item, and assign properties to it
                                        ListViewItem temp = new ListViewItem() { Text = resName };

                                        // Edit Information on ListView Item
                                        Logs.Debug($"Setting Up ListView Item details: {MOVIEID}");
                                        LVItemSetDetails(temp, new string[] { MOVIEID.ToString(),
                                            resName, resNameEp, resNameSer,
                                            resSeason, resEp, resYear, resSum, resGenre });

                                        // Add Item to ListView lvSearchResult
                                        Logs.Debug($"Adding item to ListView, with ID: {MOVIEID}");
                                        AddItem(lvSearchResult, temp);
                                        progress += 1;
                                    }
                                    catch (Exception ex) { Logs.LogErr(errFrom, ex); }
                                }
                            }
                            form.UpdateProgress();
                            Logs.Debug($"Updated progress to {progress}\n\n");
                        }
                        Logs.Debug($"DONE Loading ListView items BGworker from: {Name}");
                    };
                    form.ShowDialog();
                }
            }
            filepaths?.Clear(); // micro optimization
            AfterPopulatingMovieLV(lvSearchResult, progress);
            Logs.Debug("ListView loaded!");
            // Check if no TMDB Key
            if (!GlobalVars.HAS_TMDB_KEY && AppStart)
            {
                Msg.ShowWarning(GlobalVars.MSG_NO_TMDB);
            }
            // Auto check update
            if ((Settings.IsOffline == false) && (Settings.IsAutoUpdate) && AppStart)
            {
                GlobalVars.CheckForUpdate(this, false);
            }
        }
        #endregion
        // ####################################################################################### Form CUSTOM events
        #region Custom Events
        void Form_KeyDown(object sender, KeyEventArgs e)
        {
            // Ctrl-S = Opens settings form
            if (e.Control && e.KeyCode == Keys.S)
            {
                // Create or Show Settings Form
                ShowSettingsForm();
                e.SuppressKeyPress = true;  // Stops other controls on the form receiving event.
            }
        }
        // Rightclicked on ListView Item event
        void cmLV_ItemCLicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string errFrom = "frmMain - cmLV_ItemCLicked";
            ToolStripItem item = e.ClickedItem;
            // check which item is clicked
            if (item == toolMenuView)
            {
                // Open Movie Details form
                OpenNewFormMovie();
            }
            else if (item == toolMenuEdit)
            {
                // Create form for editing Movie information, Only if MOVIE_ID is valid
                string MOVIE_ID = lvSearchResult.SelectedItems[0].Tag.ToString();
                if (MOVIE_ID != "0" && (!String.IsNullOrWhiteSpace(MOVIE_ID)))
                {
                    GlobalVars.OpenFormMovieInfo(this, MOVIE_ID, lvSearchResult.SelectedItems[0], $"{errFrom} [toolMenuEdit]");
                }
            }
            else if (item == toolMenuFileExplorer)
            {
                // Open file in Explorer, Only if MOVIE_ID is valid
                string MOVIE_ID = lvSearchResult.SelectedItems[0].Tag.ToString();
                if (MOVIE_ID != "0" && (!String.IsNullOrWhiteSpace(MOVIE_ID)))
                {
                    string file = GetFilePath(MOVIE_ID, $"{errFrom} [toolMenuFileExplorer]");
                    GlobalVars.FileOpeninExplorer(file, $"{errFrom} [toolMenuFileExplorer]");
                }
            }
        }
        #endregion
        // ####################################################################################### Form Control events
        private void frmMain_Load(object sender, EventArgs e)
        {
            // Startup events
            WindowState = FormWindowState.Maximized; // *Required lines to trigger Window Resize event
            WindowState = FormWindowState.Normal; // *Required lines to trigger Window Resize event
            TopLevel = true;
            // Clean temp and log files
            if (Settings.IsAutoClean)
            {
                GlobalVars.CleanAppDirectory();
            }
            // Delete previous log file, if exceeds file size limit
            GlobalVars.CheckLogFile(DataFile.FILE_LOG_APP, "frmMain-(Delete AppLog)", Text + "\n  : Start of LogFile");
            GlobalVars.CheckLogFile(DataFile.FILE_LOG_DB, "frmMain-(Delete App_DB.log)", Text + "\n  : Database Log");
            GlobalVars.CheckLogFile(DataFile.FILE_LOG_ERROR, "frmMain-(Delete App_ErrorLog.log)", Text + "\n  : Error Logs");

            // Put default Image on ImageList
            try
            {
                GlobalVars.MOVIE_IMGLIST.Images.Clear();
                string imageFilePath = GlobalVars.ImgFullPath("0");
                string Imagefile = (File.Exists(imageFilePath)) ? imageFilePath : DataFile.FILE_DEFIMG;
                using (Image imgFromFile = Image.FromFile(Imagefile))
                {
                    GlobalVars.MOVIE_IMGLIST.Images.Add(Path.GetFileName(Imagefile), imgFromFile);
                }
            }
            catch (Exception exc)
            {
                Msg.ShowError("frmMain-Load", exc, "Default image is missing!\nRestart App", this);
                IsLoadedSuccess = false;
                return;
            }

            // Perform click on Change View
            btnChangeView.PerformClick();

            // Initialize connection to database
            var loadForm = new frmLoading("Loading database", "Please wait while loading..");
            loadForm.BackgroundWorker.DoWork += (sender1, e1) =>
            {
                if (!SQLHelper.Initiate("frmMain"))
                {
                    // Database not loaded!
                    Msg.ShowWarning("Database is possibly corrupted!\nDelete HomeCinema.db and try again\nNOTE: This will remove all your entries", "", loadForm);
                    IsLoadedSuccess = false;
                }
            };
            loadForm.ShowDialog(this);
            // Start finding files in folder
            if (IsLoadedSuccess)
            {
                GetMediaFromFolders();
            }
            else
            {
                AfterPopulatingMovieLV(lvSearchResult, 0);
                btnSearch.Enabled = false;
                cbHideAnim.Enabled = false;
                cbClearSearch.Enabled = false;
            }
        }
        private void frmMain_Resize(object sender, EventArgs e)
        {
            // Resize controls
            int ClientW = ClientRectangle.Width;
            int ClearBtnAdjust = (this.WindowState == FormWindowState.Maximized) ? 40 : 24;
            int ControlsSize = (int)(0.11*ClientW);
            double size = (this.WindowState == FormWindowState.Maximized) ? 0.15 : 0.13;
            int buttonWidth = (int)(this.Width * size);
            int expHeight = cbSort.Bottom + 8;

            // Reposition 'Clear' button and 'Search after clear' checkbox
            btnSearch.Left = (int)(this.Width - btnSearch.Width) - ClearBtnAdjust;
            btnClear.Left = btnSearch.Left;
            cbHideAnim.Left = (int)(this.Width - cbHideAnim.Width) - ClearBtnAdjust; // reposition 'Hide animations' checkbox
            cbClearSearch.Left = btnClear.Left;
            cbSort.Width = ControlsSize; // Set size for Sort By combobox
            cbSortOrder.Width = ControlsSize;
            cbSortOrder.Left = cbSort.Right + 4;
            btnChangeView.Left = cbSortOrder.Right + 4;
            btnFixNoInfo.Left = btnChangeView.Right + 4;
            // Top-most controls
            txtSearch.Width = (btnSearch.Left - txtSearch.Left) - 16;
            expSearch.Left = 1;
            expSearch.ClientSize = new Size(ClientW-2, expHeight);
            expSearch.Width = ClientW;
            lvSearchResult.Top = expSearch.Bottom + 2;
            lvSearchResult.Height = (ClientRectangle.Height - lvSearchResult.Top) - 2;
            // Other controls
            txtIMDB.Width = buttonWidth; // top row
            txtDirector.Width = buttonWidth;
            txtStudio.Width = buttonWidth;
            cbGenre.Width = buttonWidth;
            txtYearFrom.Width = (int)(buttonWidth * 0.46); // bottom row
            txtYearTo.Width = (int)(buttonWidth * 0.46);
            txtCast.Width = buttonWidth;
            cbCountry.Width = buttonWidth;
            cbCategory.Width = buttonWidth;

            // Reposition 'Search' controls
            txtIMDB.Left = (int)(this.Width * 0.06); // top row
            txtDirector.Left = (int)(this.Width * 0.27);
            txtStudio.Left = (int)(this.Width * 0.47);
            cbGenre.Left = (int)(this.Width * 0.685);
            txtYearFrom.Left = txtIMDB.Left; // bottom row
            lblYearDiv.Left = txtYearFrom.Right + 1;
            txtYearTo.Left = lblYearDiv.Right + 1;
            txtCast.Left = txtDirector.Left;
            cbCountry.Left = txtStudio.Left;
            cbCategory.Left = cbGenre.Left;
            // Reposition labels
            lblImdb.Left = ControlLeftFromAttach(lblImdb, txtIMDB); // top row
            lblDirector.Left = ControlLeftFromAttach(lblDirector, txtDirector);
            lblStudio.Left = ControlLeftFromAttach(lblStudio, txtStudio);
            lblGenre.Left = ControlLeftFromAttach(lblGenre, cbGenre);
            lblYear.Left = ControlLeftFromAttach(lblYear, txtYearFrom); // bottom row
            lblCast.Left = ControlLeftFromAttach(lblCast, txtCast);
            lblCountry.Left = ControlLeftFromAttach(lblCountry, cbCountry);
            lblCategory.Left = ControlLeftFromAttach(lblCategory, cbCategory);
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Settings.SaveSettings(); // Save settings
            SaveComboBoxItemsToFile(cbCountry, DataFile.FILE_COUNTRY); // Replace Country text file
            SaveComboBoxItemsToFile(cbGenre, DataFile.FILE_GENRE); // Replace Genre text file
            // Close all forms
            try
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form != this)
                        form?.Close();
                }
            }
            catch (Exception ex)
            {
                Logs.LogErr("frmMain_FormClosing", ex);
            }
            // Clear imglist
            if (GlobalVars.MOVIE_IMGLIST != null)
            {
                foreach (Image img in GlobalVars.MOVIE_IMGLIST.Images)
                {
                    img?.Dispose();
                }
                GlobalVars.MOVIE_IMGLIST.Images.Clear();
                GlobalVars.MOVIE_IMGLIST.Dispose();
            }
            GlobalVars.TILE_FONT?.Dispose();
            GlobalVars.MOVIE_EXTENSIONS?.Clear();
            GlobalVars.formAbout?.Dispose();
            GlobalVars.formSetting?.Dispose();
            Dispose();
        }
        private void btnChangeView_Click(object sender, EventArgs e)
        {
            lvSearchResult.View = (lvSearchResult.View == View.Tile) ? View.LargeIcon : View.Tile;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (Settings.IsConfirmSearch)
            {
                if (!Msg.ShowYesNo("Are you sure of your search filters?", this)) { return; }
            }
            SearchEntries();
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
            txtSearch.ForeColor = Color.Black;
            txtSearch.Text = SEARCHBOX_PLACEHOLDER;
            txtIMDB.Text = "";
            txtStudio.Text = "";
            txtDirector.Text = "";
            txtCast.Text = "";
            txtYearFrom.Text = "";
            txtYearTo.Text = DateTime.Now.Year.ToString();
            cbCategory.SelectedIndex = 0;
            cbCountry.SelectedIndex = 0;
            cbGenre.SelectedIndex = 0;

            // Perform click on search button: btnSearch
            if (cbClearSearch.CheckState == CheckState.Checked)
            {
                if (Settings.IsConfirmSearch)
                {
                    if (!Msg.ShowYesNo("Reload entries?", this)) {
                        return;
                    }
                }
                SearchEntries();
            }
        }
        // Auto search if Enter key is pressed
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            if (e.KeyCode == Keys.Enter)
            {
                // Check if searchbox is empty
                if ((String.IsNullOrWhiteSpace(txtSearch.Text)==false) && (txtSearch.Text != SEARCHBOX_PLACEHOLDER))
                {
                    // Perform click on search button: btnSearch
                    SearchEntries();
                }
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
        private void cbHideAnim_CheckedChanged(object sender, EventArgs e)
        {
            // Perform Search
            if (Settings.IsConfirmSearch)
            {
                if (!Msg.ShowYesNo("Reload entries?", this)) { return; }
            }
            SearchEntries();
        }
        // Change lvSearchResult Sort by
        private void cbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            try { SortItemsInListView(cbSort.SelectedIndex); }
            catch (Exception ex)
            {
                SortItemsInListView(0);
                Msg.ShowError("frmMain-cbSort_SelectedIndexChanged", ex, "Error occured on sort!", this);
            }
        }

        private void expSearch_ExpandCollapse(object sender, MakarovDev.ExpandCollapsePanel.ExpandCollapseEventArgs e)
        {
            lvSearchResult.Top = expSearch.Bottom + 2;
            lvSearchResult.Height = (ClientRectangle.Height - lvSearchResult.Top) - 2;
        }

        private void btnFixNoInfo_Click(object sender, EventArgs e)
        {
            string qry = $"SELECT * FROM {HCTable.info} " +
                $"WHERE (`{HCInfo.imdb}`=null OR `{HCInfo.imdb}`='' OR `{HCInfo.imdb}`='0') AND " +
                $"(`{HCInfo.anilist}`=null OR `{HCInfo.anilist}`='' OR `{HCInfo.anilist}`='0');";
            PopulateMovieBG(qry, false);
        }

        private void tlbtnSettings_Click(object sender, EventArgs e)
        {
            ShowSettingsForm();
        }

        private void tlbtnAbout_Click(object sender, EventArgs e)
        {
            ShowAboutForm(); //Show About form
        }

        private void tlbtnClean_Click(object sender, EventArgs e)
        {
            GlobalVars.CleanAppDirectory(true);
        }

        private void cbSortOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            try { SortItemsInListView(cbSort.SelectedIndex); }
            catch (Exception ex)
            {
                SortItemsInListView(0);
                Msg.ShowError("frmMain-cbSortOrder_SelectedIndexChanged", ex, "Error occured on sort!", this);
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
