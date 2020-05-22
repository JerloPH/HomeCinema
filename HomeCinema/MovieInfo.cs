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
    public class MovieInfo
    {
        public string id { get; set; } = "";
        //public string imdb_id { get; set; } = "";
        public string title { get; set; } = "";
        public string original_title { get; set; } = "";
        public string release_date { get; set; } = "";
        public string overview { get; set; } = "";
        public string poster_path { get; set; } = "";
        public string backdrop_path { get; set; } = "";
    }
}