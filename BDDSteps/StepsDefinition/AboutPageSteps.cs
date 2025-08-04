using Core.Utilities;
using Business.Pages;
using Reqnroll;
using OpenQA.Selenium;
using Core.Drivers;
using NUnit.Framework;

namespace BDDSteps.StepsDefinition
{
    [Binding]
    [Category("UITests")]
    public class AboutPageSteps
    {
        private IWebDriver Driver;
        private readonly AboutPage aboutPage;
        private FileHelper fileHelper = new FileHelper();

        public AboutPageSteps()
        {
            Driver = DriverFactory.GetDriver();
            aboutPage = new AboutPage(Driver);
        }

        [When("I click the download button")]
        public void WhenIClickTheDownloadButton()
        {
            aboutPage.DownloadButtonClicked();
        }

        [Then("the file should be downloaded")]
        public void ThenTheFileShouldBeDownloaded()
        {
            Assert.That(fileHelper
                    .WaitForFileDownload(Path.Combine(Directory.GetCurrentDirectory(),
                    "Downloads"), "EPAM_Corporate_Overview*.pdf", 10), "File was not downloaded successfully.");
        }
    }
}
