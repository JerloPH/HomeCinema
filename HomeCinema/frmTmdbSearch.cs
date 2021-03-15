using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HomeCinema.Global;
using Newtonsoft.Json;

namespace HomeCinema
{
    public partial class frmTmdbSearch : Form
    {
        private string mediatype;
        private string movieId;
        private ImageList imageList = new ImageList();
        private string result;
        public string getResult
        {
            get { return result; }
            set { result = value; }
        }
        public frmTmdbSearch(string caption, string query, string mediaType, string id)
        {
            InitializeComponent();
            // Set variables
            mediatype = (!String.IsNullOrWhiteSpace(mediaType)) ? mediaType : "movie";
            movieId = id;
            result = "";
            // Set textbox and labels
            txtInput.Text = query;
            Text = GlobalVars.HOMECINEMA_NAME;
            lblCaption.Text = caption;
            // Set control properties
            FormClosed += FrmTmdbSearch_FormClosed;
            //btnOK.DialogResult = DialogResult.OK;
            //btnCancel.DialogResult = DialogResult.Cancel;
            // Set lvResult properties
            lvResult.Columns.Add("colname");
            //lvResult.Columns.Add("colimg");
            lvResult.View = View.LargeIcon;
            
            imageList.ImageSize = new Size(48, 64);// Set imageList properties
            imageList.ColorDepth = ColorDepth.Depth32Bit;// Set imageList properties

            //lvResult.SmallImageList = imageList;
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
        private void ClearImageList(ImageList imglist)
        {
            if (imglist != null)
            {
                try
                {
                    if (imglist.Images.Count > 0)
                    {
                        foreach (Image img in imglist.Images)
                        {
                            img.Dispose();
                        }
                    }
                    imglist.Dispose();
                }
                catch { return; }
            }
        }
        private void SearchTmdb()
        {
            string errFrom = $"frmTmdbSearch-SearchTmdb";
            // Setup vars and links
            string KEY = GlobalVars.TMDB_KEY;
            // GET TMDB MOVIE ID
            string urlJSONgetId = @"https://api.themoviedb.org/3/search/" + mediatype + "?api_key=" + KEY + "&query=" + txtInput.Text;
            string JSONgetID = GlobalVars.PATH_TEMP + movieId + "_id.json";
            string JSONContents = "";
            string rPosterLink = "";
            int count = 0;

            ClearImageList(imageList);
            Image defImg = Image.FromFile(GlobalVars.FILE_DEFIMG);
            imageList.Images.Add("0", defImg);
            //defImg.Dispose();
            lvResult.View = View.LargeIcon;
            lvResult.Items.Clear();
            lvResult.BeginUpdate(); // Pause drawing events on ListView
            lvResult.SuspendLayout();
            

            var form = new frmLoading($"Searching for {txtInput.Text}", GlobalVars.HOMECINEMA_NAME);
            form.BackgroundWorker.DoWork += (sender1, e1) =>
            {
                // Download json file of search results
                if (GlobalVars.DownloadAndReplace(JSONgetID, urlJSONgetId, errFrom))
                {

                    JSONContents = GlobalVars.ReadStringFromFile(JSONgetID, errFrom);
                    var objPageResult = JsonConvert.DeserializeObject<TmdbPageResult>(JSONContents);
                    if (objPageResult.results.Count > 0)
                    {
                        // Add to ListView
                        foreach (Result result in objPageResult.results)
                        {
                            string ImdbFromApi  = GlobalVars.GetImdbFromAPI(result.id.ToString(), mediatype);
                            string imgFilePath = $"{GlobalVars.PATH_TEMP}{ImdbFromApi}.jpg";
                            string imgKey = "0";

                            // Add image to ImageList
                            rPosterLink = result.poster_path;
                            GlobalVars.TryDelete(imgFilePath, errFrom);
                            if (!String.IsNullOrWhiteSpace(rPosterLink))
                            {
                                GlobalVars.DownloadCoverFromTMDB(ImdbFromApi, rPosterLink, errFrom);
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
                                        //GlobalVars.TryDelete(imgFilePath, errFrom);
                                        if (Debugger.IsAttached)
                                        {
                                            //GlobalVars.ShowInfo("success adding image!");
                                        }
                                    }));
                                }
                                catch { imgKey = "0"; }
                            }
                            // Create ListView item
                            ListViewItem lvItem = new ListViewItem();
                            int index = imageList.Images.IndexOfKey(imgKey);
                            lvItem.Text = mediatype.Equals("movie") ? result.title : result.name;
                            lvItem.Tag = ImdbFromApi;
                            lvItem.ImageIndex = (index > 0) ? index : 0;
                            lvItem.ImageIndex = 0;
                            AddItem(lvResult, lvItem);
                            ++count;
                            if (count > 2)
                            {
                                break;
                            }
                        }
                    }
                }
            };
            form.ShowDialog(this);
            lvResult.EndUpdate(); // Draw the ListView
            lvResult.ResumeLayout();
            lvResult.Refresh();
            //lvResult.RedrawItems(0, lvResult.Items.Count-1, false);
            if (Debugger.IsAttached)
            {
                GlobalVars.ShowInfo($"success adding image! Count: {imageList.Images.Count}");
            }
        }
        #endregion
        // ######################################################### EVENTS
        private void FrmTmdbSearch_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClearImageList(imageList);
            //Dispose();
        }

        private void frmTmdbSearch_Load(object sender, EventArgs e)
        {
            SearchTmdb();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lvResult.SelectedItems.Count > 0)
            {
                result = lvResult.SelectedItems[0].Tag.ToString();
                if (String.IsNullOrWhiteSpace(result))
                {
                    GlobalVars.ShowWarning("Selected item has invalid IMDB Id!", GlobalVars.HOMECINEMA_NAME);
                    return;
                }
                Close();
            }
            else
            {
                GlobalVars.ShowWarning("No selected result!", GlobalVars.HOMECINEMA_NAME);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int size;
            SearchTmdb();
            size = lvResult.Items.Count;
            if (Debugger.IsAttached)
            {
                string msg = "";
                foreach (ListViewItem lv in lvResult.Items)
                {
                    msg += $"Key: {lv.ImageKey}\nIndex: {lv.ImageIndex}\nTag: {lv.Tag}\n\n";
                }
                GlobalVars.ShowInfo(msg);
            }
            if (size > 0)
            {
                GlobalVars.ShowInfo($"Found {size} results!");
            }
        }
    }
}
