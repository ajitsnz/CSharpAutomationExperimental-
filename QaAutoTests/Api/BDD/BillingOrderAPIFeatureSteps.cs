using NUnit.Framework;
using System.Net;
using RestSharp;
using TechTalk.SpecFlow;
using QaAutoTests.DataObjects;
using QaAutoTests.Api.BDD.Client;
using System.IO;
using System;

namespace QaAutoTests.Api.BDD
{
	[Binding]
	public class BillingOrderAPIFeatureSteps 
	{
        [OneTimeSetUp]
        public void Init()
        {
            Environment.CurrentDirectory = Path.GetDirectoryName(GetType().Assembly.Location);
        }


        [Given(@"I have correct billing order")]
		public void GivenIHaveCorrectBillingOrder()
		{
            _order = new BillingOrder();
		}

		[Given(@"I have correct billing order with params: (.*), (.*), (.*), (.*)")]
		public void GivenIHaveCorrectBillingOrderWithParams(string firstName, string lastName, string email, string phone)
		{
			_order = new BillingOrder(firstName: firstName, lastName: lastName, email: email, phone: phone);
		}

		[When(@"I send this order to API via POST request")]
		public void WhenISendThisOrderToAPIViaPOSTRequest()
		{
			var apiClient = new ApiClient();
            _response = apiClient.CreateBillingOrder(_order);
            TestContext.WriteLine(_response.Content);
		}

		[Then(@"I receive response with correct HTTP code")]
		public void ThenIReceiveResponseWithCorrectHTTPCode()
		{
			Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
		}

		private BillingOrder _order;
		private IRestResponse _response;
	}
}
