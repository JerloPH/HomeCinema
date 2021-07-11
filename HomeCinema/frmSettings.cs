﻿#region License
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

namespace HomeCinema
{
    public partial class frmSettings : Form
    {
        private Color BackgroundColor = GlobalVars.SET_COLOR_BG;
        private Color FontColor = GlobalVars.SET_COLOR_FONT;
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
        private void tabControl1_DrawItem(Object sender, DrawItemEventArgs e)
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
            _stringFlags.Dispose();
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
        private void RemoveFromListBox(ListBox lb)
        {
            for (int i = lb.SelectedIndices.Count - 1; i >= 0; i--)
            {
                int item = lb.SelectedIndices[i];
                lb.Items.RemoveAt(item);
            }
        }
        private void EditListBoxItems(ListBox lb)
        {
            int item, pos;
            for (int i = lb.SelectedIndices.Count - 1; i >= 0; i--)
            {
                item = lb.SelectedIndices[i];
                pos = lb.Items.IndexOf(lb.Items[item]);
                if (pos > -1)
                {
                    lb.Items[pos] = GlobalVars.GetStringInputBox($"Change '{lb.Items[item]}' to", lb.Items[item].ToString());
                }
            }
        }
        #endregion
        // ############################################################################################### EVENTS
        private void frmSettings_Load(object sender, EventArgs e)
        {
            string errFrom = "frmSettings-frmSettings_Load";
            ToolTip tooltip = new ToolTip();
            string[] choice = { "Yes", "No" };
            string text = "";

            // Add ToolTips
            tooltip.SetToolTip(lblAutoUpdate, "Automatically check for App updates.");
            tooltip.SetToolTip(lblOfflineMode, "Disable Automatic online functionalities. Overrides Auto update.");
            tooltip.SetToolTip(lblPlayMovieClick, "On double-clicking an item, plays the File, instead of viewing its details.");
            tooltip.SetToolTip(lblMaxLogFileSize, "Maximum file size of log before deleting it.");
            tooltip.SetToolTip(lblItemDisplayCount, "Maximum number of Items displayed for Search results.\n'0' displays all.");
            tooltip.SetToolTip(lblImdbSearchLimit, "Limit Search results in searching Imdb entry.");
            tooltip.SetToolTip(btnCheckUpdate, "Manually check for updates.");
            tooltip.SetToolTip(lblAutoClean, "Clean logs and temporary files on startup.");
            tooltip.SetToolTip(lblConfirmSearch, "Prompt when Searching or Reloading Items.");

            // setup contents
            cbAutoUpdate.Items.AddRange(choice);
            cbOffline.Items.AddRange(choice);
            cbPlayMovie.Items.AddRange(choice);
            cbAutoClean.Items.AddRange(choice);
            cbConfirmSearch.Items.AddRange(choice);

            // Setting Values Initialization
            // ##################### - GENERAL
            // Booleans
            try { cbAutoUpdate.SelectedIndex = Convert.ToInt16(!GlobalVars.SET_AUTOUPDATE); }
            catch { cbAutoUpdate.SelectedIndex = 0; }

            try { cbOffline.SelectedIndex = Convert.ToInt16(!GlobalVars.SET_OFFLINE); }
            catch { cbOffline.SelectedIndex = 1; }

            try { cbPlayMovie.SelectedIndex = Convert.ToInt16(!GlobalVars.SET_AUTOPLAY); }
            catch { cbPlayMovie.SelectedIndex = 0; }

            try { cbAutoClean.SelectedIndex = Convert.ToInt16(!GlobalVars.SET_AUTOCLEAN); }
            catch { cbAutoClean.SelectedIndex = 1; }

            try { cbConfirmSearch.SelectedIndex = Convert.ToInt16(!GlobalVars.SET_CONFIRMSEARCH); }
            catch { cbConfirmSearch.SelectedIndex = 1; }

            // TextBox
            try { txtLogSize.Text = (GlobalVars.SET_LOGMAXSIZE / GlobalVars.BYTES).ToString(); }
            catch { txtLogSize.Text = "1"; }

            try { txtMaxItemCount.Text = GlobalVars.SET_ITEMLIMIT.ToString(); }
            catch { txtMaxItemCount.Text = "0"; }

            try { txtImdbSearchLimit.Text = GlobalVars.SET_SEARCHLIMIT.ToString(); }
            catch { txtImdbSearchLimit.Text = "5"; }

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

            // Load medialocation.hc_data

            // Theme-related
            btnColorBG.BackColor = BackgroundColor;
            btnColorFont.BackColor = FontColor;
        }
        private void frmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalVars.formSetting = null;
            Program.FormMain.lvSearchResult.BackColor = GlobalVars.SET_COLOR_BG;
            
            Dispose();

            Program.FormMain.Focus();
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
            string error = "";

            // Booleans
            GlobalVars.SET_AUTOUPDATE = !Convert.ToBoolean(cbAutoUpdate.SelectedIndex);
            GlobalVars.SET_OFFLINE = !Convert.ToBoolean(cbOffline.SelectedIndex);
            GlobalVars.SET_AUTOPLAY = !Convert.ToBoolean(cbPlayMovie.SelectedIndex);
            GlobalVars.SET_AUTOCLEAN = !Convert.ToBoolean(cbAutoClean.SelectedIndex);
            GlobalVars.SET_CONFIRMSEARCH = !Convert.ToBoolean(cbConfirmSearch.SelectedIndex);

            // TextBox
            try
            {
                logsize = (long)Convert.ToDouble(txtLogSize.Text);
                GlobalVars.SET_LOGMAXSIZE = logsize * GlobalVars.BYTES;
            }
            catch { error += Environment.NewLine + lblMaxLogFileSize.Text.Trim().TrimEnd(':'); }
            
            try { GlobalVars.SET_ITEMLIMIT = Convert.ToInt32(txtMaxItemCount.Text); }
            catch { error += Environment.NewLine + lblItemDisplayCount.Text.Trim().TrimEnd(':'); }

            try { GlobalVars.SET_SEARCHLIMIT = Convert.ToInt32(txtImdbSearchLimit.Text); }
            catch { error += Environment.NewLine + lblImdbSearchLimit.Text.Trim().TrimEnd(':'); }

            // Write supported file extensions
            try
            {
                var arryExt = txtMediaExt.Text.Split(',');
                string text = "";
                foreach (string c in arryExt)
                {
                    if (!String.IsNullOrWhiteSpace(c))
                    {
                        text += c.Trim() + ",";
                    }
                }
                text.TrimEnd(',');
                GlobalVars.WriteToFile(GlobalVars.FILE_MEDIA_EXT, text);
            }
            catch { error += Environment.NewLine + "Media Extensions"; }

            // Save medialocation.hc_data file with new contents

            // Replace country file
            GlobalVars.WriteListBoxToFile(GlobalVars.FILE_COUNTRY, listboxCountry, ",");
            Program.FormMain.PopulateCountryCB();

            // Replace genre file
            GlobalVars.WriteListBoxToFile(GlobalVars.FILE_GENRE, listboxGenre, ",");
            Program.FormMain.PopulateGenreCB();

            // Theme Settings
            GlobalVars.SET_COLOR_BG = BackgroundColor;
            GlobalVars.SET_COLOR_FONT = FontColor;

            // Save settings and Show Message
            GlobalVars.SaveSettings();
            GlobalVars.ShowInfo((String.IsNullOrWhiteSpace(error)) ? "Done saving Settings!" : "Some settings are not saved:" + error);
            if (String.IsNullOrWhiteSpace(error))
            {
                Close();
            }
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

        private void btnGenreAdd_Click(object sender, EventArgs e)
        {
            AddToListBox(listboxGenre, "Type genre to add");
        }

        private void btnCountryAdd_Click(object sender, EventArgs e)
        {
            AddToListBox(listboxCountry, "Type country to add");
        }

        private void btnGenreRemove_Click(object sender, EventArgs e)
        {
            RemoveFromListBox(listboxGenre);
        }

        private void btnCountryRemove_Click(object sender, EventArgs e)
        {
            RemoveFromListBox(listboxCountry);
        }

        private void btnGenreClear_Click(object sender, EventArgs e)
        {
            listboxGenre.Items.Clear();
        }

        private void btnCountryClear_Click(object sender, EventArgs e)
        {
            listboxCountry.Items.Clear();
        }

        private void btnGenreEdit_Click(object sender, EventArgs e)
        {
            EditListBoxItems(listboxGenre);
        }

        private void btnCountryEdit_Click(object sender, EventArgs e)
        {
            EditListBoxItems(listboxCountry);
        }

        private void btnCheckUpdate_Click(object sender, EventArgs e)
        {
            GlobalVars.CheckForUpdate(this, true);
        }

        private void btnChangeColorBG_Click(object sender, EventArgs e)
        {
            var col = new ColorDialog();
            col.ShowDialog();
            Program.FormMain.lvSearchResult.BackColor = col.Color;
            BackgroundColor = col.Color;
            btnColorBG.BackColor = col.Color;
            col.Dispose();
        }

        private void btnChangeColorFont_Click(object sender, EventArgs e)
        {
            var col = new ColorDialog();
            col.ShowDialog();
            FontColor = col.Color;
            btnColorFont.BackColor = col.Color;
            col.Dispose();
        }
    }
}
