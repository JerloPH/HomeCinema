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
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Threading;

namespace HomeCinema
{
    public partial class frmMovieInfo : Form
    {
        // Editable vars
        private string PARENT_NAME { get; set; } = "";
        private string MOVIE_ID { get; set; } = "";
        private string MEDIA_TYPE { get; set; } = "";
        private Image MOVIE_COVER { get; set; } = null;
        private Image tempImage { get; set; } = null;

        // Source ListView lvSearch Item index
        public ListViewItem LVITEM = null;

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

            cbSource.Items.AddRange(new string[] { "TMDB", "Anilist" });
            cbSource.SelectedIndex = 0;

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
            string errFrom = $"frmMovieInfo-({Name})-LoadInformation";
            // Set Form Properties
            Text = $"[EDIT] {text}";//text + " [Edit Information]";

            // Change cover image, if image exists in ImageList
            MOVIE_COVER = GlobalVars.ImgGetImageFromList(MOVIE_ID);
            if (MOVIE_COVER != null)
            {
                picBox.Image = MOVIE_COVER;
            }

            // Set textbox values from Database
            // Build query string
            string qry = $"SELECT * FROM {GlobalVars.DB_TNAME_INFO} A LEFT JOIN {GlobalVars.DB_TNAME_FILEPATH} B ON A.`Id`=B.`Id` WHERE A.`Id`={ID.TrimStart('0')} LIMIT 1;";

            using (DataTable dtFile = SQLHelper.DbQuery(qry, "frmMovie-LoadInformation"))
            {
                try
                {
                    DataRow row = dtFile.Rows[0];
                    txtPathFile.Text = row[HCFile.file.ToString()].ToString();
                    txtPathSub.Text = row[HCFile.sub.ToString()].ToString();
                    txtPathTrailer.Text = row[HCFile.trailer.ToString()].ToString();

                    var r1 = row[HCInfo.imdb.ToString()]; // imdb
                    var r2 = row[HCInfo.name.ToString()]; // name
                    var r3 = row[HCInfo.name_orig.ToString()]; // name_ep
                    var r4 = row[HCInfo.name_series.ToString()]; // name_series
                    var r5 = row[HCInfo.season.ToString()]; // season
                    var r6 = row[HCInfo.episode.ToString()]; // episode
                    var r7 = row[HCInfo.country.ToString()]; // country
                    var r8 = row[HCInfo.category.ToString()]; // category
                    var r9 = row[HCInfo.genre.ToString()]; // genre
                    var r10 = row[HCInfo.studio.ToString()]; // studio
                    var r11 = row[HCInfo.producer.ToString()]; // producer
                    var r12 = row[HCInfo.director.ToString()]; // director
                    var r13 = row[HCInfo.artist.ToString()]; // artist
                    var r14 = row[HCInfo.year.ToString()]; // year
                    var r15 = row[HCInfo.summary.ToString()]; // summary  
                    // Set textboxes
                    txtIMDB.Text = r1.ToString();
                    txtName.Text = r2.ToString();
                    txtNameOrig.Text = r3.ToString();
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
                catch (Exception ex)
                {
                    GlobalVars.LogErr(errFrom, ex.Message);
                    GlobalVars.ShowWarning("Entry not found!", "", this);
                }
            }

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
            bool ContAfterQry = false;

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
                txtYear.Text = "0";
            }

            // Check MOVIE ID value if Lower than 1 (Not existing)
            if (Convert.ToInt32(MOVIE_ID) > 0)
            {
                // SAVE changes to EXISTING MOVIE
                // Add row values
                var entry = new Dictionary<string, string>();
                entry.Add(HCInfo.Id.ToString(), GlobalVars.ValidateEmptyOrNull(txtID.Text));
                entry.Add(HCInfo.imdb.ToString(), GlobalVars.ValidateEmptyOrNull(txtIMDB.Text));
                entry.Add(HCInfo.name.ToString(), GlobalVars.ValidateEmptyOrNull(txtName.Text));
                entry.Add(HCInfo.name_orig.ToString(), GlobalVars.ValidateEmptyOrNull(txtNameOrig.Text));
                entry.Add(HCInfo.name_series.ToString(), GlobalVars.ValidateEmptyOrNull(txtSeriesName.Text));
                entry.Add(HCInfo.season.ToString(), GlobalVars.ValidateEmptyOrNull(txtSeasonNum.Text));
                entry.Add(HCInfo.episode.ToString(), GlobalVars.ValidateEmptyOrNull(txtEpNum.Text));
                entry.Add(HCInfo.country.ToString(), GetCountry()); //GlobalVars.ValidateEmptyOrNull(lblCountry.Text);
                entry.Add(HCInfo.category.ToString(), GetCategory());
                entry.Add(HCInfo.genre.ToString(), GetGenre());
                entry.Add(HCInfo.studio.ToString(), GlobalVars.ValidateEmptyOrNull(txtStudio.Text));
                entry.Add(HCInfo.producer.ToString(), GlobalVars.ValidateEmptyOrNull(txtProducer.Text));
                entry.Add(HCInfo.director.ToString(), GlobalVars.ValidateEmptyOrNull(txtDirector.Text));
                entry.Add(HCInfo.artist.ToString(), GlobalVars.ValidateEmptyOrNull(txtArtist.Text));
                entry.Add(HCInfo.year.ToString(), GlobalVars.ValidateEmptyOrNull(txtYear.Text));
                entry.Add(HCInfo.summary.ToString(), GlobalVars.ValidateEmptyOrNull(txtSummary.Text));

                // Check if first query successfully executed
                ContAfterQry = SQLHelper.DbUpdateInfo(entry, callFrom);

                // IF INFO is executed, continue to FILEPATH
                if (ContAfterQry)
                {
                    var entryFile = new Dictionary<string, string>();
                    entryFile.Add(HCFile.Id.ToString(), GlobalVars.ValidateEmptyOrNull(txtID.Text)); // ID
                    entryFile.Add(HCFile.file.ToString(), GlobalVars.ValidateEmptyOrNull(txtPathFile.Text)); // file
                    entryFile.Add(HCFile.sub.ToString(), GlobalVars.ValidateEmptyOrNull(txtPathSub.Text)); // sub
                    entryFile.Add(HCFile.trailer.ToString(), GlobalVars.ValidateEmptyOrNull(txtPathTrailer.Text)); // trailers
                    SQLHelper.DbUpdateFilepath(entryFile, callFrom);
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
                // Save MetaData details
                var metaData = new List<string>(); // List for metadata values
                metaData.Add(GlobalVars.ValidateEmptyOrNull(txtName.Text)); // title
                metaData.Add(GlobalVars.ValidateEmptyOrNull(txtYear.Text)); // year
                metaData.Add(GetGenre()); // genre
                metaData.Add(GlobalVars.ValidateEmptyOrNull(txtDirector.Text)); // director
                metaData.Add(GlobalVars.ValidateEmptyOrNull(txtProducer.Text)); // producer
                GlobalVars.SaveMetadata(txtPathFile.Text, metaData);
            }

            // Close this form
            Close();
        }
        // OpenDialog and Select Cover of Movie and Set it to picBox.Image
        private void btnChangeCover_Click(object sender, EventArgs e)
        {
            // Get FileName on openDialog
            string selectedFilename = GlobalVars.GetAFile("Select Image file for Cover", "JPG Files (*.jpg)|*.jpg", Settings.LastPathCover);
            if (selectedFilename != "")
            {
                // Load image from file
                if (SetPicboxImgFromFile(picBox, selectedFilename, "btnChangeCover_Click"))
                {
                    try
                    {
                        Settings.LastPathCover = Path.GetDirectoryName(selectedFilename);
                    }
                    catch { }
                }
            }
        }
        // Change Movie File path
        private void btnChangeFile_Click(object sender, EventArgs e)
        {
            // Get file
            string selectedFile = GlobalVars.GetAFile("Select video file....", GlobalVars.FILTER_VIDEO, Settings.LastPathVideo);

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
            if (!GlobalVars.ShowYesNo("Replace information?", this)) { return; }
            string source = btnFetchData.Tag?.ToString();
            if (String.IsNullOrWhiteSpace(source))
            {
                source = "tmdb";
            }
            // Exit when no TMDB key
            if (!GlobalVars.HAS_TMDB_KEY && source.Equals("tmdb"))
            {
                GlobalVars.ShowWarning(GlobalVars.MSG_NO_TMDB);
                return;
            }
            // Exit when no Anilist config
            if ((String.IsNullOrWhiteSpace(GlobalVars.ANILIST_ID) || String.IsNullOrWhiteSpace(GlobalVars.ANILIST_SECRET)) && source.Equals("anilist"))
            {
                GlobalVars.ShowWarning(GlobalVars.MSG_NO_ANILIST);
                return;
            }
            // Declare vars
            frmLoading form = null;
            string errFrom = "frmMovieInfo-btnFetchData_Click";
            string IMDB_ID = txtIMDB.Text;
            string AnilistId = txtAnilist.Text;
            string country; // country text
            string genre; // genre text 
            bool coverDownloaded = false;
            MediaInfo mediaInfo = null;

            // Exit if IMDB id is invalid
            if ((String.IsNullOrWhiteSpace(IMDB_ID) || IMDB_ID=="0" || (IMDB_ID.StartsWith("tt")==false)) && (source.Equals("tmdb")))
            {
                GlobalVars.ShowWarning("Invalid IMDB Id!");
                txtIMDB.Focus();
                return;
            }

            // Get List of values from either TMDB or Anilist
            form = new frmLoading($"Fetching info from {source.ToUpper()}..", "Loading");
            form.BackgroundWorker.DoWork += (sender1, e1) =>
            {
                if (source.Equals("anilist"))
                {
                    mediaInfo = GlobalVars.GetMovieInfoByImdb(AnilistId, MEDIA_TYPE);
                }
                else
                {
                    mediaInfo = GlobalVars.GetMovieInfoByImdb(IMDB_ID, MEDIA_TYPE);
                }
            };
            form.ShowDialog(this);

            if (mediaInfo == null)
            {
                GlobalVars.ShowWarning("Title not found!", "", this);
                return;
            }

            // Set the values to textboxes
            if ((String.IsNullOrWhiteSpace(txtPathTrailer.Text)) && !string.IsNullOrWhiteSpace(mediaInfo.Trailer))
            {
                txtPathTrailer.Text = GlobalVars.LINK_YT + mediaInfo.Trailer;
            }
            // Set to textboxes
            if (String.IsNullOrWhiteSpace(mediaInfo.Title) ==false)
            {
                txtName.Text = mediaInfo.Title;
            }
            if (String.IsNullOrWhiteSpace(mediaInfo.OrigTitle) == false)
            {
                if (!mediaInfo.OrigTitle.Equals(txtName.Text))
                {
                    txtNameOrig.Text = mediaInfo.OrigTitle;
                }
            }
            if (String.IsNullOrWhiteSpace(mediaInfo.Summary) == false)
            {
                txtSummary.Text = mediaInfo.Summary;
            }
            if (String.IsNullOrWhiteSpace(mediaInfo.ReleaseDate) == false)
            {
                txtYear.Text = mediaInfo.ReleaseDate.Substring(0, 4);
            }

            txtArtist.Text = mediaInfo.Actor;
            txtDirector.Text = mediaInfo.Director;
            txtProducer.Text = mediaInfo.Producer;
            txtStudio.Text = mediaInfo.Studio;
            // Clear country and genre listbox
            listboxCountry.Items.Clear();
            listboxGenre.Items.Clear();
            // Set Country
            country = GlobalVars.ConvertListToString(mediaInfo.Country, ",", errFrom);
            LoadCountry(country);
            // Set Genres
            genre = GlobalVars.ConvertListToString(mediaInfo.Genre, ",", errFrom);
            LoadGenre(genre);
            // Set mediatype, after getting info from TMDB
            cbCategory.SelectedIndex = GlobalVars.GetCategoryByFilter(genre, country, MEDIA_TYPE);

            // Ask to change cover - poster image
            if (String.IsNullOrWhiteSpace(mediaInfo.PosterPath) == false)
            {
                if (GlobalVars.ShowYesNo("Do you want to change poster image?", this))
                {
                    // Show form loading
                    Thread.Sleep(2); // prevent overloading for TMDBor Anilist
                    string moviePosterDL = GlobalVars.PATH_TEMP + MOVIE_ID + ".jpg";

                    form = new frmLoading($"Downloading cover from {source.ToUpper()}..", "Loading");
                    form.BackgroundWorker.DoWork += (sender1, e1) =>
                    {
                        // Parse image link from JSON and download it
                        if (source.Equals("anilist"))
                        {
                            coverDownloaded = GlobalVars.DownloadCoverFromAnilist(MOVIE_ID, mediaInfo.PosterPath, errFrom);
                        }
                        else
                        {
                            coverDownloaded = GlobalVars.DownloadCoverFromTMDB(MOVIE_ID, mediaInfo.PosterPath, errFrom);
                        }
                    };
                    form.ShowDialog(this);
                    // Update Image in PictureBox
                    if (coverDownloaded)
                    {
                        if (SetPicboxImgFromFile(picBox, moviePosterDL, errFrom) == false)
                        {
                            GlobalVars.ShowWarning("Cover cannot be changed! File may be in use..");
                        }
                    }
                    else
                    {
                        GlobalVars.ShowWarning("Image file cannot be\ndownloaded at the moment!\n Try again later...");
                    }
                }
            }
            else
            {
                GlobalVars.ShowWarning($"No cover image fetched!\nSource: {source.ToUpper()}", "", this);
            }
        }
        // Get IMDB ID using Movie Name
        private void btnGetImdb_Click(object sender, EventArgs e)
        {
            string source = cbSource.Text.ToLower();
            // Exit when no TMDB key
            if (!GlobalVars.HAS_TMDB_KEY && source.Equals("tmdb"))
            {
                GlobalVars.ShowWarning(GlobalVars.MSG_NO_TMDB);
                return;
            }
            // Exit when no Anilist config
            if ((String.IsNullOrWhiteSpace(GlobalVars.ANILIST_ID) || String.IsNullOrWhiteSpace(GlobalVars.ANILIST_SECRET)) && source.Equals("anilist"))
            {
                GlobalVars.ShowWarning(GlobalVars.MSG_NO_ANILIST);
                return;
            }

            // Check if txtName is valid
            if (String.IsNullOrWhiteSpace(txtName.Text))
            {
                GlobalVars.ShowWarning("Invalid Name!");
                txtName.Focus();
                return;
            }

            // Show form for tmdb searching
            var form = new frmSearchMedia($"Search for {txtName.Text}", txtName.Text, txtID.Text, source);
            form.ShowDialog(this);
            var getResult = form.getResult;
            MEDIA_TYPE = form.getResultMedia.Equals("tv") ? "series" : "movie";
            form.Dispose();

            // Get IMDB from TMDB json info
            if (String.IsNullOrWhiteSpace(getResult) == false)
            {
                if (source.Equals("anilist"))
                {
                    txtAnilist.Text = getResult;
                }
                else
                {
                    txtIMDB.Text = getResult;
                }
                btnFetchData.Tag = source;
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
