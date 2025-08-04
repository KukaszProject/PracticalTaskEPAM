using Business.Pages;
using Core.Drivers;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;

namespace BDDSteps.StepsDefinition
{
    [Binding]
    [Category("UITests")]
    public class InsightsPageSteps
    {
        private IWebDriver Driver;
        private readonly InsightsPage insightsPage;

        public InsightsPageSteps()
        {
            Driver = DriverFactory.GetDriver();
            insightsPage = new InsightsPage(Driver);
        }

        [When(@"I click on the arrow {int} times")]
        public void WhenIClickOnTheArrowTimes(int number)
        {
            insightsPage.ClickOnArrow(number);
        }

        [When(@"I click on the read more button")]
        public void WhenIClickOnTheReadMoreButton()
        {
            insightsPage.ClickOnReadMore();
        }
    }
}
