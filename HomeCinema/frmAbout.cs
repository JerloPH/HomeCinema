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

            txtTitle.BackColor = Settings.ColorBg;
            txtLicense.BackColor = Settings.ColorBg;

            txtTitle.ForeColor = Settings.ColorFont;
            txtLicense.ForeColor = Settings.ColorFont;

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
