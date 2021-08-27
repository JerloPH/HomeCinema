# Changes not released
- NEW: Setting to skip entries not on Media Location
- MINOR: Break search on TMDB if SearchLimit is larger than 1
- REV: Parametized some query on SQLHelper
- REV: Closes all other forms upon exit
- REV: Moved opening movie info form to GlobalVars
- REV: Properly delete listview item, if movie is deleted
- REV: Refactors
  - Add rootFolder to filepath table
  - Add date to log filenames
  - Try..catch in appending to file
  - Removed unused variable
  - Code cleanup
- REV: Use custom InputBox for single value
- REV: Use custom form for Inputbox
- REV: Add control buttons for Casts and Crews
- Renamed 'Actor' to 'Casts'
- REV: Convert 'Casts, Producer, Director, Studio' to List
- Dispose media Entity after single-time use
- REV: Refactor fields with multiple values
  - Producer, Director, Studio, Actor/Artist
- REV: Refactor loading of info, with separators
  - Use sep ; for 'Producer,Studio,Artist,Director'
- Refactor queue of prompts upon load
  - Moved No TMDB Key prompt to before update check
- REV: Refactor file-checking upon first load
- Refactor to 'Hide Animation' flag
- REV: Search by category refactor
- REV: Refactor code on category
- Fixed frmloading form design bug caused by recent change on Icons
- REV: Removed last 2 categories, updating all prev entries
  - Removed 'Animated Movie' and 'Cartoon series' category.
- MINOR: Center alert on screen, if no parent form
- REV: Changed Mutex
- REV: Better timeout setting checker
- REV: Set Static Icon to forms
- REV: Moved all Data Files paths to its own class


# KNOWN BUGS on Stable
- Folder names with special characters are sometimes not loaded when trying to open it with Windows Explorer.

# KNOWN BUGS on Dev