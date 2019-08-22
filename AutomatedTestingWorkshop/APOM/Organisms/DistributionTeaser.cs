using OpenQA.Selenium;
using FunkyBDD.SxS.Selenium.APOM;
using FunkyBDD.SxS.Selenium.WebElement;
using FunkyBDD.SxS.Framework.APOM.Atoms;
using FunkyBDD.SxS.Framework.APOM.Molecules;
using System.Collections.Generic;

namespace FunkyBDD.SxS.Framework.APOM.Organisms
{
    public class DistributionTeaser : DefaultProps
    {
        public Header Header;
        public List<DistributionTeaserItem> Items = new List<DistributionTeaserItem>();

        public DistributionTeaser(IWebElement parent)
        {
            Component = parent.FindElementFirstOrDefault(By.ClassName("distributionteaser"));
            Header = new Header(Component, By.TagName("h3"));
            foreach (var item in Component.FindElementsOrDefault(By.TagName("li")))
            {
                Items.Add(new DistributionTeaserItem(item));
            }
        }
    }
}
