using System;

namespace RRMDesktopWPF.Library.Models
{
	public class LoggedInUserModel : ILoggedInUserModel
	{
		public string AccessToken { get; set; }
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string EmailAddress { get; set; }
		public DateTime CreatedTime { get; set; }

		public void Clear()
		{
			AccessToken = "";
			Id = "";
			FirstName = "";
			LastName = "";
			EmailAddress = "";
			CreatedTime = DateTime.MinValue;
		}
	}
}