using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        // ######################################################### Methods
        // ######################################################### Events
        private void frmAbout_Load(object sender, EventArgs e)
        {
            string line = Environment.NewLine;
            txtHomeCinema.Text = $"{GlobalVars.HOMECINEMA_NAME}{line}v{GlobalVars.HOMECINEMA_VERSION} build {GlobalVars.HOMECINEMA_BUILD}";
        }
        private void frmAbout_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalVars.formAbout = null;
            Dispose();
        }
        private void frmAbout_Resize(object sender, EventArgs e)
        {
            var rect = this.ClientRectangle;
            int adj = 12, adj2 = 18, adj3 = 32;
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
