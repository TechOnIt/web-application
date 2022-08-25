Feature: UserApiDeleteAccountFeatures
In Order to Delete user Account
As an api consumer
I want to Delete User Account by sending user id as post request

@Delete
Scenario: remove an existing user account 
	Given Id of existing user
	When i request to remove the user account by Id
	Then the response status code is '200 Ok'

@Delete
Scenario: remove an non-existing user account
	Given Id of non-existing user
	When i request to remove the user account by Id
	Then the response status code is '404 Not Found'

@Delete
Scenario: remove an user account with invalid data
	When i request to remove the user account by Id:
	Then the response status code is '404 Not Found'


@Delete
Scenario: ForceDelete an existing user account 
	Given Id of existing user
	When i request to ForceDelete the user account by Id
	Then the response status code is '200 Ok'

@Delete
Scenario: ForceDelete an non-existing user account
	Given Id of non-existing user
	When i request to ForceDelete the user account by Id
	Then the response status code is '404 Not Found'

@Delete
Scenario: ForceDelete an user account with invalid data
	When i request to ForceDelete the user user account by Id:
	Then the response status code is '404 Not Found'
