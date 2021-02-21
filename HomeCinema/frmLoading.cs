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
    public partial class frmLoading : Form
    {
        public string Caption
        {
            get { return this.Text; }
            set { this.Text = value; }
        }
        public string Message
        {
            get { return label1.Text; }
            set
            {
                if (label1.InvokeRequired)
                {
                    BeginInvoke((Action)delegate
                    {
                        label1.Text = value;
                    });
                }
                else
                    label1.Text = value;
            }
        }
        public bool isCanceled { get; set; }
        public int TopPosition { get; set; }
        public frmLoading(string message, string caption)
        {
            InitializeComponent();
            Icon = GlobalVars.HOMECINEMA_ICON;
            Message = message;
            Caption = caption;
            isCanceled = false;
            TopPosition = 0;
            CenterToParent();
        }

        private void frmPopulateMovie_Shown(object sender, EventArgs e)
        {
            if (BackgroundWorker.IsBusy)
                return;
            BackgroundWorker.RunWorkerAsync();
            if (TopPosition != 0)
                this.Top = TopPosition;
        }

        private void frmPopulateMovie_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (BackgroundWorker.IsBusy)
                e.Cancel = true;
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            isCanceled = true;
        }

        private void frmPopulateMovie_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    escapeButton.PerformClick();
                    break;
            }
        }
    }
}
