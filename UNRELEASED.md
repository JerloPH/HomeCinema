# Commits not released

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

# Commits for Dev changes

- REV: Automatically refresh *Country* and *Genre* whenever **Edit Information** is clicked. [0de00d4]
- REV: Refactored codes. [420c42d], [9393b92], [405c857], [66612b9], [4969640], [9b4181b], [d36df56], [918f4bf]
- REV: Prompt when there's no TMDB Key, also disable features that uses TMDB. [84360b2], [e14ed97], [4b6e861]
- MINOR: Removed overload for method **'GlobalVars.ShowWarning()'**. [a38108b]
- MINOR: Don't show error message on method **'GlobalVars.ReadStringFromFile()'**. [9f5e362]
- MINOR: Moved all pre-load setup to **'Program.cs'**. [7965d4b]
- MINOR: Use **'MessageBox.Show()'** method if Main Form is not loaded. [b207f68]

# KNOWN BUGS on dev

