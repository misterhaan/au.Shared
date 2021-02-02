# the analog underground Shared Utilities
Shared Utilities are a collection of .NET assemblies that are likely to be used by multiple applications.  The collection is versioned together but the components are mostly separate, so some assemblies may be functionally identical across versions.  Initial version of this collection is 5.0.0 since it supersedes au.util which was at 4.1.0 when this collection began.

## Assemblies
Each assembly builds to a DLL of the same name.  While they are in one Visual Studio solution file they for the most part don’t reference each other.

Some assemblies have their interface types in a separate Types DLL.  Generally consumers only need to reference the types while the base DLL is needed to instantiate any of the objects.

### au.IO.Files.FileOperation
Provides access to the Windows Explorer file move / copy dialog.  Requires Windows Vista or newer.

### au.IO.Web.API.GitHub
Provides access to the GitHub web API.  Currently used by au.UI.LatestVersion.

### au.Settings
Saving and loading application settings files, and a settings object to save initial form state.

### au.UI.CaptionedPictureBox
Provides a control for displaying a picture with a caption beneath it.

### au.UI.DirectoryBox
Provides a control for selecting a directory.

### au.UI.LatestVersion
Provides dialogs for checking for, downloading, and installing application updates.

## Revision History
### 6.0.0
* Update to .NET 5.0 (older .NET Framework projects will need to stay on older versions)
* Removed au.UI.TaskDialog since .NET 5.0 provides it

### 5.1.1
* Improved threading for update checking
* Better error message when update checker can’t connect to server

### 5.1.0
* Added libraries for checking for new releases on GitHub
	* au.IO.Web.API.GitHub
	* au.UI.LatestVersion

### 5.0.0
* Starting over from scratch in place of au.util
* Created assemblies needed for [PhotoComb](https://github.com/misterhaan/PhotoComb) and [MythClient](https://github.com/misterhaan/MythClient)
	* au.IO.Files.FileOperation
	* au.Settings
	* au.UI.CaptionedPictureBox
	* au.UI.DirectoryBox
	* au.UI.TaskDialog
