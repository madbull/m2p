using System.Linq;
using m2pService.DataObjects;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Tables;

namespace m2pService.Controllers
{
	public class ActivityController : TableController<Activity>
	{
		// GET tables/TodoItem
		public IQueryable<Activity> GetAllTodoItems()
		{
			return Query();
		}
	}
}