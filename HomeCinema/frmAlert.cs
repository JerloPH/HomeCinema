using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HomeCinema.Global;

namespace HomeCinema
{
    public partial class frmAlert : Form
    {
        public frmAlert(string message, string caption)
        {
            InitializeComponent();
            Icon = GlobalVars.HOMECINEMA_ICON;
            BackColor = GlobalVars.SET_COLOR_BG;
            ForeColor = GlobalVars.SET_COLOR_FONT;
            Text = String.IsNullOrWhiteSpace(caption) ? GlobalVars.CAPTION_DIALOG : caption;
            TopMost = true;
            //TopLevel = false;
            //Parent = (Form)Program.FormMain;
            CenterToParent();

            lblMessage.Text = message;
            lblMessage.Left = ((this.Width - lblMessage.Width) / 2) - 8;
            Height = lblMessage.Bottom + btnOk.Height + 64;

            btnOk.Top = this.Height - (btnOk.Height + 48);
            //btnOk.DialogResult = DialogResult.OK;
            btnOk.ForeColor = Color.Black;
        }

        private void frmAlert_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dispose();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
