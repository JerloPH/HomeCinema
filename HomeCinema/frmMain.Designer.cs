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
            this.btnFixNoInfo = new System.Windows.Forms.Button();
            this.cbHideAnim = new System.Windows.Forms.CheckBox();
            this.cbSortOrder = new System.Windows.Forms.ComboBox();
            this.cbSort = new System.Windows.Forms.ComboBox();
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
            this.tlstripMenu = new System.Windows.Forms.ToolStrip();
            this.tlbtnSettings = new System.Windows.Forms.ToolStripButton();
            this.tlbtnAbout = new System.Windows.Forms.ToolStripButton();
            this.tlbtnClean = new System.Windows.Forms.ToolStripButton();
            this.lblStatus = new System.Windows.Forms.Label();
            this.expSearch.SuspendLayout();
            this.tlstripMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFixNoInfo
            // 
            this.btnFixNoInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnFixNoInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFixNoInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnFixNoInfo.Location = new System.Drawing.Point(492, 168);
            this.btnFixNoInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFixNoInfo.Name = "btnFixNoInfo";
            this.btnFixNoInfo.Size = new System.Drawing.Size(186, 39);
            this.btnFixNoInfo.TabIndex = 31;
            this.btnFixNoInfo.Text = "Show No Info";
            this.btnFixNoInfo.UseVisualStyleBackColor = false;
            this.btnFixNoInfo.Click += new System.EventHandler(this.btnFixNoInfo_Click);
            // 
            // cbHideAnim
            // 
            this.cbHideAnim.AutoSize = true;
            this.cbHideAnim.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbHideAnim.ForeColor = System.Drawing.Color.White;
            this.cbHideAnim.Location = new System.Drawing.Point(1039, 172);
            this.cbHideAnim.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbHideAnim.Name = "cbHideAnim";
            this.cbHideAnim.Size = new System.Drawing.Size(210, 33);
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
            this.cbSortOrder.Location = new System.Drawing.Point(160, 175);
            this.cbSortOrder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbSortOrder.Name = "cbSortOrder";
            this.cbSortOrder.Size = new System.Drawing.Size(131, 30);
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
            this.cbSort.Location = new System.Drawing.Point(20, 175);
            this.cbSort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbSort.Name = "cbSort";
            this.cbSort.Size = new System.Drawing.Size(135, 30);
            this.cbSort.TabIndex = 28;
            this.cbSort.SelectedIndexChanged += new System.EventHandler(this.cbSort_SelectedIndexChanged);
            // 
            // btnChangeView
            // 
            this.btnChangeView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnChangeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnChangeView.Location = new System.Drawing.Point(297, 168);
            this.btnChangeView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnChangeView.Name = "btnChangeView";
            this.btnChangeView.Size = new System.Drawing.Size(189, 39);
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
            this.lvSearchResult.Location = new System.Drawing.Point(4, 221);
            this.lvSearchResult.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lvSearchResult.Name = "lvSearchResult";
            this.lvSearchResult.Size = new System.Drawing.Size(1263, 475);
            this.lvSearchResult.TabIndex = 22;
            this.lvSearchResult.UseCompatibleStateImageBehavior = false;
            this.lvSearchResult.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvSearchResult_KeyDown);
            this.lvSearchResult.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvSearchResult_MouseClick);
            this.lvSearchResult.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvSearchResult_MouseDoubleClick);
            // 
            // txtSearch
            // 
            this.txtSearch.AccessibleDescription = "Search here";
            this.txtSearch.BackColor = System.Drawing.Color.DarkGray;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(12, 52);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(1040, 34);
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
            this.lblImdb.Location = new System.Drawing.Point(8, 94);
            this.lblImdb.Name = "lblImdb";
            this.lblImdb.Size = new System.Drawing.Size(70, 24);
            this.lblImdb.TabIndex = 3;
            this.lblImdb.Text = "IMDB : ";
            this.lblImdb.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtIMDB
            // 
            this.txtIMDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIMDB.Location = new System.Drawing.Point(76, 94);
            this.txtIMDB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtIMDB.Name = "txtIMDB";
            this.txtIMDB.Size = new System.Drawing.Size(185, 28);
            this.txtIMDB.TabIndex = 4;
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.BackColor = System.Drawing.Color.Transparent;
            this.lblYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYear.ForeColor = System.Drawing.Color.White;
            this.lblYear.Location = new System.Drawing.Point(9, 126);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(64, 24);
            this.lblYear.TabIndex = 5;
            this.lblYear.Text = "Year : ";
            this.lblYear.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtYearFrom
            // 
            this.txtYearFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYearFrom.Location = new System.Drawing.Point(75, 127);
            this.txtYearFrom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtYearFrom.Name = "txtYearFrom";
            this.txtYearFrom.Size = new System.Drawing.Size(81, 28);
            this.txtYearFrom.TabIndex = 6;
            // 
            // txtYearTo
            // 
            this.txtYearTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYearTo.Location = new System.Drawing.Point(179, 127);
            this.txtYearTo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtYearTo.Name = "txtYearTo";
            this.txtYearTo.Size = new System.Drawing.Size(81, 28);
            this.txtYearTo.TabIndex = 7;
            // 
            // lblYearDiv
            // 
            this.lblYearDiv.AutoSize = true;
            this.lblYearDiv.BackColor = System.Drawing.Color.Black;
            this.lblYearDiv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYearDiv.ForeColor = System.Drawing.Color.White;
            this.lblYearDiv.Location = new System.Drawing.Point(157, 129);
            this.lblYearDiv.Name = "lblYearDiv";
            this.lblYearDiv.Size = new System.Drawing.Size(16, 24);
            this.lblYearDiv.TabIndex = 8;
            this.lblYearDiv.Text = "-";
            // 
            // lblStudio
            // 
            this.lblStudio.AutoSize = true;
            this.lblStudio.BackColor = System.Drawing.Color.Transparent;
            this.lblStudio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStudio.ForeColor = System.Drawing.Color.White;
            this.lblStudio.Location = new System.Drawing.Point(540, 94);
            this.lblStudio.Name = "lblStudio";
            this.lblStudio.Size = new System.Drawing.Size(78, 24);
            this.lblStudio.TabIndex = 9;
            this.lblStudio.Text = "Studio : ";
            this.lblStudio.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtStudio
            // 
            this.txtStudio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStudio.Location = new System.Drawing.Point(621, 90);
            this.txtStudio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtStudio.Name = "txtStudio";
            this.txtStudio.Size = new System.Drawing.Size(159, 28);
            this.txtStudio.TabIndex = 10;
            // 
            // lblCast
            // 
            this.lblCast.AutoSize = true;
            this.lblCast.BackColor = System.Drawing.Color.Transparent;
            this.lblCast.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCast.ForeColor = System.Drawing.Color.White;
            this.lblCast.Location = new System.Drawing.Point(289, 130);
            this.lblCast.Name = "lblCast";
            this.lblCast.Size = new System.Drawing.Size(61, 24);
            this.lblCast.TabIndex = 11;
            this.lblCast.Text = "Cast : ";
            this.lblCast.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtCast
            // 
            this.txtCast.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCast.Location = new System.Drawing.Point(355, 129);
            this.txtCast.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCast.Name = "txtCast";
            this.txtCast.Size = new System.Drawing.Size(168, 28);
            this.txtCast.TabIndex = 12;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClear.Location = new System.Drawing.Point(1068, 96);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(192, 38);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSearch.Location = new System.Drawing.Point(1068, 53);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(191, 39);
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
            this.lblCountry.Location = new System.Drawing.Point(531, 127);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(85, 24);
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
            this.cbCountry.Location = new System.Drawing.Point(620, 127);
            this.cbCountry.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbCountry.Name = "cbCountry";
            this.cbCountry.Size = new System.Drawing.Size(160, 30);
            this.cbCountry.TabIndex = 20;
            // 
            // lblGenre
            // 
            this.lblGenre.AutoSize = true;
            this.lblGenre.BackColor = System.Drawing.Color.Transparent;
            this.lblGenre.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGenre.ForeColor = System.Drawing.Color.White;
            this.lblGenre.Location = new System.Drawing.Point(813, 97);
            this.lblGenre.Name = "lblGenre";
            this.lblGenre.Size = new System.Drawing.Size(73, 24);
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
            this.cbGenre.Location = new System.Drawing.Point(892, 94);
            this.cbGenre.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbGenre.Name = "cbGenre";
            this.cbGenre.Size = new System.Drawing.Size(160, 30);
            this.cbGenre.TabIndex = 22;
            // 
            // cbClearSearch
            // 
            this.cbClearSearch.AutoSize = true;
            this.cbClearSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbClearSearch.ForeColor = System.Drawing.Color.White;
            this.cbClearSearch.Location = new System.Drawing.Point(1068, 140);
            this.cbClearSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbClearSearch.Name = "cbClearSearch";
            this.cbClearSearch.Size = new System.Drawing.Size(186, 28);
            this.cbClearSearch.TabIndex = 23;
            this.cbClearSearch.Text = "Refresh after Clear";
            this.cbClearSearch.UseVisualStyleBackColor = true;
            // 
            // txtDirector
            // 
            this.txtDirector.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDirector.Location = new System.Drawing.Point(355, 94);
            this.txtDirector.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDirector.Name = "txtDirector";
            this.txtDirector.Size = new System.Drawing.Size(168, 28);
            this.txtDirector.TabIndex = 25;
            // 
            // lblDirector
            // 
            this.lblDirector.AutoSize = true;
            this.lblDirector.BackColor = System.Drawing.Color.Transparent;
            this.lblDirector.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDirector.ForeColor = System.Drawing.Color.White;
            this.lblDirector.Location = new System.Drawing.Point(269, 95);
            this.lblDirector.Name = "lblDirector";
            this.lblDirector.Size = new System.Drawing.Size(90, 24);
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
            this.cbCategory.Location = new System.Drawing.Point(891, 129);
            this.cbCategory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(161, 30);
            this.cbCategory.TabIndex = 19;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.BackColor = System.Drawing.Color.Transparent;
            this.lblCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory.ForeColor = System.Drawing.Color.White;
            this.lblCategory.Location = new System.Drawing.Point(787, 133);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(100, 24);
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
            this.expSearch.Controls.Add(this.lblStatus);
            this.expSearch.Controls.Add(this.btnFixNoInfo);
            this.expSearch.Controls.Add(this.cbSortOrder);
            this.expSearch.Controls.Add(this.cbSort);
            this.expSearch.Controls.Add(this.cbHideAnim);
            this.expSearch.Controls.Add(this.txtDirector);
            this.expSearch.Controls.Add(this.lblDirector);
            this.expSearch.Controls.Add(this.btnChangeView);
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
            this.expSearch.ExpandedHeight = 0;
            this.expSearch.Location = new System.Drawing.Point(0, 0);
            this.expSearch.Margin = new System.Windows.Forms.Padding(1);
            this.expSearch.Name = "expSearch";
            this.expSearch.Size = new System.Drawing.Size(1271, 218);
            this.expSearch.TabIndex = 23;
            this.expSearch.Text = "Search";
            this.expSearch.ExpandCollapse += new System.EventHandler<MakarovDev.ExpandCollapsePanel.ExpandCollapseEventArgs>(this.expSearch_ExpandCollapse);
            // 
            // tlstripMenu
            // 
            this.tlstripMenu.BackColor = System.Drawing.Color.Black;
            this.tlstripMenu.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tlstripMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tlstripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbtnSettings,
            this.tlbtnAbout,
            this.tlbtnClean});
            this.tlstripMenu.Location = new System.Drawing.Point(0, 0);
            this.tlstripMenu.Name = "tlstripMenu";
            this.tlstripMenu.Size = new System.Drawing.Size(1271, 39);
            this.tlstripMenu.TabIndex = 24;
            this.tlstripMenu.Text = "toolStrip1";
            // 
            // tlbtnSettings
            // 
            this.tlbtnSettings.BackColor = System.Drawing.Color.Transparent;
            this.tlbtnSettings.ForeColor = System.Drawing.Color.White;
            this.tlbtnSettings.Image = ((System.Drawing.Image)(resources.GetObject("tlbtnSettings.Image")));
            this.tlbtnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbtnSettings.Name = "tlbtnSettings";
            this.tlbtnSettings.Size = new System.Drawing.Size(131, 36);
            this.tlbtnSettings.Text = "Settings";
            this.tlbtnSettings.ToolTipText = "Show Settings form";
            this.tlbtnSettings.Click += new System.EventHandler(this.tlbtnSettings_Click);
            // 
            // tlbtnAbout
            // 
            this.tlbtnAbout.BackColor = System.Drawing.Color.Transparent;
            this.tlbtnAbout.ForeColor = System.Drawing.Color.White;
            this.tlbtnAbout.Image = global::HomeCinema.Properties.Resources.IconInfo;
            this.tlbtnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbtnAbout.Name = "tlbtnAbout";
            this.tlbtnAbout.Size = new System.Drawing.Size(110, 36);
            this.tlbtnAbout.Text = "About";
            this.tlbtnAbout.ToolTipText = "About App";
            this.tlbtnAbout.Click += new System.EventHandler(this.tlbtnAbout_Click);
            // 
            // tlbtnClean
            // 
            this.tlbtnClean.BackColor = System.Drawing.Color.Transparent;
            this.tlbtnClean.ForeColor = System.Drawing.Color.White;
            this.tlbtnClean.Image = global::HomeCinema.Properties.Resources.CleanIcon;
            this.tlbtnClean.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbtnClean.Name = "tlbtnClean";
            this.tlbtnClean.Size = new System.Drawing.Size(102, 36);
            this.tlbtnClean.Text = "Clean";
            this.tlbtnClean.ToolTipText = "Clean temporary files";
            this.tlbtnClean.Click += new System.EventHandler(this.tlbtnClean_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(684, 173);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(79, 29);
            this.lblStatus.TabIndex = 32;
            this.lblStatus.Text = "Status";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1271, 703);
            this.Controls.Add(this.tlstripMenu);
            this.Controls.Add(this.expSearch);
            this.Controls.Add(this.lvSearchResult);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(1279, 648);
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home Cinema";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.expSearch.ResumeLayout(false);
            this.expSearch.PerformLayout();
            this.tlstripMenu.ResumeLayout(false);
            this.tlstripMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button btnChangeView;
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
        private MakarovDev.ExpandCollapsePanel.ExpandCollapsePanel expSearch;
        private Button btnFixNoInfo;
        private ToolStrip tlstripMenu;
        private ToolStripButton tlbtnSettings;
        private ToolStripButton tlbtnAbout;
        private ToolStripButton tlbtnClean;
        private Label lblStatus;
    }
}

