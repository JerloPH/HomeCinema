# HomeCinema - For Devs [DO NOT EDIT]

## Project Started:

> March 04, 2020 PHT (Philippine Standard Time)

## Project History:

**[View Link to read project history](../VERSION_HISTORY.md)**
 
## Project specific files
  - **hc_data**	: Text files used by the app.

## Project References (Required Packages)
  - **[SQLite](https://www.nuget.org/packages/System.Data.SQLite.Core/)**
  - **[SQLite Stub](https://packages.nuget.org/packages/Stub.System.Data.SQLite.Core.NetFramework/)**
  - **[Microsoft Windows API CodePack Core](https://www.nuget.org/packages/Microsoft-WindowsAPICodePack-Core/)**
  - **[Microsoft Windows API CodePack Shell](https://www.nuget.org/packages/Microsoft-WindowsAPICodePack-Shell/)**
  - **[Microsoft Universal Windows Platform](https://www.nuget.org/packages/Microsoft.NETCore.UniversalWindowsPlatform/)**
  - **[Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/)**

## Commit / Pull Request Labels

- **Feature**
  > Adds a new feature to the app
- **Revision**
  > Major changes to the codes  and functions / methods
- **Fix**
  > Bugfixes
- **Optimization**
  > Minor improvements to codes
- **Resource**
  > Updates to UI / Form designs. Include changes to Resources used by app. Icon, Image, etc..
- **Docs**
  > Updates to Technical document files
  
## Versioning

**1**.**2**.**3**.**4** build **5678**

- **1**: Major *Revisions* and Changes to codes.
- **2**: Major *Features* and/or *Bugfixes*. *(> 10)*
- **3**: Multiple *Feature* and/or *Fix*, with at least 1 Major *Feature*/ Minor *Revision* addition/update. *(<= 10)*
- **4**: Minor *bugfix* and/or *feature* update. Includes *Optimizations, GUI, Resource* and *Documents* changes. *(<= 10)*
- **5678**: Increase by 1 for each release.

***Sample***: **Release 0.0.0.1 build 1**

## How to commit? (NOTE: Each branch must contain separate feature / fix for it to be cherry-picked)

1. Fork from **master**
2. Name your *commit* and *PR* by following these naming conventions:
  - **REV**		: Revisions to codes.
  - **FT**		: When adding a Feature
  - **MINOR**	: Minor changes and optimizations.
  - **FIX**		: Bugfixes
  - **RES**		: GUI and Resource changes.
  - **DOC**		: Documents changes.
3. Make a **Pull Request** with the name using the above labels.
  - Make the name short and simple.
  - Include a *description* of your code. (What it does).
  - Also include what files are affected.
  - If *FIX*, include the **ticket ID**.
  - Sample:
    - **FT: Adds new button.** (Commit and PR name)
	- *Adds button on frmMain that when clicked, browse for an icon and replace the old one* (PR description)
4. Send the **PR** back to the repository.
5. Wait for approval (Might take days or weeks, even months).

**\*Make sure that you have tested your code before you commit**