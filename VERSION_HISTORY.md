# HomeCinema - Project History:

## v0.0.8.0 ***build 24 - (22 September 2020 PHT)***
### [Series] can now be Added!

Build from latest commit: [2070c18](https://github.com/JerloPH/HomeCinema/commit/2070c18938bde878abeb19c2c3fd3e2dd6b84f41)

**[Download link for Windows 64 bit](https://github.com/JerloPH/HomeCinema/releases/download/v0.0.8.0/HomeCinema-Windows_v0.0.8.0.zip "HomeCinema-Windows_v0.0.8.0.zip")**

[![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/v0.0.8.0/total.svg)]() <br>

**What's New?**
- REV: Add your "**Series**" top-folder into the App!
- REV: Removed **[Add Movie]** Button.
- FT: Added [**Hide Animation**] checkbox, to filter out *Anime and Cartoons*.
- FIX: Allow to add File names / folders with single quotations ( **'** ).
- MINOR: Show Message when error occurs on Settings changes.
- GUI: Fix multiple label and tooltips mistakes.
- GUI: Changed the **[Loading]** Icon.
- Code cleanup, and improvements.
****

## v0.0.7.0 ***build 23 - (17 September 2020 PHT)***
### More Features and Fixes

Build from latest commit: [e1c4a97](https://github.com/JerloPH/HomeCinema/commit/e1c4a97bb763b8df0f8ad840db507501e250b72a)

**[Download link for Windows 64 bit](https://github.com/JerloPH/HomeCinema/releases/download/v0.0.7.0/HomeCinema-Windows_v0.0.7.0.zip "HomeCinema-Windows_v0.0.7.0.zip")**

[![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/v0.0.7.0/total.svg)]() <br>

**What's New?**
- FT: Automatically Download cover image from [TMDB](https://www.themoviedb.org/), when online, during insert of new media.
- FT: Added **[Settings]** button.
- FT: Set LIMIT count to search results. *See **Item Display Count** in [Settings]*
- FIX: Search results error:
	- Other filters can override *Title* search.
	- *Category* can override other filters.
- MINOR: Clean JSON files from **temp** folder, when **[Clean]** button is pressed.
- MINOR: Automatically remove excess final backslash, from Folder paths.
- GUI: Removed **[Show New]** button.
- RES: Changed App Icon.
- Code cleanup, and improvements.
****

## v0.0.6.3 ***build 22 - (06 September 2020 PHT)***
### Optimizations and Fixes

Build from latest commit: [4b2f74d](https://github.com/JerloPH/HomeCinema/commit/4b2f74d59475f95a48fb7cecbda3e819cbf6fd0e)

**[Download link for Windows 64 bit](https://github.com/JerloPH/HomeCinema/releases/download/v0.0.6.3/HomeCinema-Windows_v0.0.6.3.zip "HomeCinema-Windows_v0.0.6.3.zip")**

[![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/v0.0.6.3/total.svg)]() <br>

**What's New?**
- FIX: Properly save the *genres* to file.
- MINOR: Alpabetically arrange country list properly.
- GUI: Settings form minor change.
- Optimizations and code cleanups.
****

## v0.0.6.2 ***build 21 - (05 September 2020 PHT)***
### Additional Features and Fixes

Build from latest commit: [6d0e4d6](https://github.com/JerloPH/HomeCinema/commit/6d0e4d68d9ced73bdaaa6d7b143c8ec7ac34e4b7)

**[Download link for Windows 64 bit](https://github.com/JerloPH/HomeCinema/releases/download/v0.0.6.2/HomeCinema-Windows_v0.0.6.2.zip "HomeCinema-Windows_v0.0.6.2.zip")**

[![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/v0.0.6.2/total.svg)]() <br>

**What's New?**
- REV: Speed improvements on Main Form.
- FT: Fetch additional information from [**The Movie Database**](https://www.themoviedb.org/).
  - Fetch: Country (Based on Producers).
- FT: Automatically add *category* to the movie. *Movie, Animated Movie, etc..*
- FIX: Problems with *single quotation characters ( **'** )*.
- FIX: Settings Form: Closing the form when *Cancel* button is clicked.
- Code cleanups.
****

## v0.0.6.1 ***build 20 - (30 August 2020 PHT)***
### Follow up Fixes

Build from latest commit: [9e37136](https://github.com/JerloPH/HomeCinema/commit/9e371367d44ab5b56a7368f9ceabfe280f9902b6)

**[Download link for Windows 64 bit](https://github.com/JerloPH/HomeCinema/releases/download/v0.0.6.1/HomeCinema-Windows_v0.0.6.1.zip "HomeCinema-Windows_v0.0.6.1.zip")**

[![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/v0.0.6.1/total.svg)]() <br>

**What's New?**
- FIX: Cast Textbox not reset when CLEAR button is pressed.
- FIX: Another Attempt to fix cover not updating when fetched from TMDB.
- Minor Code cleanups.
****

## v0.0.6.0 ***build 19 - (29 August 2020 PHT)***
### Major Update

Build from latest commit: [6a4d90e](https://github.com/JerloPH/HomeCinema/commit/6a4d90e146e90100d2763a1fcc59bc5ba197cf6b)

**[Download link for Windows 64 bit](https://github.com/JerloPH/HomeCinema/releases/download/v0.0.6.0/HomeCinema-Windows_v0.0.6.0.zip "HomeCinema-Windows_v0.0.6.0.zip")**

[![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/v0.0.6.0/total.svg)]() <br>

**What's New?**
- REV: Better handle on downloading files in Fetching data from TMDB.
- FT: Fetch additional information from [**The Movie Database**](https://www.themoviedb.org/).
  - Fetch: Artists, Director, and Producer.
- FT: Added Right-click ContextMenu to ListView Item.
  - *[Find File in Explorer]* - Open Explorer and Show where the file is located on the drive.
- FIX: Don't ask for changing cover image when no JSON file is fetched.
- MINOR: Better Closing Log. Functions return type changed.
- MINOR: Logs of files skipped during startup.
- GUI: Changed Title Caption.
- GUI: Reverted SearchBox Placeholder text to Color Black.
- DOC: Added download count.
- Code cleanup and improvements.
****

## v0.0.5.1 ***build 18 - (15 August 2020 PHT)***
### Quick Fixes

Build from latest commit: [8c57be2](https://github.com/JerloPH/HomeCinema/commit/8c57be28fed2c74e2c49c947ff08246de7cc884a)

**[Download link for Windows 64 bit](https://github.com/JerloPH/HomeCinema/releases/download/v0.0.5.1/HomeCinema-Windows_v0.0.5.1.zip "HomeCinema-Windows_v0.0.5.1.zip")**

[![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/v0.0.5.1/total.svg)]() <br>

**What's New?**
- FIX: Hangs on Fetching data when there is no Internet Connection.
- FIX: Try to fix Image Cover sometimes not loading.
****

## v0.0.5.0 ***build 17 - (14 August 2020 PHT)***
### Automatic Data Fetching

Build from latest commit: [203de9d](https://github.com/JerloPH/HomeCinema/commit/203de9d8738e31eb9805320f878b91b477b58db6)

**[Download link for Windows 64 bit](https://github.com/JerloPH/HomeCinema/releases/download/v0.0.5.0/HomeCinema-Windows_v0.0.5.0.zip "HomeCinema-Windows_v0.0.5.0.zip")**

[![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/v0.0.5.0/total.svg)]() <br>

**What's New?**
- REV: Automatically fetch movie details, when the automatic media scan adds a new *Movie Item*.
- REV: Improved how *Cover Image* is loaded when fetching data from [**The Movie Database**](https://www.themoviedb.org/).
- REV: Heavy Improvements to Code.
- MINOR: Delete Logfile for DataBase when file reached **Max Log File Size** in **Settings**.
- MINOR: Make Most MessageBox *Top Most / Appear In front of other Forms*.
- MINOR: Use a proper *Movie Title* from meda filename, when fetching from [**TMDB**](https://www.themoviedb.org/) is unavailable.
- GUI: Changed Text and Background Color for Controls in Main Form.
- Various Code cleanup, and error-logging improvements.
****

## v0.0.4.0 ***build 16 - (12 August 2020 PHT)***
### Settings UI and Improvements

Build from latest commit: [0b2987a](https://github.com/JerloPH/HomeCinema/commit/0b2987a6f0e5b9717e5c4abe9a283bbb4d4382e5)

**[Download link for Windows 64 bit](https://github.com/JerloPH/HomeCinema/releases/download/v0.0.4.0/HomeCinema-Windows_v0.0.4.0.zip "HomeCinema-Windows_v0.0.4.0.zip")**

[![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/v0.0.4.0/total.svg)]() <br>

**What's New?**
- REV: Settings UI. Press **Control + S** to Open Settings UI.
- FT: Update *country* and *media_location* files on Settings' changes.
- MINOR: Faster searching for files already existing in DB.
- FIX: When Genre: Animation, is selected, it also shows non-Animation.
- FIX: Infinite loop on *Please wait form*.
- CHANGE: category filters now show all Movies, *Eg. Show Anime and Animation Movies on selecting "Movie" filter.*
- GUI: Sorting Button -> ComboBox.
- GUI changes.
- Various improvements and optimizations, and code cleanup.
****

## v0.0.3.3 ***build 15 - (27 July 2020 PHT)***
### Performance Improvement

Build from latest commit: [a633a4b](https://github.com/JerloPH/HomeCinema/commit/a633a4b62f15cedb7b6bb67f90cfea0a268eb9be)

**[Download link for Windows 64 bit](https://github.com/JerloPH/HomeCinema/releases/download/v0.0.3.3/HomeCinema-Windows_v0.0.3.3.zip "HomeCinema-Windows_v0.0.3.3.zip")**

[![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/v0.0.3.3/total.svg)]() <br>

**What's New?**
- REV: Changed how Movie Item is Refreshed. Instead of performing query, only the selected Movie is refreshed.
- FT: Added Right-click ContextMenu to ListView Item.
  - *[Edit Information]* - Edit Movie Information.
- MINOR: Added Tooltips to Buttons.
- MINOR: Improved Error-Logging and Checking.
- RES: Updated settings default values.
- Image-related Code Improvements.
- Various improvements and optimizations, and code cleanup.
****

## v0.0.3.2 ***build 14 - (23 July 2020 PHT)***
### Optimizations and More Features

Build from latest commit: [49aee99](https://github.com/JerloPH/HomeCinema/commit/49aee99)

**[Download link for Windows 64 bit](https://github.com/JerloPH/HomeCinema/releases/download/v0.0.3.2/HomeCinema-Windows_v0.0.3.2.zip "HomeCinema-Windows_v0.0.3.2.zip")**

[![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/v0.0.3.2/total.svg)]() <br>

**What's New?**
- REV: Improve loading of Movie, by using ProgressChanged on BGworker.
- FT: Added ToolTip text to ListView Item. Shows Movie Summary and Genre.
- FT: Added Right-click ContextMenu to ListView Item.
  - *[View Details]* - Shows Movie Information / Details form.
- Minor improvements and optimizations, and code cleanup.
****

## v0.0.2.2 ***build 13 - (30 June 2020 PHT)***
### Minor Update Fix

Build from latest commit: [e51182d](https://github.com/JerloPH/HomeCinema/commit/e51182d)

**[Download link for Windows 32/64 bit](https://github.com/JerloPH/HomeCinema/releases/download/v0.0.2.2/HomeCinema-Windows_v0.0.2.2.zip "HomeCinema-Windows_v0.0.2.2.zip")**

[![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/v0.0.2.2/total.svg)]() <br>

**What's New?**
- FIX: Auto-check-update feature.
- CHANGE: Auto-create settings File with default values, if not existing.
****

## v0.0.2.1 ***build 12 - (07 June 2020 PHT)***
### Various GUI and Settings adjustments

Build from latest commit: [a35d7ed](https://github.com/JerloPH/HomeCinema/commit/a35d7ed)

**[Download link for Windows 32/64 bit](https://github.com/JerloPH/HomeCinema/releases/download/v0.0.2.1/HomeCinema-Windows_v0.0.2.1.zip "HomeCinema-Windows_v0.0.2.1.zip")**

[![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/v0.0.2.1/total.svg)]() <br>

**What's New?**
- FT: Auto check for updates.
- FT: Added **Sort Order** option: Sort Items in Ascending or Descending order.
- FT: Added new setting to autoplay media files, instead of viewing its details.
- FIX: **Sort by Year** not working properly.
- GUI adjustments.
- Minor improvements and code cleanup.
****

## v0.0.2.0 ***build 11 - (25 May 2020 PHT)***
### Added In-App Settings

Build from latest commit: [e59d3ae](https://github.com/JerloPH/HomeCinema/commit/e59d3ae)

**[Download link for Windows 32/64 bit](https://github.com/JerloPH/HomeCinema/releases/download/v0.0.2.0/HomeCinema-Windows_v0.0.2.0.zip "HomeCinema-Windows_v0.0.2.0.zip")**

[![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/v0.0.2.0/total.svg)]() <br>

**What's New?**
- REV: **Load** and **Save** settings to [*settings file*](https://github.com/JerloPH/HomeCinema/blob/v0.0.2.0/HomeCinema/Resources/settings.json).
- REV: Added "*Settings*" values to [*settings file*](https://github.com/JerloPH/HomeCinema/blob/v0.0.2.0/HomeCinema/Resources/settings.json).
- FT: Generate list of "*Genre*" of Movie and Automatically tick the Checkbox in Movie Info Edit. [*Button*]: **Fetch Data**.
- CHANGE: If *Original title* is the same as *Title*, don't apply it.
- Minor GUI adjustments.
- Minor code adjustments and improvements.
****

## v0.0.1.1 ***build 10 - (24 May 2020 PHT)***
### Quick Update

Build from latest commit: [3e3f94b](https://github.com/JerloPH/HomeCinema/commit/3e3f94b)

**[Download link for Windows 32/64 bit](https://github.com/JerloPH/HomeCinema/releases/download/v0.0.1.1/HomeCinema-Windows_v0.0.1.1.zip "HomeCinema-Windows_v0.0.1.1.zip")**

[![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/v0.0.1.1/total.svg)]() <br>

**What's New?**
- ADD: *[Settings File](https://github.com/JerloPH/HomeCinema/blob/v0.0.1.1/HomeCinema/Resources/settings.json)* to save persistent settings in App.
- CHANGE: Open Movie when *ENTER* Key is pressed. 
- Minor improvements and code adjustments.
****

## v0.0.1.0 ***build 9 - (23 May 2020 PHT)***
### Improvements to MovieInfo

Build from latest commit: [33a6d77](https://github.com/JerloPH/HomeCinema/commit/33a6d77)

**[Download link for Windows 32/64 bit](https://github.com/JerloPH/HomeCinema/releases/download/v0.0.1.0/HomeCinema-Windows_v0.0.1.0.zip "HomeCinema-Windows_v0.0.1.0.zip")**

[![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/v0.0.1.0/total.svg)]() <br>

**What's New?**
- REV: Use *[Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/)* to parse JSON files.
- REV: Main form is scalable - Can be Maximized and Resized.
- ADD: **Director** Textbox filter.
- CHANGE: Move files to recycle bin, instead of permanently deleting.
- CHANGE: Force overwrite JSON file for Finding IMDB ID of Movie.
- FIX: Validate search strings for buttons: **Search IMDB** and **Fetch Data**.
- FIX: Double-clicking an item on ListView Movies is not working.
- RES: Changed screenshot.
- GUI adjustments.
- Minor adjustments and Code cleanup.
****

## v0.0.0.8 ***build 8 - (21 May 2020 PHT)***
### TMDB Fetching Follow-up

Build from latest commit: [e1cc236](https://github.com/JerloPH/HomeCinema/commit/e1cc236)

**[Download link for Windows 32/64 bit](https://github.com/JerloPH/HomeCinema/releases/download/0.0.0.8/HomeCinema-Windows_v0.0.0.8.zip "HomeCinema-Windows_v0.0.0.8.zip")**

[![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/0.0.0.8/total.svg)]() <br>

**What's New?**
- ADD: **Clean** button to delete *".jpg"* files in *temp* folder.
- ADD: **Search IMDB** button to search for Movie IMDB ID, using Movie title.
- CHANGE: Asks to change Poster Image after clicking **Fetch Data**.
- FIX: JSON file missing.
- FIX: Changing of Poster Image with existing file not working.
- GUI Adjustments.
- Minor adjustments and Code cleanup.
****

## v0.0.0.7 ***build 7 - (21 May 2020 PHT)***
### Auto Fetch from TMDB

Build from latest commit: [ec36b4d](https://github.com/JerloPH/HomeCinema/commit/ec36b4d)

**[Download link for Windows 32/64 bit](https://github.com/JerloPH/HomeCinema/releases/download/0.0.0.7/HomeCinema-Windows_v0.0.0.7.zip "HomeCinema-Windows_v0.0.0.7.zip")**

[![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/0.0.0.7/total.svg)]() <br>

**What's New?**
- ADD: **Fetch Data** Button to automatically fetch information from [**The Movie Database**](https://www.themoviedb.org/).
  - Fetch: Title, Summary, Year, Trailer, Poster Image
- Minor GUI Adjustments
- Minor adjustments and code cleanup.
****

## v0.0.0.6 ***build 6 - (14 May 2020 PHT)***
### Quick Fix 

Build from latest commit: [6dde01a](https://github.com/JerloPH/HomeCinema/commit/6dde01a)

**[Download link for Windows 32/64 bit](https://github.com/JerloPH/HomeCinema/releases/download/0.0.0.6/HomeCinema-Windows_v0.0.0.6.zip "HomeCinema-Windows_v0.0.0.6.zip")**

[![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/0.0.0.6/total.svg)]() <br>

**What's New?**
- FIX: When movie is deleted, dispose cover image from resources and delete it from disk.
- FIX: Automatic change of *View* and *Sort*.
- Code cleanup.
****

## v0.0.0.5 ***build 5 - (13 May 2020 PHT)***
### **GUI Update and Minor Improvements** 

Build from latest commit: [99e2dd4](https://github.com/JerloPH/HomeCinema/commit/99e2dd4)

**[Download link for Windows 32/64 bit](https://github.com/JerloPH/HomeCinema/releases/download/0.0.0.5/HomeCinema-Windows_v0.0.0.5.zip "HomeCinema-Windows_v0.0.0.5.zip")**

[![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/0.0.0.5/total.svg)]() <br>

**What's New?**
- ADD: Checkbox to perform SEARCH after CLEAR of Search results.
- CHANGE: Only perform search on *Key Press Enter* when *Searchbox* is not empty.
- CHANGE: Set Movie Cover image as Form Icon
- FIX: Search results not clearing after Searching with no Results.
- Minor GUI Adjustments
- Minor adjustments and code cleanup.
****

## v0.0.0.4 ***build 4 - (07 May 2020 PHT)***
### **Auto Cover and Minor Update** 

Build from latest commit: [8343553](https://github.com/JerloPH/HomeCinema/commit/8343553)

**[Download link for Windows 32/64 bit](https://github.com/JerloPH/HomeCinema/releases/download/v0.0.0.4/HomeCinema-Windows_v0.0.0.4.zip "HomeCinema-Windows_v0.0.0.4.zip")**

[![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/v0.0.0.4/total.svg)]() <br>

**What's New?**
- Automatically add cover image from media thumbnail.
- FIX: Loading form infinite looping on first time load.
- Minor improvements and Code cleanup.
****

## v0.0.0.3 ***build 3 - (30 April 2020 PHT)***
### **GUI Update** 

Build from latest commit: [4b4217f](https://github.com/JerloPH/HomeCinema/commit/4b4217f)

**[Download link for Windows 32/64 bit](https://github.com/JerloPH/HomeCinema/releases/download/v0.0.0.3/HomeCinema-Windows_v0.0.0.3.zip "HomeCinema-Windows_v0.0.0.3.zip")**

[![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/v0.0.0.3/total.svg)]() <br>

**What's New?**
- Added new ["File"](https://github.com/JerloPH/HomeCinema/blob/v0.0.0.3/HomeCinema/Resources/media_ext.hc_data "media_ext.hc_data") that loads supported extensions for media files.
- Changed GUI.
- Changed "No Cover" Image file.
****

## v0.0.0.2 ***build 2 - (26 April 2020 PHT)***
### **Second Release - First Update** 

Build from latest commit: [a57d298](https://github.com/JerloPH/HomeCinema/commit/a57d298)

**[Download link for Windows 32/64 bit](https://github.com/JerloPH/HomeCinema/releases/download/v0.0.0.2/HomeCinema-Windows_v0.0.0.2.zip "HomeCinema-Windows_v0.0.0.2.zip")**

[![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/v0.0.0.2/total.svg)]() <br>

**What's New?**
- Allow to search in multiple directories (See ["Edit Media Location"](https://github.com/JerloPH/HomeCinema/blob/0.0.0.8/README.md#edit-medialocation))
- Added **"Show New"** button: Shows all newly added media files
- Automatically search when "ENTER" key is pressed on SearchBox
- Movie / TV Show item Container is bigger
- Add year to title of Movie / TV Show
- Cleaned the GUI
****

## v0.0.0.1 ***build 1 - (19 April 2020 PHT)***
### **First Public Release of HomeCinema** 

Build from latest commit: [c0b7b7d](https://github.com/JerloPH/HomeCinema/commit/c0b7b7d)

**[Download link for Windows 32/64 bit](https://github.com/JerloPH/HomeCinema/releases/download/v0.0.0.1/HomeCinema-Windows_v0.0.0.1.zip "HomeCinema-Windows_v0.0.0.1.zip")**

[![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/v0.0.0.1/total.svg)]() <br>
