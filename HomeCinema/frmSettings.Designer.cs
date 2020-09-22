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
            this.label8 = new System.Windows.Forms.Label();
            this.txtMediaLoc = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMediaExt = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCountry = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtGenre = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSeriesLoc = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(810, 431);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
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
            this.tabPage1.Location = new System.Drawing.Point(4, 31);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(802, 396);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblItemDisplayCount
            // 
            this.lblItemDisplayCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemDisplayCount.Location = new System.Drawing.Point(11, 275);
            this.lblItemDisplayCount.Name = "lblItemDisplayCount";
            this.lblItemDisplayCount.Size = new System.Drawing.Size(244, 31);
            this.lblItemDisplayCount.TabIndex = 9;
            this.lblItemDisplayCount.Text = "Item Display Count : ";
            this.lblItemDisplayCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtMaxItemCount
            // 
            this.txtMaxItemCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxItemCount.Location = new System.Drawing.Point(271, 269);
            this.txtMaxItemCount.Name = "txtMaxItemCount";
            this.txtMaxItemCount.Size = new System.Drawing.Size(167, 30);
            this.txtMaxItemCount.TabIndex = 8;
            // 
            // lblMaxLogFileSize
            // 
            this.lblMaxLogFileSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxLogFileSize.Location = new System.Drawing.Point(11, 215);
            this.lblMaxLogFileSize.Name = "lblMaxLogFileSize";
            this.lblMaxLogFileSize.Size = new System.Drawing.Size(244, 31);
            this.lblMaxLogFileSize.TabIndex = 7;
            this.lblMaxLogFileSize.Text = "Max Log File Size (MB) :";
            this.lblMaxLogFileSize.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtLogSize
            // 
            this.txtLogSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLogSize.Location = new System.Drawing.Point(271, 209);
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
            this.cbPlayMovie.Name = "cbPlayMovie";
            this.cbPlayMovie.Size = new System.Drawing.Size(167, 33);
            this.cbPlayMovie.TabIndex = 5;
            // 
            // lblPlayMovieClick
            // 
            this.lblPlayMovieClick.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayMovieClick.Location = new System.Drawing.Point(6, 143);
            this.lblPlayMovieClick.Name = "lblPlayMovieClick";
            this.lblPlayMovieClick.Size = new System.Drawing.Size(249, 23);
            this.lblPlayMovieClick.TabIndex = 4;
            this.lblPlayMovieClick.Text = "Play Movie on Click :";
            this.lblPlayMovieClick.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbOffline
            // 
            this.cbOffline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOffline.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOffline.FormattingEnabled = true;
            this.cbOffline.Location = new System.Drawing.Point(271, 86);
            this.cbOffline.Name = "cbOffline";
            this.cbOffline.Size = new System.Drawing.Size(167, 33);
            this.cbOffline.TabIndex = 3;
            // 
            // lblOfflineMode
            // 
            this.lblOfflineMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOfflineMode.Location = new System.Drawing.Point(11, 89);
            this.lblOfflineMode.Name = "lblOfflineMode";
            this.lblOfflineMode.Size = new System.Drawing.Size(244, 23);
            this.lblOfflineMode.TabIndex = 2;
            this.lblOfflineMode.Text = "Offline Mode :";
            this.lblOfflineMode.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbAutoUpdate
            // 
            this.cbAutoUpdate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAutoUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAutoUpdate.FormattingEnabled = true;
            this.cbAutoUpdate.Location = new System.Drawing.Point(271, 29);
            this.cbAutoUpdate.Name = "cbAutoUpdate";
            this.cbAutoUpdate.Size = new System.Drawing.Size(167, 33);
            this.cbAutoUpdate.TabIndex = 1;
            // 
            // lblAutoUpdate
            // 
            this.lblAutoUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAutoUpdate.Location = new System.Drawing.Point(11, 32);
            this.lblAutoUpdate.Name = "lblAutoUpdate";
            this.lblAutoUpdate.Size = new System.Drawing.Size(244, 23);
            this.lblAutoUpdate.TabIndex = 0;
            this.lblAutoUpdate.Text = "Auto Update :";
            this.lblAutoUpdate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.txtSeriesLoc);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.txtMediaLoc);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.txtMediaExt);
            this.tabPage2.Location = new System.Drawing.Point(4, 31);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(802, 396);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "File";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(220, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(182, 31);
            this.label8.TabIndex = 15;
            this.label8.Text = "*Media Locations :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtMediaLoc
            // 
            this.txtMediaLoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMediaLoc.Location = new System.Drawing.Point(225, 49);
            this.txtMediaLoc.Multiline = true;
            this.txtMediaLoc.Name = "txtMediaLoc";
            this.txtMediaLoc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMediaLoc.Size = new System.Drawing.Size(566, 139);
            this.txtMediaLoc.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(196, 31);
            this.label5.TabIndex = 9;
            this.label5.Text = "*Media File Format :";
            // 
            // txtMediaExt
            // 
            this.txtMediaExt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMediaExt.Location = new System.Drawing.Point(10, 49);
            this.txtMediaExt.Multiline = true;
            this.txtMediaExt.Name = "txtMediaExt";
            this.txtMediaExt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMediaExt.Size = new System.Drawing.Size(194, 325);
            this.txtMediaExt.TabIndex = 8;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(572, 433);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(226, 59);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(355, 433);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(198, 59);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(-1, 434);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(335, 34);
            this.label9.TabIndex = 16;
            this.label9.Text = "* Changes Apply after Restart";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.txtCountry);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.txtGenre);
            this.tabPage3.Location = new System.Drawing.Point(4, 31);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(802, 396);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Filters";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 185);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 31);
            this.label7.TabIndex = 17;
            this.label7.Text = "Country :";
            // 
            // txtCountry
            // 
            this.txtCountry.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCountry.Location = new System.Drawing.Point(13, 219);
            this.txtCountry.Multiline = true;
            this.txtCountry.Name = "txtCountry";
            this.txtCountry.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCountry.Size = new System.Drawing.Size(767, 161);
            this.txtCountry.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 31);
            this.label6.TabIndex = 15;
            this.label6.Text = "Genre :";
            // 
            // txtGenre
            // 
            this.txtGenre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGenre.Location = new System.Drawing.Point(13, 46);
            this.txtGenre.Multiline = true;
            this.txtGenre.Name = "txtGenre";
            this.txtGenre.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGenre.Size = new System.Drawing.Size(767, 126);
            this.txtGenre.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(220, 202);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 31);
            this.label1.TabIndex = 17;
            this.label1.Text = "*Series Locations :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtSeriesLoc
            // 
            this.txtSeriesLoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSeriesLoc.Location = new System.Drawing.Point(225, 236);
            this.txtSeriesLoc.Multiline = true;
            this.txtSeriesLoc.Name = "txtSeriesLoc";
            this.txtSeriesLoc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSeriesLoc.Size = new System.Drawing.Size(566, 139);
            this.txtSeriesLoc.TabIndex = 16;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 500);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
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
            this.tabPage3.PerformLayout();
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
        private System.Windows.Forms.TextBox txtMediaLoc;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblItemDisplayCount;
        private System.Windows.Forms.TextBox txtMaxItemCount;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCountry;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtGenre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSeriesLoc;
    }
}