using Microsoft.WindowsAzure.Mobile.Service;

namespace m2pService.DataObjects
{
	public class Avocation : EntityData
	{
		public Activity Activity { get; set; }
		public Location Location { get; set; }
	}
}