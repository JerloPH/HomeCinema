﻿using System;
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
        public frmAlert()
        {
            InitializeComponent();
        }
        public frmAlert(string message, string caption, int button = 0)
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

            // Adjust form and lblMessage, dynamically
            lblMessage.Text = message;
            lblMessage.Left = ((this.Width - lblMessage.Width) / 2) - 8;
            Height = lblMessage.Bottom + btnOk.Height + 64;
            var centerForm = Width / 2; // center point of form

            // Disable all buttons first
            btnOk.Enabled = btnOk.Visible = false;
            btnYes.Enabled = btnYes.Visible = false;
            btnNo.Enabled = btnNo.Visible = false;

            // Switch type of prompt message
            switch (button)
            {
                case 1: // Yes, No
                    btnYes.Enabled = btnYes.Visible = true;
                    btnNo.Enabled = btnNo.Visible = true;
                    btnYes.Top = this.Height - (btnYes.Height + 48);
                    btnNo.Top = btnYes.Top;
                    btnYes.Left = centerForm - (int)(btnYes.Width * 1.5);
                    btnNo.Left = centerForm + (btnNo.Width/2);
                    btnYes.DialogResult = DialogResult.Yes;
                    btnNo.DialogResult = DialogResult.No;
                    btnYes.ForeColor = Color.Black;
                    btnNo.ForeColor = Color.Black;
                    break;
                default: // Simple messagebox
                    btnOk.Enabled = btnOk.Visible = true;
                    btnOk.Top = this.Height - (btnOk.Height + 48);
                    btnOk.Left = centerForm - (btnOk.Width / 2) - 10;
                    btnOk.ForeColor = Color.Black;
                    break;
            }
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
