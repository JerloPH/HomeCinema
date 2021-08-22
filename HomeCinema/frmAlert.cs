using HomeCinema.Properties;
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
    public partial class frmAlert : Form
    {
        public frmAlert()
        {
            InitializeComponent();
        }
        public frmAlert(string message, string caption, int button, Form parent, HCIcons icon)
        {
            InitializeComponent();
            var centerForm = Width / 2; // center point of form
            int adjlbl = 0; // Adjust when there's an icon

            BackColor = Settings.ColorBg;
            ForeColor = Settings.ColorFont;
            Text = String.IsNullOrWhiteSpace(caption) ? GlobalVars.CAPTION_DIALOG : caption;
            TopMost = true;
            //TopLevel = false;
            //Parent = (Form)Program.FormMain;
            if (parent != null)
                CenterToParent();
            else
                CenterToScreen();

            lblMessage.Text = message;

            // Disable all buttons first
            btnOk.Enabled = btnOk.Visible = false;
            btnYes.Enabled = btnYes.Visible = false;
            btnNo.Enabled = btnNo.Visible = false;

            // Assign Icon
            picIcon.BackColor = Color.Transparent;
            picIcon.BringToFront();
            Bitmap image = null;
            if (icon == HCIcons.None)
            {
                // No Icon
                picIcon.Visible = false;
                lblMessage.Left = picIcon.Left;
                // Center label message
                lblMessage.Left = ((ClientRectangle.Width - lblMessage.Width) / 2) - 8 + adjlbl;
            }
            else
            {
                switch ((int)icon)
                {
                    case (int)HCIcons.Warning:
                    {
                        image = Resources.IconWarning;
                        break;
                    }
                    case (int)HCIcons.Error:
                    {
                        image = Resources.IconError;
                        break;
                    }
                    case (int)HCIcons.Check:
                    {
                        image = Resources.IconCheckmark;
                        break;
                    }
                    case (int)HCIcons.Question:
                    {
                        image = Resources.IconQuestion;
                        break;
                    }
                    default:
                    {
                        image = Resources.IconInfo;
                        break;
                    }
                }
                if (picIcon.InvokeRequired)
                {
                    this.BeginInvoke((Action)delegate
                    {
                        picIcon.Image?.Dispose();
                        picIcon.Image = image;
                    });
                }
                else
                {
                    picIcon.Image?.Dispose();
                    picIcon.Image = image;
                }
                if (lblMessage.Height < picIcon.Height)
                {
                    lblMessage.AutoSize = false;
                    lblMessage.TextAlign = ContentAlignment.TopLeft;
                    lblMessage.Width = (ClientRectangle.Width - lblMessage.Left - 8);
                    lblMessage.Height += picIcon.Height - lblMessage.Height + 8;
                }
            }
            // Adjust form height, with label message height
            Height = lblMessage.Bottom + btnOk.Height + 64; 

            // Switch type of prompt message
            switch (button)
            {
                case 1: // Yes, No
                    {
                        btnYes.Enabled = btnYes.Visible = true;
                        btnNo.Enabled = btnNo.Visible = true;
                        btnYes.Top = this.Height - (btnYes.Height + 48);
                        btnNo.Top = btnYes.Top;
                        btnYes.Left = centerForm - (int)(btnYes.Width * 1.5);
                        btnNo.Left = centerForm + (btnNo.Width / 2);
                        btnYes.DialogResult = DialogResult.Yes;
                        btnNo.DialogResult = DialogResult.No;
                        btnYes.ForeColor = Color.Black;
                        btnNo.ForeColor = Color.Black;
                        break;
                    }
                default: // Simple messagebox
                    {
                        btnOk.Enabled = btnOk.Visible = true;
                        btnOk.Top = ClientRectangle.Height - (btnOk.Height + 8);
                        btnOk.Left = centerForm - (btnOk.Width / 2) - 10;
                        if (parent != null)
                        {
                            btnOk.ForeColor = Color.Black;
                        }
                        else
                        {
                            // For use in Program.cs
                            btnOk.ForeColor = Color.White;
                            btnOk.BackColor = Color.Black;
                        }
                        break;
                    }
            }
        }

        private void frmAlert_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (picIcon.Visible)
                picIcon.Image?.Dispose();

            Dispose();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
