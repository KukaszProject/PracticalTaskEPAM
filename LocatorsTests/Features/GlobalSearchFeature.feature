Feature: Global Search Functionality
As a user, I want to perform a global search from the EPAM homepage so that I can quickly find relevant content.

Background: 
	Given I am on the EPAM home page

@UITests
Scenario Outline: Display relevant results for various search keywords
	Given I navigate to the Global Search
	When I enter "<keyword>" in the search field
	And  I click the find button
	Then I should see search results related to "<keyword>"
	Examples:
	| keyword    |
	| BLOCKCHAIN  |
	| Automation |
	| Cloud      |
