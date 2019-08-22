@Styleguide
Feature: The base framework works well
	In order to 
		run automated tests
	As a 
		tester
	I want to 
		have a working base framework

Background: Open the test page
	Given I open the test page

Scenario: All packages correctly installed | Check the helpers
	Given I change the language to 'DE'
	When I confirm the disclaimer
	Then The homepage has the title 'Private | Swiss Life'
		And The page has the color 255, 53, 53, 53

Scenario Outline: Check the titel translation
	When I change the language to '<lang>'
	Then The banner title should be '<title>'
		And The family teaser title schould be '<familyTeaserTitle>'
		And The occupational teaser title schould be '<occupationalTeaserTitle>'
	Examples: 
	| lang | title                                          | familyTeaserTitle               | occupationalTeaserTitle    |
	| DE   | Die passende Vorsorge für jede Lebensphase     | Vorsorge für Familien           | Berufliche Vorsorge        |
	| EN   | The right provision for each stage of life     | Future provisions for families  | Occupational provisions    |
	| FR   | La prévoyance adaptée à chaque phase de la vie | La prévoyance pour les familles | Prévoyance professionnelle |
	| IT   | La previdenza giusta per ogni fase della vita  | Previdenza per la famiglia      | Previdenza professionale   |

Scenario: Check mouse over button
	When I mover over the family teaser
	Then The button should be shown

Scenario: Page 404 works
	When I call a page not exists
	Then The homepage has the title '404 | Swiss Life'

Scenario: Validate FunkyBDD.SxS.Helpers table comparer
	Then the TableComparer should work right
	| Col1    | Col2    | Col3    |
	| Value 1 | Value 2 | Value 3 |