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
    public class CastCrew
    {
        [JsonProperty("cast")]
        public List<Cast> cast { get; set; }

        [JsonProperty("crew")]
        public List<Crew> crew { get; set; }
    }

    public class Cast
    {
        [JsonProperty("cast_id")]
        public string cast_id { get; set; }

        [JsonProperty("character")]
        public string character { get; set; }

        [JsonProperty("credit_id")]
        public string credit_id { get; set; }

        [JsonProperty("gender")]
        public string gender { get; set; }

        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("order")]
        public string order { get; set; }

        [JsonProperty("profile_path")]
        public string profile_path { get; set; }
    }

    public class Crew
    {
        [JsonProperty("credit_id")]
        public string credit_id { get; set; }

        [JsonProperty("department")]
        public string department { get; set; }

        [JsonProperty("gender")]
        public string gender { get; set; }

        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("job")]
        public string job { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("profile_path")]
        public string profile_path { get; set; }
    }
}
