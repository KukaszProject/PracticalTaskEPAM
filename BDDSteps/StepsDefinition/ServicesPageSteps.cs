using Business.Pages;
using Core.Drivers;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;

namespace BDDSteps.StepsDefinition
{
    [Binding]
    [Category("UITests")]
    public class ServicesPageSteps
    {
        private IWebDriver Driver;
        private readonly ServicesPage servicesPage;

        public ServicesPageSteps()
        {
            Driver = DriverFactory.GetDriver();
            servicesPage = new ServicesPage(Driver);
        }

        [Then(@"""(.*)"" should contain the expected text")]
        public void ThenShouldContainTheExpectedText(string category)
        {
            Assert.That(servicesPage.IsTitleMatchingCategory(category),
                "The article title does not match the expected title from the carousel.");
        }

        [Then("Our Related Expertise should be displayed")]
        public void ThenOurRelatedExpertiseShouldBeDisplayed()
        {
            Assert.That(servicesPage.IsRelatedExpertiseSectionVisible(),
                "The 'Our Related Expertise' section is not visible.");
        }
    }
}