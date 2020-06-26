using System.Net;
using Newtonsoft.Json;
using NUnit.Framework;
using QaAutoTests.Api.BDD.Client;
using QaAutoTests.DataObjects;

namespace RestSharpExample
{
	[TestFixture]
	public class BillingOrderTests
	{
		[SetUp]
		public void SetUp()
		{
			_apiClient = new ApiClient();
		}

		[Test]
		public void CreateBillingOrderTest()
		{
			var order = new BillingOrder();
			var response = _apiClient.CreateBillingOrder(order);

			Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
		}

		[Test]
		public void GetBillingOrdersTest()
		{
			var order = new BillingOrder();
			_apiClient.CreateBillingOrder(order);
			var response = _apiClient.GetBillingOrders();
			TestContext.WriteLine(response.Content);

			Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
		}

		[Test]
		public void GetBillingOrderTest()
		{
			var order = new BillingOrder();
			var response = _apiClient.CreateBillingOrder(order);
			BillingOrder createdOrder = JsonConvert.DeserializeObject<BillingOrder>(response.Content);
			response = _apiClient.GetBillingOrder(createdOrder.Id);
			BillingOrder receivedOrder = JsonConvert.DeserializeObject<BillingOrder>(response.Content);

			Assert.Multiple(() =>
			{
				Assert.AreEqual(createdOrder.Id, receivedOrder.Id);
				Assert.AreEqual(createdOrder.FirstName, receivedOrder.FirstName);
				Assert.AreEqual(createdOrder.LastName, receivedOrder.LastName);
				Assert.AreEqual(createdOrder.Phone, receivedOrder.Phone);
				Assert.AreEqual(createdOrder.Email, receivedOrder.Email);
			});
		}

		[Test]
		public void DeleteBillingOrderTest()
		{
			var order = new BillingOrder();
			var response = _apiClient.CreateBillingOrder(order);
			BillingOrder createdOrder = JsonConvert.DeserializeObject<BillingOrder>(response.Content);
			response = _apiClient.DeleteBillingOrder(createdOrder.Id);

			Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
			Assert.AreEqual(response.Content, string.Empty);
		}


		private ApiClient _apiClient;
	}
}
