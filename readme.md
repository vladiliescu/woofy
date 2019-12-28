# Woofy

A webcomic downloader with a (crazy) powerful download engine. Will download comics for offline reading, including metadata such as the title and description. _Just a liiittle bit discontinued 🙈_

Also, due to the fact that it uses comic definition files for downloading the comics, it can easily be extended to support new comics.

## What's new in version 1.x

* A completely rewritten download engine.
* An improved user interface, with more information about the download process.
* Downloads comics from start to finish, and not backwards.
* Can embed additional metadata such as the title and description inside each downloaded strip (the metadata can be viewed using any image viewer that supports the XMP standard, such as XnView MP or Zoner Photo Studio)
* The settings files are stored under each user's profile - should fix some issues with Windows 7/Vista.
* Woofy can now be installed in portable mode


## Version history

### 1.24 (2015.04.11)

#### Bugfixes
* Woofy doesn't crash anymore when the network goes down or when a site returns an error (e.g. 403 Forbidden).

---

### 1.23 (2015.03.28)

#### Improvements
* Woofy sends an user agent with each request, in the form of "Woofy/<version number>".

---


### 1.22 (2014.02.02)

#### Bugfixes
* A better fix for the 404 issue.

---

### 1.21 (2014.01.25)

#### Improvements
* Errors are now logged in a flat file instead of the system Event Log, to prevent the need for administrative priviledges.
* Updated exiftool to the latest version.

#### Bugfixes
* Woofy should no longer crash when web sites return 404.
* Metadata strings containing double quotes are now supported.

---

### 1.20 (2013.06.29)

#### Improvements
* Added a definition for Dead Winter.
* The comics to be added are sorted alphabetically.
* The "download" expression can receive arguments from other expressions, such as "match".
* The "base" HTML element is now handled correctly.

####Bugfixes
* Fixed crash when trying to download images from urls containing query arguments.

---

### 1.12 Final (2013.02.09)

#### Bugfixes
* Fixed the random crashes.
* Fixed the issue that prevented Woofy from visiting urls that ended with a dot.
* Woofy is now compiled for .NET 4.5, which means that it will no longer run on Windows XP (if you have to run it on XP, please raise an issue and I'll try to provide a package for it).

---

### 1.11 Beta (2013.02.02)

#### Improvements
* The installer no longer requires administrative permissions to run (will be installed on a per-user basis, similar to Google Chrome).
* Updated exiftool to the latest version.

#### Bugfixes
* Better error handling when visiting pages and downloading comics
* Fixed the xkcd definition.
* Updated the Snowflakes definition.
* Small user interface fixes.

---

### 1.1 Beta (2011.04.10)

#### Improvements
* Added the ability to rename strips based on their index
* Added the ability to edit a comic's settings
* Woofy now handles cookies
* Four new expressions goto, match, log and warn
* Better logging
* Replaced the NSIS installer with Inno Setup
* A new comic - Stuff No One Told Me

#### Bugfixes
* Several user interface fixes

---

###1.0 Alpha (2010.11.27)

#### Improvements
* A completely rewritten download engine.
* An improved user interface, with more information about the download process.
* Downloads comics from start to finish, and not backwards.
* Can embed additional metadata such as the title and description inside each downloaded strip (the metadata can be viewed using any image viewer that supports the XMP standard, such as XnView MP or Zoner Photo Studio)
* A log is kept for each downloaded comic
* After each download Woofy pauses for 3 seconds, in order to minimize server hammering.
* The settings files are stored under each user's profile - should fix some issues with Windows 7/Vista.
* Removed the proxy settings, Woofy will use whatever proxy has been set for Internet Explorer.
* A new comic - Snowflakes
* Woofy can now be installed in portable mode

---

### 0.6.2 (2010.10.04)

#### Improvements
* A few UI tweaks

#### Bugfixes
* Pages containing Unicode characters are now retrieved correctly

---

###0.6.1 (2010.03.28)

#### Improvements
* Added an option to enable random pauses between comic downloads
* Rename captures can now target the url
* Some usability enhancements

#### Bugfixes
* Some minor bugfixes

---

### 0.6.0 (2010.02.17)

#### Improvements
* Removed all comic definitions except xkcd, in order to avoid any further complaints.
* The default download folder is now under My Documents\My Comics.
* Added an experimental option to close Woofy when all comics have finished downloading.
* Woofy will now present a user-agent when downloading comics.
* Replaced the SQLite database with a simple json file - so there shouldn't be any more issues with x64 systems.
* Changed the user settings serialization format from xml to json.
* Changed the license to GPL.

#### Bugfixes
* Woofy no longer crashes when resuming a paused task
* Woofy should no longer crash when the network goes down during a comic download.
