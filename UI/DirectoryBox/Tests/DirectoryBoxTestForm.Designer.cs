namespace Tests {
	partial class DirectoryBoxTestForm {
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
			System.Windows.Forms.Panel pnlProperties;
			System.Windows.Forms.Label lblBasePath;
			System.Windows.Forms.Label lblText;
			this._chkEnabled = new System.Windows.Forms.CheckBox();
			this._chkAutoComplete = new System.Windows.Forms.CheckBox();
			this._chkAllowCreation = new System.Windows.Forms.CheckBox();
			this._btnSetDirectory = new System.Windows.Forms.Button();
			this.lblDirectory = new System.Windows.Forms.Label();
			this._txtDirectory = new System.Windows.Forms.TextBox();
			this.lblDescription = new System.Windows.Forms.Label();
			this._txtDescription = new System.Windows.Forms.TextBox();
			this._txtBasePath = new System.Windows.Forms.TextBox();
			this._dirTest = new au.UI.DirectoryBox.DirectoryBox();
			this._txtText = new System.Windows.Forms.TextBox();
			pnlProperties = new System.Windows.Forms.Panel();
			lblBasePath = new System.Windows.Forms.Label();
			lblText = new System.Windows.Forms.Label();
			pnlProperties.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlProperties
			// 
			pnlProperties.BackColor = System.Drawing.SystemColors.Window;
			pnlProperties.Controls.Add(this._txtText);
			pnlProperties.Controls.Add(lblText);
			pnlProperties.Controls.Add(this._chkEnabled);
			pnlProperties.Controls.Add(this._chkAutoComplete);
			pnlProperties.Controls.Add(this._chkAllowCreation);
			pnlProperties.Controls.Add(this._btnSetDirectory);
			pnlProperties.Controls.Add(this.lblDirectory);
			pnlProperties.Controls.Add(this._txtDirectory);
			pnlProperties.Controls.Add(this.lblDescription);
			pnlProperties.Controls.Add(this._txtDescription);
			pnlProperties.Controls.Add(lblBasePath);
			pnlProperties.Controls.Add(this._txtBasePath);
			pnlProperties.Dock = System.Windows.Forms.DockStyle.Top;
			pnlProperties.Location = new System.Drawing.Point(0, 0);
			pnlProperties.Name = "pnlProperties";
			pnlProperties.Size = new System.Drawing.Size(487, 138);
			pnlProperties.TabIndex = 0;
			// 
			// _chkEnabled
			// 
			this._chkEnabled.AutoSize = true;
			this._chkEnabled.Checked = true;
			this._chkEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
			this._chkEnabled.Location = new System.Drawing.Point(275, 116);
			this._chkEnabled.Name = "_chkEnabled";
			this._chkEnabled.Size = new System.Drawing.Size(65, 17);
			this._chkEnabled.TabIndex = 10;
			this._chkEnabled.Text = "Enabled";
			this._chkEnabled.UseVisualStyleBackColor = true;
			this._chkEnabled.CheckedChanged += new System.EventHandler(this._chkEnabled_CheckedChanged);
			// 
			// _chkAutoComplete
			// 
			this._chkAutoComplete.AutoSize = true;
			this._chkAutoComplete.Checked = true;
			this._chkAutoComplete.CheckState = System.Windows.Forms.CheckState.Checked;
			this._chkAutoComplete.Location = new System.Drawing.Point(177, 116);
			this._chkAutoComplete.Name = "_chkAutoComplete";
			this._chkAutoComplete.Size = new System.Drawing.Size(92, 17);
			this._chkAutoComplete.TabIndex = 9;
			this._chkAutoComplete.Text = "AutoComplete";
			this._chkAutoComplete.UseVisualStyleBackColor = true;
			this._chkAutoComplete.CheckedChanged += new System.EventHandler(this._chkAutoComplete_CheckedChanged);
			// 
			// _chkAllowCreation
			// 
			this._chkAllowCreation.AutoSize = true;
			this._chkAllowCreation.Location = new System.Drawing.Point(81, 116);
			this._chkAllowCreation.Name = "_chkAllowCreation";
			this._chkAllowCreation.Size = new System.Drawing.Size(90, 17);
			this._chkAllowCreation.TabIndex = 8;
			this._chkAllowCreation.Text = "AllowCreation";
			this._chkAllowCreation.UseVisualStyleBackColor = true;
			this._chkAllowCreation.CheckedChanged += new System.EventHandler(this._chkAllowCreation_CheckedChanged);
			// 
			// _btnSetDirectory
			// 
			this._btnSetDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._btnSetDirectory.Location = new System.Drawing.Point(436, 64);
			this._btnSetDirectory.Name = "_btnSetDirectory";
			this._btnSetDirectory.Size = new System.Drawing.Size(39, 20);
			this._btnSetDirectory.TabIndex = 7;
			this._btnSetDirectory.Text = "Set";
			this._btnSetDirectory.UseVisualStyleBackColor = true;
			this._btnSetDirectory.Click += new System.EventHandler(this._btnSetDirectory_Click);
			// 
			// lblDirectory
			// 
			this.lblDirectory.AutoSize = true;
			this.lblDirectory.Location = new System.Drawing.Point(12, 67);
			this.lblDirectory.Name = "lblDirectory";
			this.lblDirectory.Size = new System.Drawing.Size(52, 13);
			this.lblDirectory.TabIndex = 5;
			this.lblDirectory.Text = "Directory:";
			// 
			// _txtDirectory
			// 
			this._txtDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._txtDirectory.Location = new System.Drawing.Point(81, 64);
			this._txtDirectory.Name = "_txtDirectory";
			this._txtDirectory.Size = new System.Drawing.Size(349, 20);
			this._txtDirectory.TabIndex = 6;
			// 
			// lblDescription
			// 
			this.lblDescription.AutoSize = true;
			this.lblDescription.Location = new System.Drawing.Point(12, 41);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(63, 13);
			this.lblDescription.TabIndex = 3;
			this.lblDescription.Text = "Description:";
			// 
			// _txtDescription
			// 
			this._txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._txtDescription.Location = new System.Drawing.Point(81, 38);
			this._txtDescription.Name = "_txtDescription";
			this._txtDescription.Size = new System.Drawing.Size(394, 20);
			this._txtDescription.TabIndex = 4;
			this._txtDescription.Validating += new System.ComponentModel.CancelEventHandler(this._txtDescription_Validating);
			// 
			// lblBasePath
			// 
			lblBasePath.AutoSize = true;
			lblBasePath.Location = new System.Drawing.Point(12, 15);
			lblBasePath.Name = "lblBasePath";
			lblBasePath.Size = new System.Drawing.Size(56, 13);
			lblBasePath.TabIndex = 1;
			lblBasePath.Text = "BasePath:";
			// 
			// _txtBasePath
			// 
			this._txtBasePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._txtBasePath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this._txtBasePath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
			this._txtBasePath.Location = new System.Drawing.Point(81, 12);
			this._txtBasePath.Name = "_txtBasePath";
			this._txtBasePath.Size = new System.Drawing.Size(394, 20);
			this._txtBasePath.TabIndex = 2;
			this._txtBasePath.Validating += new System.ComponentModel.CancelEventHandler(this._txtBasePath_Validating);
			// 
			// _dirTest
			// 
			this._dirTest.AllowCreation = true;
			this._dirTest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._dirTest.BasePath = null;
			this._dirTest.Location = new System.Drawing.Point(12, 151);
			this._dirTest.Name = "_dirTest";
			this._dirTest.Size = new System.Drawing.Size(463, 20);
			this._dirTest.TabIndex = 11;
			this._dirTest.DirectoryChanged += new System.EventHandler(this._dirTest_DirectoryChanged);
			// 
			// lblText
			// 
			lblText.AutoSize = true;
			lblText.Location = new System.Drawing.Point(12, 93);
			lblText.Name = "lblText";
			lblText.Size = new System.Drawing.Size(31, 13);
			lblText.TabIndex = 8;
			lblText.Text = "Text:";
			// 
			// _txtText
			// 
			this._txtText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._txtText.Location = new System.Drawing.Point(81, 90);
			this._txtText.Name = "_txtText";
			this._txtText.ReadOnly = true;
			this._txtText.Size = new System.Drawing.Size(394, 20);
			this._txtText.TabIndex = 9;
			// 
			// DirectoryBoxTestForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(487, 183);
			this.Controls.Add(pnlProperties);
			this.Controls.Add(this._dirTest);
			this.Name = "DirectoryBoxTestForm";
			this.Text = "DirectoryBox Test";
			pnlProperties.ResumeLayout(false);
			pnlProperties.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private au.UI.DirectoryBox.DirectoryBox _dirTest;
		private System.Windows.Forms.CheckBox _chkEnabled;
		private System.Windows.Forms.CheckBox _chkAutoComplete;
		private System.Windows.Forms.CheckBox _chkAllowCreation;
		private System.Windows.Forms.Button _btnSetDirectory;
		private System.Windows.Forms.Label lblDirectory;
		private System.Windows.Forms.TextBox _txtDirectory;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.TextBox _txtDescription;
		private System.Windows.Forms.TextBox _txtBasePath;
		private System.Windows.Forms.TextBox _txtText;
	}
}