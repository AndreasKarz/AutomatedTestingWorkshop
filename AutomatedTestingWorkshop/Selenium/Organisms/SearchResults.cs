using OpenQA.Selenium;
using Framework.Selenium.Molecules;

namespace Framework.Selenium.Organisms
{
    public class SearchResults
    {
        private IWebElement _component;
        public SearchResultHeader Header;

        public SearchResults(IWebElement Parent, By by)
        {
            _component = Hooks.Driver.FindElement(by);
            Header = new SearchResultHeader(_component, By.ClassName("page-heading"));
        }
    }
}
