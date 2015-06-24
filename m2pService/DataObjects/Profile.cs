using System.Collections.Generic;
using Microsoft.WindowsAzure.Mobile.Service;

namespace m2pService.DataObjects
{
	public class Profile : EntityData
	{
		public IList<Avocation> Interests { get; set; } //to czym ktos jest zainteresowany

		public IList<Event> MyEvents { get; set; } //wydarzenia na ktore jestem zapisany
	}
}