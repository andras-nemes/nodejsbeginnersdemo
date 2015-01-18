using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTester
{
	public class Order
	{
		[JsonProperty(PropertyName = "item")]
		public string Item { get; set; }
		[JsonProperty(PropertyName = "quantity")]
		public int Quantity { get; set; }
		[JsonProperty(PropertyName = "itemPrice")]
		public decimal Price { get; set; }
	}
}
