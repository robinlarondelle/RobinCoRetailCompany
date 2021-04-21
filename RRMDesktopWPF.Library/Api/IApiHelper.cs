using System.Threading.Tasks;

using RRMDesktopWPF.Library.Models;

namespace RRMDesktopWPF.Library.Api
{
	public interface IApiHelper
	{
		Task<AuthenticatedUser> Authenticate( string username , string password );
		Task GetLoggedInUserInformation(string token);
	}
}