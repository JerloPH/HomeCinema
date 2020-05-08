/* #####################################################################################
 * LICENSE - GPL v3
* HomeCinema - Organize your Movie Collection
* Copyright (C) 2020  JerloPH (https://github.com/JerloPH)

* This program is free software: you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation, either version 3 of the License, or
* (at your option) any later version.

* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.

* You should have received a copy of the GNU General Public License
* along with this program.  If not, see <https://www.gnu.org/licenses/>.
##################################################################################### */
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
            Icon = GlobalVars.HOMECINEMA_ICON;
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
