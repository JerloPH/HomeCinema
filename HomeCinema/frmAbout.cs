using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeCinema
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
            // Form properties
            BackColor = Settings.ColorBg;
            ForeColor = Settings.ColorFont;
            // Background Color
            txtTitle.BackColor = Settings.ColorBg;
            txtLicense.BackColor = Settings.ColorBg;
            // Fore Color
            txtTitle.ForeColor = Settings.ColorFont;
            txtLicense.ForeColor = Settings.ColorFont;
            // events
            webHC.Navigating += new WebBrowserNavigatingEventHandler(webHC_Navigating);
            // Focus
            Focus();
        }
        // ######################################################### Events
        private void frmAbout_Load(object sender, EventArgs e)
        {
            try
            {
                string version = GlobalVars.ReadStringFromFile(Path.Combine(DataFile.PATH_START, "VERSION_HISTORY.md"), "frmAbout_Load");
                string style = $"body {{ background-color: {ColorTranslator.ToHtml(Settings.ColorBg)};" +
                    $"color: {ColorTranslator.ToHtml(Settings.ColorFont)} }}";
                var html = $"<html><head><style>{style}</style></head>{Markdig.Markdown.ToHtml($"{version}")}</body></html>";
                webHC.DocumentText = html;
                Logs.Debug(html);
            }
            catch (Exception ex)
            {
                Logs.LogErr("frmAbout_Load", ex);
            }
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
                //cancel the current event
                e.Cancel = true;
                //this opens the URL in the user's default browser
                try { System.Diagnostics.Process.Start(e.Url.ToString()); }
                catch (Exception ex)
                {
                    Logs.LogErr("webHC_Navigating", ex);
                }
            }
        }
        private void btnCheckUpdate_Click(object sender, EventArgs e)
        {
            GlobalVars.CheckForUpdate(this, true);
        }
    }
}
