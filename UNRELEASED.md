# Commits not released

- FIX: Double apostrophe on entry title. Closes [Issue # 11](https://github.com/JerloPH/HomeCinema/issues/11).
- MINOR: When year is empty, default to "0".
- GUI: Adjusted 'About' form.
- GUI: Reposition 'Original Title' on Movie Info editing form.

# Commits for Dev changes
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

# KNOWN BUGS on dev

