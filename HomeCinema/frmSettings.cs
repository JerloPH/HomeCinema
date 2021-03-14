#region License
/* #####################################################################################
 * LICENSE - GPL v3
* HomeCinema - Organize your Movie Collection
* Copyright (C) 2020  JerloPH (https://github.com/JerloPH)

* This program is free software: you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation, either version 3 of the License, or
* (at your option) any later version.

* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.

* You should have received a copy of the GNU General Public License
* along with this program.  If not, see <https://www.gnu.org/licenses/>.
##################################################################################### */
#endregion
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HomeCinema.Global;

namespace HomeCinema
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
            // Form properties
            FormClosing += new FormClosingEventHandler(frmSettings_FormClosing);
            Icon = GlobalVars.HOMECINEMA_ICON;
            Text = $"[Settings] {GlobalVars.HOMECINEMA_NAME} - Media Organizer (v{GlobalVars.HOMECINEMA_VERSION} r{GlobalVars.HOMECINEMA_BUILD.ToString()})";

            // Controls
            tabControl1.DrawItem += new DrawItemEventHandler(tabControl1_DrawItem);
        }
        // ############################################################################################### CUSTOM EVENTS
        private void tabControl1_DrawItem(Object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _textBrush;

            // Get the item from the collection.
            TabPage _tabPage = tabControl1.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = tabControl1.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {

                // Draw a different background color, and don't paint a focus rectangle.
                _textBrush = new SolidBrush(Color.Yellow);
                g.FillRectangle(Brushes.Black, e.Bounds);
            }
            else
            {
                _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
                e.DrawBackground();
            }

            // Use our own font.
            Font _tabFont = new Font("Calibri", 18.0f, FontStyle.Bold, GraphicsUnit.Pixel);

            // Draw string. Center the text.
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        }
        // ############################################################################################### FUNCTIONS
        #region Functions
        private bool CanAddToListBox(ListBox lb, string item)
        {
            if (String.IsNullOrWhiteSpace(item)) { return false; }
            if (lb.Items.Contains(item)) { return false; }
            foreach (string sItem in lb.Items)
            {
                if (sItem.Equals(item, StringComparison.CurrentCultureIgnoreCase))
                {
                    return false;
                }
            }
            return true;
        }
        private void AddToListBox(ListBox lb, string caption = "Type here")
        {
            string item = GlobalVars.GetStringInputBox(caption);
            if (CanAddToListBox(lb, item))
            {
                lb.Items.Add(item);
            }
        }
        #endregion
        // ############################################################################################### EVENTS
        private void frmSettings_Load(object sender, EventArgs e)
        {
            string errFrom = "frmSettings-frmSettings_Load";
            ToolTip tooltip = new ToolTip();
            string[] choice = { "True", "False" };
            string text = "";

            // Add ToolTips
            tooltip.SetToolTip(lblAutoUpdate, "Automatically check for App updates.");
            tooltip.SetToolTip(lblOfflineMode, "Disable Automatic online functionalities. Overrides Auto update.");
            tooltip.SetToolTip(lblPlayMovieClick, "On double-clicking an item, plays the File, instead of viewing its details.");
            tooltip.SetToolTip(lblMaxLogFileSize, "Maximum file size of log before deleting it.");
            tooltip.SetToolTip(lblItemDisplayCount, "Maximum number of Items displayed for Search results.\n'0' displays all.");

            // setup contents
            cbAutoUpdate.Items.AddRange(choice);
            cbOffline.Items.AddRange(choice);
            cbPlayMovie.Items.AddRange(choice);

            // Setting Values Initialization
            // ##################### - GENERAL
            // Booleans
            try { cbAutoUpdate.SelectedIndex = Convert.ToInt16(!GlobalVars.SET_AUTOUPDATE); }
            catch { cbAutoUpdate.SelectedIndex = 0; }

            try { cbOffline.SelectedIndex = Convert.ToInt16(!GlobalVars.SET_OFFLINE); }
            catch { cbOffline.SelectedIndex = 1; }

            try { cbPlayMovie.SelectedIndex = Convert.ToInt16(!GlobalVars.SET_AUTOPLAY); }
            catch { cbPlayMovie.SelectedIndex = 0; }

            // TextBox
            try { txtLogSize.Text = (GlobalVars.SET_LOGMAXSIZE / GlobalVars.BYTES).ToString(); }
            catch { txtLogSize.Text = "1"; }

            try { txtMaxItemCount.Text = GlobalVars.SET_ITEMLIMIT.ToString(); }
            catch { txtMaxItemCount.Text = "0"; }

            // ##################### - FILE changes
            // Country Texts
            foreach (string country in GlobalVars.TEXT_COUNTRY)
            {
                if ((String.IsNullOrWhiteSpace(country) == false) && country != "All")
                {
                    listboxCountry.Items.Add(country);
                }
            }

            // Genre items
            foreach (string genre in GlobalVars.TEXT_GENRE)
            {
                if ((String.IsNullOrWhiteSpace(genre) == false) && genre != "All")
                {
                    listboxGenre.Items.Add(genre);
                }
            }

            // Media File Format / File Extensions Texts
            text = "";
            foreach (string c in GlobalVars.BuildArrFromFile(GlobalVars.FILE_MEDIA_EXT, $"{errFrom}[FILE_MEDIA_EXT]"))
            {
                if (String.IsNullOrWhiteSpace(c) == false)
                {
                    text += c.Trim() + ", ";
                }
            }
            text = text.TrimEnd();
            text = text.TrimEnd(',');
            txtMediaExt.Text = text;

            // Media LOCATIONS Folders Texts
            BoxMediaLoc.SelectionMode = SelectionMode.MultiExtended;
            foreach (string c in GlobalVars.BuildDirArrFromFile(GlobalVars.FILE_MEDIALOC, $"{errFrom}[FILE_MEDIALOC]", '*'))
            {
                if (String.IsNullOrWhiteSpace(c) == false)
                {
                    BoxMediaLoc.Items.Add(c);
                }
            }

            // Series LOCATIONS Folders Texts
            BoxSeriesLoc.SelectionMode = SelectionMode.MultiExtended;
            foreach (string c in GlobalVars.BuildDirArrFromFile(GlobalVars.FILE_SERIESLOC, $"{errFrom}[FILE_SERIESLOC]", '*'))
            {
                if (String.IsNullOrWhiteSpace(c) == false)
                {
                    BoxSeriesLoc.Items.Add(c);
                }
            }
        }
        private void frmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Closed
            GlobalVars.formSetting = null;
        }
        // Exit settings form
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        // Save changes to settings
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Try settings values
            long logsize;
            char separator = '*';
            string toWrite = "";

            // Booleans
            GlobalVars.SET_AUTOUPDATE = !Convert.ToBoolean(cbAutoUpdate.SelectedIndex);
            GlobalVars.SET_OFFLINE = !Convert.ToBoolean(cbOffline.SelectedIndex);
            GlobalVars.SET_AUTOPLAY = !Convert.ToBoolean(cbPlayMovie.SelectedIndex);

            // TextBox
            logsize = (long)Convert.ToDouble(txtLogSize.Text);
            GlobalVars.SET_LOGMAXSIZE = logsize * GlobalVars.BYTES;
            GlobalVars.SET_ITEMLIMIT = Convert.ToInt32(txtMaxItemCount.Text);

            // Write MediaLoc file
            foreach (var x in BoxMediaLoc.Items)
            {
                toWrite += x.ToString() + separator;
            }
            toWrite = toWrite.TrimEnd(separator);
            GlobalVars.WriteToFile(GlobalVars.FILE_MEDIALOC, toWrite);
            toWrite = "";

            // Write SeriesLoc file
            foreach (var x in BoxSeriesLoc.Items)
            {
                toWrite += x.ToString() + separator;
            }
            toWrite = toWrite.TrimEnd(separator);
            GlobalVars.WriteToFile(GlobalVars.FILE_SERIESLOC, toWrite);
            toWrite = "";

            // Replace country file
            var listCountry = listboxCountry.Items.Cast<String>().ToList();
            toWrite = (listCountry.Count > 0) ? listCountry.Aggregate((a, b) => a + "," + b) : "";
            GlobalVars.WriteToFile(GlobalVars.FILE_COUNTRY, toWrite);
            Program.FormMain.PopulateCountryCB();

            // Replace genre file
            var listGenre = listboxGenre.Items.Cast<String>().ToList();
            toWrite = (listGenre.Count > 0) ? listGenre.Aggregate((a, b) => a + "," + b) : "";
            GlobalVars.WriteToFile(GlobalVars.FILE_GENRE, toWrite);
            Program.FormMain.PopulateGenreCB();

            // Show Message
            GlobalVars.ShowInfo("Done saving Settings!");
        }
        private void btnMediaLocAdd_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Select folder that contains movie files";
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    if (BoxMediaLoc.Items.Contains(fbd.SelectedPath))
                    {
                        GlobalVars.ShowInfo("Path already exists!");
                        return;
                    }
                    BoxMediaLoc.Items.Add(fbd.SelectedPath);
                }
            }
        }
        private void btnMediaLocRemove_Click(object sender, EventArgs e)
        {
            // Remove selected from ListBox: BoxMediaLoc
            for (int i = BoxMediaLoc.SelectedIndices.Count - 1; i >= 0; i--)
            {
                BoxMediaLoc.Items.RemoveAt(BoxMediaLoc.SelectedIndices[i]);
            }
        }
        private void btnMediaLocClear_Click(object sender, EventArgs e)
        {
            // Remove all from ListBox: BoxMediaLoc
            BoxMediaLoc.Items.Clear();
        }
        private void btnSeriesLocAdd_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Select base folder that contains series' folders";
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    if (BoxSeriesLoc.Items.Contains(fbd.SelectedPath))
                    {
                        GlobalVars.ShowInfo("Path already exists!");
                        return;
                    }
                    BoxSeriesLoc.Items.Add(fbd.SelectedPath);
                }
            }
        }
        private void btnSeriesLocRemove_Click(object sender, EventArgs e)
        {
            // Remove selected from ListBox: BoxSeriesLoc
            for (int i = BoxSeriesLoc.SelectedIndices.Count - 1; i >= 0; i--)
            {
                BoxSeriesLoc.Items.RemoveAt(BoxSeriesLoc.SelectedIndices[i]);
            }
        }
        private void btnSeriesLocClear_Click(object sender, EventArgs e)
        {
            // Remove all from ListBox: BoxSeriesLoc
            BoxSeriesLoc.Items.Clear();
        }

        private void btnGenreAdd_Click(object sender, EventArgs e)
        {
            AddToListBox(listboxGenre, "Type genre to add");
        }

        private void btnCountryAdd_Click(object sender, EventArgs e)
        {
            AddToListBox(listboxCountry, "Type country to add");
        }
    }
}
