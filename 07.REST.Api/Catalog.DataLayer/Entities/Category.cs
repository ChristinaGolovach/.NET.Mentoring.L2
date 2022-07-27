﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.DataLayer.Entities
{
	public class Category: Entity
	{
		public string Name { get; set; }

		public ICollection<Item> Items { get; set; }
	}
}
