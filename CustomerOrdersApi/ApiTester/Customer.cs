using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTester
{
	public class Customer
	{
		[JsonProperty(PropertyName = "_id")]
		public String Id { get; set; }
		[JsonProperty(PropertyName="name")]
		public String Name { get; set; }
		[JsonProperty(PropertyName="orders")]
		public List<Order> Orders { get; set; }
	}
}
