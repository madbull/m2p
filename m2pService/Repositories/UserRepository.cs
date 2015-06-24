using System.Linq;
using System.Threading.Tasks;
using Facebook;
using m2pService.DataObjects;
using Microsoft.WindowsAzure.Mobile.Service.Security;

namespace m2pService.Repositories
{
	public class UserRepository
	{
		public async Task<User> CreateUser(ServiceUser serviceUser)
		{
			var indentites = await serviceUser.GetIdentitiesAsync();
			var fb = indentites.OfType<FacebookCredentials>().FirstOrDefault();
			var go = indentites.OfType<GoogleCredentials>().FirstOrDefault();
			var user = new User()
			{
				Id = serviceUser.Id
			};

			if (fb != null)
			{
				var client = new FacebookClient(fb.AccessToken);
				dynamic me = client.Get("me", new { fields = "name,id" });
				user.Type = UserType.Facebook;
				user.Name = me.name;
			}
			else if (go != null)
			{
				user.Type = UserType.Google;
				//TODO
			}
			return user;
		} 
	}
}