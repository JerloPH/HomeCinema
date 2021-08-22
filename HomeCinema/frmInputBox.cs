using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HomeCinema
{
    public partial class frmInputBox : Form
    {
        public string Result { get; set; } = "";

        public frmInputBox(string message, List<String> contents)
        {
            InitializeComponent();
            // Initialized Properties
            this.Text = GlobalVars.HOMECINEMA_NAME;
            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
            this.ForeColor = Settings.ColorFont;
            this.BackColor = Settings.ColorBg;
            this.Font = GlobalVars.TILE_FONT;

            cbContents.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbContents.AutoCompleteSource = AutoCompleteSource.ListItems;
            cbContents.DropDownStyle = ComboBoxStyle.DropDown;
            btnOk.ForeColor = Color.Black;
            btnCancel.ForeColor = Color.Black;
            btnOk.BackColor = Color.DarkGray;
            btnCancel.BackColor = Color.DarkGray;

            // Initialize values
            lblMessage.Text = message;
            foreach (var item in contents)
            {
                cbContents.Items.Add(item);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cbContents.Items.Contains(cbContents.Text))
            {
                GlobalVars.ShowWarning("Value is already existing!", "", this);
                return;
            }
            Result = cbContents.Text;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbContents_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk.PerformClick();
            }
        }
    }
}
