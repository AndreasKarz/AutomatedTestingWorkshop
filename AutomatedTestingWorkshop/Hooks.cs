using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;
using TechTalk.SpecFlow;

namespace AutomatedTestingWorkshop
{
    [Binding]
    public sealed class Hooks
    {
        public static IWebDriver Driver;
        public static OpenQA.Selenium.Support.UI.WebDriverWait Wait;
        private static string _browserName;
        private static string _testURL;

        private void _initDriver(string browser = "Chrome")
        {
            switch (browser)
            {
                case "Chrome":
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AcceptInsecureCertificates = true;
                    Driver = new ChromeDriver("./", chromeOptions);
                    break;
            }
            Wait = new OpenQA.Selenium.Support.UI.WebDriverWait(Driver, TimeSpan.FromSeconds(30));
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }

        [BeforeScenario(Order = 1)]
        public void BeforeScenario()
        {
            #region Set _browserName
                if (Environment.GetEnvironmentVariable("TEST_BROWSER") == null)
                {
                    _browserName = "Chrome";
                }
                else
                {
                    _browserName = Environment.GetEnvironmentVariable("TEST_BROWSER");
                }
            #endregion
        }

        [BeforeScenario(Order = 3)]
        public void InitDriver()
        {
            _initDriver(_browserName);
        }


        [Given(@"I open the test page")]
        public void IOpenTheTestPage()
        {
            #region Set _testURL
                if (Environment.GetEnvironmentVariable("TEST_URL") == null)
                {
                    _testURL = "http://automationpractice.com/";
                }
                else
                {
                    _testURL = Environment.GetEnvironmentVariable("TEST_URL");
                }
            #endregion
            Driver.Navigate().GoToUrl(_testURL);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Driver.Close();
            Driver.Dispose();
        }

        [AfterFeature]
        public static void TaskCleanUp()
        {
            Process[] prs = Process.GetProcesses();
            foreach (Process pr in prs)
            {
                if (pr.ProcessName == "chromedriver" || pr.ProcessName == "IEDriverServer" || pr.ProcessName == "IEDriverServer64" || pr.ProcessName == "geckodriver")
                {
                    try
                    {
                        pr.Kill();
                    }
                    catch
                    {

                    }
                }
            }
        }

    }
}
