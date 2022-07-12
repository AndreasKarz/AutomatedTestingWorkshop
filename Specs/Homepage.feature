@Business @Homepage
Feature: Homepage elements
	In order to 
		find the expected information
	As a 
		customer
	I want to 
		see all relevant elements

Background: Open the test page
	Given I open the test page

@distribution
Scenario: All distribution teasers are visible
	Given I change the language to 'EN'
	Then I see the following distribution teasers
	| title                                       | subTitle             |
	| Future provisions and wealth accumulation   | To product selection |
	| Property and asset insurance                | To product selection |
	| Health insurance                            | To product selection |
	| Property financing and residential property | To product selection |
