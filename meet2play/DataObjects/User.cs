namespace meet2play.DataObjects
{
	public class User
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