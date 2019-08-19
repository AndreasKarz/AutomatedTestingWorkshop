using FunkyBDD.SxS.Framework.APOM.Organisms;
using FunkyBDD.SxS.Selenium.WebElement;
using OpenQA.Selenium;

namespace FunkyBDD.SxS.Framework.Selenium.Pages
{
    public class Homepage : Basepage
    {
        public Banner Banner;

        public Homepage(IWebDriver driver) : base(driver)
        {
            Banner = new Banner(Component);
        }

        public string Title => Component.GetDriver().Title;
    }
}
