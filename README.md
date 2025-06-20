# BananaSplit

BananaSplit is an application built in .NET WinForms that can be used to split video files based on ffmpeg's black frame detection feature.

## Installation

To install BananaSplit, simply extract the zip file to a location of your choosing. Those more interested in the code can clone the directory and run the application from Visual Studio.

## Known Limitations

 - The application in its current state is not very thread-safe. Some processes are spun off into separate threads so the application does not become unresponsive, but in general you should avoid adding files or encoding files if the application is already scanning for black frames or currently encoding a file.
 - Windows only. 

### Version
4.0.3

### Changelog

#### 4.0.3
Pending

#### 4.0.2
Pending

#### 4.0.0
Pending

#### 3.1.0

- Added support for drag and drop files
- Added support to delete files with delete key
- Fixed possible crash
- Fixed cursor not changing to the right icon

(Thanks to [ricardosabino](https://github.com/ricardosabino) for his [pull request](https://github.com/pathartl/BananaSplit/pull/24)!)

#### 3.0.0

- Another complete rewrite. We've become exceedingly efficient at it.
- Drop support of Mac/Linux
- ffmpeg arguments are now customizable from the settings menu
- It's now easier to pick where the splits/transitions exist
- The offset for generating the reference frame (thumbnail) is now customizable
- Frame detection is more granular and more reliable
- Instead of reencoding files, you can now choose to generate chapters in the resulting Matroska file (useful for later reencoding or playback with some players)
- No longer built on Electron, so performance should be somewhat better

#### 2.0.0

- Complete rewrite of the backend, now built in Electron!
- Support of Windows, Mac, Linux
- Made more sections of the app asynchronous
- Fixed some issues with the file browser

#### 1.1.0

- Added Windows compatibility
- Fixed a few UX/interface bugs
- Allowed the queue to stay opened but hidden when using the app
- Removed the need to write thumbnails to disk

#### 1.0.0

- Initial Release!

### License

This software is provided as non-commercial free software with source available. Don't be an ass.
