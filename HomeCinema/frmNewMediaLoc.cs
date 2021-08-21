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
    public partial class frmNewMediaLoc : Form
    {
        private string Caption = "";
        private bool Save = false;
        private List<String> PathList = null;
        public string Path { get; set; } = ""; // Directory to search for files
        public string Type { get; set; } = ""; // media type
        public string Source { get; set; } = ""; // source for fetching info

        public frmNewMediaLoc(string caption, List<string> existingList)
        {
            InitializeComponent();
            Text = $"[Add New Location] {GlobalVars.HOMECINEMA_NAME} - Media Organizer (v{GlobalVars.HOMECINEMA_VERSION} r{GlobalVars.HOMECINEMA_BUILD})";
            FormClosing += new FormClosingEventHandler(frmNewMediaLoc_FormClosing);

            // Theme-related
            BackColor = Settings.ColorBg;
            ForeColor = Settings.ColorFont;
            btnBrowse.ForeColor = Color.Black;
            btnOK.ForeColor = Color.Black;
            btnBrowse.BackColor = Color.DarkGray;
            btnOK.BackColor = Color.DarkGray;

            CenterToParent();

            // Vars
            Caption = caption;
            PathList = existingList;

            // Set Properties of Controls
            cbSource.Items.AddRange(HCSource.sources);
            cbType.Items.AddRange(new string[] { "Movie", "Series" });
            cbSource.SelectedIndex = 0;
            cbType.SelectedIndex = 0;
        }

        private void frmNewMediaLoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Save)
            {
                Path = txtPath.Text;
                Source = cbSource.Text.ToLower();
                Type = cbType.Text.ToLower();
            }
            PathList?.Clear();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = Caption;
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtPath.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (PathList != null)
            {
                if (PathList.Contains(txtPath.Text))
                {
                    GlobalVars.ShowInfo("Path is already existing!\nSelect a different folder", "", this);
                    return;
                }
            }
            Save = true;
            Close();
        }
    }
}
