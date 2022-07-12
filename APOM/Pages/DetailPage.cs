using FunkyBDD.SxS.Selenium.WebElement;
using OpenQA.Selenium;

namespace FunkyBDD.SxS.Framework.Selenium.Pages
{
    public class DetailPage : Basepage
    {
        public DetailPage(IWebDriver driver) : base(driver)
        {
            
        }

        public string Title => Component.GetDriver().Title;
    }
}
