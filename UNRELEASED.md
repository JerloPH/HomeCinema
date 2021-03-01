# Commits not released

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