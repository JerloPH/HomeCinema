
namespace HomeCinema
{
    partial class frmAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tpHome = new System.Windows.Forms.TabPage();
            this.webHC = new System.Windows.Forms.WebBrowser();
            this.tpLicense = new System.Windows.Forms.TabPage();
            this.tabLicense = new System.Windows.Forms.TabControl();
            this.tpLHC = new System.Windows.Forms.TabPage();
            this.txtLicense = new System.Windows.Forms.TextBox();
            this.tpLMS = new System.Windows.Forms.TabPage();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.tpLNewton = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tpLRest = new System.Windows.Forms.TabPage();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tpLSqlite = new System.Windows.Forms.TabPage();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.tpLECpanel = new System.Windows.Forms.TabPage();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.tpMarkdig = new System.Windows.Forms.TabPage();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.tpWinApi = new System.Windows.Forms.TabPage();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.tpCredits = new System.Windows.Forms.TabPage();
            this.txtCredit = new System.Windows.Forms.TextBox();
            this.btnCheckUpdate = new System.Windows.Forms.Button();
            this.picTmdb = new System.Windows.Forms.PictureBox();
            this.tabMain.SuspendLayout();
            this.tpHome.SuspendLayout();
            this.tpLicense.SuspendLayout();
            this.tabLicense.SuspendLayout();
            this.tpLHC.SuspendLayout();
            this.tpLMS.SuspendLayout();
            this.tpLNewton.SuspendLayout();
            this.tpLRest.SuspendLayout();
            this.tpLSqlite.SuspendLayout();
            this.tpLECpanel.SuspendLayout();
            this.tpMarkdig.SuspendLayout();
            this.tpWinApi.SuspendLayout();
            this.tpCredits.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTmdb)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTitle
            // 
            this.txtTitle.BackColor = System.Drawing.Color.Black;
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtTitle.Location = new System.Drawing.Point(12, 12);
            this.txtTitle.Multiline = true;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.ReadOnly = true;
            this.txtTitle.ShortcutsEnabled = false;
            this.txtTitle.Size = new System.Drawing.Size(566, 75);
            this.txtTitle.TabIndex = 2;
            this.txtTitle.TabStop = false;
            this.txtTitle.Text = "HomeCinema - Organize your Movie Collection\r\nJerloPH (https://github.com/JerloPH)" +
    "";
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tpHome);
            this.tabMain.Controls.Add(this.tpLicense);
            this.tabMain.Controls.Add(this.tpCredits);
            this.tabMain.Location = new System.Drawing.Point(12, 93);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(803, 390);
            this.tabMain.TabIndex = 3;
            // 
            // tpHome
            // 
            this.tpHome.Controls.Add(this.webHC);
            this.tpHome.ForeColor = System.Drawing.Color.White;
            this.tpHome.Location = new System.Drawing.Point(4, 29);
            this.tpHome.Name = "tpHome";
            this.tpHome.Padding = new System.Windows.Forms.Padding(3);
            this.tpHome.Size = new System.Drawing.Size(795, 357);
            this.tpHome.TabIndex = 0;
            this.tpHome.Text = "HomeCinema";
            this.tpHome.UseVisualStyleBackColor = true;
            // 
            // webHC
            // 
            this.webHC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webHC.Location = new System.Drawing.Point(3, 3);
            this.webHC.MinimumSize = new System.Drawing.Size(20, 20);
            this.webHC.Name = "webHC";
            this.webHC.Size = new System.Drawing.Size(789, 351);
            this.webHC.TabIndex = 19;
            this.webHC.WebBrowserShortcutsEnabled = false;
            // 
            // tpLicense
            // 
            this.tpLicense.Controls.Add(this.tabLicense);
            this.tpLicense.Location = new System.Drawing.Point(4, 29);
            this.tpLicense.Name = "tpLicense";
            this.tpLicense.Padding = new System.Windows.Forms.Padding(3);
            this.tpLicense.Size = new System.Drawing.Size(795, 357);
            this.tpLicense.TabIndex = 1;
            this.tpLicense.Text = "License";
            this.tpLicense.UseVisualStyleBackColor = true;
            // 
            // tabLicense
            // 
            this.tabLicense.Controls.Add(this.tpLHC);
            this.tabLicense.Controls.Add(this.tpLMS);
            this.tabLicense.Controls.Add(this.tpLNewton);
            this.tabLicense.Controls.Add(this.tpLRest);
            this.tabLicense.Controls.Add(this.tpLSqlite);
            this.tabLicense.Controls.Add(this.tpLECpanel);
            this.tabLicense.Controls.Add(this.tpMarkdig);
            this.tabLicense.Controls.Add(this.tpWinApi);
            this.tabLicense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabLicense.Location = new System.Drawing.Point(3, 3);
            this.tabLicense.Name = "tabLicense";
            this.tabLicense.SelectedIndex = 0;
            this.tabLicense.Size = new System.Drawing.Size(789, 351);
            this.tabLicense.TabIndex = 0;
            // 
            // tpLHC
            // 
            this.tpLHC.Controls.Add(this.txtLicense);
            this.tpLHC.Location = new System.Drawing.Point(4, 29);
            this.tpLHC.Name = "tpLHC";
            this.tpLHC.Padding = new System.Windows.Forms.Padding(3);
            this.tpLHC.Size = new System.Drawing.Size(781, 318);
            this.tpLHC.TabIndex = 0;
            this.tpLHC.Text = "HomeCinema";
            this.tpLHC.UseVisualStyleBackColor = true;
            // 
            // txtLicense
            // 
            this.txtLicense.BackColor = System.Drawing.Color.Black;
            this.txtLicense.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLicense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLicense.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicense.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtLicense.Location = new System.Drawing.Point(3, 3);
            this.txtLicense.Multiline = true;
            this.txtLicense.Name = "txtLicense";
            this.txtLicense.ReadOnly = true;
            this.txtLicense.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLicense.ShortcutsEnabled = false;
            this.txtLicense.Size = new System.Drawing.Size(775, 312);
            this.txtLicense.TabIndex = 3;
            this.txtLicense.TabStop = false;
            this.txtLicense.Text = resources.GetString("txtLicense.Text");
            // 
            // tpLMS
            // 
            this.tpLMS.Controls.Add(this.textBox6);
            this.tpLMS.Location = new System.Drawing.Point(4, 29);
            this.tpLMS.Name = "tpLMS";
            this.tpLMS.Size = new System.Drawing.Size(781, 318);
            this.tpLMS.TabIndex = 6;
            this.tpLMS.Text = "dotNet";
            this.tpLMS.UseVisualStyleBackColor = true;
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.Black;
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox6.Location = new System.Drawing.Point(0, 0);
            this.textBox6.Multiline = true;
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox6.ShortcutsEnabled = false;
            this.textBox6.Size = new System.Drawing.Size(781, 318);
            this.textBox6.TabIndex = 5;
            this.textBox6.TabStop = false;
            this.textBox6.Text = resources.GetString("textBox6.Text");
            // 
            // tpLNewton
            // 
            this.tpLNewton.Controls.Add(this.textBox1);
            this.tpLNewton.Location = new System.Drawing.Point(4, 29);
            this.tpLNewton.Name = "tpLNewton";
            this.tpLNewton.Padding = new System.Windows.Forms.Padding(3);
            this.tpLNewton.Size = new System.Drawing.Size(781, 318);
            this.tpLNewton.TabIndex = 1;
            this.tpLNewton.Text = "Newtonsoft.Json";
            this.tpLNewton.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Black;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.ShortcutsEnabled = false;
            this.textBox1.Size = new System.Drawing.Size(775, 312);
            this.textBox1.TabIndex = 4;
            this.textBox1.TabStop = false;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // tpLRest
            // 
            this.tpLRest.Controls.Add(this.textBox2);
            this.tpLRest.Location = new System.Drawing.Point(4, 29);
            this.tpLRest.Name = "tpLRest";
            this.tpLRest.Size = new System.Drawing.Size(781, 318);
            this.tpLRest.TabIndex = 2;
            this.tpLRest.Text = "RestSharp";
            this.tpLRest.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.Black;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox2.Location = new System.Drawing.Point(0, 0);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.ShortcutsEnabled = false;
            this.textBox2.Size = new System.Drawing.Size(781, 318);
            this.textBox2.TabIndex = 5;
            this.textBox2.TabStop = false;
            this.textBox2.Text = resources.GetString("textBox2.Text");
            // 
            // tpLSqlite
            // 
            this.tpLSqlite.Controls.Add(this.textBox3);
            this.tpLSqlite.Location = new System.Drawing.Point(4, 29);
            this.tpLSqlite.Name = "tpLSqlite";
            this.tpLSqlite.Size = new System.Drawing.Size(781, 322);
            this.tpLSqlite.TabIndex = 3;
            this.tpLSqlite.Text = "SQLite";
            this.tpLSqlite.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.Black;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox3.Location = new System.Drawing.Point(0, 0);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox3.ShortcutsEnabled = false;
            this.textBox3.Size = new System.Drawing.Size(781, 322);
            this.textBox3.TabIndex = 5;
            this.textBox3.TabStop = false;
            this.textBox3.Text = resources.GetString("textBox3.Text");
            // 
            // tpLECpanel
            // 
            this.tpLECpanel.Controls.Add(this.textBox4);
            this.tpLECpanel.Location = new System.Drawing.Point(4, 29);
            this.tpLECpanel.Name = "tpLECpanel";
            this.tpLECpanel.Size = new System.Drawing.Size(781, 322);
            this.tpLECpanel.TabIndex = 4;
            this.tpLECpanel.Text = "ExpandCollapsePanel";
            this.tpLECpanel.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.Black;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox4.Location = new System.Drawing.Point(0, 0);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox4.ShortcutsEnabled = false;
            this.textBox4.Size = new System.Drawing.Size(781, 322);
            this.textBox4.TabIndex = 5;
            this.textBox4.TabStop = false;
            this.textBox4.Text = resources.GetString("textBox4.Text");
            // 
            // tpMarkdig
            // 
            this.tpMarkdig.Controls.Add(this.textBox7);
            this.tpMarkdig.Location = new System.Drawing.Point(4, 29);
            this.tpMarkdig.Name = "tpMarkdig";
            this.tpMarkdig.Size = new System.Drawing.Size(781, 322);
            this.tpMarkdig.TabIndex = 7;
            this.tpMarkdig.Text = "Markdig";
            this.tpMarkdig.UseVisualStyleBackColor = true;
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.Color.Black;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox7.Location = new System.Drawing.Point(0, 0);
            this.textBox7.Multiline = true;
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox7.ShortcutsEnabled = false;
            this.textBox7.Size = new System.Drawing.Size(781, 322);
            this.textBox7.TabIndex = 6;
            this.textBox7.TabStop = false;
            this.textBox7.Text = resources.GetString("textBox7.Text");
            // 
            // tpWinApi
            // 
            this.tpWinApi.Controls.Add(this.textBox5);
            this.tpWinApi.Location = new System.Drawing.Point(4, 29);
            this.tpWinApi.Name = "tpWinApi";
            this.tpWinApi.Size = new System.Drawing.Size(781, 322);
            this.tpWinApi.TabIndex = 5;
            this.tpWinApi.Text = "Windows API";
            this.tpWinApi.UseVisualStyleBackColor = true;
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.Black;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBox5.Location = new System.Drawing.Point(0, 0);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox5.ShortcutsEnabled = false;
            this.textBox5.Size = new System.Drawing.Size(781, 322);
            this.textBox5.TabIndex = 5;
            this.textBox5.TabStop = false;
            this.textBox5.Text = resources.GetString("textBox5.Text");
            // 
            // tpCredits
            // 
            this.tpCredits.Controls.Add(this.picTmdb);
            this.tpCredits.Controls.Add(this.txtCredit);
            this.tpCredits.Location = new System.Drawing.Point(4, 29);
            this.tpCredits.Name = "tpCredits";
            this.tpCredits.Padding = new System.Windows.Forms.Padding(3);
            this.tpCredits.Size = new System.Drawing.Size(795, 357);
            this.tpCredits.TabIndex = 2;
            this.tpCredits.Text = "Credits";
            this.tpCredits.UseVisualStyleBackColor = true;
            // 
            // txtCredit
            // 
            this.txtCredit.BackColor = System.Drawing.Color.Black;
            this.txtCredit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCredit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCredit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCredit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtCredit.Location = new System.Drawing.Point(3, 3);
            this.txtCredit.Multiline = true;
            this.txtCredit.Name = "txtCredit";
            this.txtCredit.ReadOnly = true;
            this.txtCredit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCredit.ShortcutsEnabled = false;
            this.txtCredit.Size = new System.Drawing.Size(789, 351);
            this.txtCredit.TabIndex = 5;
            this.txtCredit.TabStop = false;
            this.txtCredit.Text = resources.GetString("txtCredit.Text");
            // 
            // btnCheckUpdate
            // 
            this.btnCheckUpdate.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnCheckUpdate.ForeColor = System.Drawing.Color.Black;
            this.btnCheckUpdate.Location = new System.Drawing.Point(602, 12);
            this.btnCheckUpdate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCheckUpdate.Name = "btnCheckUpdate";
            this.btnCheckUpdate.Size = new System.Drawing.Size(213, 45);
            this.btnCheckUpdate.TabIndex = 18;
            this.btnCheckUpdate.Text = "Check for Update";
            this.btnCheckUpdate.UseVisualStyleBackColor = true;
            this.btnCheckUpdate.Click += new System.EventHandler(this.btnCheckUpdate_Click);
            // 
            // picTmdb
            // 
            this.picTmdb.BackColor = System.Drawing.Color.Transparent;
            this.picTmdb.Image = global::HomeCinema.Properties.Resources.TMDB_Logo;
            this.picTmdb.Location = new System.Drawing.Point(647, 15);
            this.picTmdb.Name = "picTmdb";
            this.picTmdb.Size = new System.Drawing.Size(114, 102);
            this.picTmdb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picTmdb.TabIndex = 6;
            this.picTmdb.TabStop = false;
            // 
            // frmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(827, 495);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.btnCheckUpdate);
            this.Controls.Add(this.txtTitle);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(845, 542);
            this.Name = "frmAbout";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About HomeCinema";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAbout_FormClosing);
            this.Load += new System.EventHandler(this.frmAbout_Load);
            this.Resize += new System.EventHandler(this.frmAbout_Resize);
            this.tabMain.ResumeLayout(false);
            this.tpHome.ResumeLayout(false);
            this.tpLicense.ResumeLayout(false);
            this.tabLicense.ResumeLayout(false);
            this.tpLHC.ResumeLayout(false);
            this.tpLHC.PerformLayout();
            this.tpLMS.ResumeLayout(false);
            this.tpLMS.PerformLayout();
            this.tpLNewton.ResumeLayout(false);
            this.tpLNewton.PerformLayout();
            this.tpLRest.ResumeLayout(false);
            this.tpLRest.PerformLayout();
            this.tpLSqlite.ResumeLayout(false);
            this.tpLSqlite.PerformLayout();
            this.tpLECpanel.ResumeLayout(false);
            this.tpLECpanel.PerformLayout();
            this.tpMarkdig.ResumeLayout(false);
            this.tpMarkdig.PerformLayout();
            this.tpWinApi.ResumeLayout(false);
            this.tpWinApi.PerformLayout();
            this.tpCredits.ResumeLayout(false);
            this.tpCredits.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTmdb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tpHome;
        private System.Windows.Forms.TabPage tpLicense;
        private System.Windows.Forms.TabControl tabLicense;
        private System.Windows.Forms.TabPage tpLHC;
        private System.Windows.Forms.TextBox txtLicense;
        private System.Windows.Forms.TabPage tpLNewton;
        private System.Windows.Forms.TabPage tpCredits;
        private System.Windows.Forms.Button btnCheckUpdate;
        private System.Windows.Forms.TabPage tpLRest;
        private System.Windows.Forms.TabPage tpLSqlite;
        private System.Windows.Forms.TabPage tpLECpanel;
        private System.Windows.Forms.TabPage tpWinApi;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TabPage tpLMS;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox txtCredit;
        private System.Windows.Forms.WebBrowser webHC;
        private System.Windows.Forms.TabPage tpMarkdig;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.PictureBox picTmdb;
    }
}