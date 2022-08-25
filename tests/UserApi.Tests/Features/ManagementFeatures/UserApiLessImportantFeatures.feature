Feature: UserApiLessImportantFeatures
In Order to change user baned status
As an api consumer
I want to send post request with user id to change the status of ban or unbaned account


Scenario: change existing user baned status to ban
	Given Id of existing user
	When i request to update the user baned status by Id
	Then the response status code is '200 Ok'

Scenario: change non-existing user baned status to ban
	Given Id of non-existing user
	When i request to update the user baned status by Id
	Then the response status code is '404 Not Found'

Scenario: change user baned status to ban with invalid data
	When i request to update the user baned status by Id:
	Then the response status code is '404 Not Found'


Scenario: change existing user baned status to unBaned
	Given Id of existing user
	When i request to update the user baned status by Id
	Then the response status code is '200 Ok'

Scenario: change non-existing user baned status to unBaned
	Given Id of non-existing user
	When i request to update the user baned status by Id
	Then the response status code is '404 Not Found'

Scenario: change user baned status to unBaned with invalid data
	When i request to update the user baned status by Id:
	Then the response status code is '404 Not Found'
