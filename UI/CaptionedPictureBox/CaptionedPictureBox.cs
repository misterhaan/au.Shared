using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace au.UI.CaptionedPictureBox {
	/// <summary>
	/// Captioned PictureBox control
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(PictureBox))]
	public partial class CaptionedPictureBox : UserControl {
		/// <summary>
		/// Default constructor
		/// </summary>
		public CaptionedPictureBox()
			=> InitializeComponent();

		/// <summary>
		/// Image to display when the load of another image fails
		/// </summary>
		[Category("Asynchronous"), Description("Image to display when the load of another image fails.")]
		public Image ErrorImage {
			get => _pb.ErrorImage;
			set => _pb.ErrorImage = value;
		}

		/// <summary>
		/// The image displayed in the PictureBox when the main image is loading
		/// </summary>
		[Category("Asynchronous"), Description("The image displayed in the PictureBox when the main image is loading.")]
		public Image InitialImage {
			get => _pb.InitialImage;
			set => _pb.InitialImage = value;
		}

		/// <summary>
		/// Controls whether processing will stop until the image is loaded
		/// </summary>
		[Category("Asynchronous"), DefaultValue(false), Description("Controls whether processing will stop until the image is loaded.")]
		public bool WaitOnLoad {
			get => _pb.WaitOnLoad;
			set => _pb.WaitOnLoad = value;
		}

		/// <summary>
		/// Disk or Web location to load image from
		/// </summary>
		[Category("Asynchronous"), DefaultValue(null), Description("Disk or Web location to load image from.")]
		public string ImageLocation {
			get => _pb.ImageLocation;
			set => _pb.ImageLocation = value;
		}

		/// <summary>
		/// The image displayed in the PictureBox
		/// </summary>
		[Category("Appearance"), DefaultValue(null), Description("The image displayed in the PictureBox.")]
		public Image Image {
			get => _pb.Image;
			set => _pb.Image = value;
		}

		/// <summary>
		/// Controls how the PictureBox will handle image placement and control sizing
		/// </summary>
		[Category("Behavior"), DefaultValue(PictureBoxSizeMode.Normal), Description("Controls how the PictureBox will handle image placement and control sizing.")]
		public PictureBoxSizeMode SizeMode {
			get => _pb.SizeMode;
			set => _pb.SizeMode = value;
		}

		/// <summary>
		/// The text associated with the control
		/// </summary>
		[Category("Appearance"), DefaultValue(""), Description("The text associated with the control.")]
		public override string Text {
			get => _lblCaption.Text;
			set => _lblCaption.Text = value;
		}

		/// <summary>
		/// The height of the caption in pixels
		/// </summary>
		[Category("Layout"), DefaultValue(23), Description("The height of the caption in pixels.")]
		public int CaptionHeight {
			get => _lblCaption.Height;
			set => _lblCaption.Height = value;
		}

		/// <summary>
		/// The height of the gap between the picture and the caption in pixels
		/// </summary>
		[Category("Layout"), DefaultValue(3), Description("The height of the gap between the picture and the caption in pixels.")]
		public int Gap {
			get => _lblCaption.Padding.Top;
			set => _lblCaption.Padding = new Padding(_lblCaption.Padding.Left, value, _lblCaption.Padding.Right, _lblCaption.Padding.Bottom);
		}

		/// <summary>
		/// Loads the image at the specified location, asynchronously.
		/// </summary>
		/// <param name="url">The path for the image to display in the PictureBox</param>
		public void LoadAsync(string url) => _pb.LoadAsync(url);

		private void _pb_Click(object sender, EventArgs e)
			=> OnClick(e);

		private void _pb_DoubleClick(object sender, EventArgs e)
			=> OnDoubleClick(e);

		private void _lblCaption_Click(object sender, EventArgs e)
			=> OnClick(e);

		private void _lblCaption_DoubleClick(object sender, EventArgs e)
			=> OnDoubleClick(e);
	}
}
