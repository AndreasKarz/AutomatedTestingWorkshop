using OpenQA.Selenium;
using SwissLife.Selenium.IWebElement.DefaultProps;

namespace Framework.Selenium.Atoms
{
    /// <summary>
    ///     Input Type Text atom
    /// </summary>
    public class InputText : DefaultProps
    {
        /// <param name="Parent">Reference to the parent component within which you want to search</param>
        public InputText(IWebElement Parent)
        {
            _component = Parent.FindElement(By.XPath(".//input[@type='text']"));
        }


        /// <summary>
        ///     Get the text of the input field
        /// </summary>
        public string Text => _component.Text;

        /// <summary>
        ///     Set the focus into the input field
        /// </summary>
        public void FocusIn() => _component.SendKeys("");

        /// <summary>
        ///     Remove the focus from the input field
        /// </summary>
        public void FocusOut() => _component.SendKeys(Keys.Tab);

        /// <summary>
        ///     Clear the input field
        /// </summary>
        public void Clear() => _component.Clear();

        /// <summary>
        ///     Write text into the input field
        /// </summary>
        /// <param name="Text"></param>
        public void SendKeys(string Text) => _component.SendKeys(Text);

    }
}
