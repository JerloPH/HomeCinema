using HomeCinema.Global;
using Microsoft.Build.Evaluation;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HomeCinema
{
    public partial class frmLoading : Form
    {
        public frmLoading()
        {
            InitializeComponent();
            Icon = new Icon(GlobalVars.FILE_ICON);
            FormClosing += new FormClosingEventHandler(frmLoading_FormClosing);
        }

        private void frmLoading_Load(object sender, EventArgs e)
        {
            // at form load
            string file = GlobalVars.PATH_RES + "Loading.gif";
            picBox.Image = Image.FromFile(file);
        }

        // Dispose Image loading
        private void frmLoading_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (picBox.Image != null)
            {
                picBox.Image.Dispose();
            }
            foreach (Control c in Controls)
            {
                c.Dispose();
            }
            //Dispose();
        }

        private void timeClose_Tick(object sender, EventArgs e)
        {
            Form fc = Application.OpenForms["frmMain"];
            if (fc == null)
            {
                Close();
            }
        }
    }
}
