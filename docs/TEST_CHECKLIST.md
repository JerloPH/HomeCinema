# Test Checklist

## Update checker
1. Reduce **'HOMECINEMA_BUILD'** from **'GlobalVars.cs'**
2. Make sure in **'data/settings.json'**, **"autoUpdate"** is set to **1**.
3. Run App. It will prompt an update.
4. Also, manually check update from **'Settings'** form.

## New User / No HomeCinema.db
1. Build project for **"Release"**.
2. Run App. It will prompt to select folder to search for media files.

## TMDB Features
1. Run App.
2. Select an entry and right-click.
3. Choose **'Edit Information'**.
4. Search the entry by clicking **'Search IMDB'**.
5. It will display search results.
6. Click 1 result and click on **OK** button.
7. It will fetch data from TMDB.