﻿using System;
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
        SQLHelper DBCON = new SQLHelper();
        //ImageList MovieIcons = GlobalVars.MOVIE_IMGLIST;
        Form formLoading = new frmLoading();
        string SEARCH_QUERY = "";
        string SEARCH_COLS = "";
        string FOLDERTOSEARCH = "";
        string[] MOVIE_EXTENSIONS = { ".mp4", ".mkv", ".ts" };
        ListViewColumnSorter lvSorter = new ListViewColumnSorter();
        BackgroundWorker bgWorkInsertMovie = new BackgroundWorker();

        public frmMain()
        {
            // First Time to run app?
            //bool Start = DBCON.ExistingDB; // First-time running the app?

            // Create directories
            GlobalVars.CreateDir(GlobalVars.PATH_IMG);
            GlobalVars.CreateDir(GlobalVars.PATH_DATA);

            // Check files first
            GlobalVars.CheckAllFiles();

            // Start app
            InitializeComponent();

            // Form properties
            FormClosing += new FormClosingEventHandler(frmMain_FormClosing);
            Icon = new Icon(GlobalVars.FILE_ICON);

            // Change Caption and Title
            Text = GlobalVars.HOMECINEMA_NAME + " v" + GlobalVars.HOMECINEMA_VERSION + " " + GlobalVars.HOMECINEMA_BUILD;

            // Delete previous log file, if exceeds file size limit
            if (File.Exists(GlobalVars.PATH_LOG))
            {
                FileInfo f = new FileInfo(GlobalVars.PATH_LOG);
                if (f.Length > GlobalVars.SET_LOGMAXSIZE)
                {
                    File.Delete(GlobalVars.PATH_LOG);
                    GlobalVars.Log("frmMain-frmMain", Text + "\n  : Start of LogFile");
                }
            }

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

            lvSearchResult.LargeImageList = GlobalVars.MOVIE_IMGLIST;
            lvSearchResult.SmallImageList = GlobalVars.MOVIE_IMGLIST;
            lvSearchResult.View = View.LargeIcon;
            lvSearchResult.TileSize = new Size((lvSearchResult.ClientRectangle.Width / 2) - 35, GlobalVars.IMGTILE_HEIGHT); // lvSearchResult.Width - (GlobalVars.IMGTILE_WIDTH + 120)
            GlobalVars.MOVIE_IMGLIST.ImageSize = new Size(GlobalVars.IMGTILE_WIDTH, GlobalVars.IMGTILE_HEIGHT);
            GlobalVars.MOVIE_IMGLIST.ColorDepth = ColorDepth.Depth32Bit;
            lvSearchResult.AllowDrop = false;
            lvSearchResult.AllowColumnReorder = false;
            lvSearchResult.MultiSelect = false;
            foreach (ListViewItem lv in lvSearchResult.Items)
            {
                lv.Font = GlobalVars.TILE_FONT;
            }

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
            GlobalVars.TEXT_GENRE = GlobalVars.BuildArrFromFile(GlobalVars.FILE_GENRE, "frmMain-frmMain[FILE_GENRE");
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

            // Perform background worker that Automatically inserts all movies from designated folder
            // It checks if it exists first on database, using filepath
            string tempFolder = GlobalVars.GetSingleLine(GlobalVars.FILE_MEDIALOC, "frmMain"); // Get directory to start search
            if (String.IsNullOrWhiteSpace(tempFolder))
            {
                FOLDERTOSEARCH = GlobalVars.GetDirectoryFolder("Select folder to search for media files"); // Browse for Dir
                GlobalVars.WriteToFile(GlobalVars.FILE_MEDIALOC, FOLDERTOSEARCH);
            }
            else
            {
                FOLDERTOSEARCH = tempFolder; // Load existing dir from file
            }

            // Add events to BG Worker for Searching movie files in a folder
            bgWorkInsertMovie.DoWork += new DoWorkEventHandler(bgw_SearchFileinFolder);
            bgWorkInsertMovie.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_DoneSearchFileinFolder);

            // Start finding files in folder
            getAllMediaFiles();

        }
        // ############################################################################## Functions
        private void getAllMediaFiles()
        {
            // Start the worker to fetch all data
            bgWorkInsertMovie.RunWorkerAsync();

            // Display the loading form.
            formLoading.ShowDialog(this);
            formLoading.Focus();
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
        
        // ############################################################################## BACKGROUND WORKERS
        private void bgw_SearchMovie(object sender, DoWorkEventArgs e)
        {
            // formLoading.Focus(); // set focus to loading

            // Get query from variable, set by background worker
            string qry = SEARCH_QUERY;
            string cols = SEARCH_COLS;

            // Log Query
            GlobalVars.Log("frmMain-bgw_SearchMovie", $"Start Background worker from: {Name}");
            DataTable dt = DBCON.DbQuery(qry, cols);

            e.Result = dt;
        }
        private void bgw_DoneSearchMovie(object sender, RunWorkerCompletedEventArgs e)
        {
            // Retrieve the result pass from bg_DoWork() if any.
            // Note, you may need to cast it to the desired data type.
            GlobalVars.Log("frmMain-bgw_DoneSearchMovie", "RETRIEVES data from Previous Search query in bgWorker");
            DataTable dt = null;
            if (e.Result is DataTable)
            {
                // Set the e.Result from BgWorker as DT
                dt = e.Result as DataTable;
                // Clear previous list
                lvSearchResult.Items.Clear();
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

                    ListViewItem temp = new ListViewItem() { Text = r1.ToString() };
                    // Is it a Movie? (by checking if there are no season)
                    if (String.IsNullOrWhiteSpace(r4.ToString()))
                    {
                        // If there is no Series name
                        if (String.IsNullOrWhiteSpace(r3.ToString()))
                        {
                            // Use Episode name
                            temp.SubItems.Add(r2.ToString());
                        }
                        else
                        {
                            // Otherwise use Series
                            temp.SubItems.Add(r3.ToString());
                        }
                    }
                    else
                    {
                        // Set Series Name
                        if (String.IsNullOrWhiteSpace(r3.ToString()))
                        {
                            temp.Text = r3.ToString(); //Set Series Name
                        }
                        temp.SubItems.Add("S" + GlobalVars.ValidateNum(r4.ToString()) + " E" + GlobalVars.ValidateNum(r5.ToString()));
                    }
                    temp.SubItems.Add(r6.ToString());
                    string imgKey = Convert.ToString(MOVIEID) + ".jpg";
                    if (GlobalVars.MOVIE_IMGLIST.Images.ContainsKey(imgKey))
                    {
                        temp.ImageIndex = GlobalVars.MOVIE_IMGLIST.Images.IndexOfKey(imgKey); //CC;
                    }
                    else
                    {
                        temp.ImageIndex = 0;
                    }
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

            // Close the loading form.
            if (formLoading != null)
            {
                formLoading.Close();
            }

            // If there are no results, show message
            if (lvSearchResult.Items.Count < 1)
            {
                ListViewItem temp = new ListViewItem() { Text = "No Search Results!" };
                temp.Tag = "0";
                temp.ImageIndex = 0;
                lvSearchResult.Items.Add(temp);
                //GlobalVars.ShowInfo("There are no results for search query '" + text + "'!");
            }

            // Set Focus
            txtSearch.Focus();

            // Run GC to clean
            GlobalVars.CleanMemory();
        }
        // Search all Movie files in folder
        private void bgw_SearchFileinFolder(object sender, DoWorkEventArgs e)
        {
            formLoading.Focus(); // Set focus to Loading

            // Get Movie files on Folder, even subFolder
            string folder = FOLDERTOSEARCH;
            if (String.IsNullOrWhiteSpace(folder))
            {
                e.Result = null;
                return;
            }
            // Create variables
            int count = 0;
            int countVoid = 0;
            List<string> fileList = GlobalVars.DirSearch(folder, $"frmMain ({Name})-DirSearch (Exception)");

            // Find all files that match criteria
            if (fileList.Count > 0)
            {
                // List already in db
                List<string> listAlreadyinDB = DBCON.DbQrySingle(GlobalVars.DB_TNAME_FILEPATH, "[file]", $"frmMain ({Name})-bgw_DoneSearchFileinFolder");

                // List of files valid to add
                List<string> listToAdd = new List<string>();

                string res = "";
                string nonres = "";
                string prevF = "";

                // If file is a movie,
                foreach (string file in fileList)
                {
                    // Check if file have an extension of MOVIE_EXTENSIONS
                    foreach (string ext in MOVIE_EXTENSIONS)
                    {
                        if (Path.GetExtension(file).ToLower() == ext)
                        {
                            // Check if file is already in the database
                            bool canAdd = true;
                            foreach (string pathEx in listAlreadyinDB)
                            {
                                if (pathEx == file)
                                {
                                    canAdd = false;
                                    break;
                                }
                            }
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
                fileList.Clear();
                listAlreadyinDB.Clear();

                GlobalVars.WriteToFile(GlobalVars.PATH_START + "MovieResult.Log", res);
                GlobalVars.WriteToFile(GlobalVars.PATH_START + "MovieResult_Void.Log", nonres);

                // Add now to database
                if (InsertToDB(listToAdd, "bgw_DoneSearchFileinFolder"))
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
                e.Result = null;
            }

        }
        private void bgw_DoneSearchFileinFolder(object sender, RunWorkerCompletedEventArgs e)
        {
            // Get result from BGworker
            if (e.Result != null)
            {
                // Counter for files
                int count = 0;
                int countVoid = 0;

                List<int> res = e.Result as List<int>;
                count = res[0];
                countVoid = res[1];
                // Show message
                GlobalVars.ShowInfo($"\tTotal files added: {count.ToString()}\n\tFiles skipped: {countVoid.ToString()}");
            }

            // Run GC to clean
            GlobalVars.CleanMemory();

            // Perform click on button: btnSearch
            btnSearch_Click(this, EventArgs.Empty);
        }
        // ############################################################################## Database Functions
        bool InsertToDB(List<string> listofFiles, string errFrom)
        {
            string callFrom = $"frmMain ({Name})-InsertToDB-({errFrom})";

            // Insert to DB if NEW MOVIE
            SQLHelper conn = new SQLHelper();

            // Build new string[] from INFO, FIlepath
            string[] combined = new string[GlobalVars.DB_TABLE_INFO.Length + GlobalVars.DB_TABLE_FILEPATH.Length - 2];
            Array.Copy(GlobalVars.DB_TABLE_INFO, 1, combined, 0, GlobalVars.DB_TABLE_INFO.Length - 1);
            Array.Copy(GlobalVars.DB_TABLE_FILEPATH, 1, combined, GlobalVars.DB_TABLE_INFO.Length - 1, 3);

            // Create DT
            DataTable dt = conn.InitializeDT(false, combined);
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

            if (conn.DbInsertMovie(dt, callFrom) > 0)
            {
                return true;
            }
            return false;
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
            GlobalVars.Log("frmMain.Designer-Dispose", "Disposing MOVIE_IMGLIST");
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
            GlobalVars.Log("frmMain.Designer-GlobalVars.CleanMemory()", "Runs GC");
            GlobalVars.CleanMemory();

            GlobalVars.Log("frmMain.Designer-Dispose", "Closed the program");
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
            // Clear previous values
            SEARCH_QUERY = "";
            SEARCH_COLS = "";
            
            string cols = "[Id],[name],[name_ep],[name_series],[season],[episode],[year]";
            SEARCH_COLS = cols;

            // SELECT query
            string qry = $"SELECT {cols} FROM {GlobalVars.DB_TNAME_INFO}";

            // Build Filter
            // Name Text search
            if (txtSearch.Text != GlobalVars.SEARCHBOX_PLACEHOLDER)
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
                qry = $"SELECT {cols} FROM {GlobalVars.DB_TNAME_INFO} WHERE ";
                qry += $"[imdb] = '{txtIMDB.Text}'";
            }
            // Append to end
            qry += ";";
            SEARCH_QUERY = qry;

            // Create BG Worker
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += new DoWorkEventHandler(bgw_SearchMovie);
            bg.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_DoneSearchMovie);

            // Start the worker.
            bg.RunWorkerAsync();

            // Display the loading form.
            if (formLoading == null)
            {
                formLoading.ShowDialog(this);
            }
        }
        private void lvSearchResult_MouseDoubleClick(object sender, MouseEventArgs e)
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
        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear searchbox and Filter
            txtSearch.Text = GlobalVars.SEARCHBOX_PLACEHOLDER;
            txtIMDB.Text = "";
            txtStudio.Text = "";
            txtYearFrom.Text = "";
            txtYearTo.Text = DateTime.Now.Year.ToString();
            cbCategory.SelectedIndex = 0;
            cbCountry.SelectedIndex = 0;
            cbGenre.SelectedIndex = 0;

            // Reset search
            btnSearch.PerformClick();
        }

        private void btnAddMovie_Click(object sender, EventArgs e)
        {
            string formName = GlobalVars.PREFIX_MOVIEINFO + "0";
            // Create NEW MOVIE and (frmMovieInfo) SHOW Edit Information form
            GlobalVars.OpenFormMovieInfo(this, formName, "0", "New Movie ", "frmMain-btnAddMovie_Click (ADD MOVIE)");
        }

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

    }
}
