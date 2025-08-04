Feature: Job Search Functionality
As a job seeker, I want to search for remote jobs using specific keywords so that I can find relevant opportunities on the EPAM Careers page.

Background: 
	Given I am on the EPAM home page

@UITests
Scenario Outline: Searching for remote job opportunities using specific keywords
	Given I navigate to the Careers Page
	When I select remote work option
	And I enter "<keyword>" in the job search field
	And I select All Locations from location dropdown
	And I click the search button
	And I sort by date
	And I open latest job offer
	Then I should see job listings related to "<keyword>"
	Examples:
	| keyword |
	| .NET	|
	| Java  |