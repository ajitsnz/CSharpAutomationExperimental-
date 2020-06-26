using Newtonsoft.Json;
using NUnit.Allure.Steps;
using NUnit.Framework;
using QaAutoTests.Dictionaries;
using QaAutoTests.Extensions;

namespace QaAutoTests.DataObjects
{
	public class BillingOrder
	{
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("firstName")]
        public string FirstName;

        [JsonProperty("lastName")]
        public string LastName;

        [JsonProperty("email")]
        public string Email;

        [JsonProperty("phone")]
        public string Phone;

        [JsonProperty("city")]
        public string City;

        [JsonProperty("zipCode")]
        public string ZipCode;

        [JsonProperty("state")]
        public State State;

        [JsonProperty("addressLine1")]
        public string AddressLine1;

        [JsonProperty("addressLine2")]
        public string AddressLine2;

        [JsonProperty("itemNumber")]
        public int ItemNumber;

        [JsonProperty("comment")]
        public string Comment;

        
        public BillingOrder(
			string firstName = null,
			string lastName = null,
			string email = null,
			string phone = null,
			string city = null,
			string zipCode = null,
			State state = State.AK,
			string addressLine1 = null,
			string addressLine2 = null,
			int itemNumber = 1,
			string comment = null)
		{
			FirstName = firstName ?? TestContext.CurrentContext.Random.GetString(6, "abcdefghijklmnopqrstuvwxyz ").FirstCharToUpper();
			LastName = lastName ?? TestContext.CurrentContext.Random.GetString(10, "abcdefghijklmnopqrstuvwxyz ").FirstCharToUpper();
			Email = email ?? "fake-email@gmail.com";
			Phone = phone ?? TestContext.CurrentContext.Random.GetString(10, "1234567890");
			City = city ?? TestContext.CurrentContext.Random.GetString(10, "abcdefghijklmnopqrstuvwxyz1234567890").FirstCharToUpper();
			ZipCode = zipCode ?? TestContext.CurrentContext.Random.GetString(6, "1234567890");
			State = state;
			AddressLine1 = addressLine1 ?? TestContext.CurrentContext.Random.GetString(6, "abcdefghijklmnopqrstuvwxyz").FirstCharToUpper();
			AddressLine2 = addressLine2 ?? TestContext.CurrentContext.Random.GetString(6, "abcdefghijklmnopqrstuvwxyz").FirstCharToUpper();
			ItemNumber = itemNumber;
			Comment = comment ?? TestContext.CurrentContext.Random.GetString(100, "abcde fghijklm nopqrstu vwxyz 12345 67890").FirstCharToUpper();
		}
	};
}