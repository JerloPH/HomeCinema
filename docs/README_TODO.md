# HomeCinema - Features to Add / Bugs to Fix

## Priority List
- Move all show alert methods to new static class: "Alert".
- On "About" form, include tabs: { APP VERSION and INFO (links, etc.), Check for Update button, VERSION_HISTORY, LICENSE, CREDITS }
- Make "About" form similar to "Settings" form when browsing tabs.
- Remove check for update button on frmSetting.
- Add new Menu selection "File" to ToolStrip Menu (before "Setting"). With elements: { "About", "Exit" }
- Remove "About" on ToolStrip menu.
- Convert { Producer, Director, Artist, Studio } from Textbox to ListBox, delimiter: ";".

- Loading form before Main form is shown (On Program.cs, preferably)
- Refactor database.
  - Save deleted Uid to 'uid_free' table.
- Portability option
  - Add toggle in setting
  - save entries to '_portable' tables
  - Saving filepath removes the root folder
  - Add root folder to 'filepath' entry, for portability support
  - Portable entrues root folder is saved within the App root (divided into 4 subfolders: Movie,Series,AnimeMovie,AnimeSeries)
- Marked 'Watched' entry.
- Chromecast support.
- Copy movie/series file to directory.
- Load label texts from external files, when using translation.
- Faster showing of movie info when clicking **"View details"**.
- Improve speed of App load (check background worker and optimize).
- Check if file still exists, before loading it into the App.
  - Delete entry from database, and cover picture (if existing). *For now, skips the entry.*
  - Remove ListView item, when the file is not existing. Then, delete the entry from database. *Or archive to another database*.
- Create new TextBox Setting to set TimeOut for connections.

## Features:
- General
  - [ ] Update GUI
  - [ ] Menu Strip (Preferences Button)
    - [ ] File
	  - [ ] Version Log
	  - [ ] Exit
	- [ ] Settings
	  - [ ] Change Color of ListView. Back/Fore.
	- [ ] Help
	  - [ ] How to Use?
	  - [ ] Credits
	  - [ ] About
  - [ ] Check all Files in DB if Existed. If not, "move to archive db".
  - [ ] Open Media Player in 2nd monitor, if existing. **On hold**.

## Main Form
  - [x] Automatically get information from IMDB when newly added media (IMDB Scraper). **Ongoing**.
    - Done:
      - IMDB Id
      - Year, Title
      - Summary
      - Genre
      - Trailer YT Link
      - Artist, Director, Producer
      - Country
      - Cover Image.
      - Studio.
	  
## Movie Information Form
  - [ ] Save Metadata to movie file. *Only for movies*. Save info showing frmLoad.
    - [x] Title
	  - [x] Year
  	- [x] Genre
	  - [x] Director

## Bugs

## Others

### Github Profile stats
<img src="https://github-readme-stats.vercel.app/api?username=JerloPH&&show_icons=true">
