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
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HomeCinema
{
    public class MovieInfo
    {
        [JsonProperty("id")]
        public string id { get; set; } = "";

        [JsonProperty("imdb_id")]
        public string imdb_id { get; set; } = "";

        [JsonProperty("title")]
        public string title { get; set; } = "";

        [JsonProperty("original_title")]
        public string original_title { get; set; } = "";

        [JsonProperty("release_date")]
        public string release_date { get; set; } = "";

        [JsonProperty("overview")]
        public string overview { get; set; } = "";

        [JsonProperty("poster_path")]
        public string poster_path { get; set; } = "";

        [JsonProperty("backdrop_path")]
        public string backdrop_path { get; set; } = "";

        [JsonProperty("production_companies")]
        public List<ProdCom> production_companies { get; set; }
    }

    public class ProdCom
    {
        [JsonProperty("origin_country")]
        public string origin_country { get; set; }
    }
}