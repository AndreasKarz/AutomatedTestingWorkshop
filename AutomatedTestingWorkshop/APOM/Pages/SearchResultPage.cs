using OpenQA.Selenium;
using Framework.Selenium.Organisms;

namespace Framework.Selenium.Pages
{
    public class SearchResultPage
    {
        private IWebElement _component;
        public SearchResults SearchResults;

        public SearchResultPage()
        {
            _component = Hooks.Driver.FindElement(By.TagName("body"));
            SearchResults = new SearchResults(_component, By.Id("center_column"));
        }
    }
}
