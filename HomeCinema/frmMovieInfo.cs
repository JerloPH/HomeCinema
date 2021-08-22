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
        // Private Vars
        private Form PARENT = null;
        private long MOVIE_ID { get; set; } = 0;
        private string MEDIA_TYPE { get; set; } = "movie";
        private Image MOVIE_COVER { get; set; } = null;
        private Image tempImage { get; set; } = null;

        // Source ListView lvSearch Item index
        public ListViewItem LVITEM = null;

        #region Initialize class
        public frmMovieInfo(Form parent,string ID, string formName, ListViewItem lvitem)
        {
            InitializeComponent();
            // Form properties
            Name = formName;
            Icon = parent.Icon;

            // Set vars
            long movieId = 0;
            if (long.TryParse(ID, out movieId))
            {
                MOVIE_ID = movieId;
            }
            PARENT = parent;
            LVITEM = lvitem;

            // Set Controls text and properties
            txtID.Text = MOVIE_ID.ToString();

            // Set picBox size mode
            picBox.SizeMode = PictureBoxSizeMode.StretchImage;
            picBox.Tag = null;

            // Add items to cbCategory
            cbCategory.Items.AddRange(GlobalVars.DB_INFO_CATEGORY);
            cbSource.Items.AddRange(HCSource.sources);

            // LOAD Information from DATABASE and SET to Controls
            if (MOVIE_ID > 0)
            {
                LoadInformation(ID);
                RefreshCountryAndGenre();
            }

            // Chang default source, depending on loaded info
            cbSource.SelectedIndex = (cbCategory.Text.ToLower().Contains("anime") ? 1 : 0);

            // Show the form at center
            StartPosition = FormStartPosition.CenterParent;

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
        public void LoadListBoxItems(ListBox lb, object items, char sep)
        {
            var temp = GlobalVars.ValidateStringToArray(items, sep).ToList<string>();
            LoadListBoxItems(temp, lb);
        }
        public void LoadListBoxItems(List<string> itemsList, ListBox lb)
        {
            string itemAdd = "";

            lb.Items.Clear();
            foreach (string item in itemsList)
            {
                itemAdd = item.Trim();
                if (CanAddToListBox(lb, itemAdd))
                {
                    lb.Items.Add(itemAdd);
                }
            }
        }
        // ########################## OTHER FUNCTIONS
        // REFRESH INFORMATION
        public void LoadInformation(string ID)
        {
            string errFrom = $"frmMovieInfo-({Name})-LoadInformation";
            // Set Form Properties
            Text = $"[EDIT] Unknown Movie";

            // Change cover image, if image exists in ImageList
            MOVIE_COVER = GlobalVars.ImgGetImageFromList(MOVIE_ID.ToString());
            if (MOVIE_COVER != null)
            {
                picBox.Image = MOVIE_COVER;
            }

            // Set textbox values from Database
            // Build query string
            string qry = $"SELECT * FROM {HCTable.info} A LEFT JOIN {HCTable.filepath} B ON A.`{HCInfo.Id}`=B.`{HCInfo.Id}` WHERE A.`{HCInfo.Id}`={ID.TrimStart('0')} LIMIT 1;";

            using (DataTable dtFile = SQLHelper.DbQuery(qry, "frmMovie-LoadInformation"))
            {
                try
                {
                    DataRow row = dtFile.Rows[0];
                    txtPathFile.Text = row[HCFile.File].ToString();
                    txtPathSub.Text = row[HCFile.Sub].ToString();
                    txtPathTrailer.Text = row[HCFile.Trailer].ToString();

                    // Set textboxes
                    txtIMDB.Text = row[HCInfo.imdb].ToString();
                    txtIMDB.Tag = "";
                    txtAnilist.Text = row[HCInfo.anilist].ToString();
                    txtName.Text = row[HCInfo.name].ToString();
                    txtNameOrig.Text = row[HCInfo.name_orig].ToString();
                    txtSeriesName.Text = row[HCInfo.name_series].ToString();
                    txtSeasonNum.Text = row[HCInfo.season].ToString();
                    txtEpNum.Text = row[HCInfo.episode].ToString();
                    LoadListBoxItems(listboxCountry, row[HCInfo.country], ',');
                    cbCategory.SelectedIndex = Convert.ToInt16(row[HCInfo.category]);
                    LoadListBoxItems(listboxGenre, row[HCInfo.genre], ',');
                    LoadListBoxItems(lboxStudio, row[HCInfo.studio], ';');
                    txtProducer.Text = row[HCInfo.producer].ToString();
                    txtDirector.Text = row[HCInfo.director].ToString();
                    txtArtist.Text = row[HCInfo.artist].ToString();
                    txtYear.Text = row[HCInfo.year].ToString();
                    txtSummary.Text = row[HCInfo.summary].ToString();
                    Text = $"[EDIT] {row[HCInfo.name].ToString()}";
                }
                catch (Exception ex)
                {
                    Logs.LogErr(errFrom, ex);
                    GlobalVars.ShowWarning("Entry not found!", "", this);
                }
            }

            // Disable setting metadata if series
            if (cbCategory.Text.ToLower().Contains("series"))
            {
                MEDIA_TYPE = "tv";
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
                    if (Settings.IsConfirmMsg)
                        GlobalVars.ShowInfo("Changed the image cover!");
                    
                    return true;
                }
                catch (Exception exc)
                {
                    GlobalVars.ShowError(errFrom, exc, "Image selected is not loaded!", this);
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
            if (File.Exists(GlobalVars.ImgFullPath(MOVIE_ID.ToString())))
            {
                if (GlobalVars.DeleteImageFromList(this, MOVIE_ID.ToString(), ExceptionFrom + " (Previous Image)") == false)
                {
                    GlobalVars.ShowWarning("Cannot replace image!\nFile must be NOT in use!");
                    return;
                }
            }
            // Remove previous Image from memory
            DisposeImages();

            // Copy new file to App Path
            string sourceFile = selectedFilename;
            string destFile = GlobalVars.ImgFullPath(MOVIE_ID.ToString());

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
                Logs.Log(ExceptionFrom + " [NEW Image File and Name]", sourceFile);
            }
            catch (Exception fex)
            {
                GlobalVars.ShowError(ExceptionFrom, fex, "Image file selected is not saved!", this);
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
                var form = PARENT as frmMovie;
                form.DisposePoster();
            }
        }
        private void RefreshCountryAndGenre()
        {
            // Add new country to text file
            foreach (string item in listboxCountry.Items)
            {
                GlobalVars.WriteAppend(DataFile.FILE_COUNTRY, $",{item}", false);
            }
            // Add new genre to text file
            foreach (string item in listboxGenre.Items)
            {
                GlobalVars.WriteAppend(DataFile.FILE_GENRE, $",{item}", false);
            }
            Program.FormMain.PopulateCountryCB(); // Refresh Country list
            Program.FormMain.PopulateGenreCB(); // Refresh Genre list
        }
        #endregion
        // ############################################################################## Form Controls methods event
        private void frmMovieInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If Parent is of type: frmMovie
            if (PARENT is frmMovie)
            {
                var form = PARENT as frmMovie;
                form.ChildForm = null;
                Logs.LogDebug("frmMovie Parent is null!");
            }
            picBox.Image?.Dispose();
            tempImage?.Dispose();
            foreach (Control c in Controls)
            {
                c?.Dispose();
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
            if (MOVIE_ID > 0)
            {
                // SAVE changes to EXISTING MOVIE
                var entry = new MediaInfo();
                int seasons = 0, eps = 0;

                // Parse integers
                int.TryParse(txtSeasonNum.Text, out seasons);
                int.TryParse(txtEpNum.Text, out eps);

                entry.Id = txtID.Text;
                entry.Imdb = txtIMDB.Text;
                entry.Anilist = txtAnilist.Text;
                entry.Title = txtName.Text;
                entry.OrigTitle = txtNameOrig.Text;
                entry.SeriesName = txtSeriesName.Text;
                entry.Seasons = seasons;
                entry.Episodes = eps;
                entry.Category = cbCategory.SelectedIndex;
                entry.Producer = txtProducer.Text;
                entry.Director = txtDirector.Text;
                entry.Actor = txtArtist.Text;
                entry.ReleaseDate = txtYear.Text;
                entry.Summary = txtSummary.Text;
                foreach (var item in listboxCountry.Items)
                {
                    if (item != null)
                        entry.Country.Add(item.ToString().Trim());
                }
                foreach (var item in listboxGenre.Items)
                {
                    if (item != null)
                        entry.Genre.Add(item.ToString().Trim());
                }
                foreach (var item in lboxStudio.Items)
                {
                    if (item != null)
                        entry.Studio.Add(item.ToString().Trim());
                }
                entry.FilePath = txtPathFile.Text;
                entry.FileSub = txtPathSub.Text;
                entry.Trailer = txtPathTrailer.Text;
                // Check if first query successfully executed
                ContAfterQry = SQLHelper.DbUpdateInfo(entry, callFrom);
                if (!ContAfterQry)
                {
                    GlobalVars.ShowWarning("Not saved properly!\nReview data and Try again,", "", this);
                    return;
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
                var form = PARENT as frmMovie;
                form.LoadInformation(MOVIE_ID.ToString());
            }
            RefreshCountryAndGenre(); // Add new country/genre to text files

            // Refresh main form properties
            Program.FormMain.UpdateMovieItemOnLV(LVITEM); // ListView item of this

            // Save Metadata
            if (cbSaveMetadata.Checked)
            {
                // Save MetaData details
                string stringGenre = GlobalVars.ConvertListBoxToString(listboxGenre);
                var metaData = new List<string>(); // List for metadata values
                metaData.Add(GlobalVars.ValidateEmptyOrNull(txtName.Text)); // title
                metaData.Add(GlobalVars.ValidateEmptyOrNull(txtYear.Text)); // year
                metaData.Add(stringGenre); // genre
                metaData.Add(GlobalVars.ValidateEmptyOrNull(txtDirector.Text)); // director
                metaData.Add(GlobalVars.ValidateEmptyOrNull(txtProducer.Text)); // producer
                GlobalVars.SaveMetadata(txtPathFile.Text, metaData);
            }

            Close(); // Close this form
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
                    if (Settings.IsConfirmMsg)
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
            // Declare vars
            string errFrom = "frmMovieInfo-btnFetchData_Click";
            string IMDB_ID = txtIMDB.Text;
            string AnilistId = txtAnilist.Text;
            string source = cbSource.Text.ToLower();
            long AniId = 0;
            // Set default source
            if (String.IsNullOrWhiteSpace(source))
            {
                source = HCSource.tmdb;
            }
            // Exit conditions
            if (!String.IsNullOrWhiteSpace(txtYear.Text) && txtYear.Text!="0")
            {
                if (!GlobalVars.ShowYesNo("Replace information?", this)) { return; }
            }
            // Exit when no TMDB key
            if (!GlobalVars.HAS_TMDB_KEY && source.Equals(HCSource.tmdb))
            {
                GlobalVars.ShowWarning(GlobalVars.MSG_NO_TMDB);
                return;
            }
            // Exit if IMDB id is invalid
            if ((String.IsNullOrWhiteSpace(IMDB_ID) || IMDB_ID=="0" || (IMDB_ID.StartsWith("tt")==false)) && (source.Equals(HCSource.tmdb)))
            {
                GlobalVars.ShowWarning("Invalid IMDB Id!");
                txtIMDB.Focus();
                return;
            }
            // Exit if Anilist id is invalid
            if ((String.IsNullOrWhiteSpace(AnilistId) || AnilistId == "0" || !long.TryParse(AnilistId, out AniId)) && (source.Equals(HCSource.anilist)))
            {
                GlobalVars.ShowWarning("Invalid Anilist Id!");
                txtAnilist.Focus();
                return;
            }
            // Variables used
            frmLoading form = null;
            string country = ""; // country text
            string genre = ""; // genre text 
            bool coverDownloaded = false;
            MediaInfo mediaInfo = null;
            
            // Get List of values from either TMDB or Anilist
            form = new frmLoading($"Fetching info from {source.ToUpper()}..", "Loading");
            form.BackgroundWorker.DoWork += (sender1, e1) =>
            {
                if (source.Equals(HCSource.anilist))
                {
                    mediaInfo = AnilistAPI.GetMovieInfoFromAnilist(AnilistId);
                }
                else
                {
                    string tag = (txtIMDB.Tag != null) ? txtIMDB.Tag.ToString() : "";
                    string TmdbId = (String.IsNullOrWhiteSpace(tag)) ? TmdbAPI.GetTmdbFromImdb(IMDB_ID, MEDIA_TYPE) : tag;
                    mediaInfo = TmdbAPI.GetMovieInfoFromTmdb(TmdbId, MEDIA_TYPE);
                }
            };
            form.ShowDialog(this);

            if (mediaInfo == null)
            {
                GlobalVars.ShowWarning("Title not found!", "", this);
                return;
            }

            // Replace values, only when its not empty
            if (!string.IsNullOrWhiteSpace(mediaInfo.Trailer))
            {
                txtPathTrailer.Text = mediaInfo.Trailer; // Youtube trailer
            }
            if (!String.IsNullOrWhiteSpace(mediaInfo.Title)) // Title
            {
                txtName.Text = mediaInfo.Title;
            }
            if (!String.IsNullOrWhiteSpace(mediaInfo.OrigTitle)) // Orig Title
            {
                if (!mediaInfo.OrigTitle.Equals(txtName.Text))
                {
                    txtNameOrig.Text = mediaInfo.OrigTitle;
                }
            }
            if (!String.IsNullOrWhiteSpace(mediaInfo.Summary)) // Description / Summary
            {
                txtSummary.Text = mediaInfo.Summary;
            }
            if (!String.IsNullOrWhiteSpace(mediaInfo.ReleaseDate)) // Year
            {
                txtYear.Text = mediaInfo.ReleaseDate.Substring(0, 4);
            }
            if (!String.IsNullOrWhiteSpace(mediaInfo.Actor)) // Actor/Actress/Artists
            {
                txtArtist.Text = mediaInfo.Actor;
            }
            if (!String.IsNullOrWhiteSpace(mediaInfo.Director)) // Director
            {
                txtDirector.Text = mediaInfo.Director;
            }
            if (!String.IsNullOrWhiteSpace(mediaInfo.Producer)) // Producer
            {
                txtProducer.Text = mediaInfo.Producer;
            }
            //Season and Episode counts
            txtEpNum.Text = mediaInfo.Episodes.ToString();
            txtSeasonNum.Text = mediaInfo.Seasons.ToString();
            
            // Set Country
            if (mediaInfo.Country?.Count > 0)
            {
                LoadListBoxItems(mediaInfo.Country, listboxCountry);
            }
            // Set Genres
            if (mediaInfo.Genre?.Count > 0)
            {
                LoadListBoxItems(mediaInfo.Genre, listboxGenre);
            }
            // Set Studio
            if (mediaInfo.Studio?.Count > 0)
            {
                LoadListBoxItems(mediaInfo.Studio, listboxGenre);
            }
            // Set mediatype, after getting info from TMDB or Anilist
            if (!String.IsNullOrWhiteSpace(genre) && !String.IsNullOrWhiteSpace(country))
            {
                cbCategory.SelectedIndex = GlobalVars.GetCategoryValue(MEDIA_TYPE, source);
            }

            // Ask to change cover - poster image
            if (String.IsNullOrWhiteSpace(mediaInfo.PosterPath) == false)
            {
                string moviePosterDL = $"{DataFile.PATH_TEMP}{MOVIE_ID}.jpg";
                string existingcover = $"{DataFile.PATH_IMG}{MOVIE_ID}.jpg";
                bool downloadCover = true;
                if (File.Exists(existingcover))
                {
                    downloadCover = GlobalVars.ShowYesNo("Do you want to change poster image?", this);
                }
                if (downloadCover)
                {
                    // Show form loading
                    Thread.Sleep(2); // prevent overloading for TMDB or Anilist
                    form = new frmLoading($"Downloading cover from {source.ToUpper()}..", "Loading");
                    form.BackgroundWorker.DoWork += (sender1, e1) =>
                    {
                        // Parse image link from JSON and download it
                        if (source.Equals(HCSource.anilist))
                        {
                            coverDownloaded = AnilistAPI.DownloadCoverFromAnilist(MOVIE_ID.ToString(), mediaInfo.PosterPath, errFrom);
                        }
                        else
                        {
                            coverDownloaded = TmdbAPI.DownloadCoverFromTMDB(MOVIE_ID.ToString(), mediaInfo.PosterPath, errFrom);
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
                        GlobalVars.ShowWarning("Image file cannot be\ndownloaded at the moment!\nTry again later...");
                    }
                }
            }
            else
            {
                GlobalVars.ShowWarning($"No cover image fetched!\nSource: {source.ToUpper()}", "", this);
            }
        }
        // Get IMDB ID using Movie Name
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string source = cbSource.Text.ToLower();
            // Exit when no TMDB key
            if (!GlobalVars.HAS_TMDB_KEY && source.Equals(HCSource.tmdb))
            {
                GlobalVars.ShowWarning(GlobalVars.MSG_NO_TMDB);
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
            var getTmdb = form.getResultTmdb;
            MEDIA_TYPE = form.getResultMedia.Equals("tv") ? "series" : "movie";
            form.Dispose();

            // Get IMDB from TMDB json info
            if (String.IsNullOrWhiteSpace(getResult) == false)
            {
                if (source.Equals(HCSource.anilist))
                {
                    txtAnilist.Text = getResult;
                }
                else
                {
                    txtIMDB.Text = getResult;
                    txtIMDB.Tag = getTmdb;
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
