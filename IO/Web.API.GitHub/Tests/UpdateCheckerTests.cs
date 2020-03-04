using System;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using au.IO.Web.API.GitHub.Types;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace au.IO.Web.API.GitHub.Tests {
	[TestClass]
	public class UpdateCheckerTests {
		private const string _username = "username";
		private const string _repoSameVersion = "same";
		private const string _repoNewerVersion = "newer";
		private const string _repoNoFiles = "noFiles";

		[TestMethod]
		public async Task CheckAsync_SameVersion_NoUpdateAlreadyLatest() {
			UpdateChecker updates = GetUpdateChecker(_repoSameVersion);

			IUpdateCheckResult result = await updates.CheckAsync();

			Assert.IsFalse(result.Available, $"{nameof(updates.CheckAsync)}() should say there's no update available when the latest version matches the current version.");
			Assert.AreEqual(Messages.NoUpdateAlreadyLatest, result.Description, $"{nameof(updates.CheckAsync)}() should use the {nameof(Messages.NoUpdateAlreadyLatest)} message when the latest version matches the current version.");
		}

		[TestMethod]
		public async Task CheckAsync_NewerVersion_UpdateAvailable() {
			UpdateChecker updates = GetUpdateChecker(_repoNewerVersion);

			IUpdateCheckResult result = await updates.CheckAsync();

			Assert.IsTrue(result.Available, $"{nameof(updates.CheckAsync)}() should say there's an update available when the latest version is newer than the current version.");
		}

		[TestMethod]
		public async Task CheckAsync_NoFiles_NoUpdateNoFiles() {
			UpdateChecker updates = GetUpdateChecker(_repoNoFiles);

			IUpdateCheckResult result = await updates.CheckAsync();

			Assert.IsFalse(result.Available, $"{nameof(updates.CheckAsync)}() should say there's no update available when the latest version has no files.");
			Assert.AreEqual(Messages.NoUpdateNoFiles, result.Description, $"{nameof(updates.CheckAsync)}() should use the {nameof(Messages.NoUpdateNoFiles)} message when the latest version has no files.");
		}

		[TestMethod]
		public async Task CheckAsync_NotFoundException_NoUpdateNoReleases() {
			UpdateChecker updates = GetUpdateChecker("notFound", GetHttpWebException(HttpStatusCode.NotFound));

			IUpdateCheckResult result = await updates.CheckAsync();

			Assert.IsFalse(result.Available, $"{nameof(updates.CheckAsync)}() should say there's no update available when there are no releases for the repo or it doesn't exist.");
			Assert.AreEqual(Messages.NoUpdateNoReleases, result.Description, $"{nameof(updates.CheckAsync)}() should use the {nameof(Messages.NoUpdateNoReleases)} message when there are no releases for the repo or it doesn't exist.");
		}

		[DataTestMethod]
		[DataRow(HttpStatusCode.BadGateway)]
		[DataRow(HttpStatusCode.Unauthorized)]
		public async Task CheckAsync_OtherProtocolException_NoUpdateError(HttpStatusCode status) {
			UpdateChecker updates = GetUpdateChecker("otherProtocolException", GetHttpWebException(status));

			IUpdateCheckResult result = await updates.CheckAsync();

			Assert.IsFalse(result.Available, $"{nameof(updates.CheckAsync)}() should say there's no update available when the API returns an error response other than 404 not found.");
			Assert.AreEqual(Messages.NoUpdateError, result.Description, $"{nameof(updates.CheckAsync)}() should use the {nameof(Messages.NoUpdateError)} message when the API returns an error response other than 404 not found.");
		}

		[DataTestMethod]
		[DataRow(WebExceptionStatus.Timeout)]
		[DataRow(WebExceptionStatus.ConnectFailure)]
		[DataRow(WebExceptionStatus.UnknownError)]
		public async Task CheckAsync_UnexpectedException_NoUpdateError(WebExceptionStatus status) {
			UpdateChecker updates = GetUpdateChecker("otherException", GetWebException(status));

			IUpdateCheckResult result = await updates.CheckAsync();

			Assert.IsFalse(result.Available, $"{nameof(updates.CheckAsync)}() should say there's no update available when the API does not return a response.");
			Assert.AreEqual(Messages.NoUpdateError, result.Description, $"{nameof(updates.CheckAsync)}() should use the {nameof(Messages.NoUpdateError)} message when the API does not return a response.");
		}

		private static UpdateChecker GetUpdateChecker(string repoName)
			=> GetUpdateChecker(repoName, null);

		private static UpdateChecker GetUpdateChecker(string repoName, WebException wex)
			=> new UpdateChecker(_username, repoName, GetApiFactory(wex));

		private static IApiFactory GetApiFactory(WebException wex) {
			IApiFactory factory = A.Fake<IApiFactory>();
			if(wex == null) {
				A.CallTo(() => factory.BuildReposApi(_username, _repoSameVersion)).Returns(GetReposApi(GetSameVersionRelease()));
				A.CallTo(() => factory.BuildReposApi(_username, _repoNewerVersion)).Returns(GetReposApi(GetNewerVersionRelease(true)));
				A.CallTo(() => factory.BuildReposApi(_username, _repoNoFiles)).Returns(GetReposApi(GetNewerVersionRelease(false)));
			} else {
				A.CallTo(() => factory.BuildReposApi(_username, A<string>.Ignored)).Returns(GetReposApi(wex));
			}
			return factory;
		}

		private static IReposApi GetReposApi(IRelease latestRelease) {
			IReposApi api = A.Fake<IReposApi>();
			A.CallTo(() => api.LatestRelease()).Returns(Task.FromResult(latestRelease));
			return api;
		}

		private static IReposApi GetReposApi(WebException wex) {
			IReposApi api = A.Fake<IReposApi>();
			A.CallTo(() => api.LatestRelease()).Throws(wex);
			return api;
		}

		private static IRelease GetSameVersionRelease() {
			IRelease release = A.Fake<IRelease>();
			A.CallTo(() => release.tag_name).Returns("v" + GetThisVersion());
			return release;
		}

		private static IRelease GetNewerVersionRelease(bool hasFiles) {
			IRelease release = A.Fake<IRelease>();
			A.CallTo(() => release.tag_name).Returns("v" + GetNewerVersion());
			if(hasFiles)
				A.CallTo(() => release.assets).Returns(new IAsset[] { GetAsset() });
			return release;
		}

		private static IAsset GetAsset() {
			IAsset asset = A.Fake<IAsset>();
			A.CallTo(() => asset.browser_download_url).Returns("http://localhost/");
			return asset;
		}

		private static string GetThisVersion() {
			Version version = Assembly.GetExecutingAssembly().GetName().Version;
			return version.Major + "." + version.Minor + "." + version.Build;
		}

		private static string GetNewerVersion() {
			Version version = Assembly.GetExecutingAssembly().GetName().Version;
			return 1 + version.Major + "." + version.Minor + "." + version.Build;
		}

		private static WebException GetHttpWebException(HttpStatusCode statusCode) {
			SerializationInfo info = new SerializationInfo(typeof(HttpWebResponse), new FormatterConverter());
			info.AddValue("m_StatusCode", statusCode);
			info.AddValue("m_HttpResponseHeaders", new WebHeaderCollection());
			info.AddValue("m_Uri", new Uri("test://localhost/"));
			info.AddValue("m_Certificate", null);
			info.AddValue("m_Version", HttpVersion.Version11);
			info.AddValue("m_ContentLength", 0);
			info.AddValue("m_Verb", "GET");
			info.AddValue("m_StatusDescription", "test response");
			info.AddValue("m_MediaType", null);
			HttpWebResponse webResponse = new TestHttpWebResponse(info, new StreamingContext());
			return new WebException(nameof(GetHttpWebException), new Exception(), WebExceptionStatus.ProtocolError, webResponse);
		}

		private static WebException GetWebException(WebExceptionStatus status)
			=> new WebException(nameof(GetWebException), new Exception(), status, A.Fake<WebResponse>());

		private class TestHttpWebResponse : HttpWebResponse {
			public TestHttpWebResponse(SerializationInfo serializationInfo, StreamingContext streamingContext)
#pragma warning disable 0618
				// i know this is deprecated but for now it's a way to fake an HttpWebResponse
				: base(serializationInfo, streamingContext) { }
#pragma warning restore 0618
		}
	}
}
