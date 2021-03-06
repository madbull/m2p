﻿using System;
using Microsoft.WindowsAzure.Mobile.Service;

namespace m2pService.DataObjects
{
	public class Event : EntityData
	{
		public Activity Activity { get; set; }
		public Location Location { get; set; }

		public DateTime Date { get; set; }
	}
}