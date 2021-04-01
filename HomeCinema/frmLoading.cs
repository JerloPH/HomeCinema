﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HomeCinema.Global;
using HomeCinema.Properties;

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
        public int TopPosition { get; set; }
        public frmLoading(string message, string caption)
        {
            InitializeComponent();
            Icon = GlobalVars.HOMECINEMA_ICON;
            Message = message;
            Caption = caption;
            TopPosition = 0;
            CenterToParent();
        }

        public void SetIcon(int IconIndex = 0)
        {
            Bitmap image = null;
            try
            {
                switch (IconIndex)
                {
                    case (int)GlobalVars.Icons.Check:
                        image = Resources.IconCheckmark;
                        break;
                    case (int)GlobalVars.Icons.Warning:
                        image = Resources.IconWarning;
                        break;
                    default:
                        image = Resources.LoadingColored;
                        break;
                }
                pictureBox1.Image.Dispose();
                pictureBox1.Image = image;
            }
            catch { }
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

            pictureBox1.Image.Dispose();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Dispose();
        }
    }
}
