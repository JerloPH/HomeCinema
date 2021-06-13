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
            Icon = GlobalVars.HOMECINEMA_ICON;
            BackColor = GlobalVars.SET_COLOR_BG;
            ForeColor = GlobalVars.SET_COLOR_FONT;

            txtTitle.BackColor = GlobalVars.SET_COLOR_BG;
            txtLicense.BackColor = GlobalVars.SET_COLOR_BG;

            txtTitle.ForeColor = GlobalVars.SET_COLOR_FONT;
            txtLicense.ForeColor = GlobalVars.SET_COLOR_FONT;

            Focus();
        }
        // ######################################################### Methods
        // ######################################################### Events
        private void frmAbout_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Closed
            GlobalVars.formAbout = null;
            Dispose();
        }
    }
}
