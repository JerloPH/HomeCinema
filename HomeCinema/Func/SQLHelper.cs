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

namespace HomeCinema.SQLFunc
{
    public static class SQLHelper
    {
        private static readonly int RetryCount = 5;
        /// <summary>
        /// Initialize SQLite database. Create if NOT existing.
        /// </summary>
        /// <param name="InitiatedFrom">Caller of the function.</param>
        public static bool Initiate(string InitiatedFrom)
        {
            string CalledFrom = "SQLHelper (Instance)-" + InitiatedFrom;
            DbExecNonQuery($"CREATE TABLE IF NOT EXISTS '{GlobalVars.DB_TNAME_INFO}' (" +
                "'Id'	INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "'imdb'	TEXT DEFAULT 0, " +
                "'name'	TEXT, " +
                "'name_ep'	TEXT, " +
                "'name_series'	TEXT, " +
                "'season'	INTEGER, " +
                "'episode'	INTEGER, " +
                "'country'	TEXT, " +
                "'category'	VARCHAR(1) DEFAULT 0, " + // 0-None | 1-MOVIE | 2-TVSERIES | 3-ANIMEMOVIE | 4-ANIME SERIES | 5-ANIMATED MOVIE | 6-CARTOON SERIES
                "'genre'	TEXT, " +
                "'studio'	TEXT, " +
                "'producer'	TEXT, " +
                "'director'	TEXT, " +
                "'artist'	TEXT, " +
                "'year'	VARCHAR(5) DEFAULT 0, " +
                "'summary'  TEXT DEFAULT 'This has no summary');", CalledFrom);
            DbExecNonQuery($"CREATE TABLE IF NOT EXISTS '{GlobalVars.DB_TNAME_FILEPATH}' (" +
                "[Id]	INTEGER  PRIMARY KEY AUTOINCREMENT, " +
                "[file]	TEXT, " +
                "[sub]	TEXT, " +
                "[trailer] TEXT);", CalledFrom);
            var success = DbExecNonQuery($"CREATE TABLE IF NOT EXISTS `config` (" +
                "[Id] INTEGER  PRIMARY KEY AUTOINCREMENT, " +
                "[appBuild]	INTEGER, " +
                "[dbVersion] INTEGER);", CalledFrom);
            if (success)
            {
                int dbVersion = 1;
                var result = DbQuery("SELECT * FROM `config`", CalledFrom);
                if (result != null)
                {
                    if (result.Rows.Count < 1)
                    {
                        DbExecNonQuery("INSERT INTO `config` (`Id`, `appBuild`, `dbVersion`)" +
                            $" VALUES (1, {GlobalVars.HOMECINEMA_BUILD}, 1);", CalledFrom);
                        dbVersion = 1;
                    }
                    else
                    {
                        try { dbVersion = Convert.ToInt32(result.Rows[0]["dbVersion"]); }
                        catch { dbVersion = 1; }
                    }
                    if (dbVersion < GlobalVars.HOMECINEMA_DBVER)
                    {
                        GlobalVars.ShowNoParent("Outdated database!");
                    }
                }
            }
            GlobalVars.LogDb(CalledFrom, "Database is loaded succesfully!\n " + GlobalVars.DB_PATH);
            return true;
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
                SQLiteConnection conn = new SQLiteConnection(GlobalVars.DB_DATAPATH);
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
        /// <returns>True, if successful. Otherwise, false</returns>
        public static bool DbExecNonQuery(string qry, string calledFrom)
        {
            bool DONE = false;
            int retry = RetryCount;
            string errFrom = $"SQLHelper-DbExecNonQuery [Called by: {calledFrom}]";
            while (retry > 0)
            {
                using (var conn = DbOpen())
                {
                    if (conn == null)
                    {
                        GlobalVars.LogDb(errFrom, "Cannot establish connection to database (connection is null)!");
                        --retry;
                        continue;
                    }
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        cmd.CommandText = qry;
                        try
                        {
                            GlobalVars.LogDb("Executing query..", qry);
                            DONE = (cmd.ExecuteNonQuery() > 0);
                            retry = -1;
                        }
                        catch (SQLiteException ex)
                        {
                            GlobalVars.ShowError("SQLHelper-DbExecNonQuery (SQL Error)", ex);
                            --retry;
                        }
                        catch (Exception ex)
                        {
                            GlobalVars.ShowError("SQLHelper-DbExecNonQuery (Error)", ex);
                            --retry;
                        }
                    }
                }
                return DONE;
            }
            return false;
        }
        /// <summary>
        /// Initialize a DataTable, with COLUMN [Id], and other columns.
        /// </summary>
        /// <param name="WITH_ID">Include [Id] to DataTable Column.</param>
        /// <param name="cols">Column names.</param>
        /// <returns>DataTable ref</returns>
        public static DataTable InitializeDT(bool WITH_ID, String[] cols)
        {
            var dt = new DataTable();
            if (WITH_ID)
            {
                // table initialization for columns with Id as first column
                DataColumn column = new DataColumn();
                column.DataType = System.Type.GetType("System.Int64");
                column.ColumnName = "Id";
                column.ReadOnly = true;
                column.Unique = true;
                // Add the Column to the DataColumnCollection.
                dt.Columns.Add(column);

                // Multiple col
                foreach (string name in cols)
                {
                    if (name != "Id")
                    {
                        DataColumn col = new DataColumn();
                        col.DataType = System.Type.GetType("System.String");
                        col.ColumnName = name;
                        col.ReadOnly = true;
                        // Add the Column to the DataColumnCollection.
                        dt.Columns.Add(col);
                    }
                }
            }
            else
            {
                // table initialization for ONLY Strings without [Id]
                // Multiple col
                foreach (string name in cols)
                {
                    if (name != "Id")
                    {
                        DataColumn col = new DataColumn();
                        col.DataType = System.Type.GetType("System.String");
                        col.ColumnName = name;
                        col.ReadOnly = true;
                        // Add the Column to the DataColumnCollection.
                        dt.Columns.Add(col);
                    }
                }
            }
            // Apply changes to dt and return
            dt.AcceptChanges();
            return dt;
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

                            // Create DataTable for results
                            var dt = new DataTable();

                            // Execute query
                            GlobalVars.LogDb($"{errFrom} (START) [Called by: {calledFrom}]", "qry: " + qry);

                            SQLiteDataAdapter sqlda = new SQLiteDataAdapter(qry, conn);
                            sqlda.Fill(dt);

                            // Log actions to textfile
                            GlobalVars.LogDb($"{errFrom} (Finished executing Query)", "Number of Rows returned by query: " + Convert.ToString(dt.Rows.Count));

                            conn.Close();
                            retry = -1;
                            return dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    GlobalVars.LogDb($"{errFrom} (Called: {calledFrom})", ex.Message);
                    --retry;
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
                        GlobalVars.LogDb("SQLHelper-DbQrySingle", "Cannot establish connection to database (connection is null)!");
                        --retry;
                        continue;
                    }
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        string qry = $"SELECT {col} FROM {tableName}";
                        cmd.CommandText = qry;

                        // Execute query
                        GlobalVars.LogDb($"SQLHelper-DbQrySingle (START)(From: {From})", "qry: " + qry);
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

                        GlobalVars.LogDb($"SQLHelper-DbQrySingle (END)(From: {From})", "Rows returned: " + list.Count.ToString());
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
        public static int DbInsertMovie(Dictionary<string, string> dtInfo, Dictionary<string, string> dtFilepath, string callFrom)
        {
            // Setups
            int retry = RetryCount;
            string errFrom = $"SQLHelper-DbInsertMovie [calledFrom: {callFrom}]";
            int LastID = 0; // Last ID Inserted succesfully
            string infoCols = "", infoVals = ""; // info table
            string fileCols = "", fileVals = ""; // filepath table
            int successCode; // code after execute query
            string fPathFile = ""; // full path for file

            // Create pairing of colname and colvals
            foreach (var item in dtInfo)
            {
                if (item.Key != HCInfo.Id.ToString())
                {
                    infoCols += item.Key + ",";
                    infoVals += GlobalVars.QryString(item.Value.Replace("'", "''"), !GlobalVars.QryColNumeric(item.Key)) + ",";
                }
            }
            infoCols = infoCols.TrimEnd(',');
            infoVals = infoVals.TrimEnd(',');
            // Filepath table
            foreach (var item in dtFilepath)
            {
                if (item.Key != HCInfo.Id.ToString())
                {
                    fileCols += item.Key + ",";
                    fileVals += GlobalVars.QryString(item.Value.Replace("'", "''"), true) + ",";
                }
                if (item.Key == HCFile.file.ToString())
                {
                    fPathFile = item.Value;
                }
            }
            fileCols = fileCols.TrimEnd(',');
            fileVals = fileVals.TrimEnd(',');

            while (retry > 0)
            {
                // Create Connection to database
                using (var conn = DbOpen())
                {
                    if (conn == null)
                    {
                        GlobalVars.LogDb("SQLHelper-DbInsertMovie", "Cannot establish connection to database (connection is null)!");
                        --retry;
                        continue;
                    }
                    // Make Command and Transaction
                    var cmd = new SQLiteCommand(conn);
                    var transaction = conn.BeginTransaction();

                    // Insert entry
                    cmd.CommandText = $"INSERT INTO {GlobalVars.DB_TNAME_INFO} ({infoCols}) VALUES({infoVals});";
                    successCode = cmd.ExecuteNonQuery();
                    LastID = (int)conn.LastInsertRowId;

                    if (successCode > 0)
                    {
                        cmd.CommandText = $"INSERT INTO {GlobalVars.DB_TNAME_FILEPATH} (Id, {fileCols}) VALUES({LastID},{fileVals});";
                        successCode = cmd.ExecuteNonQuery();
                        // Add cover image by capturing media
                        string coverFilepath = GlobalVars.PATH_IMG + LastID + ".jpg";
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
                            GlobalVars.LogDb($"{errFrom} (ShellFile thumbnail Error)({coverFilepath})", exShell.Message);
                        }
                    }
                    else
                    {
                        GlobalVars.LogDb($"{errFrom} [Insert failed code: {successCode.ToString()}]", "Query: " + cmd.CommandText);
                    }

                    // Commit transaction
                    if (successCode > 0)
                    {
                        transaction.Commit();
                        GlobalVars.LogDb($"{errFrom} (FINISHED INSERT)", $"Last ID inserted: ({LastID.ToString()})");
                        retry = -1;
                    }
                    else
                    {
                        transaction.Rollback();
                        --retry;
                    }
                    transaction.Dispose();

                    // Close Connection to DB
                    cmd.Dispose();
                    conn.Close();
                }
            }
            return LastID; // Return LastID inserted.
        }
        /// <summary>
        /// Update Table INFO, with new values.
        /// </summary>
        /// <param name="dt">Dictionary that contains key-pair of column-value.</param>
        /// <param name="from">Method calling.</param>
        /// <returns>True if succesful. Otherwise, false.</returns>
        public static bool DbUpdateInfo(Dictionary<string, string> dt, string from)
        {
            // Set values
            string TableName = GlobalVars.DB_TNAME_INFO;
            string callFrom = $"SQLHelper-DbUpdateInfo (calledFrom: {from})";
            string valpair = "";
            string Id = "";
            try
            {
                dt.TryGetValue(HCInfo.Id.ToString(), out Id);
            }
            catch { Id = ""; }
            if (String.IsNullOrWhiteSpace(Id))
            {
                GlobalVars.LogDb(callFrom, "Cannot Update entry with empty Id!");
                return false;
            }

            foreach (var item in dt)
            {
                if (item.Key != HCInfo.Id.ToString())
                {
                    valpair += "[" + item.Key + "]=" + GlobalVars.QryString(item.Value, !GlobalVars.QryColNumeric(item.Key)) + ",";
                }
            }
            valpair = valpair.TrimEnd(',');

            // Query to db
            if (!String.IsNullOrWhiteSpace(valpair))
            {
                string qry = $"UPDATE {TableName} " +
                             "SET " + valpair + " " +
                            $"WHERE [Id] = {Id}";
                if (DbExecNonQuery(qry, callFrom))
                {
                    GlobalVars.LogDb(callFrom, $"Entry with Id({Id}) is updated Succesfully!");
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Update Table FILEPATH, with new values.
        /// </summary>
        /// <param name="dt">DataTable which contains the new values.</param>
        /// <param name="from">Method calling.</param>
        /// <returns>True if succesful. Otherwise, false.</returns>
        public static bool DbUpdateFilepath(DataTable dt, string from)
        {
            // Set values
            string TableName = GlobalVars.DB_TNAME_FILEPATH;
            string callFrom = $"SQLHelper-DbUpdateFilepath (CalledFrom: {from})";
            string valpair = "";
            string r0 = "";
            foreach (DataRow r in dt.Rows)
            {
                r0 = r[0].ToString();
                for (int i = 1; i < GlobalVars.DB_TABLE_FILEPATH.Length; i++)
                {
                    valpair += "[" + GlobalVars.DB_TABLE_FILEPATH[i] + "]=" + GlobalVars.QryString(r[i].ToString(), true) + ",";
                }
            }
            valpair = valpair.TrimEnd(',');

            // dispose table
            dt.Clear();
            dt.Dispose();
            // Query to db
            string qry = $"UPDATE {TableName} " +
                         "SET " + valpair + " " +
                        $"WHERE [Id] = {r0}";
            if (DbExecNonQuery(qry, callFrom))
            {
                GlobalVars.LogDb(callFrom, $"ID ({r0}) is updated Succesfully!");
                return true;
            }
            return false;
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
            string qry = $"DELETE FROM {GlobalVars.DB_TNAME_INFO} WHERE [Id] = {ID};";
            if (DbExecNonQuery(qry, calledFrom))
            {
                GlobalVars.LogDb(errFrom, $"ID ({ID}) is removed from database! Table: ({GlobalVars.DB_TNAME_INFO})");
                // Remove filepath
                qry = $"DELETE FROM {GlobalVars.DB_TNAME_FILEPATH} WHERE [Id] = {ID};";
                if (DbExecNonQuery(qry, calledFrom))
                {
                    GlobalVars.LogDb(errFrom, $"ID ({ID}) is removed from database! Table: ({GlobalVars.DB_TNAME_FILEPATH})");
                    return true;
                }
            }
            return false;
        }
    }
}
