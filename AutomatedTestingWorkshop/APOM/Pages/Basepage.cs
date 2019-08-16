using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using FunkyBDD.SxS.Selenium.APOM;

namespace AutomatedTestingWorkshop.APOM.Pages
{
    public class Basepage : DefaultProps
    {
        public readonly IWebElement WebElement;
        public readonly DefaultWait<IWebDriver> Wait;

        public Basepage(IWebDriver driver)
        {
            Wait = new DefaultWait<IWebDriver>(driver)
            {
                PollingInterval = TimeSpan.FromMilliseconds(330),
                Timeout = TimeSpan.FromSeconds(5)
            };
            WebElement = Wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("body")));
        }
    }
}
