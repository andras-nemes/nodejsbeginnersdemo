using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTester
{
	class Program
	{
		static void Main(string[] args)
		{			
			//TestCustomerRetrieval();
			//TestCustomerInsertion();
			//TestCustomerUpdate();
			TestCustomerDeletion();

			Console.WriteLine("Main done...");
			Console.ReadKey();
		}

		private static void TestCustomerDeletion()
		{
			Console.WriteLine("Testing item deletion.");
			Console.WriteLine("=================================");
			try
			{
				ApiTesterService service = new ApiTesterService();
				List<Customer> allCustomers = service.GetAllCustomers();
				Customer customer = SelectRandom(allCustomers);

				int deletedItemsCount = service.TestDeleteFunction(customer.Id);
				Console.WriteLine("Deleted customer {0} ", customer.Name);
				Console.WriteLine("Deleted items count: {0}", deletedItemsCount);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception caught while testing DELETE: {0}", ex.Message);
			}

			Console.WriteLine("=================================");
			Console.WriteLine("End of DELETE operation test.");
		}

		private static void TestCustomerUpdate()
		{
			Console.WriteLine("Testing item update.");
			Console.WriteLine("=================================");
			try
			{
				ApiTesterService service = new ApiTesterService();
				List<Customer> allCustomers = service.GetAllCustomers();
				Customer customer = SelectRandom(allCustomers);
				List<Order> newOrders = new List<Order>()
				{
					new Order(){Item = "Food", Price = 2, Quantity = 3}
					, new Order(){Item = "Drink", Price = 3, Quantity = 4}
					, new Order(){Item = "Taxi", Price = 10, Quantity = 1}
				};
				int updatedItemsCount = service.TestUpdateFunction(customer.Id, newOrders);
				Console.WriteLine("Updated customer {0} ", customer.Name);
				Console.WriteLine("Updated items count: {0}", updatedItemsCount);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception caught while testing PUT: {0}", ex.Message);
			}

			Console.WriteLine("=================================");
			Console.WriteLine("End of PUT operation test.");
		}

		private static void TestCustomerInsertion()
		{
			Console.Write("Customer name: ");
			string customerName = Console.ReadLine();
			ApiTesterService service = new ApiTesterService();
			try
			{
				Customer customer = service.TestCustomerCreation(customerName);
				if (customer != null)
				{
					Console.WriteLine("New customer id: {0}", customer.Id);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private static void TestCustomerRetrieval()
		{
			Console.WriteLine("Testing item retrieval.");
			Console.WriteLine("Retrieving all customers:");
			Console.WriteLine("=================================");
			ApiTesterService service = new ApiTesterService();
			try
			{
				List<Customer> allCustomers = service.GetAllCustomers();
				Console.WriteLine("Found {0} customers: ", allCustomers.Count);
				foreach (Customer c in allCustomers)
				{
					Console.WriteLine("Id: {0}, name: {1}, has {2} order(s).", c.Id, c.Name, c.Orders.Count);
					foreach (Order o in c.Orders)
					{
						Console.WriteLine("Item: {0}, price: {1}, quantity: {2}", o.Item, o.Price, o.Quantity);
					}
				}

				Console.WriteLine();
				Customer customer = SelectRandom(allCustomers);
				Console.WriteLine("Retrieving single customer with ID {0}.", customer.Id);
				Customer getById = service.GetSpecificCustomer(customer.Id);

				Console.WriteLine("Id: {0}, name: {1}, has {2} order(s).", getById.Id, getById.Name, getById.Orders.Count);
				foreach (Order o in getById.Orders)
				{
					Console.WriteLine("Item: {0}, prigetByIde: {1}, quantity: {2}", o.Item, o.Price, o.Quantity);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception caught while testing GET: {0}", ex.Message);
			}

			Console.WriteLine("=================================");
			Console.WriteLine("End of item retrieval tests.");
		}

		private static Customer SelectRandom(List<Customer> allCustomers)
		{
			Random random = new Random();
			int i = random.Next(0, allCustomers.Count);
			return allCustomers[i];
		}
	}
}
