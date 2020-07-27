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
        public int autoUpdate { get; set; } = 1;
        public int offlineMode { get; set; } = 0;
        public int logsize { get; set; } = 1;
        public string lastPathVideo { get; set; } = "";
        public string lastPathCover { get; set; } = "";
        public int instantPlayMovie { get; set; } = 1;
    }
}
