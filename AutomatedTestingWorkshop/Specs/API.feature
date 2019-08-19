@API
Feature: API testing
	In order to 
		understand API testing
	As a 
		Developer
	I want to 
		have an example

Scenario: Request user 2
	When I request user 2 of the dummy api
	Then the response status should be 'OK'
