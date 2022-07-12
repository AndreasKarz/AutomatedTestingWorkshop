using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using FunkyBDD.SxS.Selenium.APOM;

namespace FunkyBDD.SxS.Framework.Selenium.Pages
{
    public class Basepage : DefaultProps
    {
        public readonly DefaultWait<IWebDriver> Wait;

        public Basepage(IWebDriver driver)
        {
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            Component = Wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("body")));
        }

        public void SearchFor(string term)
        {
            Component.SendKeys(Keys.Home);
            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("a11y-header-search-link"))).Click();
            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("gsc-i-id1"))).SendKeys(term);
        }
    }
}
