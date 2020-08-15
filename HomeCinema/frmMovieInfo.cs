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
        public static string PARENT_NAME { get; set; } = "";
        public static string MOVIE_ID { get; set; } = "";
        public static Image MOVIE_COVER { get; set; } = null;
        public static Image tempImage { get; set; } = null;

        // Source ListView lvSearch Item index
        public ListViewItem LVITEM = null;

        // Initiate SQLHelper DB Connection
        SQLHelper conn = new SQLHelper("frmMovieInfo");

        // Fixed vars
        Form PARENT = null;

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

            // Setup fPanelCountry
            //fPanelCountry.AutoScrollMinSize = new Size(64,64);
            fPanelCountry.AutoScroll = true;
            //fPanelCountry.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            // Populate Country
            int cW = 105; // (fPanelCountry.Width / 2) - 5;
            int cH = 17;
            string[] tCountryArr = GlobalVars.BuildArrFromFile(GlobalVars.FILE_COUNTRY, $"frmMovieInfo-frmMovieInfo ({Name})-StreamReader[FILE_COUNTRY]");
            foreach (string t in tCountryArr)
            {
                if (t != "All")
                {
                    CheckBox cb = new CheckBox();
                    cb.Size = new Size(cW, cH);
                    cb.Text = GlobalVars.RemoveLine(t);
                    cb.AutoSize = false;
                    cb.TabIndex = 0;
                    cb.UseVisualStyleBackColor = true;
                    fPanelCountry.Controls.Add(cb);
                }
            }

            // Populate fPanelGenre with Checkbox
            fPanelGenre.AutoScroll = true;
            cW = 102;
            cH = 17;

            for (int i=0; i<GlobalVars.TEXT_GENRE.Length; i++)
            {
                string t = GlobalVars.TEXT_GENRE[i];
                CheckBox cb = new CheckBox();
                cb.Size = new Size(cW, cH);
                cb.Text = GlobalVars.RemoveLine(t);
                cb.AutoSize = false;
                cb.TabIndex = 0;
                cb.UseVisualStyleBackColor = true;
                fPanelGenre.Controls.Add(cb);
            }

            // LOAD Information from DATABASE and SET to Controls
            if (Convert.ToInt16(MOVIE_ID) > 0)
            {
                LoadInformation(ID, text);
            } // OR, otherwise, Create a NEW MOVIE INFO
            else
            {
                Text = text + "[Add New Movie]";
                MOVIE_COVER = GlobalVars.ImgGetImageFromList("0");
                picBox.Image = MOVIE_COVER;
                cbCategory.SelectedIndex = 1;
            }

            // Show the form
            StartPosition = FormStartPosition.CenterParent;
            ShowDialog(parent);

            // Set main focus at startup
            txtName.Focus();
        }
        // ############################################################################## Functions
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

            // Set control focus
            txtName.Focus();
        }
        // Get Checkboxes text from checked controls
        public string GetChecked(FlowLayoutPanel f)
        {
            string ret = "";
            foreach (CheckBox c in f.Controls)
            {
                if (c.Checked)
                {
                    ret += GlobalVars.RemoveLine(c.Text) + ",";
                }
            }
            ret = ret.TrimEnd(',');
            return ret;
        }
        public void ActivateCheckbox(FlowLayoutPanel f, string textToSplit)
        {
            string[] gSplit = textToSplit.Split(',');
            foreach (CheckBox c in f.Controls)
            {
                foreach (string text in gSplit)
                {
                    if (c.Text.Contains(text) && (String.IsNullOrWhiteSpace(text) == false))
                    {
                        c.Checked = true;
                        break;
                    }
                }
            }
        }
        // ########################## FOR CATEGORY
        // Get Category INT
        private string GetCategory()
        {
            if (cbCategory.SelectedIndex < 1)
            {
                return "0";
            }
            return cbCategory.SelectedIndex.ToString();
        }
        // ########################## FOR GENRE
        // Return genre [string], from checked checkboxes
        public string GetGenre()
        {
            FlowLayoutPanel f = GetFlowPanel("fPanelGenre", "tabPage2");
            return GetChecked(f);
        }
        // Check [x] the Genre for the movie
        public void LoadGenre(string gen)
        {
            FlowLayoutPanel f = GetFlowPanel("fPanelGenre", "tabPage2");
            ActivateCheckbox(f, gen);
        }
        // ########################## FOR COUNTRY
        // Return country, from checkboxes
        public string GetCountry()
        {
            FlowLayoutPanel f = GetFlowPanel("fPanelCountry", "tabPage2");
            return GetChecked(f);
        }
        // Check [x] the Country for the movie
        public void LoadCountry(string country)
        {
            FlowLayoutPanel f = GetFlowPanel("fPanelCountry", "tabPage2");
            ActivateCheckbox(f, country);
        }
        // ########################## Other Functions
        // GET FlowLayoutPanel inside [tabInfo]
        public FlowLayoutPanel GetFlowPanel(string panelName, string TABpage)
        {
            string tabPage = TABpage;
            if (string.IsNullOrWhiteSpace(tabPage))
            {
                tabPage = "tabPage1";
            }
            foreach (TabPage p in tabInfo.TabPages)
            {
                if (p.Name == tabPage)
                {
                    foreach (Control f in p.Controls)
                    {
                        if (f is FlowLayoutPanel)
                        {
                            FlowLayoutPanel ff = f as FlowLayoutPanel;
                            if (ff.Name == panelName)
                            {
                                return ff;
                            }
                        }
                    }
                    break;
                }
            }
            return null;
        }
        // Load an Image from file and set to picBox Image
        private void LoadImageFromFile(string selectedFilename, string actFrom)
        {
            // Try load image from File
            string errFrom = $"frmMovieInfo ({Name})-{actFrom}-(Image.FromFile Error)";
            if (File.Exists(selectedFilename))
            {
                try
                {
                    // Change picBox image
                    if (tempImage != null)
                    {
                        tempImage.Dispose();
                    }
                    tempImage = Image.FromFile(selectedFilename);
                    picBox.Image = tempImage;
                    picBox.Tag = selectedFilename;
                    picBox.Refresh();
                    GlobalVars.ShowInfo("Changed the image cover!");

                } catch (Exception exc)
                {
                    GlobalVars.ShowError(errFrom, exc);
                }
            }
        }
        // Saved loaded image from picBox Tag property as Poster image
        private void SaveCoverChanged(string selectedFilename)
        {
            string ExceptionFrom = "frmMovieInfo-SaveCoverChanged (" + Name + ")";
            //GlobalVars.Log(ExceptionFrom, "Try adding Image from File");

            // Delete previous image on the ImgList, exit if not succesful and show warning
            if (File.Exists(GlobalVars.ImgFullPath(MOVIE_ID)))
            {
                if (GlobalVars.DeleteImageFromList(MOVIE_ID, ExceptionFrom + " (Previous Image)") == false)
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
                GlobalVars.MOVIE_IMGLIST.Images.Add(Path.GetFileName(destFile), Image.FromFile(sourceFile));
                GlobalVars.Log(ExceptionFrom + " [NEW Image File and Name]", sourceFile);

            } catch (Exception fex)
            {
                GlobalVars.ShowError(ExceptionFrom, fex);
            }
        }
        // Dispose all Poster images
        private void DisposeImages()
        {
            if (picBox.Image != null)
            {
                picBox.Image.Dispose();
            }
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

            // If Parent is of type: frmMovie
            if (PARENT is frmMovie)
            {
                // Dispose from Parent
                string[] Params = { "" };
                GlobalVars.CallMethod(PARENT_NAME, "DisposePoster", Params, $"frmMovieInfo-DisposeImages ({Name})", "frmMovie PARENT: " + PARENT_NAME);
            }
        }
        // Check Genre textboxes from List of genres
        private void CheckGenreFromTMDB(List<string> list)
        {
            if (list.Count > 0)
            {
                foreach (string val in list)
                {
                    foreach (CheckBox cb in fPanelGenre.Controls)
                    {
                        string string1 = cb.Text.ToLower().Trim();
                        string string2 = val.ToLower().Trim();
                        if (string1 == string2)
                        {
                            cb.Checked = true;
                            break;
                        }
                    }
                }
            }
        }
        // ############################################################################## Form Controls methods event
        private void frmMovieInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalVars.LogLine();
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
            // Call is from 
            string callFrom = $"frmMovieInfo ({Name})-btnSave_Click";

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
            if (Convert.ToInt32(MOVIE_ID) < 1)
            {
                // Insert to DB if NEW MOVIE
                // Build new string[] from [INFO] & [filepath]
                string[] combined = new string[GlobalVars.DB_TABLE_INFO.Length + GlobalVars.DB_TABLE_FILEPATH.Length - 2];
                Array.Copy(GlobalVars.DB_TABLE_INFO, 1, combined, 0, GlobalVars.DB_TABLE_INFO.Length - 1);
                Array.Copy(GlobalVars.DB_TABLE_FILEPATH, 1, combined, GlobalVars.DB_TABLE_INFO.Length - 1, 3);

                // Create DT
                DataTable dt = conn.InitializeDT(false, combined);
                DataRow row = dt.NewRow();
                row[0] = GlobalVars.ValidateEmptyOrNull(txtIMDB.Text);
                row[1] = GlobalVars.ValidateEmptyOrNull(txtName.Text);
                row[2] = GlobalVars.ValidateEmptyOrNull(txtEpName.Text);
                row[3] = GlobalVars.ValidateEmptyOrNull(txtSeriesName.Text);
                row[4] = GlobalVars.ValidateEmptyOrNull(txtSeasonNum.Text);
                row[5] = GlobalVars.ValidateEmptyOrNull(txtEpNum.Text);
                row[6] = GetCountry(); //GlobalVars.ValidateEmptyOrNull(lblCountry.Text);
                row[7] = cbCategory.SelectedIndex;
                row[8] = GetGenre();
                row[9] = GlobalVars.ValidateEmptyOrNull(txtStudio.Text);
                row[10] = GlobalVars.ValidateEmptyOrNull(txtProducer.Text);
                row[11] = GlobalVars.ValidateEmptyOrNull(txtDirector.Text);
                row[12] = GlobalVars.ValidateEmptyOrNull(txtArtist.Text);
                row[13] = GlobalVars.ValidateEmptyOrNull(txtYear.Text);
                row[14] = GlobalVars.ValidateEmptyOrNull(txtSummary.Text);
                row[15] = GlobalVars.ValidateEmptyOrNull(txtPathFile.Text);
                row[16] = GlobalVars.ValidateEmptyOrNull(txtPathSub.Text);
                row[17] = GlobalVars.ValidateEmptyOrNull(txtPathTrailer.Text);
                dt.Rows.Add(row);
                dt.AcceptChanges();

                MOVIE_ID = conn.DbInsertMovie(dt , callFrom).ToString();
                txtID.Text = MOVIE_ID;

            }
            else
            {
                // Otherwise, SAVE changes to EXISTING MOVIE
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
            
            // Refresh Movie List
            frmMain master = (frmMain)Application.OpenForms["frmMain"];
            master.UpdateMovieItemOnLV(LVITEM);

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
                LoadImageFromFile(selectedFilename, "btnChangeCover_Click");
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
            string errFrom = "frmMovieInfo-btnFetchData_Click";
            string IMDB_ID = txtIMDB.Text;
            // Exit if IMDB id is invalid
            if (String.IsNullOrWhiteSpace(IMDB_ID) || IMDB_ID=="0" || (IMDB_ID.StartsWith("tt")==false))
            {
                GlobalVars.ShowWarning("Invalid IMDB Id!");
                txtIMDB.Focus();
                return;
            }

            // Get List of values from TMDB
            List<string> list = GlobalVars.GetMovieInfoByImdb(IMDB_ID, errFrom);

            // Set the values to textboxes
            string YT_ID = list[1];
            if ((String.IsNullOrWhiteSpace(txtPathTrailer.Text)) && (string.IsNullOrWhiteSpace(YT_ID) ==false))
            {
                txtPathTrailer.Text = GlobalVars.LINK_YT + YT_ID;
            }

            // Get properties and information of movies
            string jsonMainFullPath = list[0]; // json file full path
            string r1 = list[2]; // title
            string r2 = list[3]; // orig title
            string r3 = list[4]; // overview / summary
            string r4 = list[5]; // release date
            string r5 = list[6]; // poster_path

            // Set to textboxes
            if (String.IsNullOrWhiteSpace(r1)==false)
            {
                txtName.Text = r1;
            }
            if (String.IsNullOrWhiteSpace(r2) == false)
            {
                if (r2 != txtName.Text)
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
            
            string linkPoster = r5;

            // Ask to change cover - poster image
            bool ChangeCover = GlobalVars.ShowYesNo("Do you want to change poster image?");
            if (ChangeCover)
            {
                // Parse image link from JSON and download it
                if (String.IsNullOrWhiteSpace(linkPoster) == false)
                {
                    string moviePosterDL = GlobalVars.PATH_TEMP + MOVIE_ID + ".jpg";
                    while (File.Exists(moviePosterDL) == false)
                    {
                        if (GlobalVars.DownloadFrom("https://image.tmdb.org/t/p/original/" + linkPoster, moviePosterDL) == 404)
                        {
                            break;
                        }
                    }
                    if (File.Exists(moviePosterDL))
                    {
                        LoadImageFromFile(moviePosterDL, errFrom);
                    }
                }
            }

            // Set Genres
            CheckGenreFromTMDB(GlobalVars.GetGenresByJsonFile(jsonMainFullPath, errFrom + " (JSONfindmovie)"));
        }
        // Get IMDB ID using Movie Name
        private void btnGetImdb_Click(object sender, EventArgs e)
        {
            string errFrom = "frmMovieInfo-btnGetImdb_Click";
            // Check if txtName is valid
            if (String.IsNullOrWhiteSpace(txtName.Text))
            {
                GlobalVars.ShowWarning("Invalid Name!");
                txtName.Focus();
                return;
            }

            // Get imdb id and set it to textbox
            string getIMDB = GlobalVars.GetIMDBId(txtName.Text, MOVIE_ID, errFrom);
            if (String.IsNullOrWhiteSpace(getIMDB) == false)
            {
                txtIMDB.Text = getIMDB;
            }
            else
            {
                GlobalVars.ShowWarning($"Cannot search for IMDB Id for '{txtName.Text}'!");
                txtName.Focus();
            }
        }
    }
}
