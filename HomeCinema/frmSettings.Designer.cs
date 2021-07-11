namespace HomeCinema
{
    partial class frmSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cbConfirmSearch = new System.Windows.Forms.ComboBox();
            this.lblConfirmSearch = new System.Windows.Forms.Label();
            this.cbAutoClean = new System.Windows.Forms.ComboBox();
            this.lblAutoClean = new System.Windows.Forms.Label();
            this.btnCheckUpdate = new System.Windows.Forms.Button();
            this.lblImdbSearchLimit = new System.Windows.Forms.Label();
            this.txtImdbSearchLimit = new System.Windows.Forms.TextBox();
            this.lblItemDisplayCount = new System.Windows.Forms.Label();
            this.txtMaxItemCount = new System.Windows.Forms.TextBox();
            this.lblMaxLogFileSize = new System.Windows.Forms.Label();
            this.txtLogSize = new System.Windows.Forms.TextBox();
            this.cbPlayMovie = new System.Windows.Forms.ComboBox();
            this.lblPlayMovieClick = new System.Windows.Forms.Label();
            this.cbOffline = new System.Windows.Forms.ComboBox();
            this.lblOfflineMode = new System.Windows.Forms.Label();
            this.cbAutoUpdate = new System.Windows.Forms.ComboBox();
            this.lblAutoUpdate = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnMediaLocClear = new System.Windows.Forms.Button();
            this.btnMediaLocRemove = new System.Windows.Forms.Button();
            this.btnMediaLocAdd = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMediaExt = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnCountryClear = new System.Windows.Forms.Button();
            this.btnCountryRemove = new System.Windows.Forms.Button();
            this.btnCountryEdit = new System.Windows.Forms.Button();
            this.btnCountryAdd = new System.Windows.Forms.Button();
            this.btnGenreClear = new System.Windows.Forms.Button();
            this.btnGenreRemove = new System.Windows.Forms.Button();
            this.btnGenreEdit = new System.Windows.Forms.Button();
            this.btnGenreAdd = new System.Windows.Forms.Button();
            this.listboxCountry = new System.Windows.Forms.ListBox();
            this.listboxGenre = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.btnChangeColorFont = new System.Windows.Forms.Button();
            this.btnColorFont = new System.Windows.Forms.Button();
            this.btnChangeColorBG = new System.Windows.Forms.Button();
            this.btnColorBG = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.dataGridMediaLoc = new System.Windows.Forms.DataGridView();
            this.FolderPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MediaType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Source = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.lblImgTileWidth = new System.Windows.Forms.Label();
            this.txtImgTileWidth = new System.Windows.Forms.TextBox();
            this.lblImgTileHeight = new System.Windows.Forms.Label();
            this.txtImgTileHeight = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMediaLoc)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.ItemSize = new System.Drawing.Size(30, 100);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(646, 384);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Black;
            this.tabPage1.Controls.Add(this.cbConfirmSearch);
            this.tabPage1.Controls.Add(this.lblConfirmSearch);
            this.tabPage1.Controls.Add(this.cbAutoClean);
            this.tabPage1.Controls.Add(this.lblAutoClean);
            this.tabPage1.Controls.Add(this.btnCheckUpdate);
            this.tabPage1.Controls.Add(this.lblImdbSearchLimit);
            this.tabPage1.Controls.Add(this.txtImdbSearchLimit);
            this.tabPage1.Controls.Add(this.lblItemDisplayCount);
            this.tabPage1.Controls.Add(this.txtMaxItemCount);
            this.tabPage1.Controls.Add(this.lblMaxLogFileSize);
            this.tabPage1.Controls.Add(this.txtLogSize);
            this.tabPage1.Controls.Add(this.cbPlayMovie);
            this.tabPage1.Controls.Add(this.lblPlayMovieClick);
            this.tabPage1.Controls.Add(this.cbOffline);
            this.tabPage1.Controls.Add(this.lblOfflineMode);
            this.tabPage1.Controls.Add(this.cbAutoUpdate);
            this.tabPage1.Controls.Add(this.lblAutoUpdate);
            this.tabPage1.ForeColor = System.Drawing.Color.White;
            this.tabPage1.Location = new System.Drawing.Point(104, 4);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(538, 376);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            // 
            // cbConfirmSearch
            // 
            this.cbConfirmSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbConfirmSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbConfirmSearch.FormattingEnabled = true;
            this.cbConfirmSearch.Location = new System.Drawing.Point(203, 167);
            this.cbConfirmSearch.Margin = new System.Windows.Forms.Padding(2);
            this.cbConfirmSearch.Name = "cbConfirmSearch";
            this.cbConfirmSearch.Size = new System.Drawing.Size(126, 28);
            this.cbConfirmSearch.TabIndex = 21;
            // 
            // lblConfirmSearch
            // 
            this.lblConfirmSearch.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmSearch.Location = new System.Drawing.Point(4, 169);
            this.lblConfirmSearch.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblConfirmSearch.Name = "lblConfirmSearch";
            this.lblConfirmSearch.Size = new System.Drawing.Size(187, 24);
            this.lblConfirmSearch.TabIndex = 20;
            this.lblConfirmSearch.Text = "Confirm Search or Reload:";
            // 
            // cbAutoClean
            // 
            this.cbAutoClean.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAutoClean.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAutoClean.FormattingEnabled = true;
            this.cbAutoClean.Location = new System.Drawing.Point(203, 131);
            this.cbAutoClean.Margin = new System.Windows.Forms.Padding(2);
            this.cbAutoClean.Name = "cbAutoClean";
            this.cbAutoClean.Size = new System.Drawing.Size(126, 28);
            this.cbAutoClean.TabIndex = 19;
            // 
            // lblAutoClean
            // 
            this.lblAutoClean.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAutoClean.Location = new System.Drawing.Point(4, 133);
            this.lblAutoClean.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAutoClean.Name = "lblAutoClean";
            this.lblAutoClean.Size = new System.Drawing.Size(187, 24);
            this.lblAutoClean.TabIndex = 18;
            this.lblAutoClean.Text = "Clean at Startup :";
            // 
            // btnCheckUpdate
            // 
            this.btnCheckUpdate.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnCheckUpdate.ForeColor = System.Drawing.Color.Black;
            this.btnCheckUpdate.Location = new System.Drawing.Point(376, 327);
            this.btnCheckUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.btnCheckUpdate.Name = "btnCheckUpdate";
            this.btnCheckUpdate.Size = new System.Drawing.Size(146, 34);
            this.btnCheckUpdate.TabIndex = 17;
            this.btnCheckUpdate.Text = "Check for Update";
            this.btnCheckUpdate.UseVisualStyleBackColor = true;
            this.btnCheckUpdate.Click += new System.EventHandler(this.btnCheckUpdate_Click);
            // 
            // lblImdbSearchLimit
            // 
            this.lblImdbSearchLimit.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImdbSearchLimit.Location = new System.Drawing.Point(4, 272);
            this.lblImdbSearchLimit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblImdbSearchLimit.Name = "lblImdbSearchLimit";
            this.lblImdbSearchLimit.Size = new System.Drawing.Size(183, 25);
            this.lblImdbSearchLimit.TabIndex = 11;
            this.lblImdbSearchLimit.Text = "IMDB Search Limit";
            // 
            // txtImdbSearchLimit
            // 
            this.txtImdbSearchLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImdbSearchLimit.Location = new System.Drawing.Point(203, 269);
            this.txtImdbSearchLimit.Margin = new System.Windows.Forms.Padding(2);
            this.txtImdbSearchLimit.Name = "txtImdbSearchLimit";
            this.txtImdbSearchLimit.Size = new System.Drawing.Size(126, 26);
            this.txtImdbSearchLimit.TabIndex = 10;
            // 
            // lblItemDisplayCount
            // 
            this.lblItemDisplayCount.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemDisplayCount.Location = new System.Drawing.Point(4, 241);
            this.lblItemDisplayCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblItemDisplayCount.Name = "lblItemDisplayCount";
            this.lblItemDisplayCount.Size = new System.Drawing.Size(183, 25);
            this.lblItemDisplayCount.TabIndex = 9;
            this.lblItemDisplayCount.Text = "Item Display Count : ";
            // 
            // txtMaxItemCount
            // 
            this.txtMaxItemCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxItemCount.Location = new System.Drawing.Point(203, 238);
            this.txtMaxItemCount.Margin = new System.Windows.Forms.Padding(2);
            this.txtMaxItemCount.Name = "txtMaxItemCount";
            this.txtMaxItemCount.Size = new System.Drawing.Size(126, 26);
            this.txtMaxItemCount.TabIndex = 8;
            // 
            // lblMaxLogFileSize
            // 
            this.lblMaxLogFileSize.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxLogFileSize.Location = new System.Drawing.Point(4, 211);
            this.lblMaxLogFileSize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMaxLogFileSize.Name = "lblMaxLogFileSize";
            this.lblMaxLogFileSize.Size = new System.Drawing.Size(183, 25);
            this.lblMaxLogFileSize.TabIndex = 7;
            this.lblMaxLogFileSize.Text = "Max Log File Size (MB) :";
            // 
            // txtLogSize
            // 
            this.txtLogSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLogSize.Location = new System.Drawing.Point(203, 206);
            this.txtLogSize.Margin = new System.Windows.Forms.Padding(2);
            this.txtLogSize.Name = "txtLogSize";
            this.txtLogSize.Size = new System.Drawing.Size(126, 26);
            this.txtLogSize.TabIndex = 6;
            // 
            // cbPlayMovie
            // 
            this.cbPlayMovie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlayMovie.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPlayMovie.FormattingEnabled = true;
            this.cbPlayMovie.Location = new System.Drawing.Point(203, 95);
            this.cbPlayMovie.Margin = new System.Windows.Forms.Padding(2);
            this.cbPlayMovie.Name = "cbPlayMovie";
            this.cbPlayMovie.Size = new System.Drawing.Size(126, 28);
            this.cbPlayMovie.TabIndex = 5;
            // 
            // lblPlayMovieClick
            // 
            this.lblPlayMovieClick.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayMovieClick.Location = new System.Drawing.Point(4, 98);
            this.lblPlayMovieClick.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPlayMovieClick.Name = "lblPlayMovieClick";
            this.lblPlayMovieClick.Size = new System.Drawing.Size(187, 19);
            this.lblPlayMovieClick.TabIndex = 4;
            this.lblPlayMovieClick.Text = "Play Movie on Click :";
            // 
            // cbOffline
            // 
            this.cbOffline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOffline.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOffline.FormattingEnabled = true;
            this.cbOffline.Location = new System.Drawing.Point(203, 59);
            this.cbOffline.Margin = new System.Windows.Forms.Padding(2);
            this.cbOffline.Name = "cbOffline";
            this.cbOffline.Size = new System.Drawing.Size(126, 28);
            this.cbOffline.TabIndex = 3;
            // 
            // lblOfflineMode
            // 
            this.lblOfflineMode.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOfflineMode.Location = new System.Drawing.Point(4, 62);
            this.lblOfflineMode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOfflineMode.Name = "lblOfflineMode";
            this.lblOfflineMode.Size = new System.Drawing.Size(183, 19);
            this.lblOfflineMode.TabIndex = 2;
            this.lblOfflineMode.Text = "Offline Mode :";
            // 
            // cbAutoUpdate
            // 
            this.cbAutoUpdate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAutoUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAutoUpdate.FormattingEnabled = true;
            this.cbAutoUpdate.Location = new System.Drawing.Point(203, 24);
            this.cbAutoUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.cbAutoUpdate.Name = "cbAutoUpdate";
            this.cbAutoUpdate.Size = new System.Drawing.Size(126, 28);
            this.cbAutoUpdate.TabIndex = 1;
            // 
            // lblAutoUpdate
            // 
            this.lblAutoUpdate.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAutoUpdate.Location = new System.Drawing.Point(4, 26);
            this.lblAutoUpdate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAutoUpdate.Name = "lblAutoUpdate";
            this.lblAutoUpdate.Size = new System.Drawing.Size(183, 19);
            this.lblAutoUpdate.TabIndex = 0;
            this.lblAutoUpdate.Text = "Auto Check for Update :";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Black;
            this.tabPage2.Controls.Add(this.dataGridMediaLoc);
            this.tabPage2.Controls.Add(this.btnMediaLocClear);
            this.tabPage2.Controls.Add(this.btnMediaLocRemove);
            this.tabPage2.Controls.Add(this.btnMediaLocAdd);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.txtMediaExt);
            this.tabPage2.ForeColor = System.Drawing.Color.White;
            this.tabPage2.Location = new System.Drawing.Point(104, 4);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(538, 376);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "File";
            // 
            // btnMediaLocClear
            // 
            this.btnMediaLocClear.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMediaLocClear.ForeColor = System.Drawing.Color.Black;
            this.btnMediaLocClear.Location = new System.Drawing.Point(443, 181);
            this.btnMediaLocClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnMediaLocClear.Name = "btnMediaLocClear";
            this.btnMediaLocClear.Size = new System.Drawing.Size(88, 30);
            this.btnMediaLocClear.TabIndex = 19;
            this.btnMediaLocClear.Text = "CLEAR";
            this.btnMediaLocClear.UseVisualStyleBackColor = true;
            this.btnMediaLocClear.Click += new System.EventHandler(this.btnMediaLocClear_Click);
            // 
            // btnMediaLocRemove
            // 
            this.btnMediaLocRemove.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMediaLocRemove.ForeColor = System.Drawing.Color.Black;
            this.btnMediaLocRemove.Location = new System.Drawing.Point(443, 147);
            this.btnMediaLocRemove.Margin = new System.Windows.Forms.Padding(2);
            this.btnMediaLocRemove.Name = "btnMediaLocRemove";
            this.btnMediaLocRemove.Size = new System.Drawing.Size(88, 30);
            this.btnMediaLocRemove.TabIndex = 18;
            this.btnMediaLocRemove.Text = "REMOVE";
            this.btnMediaLocRemove.UseVisualStyleBackColor = true;
            this.btnMediaLocRemove.Click += new System.EventHandler(this.btnMediaLocRemove_Click);
            // 
            // btnMediaLocAdd
            // 
            this.btnMediaLocAdd.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMediaLocAdd.ForeColor = System.Drawing.Color.Black;
            this.btnMediaLocAdd.Location = new System.Drawing.Point(443, 113);
            this.btnMediaLocAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnMediaLocAdd.Name = "btnMediaLocAdd";
            this.btnMediaLocAdd.Size = new System.Drawing.Size(87, 30);
            this.btnMediaLocAdd.TabIndex = 17;
            this.btnMediaLocAdd.Text = "ADD";
            this.btnMediaLocAdd.UseVisualStyleBackColor = true;
            this.btnMediaLocAdd.Click += new System.EventHandler(this.btnMediaLocAdd_Click);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(4, 90);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(136, 25);
            this.label8.TabIndex = 15;
            this.label8.Text = "*Media Locations :";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 4);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 25);
            this.label5.TabIndex = 9;
            this.label5.Text = "*Media File Format :";
            // 
            // txtMediaExt
            // 
            this.txtMediaExt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMediaExt.Location = new System.Drawing.Point(6, 32);
            this.txtMediaExt.Margin = new System.Windows.Forms.Padding(2);
            this.txtMediaExt.Multiline = true;
            this.txtMediaExt.Name = "txtMediaExt";
            this.txtMediaExt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMediaExt.Size = new System.Drawing.Size(524, 51);
            this.txtMediaExt.TabIndex = 8;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Black;
            this.tabPage3.Controls.Add(this.btnCountryClear);
            this.tabPage3.Controls.Add(this.btnCountryRemove);
            this.tabPage3.Controls.Add(this.btnCountryEdit);
            this.tabPage3.Controls.Add(this.btnCountryAdd);
            this.tabPage3.Controls.Add(this.btnGenreClear);
            this.tabPage3.Controls.Add(this.btnGenreRemove);
            this.tabPage3.Controls.Add(this.btnGenreEdit);
            this.tabPage3.Controls.Add(this.btnGenreAdd);
            this.tabPage3.Controls.Add(this.listboxCountry);
            this.tabPage3.Controls.Add(this.listboxGenre);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.ForeColor = System.Drawing.Color.White;
            this.tabPage3.Location = new System.Drawing.Point(104, 4);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(538, 376);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Tags";
            // 
            // btnCountryClear
            // 
            this.btnCountryClear.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnCountryClear.ForeColor = System.Drawing.Color.Black;
            this.btnCountryClear.Location = new System.Drawing.Point(416, 327);
            this.btnCountryClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnCountryClear.Name = "btnCountryClear";
            this.btnCountryClear.Size = new System.Drawing.Size(106, 33);
            this.btnCountryClear.TabIndex = 26;
            this.btnCountryClear.Text = "CLEAR";
            this.btnCountryClear.UseVisualStyleBackColor = true;
            this.btnCountryClear.Click += new System.EventHandler(this.btnCountryClear_Click);
            // 
            // btnCountryRemove
            // 
            this.btnCountryRemove.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnCountryRemove.ForeColor = System.Drawing.Color.Black;
            this.btnCountryRemove.Location = new System.Drawing.Point(416, 290);
            this.btnCountryRemove.Margin = new System.Windows.Forms.Padding(2);
            this.btnCountryRemove.Name = "btnCountryRemove";
            this.btnCountryRemove.Size = new System.Drawing.Size(106, 33);
            this.btnCountryRemove.TabIndex = 25;
            this.btnCountryRemove.Text = "REMOVE";
            this.btnCountryRemove.UseVisualStyleBackColor = true;
            this.btnCountryRemove.Click += new System.EventHandler(this.btnCountryRemove_Click);
            // 
            // btnCountryEdit
            // 
            this.btnCountryEdit.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnCountryEdit.ForeColor = System.Drawing.Color.Black;
            this.btnCountryEdit.Location = new System.Drawing.Point(416, 254);
            this.btnCountryEdit.Margin = new System.Windows.Forms.Padding(2);
            this.btnCountryEdit.Name = "btnCountryEdit";
            this.btnCountryEdit.Size = new System.Drawing.Size(106, 33);
            this.btnCountryEdit.TabIndex = 24;
            this.btnCountryEdit.Text = "EDIT";
            this.btnCountryEdit.UseVisualStyleBackColor = true;
            this.btnCountryEdit.Click += new System.EventHandler(this.btnCountryEdit_Click);
            // 
            // btnCountryAdd
            // 
            this.btnCountryAdd.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnCountryAdd.ForeColor = System.Drawing.Color.Black;
            this.btnCountryAdd.Location = new System.Drawing.Point(416, 217);
            this.btnCountryAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnCountryAdd.Name = "btnCountryAdd";
            this.btnCountryAdd.Size = new System.Drawing.Size(106, 33);
            this.btnCountryAdd.TabIndex = 23;
            this.btnCountryAdd.Text = "ADD";
            this.btnCountryAdd.UseVisualStyleBackColor = true;
            this.btnCountryAdd.Click += new System.EventHandler(this.btnCountryAdd_Click);
            // 
            // btnGenreClear
            // 
            this.btnGenreClear.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnGenreClear.ForeColor = System.Drawing.Color.Black;
            this.btnGenreClear.Location = new System.Drawing.Point(416, 148);
            this.btnGenreClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenreClear.Name = "btnGenreClear";
            this.btnGenreClear.Size = new System.Drawing.Size(106, 33);
            this.btnGenreClear.TabIndex = 22;
            this.btnGenreClear.Text = "CLEAR";
            this.btnGenreClear.UseVisualStyleBackColor = true;
            this.btnGenreClear.Click += new System.EventHandler(this.btnGenreClear_Click);
            // 
            // btnGenreRemove
            // 
            this.btnGenreRemove.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnGenreRemove.ForeColor = System.Drawing.Color.Black;
            this.btnGenreRemove.Location = new System.Drawing.Point(416, 111);
            this.btnGenreRemove.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenreRemove.Name = "btnGenreRemove";
            this.btnGenreRemove.Size = new System.Drawing.Size(106, 33);
            this.btnGenreRemove.TabIndex = 21;
            this.btnGenreRemove.Text = "REMOVE";
            this.btnGenreRemove.UseVisualStyleBackColor = true;
            this.btnGenreRemove.Click += new System.EventHandler(this.btnGenreRemove_Click);
            // 
            // btnGenreEdit
            // 
            this.btnGenreEdit.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnGenreEdit.ForeColor = System.Drawing.Color.Black;
            this.btnGenreEdit.Location = new System.Drawing.Point(416, 75);
            this.btnGenreEdit.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenreEdit.Name = "btnGenreEdit";
            this.btnGenreEdit.Size = new System.Drawing.Size(106, 33);
            this.btnGenreEdit.TabIndex = 20;
            this.btnGenreEdit.Text = "EDIT";
            this.btnGenreEdit.UseVisualStyleBackColor = true;
            this.btnGenreEdit.Click += new System.EventHandler(this.btnGenreEdit_Click);
            // 
            // btnGenreAdd
            // 
            this.btnGenreAdd.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnGenreAdd.ForeColor = System.Drawing.Color.Black;
            this.btnGenreAdd.Location = new System.Drawing.Point(416, 38);
            this.btnGenreAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenreAdd.Name = "btnGenreAdd";
            this.btnGenreAdd.Size = new System.Drawing.Size(106, 33);
            this.btnGenreAdd.TabIndex = 17;
            this.btnGenreAdd.Text = "ADD";
            this.btnGenreAdd.UseVisualStyleBackColor = true;
            this.btnGenreAdd.Click += new System.EventHandler(this.btnGenreAdd_Click);
            // 
            // listboxCountry
            // 
            this.listboxCountry.FormattingEnabled = true;
            this.listboxCountry.ItemHeight = 17;
            this.listboxCountry.Location = new System.Drawing.Point(10, 219);
            this.listboxCountry.Margin = new System.Windows.Forms.Padding(2);
            this.listboxCountry.Name = "listboxCountry";
            this.listboxCountry.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listboxCountry.Size = new System.Drawing.Size(394, 123);
            this.listboxCountry.TabIndex = 19;
            // 
            // listboxGenre
            // 
            this.listboxGenre.FormattingEnabled = true;
            this.listboxGenre.ItemHeight = 17;
            this.listboxGenre.Location = new System.Drawing.Point(10, 38);
            this.listboxGenre.Margin = new System.Windows.Forms.Padding(2);
            this.listboxGenre.Name = "listboxGenre";
            this.listboxGenre.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listboxGenre.Size = new System.Drawing.Size(394, 123);
            this.listboxGenre.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 191);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 25);
            this.label7.TabIndex = 17;
            this.label7.Text = "Country :";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 10);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 25);
            this.label6.TabIndex = 15;
            this.label6.Text = "Genre :";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.Black;
            this.tabPage4.Controls.Add(this.lblImgTileHeight);
            this.tabPage4.Controls.Add(this.txtImgTileHeight);
            this.tabPage4.Controls.Add(this.lblImgTileWidth);
            this.tabPage4.Controls.Add(this.txtImgTileWidth);
            this.tabPage4.Controls.Add(this.label2);
            this.tabPage4.Controls.Add(this.btnChangeColorFont);
            this.tabPage4.Controls.Add(this.btnColorFont);
            this.tabPage4.Controls.Add(this.btnChangeColorBG);
            this.tabPage4.Controls.Add(this.btnColorBG);
            this.tabPage4.Location = new System.Drawing.Point(104, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(538, 376);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Appearance";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(454, 34);
            this.label2.TabIndex = 17;
            this.label2.Text = "Change applies on next \'Search\' button press";
            // 
            // btnChangeColorFont
            // 
            this.btnChangeColorFont.BackColor = System.Drawing.Color.Black;
            this.btnChangeColorFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeColorFont.Location = new System.Drawing.Point(75, 113);
            this.btnChangeColorFont.Name = "btnChangeColorFont";
            this.btnChangeColorFont.Size = new System.Drawing.Size(204, 38);
            this.btnChangeColorFont.TabIndex = 3;
            this.btnChangeColorFont.Text = "Change Font Color";
            this.btnChangeColorFont.UseVisualStyleBackColor = false;
            this.btnChangeColorFont.Click += new System.EventHandler(this.btnChangeColorFont_Click);
            // 
            // btnColorFont
            // 
            this.btnColorFont.Location = new System.Drawing.Point(23, 113);
            this.btnColorFont.Name = "btnColorFont";
            this.btnColorFont.Size = new System.Drawing.Size(46, 38);
            this.btnColorFont.TabIndex = 2;
            this.btnColorFont.UseVisualStyleBackColor = true;
            // 
            // btnChangeColorBG
            // 
            this.btnChangeColorBG.BackColor = System.Drawing.Color.Black;
            this.btnChangeColorBG.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeColorBG.Location = new System.Drawing.Point(75, 57);
            this.btnChangeColorBG.Name = "btnChangeColorBG";
            this.btnChangeColorBG.Size = new System.Drawing.Size(204, 38);
            this.btnChangeColorBG.TabIndex = 1;
            this.btnChangeColorBG.Text = "Change Background Color";
            this.btnChangeColorBG.UseVisualStyleBackColor = false;
            this.btnChangeColorBG.Click += new System.EventHandler(this.btnChangeColorBG_Click);
            // 
            // btnColorBG
            // 
            this.btnColorBG.Location = new System.Drawing.Point(23, 57);
            this.btnColorBG.Name = "btnColorBG";
            this.btnColorBG.Size = new System.Drawing.Size(46, 38);
            this.btnColorBG.TabIndex = 0;
            this.btnColorBG.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 23.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(464, 388);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(171, 48);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Calibri", 23.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(307, 388);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(153, 48);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(4, 386);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(251, 28);
            this.label9.TabIndex = 16;
            this.label9.Text = "* Changes Apply after Restart";
            // 
            // dataGridMediaLoc
            // 
            this.dataGridMediaLoc.AllowUserToAddRows = false;
            this.dataGridMediaLoc.AllowUserToDeleteRows = false;
            this.dataGridMediaLoc.BackgroundColor = System.Drawing.Color.DimGray;
            this.dataGridMediaLoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridMediaLoc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FolderPath,
            this.MediaType,
            this.Source});
            this.dataGridMediaLoc.GridColor = System.Drawing.Color.Black;
            this.dataGridMediaLoc.Location = new System.Drawing.Point(8, 112);
            this.dataGridMediaLoc.Name = "dataGridMediaLoc";
            this.dataGridMediaLoc.Size = new System.Drawing.Size(430, 259);
            this.dataGridMediaLoc.TabIndex = 20;
            // 
            // FolderPath
            // 
            this.FolderPath.HeaderText = "Path";
            this.FolderPath.Name = "FolderPath";
            this.FolderPath.ToolTipText = "Path to search media files, without the final backslash";
            this.FolderPath.Width = 195;
            // 
            // MediaType
            // 
            this.MediaType.HeaderText = "Media";
            this.MediaType.Items.AddRange(new object[] {
            "Movie",
            "Series"});
            this.MediaType.Name = "MediaType";
            this.MediaType.ToolTipText = "Type of media, either Movie or Series";
            this.MediaType.Width = 80;
            // 
            // Source
            // 
            this.Source.HeaderText = "Source";
            this.Source.Items.AddRange(new object[] {
            "TMDB",
            "Anilist"});
            this.Source.Name = "Source";
            this.Source.ToolTipText = "Source to search info";
            this.Source.Width = 110;
            // 
            // lblImgTileWidth
            // 
            this.lblImgTileWidth.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImgTileWidth.Location = new System.Drawing.Point(120, 169);
            this.lblImgTileWidth.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblImgTileWidth.Name = "lblImgTileWidth";
            this.lblImgTileWidth.Size = new System.Drawing.Size(183, 25);
            this.lblImgTileWidth.TabIndex = 19;
            this.lblImgTileWidth.Text = "*Width of Image covers";
            // 
            // txtImgTileWidth
            // 
            this.txtImgTileWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImgTileWidth.Location = new System.Drawing.Point(24, 168);
            this.txtImgTileWidth.Margin = new System.Windows.Forms.Padding(2);
            this.txtImgTileWidth.Name = "txtImgTileWidth";
            this.txtImgTileWidth.Size = new System.Drawing.Size(92, 26);
            this.txtImgTileWidth.TabIndex = 18;
            // 
            // lblImgTileHeight
            // 
            this.lblImgTileHeight.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImgTileHeight.Location = new System.Drawing.Point(120, 211);
            this.lblImgTileHeight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblImgTileHeight.Name = "lblImgTileHeight";
            this.lblImgTileHeight.Size = new System.Drawing.Size(183, 25);
            this.lblImgTileHeight.TabIndex = 21;
            this.lblImgTileHeight.Text = "*Height of Image covers";
            // 
            // txtImgTileHeight
            // 
            this.txtImgTileHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImgTileHeight.Location = new System.Drawing.Point(24, 210);
            this.txtImgTileHeight.Margin = new System.Windows.Forms.Padding(2);
            this.txtImgTileHeight.Name = "txtImgTileHeight";
            this.txtImgTileHeight.Size = new System.Drawing.Size(92, 26);
            this.txtImgTileHeight.TabIndex = 20;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(646, 444);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabControl1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "frmSettings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmSettings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMediaLoc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblAutoUpdate;
        private System.Windows.Forms.ComboBox cbOffline;
        private System.Windows.Forms.Label lblOfflineMode;
        private System.Windows.Forms.ComboBox cbAutoUpdate;
        private System.Windows.Forms.ComboBox cbPlayMovie;
        private System.Windows.Forms.Label lblPlayMovieClick;
        private System.Windows.Forms.Label lblMaxLogFileSize;
        private System.Windows.Forms.TextBox txtLogSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMediaExt;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblItemDisplayCount;
        private System.Windows.Forms.TextBox txtMaxItemCount;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnMediaLocAdd;
        private System.Windows.Forms.Button btnMediaLocClear;
        private System.Windows.Forms.Button btnMediaLocRemove;
        private System.Windows.Forms.ListBox listboxGenre;
        private System.Windows.Forms.ListBox listboxCountry;
        private System.Windows.Forms.Button btnGenreClear;
        private System.Windows.Forms.Button btnGenreRemove;
        private System.Windows.Forms.Button btnGenreEdit;
        private System.Windows.Forms.Button btnGenreAdd;
        private System.Windows.Forms.Button btnCountryClear;
        private System.Windows.Forms.Button btnCountryRemove;
        private System.Windows.Forms.Button btnCountryEdit;
        private System.Windows.Forms.Button btnCountryAdd;
        private System.Windows.Forms.Label lblImdbSearchLimit;
        private System.Windows.Forms.TextBox txtImdbSearchLimit;
        private System.Windows.Forms.Button btnCheckUpdate;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button btnChangeColorBG;
        private System.Windows.Forms.Button btnColorBG;
        private System.Windows.Forms.Button btnChangeColorFont;
        private System.Windows.Forms.Button btnColorFont;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbAutoClean;
        private System.Windows.Forms.Label lblAutoClean;
        private System.Windows.Forms.ComboBox cbConfirmSearch;
        private System.Windows.Forms.Label lblConfirmSearch;
        private System.Windows.Forms.DataGridView dataGridMediaLoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn FolderPath;
        private System.Windows.Forms.DataGridViewComboBoxColumn MediaType;
        private System.Windows.Forms.DataGridViewComboBoxColumn Source;
        private System.Windows.Forms.Label lblImgTileWidth;
        private System.Windows.Forms.TextBox txtImgTileWidth;
        private System.Windows.Forms.Label lblImgTileHeight;
        private System.Windows.Forms.TextBox txtImgTileHeight;
    }
}