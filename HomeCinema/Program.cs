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
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace HomeCinema
{
    static class Program
    {
        public static frmMain FormMain;
        static readonly Assembly assembly = Assembly.GetExecutingAssembly();
        static readonly GuidAttribute attrib = (GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
        static readonly Mutex mutex = new Mutex(true, "HomeCinema"+ attrib.Value);
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

                // Check files first
                if (DataFile.Initialize())
                {
                    DataFile.CheckAllFiles();
                    Settings.LoadSettings(); // Load App Settings
                    FormMain = new frmMain();
                    Application.Run(FormMain);
                }

                // release mutex after the form is closed.
                mutex.ReleaseMutex();
                mutex.Dispose();
            }
            else
            {
                Msg.ShowNoParent("HomeCinema is already open!");
            }
        }
    }
}
