using Tests.Base;
using Business.Pages;

namespace Tests.UITests
{
    [TestFixture]
    public class GlobalSearchTests : TestBase
    {
        [Category("UITests")]
        [TestCase("BLOCKCHAIN")]
        [TestCase("Cloud")]
        [TestCase("Automation")]
        public void ValidateGlobalSearch(string term)
        {
            var home = new HomePage(Driver);
            var globalSearchPage = new GlobalSearchPage(Driver);

            home.AcceptCookies()
                .ClickSearchIcon()
                .Search(term)
                .ClickFindButton();

            Assert.That(globalSearchPage.AllResultsContain(term), $"All results should contain: {term}");
        }
    }
}

