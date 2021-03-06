# HomeCinema - Features to Add / Bugs to Fix

## Priority List
- Scrape Anime info from Anilist/MyAnimeList.
- Improve speed of App load (check background worker and optimize).
- Check if file still exists, before loading it into the App. *For now, skips the entry.*
  - Delete entry from database, and cover picture (if existing).
  - Remove ListView item, when the file is not existing. Then, delete the entry from database. *Or archive to another ddatabase*.
- Use **language spoken** from TMDB to get Country, instead of Producing country.
- Replace **Episode Title** as **Original Title/Name**, for Movies.
- Use **Series Name/Title** only **if** category is **Series**, and use as **Original Title/Name**.
- Make **frmMain** static in **Program.cs**. Update any reference to **frmMain**.

## Features:
- General
  - [ ] Update GUI
  - [x] Add folders as series into the App. *Done*.
  - [x] Ctrl + S to OPEN Settings form
  - [ ] Menu Strip (Preferences Button)
    - [ ] File
	  - [ ] Version Log
	  - [ ] Exit
	- [ ] Settings
	  - [x] Auto-check update option
	  - [x] Play Movie, instead of Viewing information option
	  - [x] Log File Size limit
	  - [x] Edit MediaLocation, Country, Genre, Media Extensions
	  - [ ] Change Color of ListView. Back/Fore.
	- [ ] Help
	  - [ ] How to Use?
	  - [x] License
	  - [ ] Credits
	  - [ ] About
  - [ ] Auto-download update
  - [ ] Check all Files in DB if Existed. If not, "move to another db / keep".
  - [ ] Open Media Player in 2nd monitor, if existing. **On hold**.

## Main Form
  - [ ] Add Button to Group Items (Group by: Series Name, if Series) *(Check if season or episode is not empty, and get only episode 1)* **On-HOLD**
  - [x] Automatically get information from IMDB when newly added media (IMDB Scraper). **Ongoing**.
    - Done:
	  - IMDB Id
	  - Year, Title
	  - Summary
	  - Genre
	  - Trailer YT Link
	  - Artist, Director, Producer
	  - Country
	  - Cover Image. *Not working properly as of v0.4*
	  
## Movie Information Form
  - [ ] Scrape all info from *TMDB API*.
    - [x] Cover Image
    - [x] IMDB, Year, Name, Summary, Genre, Trailer YT Link
	- [x] Country
	- [ ] Category (Movie, Anime, Series,etc). *Partially done*.
	- [ ] Studio
	- [x] Producer
	- [x] Director
	- [x] Artist
  - [ ] Save Metadata to movie file. *Only for movies*.
    - [x] Title
	- [x] Year
	- [x] Genre
	- [x] Director
  - [ ] ~~Switch webBrowser to Cefsharp Browser.~~ **Cancelled**
  
## Settings form
- [ ] Create new TextBox to set TimeOut for connections.

## Bugs

## Others

### Github Stats on README.md

<img src="https://github-readme-stats.vercel.app/api?username=JerloPH&&show_icons=true">
