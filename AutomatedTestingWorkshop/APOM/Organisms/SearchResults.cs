using FunkyBDD.SxS.Framework.APOM.Molecules;
using FunkyBDD.SxS.Selenium.APOM;
using FunkyBDD.SxS.Selenium.WebElement;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace FunkyBDD.SxS.Framework.APOM.Organisms
{
    public class SearchResults: DefaultProps
    {
        private readonly List<SearchResult> searchResults = new List<SearchResult>();

        public SearchResults(IWebElement parent)
        { 
            var list = parent.FindElementsOrDefault(By.CssSelector(".o-search-result__list > li"));
            searchResults.AddRange(from el in list
                                   let result = new SearchResult(el)
                                   select result);
        }

        public void SelectResult(int index)
        {
            var result = searchResults[index].Component;
            result.ScrollTo();
            result.FindElement(By.TagName("a")).Click();
        }
    }
}
