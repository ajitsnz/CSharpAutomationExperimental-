Feature: Billing order API feature
	Client can send order via API

Scenario: POST billing order (simple scenario)
	Given I have correct billing order
	When I send this order to API via POST request
	Then I receive response with correct HTTP code

Scenario Outline: POST billing order
	Given I have correct billing order with params: <firstName>, <lastName>, <email>, <phone>
	When I send this order to API via POST request
	Then I receive response with correct HTTP code

	Examples:
	| firstName | lastName | email            | phone      |
	| John      | Smith    | client1@gmail.ru | 1234567890 |
	| Mike      | Do       | client2@gmail.ru | 1234567891 |
	| Luke      | White    | client3@gmail.ru | 1234567892 |


