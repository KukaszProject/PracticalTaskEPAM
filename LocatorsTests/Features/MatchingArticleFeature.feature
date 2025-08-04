Feature:  Article Title Matching on Insights Page
As a user, I want to verify that the article title displayed matches the expected title after navigation, ensuring correct content is shown.

Background: 
	Given I am on the EPAM home page

@UITests
Scenario: Verify article title after navigating through featured articles
	Given I navigate to the Insights Page
	When I click on the arrow <number> times
	And  I click on the read more button
	And I get the current article title
	Then The article title should match the expected title
	Examples: 
	| number |
	| 2      |