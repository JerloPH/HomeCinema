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
            qry = $"SELECT * FROM {GlobalVars.DB_TNAME_INFO} A LEFT JOIN {GlobalVars.DB_TNAME_FILEPATH} B ON A.`Id`=B.`Id` WHERE A.`Id`={ID} LIMIT 1;";

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
                MOVIE_FILEPATH = row[HCFile.file.ToString()].ToString(); // Get Filepath

                var r0 = row[HCInfo.Id.ToString()]; // ID
                lblID.Text = GlobalVars.ValidateAndReturn(r0.ToString());

                try { MOVIE_TRAILER = row[HCFile.trailer.ToString()].ToString(); }
                catch { MOVIE_TRAILER = ""; }

                try
                {
                    var r1 = row[HCInfo.imdb.ToString()]; // imdb
                    lblIMDB.Text = GlobalVars.ValidateAndReturn(r1.ToString());
                }
                catch { lblIMDB.Text = ""; }
                try
                {
                    var r2 = row[HCInfo.name.ToString()]; // name
                    lblName.Text = GlobalVars.ValidateAndReturn(r2.ToString());
                }
                catch
                {
                    try
                    {
                        lblName.Text = Path.GetFileName(MOVIE_FILEPATH);
                    }
                    catch { lblName.Text = ""; }
                }
                try
                {
                    var r3 = row[HCInfo.name_ep.ToString()]; // name_ep # Original title from country of Origin
                    lblNameEp.Text = GlobalVars.ValidateAndReturn(r3.ToString());
                }
                catch { lblNameEp.Text = ""; }
                try
                {
                    var r4 = row[HCInfo.name_series.ToString()]; // name_series
                    lblNameSeries.Text = GlobalVars.ValidateAndReturn(r4.ToString());
                }
                catch { lblNameSeries.Text = ""; }
                try
                {
                    var r5 = row[HCInfo.season.ToString()]; // season
                    lblSeasonNum.Text = GlobalVars.ValidateAndReturn(r5.ToString());
                }
                catch { lblSeasonNum.Text = ""; }
                try
                {
                    var r6 = row[HCInfo.episode.ToString()]; // episode
                    lblEpNum.Text = GlobalVars.ValidateAndReturn(r6.ToString());
                }
                catch { lblEpNum.Text = ""; }
                try
                {
                    var r7 = row[HCInfo.country.ToString()]; // country
                    txtCountry.Text = GlobalVars.RemoveLine(r7.ToString()).Replace(",", ", ");
                }
                catch { txtCountry.Text = ""; }
                try
                {
                    var r8 = row[HCInfo.category.ToString()]; // category
                    lblCategory.Text = GlobalVars.GetCategory(r8.ToString());
                }
                catch { lblCategory.Text = ""; }
                try
                {
                    var r9 = row[HCInfo.genre.ToString()]; // genre
                    txtGenre.Text = GlobalVars.ValidateAndReturn(r9.ToString().Replace(",", ", "));
                }
                catch { txtGenre.Text = ""; }
                try
                {
                    var r10 = row[HCInfo.studio.ToString()]; // studio
                    lblStudio.Text = GlobalVars.ValidateAndReturn(r10.ToString());
                }
                catch { lblStudio.Text = ""; }
                try
                {
                    var r11 = row[HCInfo.producer.ToString()]; // producer
                    lblProducer.Text = GlobalVars.ValidateAndReturn(r11.ToString());
                }
                catch { lblProducer.Text = ""; }
                try
                {
                    var r12 = row[HCInfo.director.ToString()]; // director
                    lblDirector.Text = GlobalVars.ValidateAndReturn(r12.ToString());
                }
                catch { lblDirector.Text = ""; }
                try
                {
                    var r13 = row[HCInfo.artist.ToString()]; // artist
                    txtArtist.Text = GlobalVars.ValidateAndReturn(r13.ToString());
                }
                catch { txtArtist.Text = ""; }
                try
                {
                    var r14 = row[HCInfo.year.ToString()]; // year
                    lblYear.Text = GlobalVars.ValidateAndReturn(r14.ToString());
                }
                catch { lblYear.Text = ""; }
                try
                {
                    var r15 = row[HCInfo.summary.ToString()]; // summary
                    lblSummary.Text = GlobalVars.ValidateAndReturn(r15.ToString());
                }
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

            // Adjust Trailer Frame
            TrailerFrame();

            // Log changes
            GlobalVars.Log($"{errFrom} ({Name})", "Refreshed the Information!");

            // Set form title and focus
            Text = $"{lblName.Text} ({lblYear.Text})";
            btnPlay.Focus();
        }
        // Dispose Image poster
        public void DisposePoster(string none)
        {
            picBox.Image.Dispose();
            MOVIE_COVER.Dispose();
            MOVIE_COVER_FULL.Dispose();
        }
        // Adjust settings on webDoc
        public void TrailerFrame()
        {
            // Exit if OfflineMOde
            if (GlobalVars.SET_OFFLINE)
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
                            DisposePoster("");
                            GlobalVars.DeleteImageFromList(this, MOVIE_ID, errFrom);
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
        private void lblIMDB_Click(object sender, EventArgs e)
        {
            string titleCode = lblIMDB.Text;
            titleCode = titleCode.Trim();
            if ((String.IsNullOrWhiteSpace(titleCode) == false) && (titleCode != "0"))
            {
                try
                {
                    Process.Start(GlobalVars.LINK_IMDB + titleCode);
                }
                catch (Exception ex)
                {
                    GlobalVars.ShowError($"frmMovie({Name})-lblIMDB_Click", ex, false);
                    GlobalVars.ShowWarning("Cannot open IMDB link on browser!");
                }
            }
        }
    }
}
