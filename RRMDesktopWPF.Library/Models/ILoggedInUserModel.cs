using System;

namespace RRMDesktopWPF.Library.Models
{
	public interface ILoggedInUserModel
	{
		DateTime CreatedTime { get; set; }
		string EmailAddress { get; set; }
		string FirstName { get; set; }
		string Id { get; set; }
		string LastName { get; set; }
		string AccessToken { get; set; }
	}
}