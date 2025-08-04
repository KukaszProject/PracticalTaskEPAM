Feature: File Download from About Page
As a user, I want to download a file from the About page so that I can access its contents offline.

Background: 
	Given I am on the EPAM home page

@UITests
Scenario: Successful file download from the About page
	Given I navigate to the About Page
	When I click the download button
	Then the file should be downloaded