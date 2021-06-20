# HomeCinema - Media Organizer

<img src="/data/v0.7.jpg"></img>

**version:**	0.7.1.0 <br>
**release:**	38

| <a href="docs/REPORT_ISSUE.md">Report Issue/Bug/Error/Feature request</a> | <a href="(docs/CONTRIBUTING.md">Submit Pull Request</a>  | <a href="VERSION_HISTORY.md">See 'App Version History'</a> |
| -- | -- | -- |

## Downloads

**Windows 10, x64 bit [Compressed ZIP]**: [Click to Download latest version](https://github.com/JerloPH/HomeCinema/releases/download/v0.7.1.0/HomeCinema-Windows.zip "Download, Extract and Open 'HomeCinema' Executable file") <br>
**Note: Untested on Windows 7/8, but it might work**

### Download Counts
[![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/total.svg)]() &nbsp;
[![](https://img.shields.io/github/downloads/JerloPH/HomeCinema/latest/HomeCinema-Windows.zip)]()

## Requirements
- 4GB or more RAM. <br>
- Microsoft .NET Framework Runtime, version 4.6 or higher. [Download Link](https://dotnet.microsoft.com/download/dotnet-framework/net46) <br>

## What is HomeCinema?
**HomeCinema** is a *"media cataloguing software"*. <br>
Organize your collection of Movies and TV shows with HomeCinema. <br>
It is like a book catalogue, except it's for Movies and Series located in your Local Drive. <br>
Ease the pain of browsing through many folders and finding out which movie is which. <br>

View reviews and posts from [**GHacks.net**](https://www.ghacks.net/2021/06/06/homecinema-media-cataloging-software-for-windows/) • [**MajorGeek**](https://www.majorgeeks.com/files/details/homecinema.html) • [**Softpedia**](https://www.softpedia.com/get/Multimedia/Video/Other-VIDEO-Tools/HomeCinema.shtml) • [**NSaneForum**](https://nsaneforums.com/topic/411276-homecinema-0530/?tab=comments#comment-1687727) • [**Netfox2** (French)](https://www.netfox2.net/modules/wfdownloads/singlefile.php?cid=123&lid=2181) • [**Jetelecharge** (v0.1)](https://www.jetelecharge.com/Bureautique/10226.php) • [**slunecnice** (Czech)](https://www.slunecnice.cz/sw/homecinema/) • [**Windows Forum** (v0.5)](https://windowsforum.kr/data/15652587). <br>
**Note: I'm not the user who posted any of them. Visit at your discretion.** <br>

## Features:
- Automatically scan and add all supported media files from designated folders.
- Automatically fetch movie information from [The Movie Database](https://www.themoviedb.org/). *Requires Internet Connection.*
- Directly open the media file, using your default player.
- Allows easy browsing of media files on your Computer / Local drive.
- Display a collection from your locally stored files.
- Allows filter and search.
- Edit and Save information within the app.

## Adding Media Locations
1. Press **CTRL + S** or click on **[Settings]** button to Open Settings UI.
2. Go to **File** Tab.
3. Under **Media Locations**, click on **[Add]** button and navigate to the directory where your movie files are located. <br>

**IMPORTANT NOTE:** First-time loading of App takes a while to finish. Even longer if you have a huge collection of media files.

### NOTES for Adding TV Series
  - Located under **Series Locations**.
  - The directory **must be** the top-level *base* folder.
  - A sample directory structure:
```
D:\TV Series\Title of Series and Season\Episode 1.mp4
```
  - With this structure, ``D:\TV Series`` is your base folder.
  
<img src="/data/guide_add_mediaseries_paths.jpg"></img>

****

## Credits

### Resources
**Animated Loading GIF** is from [**Icons8.com**](https://icons8.com/preloaders/) <br>
**Checkmark Icon** is from [*FreeIcons.io*](https://freeicons.io/regular-life-icons/sign-check-icon-17888), by [**Anu Rocks**](https://freeicons.io/profile/730) <br>
**Warning Icon** is from [*FreeIcons.io*](https://freeicons.io/regular-life-icons/sign-warning-icon-17898), by [**Anu Rocks**](https://freeicons.io/profile/730) <br>

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

## License

**Copyright 2020-2021 © JerloPH** <br>
*This project is licensed under* **[GPL v3](https://www.gnu.org/licenses/gpl-3.0.html)** <br>
**[Click HERE to read full LICENSE.md](/LICENSE.md)**

<details>
	<summary> <b>View Snippet</b> </summary>
	
    • HomeCinema - Organize your Movie Collection •
    • Copyright (C) 2021  JerloPH (https://github.com/JerloPH) •

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
