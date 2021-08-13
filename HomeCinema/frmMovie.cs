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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using HomeCinema.SQLFunc;

namespace HomeCinema
{
    public partial class frmMovie : Form
    {
        private string childForm { get; set; } = "";
        private string MOVIE_ID { get; set; } = "";
        private string MOVIE_NAME { get; set; } = "";
        private string MOVIE_FILEPATH { get; set; } = "";
        private string MOVIE_TRAILER { get; set; } = "";
        private Image MOVIE_COVER { get; set; } = null;
        private Image MOVIE_COVER_FULL { get; set; } = null;

        // Source ListView lvSearch Item index
        public ListViewItem LVITEM = null;
        private bool IsDeleted = false;

        public frmMovie(Form parent, string ID, string name, ListViewItem lvitem)
        {
            GlobalVars.Log(parent.Name + " (OPEN a MOVIE)", "MOVIE formName: " + Name);
            InitializeComponent();

            // Form properties
            FormClosing += new FormClosingEventHandler(frmMovie_FormClosing);

            // Assign values to vars
            MOVIE_ID = ID;
            MOVIE_NAME = name;
            childForm = GlobalVars.PREFIX_MOVIEINFO + MOVIE_ID;
            LVITEM = lvitem;

            // Set picBox size mode
            picBox.SizeMode = PictureBoxSizeMode.StretchImage;

            // Change controls text and properties
            Text = name;

            // Show this form
            StartPosition = FormStartPosition.CenterParent;
            Show(parent);

            // Show this to other monitor
            //GlobalVars.MaximizeToMonitor(this, 1);

            // Set main focus at startup
            btnPlay.Focus();
        }
        // ############################################################################## Functions
        #region Functions
        public void LoadInformation(string ID)
        {
            // Set textbox values from Database
            string errFrom = "frmMovie-LoadInformation";
            string qry, Imagefile;
            var dtInfo = new DataTable();
            var tp = new ToolTip();
            frmLoading form = new frmLoading("Opening media info..", "Loading");

            // Build query for FilePath, and Movie Info
            qry = $"SELECT * FROM {HCTable.info} A LEFT JOIN {HCTable.filepath} B ON A.`{HCInfo.Id}`=B.`{HCInfo.Id}` WHERE A.`{HCInfo.Id}`={ID} LIMIT 1;";

            form.BackgroundWorker.DoWork += (sender1, e1) =>
            {
                // Execute queries
                dtInfo = SQLHelper.DbQuery(qry, errFrom);
            };
            form.ShowDialog(this);

            // Get ResultSet for FilePaths
            try
            {
                DataRow row = dtInfo.Rows[0];
                MOVIE_FILEPATH = row[HCFile.File].ToString(); // Get Filepath

                // ID
                lblID.Text = GlobalVars.ValidateAndReturn(row[HCInfo.Id].ToString());

                try { MOVIE_TRAILER = row[HCFile.Trailer].ToString(); }
                catch { MOVIE_TRAILER = ""; }

                // Imdb and Anilist Id
                try
                {
                    string imdb = row[HCInfo.imdb].ToString();
                    string anilist = row[HCInfo.anilist].ToString();
                    if (!String.IsNullOrWhiteSpace(imdb) && !imdb.Equals("0"))
                    {
                        lblSourceId.Text = imdb;
                        lblSourceId.Tag = HCSource.tmdb;
                        lblSource.Text = "IMDB";
                    }
                    else
                    {
                        lblSourceId.Text = anilist;
                        lblSourceId.Tag = HCSource.anilist;
                        lblSource.Text = "Anilist";
                    }
                }
                catch
                {
                    lblSourceId.Tag = "";
                    lblSourceId.Text = "";
                }

                // name
                try { lblName.Text = GlobalVars.ValidateAndReturn(row[HCInfo.name].ToString()); }
                catch
                {
                    try { lblName.Text = Path.GetFileName(MOVIE_FILEPATH); }
                    catch { lblName.Text = ""; }
                }

                // name_ep # Original title from country of Origin
                try { lblNameOrig.Text = GlobalVars.ValidateAndReturn(row[HCInfo.name_orig].ToString()); }
                catch { lblNameOrig.Text = ""; }

                // name_series
                try { lblNameSeries.Text = GlobalVars.ValidateAndReturn(row[HCInfo.name_series].ToString()); }
                catch { lblNameSeries.Text = ""; }

                // season
                try { lblSeasonNum.Text = GlobalVars.ValidateAndReturn(row[HCInfo.season].ToString()); }
                catch { lblSeasonNum.Text = ""; }

                // episode
                try { lblEpNum.Text = GlobalVars.ValidateAndReturn(row[HCInfo.episode].ToString()); }
                catch { lblEpNum.Text = ""; }

                // country
                try { txtCountry.Text = GlobalVars.RemoveLine(row[HCInfo.country].ToString()).Replace(",", ", "); }
                catch { txtCountry.Text = ""; }

                // category
                try { lblCategory.Text = GlobalVars.GetCategory(row[HCInfo.category].ToString()); }
                catch { lblCategory.Text = ""; }

                // genre
                try { txtGenre.Text = GlobalVars.ValidateAndReturn(row[HCInfo.genre].ToString().Replace(",", ", ")); }
                catch { txtGenre.Text = ""; }

                // studio
                try { lblStudio.Text = GlobalVars.ValidateAndReturn(row[HCInfo.studio].ToString()); }
                catch { lblStudio.Text = ""; }

                // producer
                try { lblProducer.Text = GlobalVars.ValidateAndReturn(row[HCInfo.producer].ToString()); }
                catch { lblProducer.Text = ""; }

                // director
                try { lblDirector.Text = GlobalVars.ValidateAndReturn(row[HCInfo.director].ToString()); }
                catch { lblDirector.Text = ""; }

                // artist
                try { txtArtist.Text = GlobalVars.ValidateAndReturn(row[HCInfo.artist].ToString()); }
                catch { txtArtist.Text = ""; }

                // year
                try { lblYear.Text = GlobalVars.ValidateAndReturn(row[HCInfo.year].ToString()); }
                catch { lblYear.Text = ""; }

                // summary
                try { lblSummary.Text = GlobalVars.ValidateAndReturn(row[HCInfo.summary].ToString()); }
                catch { lblSummary.Text = ""; }
            }
            catch
            {
                GlobalVars.ShowWarning("Cannot load movie info!", "", this);
                Close();
                return;
            }
            finally
            {
                // Dispose table
                dtInfo?.Dispose();
            }

            // Get filesize, Create ToolTip for Movie Title
            lblName.Tag = $"{(lblCategory.Text.ToLower().Contains("series") ? "Folder" : "File")} Size: {GlobalVars.GetFileSize(MOVIE_FILEPATH)}";
            tp.SetToolTip(lblName, lblName.Tag.ToString());

            // Change cover image, if error occurs, Dispose form
            MOVIE_COVER = GlobalVars.ImgGetImageFromList(MOVIE_ID);
            
            if (MOVIE_COVER != null)
            {
                picBox.Image = MOVIE_COVER;
                picBox.Refresh();

                // Set FORM ICON
                using (Bitmap thumb = (Bitmap)MOVIE_COVER.GetThumbnailImage(64, 64, null, IntPtr.Zero))
                {
                    Icon = Icon.FromHandle(thumb.GetHicon());
                }

                // Set MOVIE_COVER_FULL | Image from file
                Imagefile = GlobalVars.ImgFullPathWithDefault(lblID.Text);
                try
                {
                    MOVIE_COVER_FULL = Image.FromFile(Imagefile);
                }
                catch (Exception exc)
                {
                    // Error log
                    GlobalVars.ShowError($"{errFrom}\n\tFile:\n\t{ Imagefile }", exc, false);
                }
            }
            // Adjust controls, if not series
            if (!lblCategory.Text.ToLower().Contains("series"))
            {
                lblSeries.Enabled = false;
                lblSeries.Visible = false;
                lblNameSeries.Enabled = false;
                lblNameSeries.Visible = false;
                lblSeason.Enabled = false;
                lblSeason.Visible = false;
                lblEpisode.Enabled = false;
                lblEpisode.Visible = false;
                lblSeasonNum.Enabled = false;
                lblSeasonNum.Visible = false;
                lblEpNum.Enabled = false;
                lblEpNum.Visible = false;

                lblSummaryLbl.Top = lblSeries.Top;
                lblSummary.Top = lblSummaryLbl.Bottom + 2;
                lblSummary.Height += 50;
            }
            // Adjust Trailer Frame
            TrailerFrame();

            // Log changes
            GlobalVars.Log($"{errFrom} ({Name})", "Refreshed the Information!");

            // Set form title and focus
            Text = $"{lblName.Text} ({lblYear.Text})";
            btnPlay.Focus();
        }
        // Dispose Image poster
        public void DisposePoster()
        {
            picBox.Image.Dispose();
            MOVIE_COVER.Dispose();
            MOVIE_COVER_FULL.Dispose();
        }
        // Adjust settings on webDoc
        public void TrailerFrame()
        {
            // Exit if OfflineMOde
            if (Settings.IsOffline)
            {
                ShowImageONWeb(GlobalVars.FILE_NOTRAILER);
                return;
            }
            // Adjust Trailer Box
            webTrailer.ScriptErrorsSuppressed = true;

            // Get url
            string url;
            if (MOVIE_TRAILER.StartsWith("http"))
            {
                // Check internet connection first
                if (GlobalVars.CheckConnection("https://www.youtube.com/") == false)
                {
                    //GlobalVars.ShowWarning("Cannot View Online Trailer!", "No Internet Connection");
                    ShowImageONWeb(GlobalVars.FILE_NOTRAILER);
                    return;
                }
                if (MOVIE_TRAILER.Contains("youtube.com"))
                {
                    url = MOVIE_TRAILER.Substring(MOVIE_TRAILER.IndexOf("?v=") + 3);
                    if (String.IsNullOrWhiteSpace(url) == false)
                    {
                        webTrailer.DocumentText = YoutubeEmbed(url);
                        // Log to file
                        LogWebDoc(url);
                        return;
                    }
                    ShowImageONWeb(GlobalVars.FILE_NOTRAILER);
                }
                else
                {
                    url = MOVIE_TRAILER;
                    webTrailer.Navigate(url);
                }
                return;
            }
            else
            {
                if (String.IsNullOrWhiteSpace(MOVIE_TRAILER))
                {
                    ShowImageONWeb(GlobalVars.FILE_NOTRAILER);
                    return;
                }
                else
                {
                    if (File.Exists(MOVIE_TRAILER))
                    {
                        url = "file://" + MOVIE_TRAILER;
                        webTrailer.DocumentText = WebLocalFile(url);
                        return;
                    }
                    else
                    {
                        ShowImageONWeb(GlobalVars.FILE_NOTRAILER);
                    }
                }
            }
        }
        private void LogWebDoc(string Youtube_ID)
        {
            // Log to file
            GlobalVars.WriteToFile(Path.Combine(GlobalVars.PATH_LOG, "WebTrailerDocText.log"), webTrailer.DocumentText);
            GlobalVars.WriteToFile(Path.Combine(GlobalVars.PATH_LOG, "WebTrailer.log"), YoutubeEmbed(Youtube_ID));
        }
        public string YoutubeEmbed(string code)
        {
            string url = "https://www.youtube.com/embed/" + code + "?rel=0;autoplay=1;loop=1;showinfo=0;controls=0;playlist=" + code + ";listType=playlist;autohide=1;version=3"; // " ?autoplay=1;version=3&amp;rel=0;html5=1"
            var sb = new StringBuilder();
            sb.Append(@"<style>
                iframe {
                position: absolute;
                top: 0;
                left: 0;
                width: 100 %;
                height: 100 %;
                } </style>");
            sb.Append("<html>");
            sb.Append("    <head>");
            sb.Append("        <meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge\"/>");
            sb.Append("    </head>");
            sb.Append("    <body marginheight=\"0\" marginwidth=\"0\" leftmargin=\"0\" topmargin=\"0\" style=\"overflow-y: hidden\">");
            sb.Append($"        <iframe width=\"100%\" height=\"100%\" src=\"{url}\"");
            sb.Append("         frameborder = \"0\" allow = \"autoplay; encrypted-media\" id=\"Overlayvideo\" allowfullscreen></iframe>");
            sb.Append("    </body>");
            sb.Append("</html>");

            return sb.ToString();
        }
        private string WebLocalFile(string sourcelink)
        {
            var sb = new StringBuilder();
            string src;
            if (String.IsNullOrWhiteSpace(sourcelink))
            {
                ShowImageONWeb(GlobalVars.FILE_NOTRAILER);
                return "";
            }
            src = sourcelink;
            sb.Append("<html>");
            sb.Append("    <head>");
            sb.Append("        <meta name=\"viewport\" content=\"width=device-width; height=device-height;\">");
            sb.Append("    </head>");
            sb.Append("    <body marginheight=\"0\" marginwidth=\"0\" leftmargin=\"0\" topmargin=\"0\" style=\"overflow-y: hidden\">");
            sb.Append("        <object width=\"100%\" height=\"100%\">");
            sb.Append("            <param name=\"movie\" value=\"" + src + "\" />");
            sb.Append("            <param name=\"allowFullScreen\" value=\"true\" />");
            sb.Append("            <param name=\"allowscriptaccess\" value=\"always\" />");
            sb.Append("            <embed src=\"" + src + "\" type=\"application/x-shockwave-flash\"");
            sb.Append("                   width=\"100%\" height=\"100%\" allowscriptaccess=\"always\" allowfullscreen=\"true\" />");
            sb.Append("        </object>");
            sb.Append("    </body>");
            sb.Append("</html>");
            return sb.ToString();
        }
        // Display a Fullscreen image
        private void ShowImageONWeb(string src)
        {
            if (File.Exists(GlobalVars.FILE_NOTRAILER))
            {
                if (String.IsNullOrWhiteSpace(src))
                {
                    return;
                }
                var sb = new StringBuilder();
                sb.Append("<html>");
                sb.Append("    <head>");
                sb.Append("        <meta name=\"viewport\" content=\"width=device-width; height=device-height;\">");
                sb.Append("    </head>");
                sb.Append("    <body marginheight=\"0\" marginwidth=\"0\" leftmargin=\"0\" topmargin=\"0\" style=\"overflow-y: hidden\">");
                sb.Append("        <img width=\"100%\" height=\"100%\" src=\"" + src + "\">");
                sb.Append("        </img>");
                sb.Append("    </body>");
                sb.Append("</html>");
                webTrailer.DocumentText = sb.ToString();
                return;
            }
            webTrailer.DocumentText = "<html><body><h1>Unknown Error!</h1></body></html>";
        }
        #endregion
        // ############################################################################## Form Control events
        private void frmMovie_Load(object sender, EventArgs e)
        {
            // Load Information from DB
            LoadInformation(MOVIE_ID);
        }
        private void frmMovie_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Dispose components
            webTrailer?.Dispose();
            picBox.Image?.Dispose();
            MOVIE_COVER_FULL?.Dispose();
            foreach (Control c in groupBox2.Controls)
            {
                c?.Dispose();
            }
            foreach (Control cc in Controls)
            {
                cc?.Dispose();
            }
            // Dispose ICON Image
            Icon?.Dispose();
            // Log to file
            GlobalVars.Log("Disposing frmMovie (" + Name + ")", "Controls are Disposed");

            // Refresh on Main form
            if (IsDeleted)
            {
                Program.FormMain.RefreshMovieList();
            }
            // Set focus to main
            Program.FormMain.Focus();
            Dispose();
        }
        // Play File on Default Player
        private void btnPlay_Click(object sender, EventArgs e)
        {
            // Play Movie file, or browse folder of Series
            GlobalVars.PlayMedia(MOVIE_FILEPATH);
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Create form, or OPEN if already exists
            GlobalVars.OpenFormMovieInfo(this, childForm, MOVIE_ID, MOVIE_NAME, "frmMovie-btnEdit_Click", LVITEM);
        }
        // Click to see Large Image cover
        private void picBox_Click(object sender, EventArgs e)
        {
            try
            {
                // Size of Cover
                int W = 340;
                int H = 511;
                // Show bigger image
                Form temp = new Form();
                temp.Text = lblName.Text + " [Large Image]";
                temp.MaximizeBox = false;
                temp.MinimizeBox = false;
                temp.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                temp.StartPosition = FormStartPosition.Manual;
                temp.Left = Left;
                temp.Top = Top;
                temp.Size = new Size(W + 15, H + 38);
                temp.ShowInTaskbar = false;
                temp.SuspendLayout();
                PictureBox pic = new PictureBox
                {
                    Image = MOVIE_COVER_FULL,
                    Location = new Point(0, 0),
                    Size = new Size(W, H),
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                temp.Controls.Add(pic);
                temp.ResumeLayout();
                temp.Show(this);
            }
            catch (Exception ex)
            {
                GlobalVars.ShowError($"frmMovie({Name.ToString()})-picBox_Click", ex);
            }
        }
        // Delete movie from database
        private void btnDeleteMovie_Click(object sender, EventArgs e)
        {
            string errFrom = $"frmMovie ({Name})-btnDeleteMovie_Click";
            frmLoading form = new frmLoading("Deleting media from disk..", "Loading");
            // Delete Movie WIP
            if (GlobalVars.ShowYesNo($"Are you sure you want to delete\n[{Text}]?", this))
            {
                form.BackgroundWorker.DoWork += (sender1, e1) =>
                {
                    if (SQLHelper.DbDeleteMovie(MOVIE_ID, errFrom))
                    {
                        // Dispose and Delete image
                        if (MOVIE_COVER != null)
                        {
                            DisposePoster();
                            GlobalVars.DeleteImageFromList(form, MOVIE_ID, errFrom);
                            GlobalVars.DeleteMove(GlobalVars.ImgFullPath(MOVIE_ID), errFrom);
                        }
                        // Delete MovieFile from local disk
                        GlobalVars.DeleteMove(MOVIE_FILEPATH, errFrom);

                        // Show message
                        GlobalVars.ShowInfo($"[{Text}] is Deleted!");

                        // Deleted and perform refresh on main form
                        IsDeleted = true;
                    }
                };
                form.ShowDialog(this);
                // Dispose
                Close();
            }
        }
        // Open IMDB link using default browser
        private void lblSourceId_Click(object sender, EventArgs e)
        {
            string titleCode = lblSourceId.Text;
            string source = lblSourceId.Tag?.ToString();
            string link = "";

            if (String.IsNullOrWhiteSpace(source)) { return; }
            link = source.Equals(HCSource.tmdb) ? GlobalVars.LINK_IMDB : GlobalVars.LINK_ANILIST;

            titleCode = titleCode.Trim();
            if ((String.IsNullOrWhiteSpace(titleCode) == false) && (titleCode != "0"))
            {
                try
                {
                    Process.Start(link + titleCode);
                }
                catch (Exception ex)
                {
                    GlobalVars.ShowError($"frmMovie({Name})-lblSourceId_Click", ex, false);
                    GlobalVars.ShowWarning("Cannot open link on browser!");
                }
            }
        }
    }
}
