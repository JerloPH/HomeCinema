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
            this.btnSeriesLocClear = new System.Windows.Forms.Button();
            this.btnSeriesLocRemove = new System.Windows.Forms.Button();
            this.btnSeriesLocAdd = new System.Windows.Forms.Button();
            this.BoxSeriesLoc = new System.Windows.Forms.ListBox();
            this.BoxMediaLoc = new System.Windows.Forms.ListBox();
            this.btnMediaLocClear = new System.Windows.Forms.Button();
            this.btnMediaLocRemove = new System.Windows.Forms.Button();
            this.btnMediaLocAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMediaExt = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.listboxGenre = new System.Windows.Forms.ListBox();
            this.listboxCountry = new System.Windows.Forms.ListBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.ItemSize = new System.Drawing.Size(30, 100);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(811, 473);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Black;
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
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(703, 465);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            // 
            // lblItemDisplayCount
            // 
            this.lblItemDisplayCount.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemDisplayCount.Location = new System.Drawing.Point(5, 274);
            this.lblItemDisplayCount.Name = "lblItemDisplayCount";
            this.lblItemDisplayCount.Size = new System.Drawing.Size(244, 31);
            this.lblItemDisplayCount.TabIndex = 9;
            this.lblItemDisplayCount.Text = "Item Display Count : ";
            // 
            // txtMaxItemCount
            // 
            this.txtMaxItemCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxItemCount.Location = new System.Drawing.Point(271, 270);
            this.txtMaxItemCount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMaxItemCount.Name = "txtMaxItemCount";
            this.txtMaxItemCount.Size = new System.Drawing.Size(167, 30);
            this.txtMaxItemCount.TabIndex = 8;
            // 
            // lblMaxLogFileSize
            // 
            this.lblMaxLogFileSize.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxLogFileSize.Location = new System.Drawing.Point(5, 215);
            this.lblMaxLogFileSize.Name = "lblMaxLogFileSize";
            this.lblMaxLogFileSize.Size = new System.Drawing.Size(244, 31);
            this.lblMaxLogFileSize.TabIndex = 7;
            this.lblMaxLogFileSize.Text = "Max Log File Size (MB) :";
            // 
            // txtLogSize
            // 
            this.txtLogSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLogSize.Location = new System.Drawing.Point(271, 209);
            this.txtLogSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtLogSize.Name = "txtLogSize";
            this.txtLogSize.Size = new System.Drawing.Size(167, 30);
            this.txtLogSize.TabIndex = 6;
            // 
            // cbPlayMovie
            // 
            this.cbPlayMovie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlayMovie.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPlayMovie.FormattingEnabled = true;
            this.cbPlayMovie.Location = new System.Drawing.Point(271, 140);
            this.cbPlayMovie.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbPlayMovie.Name = "cbPlayMovie";
            this.cbPlayMovie.Size = new System.Drawing.Size(167, 33);
            this.cbPlayMovie.TabIndex = 5;
            // 
            // lblPlayMovieClick
            // 
            this.lblPlayMovieClick.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayMovieClick.Location = new System.Drawing.Point(5, 143);
            this.lblPlayMovieClick.Name = "lblPlayMovieClick";
            this.lblPlayMovieClick.Size = new System.Drawing.Size(249, 23);
            this.lblPlayMovieClick.TabIndex = 4;
            this.lblPlayMovieClick.Text = "Play Movie on Click :";
            // 
            // cbOffline
            // 
            this.cbOffline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOffline.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOffline.FormattingEnabled = true;
            this.cbOffline.Location = new System.Drawing.Point(271, 86);
            this.cbOffline.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbOffline.Name = "cbOffline";
            this.cbOffline.Size = new System.Drawing.Size(167, 33);
            this.cbOffline.TabIndex = 3;
            // 
            // lblOfflineMode
            // 
            this.lblOfflineMode.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOfflineMode.Location = new System.Drawing.Point(5, 89);
            this.lblOfflineMode.Name = "lblOfflineMode";
            this.lblOfflineMode.Size = new System.Drawing.Size(244, 23);
            this.lblOfflineMode.TabIndex = 2;
            this.lblOfflineMode.Text = "Offline Mode :";
            // 
            // cbAutoUpdate
            // 
            this.cbAutoUpdate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAutoUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAutoUpdate.FormattingEnabled = true;
            this.cbAutoUpdate.Location = new System.Drawing.Point(271, 30);
            this.cbAutoUpdate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbAutoUpdate.Name = "cbAutoUpdate";
            this.cbAutoUpdate.Size = new System.Drawing.Size(167, 33);
            this.cbAutoUpdate.TabIndex = 1;
            // 
            // lblAutoUpdate
            // 
            this.lblAutoUpdate.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAutoUpdate.Location = new System.Drawing.Point(5, 32);
            this.lblAutoUpdate.Name = "lblAutoUpdate";
            this.lblAutoUpdate.Size = new System.Drawing.Size(244, 23);
            this.lblAutoUpdate.TabIndex = 0;
            this.lblAutoUpdate.Text = "Auto Update :";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Black;
            this.tabPage2.Controls.Add(this.btnSeriesLocClear);
            this.tabPage2.Controls.Add(this.btnSeriesLocRemove);
            this.tabPage2.Controls.Add(this.btnSeriesLocAdd);
            this.tabPage2.Controls.Add(this.BoxSeriesLoc);
            this.tabPage2.Controls.Add(this.BoxMediaLoc);
            this.tabPage2.Controls.Add(this.btnMediaLocClear);
            this.tabPage2.Controls.Add(this.btnMediaLocRemove);
            this.tabPage2.Controls.Add(this.btnMediaLocAdd);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.txtMediaExt);
            this.tabPage2.ForeColor = System.Drawing.Color.White;
            this.tabPage2.Location = new System.Drawing.Point(104, 4);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(703, 465);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "File";
            // 
            // btnSeriesLocClear
            // 
            this.btnSeriesLocClear.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeriesLocClear.ForeColor = System.Drawing.Color.Black;
            this.btnSeriesLocClear.Location = new System.Drawing.Point(541, 391);
            this.btnSeriesLocClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSeriesLocClear.Name = "btnSeriesLocClear";
            this.btnSeriesLocClear.Size = new System.Drawing.Size(117, 37);
            this.btnSeriesLocClear.TabIndex = 24;
            this.btnSeriesLocClear.Text = "CLEAR";
            this.btnSeriesLocClear.UseVisualStyleBackColor = true;
            this.btnSeriesLocClear.Click += new System.EventHandler(this.btnSeriesLocClear_Click);
            // 
            // btnSeriesLocRemove
            // 
            this.btnSeriesLocRemove.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeriesLocRemove.ForeColor = System.Drawing.Color.Black;
            this.btnSeriesLocRemove.Location = new System.Drawing.Point(541, 350);
            this.btnSeriesLocRemove.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSeriesLocRemove.Name = "btnSeriesLocRemove";
            this.btnSeriesLocRemove.Size = new System.Drawing.Size(117, 37);
            this.btnSeriesLocRemove.TabIndex = 23;
            this.btnSeriesLocRemove.Text = "REMOVE";
            this.btnSeriesLocRemove.UseVisualStyleBackColor = true;
            this.btnSeriesLocRemove.Click += new System.EventHandler(this.btnSeriesLocRemove_Click);
            // 
            // btnSeriesLocAdd
            // 
            this.btnSeriesLocAdd.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeriesLocAdd.ForeColor = System.Drawing.Color.Black;
            this.btnSeriesLocAdd.Location = new System.Drawing.Point(541, 308);
            this.btnSeriesLocAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSeriesLocAdd.Name = "btnSeriesLocAdd";
            this.btnSeriesLocAdd.Size = new System.Drawing.Size(116, 37);
            this.btnSeriesLocAdd.TabIndex = 22;
            this.btnSeriesLocAdd.Text = "ADD";
            this.btnSeriesLocAdd.UseVisualStyleBackColor = true;
            this.btnSeriesLocAdd.Click += new System.EventHandler(this.btnSeriesLocAdd_Click);
            // 
            // BoxSeriesLoc
            // 
            this.BoxSeriesLoc.FormattingEnabled = true;
            this.BoxSeriesLoc.ItemHeight = 22;
            this.BoxSeriesLoc.Location = new System.Drawing.Point(11, 308);
            this.BoxSeriesLoc.Margin = new System.Windows.Forms.Padding(4);
            this.BoxSeriesLoc.Name = "BoxSeriesLoc";
            this.BoxSeriesLoc.Size = new System.Drawing.Size(525, 114);
            this.BoxSeriesLoc.TabIndex = 21;
            // 
            // BoxMediaLoc
            // 
            this.BoxMediaLoc.FormattingEnabled = true;
            this.BoxMediaLoc.ItemHeight = 22;
            this.BoxMediaLoc.Location = new System.Drawing.Point(11, 139);
            this.BoxMediaLoc.Margin = new System.Windows.Forms.Padding(4);
            this.BoxMediaLoc.Name = "BoxMediaLoc";
            this.BoxMediaLoc.Size = new System.Drawing.Size(525, 114);
            this.BoxMediaLoc.TabIndex = 20;
            // 
            // btnMediaLocClear
            // 
            this.btnMediaLocClear.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMediaLocClear.ForeColor = System.Drawing.Color.Black;
            this.btnMediaLocClear.Location = new System.Drawing.Point(544, 222);
            this.btnMediaLocClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMediaLocClear.Name = "btnMediaLocClear";
            this.btnMediaLocClear.Size = new System.Drawing.Size(117, 37);
            this.btnMediaLocClear.TabIndex = 19;
            this.btnMediaLocClear.Text = "CLEAR";
            this.btnMediaLocClear.UseVisualStyleBackColor = true;
            this.btnMediaLocClear.Click += new System.EventHandler(this.btnMediaLocClear_Click);
            // 
            // btnMediaLocRemove
            // 
            this.btnMediaLocRemove.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMediaLocRemove.ForeColor = System.Drawing.Color.Black;
            this.btnMediaLocRemove.Location = new System.Drawing.Point(544, 180);
            this.btnMediaLocRemove.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMediaLocRemove.Name = "btnMediaLocRemove";
            this.btnMediaLocRemove.Size = new System.Drawing.Size(117, 37);
            this.btnMediaLocRemove.TabIndex = 18;
            this.btnMediaLocRemove.Text = "REMOVE";
            this.btnMediaLocRemove.UseVisualStyleBackColor = true;
            this.btnMediaLocRemove.Click += new System.EventHandler(this.btnMediaLocRemove_Click);
            // 
            // btnMediaLocAdd
            // 
            this.btnMediaLocAdd.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMediaLocAdd.ForeColor = System.Drawing.Color.Black;
            this.btnMediaLocAdd.Location = new System.Drawing.Point(544, 138);
            this.btnMediaLocAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMediaLocAdd.Name = "btnMediaLocAdd";
            this.btnMediaLocAdd.Size = new System.Drawing.Size(116, 37);
            this.btnMediaLocAdd.TabIndex = 17;
            this.btnMediaLocAdd.Text = "ADD";
            this.btnMediaLocAdd.UseVisualStyleBackColor = true;
            this.btnMediaLocAdd.Click += new System.EventHandler(this.btnMediaLocAdd_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 273);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 31);
            this.label1.TabIndex = 17;
            this.label1.Text = "*Series Locations :";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(5, 103);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(181, 31);
            this.label8.TabIndex = 15;
            this.label8.Text = "*Media Locations :";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(5, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(196, 31);
            this.label5.TabIndex = 9;
            this.label5.Text = "*Media File Format :";
            // 
            // txtMediaExt
            // 
            this.txtMediaExt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMediaExt.Location = new System.Drawing.Point(8, 39);
            this.txtMediaExt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMediaExt.Multiline = true;
            this.txtMediaExt.Name = "txtMediaExt";
            this.txtMediaExt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMediaExt.Size = new System.Drawing.Size(612, 62);
            this.txtMediaExt.TabIndex = 8;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Black;
            this.tabPage3.Controls.Add(this.listboxCountry);
            this.tabPage3.Controls.Add(this.listboxGenre);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.ForeColor = System.Drawing.Color.White;
            this.tabPage3.Location = new System.Drawing.Point(104, 4);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(703, 465);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Tags";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 235);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 31);
            this.label7.TabIndex = 17;
            this.label7.Text = "Country :";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 31);
            this.label6.TabIndex = 15;
            this.label6.Text = "Genre :";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(597, 478);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(208, 59);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(404, 478);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(183, 59);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(5, 475);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(335, 34);
            this.label9.TabIndex = 16;
            this.label9.Text = "* Changes Apply after Restart";
            // 
            // listboxGenre
            // 
            this.listboxGenre.FormattingEnabled = true;
            this.listboxGenre.ItemHeight = 22;
            this.listboxGenre.Location = new System.Drawing.Point(13, 47);
            this.listboxGenre.Name = "listboxGenre";
            this.listboxGenre.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listboxGenre.Size = new System.Drawing.Size(524, 180);
            this.listboxGenre.TabIndex = 18;
            // 
            // listboxCountry
            // 
            this.listboxCountry.FormattingEnabled = true;
            this.listboxCountry.ItemHeight = 22;
            this.listboxCountry.Location = new System.Drawing.Point(13, 269);
            this.listboxCountry.Name = "listboxCountry";
            this.listboxCountry.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listboxCountry.Size = new System.Drawing.Size(524, 180);
            this.listboxCountry.TabIndex = 19;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(811, 546);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabControl1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMediaLocAdd;
        private System.Windows.Forms.Button btnMediaLocClear;
        private System.Windows.Forms.Button btnMediaLocRemove;
        private System.Windows.Forms.ListBox BoxMediaLoc;
        private System.Windows.Forms.ListBox BoxSeriesLoc;
        private System.Windows.Forms.Button btnSeriesLocClear;
        private System.Windows.Forms.Button btnSeriesLocRemove;
        private System.Windows.Forms.Button btnSeriesLocAdd;
        private System.Windows.Forms.ListBox listboxGenre;
        private System.Windows.Forms.ListBox listboxCountry;
    }
}