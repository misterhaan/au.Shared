using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace au.Settings.Tests {
	[TestClass]
	public class SettingsFileManagerTests {
		private const string NEW_PROPERTY_DEFAULT = "defaultValue";

		[TestMethod]
		public void SaveLoad_PropertiesMatch() {
			FileInfo file = new FileInfo(Path.Combine(Path.GetTempPath(), $"{nameof(SettingsFileManagerTests)}.{nameof(this.SaveLoad_PropertiesMatch)}.xml"));
			try {
				SettingsFileManager<FakeSettings> settingsManager = new SettingsFileManager<FakeSettings>(file);
				settingsManager.Settings.NewProperty = "foo";
				settingsManager.Settings.IsAwesome = true;
				settingsManager.Settings.Title = "hello";
				FakeSettings savedSettings = settingsManager.Settings;
				settingsManager.Save();

				FakeSettings loadedSettings = new SettingsFileManager<FakeSettings>(file).Settings;

				Assert.AreEqual(savedSettings.NewProperty, loadedSettings.NewProperty);
				Assert.AreEqual(savedSettings.IsAwesome, loadedSettings.IsAwesome);
				Assert.AreEqual(savedSettings.Title, loadedSettings.Title);
			} finally {
				try { file.Delete(); } catch { }
			}
		}

		[TestMethod]
		public void LoadOldSettings_NewPropertyHasCorrectDefault() {
			FileInfo file = new FileInfo(Path.Combine(Path.GetTempPath(), $"{nameof(SettingsFileManagerTests)}.{nameof(this.LoadOldSettings_NewPropertyHasCorrectDefault)}.xml"));
			try {
				SettingsFileManager<OldFakeSettings> oldSettingsManager = new SettingsFileManager<OldFakeSettings>(file);
				oldSettingsManager.Settings.IsAwesome = true;
				oldSettingsManager.Save();
				string serialized;
				using(StreamReader reader = file.OpenText())
					serialized = reader.ReadToEnd();
				using(StreamWriter writer = file.CreateText())
					writer.Write(serialized.Replace(nameof(OldFakeSettings), nameof(FakeSettings)));

				FakeSettings settings = new SettingsFileManager<FakeSettings>(file).Settings;

				Assert.AreEqual(NEW_PROPERTY_DEFAULT, settings.NewProperty);
			} finally {
				try { file.Delete(); } catch { }
			}
		}

		public class FakeSettings : OldFakeSettings {
			public string NewProperty { get; set; } = NEW_PROPERTY_DEFAULT;
		}

		public class OldFakeSettings {
			public bool IsAwesome { get; set; } = false;
			public string Title { get; set; } = "fakeTitle";
		}
	}
}
