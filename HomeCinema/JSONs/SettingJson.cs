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

namespace HomeCinema
{
    public class SettingJson
    {
        // Settings Config
        // Booleans
        public bool autoUpdate { get; set; } = true;
        public bool offlineMode { get; set; } = false;
        public bool instantPlayMovie { get; set; } = true;
        public bool autoClean { get; set; } = false; // Auto clean during startup
        public bool confirmSearch { get; set; } = false; // confirm prompt for searching and/or reloading items
        public bool confirmMessages { get; set; } = true; // Show confirmation dialogs to most actions
        // Strings PATH
        public string lastPathVideo { get; set; } = "";
        public string lastPathCover { get; set; } = "";
        // Integers
        public int logsize { get; set; } = 1;
        public int itemMaxLimit { get; set; } = 0;
        public int searchLimit { get; set; } = 5;
        public int setTimeOut { get; set; } = 10000; // 10 secs default timeout

        // Theme
        public string BackgroundColor { get; set; } = System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.Black);
        public string FontColor { get; set; } = System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.White);
        public int ImgTileWidth { get; set; } = 96;
        public int ImgTileHeight { get; set; } = 128;
    }
}
