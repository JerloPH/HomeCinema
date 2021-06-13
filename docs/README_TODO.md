# HomeCinema - Features to Add / Bugs to Fix

## Priority List
- Store **'app and db'** version on **'config'** table.
- Marked 'Watched' entry.
- Chromecast support.
- Copy movie/series file to directory.
- Load label texts from external files, when using translation.
- Faster showing of movie info when clicking **"View details"**.
- Scrape Anime info from Anilist/MyAnimeList.
- Improve speed of App load (check background worker and optimize).
- Check if file still exists, before loading it into the App.
  - Delete entry from database, and cover picture (if existing). *For now, skips the entry.*
  - Remove ListView item, when the file is not existing. Then, delete the entry from database. *Or archive to another database*.
- Use **Series Name/Title** only **if** category is **Series**, and use as **Original Title/Name**.
- Create new TextBox to set TimeOut for connections.

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
