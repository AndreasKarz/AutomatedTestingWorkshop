using FunkyBDD.SxS.Selenium.APOM;
using OpenQA.Selenium;

namespace FunkyBDD.SxS.Framework.APOM.Molecules
{
    public class SearchResult: DefaultProps
    {
        public SearchResult(IWebElement self)
        { 
            Component = self;
        }
    }
}
