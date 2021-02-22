# Guide

## Building the Executable
1. Update Variable **"HOMECINEMA_BUILD"** in [/HomeCinema/GlobalVars.cs](../HomeCinema/GlobalVars.cs).
2. Insert TMDB Key Variable: **"TMDB_KEY"** in [/HomeCinema/GlobalVars.cs](../HomeCinema/GlobalVars.cs).
3. Update **version** in [/HomeCinema/Properties/AssemblyInfo.cs](../HomeCinema/Properties/AssemblyInfo.cs).
4. Clean and Build.

## Updating Github DOCs
1. Remove TMDB Key Variable: **"TMDB_KEY"** in [/HomeCinema/GlobalVars.cs](../HomeCinema/GlobalVars.cs).
2. Update version build in [/data/version](../data/version).
3. Update **'Version and Build'** in [/README.md](../README.md).
4. Update [/VERSION_HISTORY.md](../VERSION_HISTORY.md).

## Releasing to Github
1. Compress **/HomeCinema/bin/Release** contents.
2. Add the following file to **'.zip'** file:
    1. [/LICENSE.md](../LICENSE.md).
    2. [/README.md](../README.md).
    3. [/VERSION_HISTORY.md](../VERSION_HISTORY.md).
3. Go to **'Releases'** page, and Draft a new one.
4. Use tag: **v.x.x.x.x** , where **x** is the number version, *see [/HomeCinema/Properties/AssemblyInfo.cs](../HomeCinema/Properties/AssemblyInfo.cs)*.
5. Use Title from the [/VERSION_HISTORY.md](../VERSION_HISTORY.md) file.
6. Use Release details from [/VERSION_HISTORY.md](../VERSION_HISTORY.md).
7. Rename the compressed **zipped** file: **HomeCinema-Windows.zip**.
8. Attach the **zipped file** to the Release draft.
9. Publish release.
