using Newtonsoft.Json;
using NUnit.Allure.Steps;
using NUnit.Framework;
using QaAutoTests.DataObjects;
using RestSharp;

namespace QaAutoTests.Api.BDD.Client
{
	class ApiClient
	{

        [AllureStep("Creating Billing Order")]
        public IRestResponse CreateBillingOrder(BillingOrder order)
		{
			var client = new RestClient(BASE_URL);
			var request = new RestRequest(BILLING_ORDER_ENDPOINT, Method.POST);
			request.AddJsonBody(JsonConvert.SerializeObject(order));
			var response = client.Execute(request);
            TestContext.WriteLine(response.Content);
			return response;
		}

        [AllureStep("GetAll Billing Order")]
        public IRestResponse GetBillingOrders()
		{
			var client = new RestClient(BASE_URL);
			var request = new RestRequest(BILLING_ORDER_ENDPOINT, Method.GET);
			var response = client.Execute(request);
            TestContext.WriteLine(response.Content);
            return response;
		}

        [AllureStep("Get Single Billing Order")]
        public IRestResponse GetBillingOrder(int id)
		{
			var client = new RestClient(BASE_URL);
			var request = new RestRequest(BILLING_ORDER_ENDPOINT + $"/{id.ToString()}", Method.GET);
			var response = client.Execute(request);
            TestContext.WriteLine(response.Content);
            return response;
		}

        [AllureStep("Deleting Billing Order")]
        public IRestResponse DeleteBillingOrder(int id)
		{
			var client = new RestClient(BASE_URL);
			var request = new RestRequest(BILLING_ORDER_ENDPOINT + $"/{id.ToString()}", Method.DELETE);
			var response = client.Execute(request);
            TestContext.WriteLine(response.Content);
            return response;
		}

        [AllureStep("Updating Billing Order")]
        public IRestResponse UpdateBillingOrder(int id)
        {
            var client = new RestClient(BASE_URL);
            var request = new RestRequest(BILLING_ORDER_ENDPOINT + $"/{id.ToString()}", Method.PUT);
            var response = client.Execute(request);
            TestContext.WriteLine(response.Content);
            return response;
        }

        private const string BASE_URL = "http://localhost:8282";
		private const string BILLING_ORDER_ENDPOINT = "/BillingOrder";
	}
}
