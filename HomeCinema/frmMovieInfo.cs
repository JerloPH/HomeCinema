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
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using HomeCinema.SQLFunc;
using HomeCinema.Global;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace HomeCinema
{
    public partial class frmMovieInfo : Form
    {
        // Editable vars
        private string PARENT_NAME { get; set; } = "";
        private string MOVIE_ID { get; set; } = "";
        private Image MOVIE_COVER { get; set; } = null;
        private Image tempImage { get; set; } = null;

        // Source ListView lvSearch Item index
        public ListViewItem LVITEM = null;

        // Initiate SQLHelper DB Connection
        SQLHelper conn = new SQLHelper("frmMovieInfo");

        // Fixed vars
        private Form PARENT = null;

        #region Initialize class
        public frmMovieInfo(Form parent,string ID, string text, string formName, ListViewItem lvitem)
        {
            InitializeComponent();
            // Form properties
            Name = formName;
            Icon = parent.Icon; //GlobalVars.HOMECINEMA_ICON;
            FormClosing += new FormClosingEventHandler(frmMovieInfo_FormClosing);

            // Set vars
            MOVIE_ID = ID.TrimStart('0');
            PARENT = parent;
            PARENT_NAME = PARENT.Name;
            LVITEM = lvitem;

            // Set Controls text and properties
            txtID.Text = MOVIE_ID;

            // Set picBox size mode
            picBox.SizeMode = PictureBoxSizeMode.StretchImage;
            picBox.Tag = null;

            // Add items to cbCategory
            cbCategory.Items.AddRange(GlobalVars.DB_INFO_CATEGORY);

            // LOAD Information from DATABASE and SET to Controls
            if (Convert.ToInt16(MOVIE_ID) > 0)
            {
                LoadInformation(ID, text);
                RefreshCountryAndGenre();
            }

            // Show the form
            StartPosition = FormStartPosition.CenterParent;
            ShowDialog(parent);

            // Set main focus at startup
            txtName.Focus();
        }
        #endregion
        // ############################################################################## Functions
        #region Functions
        // ########################## LISTBOX Functions
        private bool CanAddToListBox(ListBox lb, string item)
        {
            if (String.IsNullOrWhiteSpace(item)) { return false; }
            if (lb.Items.Contains(item)) { return false; }
            foreach (string sItem in lb.Items)
            {
                if (sItem.Equals(item, StringComparison.CurrentCultureIgnoreCase))
                {
                    return false;
                }
            }
            return true;
        }
        private void AddToListBox(ListBox lb, List<string> list, string caption = "Type here")
        {
            string item = GlobalVars.GetStringInputBox(list, caption);
            if (CanAddToListBox(lb, item))
            {
                lb.Items.Add(item);
            }
        }
        private void RemoveFromListBox(ListBox lb)
        {
            for (int i = lb.SelectedIndices.Count - 1; i >= 0; i--)
            {
                int item = lb.SelectedIndices[i];
                lb.Items.RemoveAt(item);
            }
        }
        // Add items to ListBox
        public void LoadListBoxItems(string itemsList, ListBox lb)
        {
            string[] temp = itemsList.Split(',');
            string itemAdd = "";
            foreach (string item in temp)
            {
                itemAdd = item.Trim();
                if (CanAddToListBox(lb, itemAdd))
                {
                    lb.Items.Add(itemAdd);
                }
            }
        }
        // ########################## FOR CATEGORY
        // Get Category as integer, in string format
        private string GetCategory()
        {
            return (cbCategory.SelectedIndex < 1) ? "0" : cbCategory.SelectedIndex.ToString();
        }
        // ########################## FOR GENRE
        // Return genre [string], from checked checkboxes
        public string GetGenre()
        {
            return GlobalVars.ConvertListBoxToString(listboxGenre);
        }
        // Add genres to ListBox that are in the media 'genre'
        public void LoadGenre(string genre)
        {
            LoadListBoxItems(genre, listboxGenre);
        }
        // ########################## FOR COUNTRY
        // Return country, from checkboxes
        public string GetCountry()
        {
            return GlobalVars.ConvertListBoxToString(listboxCountry);
        }
        // Add countries to ListBox that are in the media 'country'
        public void LoadCountry(string country)
        {
            LoadListBoxItems(country, listboxCountry);
        }
        // ########################## OTHER FUNCTIONS
        // REFRESH INFORMATION
        public void LoadInformation(string ID, string text)
        {
            // Set Form Properties
            Text = $"[EDIT] {text}";//text + " [Edit Information]";

            // Change cover image, if image exists in ImageList
            MOVIE_COVER = GlobalVars.ImgGetImageFromList(MOVIE_ID);
            if (MOVIE_COVER == null)
            {
                GlobalVars.Log($"frmMovieInfo-({Name})-LoadInformation", "MOVIE_COVER = null");
                GlobalVars.ShowWarning("Poster Image not set!");
                //Close();
            }
            else
            {
                picBox.Image = MOVIE_COVER;
            }

            // Set textbox values from Database
            string cols = null;
            // Get filepath FROM DB
            foreach (string s in GlobalVars.DB_TABLE_FILEPATH)
            {
                cols += "[" + s + "],";
            }
            cols = cols.TrimEnd(',');
            // Build query string
            string qry = $"SELECT {cols} FROM {GlobalVars.DB_TNAME_FILEPATH} WHERE Id={ID} LIMIT 1;";
            //GlobalVars.Log($"frmMovieInfo-frmMovieInfo Table({GlobalVars.DB_TNAME_FILEPATH})", $"Query src: {qry}");

            DataTable dtFile = conn.DbQuery(qry, cols, "frmMovie-LoadInformation"); // run the query
            foreach (DataRow row in dtFile.Rows)
            {
                txtPathFile.Text = row[GlobalVars.DB_TABLE_FILEPATH[1]].ToString();
                txtPathSub.Text = row[GlobalVars.DB_TABLE_FILEPATH[2]].ToString();
                txtPathTrailer.Text = row[GlobalVars.DB_TABLE_FILEPATH[3]].ToString();
                break;
            }
            dtFile.Clear();
            dtFile.Dispose();

            // Get info FROM DB
            cols = "";
            foreach (string c in GlobalVars.DB_TABLE_INFO)
            {
                cols += "[" + c + "],";
            }
            cols = cols.TrimEnd(',');
            qry = $"SELECT {cols} FROM {GlobalVars.DB_TNAME_INFO} WHERE [Id]={txtID.Text} LIMIT 1;";

            DataTable dtInfo = conn.DbQuery(qry, cols, "frmMovie-LoadInformation");

            foreach (DataRow row in dtInfo.Rows)
            {
                var r1 = row[1]; // imdb
                var r2 = row[2]; // name
                var r3 = row[3]; // name_ep
                var r4 = row[4]; // name_series
                var r5 = row[5]; // season
                var r6 = row[6]; // episode
                var r7 = row[7]; // country
                var r8 = row[8]; // category
                var r9 = row[9]; // genre
                var r10 = row[10]; // studio
                var r11 = row[11]; // producer
                var r12 = row[12]; // director
                var r13 = row[13]; // artist
                var r14 = row[14]; // year
                var r15 = row[15]; // summary  
                // Set textboxes
                txtIMDB.Text = r1.ToString();
                txtName.Text = r2.ToString();
                txtEpName.Text = r3.ToString();
                txtSeriesName.Text = r4.ToString();
                txtSeasonNum.Text = r5.ToString();
                txtEpNum.Text = r6.ToString();
                LoadCountry(r7.ToString());
                cbCategory.SelectedIndex = Convert.ToInt32(r8);
                LoadGenre(r9.ToString());
                txtStudio.Text = r10.ToString();
                txtProducer.Text = r11.ToString();
                txtDirector.Text = r12.ToString();
                txtArtist.Text = r13.ToString();
                txtYear.Text = r14.ToString();
                txtSummary.Text = r15.ToString();
            }

            // Clean dataTable
            dtInfo.Clear();
            dtInfo.Dispose();

            // Disable setting metadata if series
            if (cbCategory.Text.ToLower().Contains("series"))
            {
                cbSaveMetadata.Enabled = false;
            }

            // Set control focus
            txtName.Focus();
        }
        // Set Image to picBox Image
        public bool SetPicboxImgFromFile(PictureBox picbox, string selectedFilename, string actFrom)
        {
            string errFrom = $"frmMovieInfo (SetPicboxImg)-{actFrom}";
            if (File.Exists(selectedFilename))
            {
                try
                {
                    // Dispose previous tempImage
                    if (tempImage != null)
                    {
                        tempImage.Dispose();
                    }
                    tempImage = Image.FromFile(selectedFilename);
                    // Dispose previous image
                    if (picbox.Image != null)
                    {
                        picbox.Image.Dispose();
                    }
                    picbox.Image = tempImage;
                    picbox.Tag = selectedFilename;
                    picbox.Refresh();
                    GlobalVars.ShowInfo("Changed the image cover!");
                    return true;
                }
                catch (Exception exc)
                {
                    GlobalVars.ShowError(errFrom, exc);
                    return false;
                }
            }
            return false;
        }
        // Saved loaded image from picBox Tag property as Poster image
        private void SaveCoverChanged(string selectedFilename)
        {
            string ExceptionFrom = "frmMovieInfo-SaveCoverChanged (" + Name + ")";
            //GlobalVars.Log(ExceptionFrom, "Try adding Image from File");

            // Remove previous image from the ImgList, exit if not succesful and show warning
            if (File.Exists(GlobalVars.ImgFullPath(MOVIE_ID)))
            {
                if (GlobalVars.DeleteImageFromList(this, MOVIE_ID, ExceptionFrom + " (Previous Image)") == false)
                {
                    GlobalVars.ShowWarning("Cannot replace image!\nFile must be NOT in use!");
                    return;
                }
            }
            // Remove previous Image from memory
            DisposeImages();

            // Copy new file to App Path
            string sourceFile = selectedFilename;
            string destFile = GlobalVars.ImgFullPath(MOVIE_ID);

            // Delete previous image file from disk
            GlobalVars.TryDelete(destFile, ExceptionFrom + $" [Delete Error, FILE: {destFile}]");

            // Copy selected File to Application Path 'covers'
            try
            {
                File.Copy(sourceFile, destFile, true);

                // Set picBox Image, from File Selected
                Image imgFromFile = Image.FromFile(destFile);
                GlobalVars.MOVIE_IMGLIST.Images.Add(Path.GetFileName(destFile), imgFromFile);
                imgFromFile.Dispose();
                GlobalVars.Log(ExceptionFrom + " [NEW Image File and Name]", sourceFile);
            }
            catch (Exception fex)
            {
                GlobalVars.ShowError(ExceptionFrom, fex);
            }
        }
        // Dispose all Poster images
        private void DisposeImages()
        {
            if (MOVIE_COVER != null)
            {
                MOVIE_COVER.Dispose();
                MOVIE_COVER = null;
            }
            if (tempImage != null)
            {
                tempImage.Dispose();
                tempImage = null;
            }
            try
            {
                picBox.Image.Dispose();
                picBox.Image = null;
            }
            catch { }
            // If Parent is of type: frmMovie
            if (PARENT is frmMovie)
            {
                // Dispose from Parent
                string[] Params = { "" };
                GlobalVars.CallMethod(PARENT_NAME, "DisposePoster", Params, $"frmMovieInfo-DisposeImages ({Name})", "frmMovie PARENT: " + PARENT_NAME);
            }
        }
        private void RefreshCountryAndGenre()
        {
            // Add new country to text file
            foreach (string item in listboxCountry.Items)
            {
                GlobalVars.WriteAppend(GlobalVars.FILE_COUNTRY, $",{item}");
            }
            // Add new genre to text file
            foreach (string item in listboxGenre.Items)
            {
                GlobalVars.WriteAppend(GlobalVars.FILE_GENRE, $",{item}");
            }
            Program.FormMain.PopulateCountryCB(); // Refresh Country list
            Program.FormMain.PopulateGenreCB(); // Refresh Genre list
        }
        #endregion
        // ############################################################################## Form Controls methods event
        private void frmMovieInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (picBox.Image != null)
            {
                picBox.Image.Dispose();
            }
            if (tempImage != null)
            {
                tempImage.Dispose();
            }
            foreach (Control c in Controls)
            {
                c.Dispose();
            }
            Dispose();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // variables
            string callFrom = $"frmMovieInfo ({Name})-btnSave_Click"; // called From
            var metaData = new List<string>(); // List for metadata values

            // Exit if Movie Name is empty
            if (String.IsNullOrWhiteSpace(txtName.Text))
            {
                GlobalVars.ShowWarning("Name cannot be Empty!");
                txtName.Focus();
                return;
            }

            // Exit if Year is empty
            if (String.IsNullOrWhiteSpace(txtYear.Text))
            {
                GlobalVars.ShowWarning("Year cannot be Empty!");
                txtYear.Focus();
                return;
            }

            // Check MOVIE ID value if Lower than 1 (Not existing)
            if (Convert.ToInt32(MOVIE_ID) > 0)
            {
                // SAVE changes to EXISTING MOVIE
                // DataTable for INFO
                DataTable dt = conn.InitializeDT(GlobalVars.DB_TABLE_INFO);
                // Add row values
                DataRow row = dt.NewRow();
                row[0] = GlobalVars.ValidateEmptyOrNull(txtID.Text);
                row[1] = GlobalVars.ValidateEmptyOrNull(txtIMDB.Text);
                row[2] = GlobalVars.ValidateEmptyOrNull(txtName.Text);
                row[3] = GlobalVars.ValidateEmptyOrNull(txtEpName.Text);
                row[4] = GlobalVars.ValidateEmptyOrNull(txtSeriesName.Text);
                row[5] = GlobalVars.ValidateEmptyOrNull(txtSeasonNum.Text);
                row[6] = GlobalVars.ValidateEmptyOrNull(txtEpNum.Text);
                row[7] = GetCountry(); //GlobalVars.ValidateEmptyOrNull(lblCountry.Text);
                row[8] = GetCategory();
                row[9] = GetGenre();
                row[10] = GlobalVars.ValidateEmptyOrNull(txtStudio.Text);
                row[11] = GlobalVars.ValidateEmptyOrNull(txtProducer.Text);
                row[12] = GlobalVars.ValidateEmptyOrNull(txtDirector.Text);
                row[13] = GlobalVars.ValidateEmptyOrNull(txtArtist.Text);
                row[14] = GlobalVars.ValidateEmptyOrNull(txtYear.Text);
                row[15] = GlobalVars.ValidateEmptyOrNull(txtSummary.Text);

                // Save MetaData details
                metaData.Add(row[2].ToString()); // title
                metaData.Add(row[14].ToString()); // year
                metaData.Add(row[9].ToString()); // genre
                metaData.Add(row[12].ToString()); // director
                metaData.Add(row[11].ToString()); // producer

                dt.Rows.Add(row);
                dt.AcceptChanges();
                // Check if first query successfully executed
                bool ContAfterQry = conn.DbUpdateInfo(dt, callFrom);

                // dispose INFO table
                dt.Clear();
                dt.Dispose();

                // IF INFO is executed, continue to FILEPATH
                if (ContAfterQry)
                {
                    // DT for Filepath
                    DataTable dtFile = conn.InitializeDT(true, GlobalVars.DB_TABLE_FILEPATH);
                    // Add row values
                    DataRow rowFile = dtFile.NewRow();
                    rowFile[0] = GlobalVars.ValidateEmptyOrNull(txtID.Text); // ID
                    rowFile[1] = GlobalVars.ValidateEmptyOrNull(txtPathFile.Text); // file
                    rowFile[2] = GlobalVars.ValidateEmptyOrNull(txtPathSub.Text); // sub
                    rowFile[3] = GlobalVars.ValidateEmptyOrNull(txtPathTrailer.Text); // trailers

                    dtFile.Rows.Add(rowFile);
                    dtFile.AcceptChanges();
                    conn.DbUpdateFilepath(dtFile, callFrom);
                    // dispose FILEPATH table
                    dtFile.Clear();
                    dtFile.Dispose();
                }
            }

            // Change Cover, or Add it
            if (picBox.Tag != null)
            {
                SaveCoverChanged(picBox.Tag.ToString());
            }

            // Check if parent form is frmMovie
            if (PARENT is frmMovie)
            {
                // Refresh parent form fmMovie
                string[] Params = { MOVIE_ID };
                GlobalVars.CallMethod(PARENT_NAME, "LoadInformation", Params, $"frmMovieInfo-btnSave_Click ({Name})", "frmMovie PARENT: " + PARENT_NAME);
            }
            RefreshCountryAndGenre(); // Add new country/genre to text files

            // Refresh main form properties
            Program.FormMain.UpdateMovieItemOnLV(LVITEM); // ListView item of this

            // Save Metadata
            if (cbSaveMetadata.Checked)
            {
                GlobalVars.SaveMetadata(txtPathFile.Text, metaData);
            }

            // Close this form
            Close();
        }
        // OpenDialog and Select Cover of Movie and Set it to picBox.Image
        private void btnChangeCover_Click(object sender, EventArgs e)
        {
            // Get FileName on openDialog
            string selectedFilename = GlobalVars.GetAFile("Select Image file for Cover", "JPG Files (*.jpg)|*.jpg", GlobalVars.PATH_GETCOVER);
            if (selectedFilename != "")
            {
                // Load image from file
                SetPicboxImgFromFile(picBox, selectedFilename, "btnChangeCover_Click");
                GlobalVars.PATH_GETCOVER = Path.GetDirectoryName(selectedFilename);
            }
        }
        // Change Movie File path
        private void btnChangeFile_Click(object sender, EventArgs e)
        {
            // Get file
            string selectedFile = GlobalVars.GetAFile("Select video file....", GlobalVars.FILTER_VIDEO, GlobalVars.PATH_GETVIDEO);

            // Check fileName from dialog
            if (String.IsNullOrWhiteSpace(selectedFile) == false)
            {
                if (File.Exists(selectedFile))
                {
                    txtPathFile.Text = selectedFile;
                    string subFile = Path.ChangeExtension(selectedFile, null);
                    subFile += ".srt";
                    if (File.Exists(subFile))
                    {
                        txtPathSub.Text = subFile;
                    }
                    GlobalVars.ShowInfo("File is located!");
                }
                else
                {
                    GlobalVars.ShowWarning("File does not exists!");
                }
            }
            else
            {
                GlobalVars.ShowWarning("No File selected!");
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        // Fetch data from TMDB, using IMDB ID
        private void btnFetchData_Click(object sender, EventArgs e)
        {
            // Exit when no TMDB key
            if (!GlobalVars.HAS_TMDB_KEY)
            {
                GlobalVars.ShowWarning(GlobalVars.MSG_NO_TMDB);
                return;
            }
            // Declare vars
            frmLoading form = null;
            var list = new List<string>();
            string errFrom = "frmMovieInfo-btnFetchData_Click";
            string IMDB_ID = txtIMDB.Text;
            string mediatype;
            string genre; // genre text 
            string jsonMainFullPath; // json file full path
            string r1, r2, r3, r4, r5, r6, r7, r8, r9, r10; // List Info from TMDB
            string linkPoster, YT_ID;
            bool coverDownloaded = false;

            // Exit if IMDB id is invalid
            if (String.IsNullOrWhiteSpace(IMDB_ID) || IMDB_ID=="0" || (IMDB_ID.StartsWith("tt")==false))
            {
                GlobalVars.ShowWarning("Invalid IMDB Id!");
                txtIMDB.Focus();
                return;
            }
            // Check if series
            mediatype = (cbCategory.Text.ToLower().Contains("series")) ? "tv" : "movie";

            // Get List of values from TMDB
            form = new frmLoading("Fetching info from TMDB..", "Loading");
            form.BackgroundWorker.DoWork += (sender1, e1) =>
            {
                list = GlobalVars.GetMovieInfoByImdb(IMDB_ID, mediatype);
            };
            form.ShowDialog(this);

            // Set the values to textboxes
            YT_ID = list[1];
            if ((String.IsNullOrWhiteSpace(txtPathTrailer.Text)) && (string.IsNullOrWhiteSpace(YT_ID) ==false))
            {
                txtPathTrailer.Text = GlobalVars.LINK_YT + YT_ID;
            }

            // Get properties and information of movies
            jsonMainFullPath = list[0]; // json file full path
            r1 = list[2]; // title
            r2 = list[3]; // orig title
            r3 = list[4]; // overview / summary
            r4 = list[5]; // release date
            r5 = list[6]; // poster_path
            r6 = list[7]; // Artist
            r7 = list[8]; // Director
            r8 = list[9]; // Producer
            r9 = list[10]; // country
            r10 = list[11]; // Studio

            // Set to textboxes
            if (String.IsNullOrWhiteSpace(r1)==false)
            {
                txtName.Text = r1;
            }
            if (String.IsNullOrWhiteSpace(r2) == false)
            {
                if (!r2.Equals(txtName.Text))
                {
                    txtEpName.Text = r2;
                }
            }
            if (String.IsNullOrWhiteSpace(r3) == false)
            {
                txtSummary.Text = r3;
            }
            if (String.IsNullOrWhiteSpace(r4) == false)
            {
                txtYear.Text = r4.Substring(0, 4);
            }

            txtArtist.Text = r6;
            txtDirector.Text = r7;
            txtProducer.Text = r8;
            txtStudio.Text = r10;
            // Clear country and genre listbox
            listboxCountry.Items.Clear();
            listboxGenre.Items.Clear();
            // Set Country
            LoadCountry(r9);
            // Set Genres
            genre = GlobalVars.GetGenresByJsonFile(jsonMainFullPath, errFrom + " (jsonMainFullPath)", ",");
            LoadGenre(genre);
            // Set mediatype, after getting info from TMDB
            cbCategory.SelectedIndex = GlobalVars.GetCategoryByFilter(genre, r9, mediatype);

            // Ask to change cover - poster image
            linkPoster = r5;
            if (String.IsNullOrWhiteSpace(r5) == false)
            {
                if (GlobalVars.ShowYesNo("Do you want to change poster image?"))
                {
                    // Show form loading
                    form = new frmLoading("Downloading cover from TMDB..", "Loading");
                    form.BackgroundWorker.DoWork += (sender1, e1) =>
                    {
                        // Parse image link from JSON and download it
                        coverDownloaded = GlobalVars.DownloadCoverFromTMDB(MOVIE_ID, linkPoster, errFrom);
                    };
                    form.ShowDialog(this);
                    // Update Image in PictureBox
                    if (coverDownloaded)
                    {
                        string moviePosterDL = GlobalVars.PATH_TEMP + MOVIE_ID + ".jpg";
                        if (SetPicboxImgFromFile(picBox, moviePosterDL, errFrom) == false)
                        {
                            // Show a Warning
                            GlobalVars.ShowWarning("Image file cannot be downloaded at the moment!\n Try again later...");
                        }
                    }
                }
            }
        }
        // Get IMDB ID using Movie Name
        private void btnGetImdb_Click(object sender, EventArgs e)
        {
            // Exit when no TMDB key
            if (!GlobalVars.HAS_TMDB_KEY)
            {
                GlobalVars.ShowWarning(GlobalVars.MSG_NO_TMDB);
                return;
            }

            // Declare vars
            string mediatype = (cbCategory.Text.ToLower().Contains("series") ? "tv" : "movie");
            string getIMDB = "";
  
            // Check if txtName is valid
            if (String.IsNullOrWhiteSpace(txtName.Text))
            {
                GlobalVars.ShowWarning("Invalid Name!");
                txtName.Focus();
                return;
            }

            // Show form for tmdb searching
            var form = new frmTmdbSearch($"Search for {mediatype}", txtName.Text, mediatype, txtID.Text);
            form.ShowDialog(this);
            getIMDB = form.getResult;
            form.Dispose();

            // Get IMDB from TMDB json info
            if (String.IsNullOrWhiteSpace(getIMDB) == false)
            {
                txtIMDB.Text = getIMDB;
                btnFetchData.PerformClick(); // Automatically search IMDB Id
            }
        }

        private void btnCountryClear_Click(object sender, EventArgs e)
        {
            listboxCountry.Items.Clear();
        }

        private void btnGenreClear_Click(object sender, EventArgs e)
        {
            listboxGenre.Items.Clear();
        }

        private void btnCountryRemove_Click(object sender, EventArgs e)
        {
            RemoveFromListBox(listboxCountry);
        }

        private void btnGenreRemove_Click(object sender, EventArgs e)
        {
            RemoveFromListBox(listboxGenre);
        }

        private void btnCountryAdd_Click(object sender, EventArgs e)
        {
            AddToListBox(listboxCountry, GlobalVars.TEXT_COUNTRY.ToList<string>(), "Type country to add");
        }

        private void btnGenreAdd_Click(object sender, EventArgs e)
        {
            AddToListBox(listboxGenre, GlobalVars.TEXT_GENRE.ToList<string>(), "Type genre to add");
        }
    }
}
