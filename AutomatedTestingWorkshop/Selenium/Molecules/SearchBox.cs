using OpenQA.Selenium;
using Framework.Selenium.Atoms;

namespace Framework.Selenium.Molecules
{
    public class SearchBox
    {
        private IWebElement _component;
        public InputText Input;
        public Button Button;

        public SearchBox(IWebElement Parent, By by)
        {
            _component = Hooks.Driver.FindElement(by);
            Input = new InputText(_component);
            Button = new Button(_component);
        }
    }
}
