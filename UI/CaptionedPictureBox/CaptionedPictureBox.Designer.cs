namespace au.UI.CaptionedPictureBox {
	partial class CaptionedPictureBox {
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
			this._lblCaption = new System.Windows.Forms.Label();
			this._pb = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this._pb)).BeginInit();
			this.SuspendLayout();
			// 
			// _lblCaption
			// 
			this._lblCaption.AutoEllipsis = true;
			this._lblCaption.Dock = System.Windows.Forms.DockStyle.Bottom;
			this._lblCaption.Location = new System.Drawing.Point(0, 127);
			this._lblCaption.Name = "_lblCaption";
			this._lblCaption.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
			this._lblCaption.Size = new System.Drawing.Size(150, 23);
			this._lblCaption.TabIndex = 1;
			this._lblCaption.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this._lblCaption.UseMnemonic = false;
			this._lblCaption.Click += new System.EventHandler(this.LblCaption_Click);
			this._lblCaption.DoubleClick += new System.EventHandler(this.LblCaption_DoubleClick);
			// 
			// _pb
			// 
			this._pb.Dock = System.Windows.Forms.DockStyle.Fill;
			this._pb.Location = new System.Drawing.Point(0, 0);
			this._pb.Name = "_pb";
			this._pb.Size = new System.Drawing.Size(150, 127);
			this._pb.TabIndex = 4;
			this._pb.TabStop = false;
			this._pb.Click += new System.EventHandler(this.Pb_Click);
			this._pb.DoubleClick += new System.EventHandler(this.Pb_DoubleClick);
			// 
			// CaptionedPictureBox
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.Controls.Add(this._pb);
			this.Controls.Add(this._lblCaption);
			this.Name = "CaptionedPictureBox";
			((System.ComponentModel.ISupportInitialize)(this._pb)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label _lblCaption;
		private System.Windows.Forms.PictureBox _pb;
	}
}
