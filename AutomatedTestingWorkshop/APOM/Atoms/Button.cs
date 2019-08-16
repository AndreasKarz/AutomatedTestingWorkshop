using OpenQA.Selenium;
using FunkyBDD.SxS.Selenium.WebElement;
using FunkyBDD.SxS.Selenium.WebDriver;
using FunkyBDD.SxS.Selenium.APOM;

namespace Framework.Selenium.Atoms
{
    /// <summary>
    ///     Button Atom
    /// </summary>
    public class Button : DefaultProps
    {

        /// <param name="Parent">Reference to the parent component within which you want to search</param>
        public Button(IWebElement Parent)
        {
            Component = Parent.FindElement(By.TagName("button"));
            var driver = Component.GetDriver();
            driver.SetSeleniumFlag();
        }

        /// <summary>
        ///     Init a click on the button
        /// </summary>
        public void Click() => Component.Click();

        /// <summary>
        ///     Get the icon of the button
        /// </summary>
        public string Icon => Component.Text;

    }
}
