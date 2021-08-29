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
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HomeCinema
{
    public partial class frmSettings : Form
    {
        private Color BackgroundColor = Settings.ColorBg;
        private Color FontColor = Settings.ColorFont;
        private ToolTip tooltip = new ToolTip();
        public frmSettings()
        {
            InitializeComponent();
            // Form properties
            FormClosing += new FormClosingEventHandler(frmSettings_FormClosing);
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
            // Use our own font.
            Font _tabFont = new Font("Calibri", 18.0f, FontStyle.Bold, GraphicsUnit.Pixel);

            if (e.State == DrawItemState.Selected)
            {
                // Draw a different background color, and don't paint a focus rectangle.
                _textBrush = new SolidBrush(Color.Yellow);
                g.FillRectangle(Brushes.Black, e.Bounds);
            }
            else
            {
                _textBrush = new SolidBrush(e.ForeColor);
                e.DrawBackground();
            }

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
            string item = Msg.GetStringInputBox(caption, "");
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
                    lb.Items[pos] = Msg.GetStringInputBox($"Change '{lb.Items[item]}' to", lb.Items[item].ToString());
                }
            }
        }
        #endregion
        // ############################################################################################### EVENTS
        private void frmSettings_Load(object sender, EventArgs e)
        {
            string errFrom = "frmSettings-frmSettings_Load";
            string[] choice = { "Yes", "No" };
            string text = "";

            // Add ToolTips
            tooltip.BackColor = Settings.ColorBg;
            tooltip.ForeColor = Settings.ColorFont;
            tooltip.ShowAlways = true;

            tooltip.SetToolTip(lblAutoUpdate, "Automatically check for App updates.");
            tooltip.SetToolTip(lblOfflineMode, "Disable Automatic online functionalities. Overrides Auto update.");
            tooltip.SetToolTip(lblPlayMovieClick, "On double-clicking an item, plays the File, instead of viewing its details.");
            tooltip.SetToolTip(lblMaxLogFileSize, "Maximum file size of log before deleting it.");
            tooltip.SetToolTip(lblItemDisplayCount, "Maximum number of Items displayed for Search results.\n'0' displays all.");
            tooltip.SetToolTip(lblImdbSearchLimit, "Limit Search results in searching Imdb entry.");
            tooltip.SetToolTip(lblAutoClean, "Clean logs and temporary files on startup.");
            tooltip.SetToolTip(lblConfirmSearch, "Prompt when Searching or Reloading Items.");
            tooltip.SetToolTip(lblConfirmAction, "Prompt when Confirming certain actions.");
            tooltip.SetToolTip(lblTimeout, "Timeout when connecting to internet." +
                "\nHigher value = Ensures data is retrieved, slower requests." +
                "\nLower value = Data might not be downloaded, faster requests." +
                "\nCannot be lower than 5 second ( 5,000 )");
            tooltip.SetToolTip(lblSkipNonMediaLoc, "Skip entries if its root folder is NOT included\nin 'Media Locations' setting.");

            // setup contents
            cbAutoUpdate.Items.AddRange(choice);
            cbOffline.Items.AddRange(choice);
            cbPlayMovie.Items.AddRange(choice);
            cbAutoClean.Items.AddRange(choice);
            cbConfirmSearch.Items.AddRange(choice);
            cbConfirmAction.Items.AddRange(choice);
            cbSkipNonMediaLoc.Items.AddRange(choice);

            // Setting Values Initialization
            // ##################### - GENERAL
            // Booleans
            cbAutoUpdate.SelectedIndex = Settings.IsAutoUpdate ? 0 : 1;
            cbOffline.SelectedIndex = Settings.IsOffline ? 0 : 1;
            cbPlayMovie.SelectedIndex = Settings.IsAutoplay ? 0 : 1;
            cbAutoClean.SelectedIndex = Settings.IsAutoClean ? 0 : 1;
            cbConfirmSearch.SelectedIndex = Settings.IsConfirmSearch ? 0 : 1;
            cbConfirmAction.SelectedIndex = Settings.IsConfirmMsg ? 0 : 1;
            cbSkipNonMediaLoc.SelectedIndex = Settings.IsSkipNotMediaLoc ? 0 : 1;

            // TextBox
            try { txtLogSize.Text = (Settings.MaxLogSize / 1000000).ToString(); }
            catch { txtLogSize.Text = "1"; }

            try { txtMaxItemCount.Text = Settings.ItemLimit.ToString(); }
            catch { txtMaxItemCount.Text = "0"; }

            try { txtImdbSearchLimit.Text = Settings.SearchLimit.ToString(); }
            catch { txtImdbSearchLimit.Text = "5"; }

            try { txtImgTileWidth.Text = Settings.ImgTileWidth.ToString(); }
            catch { txtImgTileWidth.Text = "96"; }

            try { txtImgTileHeight.Text = Settings.ImgTileHeight.ToString(); }
            catch { txtImgTileHeight.Text = "128"; }

            try { txtTimeout.Text = Settings.TimeOut.ToString(); }
            catch { txtTimeout.Text = "0"; }

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
            foreach (string c in GlobalVars.BuildArrFromFile(DataFile.FILE_MEDIA_EXT, $"{errFrom}[FILE_MEDIA_EXT]"))
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
            dataGridMediaLoc.DataError += new DataGridViewDataErrorEventHandler(dataGridMediaLoc_DataError);
            dataGridMediaLoc.ForeColor = Color.Black;

            if (GlobalVars.MEDIA_LOC?.Count > 0)
            {
                foreach (var item in GlobalVars.MEDIA_LOC)
                {
                    string path = item.Path;
                    int mediatype = item.MediaType == "movie" ? 0 : 1;
                    int source = (item.Source.Equals(HCSource.tmdb) ? 0 : 1);
                    try
                    {
                        var index = dataGridMediaLoc.Rows.Add();
                        var row = dataGridMediaLoc.Rows[index];
                        row.Cells[0].Value = path;
                        row.Cells[1].Value = (row.Cells[1] as DataGridViewComboBoxCell).Items[mediatype];
                        row.Cells[2].Value = (row.Cells[2] as DataGridViewComboBoxCell).Items[source];
                    }
                    catch (Exception ex)
                    {
                        Msg.ShowError("frmSetting", ex, "Media Location setting not loaded!", this);
                    }
                }
            }
            dataGridMediaLoc.Refresh();

            // Theme-related
            btnColorBG.BackColor = BackgroundColor;
            btnColorFont.BackColor = FontColor;
            btnSave.BackColor = Color.DarkGray;
            btnCancel.BackColor = Color.DarkGray;

            CenterToParent();
        }
        private void dataGridMediaLoc_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Logs.LogErr("frmSetting-dataGridMediaLoc_DataError", e.Exception);
        }
        private void frmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalVars.formSetting = null;
            Program.FormMain.lvSearchResult.BackColor = Settings.ColorBg;
            tooltip?.Dispose();
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
            string error = "";
            // Booleans
            Settings.IsAutoUpdate = cbAutoUpdate.SelectedIndex == 0;
            Settings.IsOffline = cbOffline.SelectedIndex == 0;
            Settings.IsAutoplay = cbPlayMovie.SelectedIndex == 0;
            Settings.IsAutoClean = cbAutoClean.SelectedIndex == 0;
            Settings.IsConfirmSearch = cbConfirmSearch.SelectedIndex == 0;
            Settings.IsConfirmMsg = cbConfirmAction.SelectedIndex == 0;
            Settings.IsSkipNotMediaLoc = cbSkipNonMediaLoc.SelectedIndex == 0;

            // TextBox changes
            try { Settings.MaxLogSize = (int)Convert.ToInt32(txtLogSize.Text); }
            catch { error += Environment.NewLine + lblMaxLogFileSize.Text.Trim().TrimEnd(':'); }
            
            try { Settings.ItemLimit = Convert.ToInt32(txtMaxItemCount.Text); }
            catch { error += Environment.NewLine + lblItemDisplayCount.Text.Trim().TrimEnd(':'); }

            try { Settings.SearchLimit = Convert.ToInt32(txtImdbSearchLimit.Text); }
            catch { error += Environment.NewLine + lblImdbSearchLimit.Text.Trim().TrimEnd(':'); }

            try { Settings.ImgTileWidth = Convert.ToInt32(txtImgTileWidth.Text); }
            catch { error += Environment.NewLine + lblImgTileWidth.Text.Trim().TrimEnd(':'); }

            try { Settings.ImgTileHeight = Convert.ToInt32(txtImgTileHeight.Text); }
            catch { error += Environment.NewLine + lblImgTileHeight.Text.Trim().TrimEnd(':'); }

            if (int.TryParse(txtTimeout.Text.Replace(",", ""), out int timeoutval))
            {
                if (Settings.TimeOut < 5000)
                {
                    Msg.ShowWarning("Timeout cannot be lower than 5,000!", "", this);
                    txtTimeout.SelectAll();
                    txtTimeout.Focus();
                    return;
                }
                Settings.TimeOut = timeoutval;
            }
            else
                error += Environment.NewLine + lblTimeout.Text.Trim().TrimEnd(':');

            // Theme Settings
            Settings.ColorBg = BackgroundColor;
            Settings.ColorFont = FontColor;

            // Save settings to file
            Settings.SaveSettings();

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
                GlobalVars.WriteToFile(DataFile.FILE_MEDIA_EXT, text);
            }
            catch { error += Environment.NewLine + "Media Extensions"; }

            // Save medialocation.hc_data file with new contents
            string newMediaLoc = "";
            foreach (DataGridViewRow item in dataGridMediaLoc.Rows)
            {
                string path = item.Cells[0].Value.ToString();
                string mediatypeFromCell = item.Cells[1].Value.ToString().ToLower();
                string sourceFromCell = item.Cells[2].Value.ToString().ToLower();
                string mediatype = mediatypeFromCell;
                string source = sourceFromCell;
                newMediaLoc += $"{path}*{mediatype}*{source}|";
            }
            newMediaLoc = newMediaLoc.TrimEnd('|');
            if (GlobalVars.WriteToFile(DataFile.FILE_MEDIALOC, newMediaLoc))
            {
                GlobalVars.LoadMediaLocations();
            }
            else { error += Environment.NewLine + "Media Locations"; }

            // Replace country file
            if (GlobalVars.WriteListBoxToFile(DataFile.FILE_COUNTRY, listboxCountry, ","))
            {
                Program.FormMain.PopulateCountryCB();
            }
            else { error += Environment.NewLine + "Country list"; }

            // Replace genre file
            if (GlobalVars.WriteListBoxToFile(DataFile.FILE_GENRE, listboxGenre, ","))
            {
                Program.FormMain.PopulateGenreCB();
            }
            else { error += Environment.NewLine + "Genre list"; }

            // Show message
            Msg.ShowInfo((String.IsNullOrWhiteSpace(error)) ? "Done saving Settings!" : "Some settings have invalid values!" + error);
            if (String.IsNullOrWhiteSpace(error))
            {
                Close();
            }
        }
        private void btnMediaLocAdd_Click(object sender, EventArgs e)
        {
            List<string> paths = new List<string>();
            if (dataGridMediaLoc.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridMediaLoc.Rows)
                {
                    var cell = row.Cells["FolderPath"]?.Value;
                    if (cell != null)
                    {
                        paths.Add(cell.ToString());
                    }

                }
            }
            var form = new frmNewMediaLoc("Select folder that contains media files", paths);
            form.ShowDialog(this);
            
            if (!String.IsNullOrWhiteSpace(form.Path))
            {
                int index = dataGridMediaLoc.Rows.Add();
                var rowAdd = dataGridMediaLoc.Rows[index];
                int type = (form.Type.Equals("movie") ? 0 : 1);
                int src = (form.Source.Equals(HCSource.tmdb) ? 0 : 1);

                rowAdd.Cells[0].Value = form.Path;
                rowAdd.Cells[1].Value = (rowAdd.Cells[1] as DataGridViewComboBoxCell).Items[type];
                rowAdd.Cells[2].Value = (rowAdd.Cells[2] as DataGridViewComboBoxCell).Items[src];
                dataGridMediaLoc.Refresh();
            }
            else
            {
                Msg.ShowWarning("Selected Path is not valid!", "", this);
            }
            form.Dispose();
        }
        private void btnMediaLocRemove_Click(object sender, EventArgs e)
        {
            // Remove selected from DataGridView: dataGridMediaLoc
            int selectedCount = dataGridMediaLoc.SelectedRows.Count;
            while (selectedCount > 0)
            {
                if (!dataGridMediaLoc.SelectedRows[0].IsNewRow)
                {
                    try
                    {
                        dataGridMediaLoc.Rows.RemoveAt(dataGridMediaLoc.SelectedRows[0].Index);
                    }
                    catch { }
                }
                selectedCount--;
            }
            dataGridMediaLoc.ClearSelection();
        }
        private void btnMediaLocClear_Click(object sender, EventArgs e)
        {
            // Remove all from DataGridView: dataGridMediaLoc
            try
            {
                dataGridMediaLoc.Rows.Clear();
            }
            catch { }
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
