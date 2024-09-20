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

		public DirectoryBoxTestForm()
			=> InitializeComponent();

		private void ChkAllowCreation_CheckedChanged(object sender, EventArgs e)
			=> _dirTest.AllowCreation = _chkAllowCreation.Checked;

		private void ChkAutoComplete_CheckedChanged(object sender, EventArgs e)
			=> _dirTest.AutoComplete = _chkAutoComplete.Checked;

		private void TxtBasePath_Validating(object sender, CancelEventArgs e)
			=> _dirTest.BasePath = _txtBasePath.Text;

		private void TxtDescription_Validating(object sender, CancelEventArgs e)
			=> _dirTest.Description = _txtDescription.Text;

		private void ChkEnabled_CheckedChanged(object sender, EventArgs e)
			=> _dirTest.Enabled = _chkEnabled.Checked;

		private void BtnSetDirectory_Click(object sender, EventArgs e) {
			if(!_dirTest.TrySetDirectory(_txtDirectory.Text, out Exception ex))
				MessageBox.Show(this, "Error setting directory:\n\n" + ex.Message, "DirectoryBox Test", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void DirTest_DirectoryChanged(object sender, EventArgs e) {
			_txtDirectory.Text = _dirTest.Directory?.FullName ?? "";
			_txtText.Text = _dirTest.Text;
		}
	}
}
