using Business.Pages;
using Core.Drivers;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;

namespace BDDSteps.StepsDefinition
{
    [Binding]
    [Category("UITests")]
    public class ArticleDetailsPageSteps
    {
        private IWebDriver Driver;
        private readonly ArticleDetailsPage articleDetailsPage;
        private readonly InsightsPage insightsPage;

        public ArticleDetailsPageSteps()
        {
            Driver = DriverFactory.GetDriver();
            articleDetailsPage = new ArticleDetailsPage(Driver);
            insightsPage = new InsightsPage(Driver);
        }

        [When("I get the current article title")]
        public void WhenIGetTheCurrentArticleTitle()
        {
            articleDetailsPage.GetCurrentArticleTitle();
        }

        [Then("The article title should match the expected title")]
        public void ThenTheArticleTitleShouldMatchTheExpectedTitle()
        {
            Assert.That(articleDetailsPage.IsArticleTitleMatching(insightsPage.GetTitleOnCarousel()),
                "The article title does not match the expected title from the carousel.");
        }
    }
}