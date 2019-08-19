using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using FunkyBDD.SxS.Selenium.APOM;
using FunkyBDD.SxS.Selenium.WebElement;
using FunkyBDD.SxS.Framework.APOM.Atoms;

namespace FunkyBDD.SxS.Framework.APOM.Molecules
{
    public class Teaser: DefaultProps
    {
        public Header Header;

        public Teaser(IWebElement parent, By by)
        { 
            Component = parent.FindElementFirstOrDefault(by);
            Header = new Header(Component, By.TagName("h4"));
        }

        public void MouserOver() { 
                Actions actions = new Actions(Driver);
                actions.MoveToElement(Component);
                actions.Build().Perform();
        }

        public bool IsButtonClickable
        {
            get {
                var button = Component.FindElementFirstOrDefault(By.ClassName("a-button"));
                return (button != null && button.Enabled && button.Displayed);
           }
        }
    }
}
