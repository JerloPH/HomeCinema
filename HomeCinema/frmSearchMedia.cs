using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace HomeCinema
{
    public partial class frmSearchMedia : Form
    {
        private string movieId;
        private ImageList imageList = new ImageList();
        private string result = "";
        private string mediatype = "";
        private string Source = HCSource.tmdb;
        private string TmdbId = "";
        public string getResult
        {
            get { return result; }
            set { result = value; }
        }
        public string getResultMedia
        {
            get { return mediatype; }
            set { mediatype = value; }
        }
        public string getResultTmdb
        {
            get { return TmdbId; }
            set { TmdbId = value; }
        }
        public frmSearchMedia(string caption, string query, string id, string source = "")
        {
            InitializeComponent();
            Themes.SetThemeAndBtns(this, this.Controls);
            // Set variables
            movieId = id;
            result = "";
            Source = (!String.IsNullOrWhiteSpace(source) ? source : HCSource.tmdb);
            // Set textbox and labels
            txtInput.Text = query;
            Text = GlobalVars.HOMECINEMA_NAME;
            lblCaption.Text = $"Search for: {caption}";
            // Set control properties
            FormClosed += frmSearchMedia_FormClosed;
            //btnOK.DialogResult = DialogResult.OK;
            //btnCancel.DialogResult = DialogResult.Cancel;
            // Set lvResult properties
            lvResult.Columns.Add("colname");
            lvResult.View = View.LargeIcon;
            // Set ImageList properties
            imageList.ImageSize = new Size(100, 150);// Set thumbnail size
            imageList.ColorDepth = ColorDepth.Depth32Bit;// Set color depth
            // use imageList in ListView
            lvResult.LargeImageList = imageList;
        }
        // ######################################################### FUNCTIONS
        #region Functions
        private delegate void AddItemDelegate(ListView lv, ListViewItem item);
        public static void AddItem(ListView lv, ListViewItem item)
        {
            if (lv.InvokeRequired)
            {
                lv.Invoke(new AddItemDelegate(AddItem), new object[] { lv, item });
            }
            else
            {
                lv.Items.Add(item);
            }
        }
        private void ClearImageList(bool dispose = false)
        {
            if (imageList != null)
            {
                if (imageList.Images.Count > 0)
                {
                    foreach (Image img in imageList.Images)
                    {
                        img?.Dispose();
                    }
                }
                if (dispose)
                {
                    imageList.Dispose();
                }
            }
        }
        private int SearchTmdb()
        {
            // Setup var and links
            string errFrom = $"frmSearchMedia-SearchTmdb";
            string rPosterLink = "";
            int count = 0;
            string query = txtInput.Text;

            var form = new frmLoading($"Searching for {query}", GlobalVars.HOMECINEMA_NAME);
            form.BackgroundWorker.DoWork += (sender1, e1) =>
            {
                var objPageResult = TmdbAPI.FindMulti(query, movieId);
                if (objPageResult != null)
                {
                    if (objPageResult.results?.Count > 0)
                    {
                        // Add to ListView
                        foreach (var result in objPageResult.results)
                        {
                            if (count > Settings.SearchLimit && Settings.SearchLimit > 0) { break; } // Exit when item result count is reached
                            string TmdbId = result.Id.ToString();
                            string imgFilePath = $"{DataFile.PATH_TEMP}{TmdbId}.jpg";
                            string imgKey = "0";
                            // Add image to ImageList
                            rPosterLink = result.PosterPath;
                            GlobalVars.TryDelete(imgFilePath, errFrom);
                            if (!String.IsNullOrWhiteSpace(rPosterLink))
                            {
                                TmdbAPI.DownloadCoverFromTMDB(TmdbId, rPosterLink, errFrom);
                            }
                            if (File.Exists(imgFilePath))
                            {
                                imgKey = Path.GetFileNameWithoutExtension(imgFilePath);
                                try
                                {
                                    this.Invoke(new Action(() =>
                                    {
                                        Image img = Image.FromFile(imgFilePath);
                                        imageList.Images.Add(imgKey, img);
                                        img.Dispose();
                                        GlobalVars.TryDelete(imgFilePath, errFrom);
                                    }));
                                }
                                catch { imgKey = "0"; }
                            }
                            // Create ListView item
                            ListViewItem lvItem = new ListViewItem();
                            int index = imageList.Images.IndexOfKey(imgKey);
                            lvItem.Text = result.MediaType.Equals("movie") ? result.Title : result.Name;
                            lvItem.Tag = $"tmdb*{result.Id}*{result.MediaType}";
                            lvItem.ImageIndex = (index > 0) ? index : 0;
                            AddItem(lvResult, lvItem);
                            count += 1;
                        }
                    }
                }
            };
            form.ShowDialog(this);
            return count;
        }

        private int SearchAnilist()
        {
            string calledFrom = "frmSearchMedia-SearchAnilist()";
            int resultCount = 0;
            string imgKey = "";

            var form = new frmLoading($"Searching for {txtInput.Text}", GlobalVars.HOMECINEMA_NAME);
            form.BackgroundWorker.DoWork += (sender1, e1) =>
            {
                var Results = AnilistAPI.SearchForAnime(txtInput.Text);
                if (Results != null)
                {
                    foreach (var media in Results.Data.Page.MediaList)
                    {
                        string title = (!String.IsNullOrWhiteSpace(media.Title.English)) ? media.Title.English : media.Title.Romaji;
                        string mediaId = media.Id.ToString();
                        string cover = media.CoverImage.Medium;
                        string mediatype = media.Format.ToLower();
                        string imgFilePath = $"{DataFile.PATH_TEMP}{mediaId}.jpg";

                        // Change mediatype
                        if (mediatype.Equals("movie") || media.Episodes < 2)
                        {
                            mediatype = "movie";
                        }
                        else { mediatype = "tv"; }

                        GlobalVars.DeleteMove(imgFilePath, calledFrom);
                        if (!String.IsNullOrWhiteSpace(cover))
                        {
                            GlobalVars.DownloadAndReplace(imgFilePath, cover, calledFrom);
                        }
                        if (File.Exists(imgFilePath))
                        {
                            imgKey = Path.GetFileNameWithoutExtension(imgFilePath);
                            try
                            {
                                this.Invoke(new Action(() =>
                                {
                                    using (Image img = Image.FromFile(imgFilePath))
                                    {
                                        imageList.Images.Add(imgKey, img);
                                    }
                                    GlobalVars.TryDelete(imgFilePath, calledFrom);
                                }));
                            }
                            catch { imgKey = "0"; }
                        }
                        // Create ListView item
                        ListViewItem lvItem = new ListViewItem();
                        int index = imageList.Images.IndexOfKey(imgKey);
                        lvItem.Text = title;
                        lvItem.Tag = $"anilist*{mediaId}*{mediatype}";
                        lvItem.ImageIndex = (index > 0) ? index : 0;
                        AddItem(lvResult, lvItem);
                        resultCount += 1;
                        Thread.Sleep(100);
                    }
                }
            };
            form.ShowDialog(this);
            return resultCount;
        }
        #endregion
        // ######################################################### EVENTS
        private void frmSearchMedia_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClearImageList(true);
            //Dispose();
            GlobalVars.Refocus(this);
        }

        private void frmSearchMedia_Load(object sender, EventArgs e)
        {
            btnSearch.PerformClick();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lvResult.SelectedItems.Count > 0)
            {
                if (lvResult.SelectedItems[0].Tag != null)
                {
                    string[] resultString = lvResult.SelectedItems[0].Tag.ToString().Split('*');
                    if (resultString.Length > 1)
                    {
                        result = resultString[1];
                        mediatype = resultString[2];
                        if (resultString[0].Equals("tmdb"))
                        {
                            string imdb = "";
                            TmdbId = resultString[1];
                            var form = new frmLoading("Fetching Imdb..", "Fetching data");
                            form.BackgroundWorker.DoWork += (sender1, e1) =>
                            {
                                imdb = TmdbAPI.GetImdbFromAPI(resultString[1], resultString[2]);
                            };
                            form.ShowDialog(this);
                            result = imdb;
                            if (String.IsNullOrWhiteSpace(result))
                            {
                                Msg.ShowWarning("Selected item has no IMDB Id!");
                                return;
                            }
                        }
                        else
                        {
                            if (String.IsNullOrWhiteSpace(result))
                            {
                                Msg.ShowWarning("Selected item has no valid Id!");
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (String.IsNullOrWhiteSpace(result))
                        {
                            Msg.ShowWarning("Selected item is invalid!");
                            return;
                        }
                    }
                }
                else
                {
                    Msg.ShowWarning("No result found!\nSearch using different query");
                    return;
                }
                Close();
            }
            else
                Msg.ShowWarning("No selected result!");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtInput.Text))
            {
                Msg.ShowWarning("Invalid search query!");
                return;
            }
            lblCaption.Text = $"Search for: {txtInput.Text}";
            ClearImageList();
            Image defImg = null;
            try
            {
                defImg = Image.FromFile(DataFile.FILE_DEFIMG);
                imageList.Images.Add("0", defImg);
                defImg.Dispose();
            }
            catch (Exception ex)
            {
                Logs.LogErr("frmSearchMedia-btnSearch_Click", ex);
                Msg.ShowWarning("Default image is missing!");
                return;
            }
            lvResult.View = View.LargeIcon;
            lvResult.Items.Clear();
            lvResult.BeginUpdate(); // Pause drawing events on ListView
            lvResult.SuspendLayout();
            int size = Source.Equals(HCSource.tmdb) ? SearchTmdb() : SearchAnilist();
            if (size > 0 && Settings.IsConfirmMsg)
            {
                Msg.ShowInfo($"Found {size} results!", "", this);
            }
            lvResult.EndUpdate(); // Draw the ListView
            lvResult.ResumeLayout();
            lvResult.Refresh();
            lvResult.LargeImageList = imageList;
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            if (e.KeyCode == Keys.Enter)
            {
                // Check if searchbox is empty
                if (!String.IsNullOrWhiteSpace(txtInput.Text))
                {
                    btnSearch.PerformClick();
                }
            }
        }

        private void lvResult_DoubleClick(object sender, EventArgs e)
        {
            // Emulate 'OK' button clicked.
            if (lvResult.SelectedItems.Count > 0)
            {
                btnOK.PerformClick();
            }
        }
    }
}
