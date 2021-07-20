# Commits not released

# Fixes
- FIX: Covers not updating when fetching from TMDB, causing duplicate cover images.
- FIX: Sometimes, covers are not replaced by new cover.
- FIX: Cannot connect to TMDB API.

# Changes and Additions
- REV: Changed MediaLocation setting. Now, setting for Movie and TV Location paths are combined.
- REV: Changed how Media Locations are loaded/saved. Might break old media location settings.
- NEW: Setting to change Image cover size.
- MINOR: Center Setting form on Main Form upon Load.

# Refactors
- REV: Refactored new entry detection.
  - Use dictionary to include additional info.
  - Separate logic according to sources, for fetching media info.
- REV: Refactor adding media mechanism.
  - Add MediaLocations structure.
  - use List of Medias structures.
  - Add each item with corresponding mediatype and source
  - Moved MediaLocation loading to GetMediaFiles method
- REV: User 'series' string to match any series type of media.
- REV: Refactor SQLHelper InsertMovie method.
  - Set LastID only if first query is successful
  - Reset LastID to 0 when an success code is not larger than 0
- REV: Refactored TMDB Search form.
  - Allow multi search (movie and tv) on TMDB Search form.
  - added default values and check for null objects.
  - replace 'spaces' with '%20' on API search link endpoint
- REV: Refactor Settings saving and loading.
- REV: Removed serieslocation file.

# KNOWN BUGS on dev
- Folder names with special characters are sometimes not loaded when trying to open it with Windows Explorer.