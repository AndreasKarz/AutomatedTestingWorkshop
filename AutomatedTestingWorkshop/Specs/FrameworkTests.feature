Feature: The base framework works well
	In order to 
		run automated tests
	As a 
		tester
	I want to 
		have a working base framework

Scenario: All packages correctly installed
	Given I open the test page
		And I change the language to DE
	When I confirm the disclaimer
	Then The homepage has the title 'Private | Swiss Life'