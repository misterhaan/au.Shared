using System;
using System.Net;
using System.Reflection;
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

			IUpdateCheckResult result = await updates.CheckAsync().ConfigureAwait(false);

			Assert.IsFalse(result.Available, $"{nameof(updates.CheckAsync)}() should say there's no update available when the latest version matches the current version.");
			Assert.AreEqual(Messages.NoUpdateAlreadyLatest, result.Description, $"{nameof(updates.CheckAsync)}() should use the {nameof(Messages.NoUpdateAlreadyLatest)} message when the latest version matches the current version.");
		}

		[TestMethod]
		public async Task CheckAsync_NewerVersion_UpdateAvailable() {
			UpdateChecker updates = GetUpdateChecker(_repoNewerVersion);

			IUpdateCheckResult result = await updates.CheckAsync().ConfigureAwait(false);

			Assert.IsTrue(result.Available, $"{nameof(updates.CheckAsync)}() should say there's an update available when the latest version is newer than the current version.");
		}

		[TestMethod]
		public async Task CheckAsync_NoFiles_NoUpdateNoFiles() {
			UpdateChecker updates = GetUpdateChecker(_repoNoFiles);

			IUpdateCheckResult result = await updates.CheckAsync().ConfigureAwait(false);

			Assert.IsFalse(result.Available, $"{nameof(updates.CheckAsync)}() should say there's no update available when the latest version has no files.");
			Assert.AreEqual(Messages.NoUpdateNoFiles, result.Description, $"{nameof(updates.CheckAsync)}() should use the {nameof(Messages.NoUpdateNoFiles)} message when the latest version has no files.");
		}

		[TestMethod]
		public async Task CheckAsync_NotFoundException_NoUpdateNoReleases() {
			UpdateChecker updates = GetUpdateChecker("notFound", GetHttpWebException(HttpStatusCode.NotFound));

			IUpdateCheckResult result = await updates.CheckAsync().ConfigureAwait(false);

			Assert.IsFalse(result.Available, $"{nameof(updates.CheckAsync)}() should say there's no update available when there are no releases for the repo or it doesn't exist.");
			Assert.AreEqual(Messages.NoUpdateNoReleases, result.Description, $"{nameof(updates.CheckAsync)}() should use the {nameof(Messages.NoUpdateNoReleases)} message when there are no releases for the repo or it doesn't exist.");
		}

		[DataTestMethod]
		[DataRow(WebExceptionStatus.NameResolutionFailure)]
		[DataRow(WebExceptionStatus.ConnectFailure)]
		public async Task CheckAsync_CannotConnect_NoUpdateCannotConnect(WebExceptionStatus status) {
			WebException ex = GetWebException(status);
			UpdateChecker updates = GetUpdateChecker("otherException", ex);

			IUpdateCheckResult result = await updates.CheckAsync().ConfigureAwait(false);

			Assert.IsFalse(result.Available, $"{nameof(updates.CheckAsync)}() should say there's no update available when unable to connect to the API server.");
			Assert.AreEqual(Messages.NoUpdateCannotConnect, result.Description, $"{nameof(updates.CheckAsync)}() should use the {nameof(Messages.NoUpdateCannotConnect)} message when unable to connect to the API server.");
		}

		[DataTestMethod]
		[DataRow(HttpStatusCode.BadGateway)]
		[DataRow(HttpStatusCode.Unauthorized)]
		public async Task CheckAsync_OtherProtocolException_NoUpdateError(HttpStatusCode status) {
			WebException ex = GetHttpWebException(status);
			UpdateChecker updates = GetUpdateChecker("otherProtocolException", ex);

			IUpdateCheckResult result = await updates.CheckAsync().ConfigureAwait(false);

			Assert.IsFalse(result.Available, $"{nameof(updates.CheckAsync)}() should say there's no update available when the API returns an error response other than 404 not found.");
			Assert.AreEqual(string.Format(Messages.NoUpdateError, ex.Message), result.Description, $"{nameof(updates.CheckAsync)}() should use the {nameof(Messages.NoUpdateError)} message when the API returns an error response other than 404 not found.");
		}

		[DataTestMethod]
		[DataRow(WebExceptionStatus.Timeout)]
		[DataRow(WebExceptionStatus.UnknownError)]
		public async Task CheckAsync_UnexpectedException_NoUpdateError(WebExceptionStatus status) {
			WebException ex = GetWebException(status);
			UpdateChecker updates = GetUpdateChecker("otherException", ex);

			IUpdateCheckResult result = await updates.CheckAsync().ConfigureAwait(false);

			Assert.IsFalse(result.Available, $"{nameof(updates.CheckAsync)}() should say there's no update available when the API does not return a response.");
			Assert.AreEqual(string.Format(Messages.NoUpdateError, ex.Message), result.Description, $"{nameof(updates.CheckAsync)}() should use the {nameof(Messages.NoUpdateError)} message when the API does not return a response.");
		}

		private static UpdateChecker GetUpdateChecker(string repoName)
			=> GetUpdateChecker(repoName, null);

		private static UpdateChecker GetUpdateChecker(string repoName, WebException wex)
			=> new(_username, repoName, GetApiFactory(wex));

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
				A.CallTo(() => release.assets).Returns([GetAsset()]);
			return release;
		}

		private static IAsset GetAsset() {
			IAsset asset = A.Fake<IAsset>();
			A.CallTo(() => asset.browser_download_url).Returns("http://localhost/");
			return asset;
		}

		private static string GetThisVersion() {
			Version version = Assembly.GetEntryAssembly().GetName().Version;
			return version.Major + "." + version.Minor + "." + version.Build;
		}

		private static string GetNewerVersion() {
			Version version = Assembly.GetEntryAssembly().GetName().Version;
			return 1 + version.Major + "." + version.Minor + "." + version.Build;
		}

		private static WebException GetHttpWebException(HttpStatusCode statusCode) {
			HttpWebResponse webResponse = A.Fake<HttpWebResponse>();
			A.CallTo(() => webResponse.StatusCode).Returns(statusCode);
			return new WebException(nameof(GetHttpWebException), new Exception(), WebExceptionStatus.ProtocolError, webResponse);
		}

		private static WebException GetWebException(WebExceptionStatus status)
			=> new(nameof(GetWebException), new Exception(), status, A.Fake<WebResponse>());
	}
}
