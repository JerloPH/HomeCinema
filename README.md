# HomeCinema

<img src="/data/v0.1.jpg"></img>

**version:**	0.3.0.0 <br>
**build:**		28

## Downloads

**Windows 64 bit [Compressed ZIP]**: [Click to Download latest version](https://github.com/JerloPH/HomeCinema/releases/download/v0.3.0.0/HomeCinema-Windows.zip "Download, Extract and Open 'HomeCinema' Executable file") <br>

**NOTE: This update breaks the old way that the App saves directories** <br>
- After updating, open the App and find the folder where your movies are saved.
- To add additional folders, and TV series location(s), go to **Settings** -> **File**.
  - Click on **[Add]** and navigate to your directories.
  
**NOTE: This does NOT remove all your previous Movies in database.**
  
### Download Counts

**Current:** &nbsp; [![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/v0.3.0.0/total.svg)]() <br>
**Overall:** &nbsp; [![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/total.svg)]()

## Requirements

- 4GB or more RAM. <br>
- Microsoft .NET Framework Runtime, version 4.6 or higher. [Download Link](https://dotnet.microsoft.com/download/dotnet-framework/net46) <br>

****

## What is Home Cinema?

**Home Cinema** is a *"media cataloguing software"*.

Organize your collection of Movies and TV shows with HomeCinema.

It is like a book catalogue, except it's for Movies and Series located in your Local Drive.

Ease the pain of browsing through many folders and finding out which movie is which.

[View **Softpedia** review here](https://www.softpedia.com/get/Multimedia/Video/Other-VIDEO-Tools/HomeCinema.shtml) <br>
[View **Netfox2** review (French site)](https://www.netfox2.net/modules/wfdownloads/singlefile.php?cid=123&lid=2181) <br>
[View **Jetelecharge** review (French Site)](https://www.jetelecharge.com/Bureautique/10226.php) <br>
[View **slunecnice** review (Czech site)](https://www.slunecnice.cz/sw/homecinema/)
	
## List of features:
	
- Automatically scan and add all supported media files from designated folders.
- Automatically fetch movie information from [The Movie Database](https://www.themoviedb.org/). *Requires Internet Connection.*
- Directly open the media file, using your default player.
- Allows easy browsing of media files on your Computer / Local drive.
- Display a collection from your locally stored files.
- Allows filter and search.
- Edit and Save information within the app.

## How to Add Media Locations?

1. Press **CTRL + S** or click on **[Settings]** button to Open Settings UI.
2. Go to **File** Tab.
3. Under **Media Locations**, click on **[Add]** button and navigate to the directory where your movie files are located. <br>

**IMPORTANT NOTE:** First-time loading of App takes a while to finish. Even longer if you have a huge collection of media files. <br>

### NOTES for Adding TV Series
  - Located under **Series Locations**.
  - The directory **must be** the top-level *base* folder.
  - A sample directory structure:
```
D:\TV Series\Title of Series and Season\Episode 1.mp4
```
  - With this structure, ``D:\TV Series`` is your base folder.
  
<img src="/data/guide_add_media_paths.jpg"></img>

****

## Credits

### Resources
**Animated Loading GIF** is from [**Icons8.com**](https://icons8.com/preloaders/) <br>

### Third-Party API
[**The Movie Database**](https://www.themoviedb.org/) - Used to fetch movie details from the web. <br>

### NuGet Packages Used
[**Newtonsoft.Json**](https://www.newtonsoft.com/json) - Parse JSON file that contains Movie Information. <br>
[**SQLite**](https://www.nuget.org/packages/System.Data.SQLite.Core/) - Used to connect to a Local Database.<br>
[**SQLite Stub**](https://packages.nuget.org/packages/Stub.System.Data.SQLite.Core.NetFramework/) - Used to connect to a Local Database. <br>
[**Microsoft Windows API CodePack Core**](https://www.nuget.org/packages/Microsoft-WindowsAPICodePack-Core/) - For various functions. <br>
[**Microsoft Windows API CodePack Shell**](https://www.nuget.org/packages/Microsoft-WindowsAPICodePack-Shell/) - For various functions. <br>
[**Microsoft Universal Windows Platform**](https://www.nuget.org/packages/Microsoft.NETCore.UniversalWindowsPlatform/) - For various functions. <br>

****

## Find a bug or want a new feature?

**Submit a ticket at *Issues* tab**.
- *When submitting a **bug report**,*
  - Include the following file (Located at the same folder as the main app):
    - App.log
    - App_DB.log
    - App_ErrorLog.log
  - If possible, include a screenshot.
  - Explain the details in full. Describe what you are doing or trying to do, on what screen when the error apppeared.
    NOTE: Strictly **NO GENERIC** *"I get an error"* or *"This app has bugs"*.
  - Try to replicate the error / bug.
  - Failure to comply will make your report probably ignored.
- *When requesting a **new feature / feature update**,*
  - Include how it works.
  - Explain in full details.
  - State how useful the feature is.

****

## License

*** Copyright 2020 © JerloPH *** <br>
*This project is licensed under* **[GPL v3](https://www.gnu.org/licenses/gpl-3.0.html)** <br>
**[Click HERE to read LICENSE.md](/LICENSE.md)**

<details>
	<summary> <b>View Snippet</b> </summary>
	
    ** <b>HomeCinema - Organize your Movie Collection</b> **
    <b>Copyright (C) 2020  JerloPH (https://github.com/JerloPH)</b>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
</details>

## Disclaimer:

No ACTUAL media files are distributed with this software. <br>
This is ONLY a cataloguing system for your OWNED files. <br>
The end-user is responsible for any misuse of this software. <br>
No copyright infringement intended.
