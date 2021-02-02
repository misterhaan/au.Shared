using System;
using System.IO;
using System.Runtime.InteropServices;

namespace au.UI.LatestVersion {
	/// <summary>
	/// Access to known folder locations
	/// </summary>
	internal static class KnownFolders {
		/// <summary>
		/// User's downloads folder
		/// </summary>
		internal static string Downloads => _downloads.Value;
		private static readonly Lazy<string> _downloads = new Lazy<string>(GetDownloadsFolder);

		/// <summary>
		/// User's temp folder
		/// </summary>
		internal static string Temp => _temp.Value;
		private static readonly Lazy<string> _temp = new Lazy<string>(Path.GetTempPath);

		/// <summary>
		/// Looks up user's downloads folder.  Should only run once.
		/// </summary>
		/// <returns>User's downloads folder path</returns>
		private static string GetDownloadsFolder()
			=> GetFolderFromGuid("374DE290-123F-4565-9164-39C4925E467B");

		/// <summary>
		/// Looks up a known folder path from a guid using Windows API.
		/// </summary>
		/// <param name="guid">GUID of known folder to look up</param>
		/// <returns>Known folder path</returns>
		private static string GetFolderFromGuid(string guid) {
			Guid folderId = new Guid(guid);
			IntPtr pathPtr = IntPtr.Zero;
			try {
				return (SHGetKnownFolderPath(ref folderId, 0, IntPtr.Zero, out pathPtr)) switch {
					0  // S_OK
						=> Marshal.PtrToStringUni(pathPtr),
					0x80070057  // E_INVALIDARG 0x80070057
						=> throw new DirectoryNotFoundException("Requested folder not defined on this system"),
					0x80004005  // E_FAIL
						=> throw new Exception("Failed to get folder path"),
					_ => throw new Exception($"Unexpected response from {nameof(SHGetKnownFolderPath)}"),
				};
			} finally {
				Marshal.FreeCoTaskMem(pathPtr);
			}
		}

		[DllImport("shell32.dll", CharSet = CharSet.Auto)]
		private static extern uint SHGetKnownFolderPath(ref Guid id, int flags, IntPtr token, out IntPtr path);
	}
}
