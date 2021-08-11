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
        // Properties
        private static int MaxLogSize_v = 0; // Maximum file size of Logfile. (in Bytes)
        public static int MaxLogSize
        { 
            get { return MaxLogSize_v * 1000000; } // Convert to Megabytes when using
            set { MaxLogSize_v = value; }
        }

        private static string LastPathCover_v = ""; // Last Path saved for changing cover image
        public static string LastPathCover
        {
            get { return LastPathCover_v; }
            set { LastPathCover_v = value; }
        }

        private static string LastPathVideo_v = ""; // Last path saved for changing media source
        public static string LastPathVideo
        {
            get { return LastPathVideo_v; }
            set { LastPathVideo_v = value; }
        }

        private static bool IsOffline_v = false; // Use [automatic online functionalities] or not.
        public static bool IsOffline
        {
            get { return IsOffline_v; }
            set { IsOffline_v = value; }
        }

        private static bool IsAutoplay_v = false; // auto play movie
        public static bool IsAutoplay
        {
            get { return IsAutoplay_v; }
            set { IsAutoplay_v = value; }
        }

        private static bool IsAutoUpdate_v = true; // auto check for update
        public static bool IsAutoUpdate
        {
            get { return IsAutoUpdate_v; }
            set { IsAutoUpdate_v = value; }
        }

        private static bool IsAutoClean_v = false; // Automatically clean temp and logs on App Load
        public static bool IsAutoClean
        {
            get { return IsAutoClean_v; }
            set { IsAutoClean_v = value; }
        }

        private static bool IsConfirmSearch_v = false; // Confirm prompt for searching / reloading ListView
        public static bool IsConfirmSearch
        {
            get { return IsConfirmSearch_v; }
            set { IsConfirmSearch_v = value; }
        }

        private static int ItemLimit_v = 0; // limit the max items to query. '0' means unlimited
        public static int ItemLimit
        {
            get { return ItemLimit_v; }
            set { ItemLimit_v = value; }
        }

        private static int SearchLimit_v = 0; // limit for searching on IMDB Id. '0' means unlimited
        public static int SearchLimit
        {
            get { return SearchLimit_v; }
            set { SearchLimit_v = value; }
        }

        private static int TimeOut_v = 3; // TimeOut in seconds (1000 ms)
        public static int TimeOut
        {
            get { return TimeOut_v; }
            set { TimeOut_v = value; }
        }

        private static int ImgTileWidth_v = 96; // Width of Cover image
        public static int ImgTileWidth
        {
            get { return ImgTileWidth_v; }
            set { ImgTileWidth_v = value; }
        }

        private static int ImgTileHeight_v = 128; // Height of Cover image
        public static int ImgTileHeight
        {
            get { return ImgTileHeight_v; }
            set { ImgTileHeight_v = value; }
        }

        private static Color ColorBg_v = Color.Black; // default color for background
        public static Color ColorBg
        {
            get { return ColorBg_v; }
            set { ColorBg_v = value; }
        }

        private static Color ColorFont_v = Color.White; // default font color
        public static Color ColorFont
        {
            get { return ColorFont_v; }
            set { ColorFont_v = value; }
        }
        #endregion

        #region Methods
        // Methods
        public static void LoadSettings()
        {
            SettingJson config;
            string errorFrom = "Settings-LoadSettings";
            string contents, sLastPathCover, sLastPathVideo;
            // If file does not exist, create it with default values from [Config.cs]
            if (File.Exists(GlobalVars.FILE_SETTINGS) == false)
            {
                config = new SettingJson();
                contents = JsonConvert.SerializeObject(config, Formatting.Indented);
                GlobalVars.WriteToFile(GlobalVars.FILE_SETTINGS, contents);
            }
            else
            {
                // Load file contents to Config
                contents = GlobalVars.ReadStringFromFile(GlobalVars.FILE_SETTINGS, $"{errorFrom} [FILE_SETTINGS]");
                config = JsonConvert.DeserializeObject<SettingJson>(contents, GlobalVars.JSON_SETTING);
            }

            // Get Max log file size
            MaxLogSize = config.logsize;
            // Get last path of poster image
            sLastPathCover = config.lastPathCover;
            if (String.IsNullOrWhiteSpace(sLastPathCover) == false)
            {
                LastPathCover = sLastPathCover;
            }
            else
            {
                try
                {
                    LastPathCover = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                }
                catch (Exception ex)
                {
                    GlobalVars.ShowError($"{errorFrom} [LastPathCover]", ex, false);
                }
            }
            // Get last path of media file when adding new one
            sLastPathVideo = config.lastPathVideo;
            if (String.IsNullOrWhiteSpace(sLastPathVideo) == false)
            {
                LastPathVideo = sLastPathVideo;
            }
            else
            {
                try
                {
                    LastPathVideo = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
                }
                catch (Exception ex)
                {
                    GlobalVars.ShowError($"{errorFrom} [PATH_GETVIDEO]", ex, false);
                }
            }

            IsOffline = Convert.ToBoolean(config.offlineMode); // Get Offline Mode
            IsAutoUpdate = Convert.ToBoolean(config.autoUpdate); // Get auto update
            IsAutoplay = Convert.ToBoolean(config.instantPlayMovie); // AutoPlay Movie, instead of Viewing its Info / Details
            IsAutoClean = Convert.ToBoolean(config.autoClean); // Auto clean on startup
            IsConfirmSearch = Convert.ToBoolean(config.confirmSearch); // Confirm prompts on search and reload

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
            config.logsize = MaxLogSize_v;
            config.offlineMode = Convert.ToInt16(IsOffline);
            config.lastPathCover = LastPathCover;
            config.lastPathVideo = LastPathVideo;
            config.autoUpdate = Convert.ToInt16(IsAutoUpdate);
            config.instantPlayMovie = Convert.ToInt16(IsAutoplay);
            config.autoClean = Convert.ToInt16(IsAutoClean);
            config.itemMaxLimit = ItemLimit;
            config.searchLimit = SearchLimit;
            config.setTimeOut = TimeOut;
            config.BackgroundColor = ColorBg.ToArgb().ToString("x");
            config.FontColor = ColorFont.ToArgb().ToString("x");
            config.confirmSearch = Convert.ToInt16(IsConfirmSearch);
            config.ImgTileWidth = Convert.ToInt32(ImgTileWidth);
            config.ImgTileHeight = Convert.ToInt32(ImgTileHeight);

            // Seriliaze to JSON
            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            return GlobalVars.WriteToFile(GlobalVars.FILE_SETTINGS, json);
        }
        #endregion
    }
}
