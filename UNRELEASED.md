# Changes not released

## What's New?
- Keep track of progress on **'adding new entries'** and **'searching/loading collection'**.
- Add Episode count to Tooltip text.
- Add **ToolStrip Menu**, replacing most button controls.
- New Setting, **'Confirm Actions'** to confirm some actions.
- New Setting to set timeout for various online functionalities.
- Added various actions to alert prompts.

# Fixes
- Anilist API not fetching English titles.
- Unsearchable media when results contains null json property.
- 'Sort by Year' error.
- Trailer not showing properly.

# Changes
- Confirm replacing cover image only if it has an existing one.
- Confirm replacing info only if year is empty.
- Set default source on Movie edit form, depending on loaded category.
- If only 1 year is entered on search, use only that year.
- When trying to open a series, load its folder instead of highlighting folder on Windows Explorer.
- Improved handling of rate-limiting.
- Improved error-handling and its alert prompts.
- Updated UI of Movie Info edit form.
- Settings form minor change.

# Dev changes
- Delete unreferenced method: **'SQLHelper.InitializeDT'**.
- Change how Movie Info is updated
  - Use MediaInfo class as Entity.
  - MOVIE_ID changed from 'String' to 'long' data type.
  - Removed obsolete methods.
- Convert sources strings to static class for convenience
- Move some methods to GlobalVar.
- Refactor ChildForm property of frmMovie.
- Removed MOVIE_NAME as param for frmMovie and frmMovieInfo.
- Changed properties of Settings, removing duplicate private properties.
- Add new entries to HCIcons enum.
- New property on frmLoading to set Progress text.
- Change Icon upon bgwork done, and prompt alert.
- Change scope of an object in Movie form.
- Moved loggers to its own static class.
- Dispose image from frmMovie properly.
- Removed unused codes
- Various code refactor, and cleanups.

# KNOWN BUGS on Stable
- Folder names with special characters are sometimes not loaded when trying to open it with Windows Explorer.

# KNOWN BUGS on Dev