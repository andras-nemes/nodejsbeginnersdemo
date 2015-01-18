using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTester
{
	public class AddOrdersToCustomerRequest
	{
		[JsonProperty(PropertyName="customerId")]
		public String CustomerId { get; set; }
		[JsonProperty(PropertyName="orders")]
		public List<Order> NewOrders { get; set; }
	}
}
