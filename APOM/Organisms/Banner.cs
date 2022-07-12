using FunkyBDD.SxS.Framework.APOM.Atoms;
using FunkyBDD.SxS.Framework.APOM.Molecules;
using FunkyBDD.SxS.Selenium.APOM;
using FunkyBDD.SxS.Selenium.WebElement;
using OpenQA.Selenium;

namespace FunkyBDD.SxS.Framework.APOM.Organisms
{
    public class Banner: DefaultProps
    {
        public Header Header;
        public Teaser FamilyTeaser;
        public Teaser OccupationalTeaser;

        public Banner(IWebElement parent)
        {
            Component = parent.FindElementFirstOrDefault(By.CssSelector(".gridctrl"));
            Component.ScrollTo();
            Header = new Header(Component, By.TagName("h2"));
            FamilyTeaser = new Teaser(Component, By.CssSelector(".g-cell-1 .segmentandthemeteaser"));
            OccupationalTeaser = new Teaser(Component, By.CssSelector(".g-cell-2 .segmentandthemeteaser"));
        }
    }
}
