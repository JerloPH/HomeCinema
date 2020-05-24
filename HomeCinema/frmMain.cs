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

namespace HomeCinema
{
    public partial class frmMain : Form
    {
        bool Start = true;
        SQLHelper DBCON = new SQLHelper("frmMain");
        //ImageList MovieIcons = GlobalVars.MOVIE_IMGLIST;
        Form formLoading = null;
        string SEARCH_QUERY = "";
        string SEARCH_COLS = "[Id],[name],[name_ep],[name_series],[season],[episode],[year]";
        string SEARCH_QUERY_PREV = "";
        string[] FOLDERTOSEARCH = { "" };
        ListViewColumnSorter lvSorter = new ListViewColumnSorter();
        BackgroundWorker bgWorkInsertMovie = new BackgroundWorker();
        BackgroundWorker bgSearchInDB = new BackgroundWorker();

        public frmMain()
        {
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
            Text = GlobalVars.HOMECINEMA_NAME + " - Media Organizer (v" + GlobalVars.HOMECINEMA_VERSION + " r" + GlobalVars.HOMECINEMA_BUILD.ToString() + ")";

            // Delete previous log file, if exceeds file size limit
            GlobalVars.CheckLogFile(GlobalVars.FILE_APPLOG, "frmMain-frmMain", Text + "\n  : Start of LogFile");

            // Add events to controls
            txtSearch.Text = GlobalVars.SEARCHBOX_PLACEHOLDER;
            txtSearch.GotFocus += new EventHandler(SearchBoxPlaceholderClear);
            txtSearch.LostFocus += new EventHandler(SearchBoxPlaceholder);

            // Change control properties
            btnSort.Tag = 0;
            btnSort.Text = GlobalVars.TEXT_SORTBY[0];

            // Load Cover Images from folder, by Populating ImageList
            GlobalVars.PopulateCover();

            // Setup listview lvSearchResult
            lvSearchResult.Columns.Add("ColName");
            lvSearchResult.Columns.Add("ColEpName");
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

            // Perform click on Change View
            //btnChangeView.PerformClick();

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
            //string tempFolder = GlobalVars.GetSingleLine(GlobalVars.FILE_MEDIALOC, "frmMain"); // Get directory to start search
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

            // Add events to BG Worker for Searching movie files in a folder
            bgWorkInsertMovie.DoWork += new DoWorkEventHandler(bgw_SearchFileinFolder);
            bgWorkInsertMovie.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_DoneSearchFileinFolder);

            // Add events to BG Worker for Searching movie in Database
            bgSearchInDB.DoWork += new DoWorkEventHandler(bgw_SearchMovie);
            bgSearchInDB.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_DoneSearchMovie);

            // Start finding files in folder
            getAllMediaFiles();

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
                row[1] = Path.GetFileNameWithoutExtension(filePath); // name
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
                row[16] = ""; // file sub
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
        // Open new form for MOVIE
        public void OpenNewFormMovie()
        {
            // A movie/show is selected
            if (lvSearchResult.SelectedItems.Count > 0)
            {
                string ID = Convert.ToString(lvSearchResult.SelectedItems[0].Tag).TrimStart(new Char[] { '0' });
                // Exit if not a valid ID
                if (Convert.ToInt16(ID) < 1)
                {
                    return;
                }
                // Otherwise, continue
                string text = Convert.ToString(lvSearchResult.SelectedItems[0].Text);
                string formName = "movie" + ID;
                Form fc = Application.OpenForms[formName];
                if (fc != null)
                {
                    fc.Focus();
                }
                else
                {
                    Form form = new frmMovie(this, ID, text);
                    form.Name = formName;
                    GlobalVars.Log(Name + " (OPEN a MOVIE)", "MOVIE formName: " + form.Name);
                    //form.Show(this);
                }
            }
        }
        // Get all Media files from folder in medialocation file
        private void getAllMediaFiles()
        {
            // Start the worker to fetch all data
            bgWorkInsertMovie.RunWorkerAsync();

            // Display the loading form.
            DisplayLoading();
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
                // Run BG Worker: bgSearchInDB, for Searching movies in database
                bgSearchInDB.RunWorkerAsync();

                // Display the loading form.
                DisplayLoading();
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
        // ############################################################################## BACKGROUND WORKERS
        private void bgw_SearchMovie(object sender, DoWorkEventArgs e)
        {
            // Get query from variable, set by background worker
            string qry = SEARCH_QUERY;
            string cols = SEARCH_COLS;
            string errFrom = "frmMain-bgw_SearchMovie";
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

            // Log Query
            GlobalVars.Log(errFrom, $"START Background worker from: {Name}");
            DataTable dt = DBCON.DbQuery(qry, cols, errFrom);
            if (dt.Rows.Count > 0)
            {
                e.Result = dt;
            }
            else
            {
                e.Result = null;
            }
        }
        private void bgw_DoneSearchMovie(object sender, RunWorkerCompletedEventArgs e)
        {
            // Clear previous list
            lvSearchResult.Items.Clear();

            // Starting, opening of App?
            if (Start)
            {
                // Perform click on Change View
                btnChangeView.PerformClick();
                // Perform click on Sort
                btnSort.PerformClick();
                Start = false;
            }

            // Retrieve the result pass from bg_DoWork() if any.
            try
            {
                // If result is null, exit;
                if (e.Result == null)
                {
                    ListViewItem temp = new ListViewItem() { Text = "No Search Results!" };
                    temp.Tag = "0";
                    temp.ImageIndex = 0;
                    lvSearchResult.Items.Add(temp);
                    CloseLoading(); // Close loading form
                    return;
                }

                // Get result, cast to object type
                GlobalVars.Log("frmMain-bgw_DoneSearchMovie", "RETRIEVES data from Previous Search query in bgWorker");
                DataTable dt = null;
                if (e.Result is DataTable)
                {
                    // Set the e.Result from BgWorker as DT
                    dt = e.Result as DataTable;
                    
                    // Load Icon Pics from folder
                    GlobalVars.PopulateCover();
                    // Add to listview lvSearchResult
                    foreach (DataRow r in dt.Rows)
                    {
                        var MOVIEID = Convert.ToInt32(r[0]);
                        var r1 = r[1]; // name
                        var r2 = r[2]; // name_ep
                        var r3 = r[3]; // name_series
                        var r4 = r[4]; // season
                        var r5 = r[5]; // episode
                        var r6 = r[6]; // year

                        // Make new ListView item, and assign properties to it
                        ListViewItem temp = new ListViewItem() { Text = r1.ToString() };

                        // Is it a Movie? (by checking if there are no season)
                        // Add sub-item for Series Name, or Episode Name
                        if (String.IsNullOrWhiteSpace(r4.ToString()))
                        {
                            temp.SubItems.Add(r3.ToString()); // Series Name
                            temp.SubItems.Add(r2.ToString()); // Episode Name
                        }
                        else
                        {
                            // Set Series Name
                            if (String.IsNullOrWhiteSpace(r3.ToString()))
                            {
                                temp.Text = r3.ToString(); //Set Series Name
                            }
                            temp.SubItems.Add(r2.ToString()); // Episode Name
                            temp.SubItems.Add("S" + GlobalVars.ValidateNum(r4.ToString()) + " E" + GlobalVars.ValidateNum(r5.ToString()));
                        }

                        // Display image (From ImageList) based on ID
                        string imgKey = Convert.ToString(MOVIEID) + ".jpg";
                        if (GlobalVars.MOVIE_IMGLIST.Images.ContainsKey(imgKey))
                        {
                            temp.ImageIndex = GlobalVars.MOVIE_IMGLIST.Images.IndexOfKey(imgKey); //CC;
                        }
                        else
                        {
                            temp.ImageIndex = 0;
                        }

                        // Add year to name/title of MOVIE
                        temp.Text = temp.Text + $" ({r6.ToString()})";

                        // Save the ID as Tag, to NOT SHOW it on LIST
                        temp.Name = Convert.ToString(MOVIEID);
                        temp.Tag = GlobalVars.ValidateZero(MOVIEID);
                        lvSearchResult.Items.Add(temp);
                    }
                }
                // Dispose the table after iterating thru its contents
                GlobalVars.Log("frmMain-bgw_DoneSearchMovie", "Done with results!");
                dt.Clear();
                dt.Dispose();

                // If there are no results, show message
                if (lvSearchResult.Items.Count < 1)
                {
                    ListViewItem temp = new ListViewItem() { Text = "No Search Results!" };
                    temp.Tag = "0";
                    temp.ImageIndex = 0;
                    lvSearchResult.Items.Add(temp);
                    //GlobalVars.ShowInfo("There are no results for search query '" + text + "'!");
                }

                // Clear previous values of variables
                SEARCH_QUERY = "";
                //lvSearchResult.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                //lvSearchResult.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception exc)
            {
                GlobalVars.ShowError("frmMain-bgwDoneSearchMovie (General Exception)", exc.Message);
            }
            finally
            {
                // Close loading and refresh Memory
                CloseLoading();
            }
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
        // ############################################################################## Form Control events
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalVars.Log("frmMain.Designer-Dispose", "Exiting....");
            // Replace genre file
            GlobalVars.WriteArray(GlobalVars.TEXT_GENRE, GlobalVars.FILE_GENRE);

            // Replace country file
            string[] arrCountry = GlobalVars.BuildStringArrayFromCB(cbCountry);
            //Array.Sort(arrCountry);
            GlobalVars.WriteArray(arrCountry, GlobalVars.FILE_COUNTRY);

            // Dispose All Resources
            if (formLoading != null)
            {
                formLoading.Dispose();
            }
            // Clean each image 1 by 1
            GlobalVars.Log("frmMain-frmMain_FormClosing", "Disposing MOVIE_IMGLIST");
            foreach (Image img in GlobalVars.MOVIE_IMGLIST.Images)
            {
                if (img != null)
                {
                    img.Dispose();
                }
            }
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
            SEARCH_COLS = "[Id],[name],[name_ep],[name_series],[season],[episode],[year]";

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

            // Run BG Worker: bgSearchInDB, for Searching movies in database
            bgSearchInDB.RunWorkerAsync();

            // Display the loading form.
            DisplayLoading();
        }
        // When double-clicked on an item, open it in new form
        private void lvSearchResult_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Call function OpenNewFormMovie
            OpenNewFormMovie();
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
        // Sort Tiles
        private void btnSort_Click(object sender, EventArgs e)
        {
            int toggle = 0;
            if (btnSort.Tag != null)
            {
                toggle = Convert.ToInt32(btnSort.Tag);
            }
            toggle += 1;
            if (toggle > GlobalVars.TEXT_SORTBY.Length - 1)
            {
                toggle = 0;
            }

            btnSort.Tag = toggle;
            btnSort.Text = GlobalVars.TEXT_SORTBY[toggle];

            // Peform sort
            switch (toggle)
            {
                case 0:
                    //GlobalVars.ShowInfo("Sorted default");
                    lvSearchResult.Sorting = SortOrder.None;
                    lvSearchResult.ListViewItemSorter = null;
                    var items = lvSearchResult.Items.Cast<ListViewItem>().OrderBy(x => x.Tag.ToString()).ToList();
                    lvSearchResult.Items.Clear();
                    lvSearchResult.Items.AddRange(items.ToArray());
                    break;
                case 1:
                    //GlobalVars.ShowInfo("Sorted AZ");
                    lvSorter.SortColumn = 0;
                    lvSorter.Order = SortOrder.Ascending;
                    lvSearchResult.ListViewItemSorter = lvSorter;
                    lvSearchResult.Sort();
                    break;
                case 2:
                    //GlobalVars.ShowInfo("Sorted year");
                    lvSorter.SortColumn = 2;
                    lvSorter.Order = SortOrder.Descending;
                    lvSearchResult.ListViewItemSorter = lvSorter;
                    lvSearchResult.Sort();
                    break;
            }
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
            SEARCH_COLS = "[Id],[name],[name_ep],[name_series],[season],[episode],[year]";
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
                OpenNewFormMovie();
            }
        }
    }
}
