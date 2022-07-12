using OpenQA.Selenium;
using FunkyBDD.SxS.Selenium.APOM;
using FunkyBDD.SxS.Selenium.WebElement;

namespace FunkyBDD.SxS.Framework.APOM.Molecules
{
    public class DistributionTeaserItem : DefaultProps
    {
        private readonly IWebElement child;

        public DistributionTeaserItem(IWebElement self)
        {
            Component = self;
            child = self.FindElementFirstOrDefault(By.CssSelector("[data-teaser-title]"));
        }

        public void Click() => child.Click();

        public string Title => child.GetAttribute("data-teaser-title").Trim();

        public string SubTitle => child.Text.Trim();
    }
}
