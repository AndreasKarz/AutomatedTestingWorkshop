using FunkyBDD.SxS.Selenium.APOM;
using OpenQA.Selenium;

namespace FunkyBDD.SxS.Framework.APOM.Atoms
{
    public class Header : DefaultProps
    {
        public Header(IWebElement parent, By by)
        {
            Component = parent.FindElement(by);
        }

        public string Text => Component.Text;
    }
}
