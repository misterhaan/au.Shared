using System.Threading.Tasks;

namespace au.IO.Web.API.GitHub.Types {
	public interface IReposApi {
		/// <summary>
		/// Get the latest release for the repository.
		/// </summary>
		/// <returns>Latest release for the repository</returns>
		Task<IRelease> LatestRelease();
	}
}
