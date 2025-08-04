Feature: Services Navigation and Validation
As a user, I want to navigate to specific service categories under the Artificial Intelligence section to verify their content and related expertise.

Background:
	Given I am on the EPAM home page

  @UITests
  Scenario: Navigate to Generative AI service category by hovering navigation bar and validate content
    Given I open Services navigation bar
    When I click on the "<ServiceCategory>" service category
    Then "<ServiceCategory>" should contain the expected text
	And Our Related Expertise should be displayed
    Examples:
      | ServiceCategory  |
      | Generative AI    |
      | Responsible AI   |