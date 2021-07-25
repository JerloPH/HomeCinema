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
        string SEARCH_QUERY = "";
        string SEARCH_QUERY_PREV = "";
        static bool IsLoadedSuccess = true;

        // Objects
        ListViewColumnSorter lvSorter = new ListViewColumnSorter();
        ToolStripItem toolMenuView, toolMenuEdit, toolMenuFileExplorer;

        #region frmMain
        public frmMain()
        {
            // Contains TMDB_KEY ?
            GlobalVars.HAS_TMDB_KEY = !String.IsNullOrWhiteSpace(GlobalVars.TMDB_KEY);
            // Start app
            InitializeComponent();

            // Form properties
            Icon = GlobalVars.HOMECINEMA_ICON;

            // Change Caption and Title
            Text = $"{GlobalVars.HOMECINEMA_NAME} - Media Organizer (v{GlobalVars.HOMECINEMA_VERSION} r{GlobalVars.HOMECINEMA_BUILD.ToString()})";

            // Load App Settings
            Settings.LoadSettings();

            // Add events to controls
            txtSearch.Text = SEARCHBOX_PLACEHOLDER;

            // Set tooltips for controls
            ToolTip ttShowNew = new ToolTip();
            ttShowNew.SetToolTip(this.btnSettings, "Show Settings form");
            ttShowNew.SetToolTip(this.btnClean, "Clean temporary files");
            ttShowNew.SetToolTip(this.cbHideAnim, "Filter out Animations from Search Results");
            ttShowNew.SetToolTip(this.cbSort, "Change Sorting By");
            ttShowNew.SetToolTip(this.btnChangeView, "Change Item view");

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
            int sizeW = (lvSearchResult.ClientRectangle.Width / 2) - Settings.ImgTileWidth;
            lvSearchResult.TileSize = new Size(sizeW, Settings.ImgTileHeight + 2); // lvSearchResult.Width - (GlobalVars.IMGTILE_WIDTH + 120)
            GlobalVars.MOVIE_IMGLIST.ImageSize = new Size(Settings.ImgTileWidth, Settings.ImgTileHeight);
            GlobalVars.MOVIE_IMGLIST.ColorDepth = ColorDepth.Depth32Bit;
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

            // Populate combobox cbCountry, from file
            PopulateCountryCB();

            // Populate comboBox cbGenre from File
            PopulateGenreCB();

            // Set background color of ListView, from settings 'background color'
            lvSearchResult.BackColor = Settings.ColorBg;

            GlobalVars.Log("frmMain", "All UIs initialized!");
        }
        #endregion
        // ####################################################################################### Database Functions
        #region Insert to Database
        int InsertToDB(List<Medias> dtNewFiles, string errFrom)
        {
            string callFrom = $"frmMain ({Name})-InsertToDB-({errFrom})";
            string logFileInsert = Path.Combine(GlobalVars.PATH_LOG, "NewInsert_Success.log");
            string logFileInsertSkipped = Path.Combine(GlobalVars.PATH_LOG, "NewInsert_Skipped.log");
            string mediatype = "movie";
            string filePath;
            string src;
            bool IsDownloadCover = false;

            int count = 0; // count of inserts, whether success or fail
            string logInsert = ""; // Log succesfully inserted

            foreach (Medias item in dtNewFiles)
            {
                filePath = item.FilePath;
                mediatype = item.MediaType;
                src = item.Source;

                // vars used for entries
                string getIMDB = "";
                string getAnilist = "";
                string mName = "";
                string yearFromFname = "";

                string rTrailer = "";
                string rTitle = "";
                string rOrigTitle = "";
                string rSummary = "";
                string rYear = "";
                string rGenre = "";
                string rArtist = "";
                string rDirector = "";
                string rProducer = "";
                string rCountry = "";
                string rStudio = "";
                string rPosterLink = "";

                // Get proper name, without the folder paths
                try
                {
                    mName = (mediatype == "series") ? new DirectoryInfo(filePath).Name : Path.GetFileNameWithoutExtension(filePath);
                }
                catch (Exception ex)
                {
                    GlobalVars.WriteAppend(logFileInsertSkipped, "Error: " + filePath);
                    GlobalVars.ShowError(callFrom, ex, false, this);
                    continue; // skip when exception thrown
                }

                // Trim Movie Name
                try
                {
                    mName = mName.ToLower();
                    mName = mName.Replace('_', ' ').Replace('.', ' ');
                    mName = mName.Replace("(", "");
                    mName = mName.Replace(")", "");
                    //mName = mName.Replace("-", "");
                    // Remove "year" and "other strings" from movie file name
                    string regExPattern = @"\b\d{4}\b"; // Match 4-digit number in the title
                    Match r = Regex.Match(mName, @regExPattern);
                    yearFromFname = r.Groups[r.Groups.Count - 1].Value;
                    mName = (!String.IsNullOrWhiteSpace(yearFromFname)) ? mName.Substring(0, mName.IndexOf(yearFromFname)) : mName;
                }
                catch (Exception ex)
                {
                    GlobalVars.ShowError(errFrom, ex, false, this);
                }

                // Scrape from TMDB, for info and details
                if (Settings.IsOffline == false)
                {
                    MediaInfo Media = null;
                    if (src == "tmdb" && GlobalVars.HAS_TMDB_KEY)
                    {
                        var movie = TmdbAPI.FindMovieTV(mName, "dummy", mediatype);
                        if (movie?.TotalResults == 1)
                        {
                            Media = 0; //WIP
                        }
                    }
                    else if (src == "anilist" && !String.IsNullOrWhiteSpace(GlobalVars.ANILIST_SECRET))
                    {
                        var anime = AnilistAPI.SearchForAnime(mName);
                        if (anime != null)
                        {
                            try
                            {
                                if (anime.Data.Page.PageInfo.Total == 1)
                                {
                                    getAnilist = anime.Data.Page.MediaList[0].Id.ToString();
                                    Media = AnilistAPI.GetMovieInfoFromAnilist(getAnilist);
                                }
                            }
                            catch { }
                        }
                    }
                    if (Media != null)
                    {
                        rTrailer = Media.Trailer;
                        rTitle = Media.Title;
                        rOrigTitle = Media.OrigTitle;
                        rSummary = Media.Summary;
                        try { rYear = Media.ReleaseDate.Substring(0, 4); }
                        catch { rYear = "0"; }
                        rPosterLink = Media.PosterPath;
                        rArtist = Media.Actor;
                        rDirector = Media.Director;
                        rProducer = Media.Producer;
                        rStudio = Media.Studio;
                        rCountry = GlobalVars.ConvertListToString(Media.Country, ",", callFrom); // Get Country
                        rGenre = GlobalVars.ConvertListToString(Media.Genre, ",", callFrom); // Get Genres
                    }
                }

                // If cannot get info online, make use of defaults
                rTitle = (String.IsNullOrWhiteSpace(rTitle)) ? mName.Trim() : rTitle;
                rYear = (String.IsNullOrWhiteSpace(rYear)) ? yearFromFname : rYear;
                // If Original title is the same as the main title, ignore it
                rOrigTitle = (rOrigTitle.Equals(rTitle)) ? String.Empty : rOrigTitle;

                // Make the DataRow
                var dtInfo = new Dictionary<string, string>();
                var dtFilepath = new Dictionary<string, string>();
                dtInfo.Add(HCInfo.imdb, getIMDB); // IMDB
                dtInfo.Add(HCInfo.anilist, getAnilist); // Anilist
                dtInfo.Add(HCInfo.name, rTitle); // name
                dtInfo.Add(HCInfo.name_orig, rOrigTitle); // episode name
                dtInfo.Add(HCInfo.name_series, ""); // series name
                dtInfo.Add(HCInfo.season, ""); // season number
                dtInfo.Add(HCInfo.episode, ""); // episode num
                dtInfo.Add(HCInfo.country, rCountry); // country
                dtInfo.Add(HCInfo.category, GlobalVars.GetCategoryByFilter(rGenre, rCountry, mediatype, src).ToString()); // category
                dtInfo.Add(HCInfo.genre, rGenre); // genre
                dtInfo.Add(HCInfo.studio, rStudio); // studio
                dtInfo.Add(HCInfo.producer, rProducer); // producer
                dtInfo.Add(HCInfo.director, rDirector); // director
                dtInfo.Add(HCInfo.artist, rArtist); // artist
                dtInfo.Add(HCInfo.year, rYear); // year
                dtInfo.Add(HCInfo.summary, rSummary); // summary

                dtFilepath.Add(HCFile.File, filePath); // filepath
                dtFilepath.Add(HCFile.Sub, GetSubtitleFile(filePath)); // file sub
                dtFilepath.Add(HCFile.Trailer, (!String.IsNullOrWhiteSpace(rTrailer)) ? GlobalVars.LINK_YT + rTrailer : ""); // trailer

                int insertResult = SQLHelper.DbInsertMovie(dtInfo, dtFilepath, callFrom);
                if (insertResult > 0)
                {
                    // Succesfully inserted
                    count += 1; // add to count
                    logInsert += $"{filePath}\n";

                    // Clear prev cover images
                    string movieId = insertResult.ToString();
                    string oldFile = GlobalVars.PATH_TEMP + movieId + ".jpg"; // cover path for temporary cover
                    string newFile = GlobalVars.ImgFullPath(movieId);
                    if (File.Exists(oldFile))
                    {
                        GlobalVars.DeleteMove(oldFile, errFrom); // Delete cover in temp folder
                    }
                    if (File.Exists(newFile) && Settings.IsOffline==false)
                    {
                        GlobalVars.DeleteMove(newFile, errFrom); // Delete existing cover first
                    }
                    // Download cover, if not OFFLINE_MODE
                    if (Settings.IsOffline == false && (!String.IsNullOrWhiteSpace(rPosterLink)))
                    {
                        Thread.Sleep(5); // sleep to prevent overloading API
                        if (GlobalVars.HAS_TMDB_KEY && src == "tmdb") // Use TMDB API
                        {
                            IsDownloadCover = TmdbAPI.DownloadCoverFromTMDB(movieId, rPosterLink, errFrom) && (!String.IsNullOrWhiteSpace(rPosterLink));
                        }
                        else if (src == "anilist") // Use ANILIST API
                        {
                            IsDownloadCover = AnilistAPI.DownloadCoverFromAnilist(movieId, rPosterLink, errFrom);
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
                            GlobalVars.ShowError(errFrom, ex, false, this);
                        }
                    }
                }
                Thread.Sleep(10); // Prevent continuous request to TMDB, prevents overloading the site.
            }
            dtNewFiles.Clear();
            GlobalVars.WriteAppend(logFileInsert, logInsert);
            return count;
        }
        // return filepath from DB
        private string GetFilePath(string ID, string calledFrom)
        {
            string ret = "";
            string errFrom = $"frmMain-GetFilePath [calledFrom: {calledFrom}]";
            string qry = $"SELECT `file` FROM { GlobalVars.DB_TNAME_FILEPATH } WHERE `Id`={ID} LIMIT 1;";
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
        public static void AfterPopulatingMovieLV(ListView lv, int count = 0)
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
                GlobalVars.Log(errFrom, $"ResultSet is null or empty!");
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
            // Search the db for movie with filters
            // Setup columns needed
            string qry = "";
            SEARCH_QUERY = ""; // reset query

            // Default SELECT Query
            qry = $"SELECT * FROM {GlobalVars.DB_TNAME_INFO}";

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
                    qry += $"`{HCInfo.year}` BETWEEN {txtYearFrom.Text} AND {DateTime.Now.Year.ToString()}";
                }
            }
            // Genre
            qry += (cbGenre.SelectedIndex > 0) ? SQLHelper.QryWhere(qry) + $"`{HCInfo.genre}` LIKE '%{cbGenre.Text}%'" : "";

            // Category
            if (cbCategory.SelectedIndex > 0)
            {
                int index = cbCategory.SelectedIndex;
                qry += SQLHelper.QryWhere(qry);
                // Search for all
                if ((index < 1) || (index > 2))
                {
                    qry += $"`{HCInfo.category}`={index}";
                }
                else
                {
                    // Search for All type of Movies, if index == 1. Otherwise, Search for All types of Series
                    qry += (index == 1) ? $" (`{HCInfo.category}`=1 OR `{HCInfo.category}`=3 OR `{HCInfo.category}`=5)" : $" (`{HCInfo.category}`=2 OR `{HCInfo.category}`=4 OR `{HCInfo.category}`=6)";
                }
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
                qry = $"SELECT * FROM {GlobalVars.DB_TNAME_INFO} WHERE `{HCInfo.imdb}` = '{txtIMDB.Text}'";
            }

            // Filter out all animations
            qry += (cbHideAnim.CheckState == CheckState.Checked) ? SQLHelper.QryWhere(qry) + $" (`{HCInfo.category}` <= 2)" : "";

            // Append to end
            qry += (Settings.ItemLimit > 0) ? $" LIMIT {Settings.ItemLimit};" : "";

            // Set query to perform on search
            SEARCH_QUERY = qry;
            // Re-populate ListView of movies
            PopulateMovieBG();
        }
        // Play Movie or Open Movie Details
        public void OpenFormPlayMovie()
        {
            // A movie/show is selected
            if (lvSearchResult.SelectedItems.Count > 0)
            {
                // Validate ID
                int ID;
                try { ID = Convert.ToInt16(lvSearchResult.SelectedItems[0].Tag.ToString().TrimStart('0')); }
                catch { return; };

                if (ID < 1) { return;  }; // exit if ID is less than 1

                // Just play the media
                if (Settings.IsAutoplay)
                {
                    GlobalVars.PlayMedia(GetFilePath(ID.ToString(), "frmMain-OpenNewFormMovie"));
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
            int ID = 0;
            try { ID = Convert.ToInt32(lvSearchResult.SelectedItems[0].Tag.ToString().TrimStart('0')); }
            catch { return; };
            if (ID < 1) { return; };

            if (lvSearchResult.SelectedItems.Count > 0)
            {
                // Create form to View Movie Details / Info
                string text = Convert.ToString(lvSearchResult.SelectedItems[0].Text);
                string formName = "movie" + ID.ToString();
                Form fc = Application.OpenForms[formName];
                if (fc != null)
                {
                    fc.Focus();
                }
                else
                {
                    Form form = new frmMovie(this, ID.ToString(), text, lvSearchResult.SelectedItems[0]);
                    form.Name = formName;
                }
            }
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
                string qry = $"SELECT * FROM {GlobalVars.DB_TNAME_INFO} WHERE `Id`={MOVIEID} LIMIT 1;";

                using (DataTable dtFile = SQLHelper.DbQuery(qry, "frmMain-UpdateMovieItemOnLV")) // run the query
                {
                    // Check if there are results
                    if (dtFile.Rows.Count > 0)
                    {
                        try
                        {
                            DataRow r = dtFile.Rows[0];
                            // Get all strings from the DataRow, passed by the BG worker
                            string r1 = r[HCInfo.name].ToString(); // name
                            string r2 = r[HCInfo.name_orig].ToString(); // name_ep
                            string r3 = r[HCInfo.name_series].ToString(); // name_series
                            string r4 = r[HCInfo.season].ToString(); // season
                            string r5 = r[HCInfo.episode].ToString(); // episode
                            string r6 = r[HCInfo.year].ToString(); // year
                            string r7 = r[HCInfo.summary].ToString(); // summary
                            string r8 = r[HCInfo.genre].ToString(); // genre

                            // Edit Information on ListView Item
                            LVItemSetDetails(lvItem, new string[] { MOVIEID.ToString(), r1, r2, r3, r4, r5, r6, r7, r8 });
                        }
                        catch { }
                    }
                }
                // Refresh imagelist and lvSearchResult to reflect changes to info and Image
                lvSearchResult.Refresh();
            }
        }
        // Execute the query, by running bgWorker bgSearchInDB
        public void RefreshMovieList(bool AppStart = false)
        {
            // Check if there was prev query
            if (!String.IsNullOrWhiteSpace(SEARCH_QUERY_PREV))
            {
                SEARCH_QUERY = SEARCH_QUERY_PREV;
            }
            // Check if SEARCH_QUERY is empty
            if (String.IsNullOrWhiteSpace(SEARCH_QUERY))
            {
                // Default SELECT Query
                SEARCH_QUERY = $"SELECT * FROM {GlobalVars.DB_TNAME_INFO}";
            }
            PopulateMovieBG(AppStart);
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
            int MOVIEID;
            string MovieId = InfoString[0]; // ID
            string resName = InfoString[1]; // name
            string resNameEp = InfoString[2]; // name_ep
            string resNameSer = InfoString[3]; // name_series
            string resSeason = InfoString[4]; // season
            string resEp = InfoString[5]; // episode
            string resYear = InfoString[6]; // year
            string resSummary = InfoString[7]; // summary
            string resGenre = InfoString[8]; // genre
            // Convert MovieId string to int
            try { MOVIEID = Convert.ToInt32(MovieId); }
            catch { MOVIEID = 0; }

            if (MOVIEID > 0)
            {
                // Set default text for item
                temp.Text = resName;

                // Append ToolTip on it
                temp.ToolTipText = "Summary: \n" + GlobalVars.LimitString(resSummary, 350) + "\n\nGenre:\n" + resGenre.Replace(",", ", ");

                // Is it a Movie? (by checking if there are no season)
                // Add sub-item for Series Name, or Episode Name
                if (String.IsNullOrWhiteSpace(resSeason))
                {
                    temp.SubItems.Add(resNameSer); // Series Name
                    temp.SubItems.Add(resNameEp); // Episode Name
                }
                else
                {
                    // Set Series Name
                    temp.SubItems.Add(resNameEp); // Episode Name
                    temp.SubItems.Add("S" + GlobalVars.ValidateNum(resSeason) + " E" + GlobalVars.ValidateNum(resEp));
                }
                // Year
                temp.SubItems.Add(resYear);
                // Display image (From ImageList) based on ImageKey
                temp.ImageKey = GlobalVars.ImgGetKey(MOVIEID.ToString());
                // Add year to name/title of MOVIE
                temp.Text = temp.Text + $" ({resYear})";

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
            GlobalVars.TEXT_COUNTRY = GlobalVars.BuildArrFromFile(GlobalVars.FILE_COUNTRY, "frmMain-PopulateCountryCB [FILE_COUNTRY]");
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
            GlobalVars.TEXT_GENRE = GlobalVars.BuildArrFromFile(GlobalVars.FILE_GENRE, "frmMain-PopulateGenreCB [FILE_GENRE]");
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
        private void SaveCountryCB()
        {
            // save cbCountry contents to FILE_COUNTRY
            string toWrite = "";
            var list = cbCountry.Items.Cast<string>().OrderBy(s => s);
            foreach (string item in list)
            {
                if (item.Equals("All"))
                    continue;
                toWrite += item + ",";
            }
            toWrite = toWrite.TrimEnd(',');
            GlobalVars.WriteToFile(GlobalVars.FILE_COUNTRY, toWrite);
        }
        private void SaveGenreCB()
        {
            // save cbGenre contents to FILE_GENRE
            string toWrite = "";
            var list = cbGenre.Items.Cast<string>().OrderBy(s => s);
            foreach (string item in list)
            {
                if (item.Equals("All"))
                    continue;
                toWrite += item + ",";
            }
            toWrite = toWrite.TrimEnd(',');
            GlobalVars.WriteToFile(GlobalVars.FILE_GENRE, toWrite);
        }
        private int ControlLeftFromAttach(Control src, Control attachedTo) // return left property appropriate to control attached to another control
        {
            return attachedTo.Left - (src.Width - 4);
        }
        #endregion
        // ####################################################################################### BACKGROUND WORKERS
        #region BG Worker: Get files in folders
        // Search all Movie files in folder
        private void GetMediaFromFolders()
        {
            // Get Movie files on Folder, even subFolder
            string calledFrom = $"frmMain-GetMediaFromFolders()";
            GlobalVars.Log(calledFrom, "Search for Supported Media files in Folders..");

            // Declare vars
            frmLoading form = new frmLoading("Getting media files from directories..", "Loading");
            var listAlreadyinDB = new List<string>();
            var dtNewFiles = new List<Medias>();
            int countMediaLoc = 0;
            int countMediaLocMax = 0;
            string logSkipped = Path.Combine(GlobalVars.PATH_LOG, "SkippedEntries.Log");
            int insertRes = 0;

            // Delegate task to frmLoading
            form.BackgroundWorker.DoWork += (sender1, e1) =>
            {
                // Build a list of Files in Directories from medialocation.hc-data
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

                // Build list of folders to search from
                GlobalVars.LoadMediaLocations();
                if (GlobalVars.MEDIA_LOC?.Count < 1)
                {
                    var folder = GlobalVars.GetDirectoryFolder("Select folder to search for Movie files"); // Browse for Dir
                    GlobalVars.MEDIA_LOC.Add(new MediaLocations(folder, "movie", "tmdb"));
                    // Save MEDIA_LOC list to file
                }

                listAlreadyinDB = SQLHelper.DbQrySingle(GlobalVars.DB_TNAME_FILEPATH, "[file]", calledFrom + "-listAlreadyinDB");
                countMediaLocMax = GlobalVars.MEDIA_LOC.Count;
                foreach (MediaLocations mediaLoc in GlobalVars.MEDIA_LOC)
                {
                    countMediaLoc += 1;
                    form.UpdateMessage($"Loading directories ({countMediaLoc}/{countMediaLocMax})..");
                    GlobalVars.Log(calledFrom, $"Searching in: ({mediaLoc.Path}), Mediatype: {mediaLoc.MediaType}, Source: {mediaLoc.Source}");
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
                                                    dtNewFiles.Add(new Medias(file, mediaLoc.MediaType, mediaLoc.Source));
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
                                                dtNewFiles.Add(new Medias(file, mediaLoc.MediaType, mediaLoc.Source));
                                            }
                                        }
                                        else
                                        {
                                            GlobalVars.WriteAppend(logSkipped, $"Not supported ext: {file}");
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        GlobalVars.WriteAppend(logSkipped, $"Error: {file}");
                                        GlobalVars.ShowError(calledFrom, ex, false, this);
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
                                            dtNewFiles.Add(new Medias(folderName, mediaLoc.MediaType, mediaLoc.Source));
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
                                        GlobalVars.WriteAppend(logSkipped, $"Directory does not exist: {folderName}");
                                    }
                                }
                            }
                        }
                    }
                }
                form.UpdateMessage($"Inserting {dtNewFiles?.Count} new found media files..");
                insertRes = InsertToDB(dtNewFiles, calledFrom + "-dtNewFiles");
                // Clear previous lists
                listAlreadyinDB?.Clear();
            };
            form.ShowDialog(this);
            if (insertRes > 0)
            {
                GlobalVars.ShowInfo($"Successfully inserted {insertRes} new entries!");
            }
            RefreshMovieList(true);
        }
        #endregion
        #region BG Worker: Populate MOVIE ListView
        private void PopulateMovieBG(bool AppStart = false)
        {
            // Stop ListView form Drawing
            lvSearchResult.BeginUpdate(); // Pause drawing events on ListView
            lvSearchResult.SuspendLayout();
            lvSearchResult.Items.Clear(); // Clear previous list
            // Populate movie listview with new entries, from another form thread
            frmLoading form = new frmLoading(AppStart ? "Loading collection.." : "Searching..", "Loading");
            string qry = SEARCH_QUERY;
            string errFrom = "frmMain-PopulateMovieBG()";
            string fileNamePath;
            int progress = 0;
            int progressMax = 0;

            // If no query
            if (String.IsNullOrWhiteSpace(qry))
            {
                // Exit
                AfterPopulatingMovieLV(lvSearchResult);
                return;
            }

            // SET as Previous query
            SEARCH_QUERY_PREV = SEARCH_QUERY;

            // Count progress
            progress = 0;
            using (DataTable dt = SQLHelper.DbQuery(qry, errFrom)) // Get DataTable from query
            {
                // Set Max Progress
                if (dt != null)
                {
                    progressMax = dt.Rows.Count;
                }

                // Iterate thru all DataRows
                if (progressMax > 0)
                {
                    form.BackgroundWorker.DoWork += (sender1, e1) =>
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            // Add Item to ListView
                            // Convert ID object to ID int
                            long MOVIEID;
                            var x = HCInfo.Id;
                            try { MOVIEID = Convert.ToInt64(r[x]); }
                            catch { MOVIEID = 0; GlobalVars.Log(errFrom, $"Invalid MovieID: {r[x].ToString()}"); }

                            // Add to listview lvSearchResult
                            if (MOVIEID > 0)
                            {
                                // Skip entry if file does not exist
                                fileNamePath = GetFilePath(MOVIEID.ToString(), errFrom);
                                if (!String.IsNullOrWhiteSpace(fileNamePath))
                                {
                                    try
                                    {
                                        FileAttributes attr = File.GetAttributes(fileNamePath);
                                        if (attr.HasFlag(FileAttributes.Directory))
                                        {
                                            // Non existing directory, skip it
                                            if (!Directory.Exists(fileNamePath))
                                            {
                                                continue;
                                            }
                                        }
                                        else
                                        {
                                            if (!File.Exists(fileNamePath))
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                    catch (DirectoryNotFoundException)
                                    {
                                        continue;
                                    }
                                    catch (FileNotFoundException)
                                    {
                                        continue;
                                    }
                                    catch (Exception ex)
                                    {
                                        GlobalVars.ShowError(errFrom, ex, false);
                                        continue;
                                    }
                                }

                                // Load 'cover' Image from 'cover' folder
                                if (AppStart)
                                {
                                    string Imagefile = GlobalVars.ImgFullPath(MOVIEID.ToString());
                                    try
                                    {
                                        if (File.Exists(Imagefile))
                                        {
                                            this.Invoke(new Action(() =>
                                            {
                                                Image imageFromFile = Image.FromFile(Imagefile);
                                                GlobalVars.MOVIE_IMGLIST.Images.Add(Path.GetFileName(Imagefile), imageFromFile);
                                                imageFromFile.Dispose();
                                            }));
                                        }
                                    }
                                    catch (Exception exImg)
                                    {
                                        GlobalVars.ShowError($"{errFrom}\n\tFile:\n\t{Imagefile}", exImg, false);
                                    }
                                }

                                try
                                {
                                    // Get all strings from the DataRow, passed by the BG worker
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
                                    LVItemSetDetails(temp, new string[] { MOVIEID.ToString(),
                                    resName, resNameEp, resNameSer,
                                    resSeason, resEp, resYear, resSum, resGenre });

                                    // Add Item to ListView lvSearchResult
                                    AddItem(lvSearchResult, temp);
                                    progress += 1;
                                }
                                catch { }
                            }
                        }
                        GlobalVars.Log(errFrom, $"DONE Background worker from: {Name}");
                        if (!GlobalVars.HAS_TMDB_KEY && AppStart)
                        {
                            GlobalVars.ShowWarning(GlobalVars.MSG_NO_TMDB);
                        }
                    };
                    form.ShowDialog();
                }
            }
            AfterPopulatingMovieLV(lvSearchResult, progress);
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
                    string MOVIE_NAME = lvSearchResult.SelectedItems[0].Text.ToString();
                    string childForm = GlobalVars.PREFIX_MOVIEINFO + MOVIE_ID;
                    GlobalVars.OpenFormMovieInfo(this, childForm, MOVIE_ID, MOVIE_NAME, $"{errFrom} [toolMenuEdit]", lvSearchResult.SelectedItems[0]);
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
            GlobalVars.CheckLogFile(GlobalVars.FILE_LOG_APP, "frmMain-(Delete AppLog)", Text + "\n  : Start of LogFile");
            GlobalVars.CheckLogFile(GlobalVars.FILE_LOG_DB, "frmMain-(Delete App_DB.log)", Text + "\n  : Database Log");
            GlobalVars.CheckLogFile(GlobalVars.FILE_LOG_ERROR, "frmMain-(Delete App_ErrorLog.log)", Text + "\n  : Error Logs");

            // Put default Image on ImageList
            try
            {
                GlobalVars.MOVIE_IMGLIST.Images.Clear();
                string imageFilePath = GlobalVars.ImgFullPath("0");
                string Imagefile = (File.Exists(imageFilePath)) ? imageFilePath : GlobalVars.FILE_DEFIMG;
                using (Image imgFromFile = Image.FromFile(Imagefile))
                {
                    GlobalVars.MOVIE_IMGLIST.Images.Add(Path.GetFileName(Imagefile), imgFromFile);
                }
            }
            catch (Exception exc)
            {
                GlobalVars.ShowError("frmMain-Load", exc, false, this);
                GlobalVars.ShowWarning("Default image is missing!\nRestart App", "", this);
                IsLoadedSuccess = false;
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
                    GlobalVars.ShowWarning("Database is possibly corrupted!\nDelete HomeCinema.db and try again\nNOTE: This will remove all your entries", "", loadForm);
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
            int buttonWidth = (int)(this.Width * 0.13);
            txtIMDB.Width = buttonWidth; // top row
            txtDirector.Width = buttonWidth;
            txtStudio.Width = buttonWidth;
            cbGenre.Width = buttonWidth;
            txtYearFrom.Width = (int)(buttonWidth * 0.46); // bottom row
            txtYearTo.Width = (int)(buttonWidth * 0.46);
            txtCast.Width = buttonWidth;
            cbCountry.Width = buttonWidth;
            cbCategory.Width = buttonWidth;

            // Reposition controls
            txtIMDB.Left = (int)(this.Width * 0.07); // top row
            txtDirector.Left = (int)(this.Width * 0.27);
            txtStudio.Left = (int)(this.Width * 0.47);
            cbGenre.Left = (int)(this.Width * 0.68);
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
            
            // Reposition Clear and 'Search after clear' checkbox
            btnClear.Left = (int)(this.Width - btnClear.Width) - 32;
            cbClearSearch.Left = btnClear.Left;
            cbHideAnim.Left = (int)(this.Width - cbHideAnim.Width) - 32; // reposition 'Hide animations' checkbox
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save settings
            Settings.SaveSettings();
            // Save text files
            SaveCountryCB(); // Replace Country text file
            SaveGenreCB(); // Replace Genre text file
            if (GlobalVars.MOVIE_IMGLIST != null)
            {
                GlobalVars.MOVIE_IMGLIST.Images.Clear();
                GlobalVars.MOVIE_IMGLIST.Dispose();
            }
            GlobalVars.HOMECINEMA_ICON?.Dispose();
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
                if (!GlobalVars.ShowYesNo("Are you sure of your search filters?", this)) { return; }
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

            // Clear values of vars
            SEARCH_QUERY = "";

            // Perform click on search button: btnSearch
            if (cbClearSearch.CheckState == CheckState.Checked)
            {
                if (Settings.IsConfirmSearch)
                {
                    if (!GlobalVars.ShowYesNo("Reload entries?", this)) {
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
        // Show "Settings"" form.
        private void btnSettings_Click(object sender, EventArgs e)
        {
            ShowSettingsForm();
        }
        // Delete files from temp
        private void btnClean_Click(object sender, EventArgs e)
        {
            GlobalVars.CleanAppDirectory(true);
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
                if (!GlobalVars.ShowYesNo("Reload entries?", this)) { return; }
            }
            SearchEntries();
        }
        private void btnAbout_Click(object sender, EventArgs e)
        {
            //Show About form
            ShowAboutForm();
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

            } catch (Exception ex)
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
