Feature: UserApiChangePasswordFeatures
In Order to Update users password and set new password for them
As an api consumer
I want to set new password for user by sending post request with user id and new password

@update
Scenario: updating user password with valid data
	Given new password and existing user data as SetUserPasswordCommand
	When i request to update the user password by Id and details Password: 'Aa123456@' and RepeatPassword: 'Aa123456@'
	Then the response status code is '200 Ok'

@update
Scenario: updating non-exists user password with valid data
	Given new password and existing user data as SetUserPasswordCommand
	When i request to update the user password by Id and details Password: 'Aa123456@' and RepeatPassword: 'Aa123456@'
	Then the response status code is '400 Not Found'

@update
Scenario: updating an user password with empty id
	Given new password and existing user data as SetUserPasswordCommand
	When i request to update the user password without Id and details Password: 'Aa123456@' and RepeatPassword: 'Aa123456@' and Id:
	Then the response status code is '400 Not Found'

@update
Scenario Outline: updating user password with invalid data
	Given that a user exists or dose not exists in the system
	When i request to set new password for user by Id and details Password: '<password>' RepeatPassword '<repeatpassword>'
	Then The response status code is '400 Bad Request'
Examples:
	| password  | RepeatPassword |
	|           |                |
	| Aa123456@ | sdfgsdfgsdfg   |
	| Aa123     | Aa123          |
	| Aa123456@ |                |
	|           | Aa123456@      |
	