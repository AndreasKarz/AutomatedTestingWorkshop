using OpenQA.Selenium;
using FunkyBDD.SxS.Selenium.WebDriver;
using FunkyBDD.SxS.Selenium.WebElement;
using FunkyBDD.SxS.Selenium.APOM;
using AutomatedTestingWorkshop.APOM.Pages;

namespace Framework.Selenium.Pages
{
    public class Homepage : Basepage
    {
        public Homepage(IWebDriver driver) : base(driver)
        {

        }

        public string Title => WebElement.GetDriver().Title;
    }
}
