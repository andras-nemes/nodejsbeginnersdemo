using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTester
{
	public class ApiTesterService
	{
		public int TestDeleteFunction(string customerId)
		{
			HttpRequestMessage getRequest = new HttpRequestMessage(HttpMethod.Delete, new Uri("http://localhost:1337/customers/" + customerId));
			getRequest.Headers.ExpectContinue = false;
			HttpClient httpClient = new HttpClient();
			httpClient.Timeout = new TimeSpan(0, 10, 0);
			Task<HttpResponseMessage> httpRequest = httpClient.SendAsync(getRequest,
					HttpCompletionOption.ResponseContentRead, CancellationToken.None);
			HttpResponseMessage httpResponse = httpRequest.Result;
			HttpStatusCode statusCode = httpResponse.StatusCode;
			HttpContent responseContent = httpResponse.Content;
			if (responseContent != null)
			{
				Task<String> stringContentsTask = responseContent.ReadAsStringAsync();
				String stringContents = stringContentsTask.Result;
				if (statusCode == HttpStatusCode.OK)
				{
					return Convert.ToInt32(stringContents);
				}
				else
				{
					throw new Exception(string.Format("No customer deleted: {0}", stringContents));
				}
			}
			throw new Exception("No customer deleted");
		}

		public int TestUpdateFunction(String customerId, List<Order> newOrders)
		{
			HttpRequestMessage putRequest = new HttpRequestMessage(HttpMethod.Put, new Uri("http://localhost:1337/customers/"));
			putRequest.Headers.ExpectContinue = false;
			AddOrdersToCustomerRequest req = new AddOrdersToCustomerRequest() { CustomerId = customerId, NewOrders = newOrders };
			string jsonBody = JsonConvert.SerializeObject(req);
			putRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
			HttpClient httpClient = new HttpClient();
			httpClient.Timeout = new TimeSpan(0, 10, 0);
			Task<HttpResponseMessage> httpRequest = httpClient.SendAsync(putRequest,
					HttpCompletionOption.ResponseContentRead, CancellationToken.None);
			HttpResponseMessage httpResponse = httpRequest.Result;
			HttpStatusCode statusCode = httpResponse.StatusCode;

			HttpContent responseContent = httpResponse.Content;
			if (responseContent != null)
			{
				Task<String> stringContentsTask = responseContent.ReadAsStringAsync();
				String stringContents = stringContentsTask.Result;
				if (statusCode == HttpStatusCode.OK)
				{
					return Convert.ToInt32(stringContents);
				}
				else
				{
					throw new Exception(string.Format("No customer updated: {0}", stringContents));
				}
			}
			throw new Exception("No customer updated");
		}

		public List<Customer> GetAllCustomers()
		{			
			HttpRequestMessage getRequest = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost:1337/customers"));
			getRequest.Headers.ExpectContinue = false;
			HttpClient httpClient = new HttpClient();
			httpClient.Timeout = new TimeSpan(0, 10, 0);
			Task<HttpResponseMessage> httpRequest = httpClient.SendAsync(getRequest,
					HttpCompletionOption.ResponseContentRead, CancellationToken.None);
			HttpResponseMessage httpResponse = httpRequest.Result;
			HttpStatusCode statusCode = httpResponse.StatusCode;
			HttpContent responseContent = httpResponse.Content;
			if (responseContent != null)
			{
				Task<String> stringContentsTask = responseContent.ReadAsStringAsync();
				String stringContents = stringContentsTask.Result;
				List<Customer> allCustomers = JsonConvert.DeserializeObject<List<Customer>>(stringContents);
				return allCustomers;
			}

			throw new IOException("Exception when retrieving all customers");
		}

		public Customer GetSpecificCustomer(String id)
		{
			HttpRequestMessage getRequest = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost:1337/customers/" + id));
			getRequest.Headers.ExpectContinue = false;
			HttpClient httpClient = new HttpClient();
			httpClient.Timeout = new TimeSpan(0, 10, 0);
			Task<HttpResponseMessage> httpRequest = httpClient.SendAsync(getRequest,
					HttpCompletionOption.ResponseContentRead, CancellationToken.None);
			HttpResponseMessage httpResponse = httpRequest.Result;
			HttpStatusCode statusCode = httpResponse.StatusCode;
			
			HttpContent responseContent = httpResponse.Content;
			if (responseContent != null)
			{
				Task<String> stringContentsTask = responseContent.ReadAsStringAsync();
				String stringContents = stringContentsTask.Result;
				List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(stringContents);
				return customers[0];
			}

			throw new IOException("Exception when retrieving single customer.");
		}

		public Customer TestCustomerCreation(String customerName)
		{
			HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, new Uri("http://localhost:1337/customers/"));
			postRequest.Headers.ExpectContinue = false;
			InsertCustomerRequest req = new InsertCustomerRequest() { CustomerName = customerName };
			string jsonBody = JsonConvert.SerializeObject(req);
			postRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
			HttpClient httpClient = new HttpClient();
			httpClient.Timeout = new TimeSpan(0, 10, 0);
			Task<HttpResponseMessage> httpRequest = httpClient.SendAsync(postRequest,
					HttpCompletionOption.ResponseContentRead, CancellationToken.None);
			HttpResponseMessage httpResponse = httpRequest.Result;
			HttpStatusCode statusCode = httpResponse.StatusCode;

			HttpContent responseContent = httpResponse.Content;
			if (responseContent != null)
			{
				Task<String> stringContentsTask = responseContent.ReadAsStringAsync();
				String stringContents = stringContentsTask.Result;
				if (statusCode == HttpStatusCode.Created)
				{
					List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(stringContents);
					return customers[0];
				}
				else
				{
					throw new Exception(string.Format("No customer created: {0}", stringContents));
				}
			}
			throw new Exception("No customer created");
		}
	}
}
