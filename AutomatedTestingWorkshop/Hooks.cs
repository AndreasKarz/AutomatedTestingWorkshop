using FunkyBDD.SxS.Selenium.Browserstack;
using FunkyBDD.SxS.Selenium.WebDriver;
using FunkyBDD.SxS.Selenium.WebElement;
using OpenQA.Selenium;
using SwissLife.SxS.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using TechTalk.SpecFlow;

namespace FunkyBDD.SxS.Framework
{
    [Binding]
    public sealed class Hooks
    {
        public static IWebDriver Driver;
        public static Browser Browser;
        private static string _testURL;

        [BeforeFeature]
        public static void BeforeFeature()
        {
            if (!FeatureContext.Current.FeatureInfo.Tags.Contains("API"))
            { 
                if (Environment.GetEnvironmentVariable("TEST_BROWSER") == null)
                {
                    Browser = new Browser("FirefoxLocal");
                }
                else
                {
                    Browser = new Browser(Environment.GetEnvironmentVariable("TEST_BROWSER"));
                }
                Driver = Browser.Driver;
                Driver.SetSeleniumFlag();
            }
        }

        [Given(@"I open the test page")]
        public void IOpenTheTestPage()
        {
            if (Environment.GetEnvironmentVariable("TEST_URL") == null)
            {
                _testURL = "https://www.swisslife.ch/";
            }
            else
            {
                _testURL = Environment.GetEnvironmentVariable("TEST_URL");
            }
            Driver.Navigate().GoToUrl(_testURL);
        }

        [Given(@"I change the language to '(.*)'"), When(@"I change the language to '(.*)'")]
        public void GivenIChangeTheLanguageTo(string lang)
        {
            ReadOnlyCollection<IWebElement> langNavi = Driver.FindElementsOrDefault(By.CssSelector("[class$='language'] li"), 10);
            var navElement = langNavi.Where<IWebElement>(el => el.Text == lang).FirstOrDefault<IWebElement>();
            if(navElement != null)
            {
                navElement.ScrollTo();
                navElement.Click();
            }
            else
            { 
                throw new NoSuchElementException($"No navigation element for lang {lang} found.");
            }
        }

        [Given(@"I confirm the disclaimer"), When(@"I confirm the disclaimer")]
        public void WhenIConfirmTheDisclaimer()
        {
            var disclaimerButton = Driver.FindElementFirstOrDefault(By.CssSelector("[class*='cookie-disclaimer']>button"), 1);
            if (disclaimerButton != null)
            {
                disclaimerButton.Click();
            }
        }

        [AfterScenario]
        public void AfterScenarion()
        {
            if(Driver != null) 
            { 
                var screenShotFileName = $"{FeatureContext.Current.FeatureInfo.Title}__{ScenarioContext.Current.ScenarioInfo.Title}.png";
                screenShotFileName = FileHelpers.RemoveIllegalFileNameChars(screenShotFileName);
                Driver.MakeScreenshot(screenShotFileName);
            }
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            if(Browser != null)
            { 
                Browser.DisposeDriver();
            }
        }

    }
}
