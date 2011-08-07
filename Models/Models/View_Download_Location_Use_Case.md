Use case name
==============
View Download Location

Summary
========
YouTube User tells System to show Download and the System then shows the download location.

Actor
=====
YouTube User

Preconditions
=============
YouTube User has completely downloaded a video.

Main sequence
==============
1. YouTube User tells System to show Download.
2. System shows download location.

Extensions
==========
1* System has not yet completely downloaded the Video.
1a. System ignores request.

1* YouTube User has not started a download.
1a. System ignores request.

Postcondition
==============
System shows download location.