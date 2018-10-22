using OpenQA.Selenium;
using SwissLife.Selenium.IWebElement.DefaultProps;

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
            _component = Parent.FindElement(By.TagName("button"));
        }

        /// <summary>
        ///     Init a click on the button
        /// </summary>
        public void Click() => _component.Click();

        /// <summary>
        ///     Get the icon of the button
        /// </summary>
        public string Icon => _component.Text;

    }
}
