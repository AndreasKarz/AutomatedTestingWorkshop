using OpenQA.Selenium;
using SwissLife.Selenium.IWebElement.DefaultProps;

namespace Framework.Selenium.Atoms
{
    /// <summary>
    ///     Span Atom
    /// </summary>
    public class Span : DefaultProps
    {

        /// <param name="Parent">Reference to the parent component within which you want to search</param>
        public Span(IWebElement Parent, By by)
        {
            _component = Parent.FindElement(by);
        }

        /// <summary>
        ///     Get the text of the span
        /// </summary>
        public string Text => _component.Text;

    }
}
