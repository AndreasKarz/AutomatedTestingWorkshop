Feature: Gherkin samples
	In order to 
		get an example of all the possibilities 
	As a 
		tester
	I want to 
		see the possibilities in action

Background: Open the browser
	Given I open the test page

Scenario: Show tops for women
	# Given is set by background
		And I have click on the Tab WOMEN
	When I click on the category Tops
	Then I see two items

Scenario Outline: Test the search function
	# Given is set by background
	When I search for <therm>
	Then I will recieve <count> results
	Examples: 
	| therm | count |
	| shoe  | 7     |
	| funky | 0     |

Scenario: Check the tabs
	# Given is set by background
	Then I see all my tabs
    | Tab | Label    |
    | 1   | WOMEN    |
    | 2   | DRESSES  |
    | 3   | T-SHIRTS |

@Firefox
Scenario: Test only for Firefox
	# Given is set by background