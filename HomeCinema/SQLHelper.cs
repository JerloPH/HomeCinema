using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SQLite;
using System.Data;
using HomeCinema.Global;

namespace HomeCinema.SQLFunc
{
    public class SQLHelper
    {
        // Variables
        //public bool ExistingDB { get; set; } = false;

        // Start Connecting to database, when object is initialize
        public SQLHelper()
        {
            SQLiteConnection conn = null;
            // Start connection
            if (File.Exists(GlobalVars.DB_PATH))
            {
                // There is already a database!
                conn = DbOpen();
                GlobalVars.Log("SQLHelper", $"Database Already Exists!\n  Path: { GlobalVars.DB_PATH }");
            }
            else
            {
                // Connect to database
                conn = DbOpen();

                //ExistingDB = true;
                GlobalVars.Log("SQLHelper-SQLHelper (Create empty database)", "No Database found! Will create..");

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
                GlobalVars.Log("SQLHelper", "Database is Created succesfully!\n " + GlobalVars.DB_PATH);
            }
            // Dispose (Close) Connection to DB
            DbClose(conn);
        }
        // Open connection to database
        public static SQLiteConnection DbOpen()
        {
            return DbOpen(GlobalVars.DB_DATAPATH);
        }
        public static SQLiteConnection DbOpen(string connectionString)
        {
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            conn.Open();
            GlobalVars.Log("SQLHelper-DbOpen", "DB Open: " + conn.FileName);
            return conn;
        }
        // Close connection to database
        public static void DbClose(SQLiteConnection c)
        {
            GlobalVars.Log("SQLHelper-DbClose", "DB Closed: " + c.FileName);
            c.Dispose();
        }
        // Execute a query that has no return row
        public static bool DbExecNonQuery(string qry)
        {
            bool DONE = false;
            SQLiteConnection conn = DbOpen();
            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = qry;
            try 
            {
                GlobalVars.Log("SQLHelper-DbExecNonQuery (START)", "query is executing");
                if (cmd.ExecuteNonQuery() > 0)
                {
                    DONE = true;
                }
            }
            catch (SQLiteException e)
            {
                GlobalVars.Log("SQLHelper-DbExecNonQuery (error message)", qry);
                GlobalVars.ShowError("SQLHelper-DbExecNonQuery (SQL Error)", e.Message);
            }
            catch (Exception e)
            {
                GlobalVars.ShowError("SQLHelper-DbExecNonQuery (Error)", e.Message);
            }
            finally
            {
                // Dispose (Close) Connection to DB
                GlobalVars.Log("SQLHelper-DbExecNonQuery (END)", "Finished executing non-query");
                cmd.Dispose();
                DbClose(conn);
            }
            return DONE;
        }
        // Initialize datatable
        public DataTable InitializeDT(String[] cols)
        {
            return InitializeDT(true, cols);
        }
        public DataTable InitializeDT(bool WITH_ID, String[] cols)
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
        // Execute query statement
        public DataTable DbQuery(string qry, string cols)
        {
            // Create Connection to database
            SQLiteConnection conn = DbOpen();
            SQLiteCommand cmd = new SQLiteCommand(conn);
            cmd.CommandText = qry;

            // Trim the [] from Column names
            string colsTrim = cols.Replace("[", "");
            colsTrim = colsTrim.Replace("]", "");
            string[] cols_qry = colsTrim.Split(',');

            // Create DataTable for results
            DataTable dt = InitializeDT(cols_qry);

            // Execute query
            SQLiteDataReader r = cmd.ExecuteReader();
            GlobalVars.Log("SQLHelper-DbQuery (START)", "qry: " + qry);
            
            // Get all data results
            while (r.Read())
            {
                DataRow row;
                row = dt.NewRow();
                int cc = 0;
                foreach (string text in cols_qry)
                {
                    row[cc] = r[text];
                    //GlobalVars.Log("SQLHelper-122", Convert.ToString(cc) + " : " + text + " / " + row[cc] + " // " + r[text]);
                    cc += 1;
                }
                dt.Rows.Add(row);
            }
            // Log actions to textfile
            //GlobalVars.Log("SQLHelper-DbQuery (END)", "Finished executing Query");
            GlobalVars.Log("SQLHelper-DbQuery (Finished executing Query)", "Number of Rows returned by query: " + Convert.ToString(dt.Rows.Count));
            // Dispose (Close) Connection to DB
            cmd.Dispose();
            DbClose(conn);
            // Apply dt changes and return
            dt.AcceptChanges();
            return dt;
        }
        // Get all from single column
        public List<string> DbQrySingle(string tableName, string col, string From)
        {
            // Create Connection to database
            SQLiteConnection conn = DbOpen();
            SQLiteCommand cmd = new SQLiteCommand(conn);
            string qry = $"SELECT {col} FROM {tableName}";
            cmd.CommandText = qry;

            // Initiate the list
            List<string> list = new List<string>();
            // Execute query
            SQLiteDataReader r = cmd.ExecuteReader();
            GlobalVars.Log($"SQLHelper-DbQrySingle (START)(From: {From})", "qry: " + qry);
            
            // Get results
            while (r.Read())
            {
                list.Add(r[0].ToString());
            }
            GlobalVars.Log($"SQLHelper-DbQrySingle (END)(From: {From})", "Rows returned: " + list.Count.ToString());

            // Dispose res
            cmd.Dispose();
            DbClose(conn);
            // Return list of results
            return list;
        }
        // Insert new record, return lastID of insert, otherwise 0
        public int DbInsertMovie(DataTable dt, string callFrom)
        {
            // Set columns
            string colsInfo = "";
            //string cols_qry = "";

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
            SQLiteConnection conn = DbOpen();
            SQLiteCommand cmd = new SQLiteCommand(conn);

            // Make Transaction
            SQLiteTransaction transaction = null;
            transaction = conn.BeginTransaction();

            // Vars
            int rows = 0; // rows returned by query
            string vals = ""; // all values stored in row of DT
            int cc = 0; // counts string[] vals size

            // Vars for filepath
            string fPathFile = "", fPathSub = "", fPathTrailer = "";

            // Create a copy of info table
            var foos = new List<string>(GlobalVars.DB_TABLE_INFO);
            foos.RemoveAt(0); // remove [Id] from table info
            string[] Arrcols = foos.ToArray(); // turn column list to array

            // Get rows from DT
            foreach (DataRow r in dt.Rows)
            {
                vals = ""; // Reset values to add
                cc = 0; // reset column count
                // Build query for INFO
                foreach (string s in Arrcols)
                {
                    if ((s == "category") || (s == "year") || (s == "season") || (s == "episode"))
                    {
                        vals += GlobalVars.QryString(r[cc].ToString(), false) + ",";
                    }
                    else
                    {
                        vals += GlobalVars.QryString(r[cc].ToString(), true) + ",";
                    }
                    cc += 1;
                }
                vals = vals.TrimEnd(',');

                // Build for FILEPATH
                fPathFile = r[Arrcols.Length].ToString(); // file
                fPathSub = r[Arrcols.Length + 1].ToString(); // sub
                fPathTrailer = r[Arrcols.Length + 2].ToString(); // trailer

                // Set the command for query
                string qry = $"INSERT INTO {GlobalVars.DB_TNAME_INFO} ({colsInfo}) VALUES({vals});";
                cmd.CommandText = qry;

                // Try Execute query for INFO
                GlobalVars.Log($"SQLHelper-DbInsertMovie (INSERT MOVIE START)({callFrom})", "Start Try inserting movie");
                try
                {
                    // Execute query for INFO
                    cmd.ExecuteNonQuery();
                    GlobalVars.Log($"SQLHelper-DbInsertMovie ({GlobalVars.DB_TNAME_INFO})({callFrom})", $"qry: {qry}");

                    // LastID of insert movie ID
                    string LastID = conn.LastInsertRowId.ToString();
                    //rows = Convert.ToInt32(LastID);
                    //GlobalVars.Log($"{callFrom} (LAST INSERT ID)", LastID);

                    // Try Execute query for FILEPATH
                    try
                    {
                        string colsFile = $"[{GlobalVars.DB_TABLE_FILEPATH[0]}],[{GlobalVars.DB_TABLE_FILEPATH[1]}],[{GlobalVars.DB_TABLE_FILEPATH[2]}],[{GlobalVars.DB_TABLE_FILEPATH[3]}]";
                        qry = $"INSERT INTO {GlobalVars.DB_TNAME_FILEPATH} ({colsFile}) VALUES({LastID},'{fPathFile}','{fPathSub}','{fPathTrailer}');";
                        cmd.CommandText = qry;
                        cmd.ExecuteNonQuery();
                        GlobalVars.Log($"SQLHelper-DbInsertMovie ({GlobalVars.DB_TNAME_FILEPATH})({callFrom})", $"qry: {qry}");
                        rows += 1;
                    }
                    // Catch error on executing query for FILEPATH
                    catch (Exception e)
                    {
                        // Catch unknown exceptions
                        GlobalVars.Log($"SQLHelper-DbInsertMovie (Insert error)({GlobalVars.DB_TNAME_FILEPATH})({callFrom})", "Error: " + e.Message);
                    }
                }
                // Catch error on executing query for INFO
                catch (Exception e)
                {
                    // Catch unknown exceptions (GlobalVars.ShowError)
                    GlobalVars.Log($"SQLHelper-DbInsertMovie (Insert error)({GlobalVars.DB_TNAME_INFO})({callFrom})", "Error: " + e.Message);
                }

                // Exit foreach
                //break;
            }
            // Release memory
            GlobalVars.Log($"SQLHelper-DbInsertMovie (FINISHED INSERT)({callFrom})", $"Rows returned: ({rows.ToString()})");
            dt.Clear();
            dt.Dispose();

            // Commit transac
            transaction.Commit();

            // Dispose (Close) Connection to DB
            cmd.Dispose();
            DbClose(conn);

            // Clear list
            foos.Clear();

            // Return rows affected
            return rows;
        }
        // update INFO
        public bool DbUpdateInfo(DataTable dt, string from)
        {
            // Set values
            string TableName = GlobalVars.DB_TNAME_INFO;
            string callFrom = $"SQLHelper-DbUpdateInfo ({from})";
            string valpair = "";
            string r0 = "";
            foreach (DataRow r in dt.Rows)
            {
                r0 = r[0].ToString();
                for (int i = 1; i < GlobalVars.DB_TABLE_INFO.Length; i++)
                {
                    if ((GlobalVars.DB_TABLE_INFO[i] == "category") || (GlobalVars.DB_TABLE_INFO[i] == "year") || (GlobalVars.DB_TABLE_INFO[i] == "season") || (GlobalVars.DB_TABLE_INFO[i] == "episode"))
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
            //GlobalVars.Log("SQLHelper-DbUpdateInfo", valpair);
            // dispose table
            dt.Clear();
            dt.Dispose();
            // Query to db
            string qry = $"UPDATE {TableName} " +
                         "SET " + valpair + " " +
                        $"WHERE [Id] = {r0}";
            GlobalVars.Log(callFrom, $"Update {GlobalVars.DB_TNAME_INFO} ID ({r0}) || Query: {qry}");
            if (DbExecNonQuery(qry))
            {
                GlobalVars.Log(callFrom, $"ID ({r0}) is updated Succesfully!");
                return true;
            }
            return false;
        }
        // update FILEPATH
        public bool DbUpdateFilepath(DataTable dt, string from)
        {
            // Set values
            string TableName = GlobalVars.DB_TNAME_FILEPATH;
            string callFrom = $"SQLHelper-DbUpdateFilepath ({from})";
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
            //GlobalVars.Log("SQLHelper-DbUpdateInfo", valpair);
            // dispose table
            dt.Clear();
            dt.Dispose();
            // Query to db
            string qry = $"UPDATE {TableName} " +
                         "SET " + valpair + " " +
                        $"WHERE [Id] = {r0}";
            GlobalVars.Log(callFrom, $"Update {GlobalVars.DB_TNAME_FILEPATH} ID ({r0}) || Query: {qry}");
            if (DbExecNonQuery(qry))
            {
                GlobalVars.Log(callFrom, $"ID ({r0}) is updated Succesfully!");
                return true;
            }
            return false;
        }
        // Delete MOVIE from Record
        public bool DbDeleteMovie(string ID, string errFrom)
        {
            // Remove info
            string qry = $"DELETE FROM {GlobalVars.DB_TNAME_INFO} WHERE [Id] = {ID};";
            if (DbExecNonQuery(qry))
            {
                GlobalVars.Log(errFrom, $"ID ({ID}) is removed from database! Table: ({GlobalVars.DB_TNAME_INFO})");
                // Remove filepath
                qry = $"DELETE FROM {GlobalVars.DB_TNAME_FILEPATH} WHERE [Id] = {ID};";
                if (DbExecNonQuery(qry))
                {
                    GlobalVars.Log(errFrom, $"ID ({ID}) is removed from database! Table: ({GlobalVars.DB_TNAME_FILEPATH})");
                    return true;
                }
            }
            return false;
        }
    }
}
