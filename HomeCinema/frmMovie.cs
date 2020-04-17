using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using HomeCinema.Global;
using HomeCinema.SQLFunc;
using Microsoft.WindowsAPICodePack.Shell;

namespace HomeCinema
{
    public partial class frmMovie : Form
    {
        public static string childForm { get; set; } = "";
        public static string MOVIE_ID { get; set; } = "";
        public static string MOVIE_NAME { get; set; } = "";
        public static string MOVIE_FILEPATH { get; set; } = "";
        public static string MOVIE_SUB { get; set; } = "";
        public static string MOVIE_TRAILER { get; set; } = "";
        public static Image MOVIE_COVER { get; set; } = null;
        // SQLHelper connection
        SQLHelper conn = new SQLHelper();
        public frmMovie(Form parent, string ID, string name)
        {
            InitializeComponent();

            // Form properties
            FormClosing += new FormClosingEventHandler(frmMovie_FormClosing);
            Icon = new Icon(GlobalVars.FILE_ICON);

            // Assign values to vars
            MOVIE_ID = ID;
            MOVIE_NAME = name;
            childForm = GlobalVars.PREFIX_MOVIEINFO + MOVIE_ID;

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
            //StartPosition = FormStartPosition.Manual;
            StartPosition = FormStartPosition.CenterParent;
            Show(parent);

            // Show this to other monitor
            //GlobalVars.MaximizeToMonitor(this, 1);

            // Adjust Trailer Frame
            TrailerFrame();

            // Set main focus at startup
            btnPlay.Focus();
        }
        // ############################################################################## Functions
        public void LoadInformation(string ID)
        {
            // Set textbox values from Database
            string cols = null;

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
            DataTable dtFile = conn.DbQuery(qry, cols);
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
            DataTable dtInfo = conn.DbQuery(qry, cols);
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
            /*
            if (GlobalVars.GetIndexfromList(MOVIE_ID) < 1)
            {
                // Get Image thumbnail and set as cover
                ShellFile shellFile = ShellFile.FromFilePath(MOVIE_FILEPATH);
                Bitmap bm = shellFile.Thumbnail.Bitmap;
                bm.Save(GlobalVars.PATH_START + @"thumbnails\" + MOVIE_ID + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                MOVIE_COVER = bm;
                shellFile.Dispose();
            }
            */
            if (MOVIE_COVER is null)
            {
                GlobalVars.Log($"frmMovie-({Name})-LoadInformation", "Setting Image Cover error");
                Close();
            }
            picBox.Image = MOVIE_COVER;

            // Log changes
            GlobalVars.Log($"frmMovie-LoadInformation ({Name})", "Refreshed the Information!");

            // Set form title and focus
            Text = $"{lblName.Text} ({lblYear.Text})";
            btnPlay.Focus();
        }
        public void TrailerFrame()
        {
            // Adjust Trailer Box
            webTrailer.ScriptErrorsSuppressed = false;

            // Get url
            string url;
            if (MOVIE_TRAILER.StartsWith("http"))
            {
                // Check internet connection first
                if (GlobalVars.CheckConnection("https://www.youtube.com/") == false)
                {
                    GlobalVars.ShowWarning("Cannot View Online Trailer!", "No Internet Connection");
                    return;
                }
                if (MOVIE_TRAILER.StartsWith("https://www.youtube.com/watch?v="))
                {
                    url = MOVIE_TRAILER.Replace("https://www.youtube.com/watch?v=", String.Empty);
                    //webTrailer.DocumentText = GetYouTubeVideoPlayerHTML(url);
                    webTrailer.DocumentText = YoutubeEmbed(url);
                    // Log to file
                    GlobalVars.WriteToFile(GlobalVars.PATH_START + "WebTrailerDocText.log", webTrailer.DocumentText.ToString());
                    GlobalVars.WriteToFile(GlobalVars.PATH_START + "WebTrailer.log", YoutubeEmbed(url));
                }
                else
                {
                    url = MOVIE_TRAILER;
                    //webTrailer.DocumentText = GetPlayerHTML(url);
                    webTrailer.Navigate(url);
                }
                return;
            }
            else
            {
                if (String.IsNullOrWhiteSpace(MOVIE_TRAILER))
                {
                    if (File.Exists(GlobalVars.FILE_NOTRAILER))
                    {
                        webTrailer.DocumentText = ShowImageONWeb(GlobalVars.FILE_NOTRAILER);
                    }
                    return;
                }
                else
                {
                    if (File.Exists(MOVIE_TRAILER))
                    {
                        url = "file://" + MOVIE_TRAILER;
                        webTrailer.DocumentText = GetPlayerHTML(url);
                        return;
                    }
                    else
                    {
                        if (File.Exists(GlobalVars.FILE_NOTRAILER))
                        {
                            webTrailer.DocumentText = ShowImageONWeb(GlobalVars.FILE_NOTRAILER);
                        }
                        return;
                    }
                }
            }
        }
        private static string GetPlayerHTML(string sourcelink)
        {
            return GetPlayerHTML(sourcelink, "");
        }
        public string YoutubeEmbed(string code)
        {
            string url = "https://www.youtube.com/embed/" + code + "?rel=0"; // " ?autoplay=1;version=3&amp;rel=0;html5=1"
            var sb = new StringBuilder();
            sb.Append("<html>");
            sb.Append("    <head>");
            sb.Append("        <meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge\"/>");
            sb.Append("    </head>");
            sb.Append("    <body marginheight=\"0\" marginwidth=\"0\" leftmargin=\"0\" topmargin=\"0\" style=\"overflow-y: hidden\">");
            sb.Append($"        <iframe width=\"100%\" height=\"100%\" src=\"{url}\"");
            sb.Append("         frameborder = \"0\" allow = \"autoplay; encrypted-media\" allowfullscreen></iframe>");
            sb.Append("    </body>");
            sb.Append("</html>");

            return sb.ToString();
        }
        private static string GetPlayerHTML(string sourcelink, string videoCode)
        {
            var sb = new StringBuilder();

            //const string YOUTUBE_URL = @"https://www.youtube.com/watch?v=";
            const string YOUTUBE_URL = @"https://www.youtube.com/embed/";
            //const string YOUTUBE_URL = @"http://www.youtube.com/v/";

            string src;
            if (String.IsNullOrWhiteSpace(videoCode))
            {
                src = sourcelink;
            }
            else
            {
                src = YOUTUBE_URL + videoCode + "?autoplay=1;version=3&amp;rel=0";
            }

            sb.Append("<html>");
            sb.Append("    <head>");
            //sb.Append("        <meta http-equiv =\"X-UA-Compatible\" content=\"IE=Edge\"/>");
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
        private static string ShowImageONWeb(string src)
        {
            if (String.IsNullOrWhiteSpace(src))
            {
                return "";
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
            return sb.ToString();

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
            foreach (Control c in groupBox2.Controls)
            {
                c.Dispose();
            }
            foreach (Control cc in Controls)
            {
                cc.Dispose();
            }
            GlobalVars.Log("Disposing frmMovie (" + Name + ")", "Controls are Disposed");
            // Run GC to clean
            GlobalVars.CleanMemory();
            Dispose();
        }
        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (File.Exists(MOVIE_FILEPATH))
            {
                Process.Start(MOVIE_FILEPATH);
            }
            else
            {
                GlobalVars.ShowWarning("File not Found! \nIt may have been Moved or Deleted!", "File not Found!");
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Create form, or OPEN if already exists
            GlobalVars.OpenFormMovieInfo(this, childForm, MOVIE_ID, MOVIE_NAME, "frmMovie-btnEdit_Click");
        }
        // Click to see Large Image cover
        private void picBox_Click(object sender, EventArgs e)
        {
            // Show bigger image
            Form temp = new Form();
            temp.Text = lblName.Text + " [Large Image]";
            temp.MaximizeBox = false;
            temp.MinimizeBox = false;
            temp.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            temp.StartPosition = FormStartPosition.CenterParent;
            temp.StartPosition = FormStartPosition.Manual;
            temp.Left = Left;
            temp.Top = Top;
            temp.Size = new Size(350, 520);
            temp.ShowInTaskbar = false;
            temp.SuspendLayout();
            PictureBox pic = new PictureBox
            {
                Image = picBox.Image,
                Location = new Point(0, 2),
                Size = new Size(350, 480),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            temp.Controls.Add(pic);
            temp.ResumeLayout();
            temp.Show(this);
            temp.StartPosition = FormStartPosition.CenterParent;
            //temp.Left = this.Left - (this.Width/2) + (temp.Width/2);
            //temp.Top = this.Top - (this.Height / 2) + (temp.Height / 2);
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
                    // Delete image
                    if (MOVIE_COVER != null)
                    {
                        MOVIE_COVER.Dispose();
                        GlobalVars.TryDelete(GlobalVars.GetPicPath(MOVIE_ID), errFrom);
                    }
                    // Perform Click on btnSearch
                    frmMain master = (frmMain)Application.OpenForms["frmMain"];
                    master.btnSearch.PerformClick();
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
            Process.Start("https://www.imdb.com/title/" + lblIMDB.Text);
        }
    }
}
