using System;
using System.Windows.Forms;

namespace au.UI.LatestVersion.Tests {
	public partial class VersionManagerTestForm : Form {
		[STAThread]
		public static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new VersionManagerTestForm());
		}

		public VersionManagerTestForm() {
			InitializeComponent();
		}

		private async void _btnPrompt_Click(object sender, EventArgs e) {
			VersionManager manager = new VersionManager(_txtUsername.Text, _txtRepoName.Text);
			await manager.PromptForUpdate(this, _chkAlwaysShow.Checked);
		}

		private void _btnClose_Click(object sender, EventArgs e)
			=> Application.Exit();
	}
}
