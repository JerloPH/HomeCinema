using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeCinema
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
            // Form properties
            Icon = GlobalVars.HOMECINEMA_ICON;
            txtLicense.Text = @"
    HomeCinema - Organize your Movie Collection
    Copyright (C) 2020  JerloPH (https://github.com/JerloPH)

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
            ";
            //txtLicense.BackColor = System.Drawing.SystemColors.Window;
        }
        // ######################################################### Methods
        // ######################################################### Events
        private void frmAbout_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Closed
            GlobalVars.formAbout = null;
        }
    }
}
