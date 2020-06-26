using FluentAssertions;
using FluentAssertions.Execution;
using Newtonsoft.Json;
using NUnit.Allure.Core;
using NUnit.Framework;
using QaAutoTests.Api.BDD.Client;
using QaAutoTests.DataObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupTests
{
    [AllureNUnit]
    public class API
    {
        [OneTimeSetUp]

        public void Init()
        {
           // Environment.CurrentDirectory = Path.GetDirectoryName(GetType().Assembly.Location);
        }
        [Description("Meetup API Test")]
        [TestCaseSource("TestDataAPI")]
        public bool APITests(BillingOrder order)
        {
            ApiClient _apiClient = new ApiClient();

            var response = _apiClient.CreateBillingOrder(order);

            BillingOrder actualOrder = JsonConvert.DeserializeObject<BillingOrder>(response.Content);
            response = _apiClient.GetBillingOrder(actualOrder.Id);

            BillingOrder expectedOrder = JsonConvert.DeserializeObject<BillingOrder>(response.Content);

            actualOrder.Should().BeEquivalentTo(expectedOrder);

            order.Should().BeEquivalentTo(expectedOrder);

            return true;
        }


        [Description("Meetup API Test - Simple")]
        [TestCase]
        public void SimpleAPITests()
        {
            var _apiClient = new ApiClient();  
            
            var mainOrder = new BillingOrder();
           
            //Creating
            var response = _apiClient.CreateBillingOrder(mainOrder);

            BillingOrder createdOrder = JsonConvert.DeserializeObject<BillingOrder>(response.Content);

            //Reading
            response = _apiClient.GetBillingOrder(createdOrder.Id);

            BillingOrder receivedOrder = JsonConvert.DeserializeObject<BillingOrder>(response.Content);

            createdOrder.FirstName = "Test";
            createdOrder.Should().BeEquivalentTo(receivedOrder); //Fluent asserion 
            
            mainOrder.Should().BeEquivalentTo(receivedOrder, 
                options => options.Excluding(o=>o.Id));


            
        }



        public static IEnumerable TestDataAPI
        {
            get
            {
                yield return new TestCaseData(new BillingOrder() { LastName = "Smith" }).SetName("Positive: simple last name").Returns(true);
                yield return new TestCaseData(new BillingOrder() { LastName = "O'Brien" }).SetName("Positive: last name with apostrophe").Returns(true);
                yield return new TestCaseData(new BillingOrder() { LastName = "Smith-Klein" }).SetName("Positive: last name with hypen").Returns(true);
                yield return new TestCaseData(new BillingOrder() { LastName = "Li" }).SetName("Positive: short last name").Returns(true);
                yield return new TestCaseData(new BillingOrder() { ZipCode = "12345" }).SetName("Negative: five digits zip code").Returns(false);
                yield return new TestCaseData(new BillingOrder() { ZipCode = "123456789" }).SetName("Positive: nine digits zip code").Returns(false);
                yield return new TestCaseData(new BillingOrder() { ItemNumber = 1 }).SetName("Positive: order with first item").Returns(true);
                yield return new TestCaseData(new BillingOrder() { ItemNumber = 2 }).SetName("Positive: order with second item").Returns(true);
                yield return new TestCaseData(new BillingOrder() { ItemNumber = 3 }).SetName("Positive: order with third item").Returns(true);

                yield return new TestCaseData(new BillingOrder() { LastName = "" }).SetName("Negative: empty last name").Returns(false);
                yield return new TestCaseData(new BillingOrder() { Email = "123456789" }).SetName("Negative: only digits in email").Returns(false);
                yield return new TestCaseData(new BillingOrder() { Email = "email" }).SetName("Negative: only chars in email").Returns(false);
                yield return new TestCaseData(new BillingOrder() { Email = "email.com" }).SetName("Negative: email without @").Returns(false);
                yield return new TestCaseData(new BillingOrder() { Email = "email@gmail" }).SetName("Negative: email without domain").Returns(false);
            }
        }

        [OneTimeSetUp]
        public void oneTimeSetup() {
            Environment.CurrentDirectory = Path.GetDirectoryName(GetType().Assembly.Location);
        }

    }
}
