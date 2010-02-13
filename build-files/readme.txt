***WARNING***

Woofy v0.6 uses a new format for the comics database and settings, making it incompatible with v0.5. It's recommended to install it in a separate folder and recreate or migrate your existing comic tasks.

***Changes***

* Removed all comic definitions except xkcd, in order to avoid any further complaints.
* The default download folder is now under My Documents\My Comics.
* Added an experimental option to close Woofy when all comics have finished downloading.
* Woofy will now present a user-agent when downloading comics.
* Woofy no longer crashes when unpausing a paused task
* Woofy should no longer crash when the network goes down during a comic download.

* Replaced the SQLite database with a simple json file.
* Changed the user settings serialization format from xml to json.

* Changed the license to GPL.