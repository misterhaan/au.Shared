using System.Threading.Tasks;

namespace au.IO.Web.API.GitHub.Types {
	/// <summary>
	/// Checks for an update to the running application.
	/// </summary>
	public interface IUpdateChecker {
		/// <summary>
		/// Check for an update in the specified repository.
		/// </summary>
		/// <returns></returns>
		Task<IUpdateCheckResult> CheckAsync();
	}
}
