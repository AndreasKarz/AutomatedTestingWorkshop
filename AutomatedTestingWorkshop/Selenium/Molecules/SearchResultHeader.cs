using OpenQA.Selenium;
using Framework.Selenium.Atoms;
using System.Text.RegularExpressions;

namespace Framework.Selenium.Organisms
{
    public class SearchResultHeader
    {
        private IWebElement _component;
        public Span CounterText;
        private Regex _countRegex = new Regex("\\d");

        public SearchResultHeader(IWebElement Parent, By by)
        {
            _component = Hooks.Driver.FindElement(by);
            CounterText = new Span(_component, By.ClassName("heading-counter"));
        }

        public string CounterMatches => _countRegex.Match(CounterText.Text).Value;
    }
}
