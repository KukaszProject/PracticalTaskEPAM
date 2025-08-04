using System.Configuration;
using Core.Drivers;
using Core.Utilities;
using log4net;
using log4net.Config;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;

namespace Tests.BDD
{
    [Binding]
    public class TestHooks
    {
        protected IWebDriver? Driver;
        protected ILog Log => LogManager.GetLogger(GetType());

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            TestEnvironmentSetup.RunBeforeAllTests();
            var logConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log.config");

            if (!File.Exists(logConfigPath))
            {
                throw new FileNotFoundException($"Log.config not found at: {logConfigPath}");
            }

            XmlConfigurator.Configure(new FileInfo(logConfigPath));
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            Log.Info("---Test setup started.---");
            Driver = DriverFactory.GetDriver();
            if (Driver == null)
            {
                Log.Error("Driver initialization failed. Driver is null.");
                throw new InvalidOperationException("Driver initialization failed. Driver is null.");
            }

            var baseUrl = ConfigHelper.Get("BaseUrl");
            if (string.IsNullOrEmpty(baseUrl))
            {
                Log.Error("BaseUrl configuration is missing or null.");
                throw new ConfigurationErrorsException("BaseUrl configuration is missing or null.");
            }

            Driver.Navigate().GoToUrl(baseUrl);
            Log.Info("---Test setup complete.---");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Log.Info("---Test teardown started.---");
            var outcome = TestContext.CurrentContext.Result.Outcome.Status;
            if (outcome == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                if (Driver != null)
                {
                    var screenshotPath = ScreenshotMaker.TakeScreenshot(Driver, TestContext.CurrentContext.Test.Name);
                    Log.Error($"Test failed. Screenshot saved to: {screenshotPath}");
                }
                else
                {
                    Log.Error("Driver is null. Unable to take a screenshot.");
                }
            }

            DriverFactory.QuitDriver();
            Log.Info("---Test teardown complete.---");
        }
    }
}