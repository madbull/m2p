using System.Collections.Generic;


namespace meet2play.DataObjects
{
	public class Profile
	{
		public IList<Avocation> Interests { get; set; } //to czym ktos jest zainteresowany

		public IList<Event> MyEvents { get; set; } //wydarzenia na ktore jestem zapisany

		public virtual User User { get; set; }
	}
}