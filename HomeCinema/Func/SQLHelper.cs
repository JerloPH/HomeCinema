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
using HomeCinema.Global;
using Microsoft.WindowsAPICodePack.Shell;
using System.Drawing;
using System.Data.Common;

namespace HomeCinema.SQLFunc
{
    public static class SQLHelper
    {
        /// <summary>
        /// Initialize SQLite database. Create if NOT existing.
        /// </summary>
        /// <param name="InitiatedFrom">Caller of the function.</param>
        public static void Initiate(string InitiatedFrom)
        {
            string CalledFrom = "SQLHelper (Instance)-" + InitiatedFrom;
            SQLiteConnection conn = DbOpen(); // connect to database

            // Create Table and Schema
            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = $"CREATE TABLE IF NOT EXISTS '{GlobalVars.DB_TNAME_INFO}' (" +
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
            "'summary'  TEXT DEFAULT 'This has no summary');";
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            // Create filepath Table and Schema
            SQLiteCommand cmd2 = new SQLiteCommand(conn);
            cmd2.CommandText = $"CREATE TABLE IF NOT EXISTS '{GlobalVars.DB_TNAME_FILEPATH}' (" +
            "[Id]	INTEGER  PRIMARY KEY AUTOINCREMENT, " +
            "[file]	TEXT, " +
            "[sub]	TEXT, " +
            "[trailer] TEXT);";
            cmd2.ExecuteNonQuery();
            cmd2.Dispose();
            //GlobalVars.ShowInfo("Database is Created succesfully!");
            GlobalVars.LogDb(CalledFrom, "Database is loaded succesfully!\n " + GlobalVars.DB_PATH);
            // Dispose (Close) Connection to DB
            DbClose(conn);
        }
        /// <summary>
        /// Open connection to SQLite database, using default values.
        /// </summary>
        /// <returns>Handle of the SQLite connection.</returns>
        public static SQLiteConnection DbOpen()
        {
            return DbOpen(GlobalVars.DB_DATAPATH);
        }
        /// <summary>
        /// Open connection to SQLite database.
        /// </summary>
        /// <param name="connectionString">Connection string to use.</param>
        /// <returns>Handle of the SQLite connection.</returns>
        public static SQLiteConnection DbOpen(string connectionString)
        {
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            conn.Open();
            GlobalVars.LogDb("SQLHelper-DbOpen", "DB Open: " + conn.FileName);
            return conn;
        }
        /// <summary>
        /// Close the open SQLite connection.
        /// </summary>
        /// <param name="c">Handle of the SQL connection.</param>
        public static void DbClose(SQLiteConnection c)
        {
            string cFile = c.FileName;
            c?.Dispose();
            GlobalVars.LogDb("SQLHelper-DbClose", "DB Closed: " + cFile);
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
            SQLiteConnection conn = DbOpen();
            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = qry;
            try 
            {
                GlobalVars.LogDb($"SQLHelper-DbExecNonQuery [Called by: {calledFrom}]", "query is executing");
                if (cmd.ExecuteNonQuery() > 0)
                {
                    DONE = true;
                }
            }
            catch (SQLiteException ex)
            {
                GlobalVars.LogDb("SQLHelper-DbExecNonQuery (Query)", qry);
                GlobalVars.ShowError("SQLHelper-DbExecNonQuery (SQL Error)", ex);
            }
            catch (Exception ex)
            {
                GlobalVars.ShowError("SQLHelper-DbExecNonQuery (Error)", ex);
            }
            finally
            {
                // Dispose (Close) Connection to DB
                GlobalVars.LogDb("SQLHelper-DbExecNonQuery", "Finished executing non-query");
                cmd.Dispose();
                DbClose(conn);
            }
            return DONE;
        }
        /// <summary>
        /// Initialize a DataTable, with columns.
        /// </summary>
        /// <param name="cols">String array of column names.</param>
        /// <returns>DataTable ref.</returns>
        public static DataTable InitializeDT(String[] cols)
        {
            return InitializeDT(true, cols);
        }
        /// <summary>
        /// Initialize a DataTable, with COLUMN [Id], and other columns.
        /// </summary>
        /// <param name="WITH_ID">Include [Id] to DataTable Column.</param>
        /// <param name="cols">Column names.</param>
        /// <returns>DataTable ref</returns>
        public static DataTable InitializeDT(bool WITH_ID, String[] cols)
        {
            DataTable dt = new DataTable();
            if (WITH_ID)
            {
                // table initialization for columns with Id as first column
                DataColumn column = new DataColumn();
                column.DataType = System.Type.GetType("System.Int32");
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
            try
            {
                // Create Connection to database
                using (SQLiteConnection conn = new SQLiteConnection(GlobalVars.DB_DATAPATH))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(conn))
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

                        // Apply dt changes and return
                        //dt.AcceptChanges();
                        conn.Close();
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalVars.LogDb($"{errFrom} (Called: {calledFrom})", ex.Message);
                return null;
            }
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
            // Create Connection to database
            using (SQLiteConnection conn = DbOpen())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    // Initiate the list
                    List<string> list = new List<string>();

                    string qry = $"SELECT {col} FROM {tableName}";
                    cmd.CommandText = qry;

                    // Execute query
                    GlobalVars.LogDb($"SQLHelper-DbQrySingle (START)(From: {From})", "qry: " + qry);
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
                    GlobalVars.LogDb($"SQLHelper-DbQrySingle (END)(From: {From})", "Rows returned: " + list.Count.ToString());
                    conn.Close();

                    // Return list of results
                    return list;
                }
            }
        }
        /// <summary>
        /// // Insert new record, return ID of last insert. Otherwise 0
        /// </summary>
        /// <param name="dt">DataTable to insert the record to.</param>
        /// <param name="callFrom">Method calling.</param>
        /// <returns>LastID inserted or 0.</returns>
        public static int DbInsertMovie(DataTable dt, string callFrom)
        {
            // Setups
            string colsInfo = "";
            string errFrom = $"SQLHelper-DbInsertMovie [calledFrom: {callFrom}]";
            int LastID = 0; // Last ID Inserted succesfully
            int rows = 0; // Number of total rows inserted successfully
            string vals = ""; // all values stored in row of DT
            int cc = 0; // counts string[] vals size

            // Vars for filepath
            string fPathFile = "", fPathSub = "", fPathTrailer = "";

            // Get columns from info
            foreach (string s in GlobalVars.DB_TABLE_INFO)
            {
                if (s != "Id")
                {
                    colsInfo += "[" + s + "],";
                }
            }
            colsInfo = colsInfo.TrimEnd(',');

            // Create Connection to database
            using (SQLiteConnection conn = DbOpen())
            {
                SQLiteCommand cmd = new SQLiteCommand(conn);

                // Make Transaction
                SQLiteTransaction transaction;
                transaction = conn.BeginTransaction();

                // Create a copy of info table
                List<string> dbTableInfoCopy = new List<string>(GlobalVars.DB_TABLE_INFO);
                dbTableInfoCopy.RemoveAt(0); // remove [Id] from table info
                string[] dbTableInfoArr = dbTableInfoCopy.ToArray(); // turn column list to array

                // Get rows from DT
                foreach (DataRow r in dt.Rows)
                {
                    vals = ""; // Reset values to add
                    cc = 0; // reset column count
                            // Build query for INFO
                    foreach (string s in dbTableInfoArr)
                    {
                        if (GlobalVars.QryColNumeric(s))
                        {
                            vals += GlobalVars.QryString(r[cc].ToString(), false) + ",";
                        }
                        else
                        {
                            vals += GlobalVars.QryString(r[cc].ToString().Replace('\'', '"'), true) + ",";
                        }
                        cc += 1;
                    }
                    vals = vals.TrimEnd(',');

                    // Build for FILEPATH
                    fPathFile = r[dbTableInfoArr.Length].ToString(); // file
                    fPathSub = r[dbTableInfoArr.Length + 1].ToString(); // sub
                    fPathTrailer = r[dbTableInfoArr.Length + 2].ToString(); // trailer

                    // Set the command for query
                    string qry = $"INSERT INTO {GlobalVars.DB_TNAME_INFO} ({colsInfo}) VALUES({vals});";
                    cmd.CommandText = qry;

                    // Log Insert filePath and query
                    GlobalVars.LogDb($"{errFrom} (INSERT MOVIE START)", "Inserting: " + fPathFile);
                    GlobalVars.LogDb($"{errFrom} ({GlobalVars.DB_TNAME_INFO})", $"qry: {qry}");

                    // Execute query for INFO
                    int affected = cmd.ExecuteNonQuery();

                    // LastID of insert movie ID
                    LastID = (int)conn.LastInsertRowId;

                    // Try Execute query for FILEPATH
                    if (affected > 0)
                    {
                        // format FilePath with single quotations
                        string fPathFileFix = fPathFile.Replace("'", "''");
                        GlobalVars.LogDb($"{errFrom} (FORMAT filepath)", "Formatted: " + fPathFileFix);
                        string colsFile = $"[{GlobalVars.DB_TABLE_FILEPATH[0]}],[{GlobalVars.DB_TABLE_FILEPATH[1]}],[{GlobalVars.DB_TABLE_FILEPATH[2]}],[{GlobalVars.DB_TABLE_FILEPATH[3]}]";
                        qry = $"INSERT INTO {GlobalVars.DB_TNAME_FILEPATH} ({colsFile}) VALUES({LastID},'{fPathFileFix}','{fPathSub}','{fPathTrailer}');";
                        cmd.CommandText = qry;
                        GlobalVars.LogDb($"{errFrom} ({GlobalVars.DB_TNAME_FILEPATH})", $"qry: {qry}");
                        cmd.ExecuteNonQuery();

                        rows += 1;

                        // Add cover image by capturing media
                        string coverFilepath = GlobalVars.PATH_IMG + LastID + ".jpg";
                        try
                        {
                            using (ShellFile shellFile = ShellFile.FromFilePath(fPathFile))
                            {
                                using (Bitmap bm = shellFile.Thumbnail.Bitmap)
                                {
                                    bm.Save(coverFilepath, System.Drawing.Imaging.ImageFormat.Jpeg);
                                }
                            }
                        }
                        catch (Exception exShell)
                        {
                            GlobalVars.LogDb($"{errFrom} (ShellFile thumbnail Error)({coverFilepath})", exShell.Message);
                        }
                    }
                    // Nothing is inserted
                    else
                    {
                        GlobalVars.LogDb($"{errFrom}", "Insert failed with code: " + affected.ToString());
                    }
                }
                // Release memory
                GlobalVars.LogDb($"{errFrom} (FINISHED INSERT)", $"Rows Inserted: ({rows.ToString()})");
                dt.Clear();
                dt.Dispose();

                // Commit transaction
                transaction.Commit();
                transaction.Dispose();

                // Close Connection to DB
                cmd.Dispose();
                conn.Close();

                // Return LastID inserted.
                return LastID;
            }
        }
        /// <summary>
        /// Update Table INFO, with new values.
        /// </summary>
        /// <param name="dt">DataTable which contains the new values.</param>
        /// <param name="from">Method calling.</param>
        /// <returns>True if succesful. Otherwise, false.</returns>
        public static bool DbUpdateInfo(DataTable dt, string from)
        {
            // Set values
            string TableName = GlobalVars.DB_TNAME_INFO;
            string callFrom = $"SQLHelper-DbUpdateInfo (calledFrom: {from})";
            string valpair = "";
            string r0 = "";
            foreach (DataRow r in dt.Rows)
            {
                r0 = r[0].ToString();
                for (int i = 1; i < GlobalVars.DB_TABLE_INFO.Length; i++)
                {
                    if (GlobalVars.QryColNumeric(GlobalVars.DB_TABLE_INFO[i]))
                    {
                        valpair += "[" + GlobalVars.DB_TABLE_INFO[i] + "]=" + GlobalVars.QryString(r[i].ToString(), false) + ",";
                    }
                    else
                    {
                        valpair += "[" + GlobalVars.DB_TABLE_INFO[i] + "]=" + GlobalVars.QryString(r[i].ToString(), true) + ",";
                    }
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
            GlobalVars.LogDb(callFrom, $"Update {GlobalVars.DB_TNAME_INFO} ID ({r0}) || Query: {qry}");
            if (DbExecNonQuery(qry, callFrom))
            {
                GlobalVars.LogDb(callFrom, $"ID ({r0}) is updated Succesfully!");
                return true;
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
            GlobalVars.LogDb(callFrom, $"Update {GlobalVars.DB_TNAME_FILEPATH} ID ({r0}) || Query: {qry}");
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
