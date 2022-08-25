Feature: UserApiUpdateFeatures
In Order to Update users
As an api consumer
I want to Update user info by sending request with details and Id

Scenario: updating a user with valid data
	Given that a user exists in the system
	When i request to update the user by id and details Name: 'testName' Surname: 'testSurname' Email: 'testEmail@gamil.com' ConcurrencyStamp: 'CreateInstance<Concurrency>'
	Then the user should be updated
	And the response status code is '200 ok'

Scenario: updating a non-existing user
	Given that a user dose not exists in the system
	When i request to update the user by id and details Name: 'testName' Surname: 'testSurname' Email: 'testEmail@gamil.com' ConcurrencyStamp: 'CreateInstance<Concurrency>'
	Then the response status code is '404 Not Found'

	Scenario: updating an user without id
	Given that a user dose not exists in the system
	When i request to update the user without id and details Name: 'testName' Surname: 'testSurname' Email: 'testEmail@gamil.com' ConcurrencyStamp: 'CreateInstance<Concurrency>' and Id:
	Then the response status code is '404 Not Found'


Scenario Outline: updating a user with invalid data
	Given that a user exists or dose not exists in the system
	When i request to update the user by id and details Name '<name>' Surname '<surname>' Email '<email>' ConcurrencyStamp '<concurrencystamp>'
	Then The response status code is '400 Bad Request'
Examples:
	| name                                                 | surname                                              | email               | concurrencystamp            |
	| testName                                             | testSurname                                          | testEmail@gamil.com | CreateInstance<Concurrency> |
	| testName                                             | testSurname                                          |                     | CreateInstance<Concurrency> |
	| testName                                             | testSurname                                          | testEmail@gamil.com |                             |
	| aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa | testSurname                                          | testEmail@gamil.com | CreateInstance<Concurrency> |
	| testName                                             | aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa | testEmail@gamil.com | CreateInstance<Concurrency> |
	| testName                                             |                                                      | testEmail@gamil.com | CreateInstance<Concurrency> |
	| testName                                             | testSurname                                          | testemail           | CreateInstance<Concurrency> |
	|                                                      |                                                      |                     |                             |
