namespace au.UI.DirectoryBox {
	partial class DirectoryBox {
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

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this._txtDirectory = new System.Windows.Forms.TextBox();
			this._btnBrowse = new System.Windows.Forms.Button();
			this._tip = new System.Windows.Forms.ToolTip(this.components);
			this._folderBrowseDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.SuspendLayout();
			// 
			// _txtDirectory
			// 
			this._txtDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._txtDirectory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this._txtDirectory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
			this._txtDirectory.Location = new System.Drawing.Point(0, 0);
			this._txtDirectory.Name = "_txtDirectory";
			this._txtDirectory.Size = new System.Drawing.Size(100, 20);
			this._txtDirectory.TabIndex = 0;
			this._txtDirectory.Validating += new System.ComponentModel.CancelEventHandler(this._txtDirectory_Validating);
			// 
			// _btnBrowse
			// 
			this._btnBrowse.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this._btnBrowse.BackgroundImage = global::au.UI.DirectoryBox.MaterialIcons.FolderBrowseFade16;
			this._btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this._btnBrowse.FlatAppearance.BorderSize = 0;
			this._btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this._btnBrowse.Location = new System.Drawing.Point(107, 1);
			this._btnBrowse.Margin = new System.Windows.Forms.Padding(4, 1, 1, 1);
			this._btnBrowse.Name = "_btnBrowse";
			this._btnBrowse.Size = new System.Drawing.Size(18, 18);
			this._btnBrowse.TabIndex = 1;
			this._btnBrowse.UseVisualStyleBackColor = true;
			this._btnBrowse.Click += new System.EventHandler(this._btnBrowse_Click);
			this._btnBrowse.Enter += new System.EventHandler(this._btnBrowse_Enter);
			this._btnBrowse.Leave += new System.EventHandler(this._btnBrowse_Leave);
			this._btnBrowse.MouseEnter += new System.EventHandler(this._btnBrowse_MouseEnter);
			this._btnBrowse.MouseLeave += new System.EventHandler(this._btnBrowse_MouseLeave);
			// 
			// _folderBrowseDialog
			// 
			this._folderBrowseDialog.ShowNewFolderButton = false;
			// 
			// DirectoryBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this._btnBrowse);
			this.Controls.Add(this._txtDirectory);
			this.Name = "DirectoryBox";
			this.Size = new System.Drawing.Size(126, 20);
			this.EnabledChanged += new System.EventHandler(this.DirectoryBox_EnabledChanged);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox _txtDirectory;
		private System.Windows.Forms.Button _btnBrowse;
		private System.Windows.Forms.ToolTip _tip;
		private System.Windows.Forms.FolderBrowserDialog _folderBrowseDialog;
	}
}
