using FunkyBDD.SxS.Framework.APOM.Organisms;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FunkyBDD.SxS.Framework.Selenium.Pages
{
    public class SearchResultPage : Basepage
    {
        public SearchResults Results;
        public SearchResultPage(IWebDriver driver) : base(driver)
        {
            Component = Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[data-analytics-search='search']")));
            Results = new SearchResults(Component);
        }

        public int ResultCount => int.Parse(Component.FindElement(By.CssSelector("[data-result-count]")).GetAttribute("data-result-count"));
    }
}
