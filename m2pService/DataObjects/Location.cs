using Microsoft.WindowsAzure.Mobile.Service;

namespace m2pService.DataObjects
{
	public class Location  : EntityData
	{
		public double Longitude { get; set; }
		public double Latitude { get; set; }

		public double Radius { get; set; }

		public string Name { get; set; }
	}
}