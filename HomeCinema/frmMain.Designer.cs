using System.Windows.Forms;

namespace HomeCinema
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.grpControls = new System.Windows.Forms.GroupBox();
            this.btnAbout = new System.Windows.Forms.Button();
            this.cbHideAnim = new System.Windows.Forms.CheckBox();
            this.cbSortOrder = new System.Windows.Forms.ComboBox();
            this.cbSort = new System.Windows.Forms.ComboBox();
            this.btnClean = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnChangeView = new System.Windows.Forms.Button();
            this.lvSearchResult = new System.Windows.Forms.ListView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblImdb = new System.Windows.Forms.Label();
            this.txtIMDB = new System.Windows.Forms.TextBox();
            this.lblYear = new System.Windows.Forms.Label();
            this.txtYearFrom = new System.Windows.Forms.TextBox();
            this.txtYearTo = new System.Windows.Forms.TextBox();
            this.lblYearDiv = new System.Windows.Forms.Label();
            this.lblStudio = new System.Windows.Forms.Label();
            this.txtStudio = new System.Windows.Forms.TextBox();
            this.lblCast = new System.Windows.Forms.Label();
            this.txtCast = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblCountry = new System.Windows.Forms.Label();
            this.cbCountry = new System.Windows.Forms.ComboBox();
            this.lblGenre = new System.Windows.Forms.Label();
            this.cbGenre = new System.Windows.Forms.ComboBox();
            this.cbClearSearch = new System.Windows.Forms.CheckBox();
            this.txtDirector = new System.Windows.Forms.TextBox();
            this.lblDirector = new System.Windows.Forms.Label();
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmLV = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.expSearch = new MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel();
            this.btnFixNoInfo = new System.Windows.Forms.Button();
            this.grpControls.SuspendLayout();
            this.expSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpControls
            // 
            this.grpControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpControls.BackColor = System.Drawing.Color.Black;
            this.grpControls.Controls.Add(this.btnFixNoInfo);
            this.grpControls.Controls.Add(this.btnAbout);
            this.grpControls.Controls.Add(this.cbHideAnim);
            this.grpControls.Controls.Add(this.cbSortOrder);
            this.grpControls.Controls.Add(this.cbSort);
            this.grpControls.Controls.Add(this.btnClean);
            this.grpControls.Controls.Add(this.btnSettings);
            this.grpControls.Controls.Add(this.btnChangeView);
            this.grpControls.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpControls.ForeColor = System.Drawing.Color.White;
            this.grpControls.Location = new System.Drawing.Point(0, 158);
            this.grpControls.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpControls.Name = "grpControls";
            this.grpControls.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpControls.Size = new System.Drawing.Size(950, 50);
            this.grpControls.TabIndex = 19;
            this.grpControls.TabStop = false;
            this.grpControls.Text = "Controls";
            // 
            // btnAbout
            // 
            this.btnAbout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAbout.Location = new System.Drawing.Point(471, 13);
            this.btnAbout.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(99, 32);
            this.btnAbout.TabIndex = 30;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = false;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // cbHideAnim
            // 
            this.cbHideAnim.AutoSize = true;
            this.cbHideAnim.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbHideAnim.ForeColor = System.Drawing.Color.White;
            this.cbHideAnim.Location = new System.Drawing.Point(770, 15);
            this.cbHideAnim.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbHideAnim.Name = "cbHideAnim";
            this.cbHideAnim.Size = new System.Drawing.Size(167, 28);
            this.cbHideAnim.TabIndex = 26;
            this.cbHideAnim.Text = "Hide Animations";
            this.cbHideAnim.UseVisualStyleBackColor = true;
            this.cbHideAnim.CheckedChanged += new System.EventHandler(this.cbHideAnim_CheckedChanged);
            // 
            // cbSortOrder
            // 
            this.cbSortOrder.BackColor = System.Drawing.Color.Silver;
            this.cbSortOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSortOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSortOrder.ForeColor = System.Drawing.Color.Black;
            this.cbSortOrder.FormattingEnabled = true;
            this.cbSortOrder.Location = new System.Drawing.Point(110, 20);
            this.cbSortOrder.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbSortOrder.Name = "cbSortOrder";
            this.cbSortOrder.Size = new System.Drawing.Size(99, 25);
            this.cbSortOrder.TabIndex = 29;
            this.cbSortOrder.SelectedIndexChanged += new System.EventHandler(this.cbSortOrder_SelectedIndexChanged);
            // 
            // cbSort
            // 
            this.cbSort.BackColor = System.Drawing.Color.Silver;
            this.cbSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSort.ForeColor = System.Drawing.Color.Black;
            this.cbSort.FormattingEnabled = true;
            this.cbSort.Location = new System.Drawing.Point(5, 20);
            this.cbSort.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbSort.Name = "cbSort";
            this.cbSort.Size = new System.Drawing.Size(102, 25);
            this.cbSort.TabIndex = 28;
            this.cbSort.SelectedIndexChanged += new System.EventHandler(this.cbSort_SelectedIndexChanged);
            // 
            // btnClean
            // 
            this.btnClean.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnClean.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClean.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClean.Location = new System.Drawing.Point(574, 13);
            this.btnClean.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(93, 32);
            this.btnClean.TabIndex = 19;
            this.btnClean.Text = "Clean";
            this.btnClean.UseVisualStyleBackColor = false;
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSettings.Location = new System.Drawing.Point(356, 13);
            this.btnSettings.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(111, 32);
            this.btnSettings.TabIndex = 18;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnChangeView
            // 
            this.btnChangeView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnChangeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnChangeView.Location = new System.Drawing.Point(214, 13);
            this.btnChangeView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnChangeView.Name = "btnChangeView";
            this.btnChangeView.Size = new System.Drawing.Size(136, 32);
            this.btnChangeView.TabIndex = 14;
            this.btnChangeView.Text = "Change View";
            this.btnChangeView.UseVisualStyleBackColor = false;
            this.btnChangeView.Click += new System.EventHandler(this.btnChangeView_Click);
            // 
            // lvSearchResult
            // 
            this.lvSearchResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSearchResult.BackColor = System.Drawing.Color.Black;
            this.lvSearchResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvSearchResult.ForeColor = System.Drawing.Color.White;
            this.lvSearchResult.HideSelection = false;
            this.lvSearchResult.Location = new System.Drawing.Point(3, 207);
            this.lvSearchResult.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lvSearchResult.Name = "lvSearchResult";
            this.lvSearchResult.Size = new System.Drawing.Size(948, 359);
            this.lvSearchResult.TabIndex = 22;
            this.lvSearchResult.UseCompatibleStateImageBehavior = false;
            this.lvSearchResult.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvSearchResult_KeyDown);
            this.lvSearchResult.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvSearchResult_MouseClick);
            this.lvSearchResult.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvSearchResult_MouseDoubleClick);
            // 
            // txtSearch
            // 
            this.txtSearch.AccessibleDescription = "Search here";
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BackColor = System.Drawing.Color.DarkGray;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(24, 43);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(800, 28);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.GotFocus += new System.EventHandler(this.SearchBoxPlaceholderClear);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            this.txtSearch.LostFocus += new System.EventHandler(this.SearchBoxPlaceholder);
            // 
            // lblImdb
            // 
            this.lblImdb.AutoSize = true;
            this.lblImdb.BackColor = System.Drawing.Color.Transparent;
            this.lblImdb.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImdb.ForeColor = System.Drawing.Color.White;
            this.lblImdb.Location = new System.Drawing.Point(6, 76);
            this.lblImdb.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblImdb.Name = "lblImdb";
            this.lblImdb.Size = new System.Drawing.Size(57, 18);
            this.lblImdb.TabIndex = 3;
            this.lblImdb.Text = "IMDB : ";
            this.lblImdb.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtIMDB
            // 
            this.txtIMDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIMDB.Location = new System.Drawing.Point(57, 76);
            this.txtIMDB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtIMDB.Name = "txtIMDB";
            this.txtIMDB.Size = new System.Drawing.Size(140, 24);
            this.txtIMDB.TabIndex = 4;
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.BackColor = System.Drawing.Color.Transparent;
            this.lblYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYear.ForeColor = System.Drawing.Color.White;
            this.lblYear.Location = new System.Drawing.Point(7, 102);
            this.lblYear.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(50, 18);
            this.lblYear.TabIndex = 5;
            this.lblYear.Text = "Year : ";
            this.lblYear.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtYearFrom
            // 
            this.txtYearFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYearFrom.Location = new System.Drawing.Point(56, 103);
            this.txtYearFrom.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtYearFrom.Name = "txtYearFrom";
            this.txtYearFrom.Size = new System.Drawing.Size(62, 24);
            this.txtYearFrom.TabIndex = 6;
            // 
            // txtYearTo
            // 
            this.txtYearTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYearTo.Location = new System.Drawing.Point(134, 103);
            this.txtYearTo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtYearTo.Name = "txtYearTo";
            this.txtYearTo.Size = new System.Drawing.Size(62, 24);
            this.txtYearTo.TabIndex = 7;
            // 
            // lblYearDiv
            // 
            this.lblYearDiv.AutoSize = true;
            this.lblYearDiv.BackColor = System.Drawing.Color.Black;
            this.lblYearDiv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYearDiv.ForeColor = System.Drawing.Color.White;
            this.lblYearDiv.Location = new System.Drawing.Point(118, 105);
            this.lblYearDiv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblYearDiv.Name = "lblYearDiv";
            this.lblYearDiv.Size = new System.Drawing.Size(13, 18);
            this.lblYearDiv.TabIndex = 8;
            this.lblYearDiv.Text = "-";
            // 
            // lblStudio
            // 
            this.lblStudio.AutoSize = true;
            this.lblStudio.BackColor = System.Drawing.Color.Transparent;
            this.lblStudio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStudio.ForeColor = System.Drawing.Color.White;
            this.lblStudio.Location = new System.Drawing.Point(405, 76);
            this.lblStudio.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStudio.Name = "lblStudio";
            this.lblStudio.Size = new System.Drawing.Size(62, 18);
            this.lblStudio.TabIndex = 9;
            this.lblStudio.Text = "Studio : ";
            this.lblStudio.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtStudio
            // 
            this.txtStudio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStudio.Location = new System.Drawing.Point(466, 73);
            this.txtStudio.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtStudio.Name = "txtStudio";
            this.txtStudio.Size = new System.Drawing.Size(120, 24);
            this.txtStudio.TabIndex = 10;
            // 
            // lblCast
            // 
            this.lblCast.AutoSize = true;
            this.lblCast.BackColor = System.Drawing.Color.Transparent;
            this.lblCast.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCast.ForeColor = System.Drawing.Color.White;
            this.lblCast.Location = new System.Drawing.Point(217, 106);
            this.lblCast.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCast.Name = "lblCast";
            this.lblCast.Size = new System.Drawing.Size(51, 18);
            this.lblCast.TabIndex = 11;
            this.lblCast.Text = "Cast : ";
            this.lblCast.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtCast
            // 
            this.txtCast.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCast.Location = new System.Drawing.Point(266, 105);
            this.txtCast.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCast.Name = "txtCast";
            this.txtCast.Size = new System.Drawing.Size(127, 24);
            this.txtCast.TabIndex = 12;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClear.Location = new System.Drawing.Point(801, 78);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(144, 31);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSearch.Location = new System.Drawing.Point(827, 42);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(124, 32);
            this.btnSearch.TabIndex = 15;
            this.btnSearch.Text = "SEARCH";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblCountry
            // 
            this.lblCountry.AutoSize = true;
            this.lblCountry.BackColor = System.Drawing.Color.Transparent;
            this.lblCountry.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountry.ForeColor = System.Drawing.Color.White;
            this.lblCountry.Location = new System.Drawing.Point(398, 103);
            this.lblCountry.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(68, 18);
            this.lblCountry.TabIndex = 18;
            this.lblCountry.Text = "Country :";
            this.lblCountry.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbCountry
            // 
            this.cbCountry.BackColor = System.Drawing.Color.Silver;
            this.cbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCountry.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCountry.ForeColor = System.Drawing.Color.Black;
            this.cbCountry.FormattingEnabled = true;
            this.cbCountry.Location = new System.Drawing.Point(465, 103);
            this.cbCountry.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbCountry.Name = "cbCountry";
            this.cbCountry.Size = new System.Drawing.Size(121, 25);
            this.cbCountry.TabIndex = 20;
            // 
            // lblGenre
            // 
            this.lblGenre.AutoSize = true;
            this.lblGenre.BackColor = System.Drawing.Color.Transparent;
            this.lblGenre.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGenre.ForeColor = System.Drawing.Color.White;
            this.lblGenre.Location = new System.Drawing.Point(610, 79);
            this.lblGenre.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGenre.Name = "lblGenre";
            this.lblGenre.Size = new System.Drawing.Size(57, 18);
            this.lblGenre.TabIndex = 21;
            this.lblGenre.Text = "Genre :";
            this.lblGenre.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbGenre
            // 
            this.cbGenre.BackColor = System.Drawing.Color.Silver;
            this.cbGenre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGenre.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGenre.ForeColor = System.Drawing.Color.Black;
            this.cbGenre.FormattingEnabled = true;
            this.cbGenre.Location = new System.Drawing.Point(669, 76);
            this.cbGenre.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbGenre.Name = "cbGenre";
            this.cbGenre.Size = new System.Drawing.Size(121, 25);
            this.cbGenre.TabIndex = 22;
            // 
            // cbClearSearch
            // 
            this.cbClearSearch.AutoSize = true;
            this.cbClearSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbClearSearch.ForeColor = System.Drawing.Color.White;
            this.cbClearSearch.Location = new System.Drawing.Point(801, 114);
            this.cbClearSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbClearSearch.Name = "cbClearSearch";
            this.cbClearSearch.Size = new System.Drawing.Size(146, 22);
            this.cbClearSearch.TabIndex = 23;
            this.cbClearSearch.Text = "Search after Clear";
            this.cbClearSearch.UseVisualStyleBackColor = true;
            // 
            // txtDirector
            // 
            this.txtDirector.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDirector.Location = new System.Drawing.Point(266, 76);
            this.txtDirector.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtDirector.Name = "txtDirector";
            this.txtDirector.Size = new System.Drawing.Size(127, 24);
            this.txtDirector.TabIndex = 25;
            // 
            // lblDirector
            // 
            this.lblDirector.AutoSize = true;
            this.lblDirector.BackColor = System.Drawing.Color.Transparent;
            this.lblDirector.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDirector.ForeColor = System.Drawing.Color.White;
            this.lblDirector.Location = new System.Drawing.Point(202, 77);
            this.lblDirector.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDirector.Name = "lblDirector";
            this.lblDirector.Size = new System.Drawing.Size(73, 18);
            this.lblDirector.TabIndex = 24;
            this.lblDirector.Text = "Director : ";
            this.lblDirector.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbCategory
            // 
            this.cbCategory.BackColor = System.Drawing.Color.Silver;
            this.cbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCategory.ForeColor = System.Drawing.Color.Black;
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Location = new System.Drawing.Point(668, 105);
            this.cbCategory.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(122, 25);
            this.cbCategory.TabIndex = 19;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.BackColor = System.Drawing.Color.Transparent;
            this.lblCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory.ForeColor = System.Drawing.Color.White;
            this.lblCategory.Location = new System.Drawing.Point(590, 108);
            this.lblCategory.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(80, 18);
            this.lblCategory.TabIndex = 16;
            this.lblCategory.Text = "Category : ";
            this.lblCategory.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmLV
            // 
            this.cmLV.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmLV.Name = "cmLV";
            this.cmLV.Size = new System.Drawing.Size(61, 4);
            // 
            // expSearch
            // 
            this.expSearch.BackColor = System.Drawing.Color.Black;
            this.expSearch.ButtonStyle = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonStyle.Circle;
            this.expSearch.Controls.Add(this.txtDirector);
            this.expSearch.Controls.Add(this.lblDirector);
            this.expSearch.Controls.Add(this.txtSearch);
            this.expSearch.Controls.Add(this.cbClearSearch);
            this.expSearch.Controls.Add(this.lblImdb);
            this.expSearch.Controls.Add(this.cbGenre);
            this.expSearch.Controls.Add(this.txtIMDB);
            this.expSearch.Controls.Add(this.lblGenre);
            this.expSearch.Controls.Add(this.lblYear);
            this.expSearch.Controls.Add(this.cbCountry);
            this.expSearch.Controls.Add(this.txtYearFrom);
            this.expSearch.Controls.Add(this.cbCategory);
            this.expSearch.Controls.Add(this.txtYearTo);
            this.expSearch.Controls.Add(this.lblCountry);
            this.expSearch.Controls.Add(this.lblYearDiv);
            this.expSearch.Controls.Add(this.lblCategory);
            this.expSearch.Controls.Add(this.lblStudio);
            this.expSearch.Controls.Add(this.btnSearch);
            this.expSearch.Controls.Add(this.txtStudio);
            this.expSearch.Controls.Add(this.btnClear);
            this.expSearch.Controls.Add(this.lblCast);
            this.expSearch.Controls.Add(this.txtCast);
            this.expSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.expSearch.ExpandedHeight = 0;
            this.expSearch.Location = new System.Drawing.Point(0, 0);
            this.expSearch.Margin = new System.Windows.Forms.Padding(1);
            this.expSearch.Name = "expSearch";
            this.expSearch.Size = new System.Drawing.Size(953, 154);
            this.expSearch.TabIndex = 23;
            this.expSearch.Text = "Search";
            this.expSearch.ExpandCollapse += new System.EventHandler<MakarovDev.ExpandCollapsePanel.ExpandCollapseEventArgs>(this.expSearch_ExpandCollapse);
            // 
            // btnFixNoInfo
            // 
            this.btnFixNoInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnFixNoInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFixNoInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnFixNoInfo.Location = new System.Drawing.Point(672, 13);
            this.btnFixNoInfo.Margin = new System.Windows.Forms.Padding(2);
            this.btnFixNoInfo.Name = "btnFixNoInfo";
            this.btnFixNoInfo.Size = new System.Drawing.Size(93, 32);
            this.btnFixNoInfo.TabIndex = 31;
            this.btnFixNoInfo.Text = "No Info";
            this.btnFixNoInfo.UseVisualStyleBackColor = false;
            this.btnFixNoInfo.Click += new System.EventHandler(this.btnFixNoInfo_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(953, 571);
            this.Controls.Add(this.expSearch);
            this.Controls.Add(this.lvSearchResult);
            this.Controls.Add(this.grpControls);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(964, 535);
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home Cinema";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.grpControls.ResumeLayout(false);
            this.grpControls.PerformLayout();
            this.expSearch.ResumeLayout(false);
            this.expSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox grpControls;
        private Button btnChangeView;
        private Button btnSettings;
        private Button btnClean;
        internal ListView lvSearchResult;
        private TextBox txtSearch;
        private Label lblImdb;
        private TextBox txtIMDB;
        private Label lblYear;
        private TextBox txtYearFrom;
        private TextBox txtYearTo;
        private Label lblYearDiv;
        private Label lblStudio;
        private TextBox txtStudio;
        private Label lblCast;
        private TextBox txtCast;
        private Button btnClear;
        internal Button btnSearch;
        private Label lblCountry;
        private ComboBox cbCountry;
        private Label lblGenre;
        private ComboBox cbGenre;
        private CheckBox cbClearSearch;
        private TextBox txtDirector;
        private Label lblDirector;
        private ComboBox cbCategory;
        private Label lblCategory;
        private ContextMenuStrip cmLV;
        private ComboBox cbSortOrder;
        private ComboBox cbSort;
        private CheckBox cbHideAnim;
        private Button btnAbout;
        private MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel expSearch;
        private Button btnFixNoInfo;
    }
}

