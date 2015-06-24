using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using m2pService.DataObjects;
using m2pService.Models;
using Microsoft.WindowsAzure.Mobile.Service.Security;

namespace m2pService.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.User)]
    public class ActivityController : TableController<Activity>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            m2pContext context = new m2pContext();
            DomainManager = new EntityDomainManager<Activity>(context, Request, Services);
        }

        // GET tables/Activity
        public IQueryable<Activity> GetAllActivity()
        {
	        return Query(); 
        }

        // GET tables/Activity/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Activity> GetActivity(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Activity/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Activity> PatchActivity(string id, Delta<Activity> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Activity
        public async Task<IHttpActionResult> PostActivity(Activity item)
        {
            Activity current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Activity/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteActivity(string id)
        {
             return DeleteAsync(id);
        }

    }
}