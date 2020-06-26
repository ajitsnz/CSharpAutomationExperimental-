using FluentAssertions;
using Newtonsoft.Json;
using NGenerics.DataStructures.Mathematical;
using NUnit.Allure.Core;
using NUnit.Framework;
using QaAutoTests.Api.BDD.Client;
using QaAutoTests.DataObjects;
using QaAutoTests.Pages;
using QaAutoTests.Tests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupTests
{
    [AllureNUnit]
    public class Web : BaseTest
    {


        [SetUp]
        public void SetUp()
        {
            var authorizationPage = new AuthorizationPage(Driver);

            authorizationPage.GoToPage("http://qaauto.co.nz/billing-order-form/").LogIn("Testing");
        }


        [Description("Meetup WebTest Test")]
        [TestCaseSource("TestDataWeb")]
        public bool WebTests(BillingOrder order)
        {
            var billingOrderPage = new BillingOrderPage(Driver);
            billingOrderPage.SendOrderForm(order);
            return billingOrderPage.IsSuccessMessageDisplayed();
        }
                                   

        public static IEnumerable TestDataWeb
        {
            get
            {
                yield return new TestCaseData(new BillingOrder() { LastName = "Smith" }).SetName("Positive: simple last name").Returns(true);
                yield return new TestCaseData(new BillingOrder() { LastName = "O'Brien" }).SetName("Positive: last name with apostrophe").Returns(true);
                yield return new TestCaseData(new BillingOrder() { LastName = "Smith-Klein" }).SetName("Positive: last name with hypen").Returns(true);
                yield return new TestCaseData(new BillingOrder() { LastName = "Li" }).SetName("Positive: short last name").Returns(true);
                yield return new TestCaseData(new BillingOrder() { ZipCode = "12345" }).SetName("Positive: five digits zip code").Returns(true);
                yield return new TestCaseData(new BillingOrder() { ZipCode = "123456789" }).SetName("Positive: nine digits zip code").Returns(true);
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



    }
}
