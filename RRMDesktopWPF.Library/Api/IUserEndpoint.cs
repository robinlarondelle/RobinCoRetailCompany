using System.Collections.Generic;
using System.Threading.Tasks;

using RRMDesktopWPF.Library.Models;

namespace RRMDesktopWPF.Library.Api
{
	public interface IUserEndpoint
	{
		Task<List<UserModel>> GetAllUsers();
	}
}