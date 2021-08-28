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
using System.Collections.Generic;
using System.IO;
using System.Data.SQLite;
using System.Data;
using Microsoft.WindowsAPICodePack.Shell;
using System.Drawing;
using System.Data.Common;
using System.Data.SqlClient;

namespace HomeCinema.SQLFunc
{
    public static class SQLHelper
    {
        private static string DB_NAME = "HomeCinemaDB.db";
        private static string DB_PATH = "";
        private static readonly int RetryCount = 5;

        // Methods for querying
        #region Query String Helper
        // Check if the column is a numeric column
        public static bool QryColNumeric(string colName)
        {
            string s = colName.ToLower();
            return ((s == "category") || (s == "year") || (s == "season") || (s == "episode"));
        }
        // Build a query, Use WHERE or AND on Filter
        public static string QryWhere(string qry)
        {
            return (qry.Contains("WHERE") == false) ? " WHERE " : " AND ";
        }
        // Build a query, If value is string or not
        public static string QryString(string text, bool UseSingleQuote)
        {
            if (String.IsNullOrWhiteSpace(text))
            {
                return (UseSingleQuote ? "''" : "null");
            }
            else
            {
                return (UseSingleQuote ? $"'{text}'" : $"{text}");
            }
        }
        #endregion

        /// <summary>
        /// Initialize SQLite database. Create if NOT existing.
        /// </summary>
        /// <param name="InitiatedFrom">Caller of the function.</param>
        public static bool Initiate(string InitiatedFrom)
        {
            DB_PATH = DataFile.PATH_START + DB_NAME;
            string CalledFrom = "SQLHelper (Initiate)-" + InitiatedFrom;
            int dbVersion = 1;
            bool loaded = true;
            bool IsNewDb = DbExecNonQuery($"CREATE TABLE IF NOT EXISTS '{HCTable.info}' (" +
                "'Id'	INTEGER PRIMARY KEY AUTOINCREMENT, " +
                $"'{HCInfo.Id}'	INTEGER, " +
                $"'{HCInfo.imdb}'  TEXT DEFAULT 0, " +
                $"'{HCInfo.anilist}'  TEXT DEFAULT 0, " +
                $"'{HCInfo.name}'  TEXT, " +
                $"'{HCInfo.name_orig}'  TEXT, " +
                $"'{HCInfo.name_series}'  TEXT, " +
                $"'{HCInfo.season}'  INTEGER, " +
                $"'{HCInfo.episode}'  INTEGER, " +
                $"'{HCInfo.country}'  TEXT, " +
                $"'{HCInfo.category}'  VARCHAR(1) DEFAULT 0, " + // 0-None | 1-MOVIE | 2-TVSERIES | 3-ANIMEMOVIE | 4-ANIME SERIES | 5-ANIMATED MOVIE | 6-CARTOON SERIES
                $"'{HCInfo.genre}'  TEXT, " +
                $"'{HCInfo.studio}'  TEXT, " +
                $"'{HCInfo.producer}'  TEXT, " +
                $"'{HCInfo.director}'  TEXT, " +
                $"'{HCInfo.artist}'  TEXT, " +
                $"'{HCInfo.year}'  VARCHAR(5) DEFAULT 0, " +
                $"'{HCInfo.summary}'  TEXT DEFAULT 'This has no summary');", CalledFrom, -1) == 0;
            DbExecNonQuery($"CREATE TABLE IF NOT EXISTS '{HCTable.filepath}' (" +
                "'Id'  INTEGER  PRIMARY KEY AUTOINCREMENT, " +
                $"'{HCFile.Id}'	INTEGER, " +
                $"[{HCFile.File}]  TEXT, " +
                $"[{HCFile.Root}]  TEXT, " +
                $"[{HCFile.Sub}]  TEXT, " +
                $"[{HCFile.Trailer}]  TEXT);", CalledFrom);
            DbExecNonQuery($"CREATE TABLE IF NOT EXISTS `config` (" +
                "[Id] INTEGER  PRIMARY KEY AUTOINCREMENT, " +
                "[appBuild]	INTEGER, " +
                "[dbVersion] INTEGER);", CalledFrom);
            
            var result = DbQuery("SELECT * FROM `config`", CalledFrom);
            if (result != null)
            {
                if (result.Rows.Count < 1)
                {
                    DbExecNonQuery("INSERT INTO `config` (`Id`, `appBuild`, `dbVersion`)" +
                        $" VALUES (1,{GlobalVars.HOMECINEMA_BUILD}," +
                        $"{(IsNewDb? GlobalVars.HOMECINEMA_DBVER.ToString() : "1")});", CalledFrom);
                    dbVersion = IsNewDb ? GlobalVars.HOMECINEMA_DBVER : 1;
                }
                else
                {
                    try { dbVersion = Convert.ToInt32(result.Rows[0]["dbVersion"]); }
                    catch { dbVersion = 1; }
                }
                // Hardcoded limit, breaks support for previous databases.
                if (dbVersion < 5) // 5 is the start of breaking changes
                {
                    GlobalVars.ShowWarning("Database is not supported anymore!");
                    return false;
                }
                if (dbVersion < GlobalVars.HOMECINEMA_DBVER)
                {
                    loaded = DBUpgradeDatabase(dbVersion);
                }
            }
            Logs.LogDb(CalledFrom, (loaded ? $"Database is loaded succesfully!\n{DB_PATH}" : "Issue on loading database!"));
            return loaded;
        }
        private static long DbGenerateId()
        {
            long Uid = 0L;
            var dt = DbQuery($"SELECT MAX({HCInfo.Id}) AS 'maxid' FROM {HCTable.info};", "DbGenerateId");
            if (dt?.Rows.Count == 1)
            {
                long.TryParse(dt.Rows[0]["maxid"].ToString(), out Uid);
            }
            Uid += 1;
            Logs.LogDb("DbGenerateId", $"UID Generated [{Uid}]");
            return Uid;
        }
        private static bool DBUpgradeDatabase(int dbVersion)
        {
            // Upgrade dbVer to match requirements
            int dbVer = dbVersion;
            string calledFrom = "DBUpgradeDatabase";
            int retry = RetryCount;
            while (dbVer < GlobalVars.HOMECINEMA_DBVER)
            {
                retry -= 1;
                if (retry > 0)
                {
                    Logs.LogDb(calledFrom, $"Loaded dbVer: ({dbVersion}), Current dbVer: ({dbVer})");
                    switch (dbVer)
                    {
                        case 5:
                        {
                            int count = 0;
                            // Changed Category: Animated Movie -> Movie, Cartoon Series -> TV Series
                            if (DbExecNonQuery($"UPDATE {HCTable.info} SET [{HCInfo.category}]=1 WHERE `{HCInfo.category}`=5;", calledFrom, -1) >= 0)
                            { count += 1;}
                            if (DbExecNonQuery($"UPDATE {HCTable.info} SET [{HCInfo.category}]=2 WHERE `{HCInfo.category}`=6;", calledFrom, -1) >= 0)
                            { count += 1; }
                            // ADD: 'rootFolder' column to 'info'
                            if (DbExecNonQuery($"ALTER TABLE {HCTable.filepath} ADD {HCFile.Root} TEXT DEFAULT '';", calledFrom, -1) >= 0)
                            { count += 1; }
                            if (count == 3) { dbVer += 1; }
                            break;
                        }
                        case 6:
                        {
                            int count = 0;
                            string qry = $"SELECT `{HCInfo.Id}`,`{HCInfo.director}`,`{HCInfo.producer}`,`{HCInfo.studio}`,`{HCInfo.artist}` FROM {HCTable.info}";
                            using (var dt = DbQuery(qry, calledFrom))
                            {
                                if (dt?.Rows.Count > 0)
                                {
                                    foreach (DataRow item in dt.Rows)
                                    {
                                        var data = new Dictionary<string, string>();
                                        data.Add(HCInfo.Id, item[HCInfo.Id].ToString());
                                        try { data.Add(HCInfo.director, item[HCInfo.director].ToString().Replace(',', ';')); }
                                        catch { }
                                        try { data.Add(HCInfo.producer, item[HCInfo.producer].ToString().Replace(',', ';')); }
                                        catch { }
                                        try { data.Add(HCInfo.studio, item[HCInfo.studio].ToString().Replace(',', ';')); }
                                        catch { }
                                        try { data.Add(HCInfo.artist, item[HCInfo.artist].ToString().Replace(',', ';')); }
                                        catch { }
                                        DbUpdateTable(HCTable.info, data, calledFrom);
                                    }
                                }
                                count += 1;
                            }
                            if (count == 1) { dbVer += 1; }
                            break;
                        }
                    }
                }
                else
                {
                    GlobalVars.ShowWarning("Database is corrupted! Delete 'HomeCinema.db' to reset data collection.", "Unable to upgrade database");
                    break;
                }
            }
            if (dbVer == GlobalVars.HOMECINEMA_DBVER)
            { 
                DbExecNonQuery($"UPDATE `config` SET `dbVersion`={dbVer}, `appBuild`={GlobalVars.HOMECINEMA_BUILD} WHERE `Id`=1;", calledFrom);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Open connection to SQLite database.
        /// </summary>
        /// <param name="connectionString">Connection string to use.</param>
        /// <returns>Handle of the SQLite connection.</returns>
        public static SQLiteConnection DbOpen()
        {
            try
            {
                SQLiteConnection conn = new SQLiteConnection(@"URI=file:" + DB_PATH);
                conn.Open();
                return conn;
            }
            catch { return null; }
        }
        /// <summary>
        /// Execute a query that has no return row.
        /// </summary>
        /// <param name="qry">SQL string query.</param>
        /// <param name="calledFrom">Caller of this function.</param>
        /// <returns>True, if query affected 1 or more rows. Otherwise, false</returns>
        public static bool DbExecNonQuery(string qry, string calledFrom, Dictionary<string, string> dt)
        {
            return (DbExecNonQuery(qry, calledFrom, dt, -1) > 0);
        }
        public static bool DbExecNonQuery(string qry, string calledFrom)
        {
            return (DbExecNonQuery(qry, calledFrom, null, -1) > 0);
        }
        public static int DbExecNonQuery(string qry, string calledFrom, int defaultValue)
        {
            return DbExecNonQuery(qry, calledFrom, null, defaultValue);
        }
        /// <summary>
        /// Execute a query that has no return row.
        /// </summary>
        /// <param name="qry">SQL string query.</param>
        /// <param name="calledFrom">Caller of this function.</param>
        /// <param name="defaultValue">Default value when query is unsuccessful</param>
        /// <returns>Returns query result as int</returns>
        public static int DbExecNonQuery(string qry, string calledFrom, Dictionary<string, string> dt, int defaultValue)
        {
            int DONE = defaultValue;
            int retry = RetryCount;
            string errFrom = $"SQLHelper-DbExecNonQuery [Called by: {calledFrom}]";
            while (retry > 0)
            {
                using (var conn = DbOpen())
                {
                    if (conn == null)
                    {
                        Logs.LogDb(errFrom, "Cannot establish connection to database (connection is null)!");
                        retry -= 1;
                        continue;
                    }
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        cmd.CommandText = qry;
                        try
                        {
                            // Parameters
                            if (dt != null)
                            {
                                foreach (var item in dt)
                                {
                                    cmd.Parameters.AddWithValue($"@{item.Key}", item.Value);
                                }
                            }
                            Logs.LogDb($"Executing query..Retry count: {RetryCount-retry+1}/{RetryCount}", $"Query: {qry}");
                            DONE = cmd.ExecuteNonQuery();
                            Logs.LogDb($"Query result: ({DONE})", "");
                            retry = -1;
                        }
                        catch (SQLiteException ex)
                        {
                            GlobalVars.ShowError($"SQLHelper-DbExecNonQuery (SQL Error)({DONE})", ex);
                            retry -= 1;
                        }
                        catch (Exception ex)
                        {
                            GlobalVars.ShowError($"SQLHelper-DbExecNonQuery (Error)({DONE})", ex);
                            retry -= 1;
                        }
                    }
                }
            }
            return DONE;
        }
        /// <summary>
        /// Execute generic query statement.
        /// </summary>
        /// <param name="qry">Query string to run.</param>
        /// <param name="cols">Columns for return.</param>
        /// <param name="calledFrom">Method calling this function.</param>
        /// <returns>DataTable, filled with result sets.</returns>
        public static DataTable DbQuery(string qry, string calledFrom)
        {
            string errFrom = "SQLHelper-DbQuery";
            int retry = RetryCount;
            while (retry > 0)
            {
                try
                {
                    // Create Connection to database
                    using (var conn = DbOpen())
                    {
                        using (var cmd = new SQLiteCommand(conn))
                        {
                            cmd.CommandText = qry;
                            var dt = new DataTable(); // Create DataTable for results
                            // Execute query
                            Logs.LogDb($"Executing query..Retry count: {RetryCount - retry + 1}/{RetryCount}", "Query: " + qry);
                            SQLiteDataAdapter sqlda = new SQLiteDataAdapter(qry, conn);
                            sqlda.Fill(dt);
                            // Log actions to textfile
                            Logs.LogDb($"{errFrom} [CalledFrom: {calledFrom}] (Finished executing Query)", $"Rows returned: {dt.Rows.Count}");
                            conn.Close();
                            retry = -1;
                            return dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logs.LogDb($"{errFrom} (Called: {calledFrom})", ex.Message);
                    retry -= 1;
                }
            }
            return null;
        }
        /// <summary>
        /// Get rows from single column query.
        /// </summary>
        /// <param name="tableName">Table Name.</param>
        /// <param name="col">Column name.</param>
        /// <param name="From">Calling method.</param>
        /// <returns>List string of results.</returns>
        public static List<string> DbQrySingle(string tableName, string col, string From)
        {
            // Initiate the list
            List<string> list = new List<string>();
            int retry = RetryCount;
            while (retry > 0)
            {
                // Create Connection to database
                using (var conn = DbOpen())
                {
                    if (conn == null)
                    {
                        Logs.LogDb("SQLHelper-DbQrySingle", "Cannot establish connection to database (connection is null)!");
                        retry -= 1;
                        continue;
                    }
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        string qry = $"SELECT {col} FROM {tableName}";
                        cmd.CommandText = qry;

                        // Execute query
                        Logs.LogDb($"SQLHelper-DbQrySingle (START)(From: {From})", "qry: " + qry);
                        try
                        {
                            SQLiteDataReader r = cmd.ExecuteReader();
                            // Get results
                            while (r.Read())
                            {
                                string stringRes = r[0].ToString();
                                if (String.IsNullOrWhiteSpace(stringRes) == false)
                                {
                                    list.Add(stringRes);
                                }
                            }
                            retry = -1;
                        }
                        catch { --retry; }

                        Logs.LogDb($"SQLHelper-DbQrySingle (END)(From: {From})", "Rows returned: " + list.Count.ToString());
                        conn.Close();
                    }
                }
            }
            return list; // Return list of results
        }
        /// <summary>
        /// // Insert new record, return ID of last insert. Otherwise 0
        /// </summary>
        /// <param name="dtInfo">Dictionary for table 'info'</param>
        /// <param name="dtFilepath">Dictionary for table 'filepath'</param>
        /// <param name="callFrom">Method calling.</param>
        /// <returns>LastID inserted or 0.</returns>
        public static long DbInsertMovie(Dictionary<string, string> dtInfo, Dictionary<string, string> dtFilepath, string callFrom)
        {
            // Setups
            int retry = RetryCount;
            string errFrom = $"SQLHelper-DbInsertMovie [calledFrom: {callFrom}]";
            string infoCols = "", infoVals = ""; // info table
            string fileCols = "", fileVals = ""; // filepath table
            int successCode; // code after execute query
            string fPathFile = ""; // full path for file
            long GeneratedID = DbGenerateId(); // Fetch new Id

            // Create pairing of colname and colvals
            foreach (var item in dtInfo)
            {
                if (item.Key != HCInfo.Id)
                {
                    infoCols += $"'{item.Key}',";
                    infoVals += $"@{item.Key},"; //QryString(value, !QryColNumeric(item.Key)) + ",";
                }
            }
            infoCols = infoCols.TrimEnd(',');
            infoVals = infoVals.TrimEnd(',');
            // Filepath table
            foreach (var item in dtFilepath)
            {
                if (item.Key != HCInfo.Id)
                {
                    fileCols += $"'{item.Key}',";
                    fileVals += $"@{item.Key},"; //QryString(item.Value.Replace("'", "''"), true) + ",";
                }
                if (item.Key == HCFile.File)
                    fPathFile = item.Value;
            }
            fileCols = fileCols.TrimEnd(',');
            fileVals = fileVals.TrimEnd(',');

            while (retry > 0)
            {
                // Create Connection to database
                using (var conn = DbOpen())
                {
                    Logs.LogDb("SQLHelper-DbInsertMovie", $"Retry count: {RetryCount-retry+1}/{RetryCount}");
                    if (conn == null)
                    {
                        Logs.LogDb("SQLHelper-DbInsertMovie", "Cannot establish connection to database (connection is null)!");
                        retry -= 1;
                        continue;
                    }
                    // Make Command and Transaction
                    var cmd = new SQLiteCommand(conn);
                    var transaction = conn.BeginTransaction();
                    // Insert entry
                    cmd.CommandText = String.Format("INSERT INTO {0} ({1}) VALUES ({2})", HCTable.info, $"'{HCInfo.Id}',{infoCols}", $"@Uid,{infoVals}");
                    cmd.Parameters.AddWithValue("@Uid", GeneratedID.ToString());
                    foreach (var item in dtInfo)
                    {
                        if (item.Key != HCInfo.Id)
                        {
                            cmd.Parameters.AddWithValue($"@{item.Key}", item.Value);
                        }
                    }
                    successCode = cmd.ExecuteNonQuery();// Execute first query
                    if (successCode > 0)
                    {
                        cmd.CommandText = String.Format("INSERT INTO {0} ({1}) VALUES ({2})", HCTable.filepath, $"'{HCInfo.Id}',{fileCols}", $"@Uid,{fileVals}");
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Uid", GeneratedID.ToString());
                        foreach (var item in dtFilepath)
                        {
                            if (item.Key != HCFile.Id)
                            {
                                cmd.Parameters.AddWithValue($"@{item.Key}", item.Value);
                            }
                        }
                        successCode = cmd.ExecuteNonQuery();
                        if (successCode < 1)
                            DbLogQuery(errFrom, successCode, cmd.CommandText, cmd.Parameters);

                        // Add cover image by capturing media
                        string coverFilepath = $"{DataFile.PATH_IMG}{GeneratedID}.jpg";
                        try
                        {
                            if (File.Exists(fPathFile))
                            {
                                using (ShellFile shellFile = ShellFile.FromFilePath(fPathFile))
                                {
                                    using (Bitmap bm = shellFile.Thumbnail.Bitmap)
                                    {
                                        bm.Save(coverFilepath, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    }
                                }
                            }
                        }
                        catch (Exception exShell)
                        {
                            Logs.LogErr($"{errFrom} (ShellFile thumbnail Error)({coverFilepath})", exShell);
                        }
                    }
                    else
                        DbLogQuery(errFrom, successCode, cmd.CommandText, cmd.Parameters);

                    // Commit transaction
                    if (successCode > 0)
                    {
                        transaction.Commit();
                        Logs.LogDb($"{errFrom} (FINISHED INSERT)", $"Last UID inserted: ({GeneratedID})");
                        retry = -1;
                    }
                    else
                    {
                        GeneratedID = 0;
                        transaction.Rollback();
                        retry -= 1;
                    }
                    transaction.Dispose();

                    // Close Connection to DB
                    cmd.Dispose();
                    conn.Close();
                }
            }
            return GeneratedID; // Return LastID inserted.
        }
        /// <summary>
        /// Update specified table wih Dictionary keypair values.
        /// </summary>
        /// <param name="TableName">Table name to update</param>
        /// <param name="dt">Dictionary that contains key-pair of column-value.</param>
        /// <param name="from">Method calling.</param>
        /// <returns>True if succesful. Otherwise, false.</returns>
        public static bool DbUpdateTable(string TableName, Dictionary<string, string> dt, string callFrom)
        {
            // Set values
            string valpair = "";
            string Id = "";
            try
            {
                dt.TryGetValue(HCInfo.Id, out Id);
            }
            catch { Id = ""; }
            if (String.IsNullOrWhiteSpace(Id))
            {
                Logs.LogDb(callFrom, "Cannot Update entry with empty Id!");
                return false;
            }

            foreach (var item in dt)
            {
                if (item.Key != HCInfo.Id)
                {
                    valpair += $"'{item.Key}'=@{item.Key},";
                }
            }
            valpair = valpair.TrimEnd(',');

            // Query to db
            if (!String.IsNullOrWhiteSpace(valpair))
            {
                string qry = $"UPDATE {TableName} " +
                             $"SET {valpair} " +
                             $"WHERE `{HCInfo.Id}`={Id}";
                if (DbExecNonQuery(qry, callFrom, dt))
                {
                    Logs.LogDb(callFrom, $"Entry with Id({Id}) is updated Succesfully!");
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Update Table INFO, with new values.
        /// </summary>
        /// <param name="dt">Dictionary that contains key-pair of column-value.</param>
        /// <param name="from">Method calling.</param>
        /// <returns>True if succesful. Otherwise, false.</returns>
        public static bool DbUpdateInfo(MediaInfo media, string from)
        {
            string calledFrom = $"SQLHelper-DbUpdateInfo (calledFrom: {from})";
            bool cont = false;
            var entry = new Dictionary<string, string>();
            entry.Add(HCInfo.Id, GlobalVars.ValidateEmptyOrNull(media.Id));
            entry.Add(HCInfo.imdb, GlobalVars.ValidateEmptyOrNull(media.Imdb));
            entry.Add(HCInfo.anilist, GlobalVars.ValidateEmptyOrNull(media.Anilist));
            entry.Add(HCInfo.name, GlobalVars.ValidateEmptyOrNull(media.Title));
            entry.Add(HCInfo.name_orig, GlobalVars.ValidateEmptyOrNull(media.OrigTitle));
            entry.Add(HCInfo.name_series, GlobalVars.ValidateEmptyOrNull(media.SeriesName));
            entry.Add(HCInfo.season, GlobalVars.ValidateEmptyOrNull(media.Seasons.ToString()));
            entry.Add(HCInfo.episode, GlobalVars.ValidateEmptyOrNull(media.Episodes.ToString()));
            entry.Add(HCInfo.country, GlobalVars.ConvertListToString(media.Country, ",", calledFrom));
            entry.Add(HCInfo.category, media.Category.ToString());
            entry.Add(HCInfo.genre, GlobalVars.ConvertListToString(media.Genre, ",", calledFrom));
            entry.Add(HCInfo.studio, GlobalVars.ConvertListToString(media.Studio, ";", calledFrom));
            entry.Add(HCInfo.producer, GlobalVars.ConvertListToString(media.Producer, ";", calledFrom));
            entry.Add(HCInfo.director, GlobalVars.ConvertListToString(media.Director, ";", calledFrom));
            entry.Add(HCInfo.artist, GlobalVars.ConvertListToString(media.Casts, ";", calledFrom));
            entry.Add(HCInfo.year, GlobalVars.ValidateEmptyOrNull(media.ReleaseDate));
            entry.Add(HCInfo.summary, GlobalVars.ValidateEmptyOrNull(media.Summary));

            cont = DbUpdateTable(HCTable.info, entry, calledFrom);
            if (cont)
            {
                var entryFile = new Dictionary<string, string>();
                entryFile.Add(HCFile.Id, media.Id); // ID
                entryFile.Add(HCFile.File, GlobalVars.ValidateEmptyOrNull(media.FilePath)); // file
                entryFile.Add(HCFile.Sub, GlobalVars.ValidateEmptyOrNull(media.FileSub)); // sub
                entryFile.Add(HCFile.Trailer, GlobalVars.ValidateEmptyOrNull(media.Trailer)); // trailers
                cont = DbUpdateTable(HCTable.filepath, entryFile, calledFrom);
            }
            return cont;
        }
        /// <summary>
        /// Delete a MOVIE from Database.
        /// </summary>
        /// <param name="ID">Movie ID to delete.</param>
        /// <param name="errFrom">Method calling.</param>
        /// <returns>True if succesful. False if otherwise.</returns>
        public static bool DbDeleteMovie(string ID, string errFrom)
        {
            // Remove info
            string calledFrom = $"SQLHelper-DbDeleteMovie [calledFrom: {errFrom}]";
            string qry = $"DELETE FROM {HCTable.info} WHERE `{HCInfo.Id}`={ID};";
            if (DbExecNonQuery(qry, calledFrom))
            {
                Logs.LogDb(errFrom, $"ID ({ID}) is removed from database! Table: ({HCTable.info})");
                // Remove filepath
                qry = $"DELETE FROM {HCTable.filepath} WHERE `{HCFile.Id}`={ID};";
                if (DbExecNonQuery(qry, calledFrom))
                {
                    Logs.LogDb(errFrom, $"ID ({ID}) is removed from database! Table: ({HCTable.filepath})");
                    return true;
                }
            }
            return false;
        }

        public static void DbLogQuery(string errFrom, int code, string qry, SQLiteParameterCollection Param)
        {
            string query = qry;
            foreach (SqlParameter p in Param)
            {
                try
                {
                    query = System.Text.RegularExpressions.Regex.Replace(query, @"\B" + p.ParameterName + @"\b", p.Value.ToString());
                }
                catch (Exception ex)
                {
                    Logs.LogErr(errFrom, ex);
                }
            }
            Logs.LogDb($"{errFrom} [Insert failed code: {code}]", $"Query: {query}");
        }
    }
}
