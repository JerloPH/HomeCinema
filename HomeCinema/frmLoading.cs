using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HomeCinema.Properties;

namespace HomeCinema
{
    public partial class frmLoading : Form
    {
        public string Caption
        {
            get { return this.Text; }
            set { this.Text = value; }
        }
        public string Message
        {
            get { return label1.Text; }
            set
            {
                if (label1.InvokeRequired)
                {
                    BeginInvoke((Action)delegate
                    {
                        label1.Text = value;
                    });
                }
                else
                    label1.Text = value;
            }
        }
        public string ProgressText
        {
            get { return lblProgress.Text; }
            set
            {
                if (lblProgress.InvokeRequired)
                {
                    BeginInvoke((Action)delegate
                    {
                        lblProgress.Text = value;
                    });
                }
                else
                    lblProgress.Text = value;
            }
        }
        private long MaxProgressHidden = 0;
        public long MaxProgress
        {
            get { return MaxProgressHidden; }
            set { MaxProgressHidden = value; }
        }
        private long ProgressCountHidden = 0;
        public long ProgressCount
        {
            get { return ProgressCountHidden; }
            set { ProgressCountHidden = value; }
        }


        public frmLoading()
        {
            InitializeComponent();
        }
        public frmLoading(string message, string caption, bool useProgress = false)
        {
            InitializeComponent();
            Icon = GlobalVars.HOMECINEMA_ICON;
            BackColor = Settings.ColorBg;
            ForeColor = Settings.ColorFont;
            Message = message;
            Caption = caption;
            CenterToParent();
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BringToFront();
            if (useProgress)
            {
                this.BackgroundWorker.WorkerReportsProgress = true;
                this.BackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(this.BackgroundWorker_ProgressChanged);
            }
            lblProgress.Visible = useProgress;
        }
        #region Public Functions
        public void UpdateProgress(long progress)
        {
            try
            {
                decimal percent = ((decimal)progress / (decimal)MaxProgress) * 100m;
                if (progress == MaxProgress)
                    percent = 100m;

                GlobalVars.LogDebug($"Percent: [{percent.ToString("###.##")}], Progress: [{progress}], Max Progress: [{MaxProgress}]");
                BackgroundWorker.ReportProgress((int)percent, progress);
            }
            catch (Exception ex)
            {
                GlobalVars.ShowError("frmLoading-UpdateProgress", ex, false);
            }
        }
        public void SetIcon(int IconIndex = 0)
        {
            Bitmap image = null;
            try
            {
                switch (IconIndex)
                {
                    case (int)HCIcons.Check:
                        image = Resources.IconCheckmark;
                        break;
                    case (int)HCIcons.Warning:
                        image = Resources.IconWarning;
                        break;
                    default:
                        image = Resources.LoadingColored;
                        break;
                }
                if (pictureBox1.InvokeRequired)
                {
                    this.BeginInvoke((Action)delegate
                    {
                        pictureBox1.Image?.Dispose();
                        pictureBox1.Image = image;
                    });
                }
                else
                {
                    pictureBox1.Image?.Dispose();
                    pictureBox1.Image = image;
                }
                
            }
            catch (Exception ex) { GlobalVars.ShowError("frmLoading-SetIcon", ex, false, this); }
        }
        private delegate void UpdateMessageThreadSafeDelegate(string message);
        public void UpdateMessage(string message)
        {
            if (label1.InvokeRequired)
            {
                label1.Invoke(new UpdateMessageThreadSafeDelegate(UpdateMessage), new object[] { message });
            }
            else
            {
                label1.Text = message;
            }
        }
        #endregion

        private void frmLoading_Shown(object sender, EventArgs e)
        {
            if (BackgroundWorker.IsBusy)
                return;
            BackgroundWorker.RunWorkerAsync();
        }

        private void frmLoading_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (BackgroundWorker.IsBusy)
                e.Cancel = true;

            pictureBox1.Image?.Dispose();
            Dispose();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (MaxProgress < 1)
                Close();
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage > -1 && MaxProgress > 0)
            {
                try
                {
                    ProgressCount = Convert.ToInt64(e.UserState);
                }
                catch { ProgressCount += 1; }
            }
            //GlobalVars.LogDebug($"Progress %: [{e.ProgressPercentage}], Actual Progress: [{ProgressCount}]");
            if (ProgressCount == MaxProgress)
            {
                GlobalVars.LogDebug($"Done loading!\nProgress %: [{e.ProgressPercentage}], Actual Progress: [{ProgressCount}]\n");
                ProgressText = $"Done loading!";
                SetIcon((int)HCIcons.Check);
                if (Settings.IsConfirmMsg)
                    GlobalVars.ShowInfo("Done loading!", "", this.ParentForm);

                Close();
            }
            else
                ProgressText = $"Progress: {ProgressCount} / {MaxProgress}";
        }

        private void frmLoading_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
