﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.WindowsAzure.Mobile.Service;

namespace m2pService.DataObjects
{
	public class Profile : EntityData
	{
		public IList<Avocation> Interests { get; set; } //to czym ktos jest zainteresowany

		public IList<Event> MyEvents { get; set; } //wydarzenia na ktore jestem zapisany

		[ForeignKey("Id")]
		public virtual User User { get; set; }
	}
}