# Version History Detailed

## Released on v0.7.2
### What's New?
- FIX: Double apostrophe on entry title. Closes [Issue #11](https://github.com/JerloPH/HomeCinema/issues/11).
- MINOR: When year is empty, default to "0".
- GUI: Adjusted 'About' form.
- GUI: Reposition 'Original Title' on Movie Info editing form.

### Dev changes:
- REV: Renamed some column on database.
- REV: Added method to automatically upgrade database to match required by app.
- REV: Added new table: **'config'** that saves information about app.
- REV: Refactored most SQL Helper methods.
- REV: Refactored enums namespace.
- REV: Added method to show messagebox without parent form.
- REV: Moved database loading to Main form load event.
- MINOR: Moved some event call to designer class.
- MINOR: Updated NuGet packages: **SQLite, Newtonsoft.JSON, and Microsoft UWP**.
- Various code cleanup.

# Released on v0.6
### What's New?
- REV: Used customizable alert prompts. (*Background and Foreground color can be set in Settings form*). [ca194ce], [4e2c4a7], [73549ca], [8cfe1f0]
- REV: UI Controls are now scalable! It resizes and reposition based on the form size. [9a1f5ec], [e748c59], [fb849dc]
- REV: Added minimum form size to limit window resizing. [5487bde]
- REV: NEW Setting: **Auto Clean**. Default **'ON'**. [f893ff1], [605c09a]
  - If **'ON'**, cleans logs and temporary files on Startup.
- MINOR: Prompt user when IMDB link cannot be opened in browser. [45a581e]
- GUI: Updated the Icon. [8caeb60]
- GUI: Changed Loading Icon. [c17d4fd]
- GUI: Reposition controls on **General** tab on **Settings** form. [79d23da]
- GUI: Alphabetically sort entries on Listbox for **'Country'** and **'Genre'**. [8fa6a49]
- GUI: Apply UI customization on prompt when adding entry to **'Country'** and **'Genre'** listbox. [0c8f37c]

### Dev changes
- REV: Automatically refresh *Country* and *Genre* whenever **Edit Information** is clicked. [0de00d4]
- REV: Refactored codes. [420c42d], [9393b92], [405c857], [66612b9], [4969640], [9b4181b], [d36df56], [918f4bf]
- REV: Prompt when there's no TMDB Key, also disable features that uses TMDB. [84360b2], [e14ed97], [4b6e861]
- MINOR: Removed overload for method **'GlobalVars.ShowWarning()'**. [a38108b]
- MINOR: Don't show error message on method **'GlobalVars.ReadStringFromFile()'**. [9f5e362]
- MINOR: Moved all pre-load setup to **'Program.cs'**. [7965d4b]
- MINOR: Use **'MessageBox.Show()'** method if Main Form is not loaded. [b207f68]

# Released on v0.4.1
**See** [**PR # 5**](https://github.com/JerloPH/HomeCinema/pull/5) **to see all commit ids**. <br>

### What's New?
- REV: Add method to delete Covers not in Database, **GlobalVars.CleanCoversNotInDb**. [3b4cbca], [a93bf4b]
- FIX: Changing metadata should not work on **'series'**. [10f2b8f]
- FIX: Removed producer to stop looping on *saving metadata*. [417b62a]
- GUI: Changed Font for Movie Summary. [971d9b6]
- GUI: Changed Font for **frmSetting**. [374a76b]
- DOC: Update **README.md**.
- DOC: Update **LICENSE.md**.
- DOC: Update **README_TODO.md**.

### Dev changes
- REV: Show *loading form* when checking for update. [d1bf2e6]
- MINOR: Include additional message for loading form. [6b824c9]

### Refactors
- REV: Refactor Image deletion from ImageList with thread-safety, delegated. [cd2d0e6], [61a12f6]
- REV: Refactor, directly save image instead of saving to variable.
- REV: Refactor media deletion, trigger **frmMovie** form refresh after **frmMovieInfo** closes. [0e7bc4c]
- MINOR: Convert **If statement** to ternary statement. [4c11264]
- MINOR: Removed unused code, removed **GlobalVars.UnParseJSON**. [7cb1b69]
- MINOR: Removed unused **GlobalVars** functions, **LogLine**, **ShowInfo**, **BuildStringArrayFromCB**. [cd7ed09]
- MINOR: Renamed **GlobalVars.DirSearch** to **GlobalVars.SearchFilesSingleDir**, **GlobalVars.SearchFilesMultipleDir**. [d4a2d04]
- MINOR: Removed linebreaks. [6486e7c]
- MINOR: Refactor logging. [c8c0d3a]

# Released on v0.4
**See** [**PR # 3**](https://github.com/JerloPH/HomeCinema/pull/3) **to see all commit ids**. <br>

### What's New?
- REV: Add loading form to **[Clean]** button.
- REV: Search for Trailers in all media. **See** [**PR # 4**](https://github.com/JerloPH/HomeCinema/pull/4)
- FIX: **'Series Directories'** not showing up on ListView. **See** [**PR # 4**](https://github.com/JerloPH/HomeCinema/pull/4)
- FIX: Cannot perform **[Clean]** when cover is changed recently. **See** [**PR # 4**](https://github.com/JerloPH/HomeCinema/pull/4)
- GUI: Changed Font of Items.

### 'Under the Hood' changes
- REV: Refactor query for Searching.
- REV: Refactor ListView Item details.
- REV: Refactor **frmMovie** Initialization.
- REV: Increase TimeOut for **CheckConnection()**.
- REV: Cast all API queries of TMDB to JSON files. **See** [**PR # 4**](https://github.com/JerloPH/HomeCinema/pull/4)
- Code refactors and cleanup.

# Released on v0.3
### What's New?
- REV: Remove entries that doesn't exist on the disk, during startup. [fca3696]
- REV: New Loading form. [PR # 2](https://github.com/JerloPH/HomeCinema/pull/2)
- REV: Show Loading form on Fetching data from TMDB. [251fbcd], [d9ca20d]
- REV: Show Loading form on Saving Metadata. [7fdcd7f]
- FIX: Playing media file for invalid movie ID. [1e58380]
- FIX: Cannot view Movie Info for Invalid ID. [81a3526]

### 'Under the Hood' changes
- REV: Separate string array builder for directories. [5c0f5b5]
- REV: Delegate background tasks to loading form. [6272ab6], [3e5678c], [bddf5af]
- MINOR: Added pre-build event to delete **'lib'** subfolder. [930da0c]
- MINOR: Append new entries to **'MovieResult_DoneInsert.Log'**. [6bad2e8]
- MINOR: Organized source files. [39cf7cc], [4d3acdb], [b88d9b2]

### Code Refactors and Improvements
- MINOR: Repositioned variable declarations. [6e0b928]
- MINOR: Removed **bgwMovie_ProgressChanged**. [6082db0]
- MINOR: Include **Regions**. [4733ad6]
- MINOR: Add **regions** and cleanups. [2b685c6]
- MINOR: Re-positioned **bgw_SearchFileinFolder**. [04ab71a]
- MINOR: Code refactor to Run background worker. [682f55c]
- MINOR: Removed **SEARCH_COLS** from some codes. [7e4e12c]
- MINOR: Refactor. Log if ResultSet is empty. [aa6efdd]
- MINOR: Refactor setting saving. [d319d5f]
- MINOR: Change **IF** statement to one line. [f86298a]
- MINOR: **IF statement** to one line. [e702638]
- MINOR: Removed un-used variable. [a413e3f], [c6a0464]
- MINOR: Removed un-used resource. [b0da3a6]