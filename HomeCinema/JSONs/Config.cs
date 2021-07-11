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
    public class Config
    {
        // Settings Config
        // Booleans
        public int autoUpdate { get; set; } = 1;
        public int offlineMode { get; set; } = 0;
        public int instantPlayMovie { get; set; } = 1;
        public int autoClean { get; set; } = 1; // Auto clean during startup
        public int confirmSearch { get; set; } = 0; // confirm prompt for searching and/or reloading items
        // Strings PATH
        public string lastPathVideo { get; set; } = "";
        public string lastPathCover { get; set; } = "";
        // Integers
        public int logsize { get; set; } = 1;
        public int itemMaxLimit { get; set; } = 0;
        public int searchLimit { get; set; } = 5;
        public int setTimeOut { get; set; } = 3;

        // Theme
        public string BackgroundColor { get; set; } = System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.Black);
        public string FontColor { get; set; } = System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.White);
        public int ImgTileWidth { get; set; } = 96;
        public int ImgTileHeight { get; set; } = 128;
    }
}
