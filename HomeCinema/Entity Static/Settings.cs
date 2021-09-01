using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace HomeCinema
{
    public static class Settings
    {
        #region Properties
        // Toggle Portable Mode
        public static bool PortableMode { get; set; } = false;

        // Properties
        private static int MaxLogSize_v = 0; // Maximum file size of Logfile. (in MegaBytes)
        public static long MaxLogSize
        { 
            get { return MaxLogSize_v * 1000000; } // Convert to bytes when using
            set { MaxLogSize_v = (int)value; }
        }

        // Last Path saved for changing cover image
        public static string LastPathCover { get; set; }

        // Last path saved for changing media source
        public static string LastPathVideo { get; set; }

        // Use [automatic online functionalities] or not.
        public static bool IsOffline { get; set; }

        // auto play movie on double-click, instead of opening movie info
        public static bool IsAutoplay { get; set; }

        // auto check for update
        public static bool IsAutoUpdate { get; set; }

        // Automatically clean temp and logs on App Load
        public static bool IsAutoClean { get; set; }

        // Confirm prompt for searching / reloading ListView
        public static bool IsConfirmSearch { get; set; }

        // Use confirmation prompts on most dialogs
        public static bool IsConfirmMsg { get; set; }

        // Skip entries not in media location settings
        public static bool IsSkipNotMediaLoc { get; set; }

        // limit the max items to query. '0' means unlimited
        public static int ItemLimit { get; set; }

        // limit for searching on IMDB Id. '0' means unlimited
        public static int SearchLimit { get; set; }

        // TimeOut in seconds (1000 ms)
        public static int TimeOut { get; set; }

        // Width of Cover image
        public static int ImgTileWidth { get; set; }

        // Height of Cover image
        public static int ImgTileHeight { get; set; }

        // default color for background
        public static Color ColorBg { get; set; }

        // default font color
        public static Color ColorFont { get; set; }
        #endregion

        #region Methods
        // Methods
        public static void LoadSettings()
        {
            SettingJson config;
            string errorFrom = "Settings-LoadSettings";
            string contents, sLastPathCover, sLastPathVideo;
            // If file does not exist, create it with default values from [Config.cs]
            try
            {
                if (File.Exists(DataFile.FILE_SETTINGS) == false)
                {
                    config = new SettingJson();
                    contents = JsonConvert.SerializeObject(config, Formatting.Indented);
                    GlobalVars.WriteToFile(DataFile.FILE_SETTINGS, contents);
                }
                else
                {
                    contents = GlobalVars.ReadStringFromFile(DataFile.FILE_SETTINGS, $"{errorFrom} [FILE_SETTINGS]");
                    config = JsonConvert.DeserializeObject<SettingJson>(contents, GlobalVars.JSON_SETTING);
                }
            }
            catch (Exception ex)
            {
                Msg.ShowError(errorFrom, ex);
                config = new SettingJson();
            }
            if (config.isdebugging)
                GlobalVars.DEBUGGING = true;

            // Get Max log file size
            MaxLogSize = config.logsize;
            // Get last path of poster image
            sLastPathCover = config.lastPathCover;
            if (String.IsNullOrWhiteSpace(sLastPathCover))
            {
                try
                {
                    sLastPathCover = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                }
                catch (Exception ex)
                {
                    sLastPathCover = DataFile.PATH_START;
                    Msg.ShowError($"{errorFrom} [LastPathCover]", ex);
                }
            }
            LastPathCover = sLastPathCover;
            // Get last path of media file when adding new one
            sLastPathVideo = config.lastPathVideo;
            if (String.IsNullOrWhiteSpace(sLastPathVideo))
            {
                try
                {
                    sLastPathVideo = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
                }
                catch (Exception ex)
                {
                    sLastPathVideo = DataFile.PATH_START;
                    Msg.ShowError($"{errorFrom} [LastPathVideo]", ex);
                }
            }
            LastPathVideo = sLastPathVideo;
            // Booleans
            IsOffline = config.offlineMode; // Get Offline Mode
            IsAutoUpdate = config.autoUpdate; // Get auto update
            IsAutoplay = config.instantPlayMovie; // AutoPlay Movie, instead of Viewing its Info / Details
            IsAutoClean = config.autoClean; // Auto clean on startup
            IsConfirmSearch = config.confirmSearch; // Confirm prompts on search and reload
            IsConfirmMsg = config.confirmMessages; // Confirm prompts on some actions
            IsSkipNotMediaLoc = config.skipEntryNotInMediaLoc; // Skip entry if root folder is not on media location
            // Values
            ItemLimit = config.itemMaxLimit; // Limit MAX items in query 
            SearchLimit = config.searchLimit; // Limit Item result on IMDB searching
            TimeOut = config.setTimeOut; // TimeOut for Internet connections            
            // Set theme
            ImgTileWidth = config.ImgTileWidth; // Cover Image width
            ImgTileHeight = config.ImgTileHeight; // Cover Image height
            try // set background color
            {
                ColorBg = ColorTranslator.FromHtml($"#{config.BackgroundColor}");
            }
            catch { ColorBg = Color.Black; }
            try // set foreground color
            {
                ColorFont = ColorTranslator.FromHtml($"#{config.FontColor}");
            }
            catch { ColorFont = Color.White; }
        }
        public static bool SaveSettings()
        {
            var config = new SettingJson();
            
            config.autoUpdate = IsAutoUpdate;
            config.offlineMode = IsOffline;
            config.instantPlayMovie = IsAutoplay;
            config.autoClean = IsAutoClean;
            config.confirmSearch = IsConfirmSearch;
            config.confirmMessages = IsConfirmMsg;
            config.skipEntryNotInMediaLoc = IsSkipNotMediaLoc;

            config.lastPathCover = LastPathCover;
            config.lastPathVideo = LastPathVideo;

            config.logsize = MaxLogSize_v;
            config.itemMaxLimit = ItemLimit;
            config.searchLimit = SearchLimit;
            config.setTimeOut = TimeOut;
            config.BackgroundColor = ColorBg.ToArgb().ToString("x");
            config.FontColor = ColorFont.ToArgb().ToString("x");
            
            config.ImgTileWidth = Convert.ToInt32(ImgTileWidth);
            config.ImgTileHeight = Convert.ToInt32(ImgTileHeight);

            // Seriliaze to JSON
            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            return GlobalVars.WriteToFile(DataFile.FILE_SETTINGS, json);
        }
        #endregion
    }
}
