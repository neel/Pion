Use case name
==============
Download Video

Summary
========
YouTube User selects YouTube Video to download. System downloads video to predefined Download Location.

Actor
=====
YouTube User

Preconditions
=============
The YouTube User has previoiusly defined a valid Download Location.

Main sequence
==============
1. YouTube User selects YouTube Video.
2. System downloads YouTube Video to predefined Download Location under the name of the Video.
3. System announces completion of download.

Extensions
==========
1* Selected Video does not offer a download
1a. System ignores download.


2* YouTube User PC is out of disk space
2a. System cancels download and deletes any progress.


2* Download is already in progress.
2a. System ignores download request.


2* Video with same name already exists
2a. System overwrites existing video.

Postcondition
==============
System has successfully downloaded video.