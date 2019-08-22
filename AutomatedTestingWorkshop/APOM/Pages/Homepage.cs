using FunkyBDD.SxS.Framework.APOM.Organisms;
using FunkyBDD.SxS.Selenium.WebElement;
using OpenQA.Selenium;

namespace FunkyBDD.SxS.Framework.Selenium.Pages
{
    public class Homepage : Basepage
    {
        public Banner Banner;
        public DistributionTeaser DistributionTeaser;

        public Homepage(IWebDriver driver) : base(driver)
        {
            Banner = new Banner(Component);
            DistributionTeaser = new DistributionTeaser(Component);
        }

        public string Title => Component.GetDriver().Title;
    }
}
