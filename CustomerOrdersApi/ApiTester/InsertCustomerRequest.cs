using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTester
{
	public class InsertCustomerRequest
	{
		[JsonProperty(PropertyName="customerName")]
		public String CustomerName { get; set; }
	}
}
