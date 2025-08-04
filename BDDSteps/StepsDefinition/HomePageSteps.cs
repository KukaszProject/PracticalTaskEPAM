using Core.Utilities;
using Business.Pages;
using OpenQA.Selenium;
using Reqnroll;
using Core.Drivers;
using NUnit.Framework;

namespace BDDSteps.StepsDefinition
{
    [Binding]
    [Category("UITests")]
    public class HomePageSteps
    {
        private IWebDriver Driver;
        private readonly HomePage homePage;
        private readonly HomePageActions homePageActions;

        public HomePageSteps()
        {
            Driver = DriverFactory.GetDriver();
            homePage = new HomePage(Driver);
            homePageActions = new HomePageActions(Driver);
        }

        [Given(@"I am on the EPAM home page")]
        public void GivenIAmOnTheEPAMHomePage()
        {
            Driver.Navigate().GoToUrl(ConfigHelper.Get("BaseUrl"));
            homePage.AcceptCookies();
        }

        [Given(@"I navigate to the About Page")]
        public void GivenINavigateToTheAboutPage()
        {
            homePage.GoToAbout();
        }

        [Given("I navigate to the Global Search")]
        public void GivenINavigateToTheGlobalSearch()
        {
            homePage.ClickSearchIcon();
        }

        [Given("I navigate to the Careers Page")]
        public void GivenINavigateToTheCareersPage()
        {
            homePage.GoToCareers();
        }

        [Given("I navigate to the Insights Page")]
        public void GivenINavigateToTheInsightsPage()
        {
            homePage.GoToInsights();
        }

        [Given("I open Services navigation bar")]
        public void GivenIOpenServicesNavigationBar()
        {
            homePageActions.OpenServicesNavigationBar();
        }

        [When(@"I click on the ""(.*)"" service category")]
        public void WhenIClickOnTheServiceCategory(string category)
        {
            homePage.NavigateToCategory(category);
        }
    }
}