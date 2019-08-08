using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Tests {
	public partial class DirectoryBoxTestForm : Form {
		[STAThread]
		public static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new DirectoryBoxTestForm());
		}

		public DirectoryBoxTestForm() {
			InitializeComponent();
		}

		private void _chkAllowCreation_CheckedChanged(object sender, EventArgs e)
			=> _dirTest.AllowCreation = _chkAllowCreation.Checked;

		private void _chkAutoComplete_CheckedChanged(object sender, EventArgs e)
			=> _dirTest.AutoComplete = _chkAutoComplete.Checked;

		private void _txtBasePath_Validating(object sender, CancelEventArgs e)
			=> _dirTest.BasePath = _txtBasePath.Text;

		private void _txtDescription_Validating(object sender, CancelEventArgs e)
			=> _dirTest.Description = _txtDescription.Text;

		private void _chkEnabled_CheckedChanged(object sender, EventArgs e)
			=> _dirTest.Enabled = _chkEnabled.Checked;

		private void _btnSetDirectory_Click(object sender, EventArgs e) {
			if(!_dirTest.TrySetDirectory(_txtDirectory.Text, out Exception ex))
				MessageBox.Show(this, "Error setting directory:\n\n" + ex.Message, "DirectoryBox Test", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void _dirTest_DirectoryChanged(object sender, EventArgs e) {
			_txtDirectory.Text = _dirTest.Directory?.FullName ?? "";
			_txtText.Text = _dirTest.Text;
		}
	}
}
