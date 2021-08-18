﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema
{
    public static class Logs
    {
        /// <summary>
        /// Log messages to text file
        /// </summary>
        /// <param name="filePath">full filepath of log file</param>
        /// <param name="codefrom">form or method that calls</param>
        /// <param name="log">string to log</param>
        public static void Log(string filePath, string codefrom, string log)
        {
            string toLog = log;
            if (!String.IsNullOrWhiteSpace(GlobalVars.TMDB_KEY))
            {
                toLog = toLog.Replace(GlobalVars.TMDB_KEY, "TMDB_KEY");
            }
            if (!File.Exists(filePath)) { GlobalVars.WriteToFile(filePath, ""); }
            try
            {
                using (StreamWriter w = File.AppendText(filePath))
                {
                    w.Write(LogFormatted(codefrom, toLog));
                }
            }
            catch (Exception ex)
            {
                GlobalVars.ShowError("GlobalVars-Log", ex, false);
            }
        }
        /// <summary>
        /// Format string for logging
        /// </summary>
        /// <param name="codefrom">form or method that calls</param>
        /// <param name="logMessage">string to format</param>
        /// <returns>Formatted message with time/date</returns>
        public static string LogFormatted(string codefrom, string logMessage)
        {
            try
            {
                return ($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff")}]: [{ codefrom }] { logMessage }\n");
            }
            catch { return $"[Unknown DateTime][{ codefrom }] { logMessage }\n"; }
        }
        /// <summary>
        /// Log database-related functions, to text file.
        /// </summary>
        /// <param name="codefrom">form or method that calls</param>
        /// <param name="log">string to log</param>
        public static void LogDb(string codefrom, string log)
        {
            Log(DataFile.FILE_LOG_DB, codefrom, log);
        }
        /// <summary>
        /// Log text to App_Log.log file
        /// </summary>
        /// <param name="codefrom">form or method that calls</param>
        /// <param name="log">string to log</param>
        public static void Log(string codefrom, string log)
        {
            Log(DataFile.FILE_LOG_APP, codefrom, log);
        }
        /// <summary>
        /// LOG Error Message to App_ErrorLog.log
        /// </summary>
        /// <param name="codefrom">Method caller</param>
        /// <param name="log">string to log</param>
        public static void LogErr(string codefrom, string log)
        {
            Log(DataFile.FILE_LOG_ERROR, codefrom, log);
        }
        public static void LogDebug(string log)
        {
            if (!GlobalVars.DEBUGGING) { return; }
            Log(Path.Combine(GlobalVars.PATH_LOG, "DEBUG.log"), "", log);
        }
    }
}
