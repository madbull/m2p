using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using m2pService.DataObjects;
using m2pService.Models;
using m2pService.Repositories;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Security;

namespace m2pService.Controllers
{
    public class ProfileController : TableController<Profile>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            m2pContext context = new m2pContext();
            DomainManager = new EntityDomainManager<Profile>(context, Request, Services);
        }

        // GET tables/Profile
        public IQueryable<Profile> GetAllProfile()
        {
            return Query(); 
        }

        // GET tables/Profile/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public async Task<SingleResult<Profile>> GetProfile(string id)
        {
	        var result = Lookup(id);
			var serviceUser = (ServiceUser)User;
	        if (!result.Queryable.Any() && id == serviceUser.Id)
	        {
		        var user = await new UserRepository().CreateUser(serviceUser);

		        var profile = new Profile()
		        {
			        Interests = new List<Avocation>(),
					MyEvents = new List<Event>(),
					User = user
		        };
		        await InsertAsync(profile);
				return Lookup(id);
	        }
	        
	        return result;
        }

	    // PATCH tables/Profile/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Profile> PatchProfile(string id, Delta<Profile> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Profile
        public async Task<IHttpActionResult> PostProfile(Profile item)
        {
            Profile current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Profile/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteProfile(string id)
        {
             return DeleteAsync(id);
        }

    }
}