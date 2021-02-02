namespace au.UI.LatestVersion.Tests {
	partial class VersionManagerTestForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Windows.Forms.Label lblRepo;
			this._txtUsername = new System.Windows.Forms.TextBox();
			this.lblRepoDivider = new System.Windows.Forms.Label();
			this._txtRepoName = new System.Windows.Forms.TextBox();
			this._chkAlwaysShow = new System.Windows.Forms.CheckBox();
			this._btnPrompt = new System.Windows.Forms.Button();
			this._btnClose = new System.Windows.Forms.Button();
			lblRepo = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblRepo
			// 
			lblRepo.AutoSize = true;
			lblRepo.Location = new System.Drawing.Point(14, 17);
			lblRepo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lblRepo.Name = "lblRepo";
			lblRepo.Size = new System.Drawing.Size(66, 15);
			lblRepo.TabIndex = 0;
			lblRepo.Text = "Repository:";
			// 
			// _txtUsername
			// 
			this._txtUsername.Location = new System.Drawing.Point(91, 14);
			this._txtUsername.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this._txtUsername.Name = "_txtUsername";
			this._txtUsername.Size = new System.Drawing.Size(116, 23);
			this._txtUsername.TabIndex = 1;
			this._txtUsername.Text = "misterhaan";
			// 
			// lblRepoDivider
			// 
			this.lblRepoDivider.AutoSize = true;
			this.lblRepoDivider.Location = new System.Drawing.Point(211, 17);
			this.lblRepoDivider.Margin = new System.Windows.Forms.Padding(0);
			this.lblRepoDivider.Name = "lblRepoDivider";
			this.lblRepoDivider.Size = new System.Drawing.Size(12, 15);
			this.lblRepoDivider.TabIndex = 2;
			this.lblRepoDivider.Text = "/";
			// 
			// _txtRepoName
			// 
			this._txtRepoName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this._txtRepoName.Location = new System.Drawing.Point(229, 14);
			this._txtRepoName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this._txtRepoName.Name = "_txtRepoName";
			this._txtRepoName.Size = new System.Drawing.Size(316, 23);
			this._txtRepoName.TabIndex = 3;
			// 
			// _chkAlwaysShow
			// 
			this._chkAlwaysShow.AutoSize = true;
			this._chkAlwaysShow.Checked = true;
			this._chkAlwaysShow.CheckState = System.Windows.Forms.CheckState.Checked;
			this._chkAlwaysShow.Location = new System.Drawing.Point(18, 44);
			this._chkAlwaysShow.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this._chkAlwaysShow.Name = "_chkAlwaysShow";
			this._chkAlwaysShow.Size = new System.Drawing.Size(193, 19);
			this._chkAlwaysShow.TabIndex = 4;
			this._chkAlwaysShow.Text = "Show no available update result";
			this._chkAlwaysShow.UseVisualStyleBackColor = true;
			// 
			// _btnPrompt
			// 
			this._btnPrompt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._btnPrompt.Location = new System.Drawing.Point(323, 58);
			this._btnPrompt.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this._btnPrompt.Name = "_btnPrompt";
			this._btnPrompt.Size = new System.Drawing.Size(128, 27);
			this._btnPrompt.TabIndex = 5;
			this._btnPrompt.Text = "PromptForUpdate";
			this._btnPrompt.UseVisualStyleBackColor = true;
			this._btnPrompt.Click += new System.EventHandler(this._btnPrompt_Click);
			// 
			// _btnClose
			// 
			this._btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
			this._btnClose.Location = new System.Drawing.Point(458, 58);
			this._btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this._btnClose.Name = "_btnClose";
			this._btnClose.Size = new System.Drawing.Size(88, 27);
			this._btnClose.TabIndex = 6;
			this._btnClose.Text = "Close";
			this._btnClose.UseVisualStyleBackColor = true;
			this._btnClose.Click += new System.EventHandler(this._btnClose_Click);
			// 
			// VersionManagerTestForm
			// 
			this.AcceptButton = this._btnPrompt;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this._btnClose;
			this.ClientSize = new System.Drawing.Size(550, 90);
			this.ControlBox = false;
			this.Controls.Add(this._btnClose);
			this.Controls.Add(this._btnPrompt);
			this.Controls.Add(this._chkAlwaysShow);
			this.Controls.Add(this._txtRepoName);
			this.Controls.Add(this.lblRepoDivider);
			this.Controls.Add(this._txtUsername);
			this.Controls.Add(lblRepo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "VersionManagerTestForm";
			this.ShowIcon = false;
			this.Text = "VersionManager Test Form";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox _txtUsername;
		private System.Windows.Forms.Label lblRepoDivider;
		private System.Windows.Forms.TextBox _txtRepoName;
		private System.Windows.Forms.CheckBox _chkAlwaysShow;
		private System.Windows.Forms.Button _btnPrompt;
		private System.Windows.Forms.Button _btnClose;
	}
}