Feature: UserApiManagement
In Order to Create Update Delete and Get users
As an api consumer
I want to impliment CRUD operations for users by sending requests restfull post,delete,get,put,patch


Scenario: Create new user
	Given new user information by details Name: 'testName' Surname: 'testSurname' Email: 'testEmail@gamil.com' PhoneNumber: '09124133486' and Password: 'Aa123456@'
	When I request to create a new user by details 
	Then the response should be user Id return as 'Result<Guid>'

Scenario Outline: creating a user with invalid data
	Given new user information and details Name: '<name>' Surname '<surname>' Email '<email>' Password '<password>' PhoneNumber '<phoneNumber>'
	When I request to create a new user and details 
	Then The response status code is '400 Bad Request'
Examples:
	| name                                                 | surname                                              | email               | password  | phoneNumber |
	| testName                                             | testSurname                                          | testEmail@gamil.com | Aa123456@ |             |
	| testName                                             | testSurname                                          |                     | Aa123456@ | 09124133486 |
	| testName                                             | testSurname                                          | testEmail@gamil.com |           | 09124133486 |
	| aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa | testSurname                                          | testEmail@gamil.com | Aa123456@ | 09124133486 |
	| testName                                             | aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa | testEmail@gamil.com | Aa123456@ | 09124133486 |
	|                                                      |                                                      |                     |           |             |
