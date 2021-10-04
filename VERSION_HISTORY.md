# HomeCinema - Project History

## HomeCinema v0.8.3
### *release 43 - build 11 September 2021 PHT*
### What's New?
- Setting to skip entries not on *Media Locations* in setting.
- **[View File]** button: Browse to file on Movie Edit form.

### Fixes
- Timeout value not saving properly on settings.
- Some alerts doesn't fit the message.

### UI Changes
- Changed controls from TextBox to ListBox for:
  - **'Casts'**, **'Producer'**, **'Director'**, and **'Studio'**.
- Renamed **'Actor'** to **'Casts'**.
- Removed **'Animated Movie'** and **'Cartoon series'** from **'Category'**.
- Major UI Change on **'About'** form.
- Moved **[Check for Updates]** button to **'About'** form.
- Add **'Root folder'** on Movie Edit form.
- Add hint to Media Extension settings form.
- Supress some messages when **'Confirm Actions'** setting is toggled off.
- Use custom Message Box for various Input prompts.

### Other Changes
- Closes all other forms upon exit, freeing memory.
- Focus on Main form after clicking **Cancel** on Movie Edit form.
- Limit search results on TMDB if **'Search Limit'** is larger than 0.
- Add current Date to log filenames.
- Moved **'No TMDB Key'** prompt to before update checking.
- Center alert on screen, if no parent form.
- Don't replace settings on app close.
- Improvement on Internet connectivity checker.
- Properly delete ListView item, if Movie is deleted.
- Various other changes. See [**'VERSION_HISTORY_DETAILED.md'**](https://github.com/JerloPH/HomeCinema/blob/master/docs/VERSION_HISTORY_DETAILED.md#released-on-v083).
****

## HomeCinema v0.8.2
### *release 42 - build 21 August 2021 PHT*
### What's New?
- Keep track of progress on **'adding new entries'** and **'searching/loading collection'**.
- Add Episode count to Tooltip text.
- Add **ToolStrip Menu**, replacing most button controls.
- New Setting, **'Confirm Actions'**. Setting it to **'No'** supresses some alert prompts.
- New Setting to set timeout for various online functionalities.
- Added various icons to alert prompts.

### Fixes
- Anilist API not fetching English titles.
- Unsearchable media when results contains null json property.
- 'Sort by Year' error.
- Trailer not showing properly.

### Changes
- Confirm replacing cover image only if it has an existing one.
- Confirm replacing info only if year is empty.
- Set default source on Movie edit form, depending on loaded category.
- If only 1 year is entered on search, use only that year.
- When trying to open a series, load its folder instead of highlighting folder on Windows Explorer.
- Improved handling of rate-limiting.
- Improved error-handling and its alert prompts.
- Updated UI of Movie Info edit form.
- Settings form minor change.
- Various code refactors and cleanups.
****

## HomeCinema v0.8.1
### *release 41 - build 08 August 2021 PHT*
### What's New?
- Add **[No Info]** button, to show entries with no IMDB and Anilist Id.

### Fixes
- Youtube trailers not saved properly on automatic fetching of info.
- Anilist Id not showing on **Movie Info** form.

## Misc.
- Series-related UIs are hidden on **Movie Info** form, if entry is not series.
****

## HomeCinema v0.8
### *release 40 - build 07 August 2021 PHT*
### New Stuff!
- Add **Anilist** support to fetch Anime-related media information. Closes [Issue #10](https://github.com/JerloPH/HomeCinema/issues/10).
- Expandable and Collapsible **Search** panel and its filters. Closes [Issue #13](https://github.com/JerloPH/HomeCinema/issues/13).
- Load your own TMDB API key from **'data/config.json'** file, if its not empty.
- Add prompt on replacing current info with info fetched online, on **Movie Info edit** form.
- Fetch **Episode** and **Season** counts.
- Combined Media Location and Series Location paths. See the new setting on **Settings** form.
- Fetch media info automatically, **only** if it finds exactly 1 match. More information on [Issue #14](https://github.com/JerloPH/HomeCinema/issues/14).
- New setting to change size of entry cover image on Main form.

### Fixes
- Covers not updating when fetching from TMDB, causing duplicate cover images.
- Sometimes, covers are not replaced by new cover.
- Cannot connect to TMDB API.
- Some Inaccuracy on query search.
- Use English title on search results (from fetching info online), if it exists.
- Duplicate TMDB request on finding movie IMDB Id.
- Entry sub-items not resetting when updating its item.
- Sanitize query with whitespaces on searching.
- Search both Movie and TV Series on **Search** form.

### Misc.
- Added RestSharp NuGet package.
- Added MakarovDev.ExpandCollapsePanel NuGet package.
- Refactored database, and related codes:
  - Use `UId` column for cross-referencing entries.
  - Cleanup queries.
  - Moved all codes related to database to its own class **'SQLHelper'**.
- Refactored mechanism for Saving/Loading settings.
- Better Robocopy script on Post-Build event.
- Various GUI improvements and cleanups.
- Various code cleanups; Removed unused methods, functions, and variables.

*Full and detailed changes on* [**'VERSION_HISTORY_DETAILED.md'**](https://github.com/JerloPH/HomeCinema/blob/master/docs/VERSION_HISTORY_DETAILED.md#released-on-v08).
****

## HomeCinema v0.7.2
### *release 39 - build 10 July 2021 PHT*
### Fixes and Changes
- FIX: Double apostrophe on entry title. Closes [Issue #11](https://github.com/JerloPH/HomeCinema/issues/11).
- MINOR: When year is empty, default to "0".
- GUI: Adjusted 'About' form.
- GUI: Reposition 'Original Title' on Movie Info editing form.
- Various **"Under the Hood"** changes.
****

## HomeCinema v0.7.1
### *release 38 - build 12 June 2021 PHT*
### Fixes
- FIX: Wrong Tooltip for **[Settings]** button.
- FIX: Media Extensions not saved properly. Closes Issue [#8](https://github.com/JerloPH/HomeCinema/issues/8).

### Changes
- REV: Add new Setting to confirm whenever **[Search]** button is pressed, or similar actions that reloads the Items. Closes issue [#9](https://github.com/JerloPH/HomeCinema/issues/9)
- REV: Added multiple prompts to confirm actions.
- MINOR: Report what setting is not saved.
- MINOR: Close **'Settings'** form after saving.
- GUI: Reduced font size on **[Save]** and **[Cancel]** buttons on **'Settings'** form.
- GUI: Re-positioned controls on Movie Details form.

### Dev-related changes
- REV: Load Movie ID as datatype **'long'**.
- REV: Refactored syntax for various **'SQLHelper'** class methods.
- REV: Refactored **'btnSearch'** click event to be modular.
- REV: Removed code forcing GC to collect.
- REV: Renamed **'GlobalVars'** class namespace.
- MINOR: Changed **Mutex** scope to **readonly**.
- MINOR: Added documented comments for some **'GlobalVars'** class methods.
- MINOR: Moved Event methods to designer.
- MINOR: Returns empty list if string array is empty or null on multiple methods.
- MINOR: Moved **'Config'** class to subfolder **'JSONs'**.
- DOCS: Added multiple docs for **'reporting issue'** and **'submitting PRs'**.
- Various code cleanups and minor refactors.
****

## HomeCinema v0.7
### *release 37 - build 06 June 2021 PHT*
### Hotfixes on top of v0.6.1
- FIXED: HomeCinema database is not generated automatically, if not existing.
- FIXED: Showing **'File'** instead of **'Folder'** when viewing folder size of series on mouse hover of Movie Title.

### Major Changes
- REV: On **'Edit Information'**, shows nothing on picture box when image cover is null.
- REV: Revert focus to **Main Form** after closing **Settings** form.
- REV: Increased delay on fetching TMDB entries, to not overload TMDB server.
- REV: Added multiple prompts for different scenarios that needed alerts.

### Code-level changes
- REFACTOR: Updated queries to much simpler and efficient ones.
- REFACTOR: Convert scope of **'SQLHelper'** class to **static**.
- REV: Added **'GlobalEnum'** class to store Enums used anywhere.
- REV: Call **ShowDialog()** instead of **Show()** on **'frmAlert'**.
- REV: Created method: **'GlobalVas.WriteListBoxToFile()'** to write ListBox items to text file.
- REV: Added default constructor for **'frmAlert'** and **'frmLoading'**.
- REV: Refactored various methods to reflect changes on **'SQLHelper'** and **'GlobalEnum'** class.
- REV: Load information on entries with default values, if it cannot be loaded properly from database.
- REV: Added **'config'** table on database. Currently WIP as it will be used to compare db version to app version.
- Various minor refactors and code cleanups.
****

## HomeCinema v0.6.1
### *release 36 - build 30 May 2021 PHT*
### Hotfixes on top of v0.6
- FIXED: Unhandled Exception thrown when clicking **"Edit Information"** on empty result.
- FIXED: Update checking not working.
- FIXED: App not working for new users.

### Changes
- REV: Moved update checking to trigger after collection is loaded.
- REV: When result from searching IMDB is double-clicked, select it.
- MINOR: Close frmLoading when ESC key is pressed.
****

## HomeCinema v0.6
### ***release 35 - build 30 May 2021 PHT***

### Hotfix
- FIXED: TMDB Key not recognized during startup.

### What's New?
- REV: UI Controls are now scalable! It resizes and reposition based on the form size.
  - Added minimum form size to limit window resizing.
- REV: Used customizable alert prompts. (*Background and Foreground color can be set in Settings form*).
- REV: NEW Setting: **Auto Clean**. Default **'ON'**.
  - If **'ON'**, cleans logs and temporary files on Startup.
- MINOR: Prompt user when IMDB link cannot be opened in browser.
- GUI: Updated the Icon.
- GUI: Changed Loading Icon.
- GUI: Reposition controls on **General** tab on **Settings** form.
- GUI: Alphabetically sort entries on Listbox for **'Country'** and **'Genre'**.
- GUI: Apply UI customization on prompt when adding entry to **'Country'** and **'Genre'** listbox.

### Dev changes
- REV: Automatically refresh *Country* and *Genre* whenever **Edit Information** is clicked.
- REV: Refactored codes.
- REV: Prompt when there's no TMDB Key, also disable features that uses TMDB.
- MINOR: Removed overload for method **'GlobalVars.ShowWarning()'**.
- MINOR: Don't show error message on method **'GlobalVars.ReadStringFromFile()'**.
- MINOR: Moved all pre-load setup to **'Program.cs'**.
- MINOR: Use **'MessageBox.Show()'** method if Main Form is not loaded.
****

## HomeCinema v0.5.3
### ***release 34 - build 15 May 2021 PHT***

### What's New?
- NEW: Json setting to change Time out when connecting to the internet, *for various features*.
  - See **data\settings.json** file, under **"setTimeOut": [value]**.
- FIX: When cleaning, app fails to load subsequent entries.
****

## HomeCinema v0.5.2
### ***release 33 - build 15 April 2021 PHT***

### What's New?
- REV: Added [**Check for Update**] button to Manually check update in Settings -> General window.
- REV: Added new category in *Settings* window, **Appearance**. 
  - Contains settings to modify GUI elements, like window background color and font color.
- FIX: Entries stop loading after a file missing is reached.
- FIX: New series cannot be added.
- MINOR: Close *Settings* window after Saving changes.
- GUI: Minor change to some messages on forms.
- GUI: Added Icons for Loading form, indicating success or failure when checking for updates.

### Dev changes
- REFACTOR: Reduced code lines, and improved disposing of resources.
- MINOR: Moved condition for checking update.
- MINOR: Moved Saving Settings on [**OK**] button pressed, on Settings window.
- MINOR: Append to Skipped log, instead of rewriting.
- DOC: Removed download links for old releases.
****

## HomeCinema v0.5.1
### ***release 32 - build 21 March 2021 PHT***

### What's New?
- FIX: App doesn't get new media files.
- MINOR: Clean App folder and logs upon app load.
- MINOR: Instantly save setings file, after changing settings.

## HomeCinema v0.5
### ***release 31 - build 16 March 2021 PHT***

### What's New?
- REV: Reduced memory usage by half, by optimizing code structure.
- REV: Use ListBox for Genre and Country, instead of Checkboxes.
- REV: Only one instance of App can now run.
- REV: You can now search for Imdb entry, and select which one to use.
- REV: Automatically fetch movie info, after searching on Imdb.
- FIX: Cover image sometimes not downloading properly.
- FIX: Correctly display Season/Episode count on Items.
- MINOR: Moved logs to subfolder **'logs'**.
- MINOR: Movie Country is now based on Production country.
- MINOR: Country and Genre file is automatically updated when new entry is added, save list in Alphabetical order.
- MINOR: Automatically fetch 'Studio' from Imdb.
- MINOR: Clean all logs on button press, instead of only specific files.
- GUI: Additional message on app loading for inserting new entry to database.
- GUI: Updated *Settings* form to reflect new changes.
- GUI: Changed **'Episode name'** to **'Original'** on Movie Info display.
- GUI: Changed color of Imdb Id on Movie Info display.
### Dev changes
- Target x64 on Build.
- Use **Path.Combine** to create filepaths, making it the standard.
- Create empty log files, when it doesn't exist.
- Log to ErrorLog at the same folder, when there's no log existing in logs subfolder.
- Make building array from text file case-insensitive.
- Added codes for easy debugging.
- Major code refactors.

## HomeCinema v0.4.1
### ***release 30 - build 02 March 2021 PHT***

### What's New?
- REV: Delete Covers not in Database, when pressing **[Clean]**.
- FIX: Changing metadata should not work on **'series'**.
- FIX: Removed producer to stop looping on *saving metadata*.
- GUI: Changed Font for **Movie Summary**, and **Settings** form.
- GUI: Updated main form caption.
### Dev changes
- REV: Show *loading form* when checking for update.
- MINOR: Include additional message for loading forms.
- DOCS: Updated Markdown docs **README, LICENSE, README_TODO**.
- DOC: Added **docs/VERSION_HISTORY_DETAILED.md** and **docs/links.md**.
- Code refactors, and improvements.
****

## v0.4.0.0 ***build 29 - (23 February 2021 PHT)***
### Major Fixes and Improvements

### What's New?
- REV: Add loading form to **[Clean]** button.
- REV: Search for Trailers in all media.
- FIX: **'Series Directories'** not showing up on ListView.
- FIX: Cannot perform **[Clean]** when cover is changed recently.
- GUI: Changed Font of Items.

### 'Under the Hood' changes
- REV: Refactor query for Searching.
- REV: Refactor ListView Item details.
- REV: Refactor **frmMovie** Initialization.
- REV: Increase TimeOut for **CheckConnection()**.
- REV: Cast all API queries of TMDB to JSON files.
- Code refactors and cleanup.
****

## v0.3.0.0 ***build 28 - (22 February 2021 PHT)***
### New Loading Form, and Improvements

### What's New?
- REV: New Loading form.
- REV: Show Loading form on Fetching data from TMDB, and Saving Metadata.
- FIX: Playing media file for invalid movie ID.
- FIX: Cannot view Movie Info for Invalid ID.

### 'Under the Hood' changes
- REV: Separate string array builder for directories.
- REV: Delegate background tasks to loading form.
- MINOR: Added pre-build event to delete **'lib'** subfolder.
- MINOR: Append new entries to **'MovieResult_DoneInsert.Log'**, instead of replacing file every form load.
- MINOR: Organized source files.
- Code cleanups, and various refactors.
- Removed un-used lines of code.
****

## v0.2.0.0 ***build 27 - (30 January 2021 PHT)***
### Breaking Changes!

**NOTE: This update breaks the old way that the App saves directories** <br>
- After updating, open the App and find the folder where your movies are saved.
- To add additional folders, and TV series location(s), go to **Settings** -> **File**.
  - Click on **[Add]** and navigate to your directories.

**What's New?**
- REV: New GUI for Settings: Added Buttons to **[Add], [Remove], [Clear]** Directories to search media files from.

**'Under the Hood' changes:**
- REV: All required DLLs and files are saved under *lib* subfolder.
- MINOR: Renamed variables for readability.
- MINOR: Code cleanups.
- DOCS: Updated README.
- RES: Updated screenshots.
****

## v0.1.0.0 ***build 26 - (12 January 2021 PHT)***
### Major Improvements.

**What's New?**
- REV: Automatically refresh Media List, when checkbox **Hide Animation** is checked/unchecked.
- REV: Perform **Update checking** when App is completely loaded, instead of *'before App Loads'*.
- GUI: Renamed **'Filters'** to **'Tags'**, in Settings.
- GUI: Added **[About]** button, to Display Info about the App.

**'Under the Hood' changes:**
- REV: Invoke method, when adding items to ListView Media List.
- RES: Update guide_add_media_paths.png.
- DOC: Removed links from previous releases (deleted).
- DOC: Added **'BUILD_GUIDE.md'**.
- MINOR: Changed logging string format.
- MINOR: Add return type and value for method:  **TryDelete**, in [**GlobalVars**](/HomeCinema/GlobalVars.cs).
- MINOR: Added logging files to **[Clean]** Button.
- MINOR: Add some more logs.
- MINOR: Code cleanup.
****

## v0.0.9.0 ***build 25 - (28 December 2020 PHT)***
### Various Fixes and Improvements.

**What's New?**
- FT: Show **'File'** or **'Folder'** **size**, on **'Movie Title'**, in **'Movie Details'** form.
- FIX: If Media type is 'Series', open Folder location in Explorer.
- GUI: Changed **[Settings]** Form's Tabbed Navigator.
- GUI: Increased Font size for **'Movie Summary'**.

**'Under the Hood' changes:**
- REV: Updated NuGets packages. (UWP and SQLite).
- RES: Removed previous screenshots.
- DOC: Included commit links, on VERSION_HISTORY.
- DOC: Added Download counts on VERSION_HISTORY.
****

## v0.0.8.0 ***build 24 - (22 September 2020 PHT)***
### [Series] can now be Added!

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

**What's New?**
- FIX: Properly save the *genres* to file.
- MINOR: Alpabetically arrange country list properly.
- GUI: Settings form minor change.
- Optimizations and code cleanups.
****

## v0.0.6.2 ***build 21 - (05 September 2020 PHT)***
### Additional Features and Fixes

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

**What's New?**
- FIX: Cast Textbox not reset when CLEAR button is pressed.
- FIX: Another Attempt to fix cover not updating when fetched from TMDB.
- Minor Code cleanups.
****

## v0.0.6.0 ***build 19 - (29 August 2020 PHT)***
### Major Update

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

**What's New?**
- FIX: Hangs on Fetching data when there is no Internet Connection.
- FIX: Try to fix Image Cover sometimes not loading.
****

## v0.0.5.0 ***build 17 - (14 August 2020 PHT)***
### Automatic Data Fetching

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

**What's New?**
- REV: Improve loading of Movie, by using ProgressChanged on BGworker.
- FT: Added ToolTip text to ListView Item. Shows Movie Summary and Genre.
- FT: Added Right-click ContextMenu to ListView Item.
  - *[View Details]* - Shows Movie Information / Details form.
- Minor improvements and optimizations, and code cleanup.
****

## v0.0.2.2 ***build 13 - (30 June 2020 PHT)***
### Minor Update Fix

**What's New?**
- FIX: Auto-check-update feature.
- CHANGE: Auto-create settings File with default values, if not existing.
****

## v0.0.2.1 ***build 12 - (07 June 2020 PHT)***
### Various GUI and Settings adjustments

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

**What's New?**
- ADD: *[Settings File](https://github.com/JerloPH/HomeCinema/blob/v0.0.1.1/HomeCinema/Resources/settings.json)* to save persistent settings in App.
- CHANGE: Open Movie when *ENTER* Key is pressed. 
- Minor improvements and code adjustments.
****

## v0.0.1.0 ***build 9 - (23 May 2020 PHT)***
### Improvements to MovieInfo

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

**What's New?**
- ADD: **Fetch Data** Button to automatically fetch information from [**The Movie Database**](https://www.themoviedb.org/).
  - Fetch: Title, Summary, Year, Trailer, Poster Image
- Minor GUI Adjustments
- Minor adjustments and code cleanup.
****

## v0.0.0.6 ***build 6 - (14 May 2020 PHT)***
### Quick Fix 

**What's New?**
- FIX: When movie is deleted, dispose cover image from resources and delete it from disk.
- FIX: Automatic change of *View* and *Sort*.
- Code cleanup.
****

## v0.0.0.5 ***build 5 - (13 May 2020 PHT)***
### **GUI Update and Minor Improvements** 

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

**What's New?**
- Automatically add cover image from media thumbnail.
- FIX: Loading form infinite looping on first time load.
- Minor improvements and Code cleanup.
****

## v0.0.0.3 ***build 3 - (30 April 2020 PHT)***
### **GUI Update** 

**What's New?**
- Added new "File" that loads supported extensions for media files.
- Changed GUI.
- Changed "No Cover" Image file.
****

## v0.0.0.2 ***build 2 - (26 April 2020 PHT)***
### **Second Release - First Update** 

**What's New?**
- Allow to search in multiple directories.
- Added **"Show New"** button: Shows all newly added media files
- Automatically search when "ENTER" key is pressed on SearchBox
- Movie / TV Show item Container is bigger
- Add year to title of Movie / TV Show
- Cleaned the GUI
****

## v0.0.0.1 ***build 1 - (19 April 2020 PHT)***
### **First Public Release of HomeCinema** 
