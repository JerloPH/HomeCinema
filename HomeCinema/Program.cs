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
using System;
using System.Threading;
using System.Windows.Forms;

namespace HomeCinema
{
    static class Program
    {
        public static frmMain FormMain;
        static readonly Mutex mutex = new Mutex(true, "a1f8da5c-12ef-45a5-b4c5-d8fece8e3e32");
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                // Default setup
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Pre-load setup
                // Create directories
                GlobalVars.CreateDir(GlobalVars.PATH_IMG);
                GlobalVars.CreateDir(GlobalVars.PATH_DATA);
                GlobalVars.CreateDir(GlobalVars.PATH_TEMP);
                GlobalVars.CreateDir(GlobalVars.PATH_LOG);

                // Check files first
                GlobalVars.CheckAllFiles();

                FormMain = new frmMain();
                Application.Run(FormMain);

                // release mutex after the form is closed.
                mutex.ReleaseMutex();
                mutex.Dispose();
            }
            else
            {
                GlobalVars.ShowNoParent("HomeCinema is already open!");
            }
        }
    }
}
