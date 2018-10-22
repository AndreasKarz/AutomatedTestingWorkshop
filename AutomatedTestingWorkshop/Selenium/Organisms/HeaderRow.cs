using OpenQA.Selenium;
using Framework.Selenium.Molecules;

namespace Framework.Selenium.Organisms
{
    public class HeaderRow
    {
        private IWebElement _component;
        public SearchBox Search;

        public HeaderRow(IWebElement Parent, By by)
        {
            _component = Hooks.Driver.FindElement(by);
            Search = new SearchBox(_component, By.Id("searchbox"));
        }
    }
}
