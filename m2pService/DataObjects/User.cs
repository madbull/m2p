using Microsoft.WindowsAzure.Mobile.Service;

namespace m2pService.DataObjects
{
	public class User : EntityData
	{
		public string Name { get; set; }
		public UserType Type { get; set; }
	}

	public enum UserType
	{
		Facebook,
		Google
	}
}