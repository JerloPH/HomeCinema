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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnShowNew = new System.Windows.Forms.Button();
            this.btnSort = new System.Windows.Forms.Button();
            this.btnAddMovie = new System.Windows.Forms.Button();
            this.btnChangeView = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbClearSearch = new System.Windows.Forms.CheckBox();
            this.cbGenre = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbCountry = new System.Windows.Forms.ComboBox();
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtCast = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtStudio = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtYearTo = new System.Windows.Forms.TextBox();
            this.txtYearFrom = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIMDB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lvSearchResult = new System.Windows.Forms.ListView();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.btnShowNew);
            this.groupBox2.Controls.Add(this.btnSort);
            this.groupBox2.Controls.Add(this.btnAddMovie);
            this.groupBox2.Controls.Add(this.btnChangeView);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(1, 149);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1167, 61);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Controls";
            // 
            // btnShowNew
            // 
            this.btnShowNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnShowNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnShowNew.Location = new System.Drawing.Point(758, 21);
            this.btnShowNew.Name = "btnShowNew";
            this.btnShowNew.Size = new System.Drawing.Size(244, 40);
            this.btnShowNew.TabIndex = 18;
            this.btnShowNew.Text = "Show New";
            this.btnShowNew.UseVisualStyleBackColor = false;
            this.btnShowNew.Click += new System.EventHandler(this.btnShowNew_Click);
            // 
            // btnSort
            // 
            this.btnSort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSort.Location = new System.Drawing.Point(258, 21);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(244, 40);
            this.btnSort.TabIndex = 17;
            this.btnSort.Text = "Sort By";
            this.btnSort.UseVisualStyleBackColor = false;
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // btnAddMovie
            // 
            this.btnAddMovie.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnAddMovie.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddMovie.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAddMovie.Location = new System.Drawing.Point(508, 21);
            this.btnAddMovie.Name = "btnAddMovie";
            this.btnAddMovie.Size = new System.Drawing.Size(244, 40);
            this.btnAddMovie.TabIndex = 15;
            this.btnAddMovie.Text = "Add Movie";
            this.btnAddMovie.UseVisualStyleBackColor = false;
            this.btnAddMovie.Click += new System.EventHandler(this.btnAddMovie_Click);
            // 
            // btnChangeView
            // 
            this.btnChangeView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnChangeView.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnChangeView.Location = new System.Drawing.Point(6, 21);
            this.btnChangeView.Name = "btnChangeView";
            this.btnChangeView.Size = new System.Drawing.Size(244, 40);
            this.btnChangeView.TabIndex = 14;
            this.btnChangeView.Text = "Change View";
            this.btnChangeView.UseVisualStyleBackColor = false;
            this.btnChangeView.Click += new System.EventHandler(this.btnChangeView_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.cbClearSearch);
            this.groupBox1.Controls.Add(this.cbGenre);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cbCountry);
            this.groupBox1.Controls.Add(this.cbCategory);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.txtCast);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtStudio);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtYearTo);
            this.groupBox1.Controls.Add(this.txtYearFrom);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtIMDB);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtSearch);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(1, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1172, 145);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // cbClearSearch
            // 
            this.cbClearSearch.AutoSize = true;
            this.cbClearSearch.Location = new System.Drawing.Point(960, 99);
            this.cbClearSearch.Name = "cbClearSearch";
            this.cbClearSearch.Size = new System.Drawing.Size(168, 24);
            this.cbClearSearch.TabIndex = 23;
            this.cbClearSearch.Text = "Search after Clear";
            this.cbClearSearch.UseVisualStyleBackColor = true;
            // 
            // cbGenre
            // 
            this.cbGenre.BackColor = System.Drawing.Color.Silver;
            this.cbGenre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGenre.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGenre.ForeColor = System.Drawing.Color.Black;
            this.cbGenre.FormattingEnabled = true;
            this.cbGenre.Location = new System.Drawing.Point(888, 61);
            this.cbGenre.Name = "cbGenre";
            this.cbGenre.Size = new System.Drawing.Size(172, 30);
            this.cbGenre.TabIndex = 22;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(809, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 24);
            this.label8.TabIndex = 21;
            this.label8.Text = "Genre :";
            // 
            // cbCountry
            // 
            this.cbCountry.BackColor = System.Drawing.Color.Silver;
            this.cbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCountry.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCountry.ForeColor = System.Drawing.Color.Black;
            this.cbCountry.FormattingEnabled = true;
            this.cbCountry.Location = new System.Drawing.Point(629, 95);
            this.cbCountry.Name = "cbCountry";
            this.cbCountry.Size = new System.Drawing.Size(172, 30);
            this.cbCountry.TabIndex = 20;
            // 
            // cbCategory
            // 
            this.cbCategory.BackColor = System.Drawing.Color.Silver;
            this.cbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCategory.ForeColor = System.Drawing.Color.Black;
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Location = new System.Drawing.Point(629, 58);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(172, 30);
            this.cbCategory.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(523, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 24);
            this.label7.TabIndex = 18;
            this.label7.Text = "Country :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(523, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 24);
            this.label6.TabIndex = 16;
            this.label6.Text = "Category : ";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSearch.Location = new System.Drawing.Point(995, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(166, 40);
            this.btnSearch.TabIndex = 15;
            this.btnSearch.Text = "SEARCH";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnClear.Location = new System.Drawing.Point(807, 94);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(147, 38);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtCast
            // 
            this.txtCast.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCast.Location = new System.Drawing.Point(331, 97);
            this.txtCast.Name = "txtCast";
            this.txtCast.Size = new System.Drawing.Size(185, 28);
            this.txtCast.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(259, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 24);
            this.label5.TabIndex = 11;
            this.label5.Text = "Cast : ";
            // 
            // txtStudio
            // 
            this.txtStudio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStudio.Location = new System.Drawing.Point(331, 61);
            this.txtStudio.Name = "txtStudio";
            this.txtStudio.Size = new System.Drawing.Size(185, 28);
            this.txtStudio.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(259, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 24);
            this.label4.TabIndex = 9;
            this.label4.Text = "Studio : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(152, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 24);
            this.label3.TabIndex = 8;
            this.label3.Text = "-";
            // 
            // txtYearTo
            // 
            this.txtYearTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYearTo.Location = new System.Drawing.Point(172, 95);
            this.txtYearTo.Name = "txtYearTo";
            this.txtYearTo.Size = new System.Drawing.Size(82, 28);
            this.txtYearTo.TabIndex = 7;
            // 
            // txtYearFrom
            // 
            this.txtYearFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYearFrom.Location = new System.Drawing.Point(68, 95);
            this.txtYearFrom.Name = "txtYearFrom";
            this.txtYearFrom.Size = new System.Drawing.Size(81, 28);
            this.txtYearFrom.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "Year : ";
            // 
            // txtIMDB
            // 
            this.txtIMDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIMDB.Location = new System.Drawing.Point(69, 61);
            this.txtIMDB.Name = "txtIMDB";
            this.txtIMDB.Size = new System.Drawing.Size(185, 28);
            this.txtIMDB.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "IMDB : ";
            // 
            // txtSearch
            // 
            this.txtSearch.AccessibleDescription = "Search here";
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(3, 21);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(986, 34);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // lvSearchResult
            // 
            this.lvSearchResult.BackColor = System.Drawing.Color.Black;
            this.lvSearchResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvSearchResult.ForeColor = System.Drawing.Color.White;
            this.lvSearchResult.HideSelection = false;
            this.lvSearchResult.Location = new System.Drawing.Point(6, 212);
            this.lvSearchResult.Name = "lvSearchResult";
            this.lvSearchResult.Size = new System.Drawing.Size(1167, 461);
            this.lvSearchResult.TabIndex = 20;
            this.lvSearchResult.UseCompatibleStateImageBehavior = false;
            this.lvSearchResult.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvSearchResult_MouseDoubleClick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1178, 673);
            this.Controls.Add(this.lvSearchResult);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.Black;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home Cinema";
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox2;
        private Button btnSort;
        private Button btnAddMovie;
        private Button btnChangeView;
        private GroupBox groupBox1;
        private ComboBox cbCountry;
        private ComboBox cbCategory;
        private Label label7;
        private Label label6;
        internal Button btnSearch;
        private Button btnClear;
        private TextBox txtCast;
        private Label label5;
        private TextBox txtStudio;
        private Label label4;
        private Label label3;
        private TextBox txtYearTo;
        private TextBox txtYearFrom;
        private Label label2;
        private TextBox txtIMDB;
        private Label label1;
        private TextBox txtSearch;
        internal ListView lvSearchResult;
        private ComboBox cbGenre;
        private Label label8;
        private Button btnShowNew;
        private CheckBox cbClearSearch;
    }
}

