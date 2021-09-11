REM This script will export a file (output.txt) of commits from 'left side' tag to current 'HEAD' of a repo.
CD /d "%~dp0"
@echo off
set /p lefttag="Type tag of last commit (left side): "

git log %lefttag%..HEAD > output.txt