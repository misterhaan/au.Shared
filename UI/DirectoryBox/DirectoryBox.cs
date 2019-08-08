using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace au.UI.DirectoryBox {
	/// <summary>
	/// Directory name input control
	/// </summary>
	[ToolboxItem(true), DefaultEvent(nameof(DirectoryChanged))]
	[ToolboxBitmap(typeof(DirectoryBox), "folder-browse.png")]
	public partial class DirectoryBox : UserControl {
		/// <summary>
		/// Default constructor
		/// </summary>
		public DirectoryBox()
			=> InitializeComponent();

		/// <summary>
		/// Event raised when the directory selected in the control changes
		/// </summary>
		[Category("Property Changed"), Description("Event raised when the directory selected in the control changes.")]
		public event EventHandler DirectoryChanged;

		/// <summary>
		/// Whether the textbox should autocomplete with file system directories
		/// </summary>
		[Category("Misc"), DefaultValue(true), Description("Whether the textbox should autocomplete with file system directories.")]
		public bool AutoComplete {
			get {
				return _txtDirectory.AutoCompleteSource == AutoCompleteSource.FileSystemDirectories;
			}
			set {
				_txtDirectory.AutoCompleteSource = value
					? AutoCompleteSource.FileSystemDirectories
					: AutoCompleteSource.None;
			}
		}

		/// <summary>
		/// The directory selected in the control
		/// </summary>
		public DirectoryInfo Directory { get; private set; }

		/// <summary>
		/// The directory selected in the control
		/// </summary>
		public override string Text {
			get => _txtDirectory.Text;
			set {
				if(!TrySetDirectory(value, out Exception ex))
					throw ex;
			}
		}

		/// <summary>
		/// Full path to the starting directory when opening the folder browser dialog with no directory filled in
		/// </summary>
		[Category("FileSystem"), DefaultValue(""), Description("Full path to the starting directory when opening the folder browser dialog with no directory filled in.")]
		public string BasePath { get; set; }

		/// <summary>
		/// Text to display above the browse dialog
		/// </summary>
		[Category("Folder Browsing"), DefaultValue(""), Description("Text to display above the browse dialog.")]
		public string Description {
			get => _folderBrowseDialog.Description;
			set => _folderBrowseDialog.Description = value;
		}

		/// <summary>
		/// Whether directories can be created from this control
		/// </summary>
		[Category("Folder Browsing"), DefaultValue(false), Description("Whether directories can be created from this control.")]
		public bool AllowCreation {
			get => _folderBrowseDialog.ShowNewFolderButton;
			set => _folderBrowseDialog.ShowNewFolderButton = value;
		}

		/// <summary>
		/// Attempt to set the directory selected in the control to the specified path.
		/// </summary>
		/// <param name="path">Directory path to select in the control</param>
		public void SetDirectory(string path) {
#if DEBUG
			if(!TrySetDirectory(path, out Exception ex))
				MessageBox.Show(this, string.Format(Messages.DirectoryInvalidWithException, path, ex.Message), Messages.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
#else
			if(!TrySetDirectory(path, out _))
				MessageBox.Show(this, string.Format(Messages.DirectoryInvalid, path), Messages.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
		}

		/// <summary>
		/// Attempt to set the directory selected in the control to the specified path.
		/// </summary>
		/// <param name="path">Directory path to select in the control</param>
		/// <param name="exception">Exception thrown when trying to set the directory (null if return value is true)</param>
		/// <returns>True if successful</returns>
		public bool TrySetDirectory(string path, out Exception exception) {
			exception = null;
			if(string.IsNullOrEmpty(path)) {
				Directory = null;
				_txtDirectory.Text = "";
				DirectoryChanged?.Invoke(this, new EventArgs());
				return true;
			}
			try {
				DirectoryInfo di = new DirectoryInfo(path);
				Directory = di;
				_txtDirectory.Text = Directory.FullName;
				DirectoryChanged?.Invoke(this, new EventArgs());
				return true;
			} catch(Exception e) {
				exception = e;
				_txtDirectory.Text = Directory?.FullName ?? "";
				return false;
			}
		}

		/// <summary>
		/// Show the browse dialog to select a new directory.
		/// </summary>
		private void GetDirectoryFromDialog() {
			_folderBrowseDialog.SelectedPath = Directory?.FullName ?? BasePath ?? "";
			switch(_folderBrowseDialog.ShowDialog(this)) {
				case DialogResult.OK:
				case DialogResult.Yes:
					SetDirectory(_folderBrowseDialog.SelectedPath);
					break;
			}
		}

		/// <summary>
		/// Check if the mouse is over the specified control
		/// </summary>
		/// <param name="control">Control to check for mouse hover</param>
		/// <returns>True if the mouse is over the control</returns>
		private static bool IsMouseOver(Control control) {
			Point mousePosition = control.PointToClient(MousePosition);
			return mousePosition.X >= 0 && mousePosition.X <= control.Width
				&& mousePosition.Y >= 0 && mousePosition.Y <= control.Height;
		}

		/// <summary>
		/// Switch the browse button to its hover / active state
		/// </summary>
		private void UnfadeBrowseButton()
			=> _btnBrowse.BackgroundImage = MaterialIcons.FolderBrowse16;

		/// <summary>
		/// Switch the browse button to its unhovered / inactive state, as long as it's neither hovered nor active
		/// </summary>
		private void MaybeFadeBrowseButton() {
			if(!_btnBrowse.Focused && !IsMouseOver(_btnBrowse))
				_btnBrowse.BackgroundImage = MaterialIcons.FolderBrowseFade16;
		}

		private void _btnBrowse_MouseEnter(object sender, System.EventArgs e)
			=> UnfadeBrowseButton();

		private void _btnBrowse_MouseLeave(object sender, System.EventArgs e)
			=> MaybeFadeBrowseButton();

		private void _btnBrowse_Enter(object sender, System.EventArgs e)
			=> UnfadeBrowseButton();

		private void _btnBrowse_Leave(object sender, System.EventArgs e)
			=> MaybeFadeBrowseButton();

		private void _btnBrowse_Click(object sender, EventArgs e)
			=> GetDirectoryFromDialog();

		private void _txtDirectory_Validating(object sender, CancelEventArgs e)
			=> SetDirectory(_txtDirectory.Text);

		private void DirectoryBox_EnabledChanged(object sender, EventArgs e)
			=> _btnBrowse.Enabled = _txtDirectory.Enabled = Enabled;
	}
}
