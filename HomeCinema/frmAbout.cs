using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Markdig;

namespace HomeCinema
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
            Themes.SetThemeParent(this, new List<Control>() { txtTitle, txtLicense });
            // events
            webHC.Navigating += new WebBrowserNavigatingEventHandler(webHC_Navigating);
            // Focus
            Focus();
        }
        // ######################################################### Events
        private async void frmAbout_Load(object sender, EventArgs e)
        {
            try
            {
                await Task.Run(delegate
                {
                    string version = GlobalVars.ReadStringFromFile(Path.Combine(DataFile.PATH_START, "VERSION_HISTORY.md"), "frmAbout_Load");
                    string style = $"body {{ background-color: {ColorTranslator.ToHtml(Settings.ColorBg)};" +
                        $"color: {ColorTranslator.ToHtml(Settings.ColorFont)} }}";
                    if (String.IsNullOrWhiteSpace(version))
                        version = "**Version History file is missing!**";

                    var html = $"<html><head><style>{style}</style></head>{Markdown.ToHtml($"{version}")}</body></html>";
                    this.BeginInvoke((Action)delegate { webHC.DocumentText = html; });
                });
            }
            catch (Exception ex) { Logs.LogErr("frmAbout_Load", ex); }
        }
        private void frmAbout_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalVars.formAbout = null;
            webHC?.Dispose();
            GlobalVars.Refocus(this);
            Dispose();
        }
        private void frmAbout_Resize(object sender, EventArgs e)
        {
            var rect = this.ClientRectangle;
            int adj = 12, adj2 = 18, adj3 = 50;
            tabMain.Width = rect.Width - adj2;
            tabMain.Height = rect.Height - adj - tabMain.Top;
            btnCheckUpdate.Left = tabMain.Right - (adj3 + btnCheckUpdate.Width);
            picTmdb.Left = tabMain.Right - (adj3 + picTmdb.Width);
        }
        public void webHC_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (!(e.Url.ToString().Equals("about:blank", StringComparison.InvariantCultureIgnoreCase)))
            {
                e.Cancel = true; //cancel the current event
                //this opens the URL in the user's default browser
                try { System.Diagnostics.Process.Start(e.Url.ToString()); }
                catch (Exception ex) { Logs.LogErr("webHC_Navigating", ex); }
            }
        }
        private void btnCheckUpdate_Click(object sender, EventArgs e)
        {
            GlobalVars.CheckForUpdate(this, true);
        }
    }
}
