using OpenQA.Selenium;
using Framework.Selenium.Organisms;

namespace Framework.Selenium.Pages
{
    public class Homepage
    {
        private IWebElement _component;
        public HeaderRow Header;

        public Homepage()
        {
            _component = Hooks.Driver.FindElement(By.TagName("body"));
            Header = new HeaderRow(_component, By.CssSelector("#header div:nth-of-type(3) .container .row"));
        }
    }
}
