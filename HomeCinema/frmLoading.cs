using HomeCinema.Global;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HomeCinema
{
    public partial class frmLoading : Form
    {
        public frmLoading(Form parent)
        {
            InitializeComponent();
            Icon = new Icon(GlobalVars.FILE_ICON);
            //Show(parent);
            // Load image from app path and assign to picBox
            picBox.Image = GlobalVars.IMG_LOADING;
        }

        private void frmLoading_Load(object sender, EventArgs e)
        {
            // Load image from app path and assign to picBox
            if (picBox.Image == null)
            {
                picBox.Image = GlobalVars.IMG_LOADING;
            }
            // Center on screen
            CenterToParent();
            /*
            Rectangle r = new Rectangle();
            r = PARENT.RectangleToScreen(PARENT.ClientRectangle);

            int x = r.Left + ((r.Width - Width) / 2);
            int y = r.Top + ((r.Height - Height) / 2);
            Location = new Point(x, y);
            */
        }
    }
}
