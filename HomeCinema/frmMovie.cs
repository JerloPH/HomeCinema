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
using HomeCinema.Global;
using HomeCinema.SQLFunc;

namespace HomeCinema
{
    public partial class frmMovie : Form
    {
        private static string childForm { get; set; } = "";
        private static string MOVIE_ID { get; set; } = "";
        private static string MOVIE_NAME { get; set; } = "";
        private static string MOVIE_FILEPATH { get; set; } = "";
        private static string MOVIE_SUB { get; set; } = "";
        private static string MOVIE_TRAILER { get; set; } = "";
        private static Image MOVIE_COVER { get; set; } = null;
        private static Image MOVIE_COVER_FULL { get; set; } = null;

        // Source ListView lvSearch Item index
        public ListViewItem LVITEM = null;

        // SQLHelper connection
        SQLHelper conn = new SQLHelper("frmMovie");

        public frmMovie(Form parent, string ID, string name, ListViewItem lvitem)
        {
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

            groupBox2.BackColor = Color.Transparent;
            foreach (Control control in groupBox2.Controls)
            {
                if (control.Name == "groupBox3")
                {
                    control.BackColor = Color.Transparent;
                }
            }

            // Load Information from DB
            LoadInformation(ID);

            // Show this form
            StartPosition = FormStartPosition.CenterParent;
            Show(parent);

            // Show this to other monitor
            //GlobalVars.MaximizeToMonitor(this, 1);

            // Set main focus at startup
            btnPlay.Focus();
        }
        // ############################################################################## Functions
        public void LoadInformation(string ID)
        {
            // Set textbox values from Database
            string cols = "";

            // get filepath FROM DB
            foreach (string s in GlobalVars.DB_TABLE_FILEPATH)
            {
                cols += "[" + s + "],";
            }
            cols = cols.TrimEnd(',');
            // Build query for FilePath
            string qry = $"SELECT {cols} FROM {GlobalVars.DB_TNAME_FILEPATH} WHERE Id={ID} LIMIT 1;";
            GlobalVars.Log($"frmMovie-frmMovie Table({GlobalVars.DB_TNAME_FILEPATH})", $"Query src: {qry}");
            // Execute query
            DataTable dtFile = conn.DbQuery(qry, cols, "frmMovie-LoadInformation");
            // Get result in DT
            foreach (DataRow row in dtFile.Rows)
            {
                MOVIE_FILEPATH = row[GlobalVars.DB_TABLE_FILEPATH[1]].ToString();
                MOVIE_SUB = row[GlobalVars.DB_TABLE_FILEPATH[2]].ToString();
                MOVIE_TRAILER = row[GlobalVars.DB_TABLE_FILEPATH[3]].ToString();
            }
            dtFile.Clear();
            dtFile.Dispose();

            // get Info FROM DB
            cols = "";
            foreach (string c in GlobalVars.DB_TABLE_INFO)
            {
                cols += "[" + c + "],";
            }
            cols = cols.TrimEnd(',');
            // Build query for INFO
            qry = $"SELECT {cols} FROM {GlobalVars.DB_TNAME_INFO} WHERE Id={ID} LIMIT 1;";
            GlobalVars.Log("frmMovieInfo-frmMovieInfo (Info)", $"Query src: {qry}");
            // Exxecute query
            DataTable dtInfo = conn.DbQuery(qry, cols, "frmMovie-LoadInformation");
            // Get result in DT
            foreach (DataRow row in dtInfo.Rows)
            {
                var r0 = row[0]; // ID
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
                lblID.Text = GlobalVars.ValidateAndReturn(r0.ToString());
                lblIMDB.Text = GlobalVars.ValidateAndReturn(r1.ToString());
                lblName.Text = GlobalVars.ValidateAndReturn(r2.ToString());
                lblNameEp.Text = GlobalVars.ValidateAndReturn(r3.ToString());
                lblNameSeries.Text = GlobalVars.ValidateAndReturn(r4.ToString());
                lblSeasonNum.Text = GlobalVars.ValidateAndReturn(r5.ToString());
                lblEpNum.Text = GlobalVars.ValidateAndReturn(r6.ToString());
                txtCountry.Text = GlobalVars.RemoveLine(r7.ToString()).Replace(",", ", ");
                lblCategory.Text = GlobalVars.GetCategory(r8.ToString());
                txtGenre.Text = GlobalVars.ValidateAndReturn(r9.ToString().Replace(",", ", "));
                lblStudio.Text = GlobalVars.ValidateAndReturn(r10.ToString());
                lblProducer.Text = GlobalVars.ValidateAndReturn(r11.ToString());
                lblDirector.Text = GlobalVars.ValidateAndReturn(r12.ToString());
                txtArtist.Text = GlobalVars.ValidateAndReturn(r13.ToString());
                lblYear.Text = GlobalVars.ValidateAndReturn(r14.ToString());
                lblSummary.Text = GlobalVars.ValidateAndReturn(r15.ToString());
            }
            // Dispose table
            dtInfo.Clear();
            dtInfo.Dispose();

            // Change cover image, if error occurs, Dispose form
            MOVIE_COVER = GlobalVars.GetImageFromList(MOVIE_ID);
            
            if (MOVIE_COVER is null || MOVIE_COVER == null)
            {
                GlobalVars.Log($"frmMovie-({Name})-LoadInformation", "Setting Image Cover error");
                Close();
            }
            picBox.Image = MOVIE_COVER;
            picBox.Refresh();

            // Set FORM ICON
            Bitmap thumb = (Bitmap)MOVIE_COVER.GetThumbnailImage(64, 64, null, IntPtr.Zero);
            Icon = Icon.FromHandle(thumb.GetHicon());
            thumb.Dispose();

            // Set MOVIE_COVER_FULL | Image from file
            string Imagefile = GlobalVars.GetPicValid(lblID.Text);
            try
            {
                MOVIE_COVER_FULL = Image.FromFile(Imagefile);
            } catch (Exception exc)
            {
                // Error log
                GlobalVars.Log("frmMovie-LoadInformation", $"File:\n{ Imagefile }\nError:\n{ exc.ToString() }");
            }

            // Adjust Trailer Frame
            TrailerFrame();

            // Log changes
            GlobalVars.Log($"frmMovie-LoadInformation ({Name})", "Refreshed the Information!");

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
            GlobalVars.WriteToFile(GlobalVars.PATH_START + "WebTrailerDocText.log", webTrailer.DocumentText);
            GlobalVars.WriteToFile(GlobalVars.PATH_START + "WebTrailer.log", YoutubeEmbed(Youtube_ID));
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
        // ############################################################################## Form Control events
        private void frmMovie_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Set focus to main
            Program.FormMain.Focus();

            // Dispose components
            webTrailer.Dispose();
            if (picBox.Image != null)
            {
                picBox.Image.Dispose();
            }
            if (MOVIE_COVER_FULL != null)
            {
                MOVIE_COVER_FULL.Dispose();
            }
            foreach (Control c in groupBox2.Controls)
            {
                c.Dispose();
            }
            foreach (Control cc in Controls)
            {
                cc.Dispose();
            }
            // Dispose ICON Image
            Icon.Dispose();
            // Log to file
            GlobalVars.Log("Disposing frmMovie (" + Name + ")", "Controls are Disposed");
            // Run GC to clean
            GlobalVars.CleanMemory("frmMovie_FormClosing");
            Dispose();
        }
        // Play File on Default Player
        private void btnPlay_Click(object sender, EventArgs e)
        {
            // Directory of Series
            if (lblCategory.Text.Contains("Series"))
            {
                return;
            }
            // Single movie file
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
            } catch (Exception ex)
            {
                // Log Error
                GlobalVars.ShowError($"frmMovie({Name.ToString()}-picBox_Click-[{ex.Source.ToString()}])", ex.ToString());
            }
        }
        // Delete movie from database
        private void btnDeleteMovie_Click(object sender, EventArgs e)
        {
            string errFrom = $"frmMovie ({Name})-btnDeleteMovie_Click";
            // Delete Movie WIP
            if (GlobalVars.ShowYesNo($"Are you sure you want to Delete [{Text}]?"))
            {
                if (conn.DbDeleteMovie(MOVIE_ID, errFrom))
                {
                    // Dispose and Delete image
                    if (MOVIE_COVER != null)
                    {
                        DisposePoster("");
                        GlobalVars.DeleteImageFromList(MOVIE_ID, errFrom);
                        GlobalVars.TryDelete(GlobalVars.GetPicPath(MOVIE_ID), errFrom);
                    }
                    // Delete MovieFile from local disk
                    GlobalVars.TryDelete(MOVIE_FILEPATH, errFrom);

                    // Refresh movie list
                    frmMain master = (frmMain)Application.OpenForms["frmMain"];
                    master.RefreshMovieList();

                    // Show message
                    GlobalVars.ShowInfo($"[{Text}] is Deleted!");
                    // Dispose
                    Close();
                }
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
                } catch (Exception ex)
                {
                    // Log Error
                    GlobalVars.ShowError($"frmMovie({Name.ToString()}-lblIMDB_Click-[{ex.Source.ToString()}])", ex.ToString());
                }
            }
        }
    }
}
