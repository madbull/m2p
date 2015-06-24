using System;

namespace meet2play.DataObjects
{
	public class Event
	{
		public Activity Activity { get; set; }
		public Location Location { get; set; }

		public DateTime Date { get; set; }
	}
}