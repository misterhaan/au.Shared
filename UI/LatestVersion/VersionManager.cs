using System;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using au.IO.Web.API.GitHub;
using au.IO.Web.API.GitHub.Types;

namespace au.UI.LatestVersion {
	/// <summary>
	/// Manages checking for and updating to the latest version of an application.
	/// </summary>
	public class VersionManager {
		/// <summary>
		/// Responses from the update available dialog.
		/// </summary>
		/// <remarks>
		/// Values not copied from DialogResult must be large enough int values to not overlap with anything in DialogResult
		/// </remarks>
		private enum UpdateDialogResponse {
			/// <summary>
			/// User canceled the dialog or chose to ignore the update for now
			/// </summary>
			Cancel = DialogResult.Cancel,

			/// <summary>
			/// User chose to download the update but not automatically install
			/// </summary>
			DownloadOnly = 80,

			/// <summary>
			/// User chose to download the update and install it automatically
			/// </summary>
			DownloadInstall = 81
		}

		/// <summary>
		/// Update checker this VersionManager will use to check for updates
		/// </summary>
		private readonly IUpdateChecker _updates;

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="username">Username that owns the repository</param>
		/// <param name="repoName">Repository that has releases of this application</param>
		public VersionManager(string username, string repoName)
			: this(new UpdateChecker(username, repoName)) { }

		/// <summary>
		/// Testing constructor
		/// </summary>
		/// <param name="updates">Task that will provide the result of an update check</param>
		internal VersionManager(IUpdateChecker updates) {
			_updates = updates;
		}

		/// <summary>
		/// Prompt to update if there's a newer version available.
		/// </summary>
		/// <param name="owner">Owner window for dialogs</param>
		public async Task PromptForUpdate(IWin32Window owner)
			=> await PromptForUpdate(owner, false).ConfigureAwait(false);

		/// <summary>
		/// Prompt to update if there's a newer version available.
		/// </summary>
		/// <param name="owner">Owner window for dialogs</param>
		/// <param name="showIfNoUpdate">True to show update check results even if there's no update available</param>
		public async Task PromptForUpdate(IWin32Window owner, bool showIfNoUpdate) {
			IUpdateCheckResult update = await _updates.CheckAsync();

			if(update.Available) {
				TaskDialogButton response = TaskDialog.ShowDialog(owner, new TaskDialogPage {
					Caption = Dialog.UpdateAvailableTitle,
					Heading = string.Format(Dialog.UpdateAvailableDescription, update.Name),
					Text = update.Description,  // note:  description can contain markdown, which will be shown to the user
					Buttons = GetUpdateDialogButtons(update.Url),
				});

				switch(response.Tag as UpdateDialogResponse?) {
					case UpdateDialogResponse.DownloadInstall:
						string installerFilename = Path.Combine(KnownFolders.Temp, Path.GetFileName(update.Url.LocalPath));
						await DownloadUpdate(update.Url, installerFilename).ConfigureAwait(false);
						Process.Start("msiexec", $"/i \"{installerFilename}\" RESTART=1");  // installer should have a custom action to launch the application with the condition RESTART=1
						Application.Exit();
						break;
					case UpdateDialogResponse.DownloadOnly:
						string downloadFilename = PromptForSaveLocation(owner, update.Url);
						if(!string.IsNullOrEmpty(downloadFilename))
							await DownloadUpdate(update.Url, downloadFilename).ConfigureAwait(false);
						break;
					case UpdateDialogResponse.Cancel:
						// Nothing to do because user chose not to update
						break;
				}
			} else if(showIfNoUpdate)
				MessageBox.Show(owner, update.Description, update.Name, MessageBoxButtons.OK, EventLevelToMessageBoxIcon(update.Level));
		}

		/// <summary>
		/// Get the dialog buttons that apply based on the update URL.
		/// </summary>
		/// <param name="updateUrl">URL to the latest version update</param>
		/// <returns></returns>
		private static TaskDialogButtonCollection GetUpdateDialogButtons(Uri updateUrl) {
			TaskDialogButtonCollection buttons = [];

			// install only works for msi
			if(Path.GetExtension(updateUrl.LocalPath).Equals(".msi", StringComparison.OrdinalIgnoreCase))
				buttons.Add(new TaskDialogCommandLinkButton(Dialog.DownloadAndInstallUpdateOptionTitle, Dialog.DownloadAndInstallUpdateOptionDescription) {
					Tag = UpdateDialogResponse.DownloadInstall
				});

			// all update package types can be downloaded or ignored
			buttons.Add(new TaskDialogCommandLinkButton(Dialog.DownloadUpdateOnlyOptionTitle, Dialog.DownloadUpdateOnlyOptionDescription) {
				Tag = UpdateDialogResponse.DownloadOnly
			});
			buttons.Add(new TaskDialogCommandLinkButton(Dialog.IgnoreUpdateOptionTitle, Dialog.IgnoreUpdateOptionDescription) {
				Tag = UpdateDialogResponse.Cancel
			});

			return buttons;
		}

		/// <summary>
		/// Prompt for a location and filename to download the update.
		/// </summary>
		/// <param name="owner">Owner window for dialogs</param>
		/// <param name="url">URL to the latest version update</param>
		/// <returns>Full path and filename where download should be saved.  Empty string if the user canceled.</returns>
		private static string PromptForSaveLocation(IWin32Window owner, Uri url) {
			SaveFileDialog dialog = new() {
				Title = Dialog.DownloadInstallerTitle,
				AutoUpgradeEnabled = true,
				InitialDirectory = KnownFolders.Downloads,
				FileName = Path.GetFileName(url.LocalPath)
			};
			return dialog.ShowDialog(owner) switch {
				DialogResult.OK
				or DialogResult.Yes
					=> dialog.FileName,
				_ => "",
			};
		}

		/// <summary>
		/// Download a URL to a local file.
		/// </summary>
		/// <param name="url">URL to download from</param>
		/// <param name="localFilename">Local filename to download to</param>
		private static async Task DownloadUpdate(Uri url, string localFilename) {
			FileInfo localFile = new(localFilename);
			if(!localFile.Directory.Exists)
				Directory.CreateDirectory(localFile.DirectoryName);
			WebRequest request = HttpWebRequest.Create(url);
			using WebResponse response = await request.GetResponseAsync().ConfigureAwait(false);
			using FileStream fileStream = localFile.Create();
			await response.GetResponseStream().CopyToAsync(fileStream).ConfigureAwait(false);
		}

		/// <summary>
		/// Convert an event level enum value to the equivalent message box icon.
		/// </summary>
		/// <param name="level">Event level to convert</param>
		/// <returns>Message box icon equilavent of level</returns>
		private static MessageBoxIcon EventLevelToMessageBoxIcon(EventLevel level) {
			return level switch {
				EventLevel.Critical
					=> MessageBoxIcon.Stop,
				EventLevel.Error
					=> MessageBoxIcon.Error,
				EventLevel.Warning
					=> MessageBoxIcon.Warning,
				EventLevel.Informational
					=> MessageBoxIcon.Information,
				EventLevel.Verbose
					=> MessageBoxIcon.Asterisk,
				_ => MessageBoxIcon.None,
			};
		}
	}
}
