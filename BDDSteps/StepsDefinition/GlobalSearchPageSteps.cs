using Business.Pages;
using Core.Drivers;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;

namespace BDDSteps.StepsDefinition
{
    [Binding]
    [Category("UITests")]
    public class GlobalSearchPageSteps
    {
        private IWebDriver Driver;
        private readonly GlobalSearchPage globalSearchPage;

        public GlobalSearchPageSteps()
        {
            Driver = DriverFactory.GetDriver();
            globalSearchPage = new GlobalSearchPage(Driver);
        }

        [When(@"I enter ""(.*)"" in the search field")]
        public void WhenIEnterKeywordInTheSearchField(string keyword)
        {
            globalSearchPage.Search(keyword);
        }

        [When("I click the find button")]
        public void WhenIClickTheFindButton()
        {
            globalSearchPage.ClickFindButton();
        }

        [Then("I should see search results related to \"(.*)\"")]
        public void ThenIShouldSeeSearchResultsRelatedToKeyword(string keyword)
        {
            Assert.That(globalSearchPage.AllResultsContain(keyword), $"All results should contain: {keyword}");
        }
    }
}
