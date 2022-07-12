@Search
Feature: Website search function
	In order to 
		find the expected information
	As a 
		customer
	I want to 
		have a well search expirience

Background: Open the Homepage
	Given I open the test page

@english
Scenario: Validate the search function
	Given I change the language to 'EN'
	When I'm searching for 'provision' 
	Then I expect more then 200 results

@german
Scenario: Validate the search detail function
	Given I change the language to 'DE'
	When I'm searching for 'Vorsorge' 
		And I select the 3. result
	Then The detail page should have the title ' | Swiss Life'
