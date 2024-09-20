using System.Net;
using System.Threading.Tasks;
using au.IO.Web.API.GitHub.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace au.IO.Web.API.GitHub.Tests {
	[TestClass]
	public class ReposApiTests {
		private const string _username = "misterhaan";  // github username that owns the repository for this project
		private const string _repoFake = "trick7.org";  // repo doesn't exist for the username
		private const string _repoNoReleases = "track7.org";  // real repo that doesn't use releases
		private const string _repoWithReleases = "au.Shared";  // this is actually the repo that contains this project.  it had a release before au.IO.Web.API.GitHub was created.

		[TestMethod]
		public async Task LatestRelease_NoRepo_Throws404() {
			ReposApi api = GetReposApi(_repoFake);
			WebException caughtException = null;
			try {
				await api.LatestRelease().ConfigureAwait(false);
			} catch(WebException e) {
				caughtException = e;
			}
			Assert.AreEqual(WebExceptionStatus.ProtocolError, caughtException.Status, $"{nameof(api.LatestRelease)}() is expected to throw an error due to the API response returning an error code when the requested repo doesn't exist.  Did GitHub user {_username} create a repo named {_repoFake}?");
			Assert.AreEqual(HttpStatusCode.NotFound, (caughtException.Response as HttpWebResponse).StatusCode, $"{nameof(api.LatestRelease)}() is expected to throw an error with a 404 not found status code when the requested repo doesn't exist.  Did GitHub user {_username} create a repo named {_repoFake}?");
		}

		[TestMethod]
		public async Task LatestRelease_NoReleases_Throws404() {
			ReposApi api = GetReposApi(_repoNoReleases);
			WebException caughtException = null;
			try {
				await api.LatestRelease().ConfigureAwait(false);
			} catch(WebException e) {
				caughtException = e;
			}
			Assert.AreEqual(WebExceptionStatus.ProtocolError, caughtException.Status, $"{nameof(api.LatestRelease)}() is expected to throw an error due to the API response returning an error code when the requested repo doesn't have any releases.  Did GitHub repo {_username}/{_repoNoReleases} have a release?");
			Assert.AreEqual(HttpStatusCode.NotFound, (caughtException.Response as HttpWebResponse).StatusCode, $"{nameof(api.LatestRelease)}() is expected to throw an error with a 404 not found status code when the requested repo doesn't have any releases.  Did GitHub repo {_username}/{_repoNoReleases} have a release?");
		}

		[TestMethod]
		public async Task LatestRelease_MultipleReleases_ReturnsIRelease() {
			ReposApi api = GetReposApi(_repoWithReleases);
			IRelease latest = await api.LatestRelease().ConfigureAwait(false);
			Assert.IsNotNull(latest, $"{nameof(api.LatestRelease)}() should return an IRelease object for a repo that has at least one release.");
		}

		private static ReposApi GetReposApi(string repoName)
			=> new(_username, repoName);
	}
}
