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
            // Focus
            Focus();
        }
        // ######################################################### Events
        private void frmAbout_Load(object sender, EventArgs e)
        {
            string line = Environment.NewLine;
            string header = $"{GlobalVars.HOMECINEMA_NAME}{line}v{GlobalVars.HOMECINEMA_VERSION} build {GlobalVars.HOMECINEMA_BUILD}";
            string version = GlobalVars.ReadStringFromFile(Path.Combine(DataFile.PATH_START, "VERSION_HISTORY.md"), "frmAbout_Load");
            txtHomeCinema.Text = $"{header}{line}{version}";
        }
        private void frmAbout_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalVars.formAbout = null;
            Dispose();
        }
        private void frmAbout_Resize(object sender, EventArgs e)
        {
            var rect = this.ClientRectangle;
            int adj = 12, adj2 = 18, adj3 = 50;
            tabMain.Width = rect.Width - adj2;
            tabMain.Height = rect.Height - adj - tabMain.Top;
            btnCheckUpdate.Left = tabMain.Right - (adj3 + btnCheckUpdate.Width);
        }

        private void btnCheckUpdate_Click(object sender, EventArgs e)
        {
            GlobalVars.CheckForUpdate(this, true);
        }
    }
}
