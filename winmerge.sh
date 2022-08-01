#!/bin/sh
 echo Launching WinMergeU.exe: $1 $2
 "G:\Aplications\WinMerge\WinMergeU.exe" -e -u -dl "Local" -dr "Remote" "$1" "$2"