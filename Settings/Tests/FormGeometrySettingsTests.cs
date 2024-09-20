using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace au.Settings.Tests {
	[TestClass]
	public class FormGeometrySettingsTests {
		[TestMethod]
		public void WindowState_DefaultNormal() {
			FormGeometrySettings settings = new();

			Assert.AreEqual(FormWindowState.Normal, settings.WindowState);
		}

		[TestMethod]
		public void Location_DefaultNoValue() {
			FormGeometrySettings settings = new();

			Assert.IsFalse(settings.Location.HasValue);
		}

		[TestMethod]
		public void Size_DefaultNoValue() {
			FormGeometrySettings settings = new();

			Assert.IsFalse(settings.Size.HasValue);
		}

		[TestMethod]
		public void XmlSerializeDeserialize_SameSettings() {
			FormGeometrySettings originalSettings = new() {
				WindowState = FormWindowState.Maximized,
				Location = new Point(12, 34),
				Size = new Size(1280, 720)
			};
			XmlSerializer serializer = new(originalSettings.GetType());
			MemoryStream stream = new();
			serializer.Serialize(stream, originalSettings);
			stream.Flush();
			stream.Position = 0;
			FormGeometrySettings deserializedSettings = serializer.Deserialize(stream) as FormGeometrySettings;

			Assert.AreEqual(originalSettings.WindowState, deserializedSettings.WindowState);
			Assert.AreEqual(originalSettings.Location, deserializedSettings.Location);
			Assert.AreEqual(originalSettings.Size, deserializedSettings.Size);
		}
	}
}
