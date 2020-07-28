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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLogSize = new System.Windows.Forms.TextBox();
            this.cbPlayMovie = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbOffline = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbAutoUpdate = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(810, 445);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnCancel);
            this.tabPage1.Controls.Add(this.btnSave);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.txtLogSize);
            this.tabPage1.Controls.Add(this.cbPlayMovie);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.cbOffline);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.cbAutoUpdate);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(802, 416);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(580, 351);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(214, 59);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(379, 351);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(185, 59);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 215);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(198, 31);
            this.label4.TabIndex = 7;
            this.label4.Text = "Log File Size (MB) :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtLogSize
            // 
            this.txtLogSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLogSize.Location = new System.Drawing.Point(233, 209);
            this.txtLogSize.Name = "txtLogSize";
            this.txtLogSize.Size = new System.Drawing.Size(167, 30);
            this.txtLogSize.TabIndex = 6;
            // 
            // cbPlayMovie
            // 
            this.cbPlayMovie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlayMovie.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPlayMovie.FormattingEnabled = true;
            this.cbPlayMovie.Location = new System.Drawing.Point(233, 140);
            this.cbPlayMovie.Name = "cbPlayMovie";
            this.cbPlayMovie.Size = new System.Drawing.Size(167, 33);
            this.cbPlayMovie.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(203, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Play Movie on Click :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbOffline
            // 
            this.cbOffline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOffline.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOffline.FormattingEnabled = true;
            this.cbOffline.Location = new System.Drawing.Point(233, 86);
            this.cbOffline.Name = "cbOffline";
            this.cbOffline.Size = new System.Drawing.Size(167, 33);
            this.cbOffline.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Offline Mode :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbAutoUpdate
            // 
            this.cbAutoUpdate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAutoUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAutoUpdate.FormattingEnabled = true;
            this.cbAutoUpdate.Location = new System.Drawing.Point(233, 29);
            this.cbAutoUpdate.Name = "cbAutoUpdate";
            this.cbAutoUpdate.Size = new System.Drawing.Size(167, 33);
            this.cbAutoUpdate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Auto Update :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(802, 416);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 445);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmSettings";
            this.Text = "frmSettings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbOffline;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbAutoUpdate;
        private System.Windows.Forms.ComboBox cbPlayMovie;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLogSize;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}