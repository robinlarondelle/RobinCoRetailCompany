using System;

namespace RRMDataManager.Library.Models
{
	public class UserModel
	{
		public string ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string EmailAddress { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}